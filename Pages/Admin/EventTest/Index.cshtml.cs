using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Admin.EventTest
{
    public class IndexModel : PageModel
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

        public IndexModel(ICrudService<Event> eventService, ICrudService<Room> roomService, ICrudService<Models.Speaker> speakerService, ICrudService<Conference> conferenceService, ICrudService<Venue> venueService)
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
        }
    }
}
