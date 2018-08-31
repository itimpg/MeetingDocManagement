using System.ComponentModel.DataAnnotations;

namespace MeetingDoc.Api.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Position { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public int Level { get; set; }
        public string LevelText { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
    }
}