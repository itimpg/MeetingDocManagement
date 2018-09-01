using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Managers.Interfaces
{
    public interface IEmailManager
    {
        void SendEmail(EmailViewModel email);
    }
}