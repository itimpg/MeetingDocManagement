using System;

namespace MeetingDoc.Api.Models
{
    public class MeetingTime : BaseEntity
    {
        public int MeetingTopicId { get; set; }
        public MeetingTopic MeetingTopic { get; set; }
        public int Count { get; set; }
        public string FiscalYear { get; set; }
        public DateTime MeetingDate { get; set; }
        public string Location { get; set; }
        public bool IsDraft { get; set; }
    }
}