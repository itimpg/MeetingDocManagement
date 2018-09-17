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
                Users = entity.MeetingAgendaUsers?.Select(x => new MeetingAgendaUserViewModel
                {
                    UserId = x.UserId,
                    IsSelected = true,
                })?.ToList() ?? new List<MeetingAgendaUserViewModel>()
            };
        }
        public override async Task<MeetingAgendaViewModel> GetAsync(int id)
        {
            MeetingAgenda entity = id == 0 ? new MeetingAgenda() : await Repository.GetAsync(id);
            var viewModel = ToViewModel(entity);
            var selectedUserIds = viewModel.Users.Select(x => x.UserId);
            var unSelectedUsers = UnitOfWork.UserRepository
                .GetQuery(x => x.IsActive && !x.IsRemoved && !selectedUserIds.Contains(x.Id))
                .Select(x => new MeetingAgendaUserViewModel
                {
                    UserId = x.Id,
                    UserFullName = $"{x.FirstName} {x.LastName}",
                    IsSelected = false,
                });

            viewModel.Users = viewModel.Users.Union(unSelectedUsers).ToList();
            return viewModel;
        }
    }
}