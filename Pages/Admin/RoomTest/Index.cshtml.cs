using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using ConFriend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Admin.RoomTest
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public List<Room> Rooms { get; private set; }
        public List<Venue> Venues { get; private set; }
        public List<Floor> Floors { get; private set; }

        public User CurrentUser;
        public Conference CurrentConference;
        public UserConferenceBinding UCBinding;

        public bool IsAdmin;

        private readonly ICrudService<Room> _roomService;
        private readonly ICrudService<Venue> _venueService;
        private readonly ICrudService<Floor> _floorService;
        private readonly ICrudService<User> _userService;
        private readonly ICrudService<Conference> _conferenceService;
        private readonly ICrudService<UserConferenceBinding> _ucBindingService;
        private readonly SessionService _sessionService;


        public IndexModel(ICrudService<Room> roomService, ICrudService<Venue> venueService, ICrudService<Floor> floorService, ICrudService<User> userService, 
            ICrudService<Conference> conferenceService, ICrudService<UserConferenceBinding> ucBindingService, SessionService sessionService)
        {
            _roomService = roomService;
            _venueService = venueService;
            _floorService = floorService;
            _userService = userService;
            _conferenceService = conferenceService;
            _ucBindingService = ucBindingService;
            _sessionService = sessionService;

            _roomService.Init(ModelTypes.Room);
            _venueService.Init(ModelTypes.Venue);
            _floorService.Init(ModelTypes.Floor);
            _userService.Init(ModelTypes.User);
            _conferenceService.Init(ModelTypes.Conference);
            _ucBindingService.Init(ModelTypes.UserConferenceBinding);
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (_sessionService.GetUserId(HttpContext.Session) != null && _sessionService.GetConferenceId(HttpContext.Session) != null)
            {
                CurrentUser = await _userService.GetFromId((int)_sessionService.GetUserId(HttpContext.Session));
                CurrentConference = await _conferenceService.GetFromId((int)_sessionService.GetConferenceId(HttpContext.Session));
                
                UCBinding = _ucBindingService.GetAll().Result.FindAll(bind => bind.UserId.Equals(CurrentUser.UserId))
                    .Find(bind => bind.ConferenceId.Equals(CurrentConference.ConferenceId));

                if (UCBinding?.UserType == UserType.Admin || UCBinding?.UserType == UserType.SuperUser)
                    IsAdmin = true;
            }

            Rooms = await _roomService.GetAll();
            Venues = await _venueService.GetAll();
            Floors = await _floorService.GetAll();
            return Page();
        }
    }
}
