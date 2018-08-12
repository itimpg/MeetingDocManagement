namespace MeetingDoc.Api.Models
{
    public class MeetingTopic : BaseEntity
    {
        public string Name { get; set; }
        public MeetingType Type { get; set; }
    }
}