using System.Threading.Tasks;
using MeetingDoc.Api.Models;

namespace MeetingDoc.Api.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity: BaseEntity;
        Task SaveChangeAsync();
    }
}