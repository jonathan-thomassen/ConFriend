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
        private readonly ICrudService<Enrollment> _enrollmentService;
        private readonly ICrudService<UserConferenceBinding> _ucBindingService;
        private readonly ICrudService<User> _userService;
        private readonly ICrudService<Conference> _conferenceService;
        private readonly SessionService _sessionService;

        public EventIndexModel(ICrudService<Event> eventService, ICrudService<Room> roomService, ICrudService<Enrollment> enrollmentService,
            ICrudService<UserConferenceBinding> ucBindingService, SessionService sessionService, ICrudService<User> userService, ICrudService<Conference> conferenceService)
        {
            _eventService = eventService;
            _roomService = roomService;
            _enrollmentService = enrollmentService;
            _ucBindingService = ucBindingService;
            _userService = userService;
            _conferenceService = conferenceService;

            _eventService.Init(ModelTypes.Event);
            _roomService.Init(ModelTypes.Room);
            _enrollmentService.Init(ModelTypes.Enrollment);
            _userService.Init(ModelTypes.User);
            _conferenceService.Init(ModelTypes.Conference);
            _ucBindingService.Init(ModelTypes.UserConferenceBinding);

            _sessionService = sessionService;
        }
        public async Task OnGetAsync()
        {
            Events = await _eventService.GetAll();
            Rooms = await _roomService.GetAll();
            Enrollments = await _enrollmentService.GetAll();

            if (_sessionService.GetConferenceId(HttpContext.Session) != null)
                CurrentConference = await _conferenceService.GetFromId((int)_sessionService.GetConferenceId(HttpContext.Session));


            if (_sessionService.GetUserId(HttpContext.Session) != null)
            {
                CurrentUser = await _userService.GetFromId((int)_sessionService.GetUserId(HttpContext.Session));

                UCBinding = _ucBindingService.GetAll().Result
                    .FindAll(binding => binding.UserId.Equals(CurrentUser.UserId)).Find(binding =>
                        binding.ConferenceId.Equals(CurrentConference.ConferenceId));

                if (UCBinding?.UserType == UserType.Admin || UCBinding?.UserType == UserType.SuperUser)
                    IsAdmin = true;
                else
                {
                    IsAdmin = false;
                }
            }
            else
            {
                IsAdmin = false;
            }

            foreach (Event e in Events)
            {
                e.Enrollments = await _enrollmentService.GetFiltered(e.EventId, ModelTypes.Event);
            }
        }
    }
}
