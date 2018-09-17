namespace MeetingDoc.Api.ViewModels
{
    public class MeetingAgendaUserViewModel : BaseViewModel
    {
        public int UserId { get; set; }
        public string UserFullName { get; internal set; }
        public bool IsSelected { get; set; }
    }
}