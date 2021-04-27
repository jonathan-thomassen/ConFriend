using System;
using System.Collections.Generic;
using SymFiend.Interfaces;
using SymFiend.Models;

namespace SymFiend.Services
{
    public class RoomService : SQLService<Room>, ICrudService<Room>
    {
        public RoomService()
        {
            Items = new List<Room>();
        }
        public bool Create(Room item)
        {
            throw new NotImplementedException();
        }

        public List<Room> GetAll()
        {
            throw new NotImplementedException();
        }

        public Room GetFromId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Room item)
        {
            throw new NotImplementedException();
        }

        public List<Room> GetFiltered(string filter, ICrudService<Room>.FilterType filterType)
        {
            throw new NotImplementedException();
        }
    }
}
