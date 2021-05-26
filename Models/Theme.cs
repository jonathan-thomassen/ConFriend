using ConFriend.Interfaces;

namespace ConFriend.Models
{
    public class Theme : IModel
    {
        public int ThemeId { get; set; }
        public string Name { get; set; }

        public string ToSQL()
        {
            return $"Name = '{Name}'";
        }

        public string Identity()
        {
            return $"ThemeId = {ThemeId}";
        }
    }
}
