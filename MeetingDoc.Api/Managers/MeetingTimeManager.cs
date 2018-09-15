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
        protected override IRepository<MeetingTime> Repository => UnitOfWork.MeetingTimeRepository;

        public MeetingTimeManager(IUnitOfWork unitOfWork, IMeetingTimeValidator validator)
            : base(unitOfWork, validator)
        {
        }

        protected override IQueryable<MeetingTime> GetByCriteria(BaseCriteria<MeetingTimeViewModel> criteria)
        {
            return Repository.GetQuery().Where(x => x.MeetingTopicId == criteria.Model.MeetingTopicId && !x.IsRemoved);
        }

        protected override MeetingTime ToEntity(MeetingTimeViewModel viewModel)
        {
            return new MeetingTime
            {
                Id = viewModel.Id,
                Count = viewModel.Count,
                MeetingTopicId = viewModel.MeetingTopicId,
                FiscalYear = viewModel.FiscalYear,
                MeetingDate = viewModel.MeetingDate,
                Location = viewModel.Location
            };
        }

        protected override MeetingTimeViewModel ToViewModel(MeetingTime entity)
        {
            return new MeetingTimeViewModel
            {
                Id = entity.Id,
                Count = entity.Count,
                MeetingTopicId = entity.MeetingTopicId,
                FiscalYear = entity.FiscalYear,
                MeetingDate = entity.MeetingDate,
                Location = entity.Location
            };
        }
    }
}