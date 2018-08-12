using System.Collections.Generic;

namespace MeetingDoc.Api.ViewModels
{
    public class PagedResult<T> where T : class
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int TotalRecord { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}