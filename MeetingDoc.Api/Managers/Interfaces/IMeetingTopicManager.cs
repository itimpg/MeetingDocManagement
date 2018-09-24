using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingDoc.Api.Models;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Managers.Interfaces
{
    public interface IMeetingTopicManager : IBaseManager<MeetingTopic, MeetingTopicViewModel>
    {
        Task<IList<MeetingTopicViewModel>> GetActivesAsync(int typeId);
    }
}