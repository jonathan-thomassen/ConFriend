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

        private readonly ICrudService<Event> _eventService;
        private readonly ICrudService<Room> _roomService;
        private readonly ICrudService<Models.Speaker> _speakerService;
        private readonly ICrudService<Conference> _conferenceService;
        private readonly ICrudService<Venue> _venueService;

        public EventIndexModel(ICrudService<Event> eventService, ICrudService<Room> roomService, ICrudService<Models.Speaker> speakerService, ICrudService<Conference> conferenceService, ICrudService<Venue> venueService)
        {
            _eventService = eventService;
            _roomService = roomService;
            _speakerService = speakerService;
            _conferenceService = conferenceService;
            _venueService = venueService;

            _eventService.Init(ModelTypes.Event);
            _roomService.Init(ModelTypes.Room);
            _speakerService.Init(ModelTypes.Speaker);
            _conferenceService.Init(ModelTypes.Conference);
            _venueService.Init(ModelTypes.Venue);
        }
        public async Task OnGetAsync()
        {
            Events = await _eventService.GetAll();
            Speakers = await _speakerService.GetAll();
            Conferences = await _conferenceService.GetAll();
            Rooms = await _roomService.GetAll();
            Venues = await _venueService.GetAll();

            //User genericUser1 = new User();
            //User genericUser2 = new User();
            //User genericUser3 = new User();
            //Events[0].Users.AddFirst(genericUser1);
            //Events[1].Users.AddFirst(genericUser2);
            //Events[1].Users.AddFirst(genericUser3);
        }
    }
}
