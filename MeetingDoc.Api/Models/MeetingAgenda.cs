namespace MeetingDoc.Api.Models
{
    public class MeetingAgenda : BaseEntity
    {
        public int MeetingTimeId { get; set; }
        public MeetingTime MeetingTime { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
    }
}