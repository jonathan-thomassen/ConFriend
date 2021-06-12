using ConFriend.Interfaces;

namespace ConFriend.Models
{
    public class EventTheme : IModel
    {
        public int ThemeId { get; set; }
        public int EventId { get; set; }

        public string ToSQL()
        {
            return $"ThemeId = {ThemeId}, EventId = {EventId}";
        }

        public string Identity()
        {
            return $"ThemeId = {ThemeId} AND EventId = {EventId}";
        }
    }
}
