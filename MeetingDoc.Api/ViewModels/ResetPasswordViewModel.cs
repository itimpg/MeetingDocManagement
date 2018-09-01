using System.ComponentModel.DataAnnotations;

namespace MeetingDoc.Api.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}