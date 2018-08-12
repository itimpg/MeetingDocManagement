using MeetingDoc.Api.Data;

namespace MeetingDoc.Api.ViewModels
{
    public abstract class BaseSearchCriteria<T>
        where T : class, new()
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}