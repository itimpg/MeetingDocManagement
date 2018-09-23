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
    public class MeetingNotesController : ControllerBase
    {
        private readonly IMeetingNoteManager _meetingNoteManager;
        private readonly IUserManager _userManager;
        private readonly ILogger<MeetingNotesController> _logger;

        public MeetingNotesController(
            IMeetingNoteManager meetingNoteManager,
            IUserManager userManager,
            ILogger<MeetingNotesController> logger)
        {
            _meetingNoteManager = meetingNoteManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]MeetingNoteCriteria criteria)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            criteria.UserId = userId;
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
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            viewModel.UserId = userId;
            await _meetingNoteManager.AddAsync(viewModel, userId);
            var user = await _userManager.GetAsync(userId);
            _logger.LogInformation($"{user.Email} Add Note : {JsonConvert.SerializeObject(viewModel)}");
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> Update(MeetingNoteViewModel viewModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            viewModel.UserId = userId;
            await _meetingNoteManager.UpdateAsync(viewModel, userId);
            var user = await _userManager.GetAsync(userId);
            _logger.LogInformation($"{user.Email} Edit Note : {JsonConvert.SerializeObject(viewModel)}");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _meetingNoteManager.DeleteAsync(id, userId);
            var user = await _userManager.GetAsync(userId);
            _logger.LogInformation($"{user.Email} Delete Note : {id}");
            return Ok();
        }
    }
}