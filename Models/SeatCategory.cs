using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConFriend.Models
{
    public class SeatCategory : IModel
    {
        public int SeatCategoryId { get; set; }
        public string NameKey { get; set; }

        public ModelTypes DataType => ModelTypes.SeatCategory;

        public string ToSQL()
        {
            return $"NameKey = '{NameKey}'";
        }

        public string Identity()
        {
            return $"SeatCategoryId = {SeatCategoryId}";
        }
   

    }
}
