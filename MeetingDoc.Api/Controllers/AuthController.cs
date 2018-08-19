using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetingDoc.Api.Loggers;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MeetingDoc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthManager _authManager;
        private readonly ILogger _logger;

        public AuthController(
            IAuthManager authManager,
            ILogger<AuthController> logger)
        {
            _authManager = authManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _authManager.LoginAsync(username, password);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel userViewModel, string password)
        {
            if (await _authManager.IsUserExistsAsync(userViewModel.Email))
            {
                return BadRequest($"user with name {userViewModel.Email} is already exists.");
            }

            userViewModel.Email = userViewModel.Email.ToLowerInvariant();
            var user = await _authManager.RegisterAsync(userViewModel, password);

            return StatusCode(201);
        }
    }
}