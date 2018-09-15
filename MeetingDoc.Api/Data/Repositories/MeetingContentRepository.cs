using MeetingDoc.Api.Data.Repositories.Interfaces;
using MeetingDoc.Api.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingDoc.Api.Data.Repositories
{
    public class MeetingContentRepository : Repository<MeetingContent>, IMeetingContentRepository
    {
        public MeetingContentRepository(DataContext dbContext) : base(dbContext)
        {
        }
        
        public override async Task InsertAsync(MeetingContent entity)
        {
             var itemsToUpdate = Dbset.Where(x =>
                x.MeetingAgendaId == entity.MeetingAgendaId
                && x.Ordinal >= entity.Ordinal);
            foreach (var item in itemsToUpdate)
            {
                item.Ordinal = item.Ordinal + 1;
                item.UpdatedBy = entity.UpdatedBy;
                item.UpdatedDate = DateTime.Now;
            }

            await base.InsertAsync(entity);
        }

        public override async Task UpdateAsync(MeetingContent entityToUpdate)
        {
            var existsEntity = await GetAsync(entityToUpdate.Id);
            if (existsEntity.Ordinal != entityToUpdate.Ordinal)
            {
                if (existsEntity.Ordinal > entityToUpdate.Ordinal)
                {
                    var itemsToUpdate = Dbset.Where(x =>
                        x.MeetingAgendaId == entityToUpdate.MeetingAgendaId
                        && x.Ordinal >= entityToUpdate.Ordinal
                        && x.Ordinal < existsEntity.Ordinal
                        && x.Id != entityToUpdate.Id);
                    foreach (var item in itemsToUpdate)
                    {
                        item.Ordinal = item.Ordinal + 1;
                        item.UpdatedBy = entityToUpdate.UpdatedBy;
                        item.UpdatedDate = DateTime.Now;
                    }
                }
                else
                {
                    var itemsToUpdate = Dbset.Where(x =>
                        x.MeetingAgendaId == entityToUpdate.MeetingAgendaId
                        && x.Ordinal > existsEntity.Ordinal
                        && x.Ordinal <= entityToUpdate.Ordinal
                        && x.Id != entityToUpdate.Id);
                    foreach (var item in itemsToUpdate)
                    {
                        item.Ordinal = item.Ordinal - 1;
                        item.UpdatedBy = entityToUpdate.UpdatedBy;
                        item.UpdatedDate = DateTime.Now;
                    }
                }
            }

            existsEntity.FileName = entityToUpdate.FileName;
            existsEntity.FileBase64 = entityToUpdate.FileBase64;
            existsEntity.Ordinal = entityToUpdate.Ordinal;
            existsEntity.UpdatedBy = entityToUpdate.UpdatedBy;
            existsEntity.UpdatedDate = DateTime.Now;
        }
    }
}