using System.Security.Claims;
using System.Threading.Tasks;
using MeetingDoc.Api.Helpers;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MeetingDoc.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _manager;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserManager manager, ILogger<UsersController> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]UserCriteria criteria)
        {
            var users = await _manager.GetAsync(criteria);
            this.Response.AddPagination(
                users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            UserViewModel viewModel = await _manager.GetAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return Ok(viewModel);
        }

        [HttpPost()]
        public async Task<IActionResult> Add(UserViewModel viewModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _manager.AddAsync(viewModel, userId);
            var user = await _manager.GetAsync(userId);
            _logger.LogInformation($"{user.Email} Add User : {JsonConvert.SerializeObject(viewModel)}");
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> Update(UserViewModel viewModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _manager.UpdateAsync(viewModel, userId);
            var user = await _manager.GetAsync(userId);
            _logger.LogInformation($"{user.Email} Edit User : {JsonConvert.SerializeObject(viewModel)}");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (id == 1)
            {
                return BadRequest("Default Admin cannot be deleted.");
            }

            await _manager.DeleteAsync(id, userId);
            var user = await _manager.GetAsync(userId);
            _logger.LogInformation($"{user.Email} Delete User : {id}");
            return Ok();
        }
    }
}