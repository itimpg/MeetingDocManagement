using MeetingDoc.Api.Validators.Interfaces;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Validators
{
    public class MeetingTimeValidator : IMeetingTimeValidator
    {
        public ValidateResult ValidateBeforeAdd(MeetingTimeViewModel viewModel)
        {
            return new ValidateResult();
        }

        public ValidateResult ValidateBeforeUpdate(MeetingTimeViewModel viewModel)
        {
            return new ValidateResult();
        }

    }
}