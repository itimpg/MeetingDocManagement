namespace MeetingDoc.Api.Models
{
    public class MeetingTopic : BaseEntity
    {
        public string Name { get; set; }
        public int MeetingTypeId { get; set; }
        public MeetingType MeetingType { get; set; }
        public bool IsDraft { get; set; }
    }
}