using MeetingDoc.Api.Data;

namespace MeetingDoc.Api.ViewModels
{
    public abstract class BaseCriteria<TViewModel>
        where TViewModel : BaseViewModel
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}