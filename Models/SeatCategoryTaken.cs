using ConFriend.Interfaces;

namespace ConFriend.Models
{
    public class SeatCategoryTaken : IModel 
    {
        public int SeatCategoryId { get; set; }
        public int EventId { get; set; }
        public int SeatsTaken { get; set; }

        public string ToSQL()
        {
            return $"SeatsTaken = {SeatsTaken}";
        }

        public string Identity()
        {
            return $"SeatCategoryId = {SeatCategoryId} AND EventId = {EventId}";
        }
    }
}
