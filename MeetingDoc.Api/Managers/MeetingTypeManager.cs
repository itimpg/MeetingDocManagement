using System.Linq;
using MeetingDoc.Api.Data.Interfaces;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.Models;
using MeetingDoc.Api.Validators.Interfaces;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Managers
{
    public class MeetingTypeManager : BaseManager<MeetingType, MeetingTypeViewModel>, IMeetingTypeManager
    {
        public MeetingTypeManager(IUnitOfWork unitOfWork, IMeetingTypeValidator validator)
            : base(unitOfWork, validator)
        {
        }

        protected override IQueryable<MeetingType> GetByCriteria(BaseCriteria<MeetingTypeViewModel> criteria)
        {
            return Repository.GetQuery();
        }

        protected override MeetingType ToEntity(MeetingTypeViewModel viewModel)
        {
            return new MeetingType
            {
                Id = viewModel.Id,
                Name = viewModel.Name
            };
        }

        protected override MeetingTypeViewModel ToViewModel(MeetingType entity)
        {
            return new MeetingTypeViewModel
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}