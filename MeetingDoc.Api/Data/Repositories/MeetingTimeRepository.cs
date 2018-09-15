using System;
using System.Threading.Tasks;
using MeetingDoc.Api.Data.Repositories.Interfaces;
using MeetingDoc.Api.Models;

namespace MeetingDoc.Api.Data.Repositories
{
    public class MeetingTimeRepository : Repository<MeetingTime>, IMeetingTimeRepository
    {
        public MeetingTimeRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public override async Task UpdateAsync(MeetingTime entityToUpdate)
        {
            var existsEntity = await Dbset.FindAsync(entityToUpdate.Id);

            existsEntity.Count = entityToUpdate.Count;
            existsEntity.FiscalYear = entityToUpdate.FiscalYear;
            existsEntity.MeetingDate = entityToUpdate.MeetingDate;
            existsEntity.Location = entityToUpdate.Location;
            existsEntity.UpdatedBy = entityToUpdate.UpdatedBy;
            existsEntity.UpdatedDate = DateTime.Now;
        }
    }
}