namespace MeetingDoc.Api.Models
{
    public class MeetingContent : BaseEntity
    {
        public string FileName { get; set; }
        // Base64
        public byte[] File { get; set; }
        public int Ordinal { get; set; }

        public MeetingAgenda Agenda { get; set; }
    }
}