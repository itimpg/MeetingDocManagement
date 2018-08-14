namespace MeetingDoc.Api.ViewModels
{
    public class MeetingContentViewModel : BaseViewModel
    { 
        public string FileName { get; set; }
        // Base64
        public byte[] File { get; set; }
        public int Ordinal { get; set; }

        public int MeetingAgendaId { get; set; }
    }
}