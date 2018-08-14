using MeetingDoc.Api.Validators.Interfaces;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Validators
{
    public class MeetingAgendaValidator : IMeetingAgendaValidator
    {
        public ValidateResult ValidateBeforeAdd(MeetingAgendaViewModel viewModel)
        {
            return new ValidateResult();
        }

        public ValidateResult ValidateBeforeUpdate(MeetingAgendaViewModel viewModel)
        {
            return new ValidateResult();
        }
    }
}