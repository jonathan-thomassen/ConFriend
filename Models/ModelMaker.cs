using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using ConFriend.Interfaces;

namespace ConFriend
{
    public enum ModelTypes
    {
        Conference,
        Enrollment,
        Event,
        Floor,
        Room,
        Speaker,
        User,
        Venue,
        SeatCategory,
        RoomSeatCategory,
        SeatCategoryTaken,
        Theme,
        Feature,
        RoomFeature,
        EventTheme,
        None,
        UserConferenceBinding
    }
}

namespace ConFriend.Models
{
    public class ModelMaker
    {
        private SqlDataReader _reader;
        
        public List<T> OnRead<T>(ModelTypes model, SqlDataReader sqlReader)
        {
            _reader = sqlReader;
            List<T> models = new List<T>();
            while (_reader.Read())
            {
                T obj = (T)MyRead(model);
                if (obj == null) return new List<T>();
                models.Add(obj);
            }
            return models;
        }
        private object MyRead(ModelTypes readModel)
        {
            switch (readModel)
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
                case ModelTypes.SeatCategory:
                    return Maker_SeatCategory();
                case ModelTypes.Theme:
                    return Maker_Theme();
                case ModelTypes.Feature:
                    return Maker_Feature();
                case ModelTypes.RoomFeature:
                    return Maker_RoomFeature();
                case ModelTypes.EventTheme:
                    return Maker_EventTheme();
                case ModelTypes.SeatCategoryTaken:
                    return Maker_SeatCategoryTaken();
                case ModelTypes.UserConferenceBinding:
                    return Maker_UserConferenceBinding();
            }
            return null;
        }

        private UserConferenceBinding Maker_UserConferenceBinding()
        {
            UserConferenceBinding userConferenceBinding = new UserConferenceBinding
            {
                UserConferenceBindingId = _reader.GetInt32(0),
                UserId = _reader.GetInt32(1),
                ConferenceId = _reader.GetInt32(2),
                UserType = (UserType) _reader.GetInt32(3)
            };

            return userConferenceBinding;
        }

        private SeatCategory Maker_SeatCategory()
        {
            SeatCategory seatType = new SeatCategory
            {
                SeatCategoryId = _reader.GetInt32(0), NameKey = _reader.GetString(1)
            };

            return seatType;
        }

        private SeatCategoryTaken Maker_SeatCategoryTaken()
        {
            SeatCategoryTaken seatType = new SeatCategoryTaken
            {
                EventId = _reader.GetInt32(0),
                SeatCategoryId = _reader.GetInt32(1),
                SeatsTaken = _reader.GetInt32(2)
            };

            return seatType;
        }

        public Conference Maker_Conference()
        {
            Conference conference = new Conference
            {
                ConferenceId = _reader.GetInt32(0),
                VenueId = _reader.GetInt32(1),
                Name = _reader.GetString(2),
                EventThemes = new List<string>(),
                Speakers = null,
                Events = null
            };

            return conference;
        }

        public IModel Maker_Enrollment()
        {
            Enrollment enrollment = new Enrollment
            {
                EnrollmentId = _reader.GetInt32(0),
                EventId = _reader.GetInt32(1),
                UserId = _reader.GetInt32(2),
                SignUpTime = _reader.GetDateTime(3)
            };

            return enrollment;
        }

        public Event Maker_Event()
        {
            Event thisEvent = new Event
            {
                EventId = _reader.GetInt32(0),
                SpeakerId = _reader.GetInt32(1),
                RoomId = _reader.GetInt32(2),
                ConferenceId = _reader.GetInt32(3),
                Name = _reader.GetString(4),
                StartTime = _reader.GetDateTime(5),
                Duration = TimeSpan.FromMinutes(_reader.GetInt32(6)),
                Type = _reader.GetString(7),
                Description = _reader.GetString(8),
                Capacity = _reader.GetInt32(9),
                Users = new LinkedList<User>(),
                Enrollments = new List<Enrollment>(),
                Image = _reader.IsDBNull(10) ? "" : _reader.GetString(10),
                Hidden = _reader.GetBoolean(11),
                Cancelled = _reader.GetBoolean(12),
                RoomHidden = _reader.GetBoolean(13),
                RoomCancelled = _reader.GetBoolean(14),
                SeatCategoriesTaken = new Dictionary<string, int>(),
                Themes = new List<string>()
            };

            return thisEvent;
        }

        public Floor Maker_Floor()
        {
            Floor floor = new Floor
            {
                FloorId = _reader.GetInt32(0),
                VenueId = _reader.GetInt32(1),
                Name = _reader.GetString(2),
                Image = _reader.IsDBNull(3) ? "" : _reader.GetString(3)
            };

            return floor;
        }

        public Speaker Maker_Speaker()
        {
            Speaker speaker = new Speaker
            {
                SpeakerId = _reader.GetInt32(0),
                FirstName = _reader.GetString(1),
                LastName = _reader.GetString(2),
                Email = _reader.GetString(3),
                Image = _reader.IsDBNull(4) ? "" : _reader.GetString(4),
                Description = _reader.IsDBNull(5) ? "" : _reader.GetString(5),
                Title = _reader.GetString(6)
            };

            return speaker;
        }

        public Room Maker_Room()
        {
            Room room = new Room
            {
                RoomId = _reader.GetInt32(0),
                FloorId = _reader.GetInt32(1),
                VenueId = _reader.GetInt32(2),
                Name = _reader.GetString(3),
                Size = _reader.GetInt32(4),
                Capacity = _reader.GetInt32(5),
                DoorAmount = _reader.GetInt32(6),
                Image = _reader.IsDBNull(7) ? "" : _reader.GetString(7),
                Coordinates = _reader.IsDBNull(8) ? "" : _reader.GetString(8),
                Features = new Dictionary<int, bool>(),
                Events = new List<Event>()
            };

            return room;
        }

        public User Maker_User()
        {
            User user = new User
            {
                UserId = _reader.GetInt32(0),
                FirstName = _reader.GetString(1),
                LastName = _reader.GetString(2),
                Email = _reader.GetString(3),
                Password = _reader.GetString(4),
                Preference = _reader.IsDBNull(5) ? new List<string>() : _reader.GetString(5).Split(';').ToList()
            };

            return user;
        }

        public Venue Maker_Venue()
        {
            Venue venue = new Venue
            {
                VenueId = _reader.GetInt32(0),
                Name = _reader.GetString(1),
                Floors = null,
                Rooms = null,
                SeatCategories = null,
                RoomFeatures = null
            };

            return venue;
        }

        public Theme Maker_Theme()
        {
            Theme theme = new Theme {ThemeId = _reader.GetInt32(0), Name = _reader.GetString(1)};

            return theme;
        }

        public Feature Maker_Feature()
        {
            Feature feature = new Feature {FeatureId = _reader.GetInt32(0), Name = _reader.GetString(1)};

            return feature;
        }

        public RoomFeature Maker_RoomFeature()
        {
            RoomFeature roomFeature = new RoomFeature
            {
                FeatureId = _reader.GetInt32(0), RoomId = _reader.GetInt32(1), IsAvailable = _reader.GetBoolean(2)
            };

            return roomFeature;
        }

        public EventTheme Maker_EventTheme()
        {
            EventTheme eventTheme = new EventTheme {ThemeId = _reader.GetInt32(0), EventId = _reader.GetInt32(1)};

            return eventTheme;
        }
    }
}
