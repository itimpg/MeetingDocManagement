using System.Security.Claims;
using System.Threading.Tasks;
using MeetingDoc.Api.Helpers;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.ViewModels;
using MeetingDoc.Api.ViewModels.Criterias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MeetingDoc.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingSchedulesController : ControllerBase
    {
        private readonly IMeetingScheduleManager _meetingScheduleManager;
        private readonly IMeetingContentManager _meetingContentManager;
        private readonly IUserManager _userManager;
        private readonly ILogger<MeetingSchedulesController> _logger;
        public MeetingSchedulesController(
            IMeetingScheduleManager meetingScheduleManager,
            IMeetingContentManager meetingContentManager,
            IUserManager userManager,
            ILogger<MeetingSchedulesController> logger)
        {
            _meetingScheduleManager = meetingScheduleManager;
            _meetingContentManager = meetingContentManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]MeetingScheduleCriteria criteria)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            criteria.Model.UserId = userId;

            var viewModels = await _meetingScheduleManager.GetAsync(criteria);
            this.Response.AddPagination(
                viewModels.CurrentPage, viewModels.PageSize, viewModels.TotalCount, viewModels.TotalPages);

            return Ok(viewModels);
        }

        [HttpGet("{meetingTimeId}/agendas")]
        public async Task<IActionResult> GetAgendas(int meetingTimeId, [FromQuery]MeetingAgendaCriteria criteria)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            criteria.Model.MeetingTimeId = meetingTimeId;
            criteria.UserId = userId;
            var viewModels = await _meetingScheduleManager.GetAgendasAsync(criteria);
            this.Response.AddPagination(
               viewModels.CurrentPage, viewModels.PageSize, viewModels.TotalCount, viewModels.TotalPages);

            return Ok(viewModels);
        }

        [HttpPost("ShareContent")]
        public async Task<IActionResult> ShareContent(ShareContentViewModel viewModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            viewModel.UserId = userId;
            var isSuccess = await _meetingContentManager.ShareContentAsync(viewModel);

            if (!isSuccess)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}