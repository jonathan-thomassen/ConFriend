using ConFriend.Interfaces;

namespace ConFriend.Models
{
    public class Feature : IModel
    {
        public int FeatureId { get; set; }
        public string Name { get; set; }

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
