
using System.Collections.Generic;

namespace ConFriend.Models
{
    public class Conference : IModel
    {
        public int ConferenceId { get; set; }
        public int VenueId { get; set; }
        public string Name { get; set; }
        public List<string> EventThemes { get; set; }
        public List<Speaker> Speakers { get; set; }
        public List<Event> Events { get; set; }

        public string ToSQL()
        {
            return $"VenueId = '{VenueId}', Name = '{Name}'";
        }
        public string Identity()
        {
            return $"ConferenceId = '{ConferenceId}'";
        }
        public static string IdentitySQL = "ConferenceId =";
    }
}
