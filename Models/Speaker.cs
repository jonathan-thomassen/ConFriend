namespace ConFriend.Models
{
    public class Speaker : IModel
    {
        public int SpeakerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

        public string ToSQL()
        {
            return $"FirstName = '{FirstName}', LastName = '{LastName}', [E-Mail] = '{Email}', ImageUrl = '{Image}', Description = '{Description}', Title = '{Title}'";
        }
        public string Identity()
        {
            return $"SpeakerId = {SpeakerId}";
        }
        public static string IdentitySQL = "SpeakerId =";
    }
}
