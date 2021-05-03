using System;
using System.Collections.Generic;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.Extensions.Configuration;

namespace ConFriend.Services
{
    public class FloorService : SQLService<Floor>, ICrudService<Floor>
    {

        public FloorService(IConfiguration configuration) : base(configuration, "Floor")
        {

        }
  

        public bool Create(Floor item)
        {
            return SQLCommand(SQLType.Create, "n", $"{item.Identity()} {item.ToSQL()}");
        }

        public List<Floor> GetAll()
        {
            SQLCommand(SQLType.GetAll);
            return Items;
        }

        public Floor GetFromId(int id)
        {
            SQLCommand(SQLType.GetSingle, $"FloorId = {id}");
            return Item;
        }

        public bool Delete(int id)
        {
            return SQLCommand(SQLType.Delete, $"FloorId = {id}");
        }

        public bool Update(Floor item)
        {
            return SQLCommand(SQLType.Update, item.Identity(), item.ToSQL());
        }

        public List<Floor> GetFiltered(string filter, ICrudService<Floor>.FilterType filterType)
        {
            throw new NotImplementedException();
        }
        public override Floor OnRead()
        {
            Floor floor = new Floor();

            floor.FloorId= Reader.GetInt32(0);
            floor.Name = Reader.GetString(1);
            floor.Image = Reader.GetString(2);

            return floor;
        }
    }
}
