using System;
using System.Threading.Tasks;
using MeetingDoc.Api.Data.Repositories.Interfaces;
using MeetingDoc.Api.Models;

namespace MeetingDoc.Api.Data.Repositories
{
    public class MeetingTypeRepository : Repository<MeetingType>, IMeetingTypeRepository
    {
        public MeetingTypeRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public override async Task UpdateAsync(MeetingType entityToUpdate)
        {
            var existsEntity = await Dbset.FindAsync(entityToUpdate.Id);

            existsEntity.Name = entityToUpdate.Name;
            existsEntity.UpdatedBy = entityToUpdate.UpdatedBy;
            existsEntity.UpdatedDate = DateTime.Now;
        }
    }
}