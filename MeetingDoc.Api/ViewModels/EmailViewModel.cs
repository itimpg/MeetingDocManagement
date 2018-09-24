using System.Net.Mail;

namespace MeetingDoc.Api.ViewModels
{
    public class EmailViewModel
    {
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Attachment Attachment { get; set; }
    }
}