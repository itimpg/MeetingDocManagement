using System;
using System.Linq;
using System.Threading.Tasks;
using MeetingDoc.Api.Data.Repositories.Interfaces;
using MeetingDoc.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingDoc.Api.Data.Repositories
{
    public class MeetingNoteRepository : Repository<MeetingNote>, IMeetingNoteRepository
    {
        public MeetingNoteRepository(DataContext dbContext) : base(dbContext)
        {
        }
        public override async Task InsertAsync(MeetingNote entity)
        {
            var note = await this.Dbset
                .FirstOrDefaultAsync(x => x.UserId == entity.UserId && x.MeetingContentId == entity.MeetingContentId);
            if (note != null)
            {
                this.Dbset.Remove(note);
            }

            await Dbset.AddAsync(entity);
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