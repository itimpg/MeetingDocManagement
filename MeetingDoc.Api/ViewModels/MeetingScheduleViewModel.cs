using System;

namespace MeetingDoc.Api.ViewModels
{
    public class MeetingScheduleViewModel : BaseViewModel
    {
        public int UserId { get; set; }
        public string MeetingType { get; set; }
        public string MeetingTopic { get; set; }
        public int MeetingTimeCount { get; set; }
        public string MeetingFiscalYear { get; set; }
        public DateTime MeetingDateTime { get; set; }
        public string MeetingPlace { get; set; }
    }
}