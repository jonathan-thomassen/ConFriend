using System;
using System.Collections.Generic;
using SymFiend.Interfaces;
using SymFiend.Models;

namespace SymFiend.Services
{
    public class UserService : SQLService<User>, ICrudService<User>
    {
        public UserService()
        {
            Items = new List<User>();
        }

        public bool Create(User item)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetFromId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(User item)
        {
            throw new NotImplementedException();
        }

        public List<User> GetFiltered(string filter, ICrudService<User>.FilterType filterType)
        {
            throw new NotImplementedException();
        }
    }
}
