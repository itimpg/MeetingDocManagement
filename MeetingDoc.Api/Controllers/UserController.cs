using System.Threading.Tasks;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MeetingDoc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _manager;

        public UserController(IUserManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult Get(UserCriteria criteria)
        {
            return Ok(_manager.Get(criteria));
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
    }
}