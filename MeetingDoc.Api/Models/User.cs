using System.ComponentModel.DataAnnotations;

namespace MeetingDoc.Api.Models
{
    public class User : BaseEntity
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; } 
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt {get;set;}
        public string PhoneNo { get; set; }
        public UserLevel Level { get; set; }
        public bool IsActive { get; set; } 
    }
}