namespace ConFriend.Models
{
    public class Floor
    {
        public int FloorId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public string ToSQL()
        {
            return $"Name = '{Name}', Image = '{Image}'";
        }
        public string Identity()
        {
            return $"FloorId = {FloorId}";
        }
    }
}
