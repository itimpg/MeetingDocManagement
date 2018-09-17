using System.Threading.Tasks;
using MeetingDoc.Api.Data.Interfaces;
using MeetingDoc.Api.Data.Repositories.Interfaces;
using MeetingDoc.Api.Models;

namespace MeetingDoc.Api.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _dbContext;

        public UnitOfWork(
            DataContext dbContext,
            IUserRepository userRepository,
            IMeetingTypeRepository meetingTypeRepository,
            IMeetingTopicRepository meetingTopicRepository,
            IMeetingTimeRepository meetingTimeRepository,
            IMeetingAgendaRepository meetingAgendaRepository,
            IMeetingContentRepository meetingContentRepository,
            IMeetingNoteRepository meetingNoteRepository,
            IMeeitngAgendaUserRepository meeitngAgendaUserRepository)
        {
            _dbContext = dbContext;
            UserRepository = userRepository;
            MeetingTypeRepository = meetingTypeRepository;
            MeetingTopicRepository = meetingTopicRepository;
            MeetingTimeRepository = meetingTimeRepository;
            MeetingAgendaRepository = meetingAgendaRepository;
            MeetingContentRepository = meetingContentRepository;
            MeetingNoteRepository = meetingNoteRepository;
            MeeitngAgendaUserRepository = meeitngAgendaUserRepository;
        }

        public IUserRepository UserRepository { get; private set; }
        public IMeetingContentRepository MeetingContentRepository { get; private set; }

        public IMeetingTypeRepository MeetingTypeRepository { get; private set; }

        public IMeetingTopicRepository MeetingTopicRepository { get; private set; }

        public IMeetingTimeRepository MeetingTimeRepository { get; private set; }

        public IMeetingAgendaRepository MeetingAgendaRepository { get; private set; }

        public IMeetingNoteRepository MeetingNoteRepository { get; private set; }

        public IMeeitngAgendaUserRepository MeeitngAgendaUserRepository { get; private set; }

        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}