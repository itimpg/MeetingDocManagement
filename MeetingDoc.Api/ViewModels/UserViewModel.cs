using System.ComponentModel.DataAnnotations;

namespace MeetingDoc.Api.ViewModels
{
    public class UserViewModel : BaseViewModel
    { 
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Position { get; set; } 
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public int Level { get; set; }
        public string LevelText { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
    }
}