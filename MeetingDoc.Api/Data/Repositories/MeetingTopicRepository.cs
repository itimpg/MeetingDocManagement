using System;
using System.Threading.Tasks;
using MeetingDoc.Api.Data.Repositories.Interfaces;
using MeetingDoc.Api.Models;

namespace MeetingDoc.Api.Data.Repositories
{
    public class MeetingTopicRepository : Repository<MeetingTopic>, IMeetingTopicRepository
    {
        public MeetingTopicRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public override async Task UpdateAsync(MeetingTopic entityToUpdate)
        {
            var existsEntity = await Dbset.FindAsync(entityToUpdate.Id);

            existsEntity.Name = entityToUpdate.Name;
            existsEntity.IsDraft = entityToUpdate.IsDraft;
            existsEntity.UpdatedBy = entityToUpdate.UpdatedBy;
            existsEntity.UpdatedDate = DateTime.Now;
        }
    }
}