using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetingDoc.Api.Data.Interfaces;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.Models;
using MeetingDoc.Api.Validators.Interfaces;
using MeetingDoc.Api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MeetingDoc.Api.Managers
{
    public class MeetingTopicManager : BaseManager<MeetingTopic, MeetingTopicViewModel>, IMeetingTopicManager
    {
        protected override IRepository<MeetingTopic> Repository => UnitOfWork.MeetingTopicRepository;

        public MeetingTopicManager(IUnitOfWork unitOfWork, IMeetingTopicValidator validator)
            : base(unitOfWork, validator)
        {
        }

        protected override IQueryable<MeetingTopic> GetByCriteria(BaseCriteria<MeetingTopicViewModel> criteria)
        {
            return Repository.GetQuery().Where(x => (x.CreatedBy == criteria.UserId || criteria.UserId == 1) && !x.IsRemoved && x.MeetingTypeId == criteria.Model.MeetingTypeId);
        }

        protected override MeetingTopic ToEntity(MeetingTopicViewModel viewModel)
        {
            return new MeetingTopic
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                MeetingTypeId = viewModel.MeetingTypeId,
                IsDraft = viewModel.IsDraft,
            };
        }

        protected override MeetingTopicViewModel ToViewModel(MeetingTopic entity)
        {
            return new MeetingTopicViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                MeetingTypeId = entity.MeetingTypeId,
                IsDraft = entity.IsDraft,
            };
        }

        public async Task<IList<MeetingTopicViewModel>> GetActivesAsync(int typeId)
        {
            var topics = Repository
                .GetQuery()
                .Where(x => !x.IsRemoved && !x.IsDraft && x.MeetingTypeId == typeId);

            return await topics.Select(x => ToViewModel(x)).ToListAsync();
        }
    }
}