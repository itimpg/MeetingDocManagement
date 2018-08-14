using System.Linq;
using MeetingDoc.Api.Data.Interfaces;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.Models;
using MeetingDoc.Api.Validators.Interfaces;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Managers
{
    public class MeetingTimeManager : BaseManager<MeetingTime, MeetingTimeViewModel>, IMeetingTimeManager
    {
        public MeetingTimeManager(IUnitOfWork unitOfWork, IMeetingTimeValidator validator)
            : base(unitOfWork, validator)
        {

        }
        protected override IQueryable<MeetingTime> GetByCriteria(BaseCriteria<MeetingTimeViewModel> criteria)
        {
            return Repository.GetQuery();
        }

        protected override MeetingTime ToEntity(MeetingTimeViewModel viewModel)
        {
            return new MeetingTime
            {
                Id = viewModel.Id,
            };
        }

        protected override MeetingTimeViewModel ToViewModel(MeetingTime entity)
        {
            return new MeetingTimeViewModel
            {
                Id = entity.Id
            };
        }
    }
}