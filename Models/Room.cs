using System.Collections.Generic;

namespace SymFiend.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string Name { get; set; }
        public string Floor { get; set; }
        public List<Event> Events { get; set; }
        public int Size { get; set; }
        public int Capacity { get; set; }
        public int DoorAmount { get; set; }
        public string Image { get; set; }
        public List<string> Coordinates { get; set; }
        public Dictionary<string, int> SeatCategories { get; set; }
        public Dictionary<string, bool> Features { get; set; }
    }
}
