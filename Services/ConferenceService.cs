using System;
using System.Collections.Generic;
using SymFiend.Interfaces;
using SymFiend.Models;

namespace SymFiend.Services
{
    public class ConferenceService : SQLService<Conference>, ICrudService<Conference>
    {
        public ConferenceService()
        {
            Items = new List<Conference>();
        }

        public bool Create(Conference item)
        {
            throw new NotImplementedException();
        }

        public List<Conference> GetAll()
        {
            throw new NotImplementedException();
        }

        public Conference GetFromId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Conference item)
        {
            throw new NotImplementedException();
        }

        public List<Conference> GetFiltered(string filter, ICrudService<Conference>.FilterType filterType)
        {
            throw new NotImplementedException();
        }
    }
}
