using System;
using System.Threading.Tasks;
using MeetingDoc.Api.Data.Repositories.Interfaces;
using MeetingDoc.Api.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MeetingDoc.Api.Data.Repositories
{
    public class MeetingAgendaRepository : Repository<MeetingAgenda>, IMeetingAgendaRepository
    {
        public MeetingAgendaRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public override async Task<MeetingAgenda> GetAsync(int id)
        {
            return await Dbset.Include(x => x.MeetingAgendaUsers).FirstOrDefaultAsync(x => x.Id == id);
        }


        public override async Task InsertAsync(MeetingAgenda entity)
        {
            foreach (var user in entity.MeetingAgendaUsers)
            {
                user.CreatedBy = entity.CreatedBy;
                user.CreatedDate = DateTime.Now;
                user.UpdatedBy = entity.UpdatedBy;
                user.UpdatedDate = DateTime.Now;
            }

            await Dbset.AddAsync(entity);
        }

        public override async Task UpdateAsync(MeetingAgenda entityToUpdate)
        {
            var existsEntity = await Dbset.Include(x => x.MeetingAgendaUsers).FirstOrDefaultAsync(x => x.Id == entityToUpdate.Id);
            existsEntity.Name = entityToUpdate.Name;
            existsEntity.Number = entityToUpdate.Number;
            existsEntity.IsDraft = entityToUpdate.IsDraft;
            existsEntity.UpdatedBy = entityToUpdate.UpdatedBy;
            existsEntity.UpdatedDate = DateTime.Now;

            var existsUsers = existsEntity.MeetingAgendaUsers;
            DbContext.MeetingAgendaUsers.RemoveRange(existsUsers);

            foreach (var user in entityToUpdate.MeetingAgendaUsers)
            {
                user.MeetingAgendaId = entityToUpdate.Id;
                user.CreatedBy = entityToUpdate.UpdatedBy;
                user.CreatedDate = DateTime.Now;
                user.UpdatedBy = entityToUpdate.UpdatedBy;
                user.UpdatedDate = DateTime.Now;
                await DbContext.MeetingAgendaUsers.AddAsync(user);
            }
        }
    }
}