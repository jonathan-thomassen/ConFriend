using System;
using System.Collections.Generic;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.Extensions.Configuration;

namespace ConFriend.Services
{
    public class RoomService : SQLService<Room>, ICrudService<Room>
    {
        public RoomService(IConfiguration configuration) : base(configuration, "Room")
        {

        }
        public bool Create(Room item)
        {
            return SQLCommand(SQLType.Create, "n", item.ToSQL());
        }

        public List<Room> GetAll()
        {
            SQLCommand(SQLType.GetAll);
            return Items;
        }

        public Room GetFromId(int id)
        {
            SQLCommand(SQLType.GetSingle, $"{Room.IdentitySQL} {id}");
            return Item;
        }

        public bool Delete(int id)
        {
            return SQLCommand(SQLType.Delete, $"{Room.IdentitySQL} {id}");
        }

        public bool Update(Room item)
        {
            return SQLCommand(SQLType.Update, item.Identity(), item.ToSQL());
        }

        public List<Room> GetFiltered(string filter, ICrudService<Room>.FilterType filterType)
        {
            return null;
        }
        public override Room OnRead()
        {
            Room room = new Room();

            room.RoomId = Reader.GetInt32(0);
            room.Name = Reader.GetString(1);
            room.Floor = Reader.GetString(2);
            room.Events = null;
            room.Size = Reader.GetInt32(4);
            room.Capacity = Reader.GetInt32(5);
            room.DoorAmount = Reader.GetInt32(6);
            room.Image = Reader.GetString(7);
            room.Coordinates = null;
            room.SeatCategories = null;
            room.Features = null;

            return room;
        }
    }
}
