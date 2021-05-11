using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConFriend.Models
{
    public class Feature: IModel
    {
        public int FeatureId { get; set; }
        public string Name { get; set; }

        public ModelTypes DataType = ModelTypes.Feature;

        public string ToSQL()
        {
            return $"Name = '{Name}'";
        }

        public string Identity()
        {
            return $"FeatureId = {FeatureId}";
        }
    }
}
