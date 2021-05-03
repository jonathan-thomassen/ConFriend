using System;
using System.Collections.Generic;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.Extensions.Configuration;

namespace ConFriend.Services
{
    public class EventService : SQLService<Event>
    {

        public EventService(IConfiguration configuration) : base(configuration, "Event")
        {

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
        public override Event OnRead()
        {
            Event _event = new Event();
            return _event;
        }
    }
}
