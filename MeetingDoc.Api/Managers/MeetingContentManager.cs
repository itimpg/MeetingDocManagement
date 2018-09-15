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
            return Repository.GetQuery().Where(x => !x.IsRemoved && x.MeetingAgendaId == criteria.Model.MeetingAgendaId);
        }

        protected override MeetingContent ToEntity(MeetingContentViewModel viewModel)
        {
            return new MeetingContent
            {
                Id = viewModel.Id,
                FileName = viewModel.FileName,
                FileBase64= viewModel.FileBase64,
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
    }
}