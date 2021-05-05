using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConFriend.Models
{
    public class RoomFeature: IModel
    {
        public int FeatureId { get; set; }
        public int RoomId { get; set; }
        public bool IsAvailable { get; set; }
        public string ToSQL()
        {
            return $"IsAvailable = {IsAvailable}";
        }

        public string Identity()
        {
            return $"FeatureId = {FeatureId} AND RoomId = {RoomId}";
        }
    }
}
