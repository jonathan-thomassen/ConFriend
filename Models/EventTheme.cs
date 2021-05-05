using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConFriend.Models
{
    public class EventTheme: IModel
    {
        public int ThemeId { get; set; }
        public int EventId { get; set; }

        public ModelTypes DataType => ModelTypes.Event;

        public string ToSQL()
        {
            return $"ThemeId = {ThemeId} AND EventId = {EventId}";
        }

 
        public string Identity()
        {
            return $"ThemeId = {ThemeId} AND EventId = {EventId}";
        }
    }
}
