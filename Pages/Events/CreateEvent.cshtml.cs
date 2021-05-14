using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConFriend.Pages.Events
{
    public class CreateEventModel : PageModel
    {
        [BindProperty] public Event NewEvent { get; set; }
        [BindProperty] public int? Duration { get; set; }
        [BindProperty] public IFormFile Upload { get; set; }

        public List<Room> Rooms;
        public List<Speaker> Speakers;

        public SelectList SelectListRooms;
        public SelectList SelectListSpeakers;

        public int VenueId;

        private readonly ICrudService<Event> _eventService;
        private readonly ICrudService<Room> _roomService;
        private readonly ICrudService<Speaker> _speakerService;

        public CreateEventModel(ICrudService<Event> eventService, ICrudService<Room> roomService, ICrudService<Venue> venueService, ICrudService<Conference> conferenceService, ICrudService<Speaker> speakerService)
        {
            _eventService = eventService;
            _roomService = roomService;
            _speakerService = speakerService;

            _eventService.Init(ModelTypes.Event);
            _roomService.Init(ModelTypes.Room);
            _speakerService.Init(ModelTypes.Speaker);
        }

        public async Task OnGetAsync()
        {
            Rooms = await _roomService.GetAll();
            Speakers = await _speakerService.GetAll();

            Rooms.Insert(0, new Room { Name = "Vælg et lokale" });
            Speakers.Insert(0, new Models.Speaker { FirstName = "Vælg en taler" });

            SelectListRooms = new SelectList(Rooms.FindAll(room => room.VenueId.Equals(1) || room.VenueId == 0), nameof(Room.RoomId), nameof(Room.Name));
            SelectListSpeakers = new SelectList(Speakers, nameof(Speaker.SpeakerId), nameof(Speaker.FullName));

            SelectListRooms.First().Disabled = true;
            SelectListSpeakers.First().Disabled = true;

            SelectListRooms.First().Selected = true;
            SelectListSpeakers.First().Selected = true;
        }

        public async Task<IActionResult> OnPostAsync(string imageName)
        {
            NewEvent.ConferenceId = 1;
            if (Duration != null)
            {
                NewEvent.Duration = TimeSpan.FromMinutes((int)Duration);
            }

            NewEvent.Image = imageName;

            if (!ModelState.IsValid)
                return Page();

            await _eventService.Create(NewEvent);
            return RedirectToPage("/Admin/EventTest/Index");
        }

        public async Task<IActionResult> OnPostImageAsync()
        {
            var file = Path.Combine("wwwroot\\", "events", Upload.FileName);
            await using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }

            await OnGetAsync();

            NewEvent.Image = Upload.FileName;

            if (NewEvent.RoomId > 0)
                foreach (var element in SelectListRooms)
                    if (int.Parse(element.Value) == NewEvent.RoomId)
                    {
                        element.Selected = true;
                        break;
                    }

            if (NewEvent.SpeakerId > 0)
                foreach (var element in SelectListSpeakers)
                    if (int.Parse(element.Value) == NewEvent.SpeakerId)
                    {
                        element.Selected = true;
                        break;
                    }

            return Page();
        }
    }
}
