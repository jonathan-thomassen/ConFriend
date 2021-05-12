using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
//
namespace ConFriend.Pages.Admin.EventTest
{
    public class CreateEventModel : PageModel
    {
        [BindProperty] public Event NewEvent { get; set; }
        public List<Room> Rooms;
        public List<Venue> Venues;
        public List<Conference> Conferences;
        public List<Models.Speaker> Speakers;

        public SelectList SelectListVenues;
        public SelectList SelectListRooms;
        public SelectList SelectListSpeakers;
        public SelectList SelectListConferences;

        public int VenueId;

        private readonly ICrudService<Event> _eventService;
        private readonly ICrudService<Room> _roomService;
        private readonly ICrudService<Venue> _venueService;
        private readonly ICrudService<Conference> _conferenceService;
        private readonly ICrudService<Models.Speaker> _speakerService;

        public CreateEventModel(ICrudService<Event> eventService, ICrudService<Room> roomService, ICrudService<Venue> venueService, ICrudService<Conference> conferenceService, ICrudService<Models.Speaker> speakerService)
        {
            _eventService = eventService;
            _roomService = roomService;
            _venueService = venueService;
            _conferenceService = conferenceService;
            _speakerService = speakerService;

            _eventService.Init(ModelTypes.Event);
            _roomService.Init(ModelTypes.Room);
            _venueService.Init(ModelTypes.Venue);
            _conferenceService.Init(ModelTypes.Conference);
            _speakerService.Init(ModelTypes.Speaker);
        }
        public async Task OnGetAsync()
        {
            NewEvent = new Event();
            Venues = await _venueService.GetAll();
            Speakers = await _speakerService.GetAll();
            Conferences = await _conferenceService.GetAll();

            Venues.Insert(0, new Venue());
            Speakers.Insert(0, new Models.Speaker());
            Conferences.Insert(0, new Conference());

            SelectListVenues = new SelectList(Venues, nameof(Venue.VenueId), nameof(Venue.Name));
            SelectListSpeakers = new SelectList(Speakers, nameof(Models.Speaker.SpeakerId), nameof(Models.Speaker.FullName));
            SelectListConferences = new SelectList(Conferences, nameof(Conference.ConferenceId), nameof(Conference.Name));
        }

        public async Task<IActionResult> OnPostAsync(int? venueId, int? roomId)
        {
            if (roomId == 0)
            {
                Venues = await _venueService.GetAll();

                Rooms = await _roomService.GetAll();
                Speakers = await _speakerService.GetAll();
                Conferences = await _conferenceService.GetAll();

                Rooms.Insert(0, new Room());
                Speakers.Insert(0, new Models.Speaker());
                Conferences.Insert(0, new Conference());

                SelectListRooms = new SelectList(Rooms.FindAll(room => room.VenueId.Equals(venueId) || room.VenueId == 0), nameof(Room.RoomId), nameof(Room.Name));
                SelectListSpeakers = new SelectList(Speakers, nameof(Models.Speaker.SpeakerId), nameof(Models.Speaker.FullName));
                SelectListConferences = new SelectList(Conferences, nameof(Conference.ConferenceId), nameof(Conference.Name));
                
                VenueId = (int)venueId;
                ModelState.Clear();
                return Page();
            }

            NewEvent.RoomId = (int)roomId;

            if (!ModelState.IsValid)
                return RedirectToPage("Index");

            await _eventService.Create(NewEvent);
            return RedirectToPage("Index");
        }

        public async Task OnPostClearVenueChoiceAsync()
        {
            await OnGetAsync();
        }
    }
}
