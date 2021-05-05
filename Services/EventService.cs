using System;
using System.Collections.Generic;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.Extensions.Configuration;

namespace ConFriend.Services
{
    public class EventService : SQLService<Event>, ICrudService<Event>
    {

        public EventService(IConfiguration configuration) : base(configuration, "Event")
        {

        }


        public bool Create(Event item)
        {
            return SQLCommand(SQLType.Create, "n", $"{item.Identity()} {item.ToSQL()}");
        }

        public List<Event> GetAll()
        {
            SQLCommand(SQLType.GetAll);
            return Items;
        }

        public Event GetFromId(int id)
        {
            SQLCommand(SQLType.GetSingle, $"{Event.IdentitySQL} {id}");
            return Item;
        }

        public bool Delete(int id)
        {
            return SQLCommand(SQLType.Delete, $"{Event.IdentitySQL} {id}");
        }

        public bool Update(Event item)
        {
            return SQLCommand(SQLType.Update, item.Identity(), item.ToSQL());
        }

        public List<Event> GetFiltered(string filter, ICrudService<Event>.FilterType filterType)
        {
            return null;
        }
        public override Event OnRead()
        {
            Event _event = new Event();

            _event.EventId = Reader.GetInt32(0);
            _event.SpeakerId = Reader.GetInt32(1);
            _event.RoomId = Reader.GetInt32(2);
            _event.ConferenceId = Reader.GetInt32(3);
            _event.Name = Reader.GetString(4);
            //_event.Host = null;
            //_event.Host = Reader.GetInt32(2);
            _event.StartTime = Reader.GetDateTime(5);
            _event.DurationInMinutes = Reader.GetInt32(6);
            _event.Type = Reader.GetString(7);
            _event.Description = Reader.GetString(8);
            //_event.Room = null;
            _event.Capacity = Reader.GetInt32(9);
            _event.Users = null;
            _event.Image = Reader.IsDBNull(10) ?  "":Reader.GetString(10);
            _event.Hidden = Reader.GetBoolean(11);
            _event.Cancelled = Reader.GetBoolean(12);
            _event.RoomHidden = Reader.GetBoolean(13);
            _event.RoomCancelled = Reader.GetBoolean(14);
            _event.SeatCategoriesTaken = null;
            _event.Themes = null;

            return _event;
        }
    }
}