using MeetingDoc.Api.Validators.Interfaces;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Validators
{
    public class MeetingNoteValidator : IMeetingNoteValidator
    {
        public ValidateResult ValidateBeforeAdd(MeetingNoteViewModel viewModel)
        {
            return new ValidateResult();
        }

        public ValidateResult ValidateBeforeUpdate(MeetingNoteViewModel viewModel)
        {
            return new ValidateResult();
        }
    }
}