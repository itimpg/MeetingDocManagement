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
            IUserRepository userRepository)
        {
            _dbContext = dbContext;
            UserRepository = userRepository;
        }

        public IUserRepository UserRepository { get; private set; }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            return new Repository<TEntity>(_dbContext);
        }

        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}