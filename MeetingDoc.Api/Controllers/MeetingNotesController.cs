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
    public class MeetingNotesController : ControllerBase
    {
        private readonly IMeetingNoteManager _meetingNoteManager;
        public MeetingNotesController(IMeetingNoteManager meetingNoteManager)
        {
            _meetingNoteManager = meetingNoteManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]MeetingNoteCriteria criteria)
        {
            var viewModels = await _meetingNoteManager.GetAsync(criteria);
            this.Response.AddPagination(
                viewModels.CurrentPage, viewModels.PageSize, viewModels.TotalCount, viewModels.TotalPages);

            return Ok(viewModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            MeetingNoteViewModel viewModel = await _meetingNoteManager.GetAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return Ok(viewModel);
        }

        [HttpPost()]
        public async Task<IActionResult> Add(MeetingNoteViewModel viewModel)
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingNoteManager.AddAsync(viewModel, id);
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> Update(MeetingNoteViewModel viewModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingNoteManager.UpdateAsync(viewModel, userId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingNoteManager.DeleteAsync(id, userId);
            return Ok();
        }
    }
}