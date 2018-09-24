using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingDoc.Api.Models;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Managers.Interfaces
{
    public interface IMeetingTypeManager : IBaseManager<MeetingType, MeetingTypeViewModel>
    { 
        Task<IList<MeetingTypeViewModel>> GetActivesAsync();
    }
}