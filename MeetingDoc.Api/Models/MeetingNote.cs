namespace MeetingDoc.Api.Models
{
    public class MeetingNote : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int MeetingContentId { get; set; }
        public MeetingContent MeetingContent { get; set; }
        public string Note { get; set; }
    }
}