namespace MeetingDoc.Api.Models
{
    public class MeetingNote : BaseEntity
    {
        public User User { get; set; }
        public MeetingContent Content { get; set; }
        public string Note { get; set; }
    }
}