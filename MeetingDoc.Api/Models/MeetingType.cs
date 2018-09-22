namespace MeetingDoc.Api.Models
{
    public class MeetingType : BaseEntity
    {
        public string Name { get; set; }
        public bool IsDraft { get; set; }
    }
}