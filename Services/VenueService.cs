using System;
using System.Collections.Generic;
using SymFiend.Interfaces;
using SymFiend.Models;

namespace SymFiend.Services
{
    public class VenueService : SQLService<Venue>, ICrudService<Venue>
    {
        public VenueService()
        {
            Items = new List<Venue>();
        }

        public bool Create(Venue item)
        {
            throw new NotImplementedException();
        }

        public List<Venue> GetAll()
        {
            throw new NotImplementedException();
        }

        public Venue GetFromId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Venue item)
        {
            throw new NotImplementedException();
        }

        public List<Venue> GetFiltered(string filter, ICrudService<Venue>.FilterType filterType)
        {
            throw new NotImplementedException();
        }
    }
}
