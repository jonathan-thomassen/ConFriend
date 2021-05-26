using ConFriend.Interfaces;

namespace ConFriend.Models
{
    public class SeatCategory : IModel
    {
        public int SeatCategoryId { get; set; }
        public string NameKey { get; set; }

        public string ToSQL()
        {
            return $"NameKey = '{NameKey}'";
        }

        public string Identity()
        {
            return $"SeatCategoryId = {SeatCategoryId}";
        }
    }
}
