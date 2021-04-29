using System;
using System.Collections.Generic;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.Extensions.Configuration;

namespace ConFriend.Services
{
    public class SpeakerService : SQLService<Speaker>, ICrudService<Speaker>
    {

        public SpeakerService(IConfiguration configuration) : base(configuration, "Speaker")
        {

        }

        public bool Create(Speaker item)
        {
            throw new NotImplementedException();
        }

        public List<Speaker> GetAll()
        {
            throw new NotImplementedException();
        }

        public Speaker GetFromId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Speaker item)
        {
            throw new NotImplementedException();
        }

        public List<Speaker> GetFiltered(string filter, ICrudService<Speaker>.FilterType filterType)
        {
            throw new NotImplementedException();
        }

        public override Speaker OnRead()
        {
            Speaker speaker = new Speaker();
            return speaker;
        }
    }
}
