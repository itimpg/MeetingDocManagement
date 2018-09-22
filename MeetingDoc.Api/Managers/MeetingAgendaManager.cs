using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetingDoc.Api.Data.Interfaces;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.Models;
using MeetingDoc.Api.Validators.Interfaces;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Managers
{
    public class MeetingAgendaManager : BaseManager<MeetingAgenda, MeetingAgendaViewModel>, IMeetingAgendaManager
    {
        protected override IRepository<MeetingAgenda> Repository => UnitOfWork.MeetingAgendaRepository;

        public MeetingAgendaManager(IUnitOfWork unitOfWork, IMeetingAgendaValidator validator)
            : base(unitOfWork, validator)
        { }

        protected override IQueryable<MeetingAgenda> GetByCriteria(BaseCriteria<MeetingAgendaViewModel> criteria)
        {
            return Repository.GetQuery().Where(x => !x.IsRemoved && x.MeetingTimeId == criteria.Model.MeetingTimeId);
        }

        protected override MeetingAgenda ToEntity(MeetingAgendaViewModel viewModel)
        {
            return new MeetingAgenda
            {
                Id = viewModel.Id,
                MeetingTimeId = viewModel.MeetingTimeId,
                Number = viewModel.Number,
                Name = viewModel.Name,
                IsDraft = viewModel.IsDraft,
                MeetingAgendaUsers = viewModel.Users.Where(x => x.IsSelected).Select(x => new MeetingAgendaUser
                {
                    UserId = x.UserId,
                }).ToList()
            };
        }

        protected override MeetingAgendaViewModel ToViewModel(MeetingAgenda entity)
        {
            return new MeetingAgendaViewModel
            {
                Id = entity.Id,
                MeetingTimeId = entity.MeetingTimeId,
                Number = entity.Number,
                Name = entity.Name,
                IsDraft = entity.IsDraft,
            };
        }
        public override async Task<MeetingAgendaViewModel> GetAsync(int id)
        {
            MeetingAgenda entity = id == 0 ? new MeetingAgenda() : await Repository.GetAsync(id);
            var viewModel = ToViewModel(entity);
            var selectedUserIds = viewModel.Users.Select(x => x.UserId);
            var users = UnitOfWork.UserRepository
                .GetQuery(x => x.IsActive && !x.IsRemoved && !selectedUserIds.Contains(x.Id))
                .Select(x => x);

            var query = from u in users
                        join au in entity.MeetingAgendaUsers ?? new List<MeetingAgendaUser>()
                            on u.Id equals au.UserId into lj
                        from au in lj.DefaultIfEmpty()
                        select new MeetingAgendaUserViewModel
                        {
                            UserId = u.Id,
                            UserFullName = $"{u.FirstName} {u.LastName}",
                            IsSelected = au != null
                        };

            viewModel.Users = query.OrderBy(x => x.UserId).ToList();
            return viewModel;
        }
    }
}