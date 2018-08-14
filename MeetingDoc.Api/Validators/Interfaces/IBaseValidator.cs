using MeetingDoc.Api.Models;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Validators.Interfaces
{
    public interface IBaseValidator<TViewModel>
        where TViewModel : BaseViewModel
    {
        ValidateResult ValidateBeforeAdd(TViewModel viewModel);
        ValidateResult ValidateBeforeUpdate(TViewModel viewModel);
    }
}