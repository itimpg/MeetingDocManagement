using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingDoc.Api.ViewModels;
using MeetingDoc.Api.ViewModels.Criterias;

namespace MeetingDoc.Api.Managers.Interfaces
{
    public interface IMeetingScheduleManager
    {
        Task<PagedList<MeetingScheduleViewModel>> GetAsync(MeetingScheduleCriteria criteria);
    }
}