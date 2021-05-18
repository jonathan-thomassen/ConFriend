using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using ConFriend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Events
{
    public class EventIndexModel : PageModel
    {
        public bool IsAdmin;
        public UserConferenceBinding UCBinding;
        public User CurrentUser;
        public Conference CurrentConference;

        public List<Event> Events { get; private set; }
        public List<Models.Speaker> Speakers { get; private set; }
        public List<Conference> Conferences { get; private set; }
        public List<Room> Rooms { get; private set; }
        public List<Venue> Venues { get; private set; }
        public List<Enrollment> Enrollments { get; private set; }

        private readonly ICrudService<Event> _eventService;
        private readonly ICrudService<Room> _roomService;
        private readonly ICrudService<Models.Speaker> _speakerService;
        private readonly ICrudService<Conference> _conferenceService;
        private readonly ICrudService<Venue> _venueService;
        private readonly ICrudService<Enrollment> _enrollmentService;
        private readonly ICrudService<UserConferenceBinding> _ucBindingService;
        private readonly SessionService _sessionService;

        public EventIndexModel(ICrudService<Event> eventService, ICrudService<Room> roomService, ICrudService<Enrollment> enrollmentService, ICrudService<UserConferenceBinding> ucBindingService, SessionService sessionService)
        {
            _eventService = eventService;
            _roomService = roomService;
            _enrollmentService = enrollmentService;
            _ucBindingService = ucBindingService;

            _eventService.Init(ModelTypes.Event);
            _roomService.Init(ModelTypes.Room);
            _enrollmentService.Init(ModelTypes.Enrollment);
            _ucBindingService.Init(ModelTypes.UserConferenceBinding);
        }
        public async Task OnGetAsync()
        {
            IsAdmin = true;
            Events = await _eventService.GetAll();
            Rooms = await _roomService.GetAll();
            Enrollments = await _enrollmentService.GetAll();

            //if (_sessionService.GetUserId(HttpContext.Session) != null)
            //    CurrentUser = await _userService.GetFromId((int)_sessionService.GetUserId(HttpContext.Session));

            //UCBinding = _ucBindingService.GetAll().Result
            //    .FindAll(binding => binding.UserId.Equals(CurrentUser.UserId)).Find(binding =>
            //        binding.ConferenceId.Equals(CurrentConference.ConferenceId));

            //if (UCBinding?.UserType != UserType.Admin && UCBinding?.UserType != UserType.SuperUser)
            //    return OnGetDeniedAsync();

            //foreach (Event e in Events)
            //{
            //    e.Users = Enrollments.FindAll(enroll => enroll.EventId.Equals(e.EventId));
            //}
            foreach (Event e in Events)
            {
                e.Enrollments = await _enrollmentService.GetFiltered(e.EventId, ModelTypes.Event);
            }

            //User genericUser1 = new User();
            //User genericUser2 = new User();
            //User genericUser3 = new User();
            //Events[0].Users.AddFirst(genericUser1);
            //Events[1].Users.AddFirst(genericUser2);
            //Events[1].Users.AddFirst(genericUser3);
        }
    }
}
