using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConFriend.Models
{
    public class RoomSeatCategory: IModel
    {
        public int RoomId { get; set; }
        public int SeatCategoryId { get; set; }
        public int Amount { get; set; }
        public string ToSQL()
        {
            return $"Amount = {Amount}";
        }

        public string Identity()
        {
            return $"RoomId = {RoomId} AND SeatCategoryId = {SeatCategoryId}";
        }
    }
}
