using System.Threading.Tasks;
using MeetingDoc.Api.Models;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Managers.Interfaces
{
    public interface IMeetingAgendaManager : IBaseManager<MeetingAgenda, MeetingAgendaViewModel>
    {
    }
}