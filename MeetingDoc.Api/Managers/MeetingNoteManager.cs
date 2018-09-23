using System;
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
        protected override IRepository<MeetingNote> Repository => UnitOfWork.MeetingNoteRepository;

        public MeetingNoteManager(IUnitOfWork unitOfWork, IMeetingNoteValidator validator)
            : base(unitOfWork, validator)
        { }

        protected override IQueryable<MeetingNote> GetByCriteria(BaseCriteria<MeetingNoteViewModel> criteria)
        {
            return Repository.GetQuery().Where(x => !x.IsRemoved);
        }

        protected override MeetingNote ToEntity(MeetingNoteViewModel viewModel)
        {
            var content = viewModel.Note.Split(',');
            return new MeetingNote
            {
                Id = viewModel.Id,
                UserId = viewModel.UserId,
                MeetingContentId = viewModel.MeetingContentId,
                NoteHeader = content[0],
                Note = Convert.FromBase64String(content[1])
            };
        }

        protected override MeetingNoteViewModel ToViewModel(MeetingNote entity)
        {
            return new MeetingNoteViewModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                MeetingContentId = entity.MeetingContentId,
                Note = entity.NoteHeader + "," + Convert.ToBase64String(entity.Note),
            };
        }
    }
}