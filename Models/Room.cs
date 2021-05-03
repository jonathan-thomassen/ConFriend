using System.Collections.Generic;

namespace ConFriend.Models
{
    public class Room : IModel
    {
        public int RoomId { get; set; }
        public int FloorId { get; set; }
        public int VenueId { get; set; }
        public string Name { get; set; }
        public string Floor { get; set; }
        public List<Event> Events { get; set; }
        public int Size { get; set; }
        public int Capacity { get; set; }
        public int DoorAmount { get; set; }
        public string Image { get; set; }
        public string Coordinates { get; set; }
        public Dictionary<string, int> SeatCategories { get; set; }
        public Dictionary<string, bool> Features { get; set; }
        public string ToSQL()
        {
            return $"FloorId = '{FloorId}', VenueId = '{VenueId}', Name = '{Name}', Size = '{Size}', Capacity = '{Capacity}', DoorAmount = '{DoorAmount}', ImageUrl = '{Image}', Coordinates = '{Coordinates}'";
        }

        public string Identity()
        {
            return $"RoomId = {RoomId}";
        }
        public static string IdentitySQL = "RoomId =";
    }
}
