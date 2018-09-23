using System;
using System.IO;
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
            var content = viewModel.FileBase64.Split(',');

            return new MeetingContent
            {
                Id = viewModel.Id,
                FileName = content[0],
                FileBase64 = Convert.FromBase64String(content[1]),
                Ordinal = viewModel.Ordinal,
                MeetingAgendaId = viewModel.MeetingAgendaId,
                Ratio = viewModel.Ratio,
            };
        }

        protected override MeetingContentViewModel ToViewModel(MeetingContent entity)
        {
            return new MeetingContentViewModel
            {
                Id = entity.Id,
                FileName = entity.FileName,
                FileBase64 = entity.FileName + "," + Convert.ToBase64String(entity.FileBase64),
                Ordinal = entity.Ordinal,
                MeetingAgendaId = entity.MeetingAgendaId,
                Ratio = entity.Ratio
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
            try
            {
                var contents = Repository
                    .GetQuery(x => x.MeetingAgendaId == criteria.Model.MeetingAgendaId && !x.IsRemoved)
                    .OrderBy(x => x.Ordinal);


                var query = from content in contents
                            join note in UnitOfWork.MeetingNoteRepository
                                .GetQuery(x => x.UserId == criteria.UserId && !x.IsRemoved)
                                on content.Id equals note.MeetingContentId into lj
                            from note in lj.DefaultIfEmpty()
                            select new MeetingContent
                            {
                                Id = content.Id,
                                FileName = content.FileName,
                                Ordinal = content.Ordinal,
                                MeetingAgendaId = content.MeetingAgendaId,
                                Ratio = content.Ratio,
                                FileBase64 = note == null ? content.FileBase64 : note.Note
                            };

                var xx = query.ToList();

                return await this.ToPagedListAsync(query, criteria.PageSize, criteria.PageNumber);
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw ex;
            }
        }
    }
}