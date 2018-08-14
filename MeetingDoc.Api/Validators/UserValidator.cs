using MeetingDoc.Api.Validators.Interfaces;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Validators
{
    public class UserValidator : IUserValidator
    {
        public ValidateResult ValidateBeforeAdd(UserViewModel viewModel)
        {
            return new ValidateResult(); // TODO: add validation here
        }

        public ValidateResult ValidateBeforeUpdate(UserViewModel viewModel)
        {
            return new ValidateResult(); // TODO: add validation here
        }
    }
}