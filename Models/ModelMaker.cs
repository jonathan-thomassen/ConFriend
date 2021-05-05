using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConFriend
{
    public enum ModelTypes {
        Conference,
        Enrollment,
        Event,
        Floor,
        Room,
        Speaker,
        User,
        Venue,
        SeatCategory,
        Theme,
        Feature
    }
}
namespace ConFriend.Models
    {
        public class ModelMaker
    {
        private SqlDataReader reader;
        public ModelMaker()
        {
           
        }
        public List<T> OnRead<T>(ModelTypes model, SqlDataReader SQLReader)
        {
            reader = SQLReader;
            List<T> models = new List<T>();
            while (reader.Read())
            {
                T obj = (T)MyRead<T>(model);
                if (obj == null) return new List<T>();
                models.Add(obj);
            }
            return models;
        }
        private object MyRead<T>(ModelTypes readmodel)
        {
           
            switch (readmodel)
            {
                case ModelTypes.Conference:
                    return Maker_Conference();
                case ModelTypes.Enrollment:
                    return Maker_Enrollment();
                case ModelTypes.Event:
                    return Maker_Event();
                case ModelTypes.Floor:
                    return Maker_Floor();
                case ModelTypes.Room:
                    return Maker_Room();
                case ModelTypes.Speaker:
                    return Maker_Speaker();
                case ModelTypes.User:
                    return Maker_User();
                case ModelTypes.Venue:
                    return Maker_Venue();
                default:
                    break;
            }
            return null;
        }
        public Conference Maker_Conference()
        {
            Conference conference = new Conference();

            conference.ConferenceId = reader.GetInt32(0);
            conference.Name = reader.GetString(1);
            conference.EventThemes = new List<string>();
            conference.Speakers = null;
            conference.Events = null;

            return conference;
        }
        public IModel Maker_Enrollment()
        {
            Enrollment enrollment = new Enrollment();

            enrollment.EnrollmentId = reader.GetInt32(0);
            enrollment.SignUpTime = reader.GetDateTime(1);
            enrollment.User = null;
            enrollment.Event = null;

            return enrollment;
        }
        public Event Maker_Event()
        {
            Event _event = new Event();

            _event.EventId = reader.GetInt32(0);
            _event.Name = reader.GetString(1);
            //_event.Host = null;
            //_event.Host = Reader.GetInt32(2);
            _event.StartTime = reader.GetDateTime(3);
            //_event.Duration = Reader.GetInt32(0);
            _event.Type = reader.GetString(4);
            _event.Description = reader.GetString(4);
            //_event.Room = null;
            _event.Capacity = reader.GetInt32(0);
            _event.Users = null;
            _event.Image = reader.GetString(4);
            _event.Hidden = reader.GetBoolean(4);
            _event.Cancelled = reader.GetBoolean(4);
            _event.RoomHidden = reader.GetBoolean(4);
            _event.RoomCancelled = reader.GetBoolean(4);
            _event.SeatCategoriesTaken = null;
            _event.Themes = null;

            return _event;
        }
        public Floor Maker_Floor()
        {
            Floor floor = new Floor();

            floor.FloorId = reader.GetInt32(0);
            floor.VenueId = reader.GetInt32(1);
            floor.Name = reader.GetString(2);
            floor.Image = reader.IsDBNull(3) ? "" : reader.GetString(3);

            return floor;
        }
        public Speaker Maker_Speaker()
        {
            Speaker speaker = new Speaker();

            speaker.SpeakerId = reader.GetInt32(0);
            speaker.FirstName = reader.GetString(1);
            speaker.LastName = reader.GetString(2);
            speaker.Email = reader.GetString(3);
            speaker.Image = reader.IsDBNull(4) ? "" : reader.GetString(4);
            speaker.Description = reader.IsDBNull(5) ? "" : reader.GetString(5);
            speaker.Title = reader.GetString(6);

            return speaker;
        }
        public Room Maker_Room()
        {
            Room room = new Room();

            room.RoomId = reader.GetInt32(0);
            room.Name = reader.GetString(1);
            room.Floor = reader.GetString(2);
            room.Events = null;
            room.Size = reader.GetInt32(4);
            room.Capacity = reader.GetInt32(5);
            room.DoorAmount = reader.GetInt32(6);
            room.Image = reader.GetString(7);
            room.Coordinates = null;
            room.SeatCategories = null;
            room.Features = null;

            return room;
        }
        public User Maker_User()
        {
            User user = new User();
            user.UserId = reader.GetInt32(0);
            user.FirstName = reader.GetString(1);
            user.LastName = reader.GetString(2);
            user.Email = reader.GetString(3);
            user.Password = reader.GetString(4);
            user.Preference = reader.IsDBNull(5) ? new List<string>() : reader.GetString(5).Split(';').ToList();
            user.Type = (UserType)reader.GetByte(6);

            return user;
        }
        public Venue Maker_Venue()
        {
            Venue venue = new Venue();
            venue.VenueId = reader.GetInt32(0);
            venue.Name = reader.GetString(1);
            venue.Floors = null;
            venue.Rooms = null;
            venue.SeatCategories = null;
            venue.RoomFeatures = null;

            return venue;
        }
    }
}
