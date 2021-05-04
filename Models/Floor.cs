namespace ConFriend.Models
{
    public class Floor : IModel
    {
        public int FloorId { get; set; }
        public int VenueId { get; set; }    
        public string Name { get; set; }
        public string Image { get; set; }

        public string ToSQL()
        {
            return $"VenueId = {VenueId}, Name = '{Name}', ImageUrl = '{Image}'";
        }
        public string Identity()
        {
            return $"FloorId = {FloorId}";
        }
        public static string IdentitySQL = "FloorId =";
    }
}
