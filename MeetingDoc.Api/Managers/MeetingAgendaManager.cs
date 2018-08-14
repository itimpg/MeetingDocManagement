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
        public MeetingAgendaManager(IUnitOfWork unitOfWork, IMeetingAgendaValidator validator)
            : base(unitOfWork, validator)
        { }
        protected override IQueryable<MeetingAgenda> GetByCriteria(BaseCriteria<MeetingAgendaViewModel> criteria)
        {
            return Repository.GetQuery();
        }

        protected override MeetingAgenda ToEntity(MeetingAgendaViewModel viewModel)
        {
            return new MeetingAgenda
            {
                Id = viewModel.Id,
                // TODO: etc
            };
        }

        protected override MeetingAgendaViewModel ToViewModel(MeetingAgenda entity)
        {
            return new MeetingAgendaViewModel
            {
                Id = entity.Id,
                // TODO: etc
            };
        }
    }
}