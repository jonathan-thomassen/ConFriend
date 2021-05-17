using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using ConFriend.Services;
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

        public User CurrentUser;
        public Conference CurrentConference;
        public UserConferenceBinding UCBinding;

        public bool AccessDenied = false;

        private readonly ICrudService<Event> _eventService;
        private readonly ICrudService<Room> _roomService;
        private readonly ICrudService<Speaker> _speakerService;
        private readonly ICrudService<User> _userService;
        private readonly ICrudService<UserConferenceBinding> _ucBindingService;
        private readonly ICrudService<Conference> _conferenceService;
        private readonly SessionService _sessionService;


        public CreateEventModel(ICrudService<Event> eventService, ICrudService<Room> roomService, ICrudService<Speaker> speakerService, ICrudService<User> userService, ICrudService<UserConferenceBinding> ucBindingService, ICrudService<Conference> conferenceService, SessionService sessionService)
        {
            _eventService = eventService;
            _roomService = roomService;
            _speakerService = speakerService;
            _userService = userService;
            _conferenceService = conferenceService;
            _ucBindingService = ucBindingService;

            _eventService.Init(ModelTypes.Event);
            _roomService.Init(ModelTypes.Room);
            _speakerService.Init(ModelTypes.Speaker);
            _userService.Init(ModelTypes.User);
            _conferenceService.Init(ModelTypes.Conference);
            _ucBindingService.Init(ModelTypes.UserConferenceBinding);

            _sessionService = sessionService;
        }

        public async Task PageSetup()
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

        public async Task<IActionResult> OnGetAsync()
        {
            if (_sessionService.GetUserId(HttpContext.Session) != null)
                CurrentUser = await _userService.GetFromId((int)_sessionService.GetUserId(HttpContext.Session));
            else
                return OnGetDeniedAsync();

            if (_sessionService.GetConferenceId(HttpContext.Session) != null)
                CurrentConference = await _conferenceService.GetFromId((int)_sessionService.GetConferenceId(HttpContext.Session));
            else
                return RedirectToPage("/Index");

            UCBinding = _ucBindingService.GetAll().Result.FindAll(binding => binding.UserId.Equals(CurrentUser.UserId)).Find(binding => binding.ConferenceId.Equals(CurrentConference.ConferenceId));
            
            if (UCBinding.UserType != UserType.Admin && UCBinding.UserType != UserType.SuperUser)
                return OnGetDeniedAsync();

            await PageSetup();

            return Page();
        }

        public IActionResult OnGetDeniedAsync()
        {
            AccessDenied = true;
            return Page();
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

            await PageSetup();

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
