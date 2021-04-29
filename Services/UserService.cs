using System;
using System.Collections.Generic;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace ConFriend.Services
{
    public class UserService : SQLService<User>, ICrudService<User>
    {
        public UserService(IConfiguration configuration) : base(configuration, "[User]")
        {

        }

        public UserService(string connectionString) : base(connectionString, "[User]")
        {
        
        }

        public bool Create(User item)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            SQLCommand(SQLType.GetAllWhere,"FirstName LIKE Mads");
            return Items;
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
            return SQLCommand(SQLType.Update, $"UserId = {item.UserId}", item.ToSQL());
        }

        public List<User> GetFiltered(string filter, ICrudService<User>.FilterType filterType)
        {
            throw new NotImplementedException();
        }

        public override User OnRead()
        {
            User user = new User();
            user.UserId = Reader.GetInt32(0);
            user.FirstName = Reader.GetString(1);
            user.LastName = Reader.GetString(2);
            user.Email = Reader.GetString(3);
            user.Password = Reader.GetString(4); 
            user.Preference = Reader.GetString(5).Split(';').ToList();
            user.Type = (UserType)Reader.GetByte(6);
          
            return user;
        }
    }
}
