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
        public string ToSQL()
        {
            throw new NotImplementedException();
        }

        public string Identity()
        {
            return $"ThemeId = {ThemeId} AND EventId = {EventId}";
        }
    }
}
