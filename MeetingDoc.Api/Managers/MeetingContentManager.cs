using System;
using System.Linq;
using System.Threading.Tasks;
using MeetingDoc.Api.Data.Interfaces;
using MeetingDoc.Api.Data.Repositories.Interfaces;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.Models;
using MeetingDoc.Api.Validators.Interfaces;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Managers
{
    public class MeetingContentManager : BaseManager<MeetingContent, MeetingContentViewModel>, IMeetingContentManager
    {
        protected override IRepository<MeetingContent> Repository => UnitOfWork.MeetingContentRepository;

        public MeetingContentManager(
            IUnitOfWork unitOfWork,
            IMeetingContentValidator validator)
            : base(unitOfWork, validator)
        {
        }

        protected override IQueryable<MeetingContent> GetByCriteria(BaseCriteria<MeetingContentViewModel> criteria)
        {
            return Repository
                .GetQuery()
                .Where(x => !x.IsRemoved && x.MeetingAgendaId == criteria.Model.MeetingAgendaId)
                .OrderBy(x => x.Ordinal);
        }

        protected override MeetingContent ToEntity(MeetingContentViewModel viewModel)
        {
            return new MeetingContent
            {
                Id = viewModel.Id,
                FileName = viewModel.FileName,
                FileBase64 = viewModel.FileBase64,
                Ordinal = viewModel.Ordinal,
                MeetingAgendaId = viewModel.MeetingAgendaId
            };
        }

        protected override MeetingContentViewModel ToViewModel(MeetingContent entity)
        {
            return new MeetingContentViewModel
            {
                Id = entity.Id,
                FileName = entity.FileName,
                FileBase64 = entity.FileBase64,
                Ordinal = entity.Ordinal,
                MeetingAgendaId = entity.MeetingAgendaId
            };
        }

        public async Task MoveContent(MoveContentViewModel viewModel, int userId)
        {
            var content = await UnitOfWork.MeetingContentRepository.GetAsync(viewModel.ContentId);
            content.MeetingAgendaId = viewModel.AgendaId;
            content.UpdatedBy = userId;
            content.UpdatedDate = DateTime.Now;

            await UnitOfWork.SaveChangeAsync();
        }

        public async Task<PagedList<MeetingContentViewModel>> GetScheduleContentsAsync(MeetingContentCriteria criteria)
        {
            var query = Repository
                .GetQuery(x => x.MeetingAgendaId == criteria.Model.MeetingAgendaId && !x.IsRemoved)
                .OrderBy(x => x.Ordinal);
            return await this.ToPagedListAsync(query, criteria.PageSize, criteria.PageNumber);
        }
    }
}