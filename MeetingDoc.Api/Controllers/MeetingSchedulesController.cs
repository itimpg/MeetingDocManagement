using System.Security.Claims;
using System.Threading.Tasks;
using MeetingDoc.Api.Helpers;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.ViewModels.Criterias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingDoc.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingSchedulesController : ControllerBase
    {
        private readonly IMeetingScheduleManager _meetingScheduleManager;
        public MeetingSchedulesController(IMeetingScheduleManager meetingScheduleManager)
        {
            _meetingScheduleManager = meetingScheduleManager;
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
    }
}