using System.Collections.Generic;

namespace ConFriend.Models
{
    public enum UserType
    {
        Normal,
        Admin,
        SpecialNeeds
    }

    public class User : IModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }

        public List<string> Preference { get; set; }

        ModelTypes IModel.DataType
        {
            get {
            return ModelTypes.User;
            }

        }

  
        public string ToSQL()
        {
            string str = "";
            if (Preference != null) {
                foreach (string item in Preference)
                {
                    str += item + ",";
                }
                str = str.Substring(0, str.Length - 1);
            }
            else{
                str = "none";
            }
            //UserId = {UserId},
            return $"FirstName = '{FirstName}', LastName = '{LastName}', [E-Mail] = '{Email}', Password = '{Password}', Preference = '{str}', UserType = {(int)Type}";
        }

        public int Identity()
        {
            return UserId;
        }

    }
}
