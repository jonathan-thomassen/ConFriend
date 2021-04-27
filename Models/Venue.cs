using System.Collections.Generic;

namespace ConFriend.Models
{
    public class Venue
    {
        public int VenueId { get; set; }
        public string Name { get; set; }
        public List<Floor> Floors { get; set; }
        public List<Room> Rooms { get; set; }
        public List<string> SeatCategories { get; set; }
        public List<string> RoomFeatures { get; set; }
    }
}
