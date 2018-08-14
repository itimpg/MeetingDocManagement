using MeetingDoc.Api.Validators.Interfaces;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Validators
{
    public class MeetingTopicValidator : IMeetingTopicValidator
    {
         public ValidateResult ValidateBeforeAdd(MeetingTopicViewModel viewModel)
        {
            return new ValidateResult();
        }

        public ValidateResult ValidateBeforeUpdate(MeetingTopicViewModel viewModel)
        {
            return new ValidateResult();
        }
    }
}