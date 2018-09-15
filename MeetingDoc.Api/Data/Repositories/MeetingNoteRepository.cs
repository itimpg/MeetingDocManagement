using System;
using System.Threading.Tasks;
using MeetingDoc.Api.Data.Repositories.Interfaces;
using MeetingDoc.Api.Models;

namespace MeetingDoc.Api.Data.Repositories
{
    public class MeetingNoteRepository : Repository<MeetingNote>, IMeetingNoteRepository
    {
        public MeetingNoteRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public override async Task UpdateAsync(MeetingNote entityToUpdate)
        {
            var existsEntity = await Dbset.FindAsync(entityToUpdate.Id);

            existsEntity.Note = entityToUpdate.Note;
            existsEntity.UpdatedBy = entityToUpdate.UpdatedBy;
            existsEntity.UpdatedDate = DateTime.Now;
        }
    }
}