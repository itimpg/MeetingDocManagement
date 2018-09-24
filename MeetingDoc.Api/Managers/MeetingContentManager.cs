using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using MeetingDoc.Api.Data.Interfaces;
using MeetingDoc.Api.Data.Repositories.Interfaces;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.Models;
using MeetingDoc.Api.Validators.Interfaces;
using MeetingDoc.Api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MeetingDoc.Api.Managers
{
    public class MeetingContentManager : BaseManager<MeetingContent, MeetingContentViewModel>, IMeetingContentManager
    {
        protected override IRepository<MeetingContent> Repository => UnitOfWork.MeetingContentRepository;
        private readonly IEmailManager _emailManager;

        public MeetingContentManager(
            IUnitOfWork unitOfWork,
            IMeetingContentValidator validator,
            IEmailManager emailManager)
            : base(unitOfWork, validator)
        {
            _emailManager = emailManager;
        }

        protected override IQueryable<MeetingContent> GetByCriteria(BaseCriteria<MeetingContentViewModel> criteria)
        {
            return Repository
                .GetQuery()
                .Where(x => (x.CreatedBy == criteria.UserId || criteria.UserId == 1) && !x.IsRemoved && x.MeetingAgendaId == criteria.Model.MeetingAgendaId)
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

        public async Task<bool> ShareContentAsync(ShareContentViewModel viewModel)
        {
            var query = from c in UnitOfWork.MeetingContentRepository
                            .GetQuery(x => x.Id == viewModel.ContentId)
                            .Include(x => x.MeetingAgenda)
                            .ThenInclude(x => x.MeetingTime)
                            .ThenInclude(x => x.MeetingTopic)
                            .ThenInclude(x => x.MeetingType)
                        join note in UnitOfWork.MeetingNoteRepository
                            .GetQuery(x => x.UserId == viewModel.UserId && !x.IsRemoved)
                            on c.Id equals note.MeetingContentId into lj
                        from note in lj.DefaultIfEmpty()
                        select new
                        {
                            Id = c.Id,
                            Ordinal = c.Ordinal,
                            FileBase64 = note == null ? c.FileBase64 : note.Note,
                            AgendaName = c.MeetingAgenda.Name,
                            Time = c.MeetingAgenda.MeetingTime,
                            TopicName = c.MeetingAgenda.MeetingTime.MeetingTopic.Name,
                            TypeName = c.MeetingAgenda.MeetingTime.MeetingTopic.MeetingType.Name
                        };

            var content = await query.FirstOrDefaultAsync();
            if (content == null)
            {
                return false;
            }
            var attachment = new Attachment(new MemoryStream(content.FileBase64), $"{content.AgendaName}_{content.Ordinal}.jpg");
            _emailManager.SendEmail(new EmailViewModel
            {
                EmailTo = viewModel.Email,
                Subject = $"ข้อมูลการประชุม{content.TopicName}",
                Body = $@"
                ประเภทการประชุม: {content.TypeName}
                ชื่อหัวข้อวาระการประชุม: {content.TopicName}
                ครั้งที่การประชุม: {content.Time.Count}/{content.Time.FiscalYear}
                ชื่อระเบียบวาระการประชุม​: {content.AgendaName}",
                Attachment = attachment
            });
            return true;
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