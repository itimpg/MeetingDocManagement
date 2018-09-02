using MeetingDoc.Api.Data;

namespace MeetingDoc.Api.ViewModels
{
    public abstract class BaseCriteria<TViewModel>
        where TViewModel : BaseViewModel
    {
        private const int MAX_PAGE_SIZE = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 5;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value; }
        }
    }
}