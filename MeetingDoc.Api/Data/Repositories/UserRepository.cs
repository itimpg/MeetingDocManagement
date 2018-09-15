using System;
using System.Threading.Tasks;
using MeetingDoc.Api.Data.Repositories.Interfaces;
using MeetingDoc.Api.Models;

namespace MeetingDoc.Api.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public override async Task UpdateAsync(User entityToUpdate)
        {
            var existsUser = await Dbset.FindAsync(entityToUpdate.Id);

            existsUser.FirstName = entityToUpdate.FirstName;
            existsUser.LastName = entityToUpdate.LastName;
            existsUser.Position = entityToUpdate.Position;
            existsUser.Email = entityToUpdate.Email;
            existsUser.PhoneNo = entityToUpdate.PhoneNo;
            existsUser.Level = entityToUpdate.Level;
            existsUser.IsActive = entityToUpdate.IsActive;
            existsUser.UpdatedBy = entityToUpdate.UpdatedBy;
            existsUser.UpdatedDate = DateTime.Now;
        }
    }
}