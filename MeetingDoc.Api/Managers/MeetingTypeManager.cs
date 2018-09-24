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
    public class MeetingTypeManager : BaseManager<MeetingType, MeetingTypeViewModel>, IMeetingTypeManager
    {
        protected override IRepository<MeetingType> Repository => UnitOfWork.MeetingTypeRepository;

        public MeetingTypeManager(IUnitOfWork unitOfWork, IMeetingTypeValidator validator)
            : base(unitOfWork, validator)
        {
        }

        protected override IQueryable<MeetingType> GetByCriteria(BaseCriteria<MeetingTypeViewModel> criteria)
        {
            return Repository.GetQuery().Where(x => (x.CreatedBy == criteria.UserId || criteria.UserId == 1) && !x.IsRemoved);
        }

        protected override MeetingType ToEntity(MeetingTypeViewModel viewModel)
        {
            return new MeetingType
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                IsDraft = viewModel.IsDraft,
            };
        }

        protected override MeetingTypeViewModel ToViewModel(MeetingType entity)
        {
            return new MeetingTypeViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                IsDraft = entity.IsDraft,
            };
        }


        public async Task<IList<MeetingTypeViewModel>> GetActivesAsync()
        {
            var topics = Repository
                .GetQuery(x => !x.IsRemoved && !x.IsDraft);

            return await topics.Select(x => ToViewModel(x)).ToListAsync();
        }
    }
}