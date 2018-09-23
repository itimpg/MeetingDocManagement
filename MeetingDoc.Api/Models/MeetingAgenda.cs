using System.Collections.Generic;

namespace MeetingDoc.Api.Models
{
    public class MeetingAgenda : BaseEntity
    {
        public int MeetingTimeId { get; set; }
        public MeetingTime MeetingTime { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public bool IsDraft { get; set; }
        public IList<MeetingAgendaUser> MeetingAgendaUsers { get; set; }
        public IList<MeetingContent> MeetingContents { get; set; }
    }
}