using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc;

namespace ConFriend.Models
{
    public class User : IModel
    {
        [Editable(false)]
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; }

        [Required, DataType(DataType.Password), MinLength(6, ErrorMessage = "Password must be at least six characters long.")]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Repeat password"), Compare(nameof(Password), ErrorMessage = "Passwords are not identical.")]
        public string PasswordRepeat { get; set; }

        public List<string> Preference { get; set; }

        public string FullName {
            get { 
                return $"{FirstName} {LastName}";
            }
        
        }

        public string ToSQL()
        {
            string str = "none";
            if (Preference != null) {
                if (Preference.Count != 0)
                {
                    str = "";
                    foreach (string item in Preference)
                    {
                        str += item + "-";
                    }
                    str = str.Substring(0, str.Length - 1);
                }
            }

            return $"FirstName = '{FirstName}', LastName = '{LastName}', [E-Mail] = '{Email}', Password = '{Password}', Preference = '{str}'";
        }

        public string Identity()
        {
            return $"UserId = {UserId}";
        }

    }
}
