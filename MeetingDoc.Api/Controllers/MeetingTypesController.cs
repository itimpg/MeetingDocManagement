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
        private readonly IMeetingTypeManager _meetingTypeManager;
        private readonly IMeetingTopicManager _meetingTopicManager;

        public MeetingTypesController(IMeetingTypeManager meetingTypeManager, IMeetingTopicManager meetingTopicManager)
        {
            _meetingTypeManager = meetingTypeManager;
            _meetingTopicManager = meetingTopicManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]MeetingTypeCriteria criteria)
        {
            var meetingTypes = await _meetingTypeManager.GetAsync(criteria);
            this.Response.AddPagination(
                meetingTypes.CurrentPage, meetingTypes.PageSize, meetingTypes.TotalCount, meetingTypes.TotalPages);

            return Ok(meetingTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            MeetingTypeViewModel viewModel = await _meetingTypeManager.GetAsync(id);
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
            await _meetingTypeManager.AddAsync(viewModel, id);
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> Update(MeetingTypeViewModel viewModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingTypeManager.UpdateAsync(viewModel, userId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingTypeManager.DeleteAsync(id, userId);
            return Ok();
        }

        [HttpGet("{id}/meetingtopics")]
        public async Task<IActionResult> Get(int id, [FromQuery]MeetingTopicCriteria criteria)
        {
            criteria.Model.MeetingTypeId = id;

            var topics = await _meetingTopicManager.GetAsync(criteria);
            this.Response.AddPagination(
                topics.CurrentPage, topics.PageSize, topics.TotalCount, topics.TotalPages);

            return Ok(topics);
        }
    }
}