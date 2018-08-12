namespace MeetingDoc.Api.Models
{
    public class MeetingAgenda : BaseEntity
    {
        public MeetingTime Time { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
    }
}