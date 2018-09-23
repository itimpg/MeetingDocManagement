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
    public class MeetingAgendasController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IMeetingAgendaManager _meetingAgendaManager;
        private readonly IMeetingContentManager _meetingContentManager;
        private ILogger<MeetingAgendasController> _logger;

        public MeetingAgendasController(
            IMeetingAgendaManager meetingTimeManager,
            IMeetingContentManager meetingContentManager,
            IUserManager userManager,
            ILogger<MeetingAgendasController> logger)
        {
            _meetingAgendaManager = meetingTimeManager;
            _meetingContentManager = meetingContentManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]MeetingAgendaCriteria criteria)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            criteria.UserId = userId;
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
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingAgendaManager.AddAsync(viewModel, userId);
            var user = await _userManager.GetAsync(userId);
            _logger.LogInformation($"{user.Email} Add Agenda : {JsonConvert.SerializeObject(viewModel)}");
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> Update(MeetingAgendaViewModel viewModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingAgendaManager.UpdateAsync(viewModel, userId);
            var user = await _userManager.GetAsync(userId);
            _logger.LogInformation($"{user.Email} Edit Agenda : {JsonConvert.SerializeObject(viewModel)}");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingAgendaManager.DeleteAsync(id, userId);
            var user = await _userManager.GetAsync(userId);
            _logger.LogInformation($"{user.Email} Delete Agenda : {id}");
            return Ok();
        }

        [HttpGet("{id}/meetingcontents")]
        public async Task<IActionResult> Get(int id, [FromQuery]MeetingContentCriteria criteria)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            criteria.UserId = userId;
            criteria.Model.MeetingAgendaId = id;

            var topics = await _meetingContentManager.GetAsync(criteria);
            this.Response.AddPagination(
                topics.CurrentPage, topics.PageSize, topics.TotalCount, topics.TotalPages);

            return Ok(topics);
        }

        [HttpGet("{id}/schedulecontents")]
        public async Task<IActionResult> GetScheduleContents(int id, [FromQuery]MeetingContentCriteria criteria)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            criteria.Model.MeetingAgendaId = id;
            criteria.UserId = userId;

            var topics = await _meetingContentManager.GetScheduleContentsAsync(criteria);
            this.Response.AddPagination(
                topics.CurrentPage, topics.PageSize, topics.TotalCount, topics.TotalPages);

            return Ok(topics);
        }

    }
}