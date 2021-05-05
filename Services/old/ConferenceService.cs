//using System;
//using System.Collections.Generic;
//using ConFriend.Interfaces;
//using ConFriend.Models;
//using Microsoft.Extensions.Configuration;

//namespace ConFriend.Services
//{
//    public class ConferenceService : SQLService<Conference>, ICrudService<Conference>
//    {
      
//        public ConferenceService(IConfiguration configuration) : base(configuration, "Conference")
//        {

//        }

//        public bool Create(Conference item)
//        {
//            return SQLCommand(SQLType.Create, "n", $"{item.Identity()} {item.ToSQL()}");
//        }

//        public List<Conference> GetAll()
//        {
//            SQLCommand(SQLType.GetAll);
//            return Items;
//        }

//        public Conference GetFromId(int id)
//        {
//            SQLCommand(SQLType.GetSingle, $"{Conference.IdentitySQL} {id}");
//            return Item;
//        }

//        public bool Delete(int id)
//        {
//            return SQLCommand(SQLType.Delete, $"{Conference.IdentitySQL} {id}");
//        }

//        public bool Update(Conference item)
//        {
//            return SQLCommand(SQLType.Update, item.Identity(), item.ToSQL());
//        }

//        public List<Conference> GetFiltered(string filter, ICrudService<Conference>.FilterType filterType)
//        {
//            return null;
//        }

//        public override Conference OnRead()
//        {

//            Conference conference = new Conference();

//            conference.ConferenceId = Reader.GetInt32(0);
//            conference.Name = Reader.GetString(1);
//            conference.EventThemes = new List<string>();
//            conference.Speakers = null;
//            conference.Events = null;

//            return conference;
//        }
//    }
//}
