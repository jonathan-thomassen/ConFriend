//using System;
//using System.Collections.Generic;
//using ConFriend.Interfaces;
//using ConFriend.Models;
//using Microsoft.Extensions.Configuration;

//namespace ConFriend.Services
//{
//    public class VenueService : SQLService<Venue>, ICrudService<Venue>
//    {

//        public VenueService(IConfiguration configuration) : base(configuration, "Venue")
//        {

//        }

//        public bool Create(Venue item)
//        {
//            return SQLCommand(SQLType.Create, "n", item.ToSQL());
//        }

//        public List<Venue> GetAll()
//        {
//            SQLCommand(SQLType.GetAll);
//            return Items;
//        }

//        public Venue GetFromId(int id)
//        {
//            SQLCommand(SQLType.GetSingle, $"{Venue.IdentitySQL} {id}");
//            return Item;
//        }

//        public bool Delete(int id)
//        {
//            return SQLCommand(SQLType.Delete, $"{Venue.IdentitySQL} {id}");
//        }

//        public bool Update(Venue item)
//        {
//            return SQLCommand(SQLType.Update, item.Identity(), item.ToSQL());
//        }

//        public List<Venue> GetFiltered(string filter, ICrudService<Venue>.FilterType filterType)
//        {
//            return null;
//        }

//        public override Venue OnRead()
//        {
//            Venue venue = new Venue();
//            venue.VenueId = Reader.GetInt32(0);
//            venue.Name = Reader.GetString(1);
//            venue.Floors = null;
//            venue.Rooms = null;
//            venue.SeatCategories = null;
//            venue.RoomFeatures = null;

//            return venue;
//        }
//    }
//}
