using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MeetingDoc.Api.Loggers;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace MeetingDoc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly IUserManager _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public AuthController(
            IAuthManager authManager,
            IUserManager userManager,
            IConfiguration configuration,
            ILogger<AuthController> logger)
        {
            _authManager = authManager;
            _configuration = configuration;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var user = await _authManager.LoginAsync(
                loginViewModel.Username.ToLower(), loginViewModel.Password);
            if (user == null)
            {
                return BadRequest("Username / Password is incorrect.");
            }

            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Level.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserViewModel userViewModel)
        {
            if (await _authManager.IsUserExistsAsync(userViewModel.Email))
            {
                return BadRequest($"User with email {userViewModel.Email} is already exists.");
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            userViewModel.Email = userViewModel.Email.ToLowerInvariant();
            var user = await _authManager.RegisterAsync(userViewModel, userViewModel.Password, userId);

            return StatusCode(201);
        }

        [HttpPut("ChangePassword/{id}")]
        public async Task<IActionResult> ChangePassword(int id, ChangePasswordViewModel changePasswordViewModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (id == 0 || id != userId)
            {
                return Unauthorized();
            }

            var user = await _userManager.GetAsync(id);
            var loginUser = await _authManager.LoginAsync(user.Email.ToLower(), changePasswordViewModel.oldPassword);
            if (loginUser == null)
            {
                return BadRequest("Old password is incorrect");
            }

            await _authManager.ChangePassword(user.Id, changePasswordViewModel.newPassword);

            return Ok();
        }
    }
}