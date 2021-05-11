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
        public ModelTypes DataType = ModelTypes.RoomFeature;
        public string ToSQL()
        {
            return $"FeatureId = {FeatureId}, RoomId = {RoomId}, IsAvailable = '{IsAvailable}'";
        }

        public string Identity()
        {
            return $"FeatureId = {FeatureId} AND RoomId = {RoomId}";
        }
    }
}
