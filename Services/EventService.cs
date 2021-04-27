using System;
using System.Collections.Generic;
using SymFiend.Interfaces;
using SymFiend.Models;

namespace SymFiend.Services
{
    public class EventService : SQLService<Event>, ICrudService<Event>
    {
        public EventService()
        {
            Items = new List<Event>();
        }

        public bool Create(Event item)
        {
            throw new NotImplementedException();
        }

        public List<Event> GetAll()
        {
            throw new NotImplementedException();
        }

        public Event GetFromId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Event item)
        {
            throw new NotImplementedException();
        }

        public List<Event> GetFiltered(string filter, ICrudService<Event>.FilterType filterType)
        {
            throw new NotImplementedException();
        }
    }
}
