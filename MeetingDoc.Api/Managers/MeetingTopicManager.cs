using System.Linq;
using MeetingDoc.Api.Data.Interfaces;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.Models;
using MeetingDoc.Api.Validators.Interfaces;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Managers
{
    public class MeetingTopicManager : BaseManager<MeetingTopic, MeetingTopicViewModel>, IMeetingTopicManager
    {
        public MeetingTopicManager(IUnitOfWork unitOfWork, IMeetingTopicValidator validator)
            : base(unitOfWork, validator)
        {

        }

        protected override IQueryable<MeetingTopic> GetByCriteria(BaseCriteria<MeetingTopicViewModel> criteria)
        {
            return Repository.GetQuery().Where(x=> !x.IsRemoved && x.Type.Id == criteria.Model.MeetingTypeId);
        }

        protected override MeetingTopic ToEntity(MeetingTopicViewModel viewModel)
        {
            return new MeetingTopic
            {
                Id = viewModel.Id
            };
        }

        protected override MeetingTopicViewModel ToViewModel(MeetingTopic entity)
        {
            return new MeetingTopicViewModel
            {
                Id = entity.Id
            };
        }
    }
}