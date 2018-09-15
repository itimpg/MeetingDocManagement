namespace MeetingDoc.Api.Models
{
    public class MeetingContent : BaseEntity
    {
        public string FileName { get; set; } 
        public string FileBase64 { get; set; }
        public int Ordinal { get; set; }
        public int MeetingAgendaId { get; set; }

        public MeetingAgenda MeetingAgenda { get; set; }
    }
}