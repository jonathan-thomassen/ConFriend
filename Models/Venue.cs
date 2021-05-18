using System.Collections.Generic;

namespace ConFriend.Models
{
    public class Venue : IModel
    {
        private ModelTypes DataType = ModelTypes.Venue;

        public int VenueId { get; set; }
        public string Name { get; set; }
        public List<Floor> Floors { get; set; }
        public List<Room> Rooms { get; set; }
        public List<string> SeatCategories { get; set; }
        public List<string> RoomFeatures { get; set; }

        public string ToSQL()
        {
            return $"Name = '{Name}'";
        }

        public string Identity()
        {
            return $"VenueId = {VenueId}";
        }
       
    }
}
