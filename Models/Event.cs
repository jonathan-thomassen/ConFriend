using System;
using System.Collections.Generic;

namespace ConFriend.Models
{
    public class Event : IModel
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public Speaker Host { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Room Room { get; set; }
        public int Capacity { get; set; }
        public LinkedList<User> Users { get; set; }
        public string Image { get; set; }
        public bool Hidden { get; set; }
        public bool Cancelled { get; set; }
        public bool RoomHidden { get; set; }
        public bool RoomCancelled { get; set; }
        public Dictionary<string, int> SeatCategoriesTaken { get; set; }
        public List<string> Themes { get; set; }
    }
}
