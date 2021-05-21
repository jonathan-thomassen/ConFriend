using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConFriend.Models
{
    public class Theme: IModel
    {
        public int ThemeId { get; set; }
        public string Name { get; set; }


        public ModelTypes DataType = ModelTypes.Theme;

        public string ToSQL()
        {
            return $"Name = '{Name}'";
        }
        public string Identity()
        {
            return $"ThemeId = {ThemeId}";
        }
       
    }
}
