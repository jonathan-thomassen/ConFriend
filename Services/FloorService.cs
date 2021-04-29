using System;
using System.Collections.Generic;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.Extensions.Configuration;

namespace ConFriend.Services
{
    public class FloorService : SQLService<Floor>, ICrudService<Floor>
    {

        public FloorService(IConfiguration configuration) : base(configuration, "User")
        {

        }
  

        public bool Create(Floor item)
        {
            throw new NotImplementedException();
        }

        public List<Floor> GetAll()
        {
            throw new NotImplementedException();
        }

        public Floor GetFromId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Floor item)
        {
            throw new NotImplementedException();
        }

        public List<Floor> GetFiltered(string filter, ICrudService<Floor>.FilterType filterType)
        {
            throw new NotImplementedException();
        }
        public override Floor OnRead()
        {
            Floor rloor = new Floor();
            return rloor;
        }
    }
}
