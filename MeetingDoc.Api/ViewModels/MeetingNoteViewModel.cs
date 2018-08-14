namespace MeetingDoc.Api.ViewModels
{
    public class MeetingNoteViewModel : BaseViewModel
    {
        public int UserId { get; set; }
        public int MeetingContentId { get; set; }
        public string Note { get; set; }
    }
}