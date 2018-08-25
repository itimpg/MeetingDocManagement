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
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public AuthController(
            IAuthManager authManager,
            IConfiguration configuration,
            ILogger<AuthController> logger)
        {
            _authManager = authManager;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var user = await _authManager.LoginAsync("root", "P@ssw0rd");
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var user = await _authManager.LoginAsync(
                loginViewModel.Username.ToLower(), loginViewModel.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
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
        public async Task<IActionResult> Register(UserViewModel userViewModel, string password)
        {
            if (await _authManager.IsUserExistsAsync(userViewModel.Email))
            {
                return BadRequest($"User with email {userViewModel.Email} is already exists.");
            }

            userViewModel.Email = userViewModel.Email.ToLowerInvariant();
            var user = await _authManager.RegisterAsync(userViewModel, password);

            return StatusCode(201);
        }
    }
}