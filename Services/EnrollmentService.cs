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
            return SQLCommand(SQLType.Create, "n", $"{item.Identity()} {item.ToSQL()}");
        }

        public List<Enrollment> GetAll()
        {
            SQLCommand(SQLType.GetAll);
            return Items;
        }

        public Enrollment GetFromId(int id)
        {
            SQLCommand(SQLType.GetSingle, $"{Enrollment.IdentitySQL} {id}");
            return Item;
        }

        public bool Delete(int id)
        {
            return SQLCommand(SQLType.Delete, $"{Enrollment.IdentitySQL} {id}");
        }

        public bool Update(Enrollment item)
        {
            return SQLCommand(SQLType.Update, item.Identity(), item.ToSQL());
        }

        public List<Enrollment> GetFiltered(string filter, ICrudService<Enrollment>.FilterType filterType)
        {
            return null;
        }

        public override Enrollment OnRead()
        {
            Enrollment enrollment = new Enrollment();

            enrollment.EnrollmentId = Reader.GetInt32(0);
            enrollment.SignUpTime = Reader.GetDateTime(1);
            enrollment.User = null;
            enrollment.Event = null;
       
            return enrollment;
        }
    }
}
