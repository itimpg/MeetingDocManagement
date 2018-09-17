namespace MeetingDoc.Api.Models
{
    public class MeetingAgendaUser : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int MeetingAgendaId { get; set; }
        public MeetingAgenda MeetingAgenda { get; set; }
    }
}