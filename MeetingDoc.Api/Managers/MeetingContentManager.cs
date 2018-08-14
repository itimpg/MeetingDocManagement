using System.Linq;
using MeetingDoc.Api.Data.Interfaces;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.Models;
using MeetingDoc.Api.Validators.Interfaces;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Managers
{
    public class MeetingContentManager : BaseManager<MeetingContent, MeetingContentViewModel>, IMeetingContentManager
    {
        public MeetingContentManager(IUnitOfWork unitOfWork, IMeetingContentValidator validator)
            : base(unitOfWork, validator)
        {

        }

        protected override IQueryable<MeetingContent> GetByCriteria(BaseCriteria<MeetingContentViewModel> criteria)
        {
            return Repository.GetQuery();
        }

        protected override MeetingContent ToEntity(MeetingContentViewModel viewModel)
        {
            return new MeetingContent
            {
                Id = viewModel.Id,
            };
        }

        protected override MeetingContentViewModel ToViewModel(MeetingContent entity)
        {
            return new MeetingContentViewModel
            {
                Id = entity.Id,
            };
        }
    }
}