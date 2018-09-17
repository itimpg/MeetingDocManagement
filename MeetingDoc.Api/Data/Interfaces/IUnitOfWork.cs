using System.Threading.Tasks;
using MeetingDoc.Api.Data.Repositories.Interfaces;
using MeetingDoc.Api.Models;

namespace MeetingDoc.Api.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IMeetingTypeRepository MeetingTypeRepository { get; }
        IMeetingTopicRepository MeetingTopicRepository { get; }
        IMeetingTimeRepository MeetingTimeRepository { get; }
        IMeetingAgendaRepository MeetingAgendaRepository { get; }
        IMeetingContentRepository MeetingContentRepository { get; }
        IMeetingNoteRepository MeetingNoteRepository { get; }
        IMeeitngAgendaUserRepository MeeitngAgendaUserRepository { get; }
        Task SaveChangeAsync();
    }
}