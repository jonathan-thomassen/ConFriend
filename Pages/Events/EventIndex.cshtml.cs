using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Events
{
    public class EventIndexModel : PageModel
    {
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

        public EventIndexModel(ICrudService<Event> eventService, ICrudService<Room> roomService, ICrudService<Models.Speaker> speakerService,
            ICrudService<Conference> conferenceService, ICrudService<Venue> venueService, ICrudService<Enrollment> enrollmentService)
        {
            _eventService = eventService;
            _roomService = roomService;
            _speakerService = speakerService;
            _conferenceService = conferenceService;
            _venueService = venueService;
            _enrollmentService = enrollmentService;

            _eventService.Init(ModelTypes.Event);
            _roomService.Init(ModelTypes.Room);
            _speakerService.Init(ModelTypes.Speaker);
            _conferenceService.Init(ModelTypes.Conference);
            _venueService.Init(ModelTypes.Venue);
            _enrollmentService.Init(ModelTypes.Enrollment);
        }
        public async Task OnGetAsync()
        {
            Events = await _eventService.GetAll();
            Speakers = await _speakerService.GetAll();
            Conferences = await _conferenceService.GetAll();
            Rooms = await _roomService.GetAll();
            Venues = await _venueService.GetAll();
            Enrollments = await _enrollmentService.GetAll();

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
