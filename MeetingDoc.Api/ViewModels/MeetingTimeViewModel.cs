using System;

namespace MeetingDoc.Api.ViewModels
{
    public class MeetingTimeViewModel : BaseViewModel
    {
        public int MeetingTopicId { get; set; }
        public int Count { get; set; }
        public string FiscalYear { get; set; }
        public DateTime MeetingDate { get; set; }
        public string Location { get; set; }
        public bool IsDraft { get; set; }
        public string MeetingTopicName { get; set; }
    }
}