using System;
using System.Collections.Generic;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ConFriend.Services
{
    public class UserService : SQLService<User>, ICrudService<User>
    {
        public UserService(IConfiguration configuration, string name) : base(configuration, name)
        {
            Items = new List<User>();
        }

        public UserService(string connectionString, string name) : base(connectionString, name)
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

        public override void OnRead()
        {
            while (Reader.Read())
            {
                
            }
        }
    }
}
