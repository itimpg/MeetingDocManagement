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
    public class MeetingAgendasController : ControllerBase
    {
        private readonly IMeetingAgendaManager _meetingAgendaManager;
        private readonly IMeetingContentManager _meetingContentManager;

        public MeetingAgendasController(IMeetingAgendaManager meetingTimeManager, IMeetingContentManager meetingContentManager)
        {
            _meetingAgendaManager = meetingTimeManager;
            _meetingContentManager = meetingContentManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]MeetingAgendaCriteria criteria)
        {
            var viewModels = await _meetingAgendaManager.GetAsync(criteria);
            this.Response.AddPagination(
                viewModels.CurrentPage, viewModels.PageSize, viewModels.TotalCount, viewModels.TotalPages);

            return Ok(viewModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            MeetingAgendaViewModel viewModel = await _meetingAgendaManager.GetAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return Ok(viewModel);
        }

        [HttpPost()]
        public async Task<IActionResult> Add(MeetingAgendaViewModel viewModel)
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingAgendaManager.AddAsync(viewModel, id);
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> Update(MeetingAgendaViewModel viewModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingAgendaManager.UpdateAsync(viewModel, userId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingAgendaManager.DeleteAsync(id, userId);
            return Ok();
        }
        
        [HttpGet("{id}/meetingcontents")]
        public async Task<IActionResult> Get(int id, [FromQuery]MeetingContentCriteria criteria)
        {
            criteria.Model.MeetingAgendaId = id;

            var topics = await _meetingContentManager.GetAsync(criteria);
            this.Response.AddPagination(
                topics.CurrentPage, topics.PageSize, topics.TotalCount, topics.TotalPages);

            return Ok(topics);
        }
    }
}