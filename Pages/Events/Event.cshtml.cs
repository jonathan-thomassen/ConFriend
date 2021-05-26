using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using ConFriend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Events
{
    public class EventModel : PageModel
    {
        private readonly ICrudService<Event> _eventService;
        private readonly ICrudService<Room> _roomService;
        private readonly ICrudService<Speaker> _speakerService;
        private readonly ICrudService<Enrollment> _enrollmentService;
        private readonly SessionService _sessionService;

        public Event Event;
        public List<Room> Rooms;
        public List<Speaker> Speakers;
        public Enrollment Enrollment;
        public bool? SuccessfullySignedUp;
        public int? CurrentUserId;
        public bool? AlreadyRegistered;
        public int? CurrentlyEnrolled;

        public int? RemainingCapacity => Event.Capacity - CurrentlyEnrolled;

        public EventModel(ICrudService<Event> eventService, ICrudService<Room> roomService, ICrudService<Speaker> speakerService, ICrudService<Enrollment> enrollmentService, SessionService sessionService)
        {
            _eventService = eventService;
            _roomService = roomService;
            _speakerService = speakerService;
            _enrollmentService = enrollmentService;

            _eventService.Init(ModelTypes.Event);
            _roomService.Init(ModelTypes.Room);
            _speakerService.Init(ModelTypes.Speaker);
            _enrollmentService.Init(ModelTypes.Enrollment);

            _sessionService = sessionService;
        }

        public async Task PageSetup(int id)
        {
            Event = await _eventService.GetFromId(id);
            Rooms = await _roomService.GetAll();
            Speakers = await _speakerService.GetAll();
            CurrentUserId = _sessionService.GetUserId(HttpContext.Session);
            CurrentlyEnrolled = _enrollmentService.GetAll().Result.FindAll(enrollment => enrollment.EventId.Equals(id)).Count;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return Page();

            await PageSetup((int) id);

            if (CurrentUserId != null)
                Enrollment = _enrollmentService.GetAll().Result.FindAll(enrollment => enrollment.EventId.Equals(id)).Find(enrollment => enrollment.UserId.Equals(CurrentUserId));

            if (Enrollment != null)
                AlreadyRegistered = true;
            else
                AlreadyRegistered = false;
           
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? eventId)
        {
            if (eventId == null)
                return BadRequest();

            int? currentUserId = _sessionService.GetUserId(HttpContext.Session);

            if (currentUserId != null)
            {
                Enrollment enrollment = new Enrollment
                {
                    UserId = currentUserId,
                    EventId = eventId,
                    SignUpTime = DateTime.Now
                };

                SuccessfullySignedUp = await _enrollmentService.Create(enrollment);
            }

            await PageSetup((int) eventId);

            return Page();
        }
    }
}
