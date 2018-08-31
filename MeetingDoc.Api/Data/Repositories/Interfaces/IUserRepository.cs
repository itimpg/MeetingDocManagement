using System.Threading.Tasks;
using MeetingDoc.Api.Data.Interfaces;
using MeetingDoc.Api.Models;

namespace MeetingDoc.Api.Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task UpdateAsync(User user);
    }
}