using System.Collections.Generic;

namespace ConFriend.Models
{
    public enum UserType
    {
        
    }

    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
        public List<string> Preference { get; set; }
    }
}
