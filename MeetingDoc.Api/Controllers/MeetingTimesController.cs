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
    public class MeetingTimesController : ControllerBase
    {
        private readonly IMeetingTimeManager _meetingTimeManager;
        private readonly IMeetingAgendaManager _meetingAgendaManager;

        public MeetingTimesController(IMeetingTimeManager meetingTimeManager, IMeetingAgendaManager meetingAgendaManager)
        {
            _meetingTimeManager = meetingTimeManager;
            _meetingAgendaManager = meetingAgendaManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]MeetingTimeCriteria criteria)
        {
            var viewModels = await _meetingTimeManager.GetAsync(criteria);
            this.Response.AddPagination(
                viewModels.CurrentPage, viewModels.PageSize, viewModels.TotalCount, viewModels.TotalPages);

            return Ok(viewModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            MeetingTimeViewModel viewModel = await _meetingTimeManager.GetAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return Ok(viewModel);
        }

        [HttpPost()]
        public async Task<IActionResult> Add(MeetingTimeViewModel viewModel)
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingTimeManager.AddAsync(viewModel, id);
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> Update(MeetingTimeViewModel viewModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingTimeManager.UpdateAsync(viewModel, userId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingTimeManager.DeleteAsync(id, userId);
            return Ok();
        }
        
        [HttpGet("{id}/meetingagendas")]
        public async Task<IActionResult> Get(int id, [FromQuery]MeetingAgendaCriteria criteria)
        {
            criteria.Model.MeetingTimeId = id;

            var topics = await _meetingAgendaManager.GetAsync(criteria);
            this.Response.AddPagination(
                topics.CurrentPage, topics.PageSize, topics.TotalCount, topics.TotalPages);

            return Ok(topics);
        }
        
    }
}