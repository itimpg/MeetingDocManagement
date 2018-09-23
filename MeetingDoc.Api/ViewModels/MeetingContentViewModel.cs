namespace MeetingDoc.Api.ViewModels
{
    public class MeetingContentViewModel : BaseViewModel
    {
        public string FileName { get; set; }
        public string FileBase64 { get; set; }
        public int Ordinal { get; set; }
        public double Ratio { get; set; }
        public int MeetingAgendaId { get; set; }
    }
}