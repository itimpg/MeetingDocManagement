using System;
using System.Threading.Tasks;
using MeetingDoc.Api.Data.Repositories.Interfaces;
using MeetingDoc.Api.Models;

namespace MeetingDoc.Api.Data.Repositories
{
    public class MeetingAgendaRepository : Repository<MeetingAgenda>, IMeetingAgendaRepository
    {
        public MeetingAgendaRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public override async Task UpdateAsync(MeetingAgenda entityToUpdate)
        {
            var existsEntity = await Dbset.FindAsync(entityToUpdate.Id);

            existsEntity.Name = entityToUpdate.Name;
            existsEntity.Number = entityToUpdate.Number;
            existsEntity.UpdatedBy = entityToUpdate.UpdatedBy;
            existsEntity.UpdatedDate = DateTime.Now;
        }
    }
}