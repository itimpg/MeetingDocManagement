using System;
using System.Threading.Tasks;
using MeetingDoc.Api.Data.Repositories.Interfaces;
using MeetingDoc.Api.Models;

namespace MeetingDoc.Api.Data.Repositories
{
    public class MeeitngAgendaUserRepository : Repository<MeetingAgendaUser>, IMeeitngAgendaUserRepository
    {
        public MeeitngAgendaUserRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public override async Task UpdateAsync(MeetingAgendaUser entityToUpdate)
        {
            var existsEntity = await Dbset.FindAsync(entityToUpdate.Id);

// TODO: map here

            existsEntity.UpdatedBy = entityToUpdate.UpdatedBy;
            existsEntity.UpdatedDate = DateTime.Now;
        }
    }
}