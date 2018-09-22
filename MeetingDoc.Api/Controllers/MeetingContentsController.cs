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
    public class MeetingContentsController : ControllerBase
    {
        private readonly IMeetingContentManager _meetingContentManager;
        private readonly IUserManager _userManager;
        private readonly ILogger<MeetingContentsController> _logger;
        public MeetingContentsController(
            IMeetingContentManager meetingTimeManager,
            IUserManager userManager,
            ILogger<MeetingContentsController> logger)
        {
            _meetingContentManager = meetingTimeManager;
            _userManager = userManager;
            _logger = logger;
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

        [HttpPost("movecontent")]
        public async Task<IActionResult> MoveContent(MoveContentViewModel viewModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingContentManager.MoveContent(viewModel, userId);
            var user = await _userManager.GetAsync(userId);
            _logger.LogInformation($"{user.Email} Move Content : {viewModel.ContentId} to Agenda {viewModel.AgendaId}");
            return Ok();
        }

        [HttpPost()]
        public async Task<IActionResult> Add(MeetingContentViewModel viewModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingContentManager.AddAsync(viewModel, userId);
            var user = await _userManager.GetAsync(userId);
            _logger.LogInformation($"{user.Email} Add Content : {JsonConvert.SerializeObject(viewModel)}");
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> Update(MeetingContentViewModel viewModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingContentManager.UpdateAsync(viewModel, userId);
            var user = await _userManager.GetAsync(userId);
            _logger.LogInformation($"{user.Email} Edit Content : {JsonConvert.SerializeObject(viewModel)}");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingContentManager.DeleteAsync(id, userId);
            var user = await _userManager.GetAsync(userId);
            _logger.LogInformation($"{user.Email} Delete Content : {id}");
            return Ok();
        }
    }
}