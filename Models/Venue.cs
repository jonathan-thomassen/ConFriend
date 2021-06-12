using System.Collections.Generic;
using ConFriend.Interfaces;

namespace ConFriend.Models
{
    public class Venue : IModel
    {
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
