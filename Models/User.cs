using System.Collections.Generic;

namespace ConFriend.Models
{
    public enum UserType
    {
        Normal,
        Admin,
        SpecialNeeds
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

        public string ToSQL()
        {
            return $"UserId = {UserId}, FirstName = {FirstName}, LastName = {LastName}, E-Mail = {Email}, Password = {Password}, Preference = Golf, UserType = {Type}";
        }
    }
}
