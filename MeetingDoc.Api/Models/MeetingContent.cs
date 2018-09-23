using System.Collections.Generic;

namespace MeetingDoc.Api.Models
{
    public class MeetingContent : BaseEntity
    {
        public string FileName { get; set; }
        public byte[] FileBase64 { get; set; }
        public int Ordinal { get; set; }
        public int MeetingAgendaId { get; set; }
        public double Ratio { get; set; }
        public MeetingAgenda MeetingAgenda { get; set; }
    }
}