using MeetingDoc.Api.Validators.Interfaces;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Validators
{
    public class MeetingContentValidator : IMeetingContentValidator
    {
        public ValidateResult ValidateBeforeAdd(MeetingContentViewModel viewModel)
        {
            return new ValidateResult();
        }

        public ValidateResult ValidateBeforeUpdate(MeetingContentViewModel viewModel)
        {
            return new ValidateResult();
        }
    }
}