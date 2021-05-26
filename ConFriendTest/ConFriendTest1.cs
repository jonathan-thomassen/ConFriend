using System;
using System.Collections.Generic;
using ConFriend;
using ConFriend.Interfaces;
using ConFriend.Models;
using ConFriend.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConFriendTest
{
    [TestClass]
    public class ConFriendTest1
    {
        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ConFriend;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [TestMethod]
        public void TestAddEvent()
        {
            //Arrange
            ICrudService<Event> eventService = new CRUD_Service<Event>(ConnectionString);
            eventService.Init(ModelTypes.Event);
            List<Event> events = eventService.GetAll().Result;

            //Act
            int numberOfEventsBefore = events.Count;
            Event newEvent = new Event
            {
                Capacity = 20,
                ConferenceId = 1,
                Description = "This is a test.",
                Duration = TimeSpan.FromMinutes(60),
                Name = "TestEvent",
                RoomId = 1,
                SpeakerId = 1,
                StartTime = DateTime.Now,
                Image = "",
                Cancelled = false,
                Hidden = false,
                RoomCancelled = false,
                RoomHidden = false,
                Type = ""
            };
            bool check = eventService.Create(newEvent).Result;
            events = eventService.GetAll().Result;
            int numberOfEventsAfter = events.Count;

            //Assert
            Assert.AreEqual(numberOfEventsBefore + 1, numberOfEventsAfter);
            Assert.IsTrue(check);
        }

        [TestMethod]
        public void TestAddUser()
        {
            //Arrange
            ICrudService<User> userService = new CRUD_Service<User>(ConnectionString);
            userService.Init(ModelTypes.User);
            List<User> users = userService.GetAll().Result;

            //Act
            int numberOfUsersBefore = users.Count;
            User newUser = new User
            {
                Email = "email@email.com",
                FirstName = "Test",
                LastName = "Person",
                Password = "password"
            };
            bool check = userService.Create(newUser).Result;
            users = userService.GetAll().Result;
            int numberOfUsersAfter = users.Count;

            //Assert
            Assert.AreEqual(numberOfUsersBefore + 1, numberOfUsersAfter);
            Assert.IsTrue(check);
        }

        [TestMethod]
        public void TestUpdateUser()
        {
            //Arrange
            ICrudService<User> userService = new CRUD_Service<User>(ConnectionString);
            userService.Init(ModelTypes.User);

            //Act
            User user = userService.GetFromId(1).Result;
            string testName = "Test";
            user.FirstName = testName;
            bool check = userService.Update(user).Result;
            user = userService.GetFromId(1).Result;

            //Assert
            Assert.AreEqual(testName, user.FirstName);
            Assert.IsTrue(check);
        }
    }
}
