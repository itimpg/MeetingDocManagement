using System.Security.Claims;
using System.Threading.Tasks;
using MeetingDoc.Api.Helpers;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingDoc.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingTypesController : ControllerBase
    {
        private readonly IMeetingTypeManager _manager;

        public MeetingTypesController(IMeetingTypeManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]MeetingTypeCriteria criteria)
        {
            var users = await _manager.GetAsync(criteria);
            this.Response.AddPagination(
                users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            MeetingTypeViewModel viewModel = await _manager.GetAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return Ok(viewModel);
        }

        [HttpPost()]
        public async Task<IActionResult> Add(MeetingTypeViewModel viewModel)
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _manager.AddAsync(viewModel, id);
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> Update(MeetingTypeViewModel viewModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _manager.UpdateAsync(viewModel, userId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _manager.DeleteAsync(id, userId);
            return Ok();
        }
    }
}