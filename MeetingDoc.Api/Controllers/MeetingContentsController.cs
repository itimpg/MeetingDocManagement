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
    public class MeetingContentsController : ControllerBase
    {
        private readonly IMeetingContentManager _meetingContentManager;

        public MeetingContentsController(IMeetingContentManager meetingTimeManager)
        {
            _meetingContentManager = meetingTimeManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]MeetingContentCriteria criteria)
        {
            var viewModels = await _meetingContentManager.GetAsync(criteria);
            this.Response.AddPagination(
                viewModels.CurrentPage, viewModels.PageSize, viewModels.TotalCount, viewModels.TotalPages);

            return Ok(viewModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            MeetingContentViewModel viewModel = await _meetingContentManager.GetAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return Ok(viewModel);
        }

        [HttpPost()]
        public async Task<IActionResult> Add(MeetingContentViewModel viewModel)
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingContentManager.AddAsync(viewModel, id);
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> Update(MeetingContentViewModel viewModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingContentManager.UpdateAsync(viewModel, userId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingContentManager.DeleteAsync(id, userId);
            return Ok();
        }
    }
}