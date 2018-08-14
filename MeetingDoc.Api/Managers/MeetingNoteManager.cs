using System.Linq;
using MeetingDoc.Api.Data.Interfaces;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.Models;
using MeetingDoc.Api.Validators.Interfaces;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Managers
{
    public class MeetingNoteManager : BaseManager<MeetingNote, MeetingNoteViewModel>, IMeetingNoteManager
    {
        public MeetingNoteManager(IUnitOfWork unitOfWork, IMeetingNoteValidator validator)
            : base(unitOfWork, validator)
        { }

        protected override IQueryable<MeetingNote> GetByCriteria(BaseCriteria<MeetingNoteViewModel> criteria)
        {
            return Repository.GetQuery();
        }

        protected override MeetingNote ToEntity(MeetingNoteViewModel viewModel)
        {
            return new MeetingNote
            {
                Id = viewModel.Id,
            };
        }

        protected override MeetingNoteViewModel ToViewModel(MeetingNote entity)
        {
            return new MeetingNoteViewModel
            {
                Id = entity.Id
            };
        }
    }
}