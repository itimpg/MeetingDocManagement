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
    public class MeetingTopicsController : ControllerBase
    {
        private readonly IMeetingTopicManager _meetingTopicManager;
        private readonly IMeetingTimeManager _meetingTimeManager;

        public MeetingTopicsController(IMeetingTopicManager meetingTopicManager, IMeetingTimeManager meetingTimeManager)
        {
            _meetingTopicManager = meetingTopicManager;
            _meetingTimeManager = meetingTimeManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]MeetingTopicCriteria criteria)
        {
            var viewModels = await _meetingTopicManager.GetAsync(criteria);
            this.Response.AddPagination(
                viewModels.CurrentPage, viewModels.PageSize, viewModels.TotalCount, viewModels.TotalPages);

            return Ok(viewModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            MeetingTopicViewModel viewModel = await _meetingTopicManager.GetAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return Ok(viewModel);
        }

        [HttpPost()]
        public async Task<IActionResult> Add(MeetingTopicViewModel viewModel)
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingTopicManager.AddAsync(viewModel, id);
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> Update(MeetingTopicViewModel viewModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingTopicManager.UpdateAsync(viewModel, userId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingTopicManager.DeleteAsync(id, userId);
            return Ok();
        }
        
        [HttpGet("{id}/meetingtimes")]
        public async Task<IActionResult> Get(int id, [FromQuery]MeetingTimeCriteria criteria)
        {
            criteria.Model.MeetingTopicId = id;

            var topics = await _meetingTimeManager.GetAsync(criteria);
            this.Response.AddPagination(
                topics.CurrentPage, topics.PageSize, topics.TotalCount, topics.TotalPages);

            return Ok(topics);
        }
    }
}