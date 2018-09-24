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
    public class MeetingTypesController : ControllerBase
    {
        private readonly IMeetingTypeManager _meetingTypeManager;
        private readonly IMeetingTopicManager _meetingTopicManager;
        private readonly IUserManager _userManager;
        private readonly ILogger<MeetingTypesController> _logger;

        public MeetingTypesController(
            IMeetingTypeManager meetingTypeManager,
            IMeetingTopicManager meetingTopicManager,
            IUserManager userManager,
            ILogger<MeetingTypesController> logger)
        {
            _meetingTypeManager = meetingTypeManager;
            _meetingTopicManager = meetingTopicManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]MeetingTypeCriteria criteria)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            criteria.UserId = userId;
            var meetingTypes = await _meetingTypeManager.GetAsync(criteria);
            this.Response.AddPagination(
                meetingTypes.CurrentPage, meetingTypes.PageSize, meetingTypes.TotalCount, meetingTypes.TotalPages);

            return Ok(meetingTypes);
        }

        [HttpGet("actives")]
        public async Task<IActionResult> GetActives()
        {
            var result = await _meetingTypeManager.GetActivesAsync();
            return Ok(result);
        }

        [HttpGet("{id}/activeTopics")]
        public async Task<IActionResult> GetActiveTopics(int id)
        {
            var result = await _meetingTopicManager.GetActivesAsync(id);
            return Ok(result);
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
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingTypeManager.AddAsync(viewModel, userId);
            var user = await _userManager.GetAsync(userId);
            _logger.LogInformation($"{user.Email} Add Type : {JsonConvert.SerializeObject(viewModel)}");
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> Update(MeetingTypeViewModel viewModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingTypeManager.UpdateAsync(viewModel, userId);
            var user = await _userManager.GetAsync(userId);
            _logger.LogInformation($"{user.Email} Edit Type : {JsonConvert.SerializeObject(viewModel)}");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingTypeManager.DeleteAsync(id, userId);
            var user = await _userManager.GetAsync(userId);
            _logger.LogInformation($"{user.Email} Delete Type : {id}");
            return Ok();
        }

        [HttpGet("{id}/meetingtopics")]
        public async Task<IActionResult> Get(int id, [FromQuery]MeetingTopicCriteria criteria)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            criteria.UserId = userId;
            criteria.Model.MeetingTypeId = id;

            var topics = await _meetingTopicManager.GetAsync(criteria);
            this.Response.AddPagination(
                topics.CurrentPage, topics.PageSize, topics.TotalCount, topics.TotalPages);

            return Ok(topics);
        }
    }
}