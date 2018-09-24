using System.Threading.Tasks;
using MeetingDoc.Api.Models;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Managers.Interfaces
{
    public interface IMeetingContentManager : IBaseManager<MeetingContent, MeetingContentViewModel>
    {
        Task MoveContent(MoveContentViewModel viewModel, int userId);
        Task<PagedList<MeetingContentViewModel>> GetScheduleContentsAsync(MeetingContentCriteria criteria);
        Task<bool> ShareContentAsync(ShareContentViewModel viewModel);
    }
}