using System;
using System.Collections.Generic;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.Extensions.Configuration;

namespace ConFriend.Services
{
    public class ConferenceService : SQLService<Conference>, ICrudService<Conference>
    {
      
        public ConferenceService(IConfiguration configuration) : base(configuration, "Conference")
        {

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

        public override Conference OnRead()
        {
            return new Conference();
        }
    }
}
