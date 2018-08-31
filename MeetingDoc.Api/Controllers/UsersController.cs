using System.Security.Claims;
using System.Threading.Tasks;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingDoc.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _manager;

        public UsersController(IUserManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_manager.Get(new UserCriteria()));
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

        [HttpPost("{id}")]
        public async Task<IActionResult> Add(int id, UserViewModel viewModel)
        {
            await _manager.AddAsync(viewModel, id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserViewModel viewModel)
        {
            await _manager.UpdateAsync(viewModel, id);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id, int objId)
        {
            await _manager.DeleteAsync(objId, id);
            return Ok();
        }
    }
}