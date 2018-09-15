using System.Linq;
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
                Name = viewModel.Name
            };
        }

        protected override MeetingAgendaViewModel ToViewModel(MeetingAgenda entity)
        {
            return new MeetingAgendaViewModel
            {
                Id = entity.Id,
                MeetingTimeId = entity.MeetingTimeId,
                Number = entity.Number,
                Name = entity.Name
            };
        }
    }
}