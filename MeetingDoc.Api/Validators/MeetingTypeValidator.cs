using MeetingDoc.Api.Validators.Interfaces;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Validators
{
    public class MeetingTypeValidator : IMeetingTypeValidator
    {
        public ValidateResult ValidateBeforeAdd(MeetingTypeViewModel viewModel)
        {
            return Validate(viewModel);
        }

        public ValidateResult ValidateBeforeUpdate(MeetingTypeViewModel viewModel)
        {
            return Validate(viewModel);
        }

        private ValidateResult Validate(MeetingTypeViewModel viewModel)
        {
            var result = new ValidateResult();
            if (string.IsNullOrEmpty(viewModel.Name))
            {
                result.ErrorMessages.Add("Name is missing.");
            }
            return result;
        }
    }
}