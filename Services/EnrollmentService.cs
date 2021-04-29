using System;
using System.Collections.Generic;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.Extensions.Configuration;

namespace ConFriend.Services
{
    public class EnrollmentService : SQLService<Enrollment>
    {

        public EnrollmentService(IConfiguration configuration) : base(configuration, "Enrollment")
        {

        }

        public bool Create(Enrollment item)
        {
            throw new NotImplementedException();
        }

        public List<Enrollment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Enrollment GetFromId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Enrollment item)
        {
            throw new NotImplementedException();
        }

        public List<Enrollment> GetFiltered(string filter, ICrudService<Enrollment>.FilterType filterType)
        {
            throw new NotImplementedException();
        }

        public override Enrollment OnRead()
        {
            throw new NotImplementedException();
        }
    }
}
