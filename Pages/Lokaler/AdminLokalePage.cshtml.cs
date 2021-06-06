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

namespace ConFriend.Pages.Lokaler
{
    public class AdminLokalePageModel : PageModel
    {
        private readonly object _createRoomLock = new object();

        [BindProperty] public Room Room { get; set; }
        [BindProperty] public IFormFile Upload { get; set; }
        [BindProperty] public List<int> SelectedFeatures { get; set; }

        public List<Event> EventsInRoom;
        public List<Floor> Floors;
        public List<RoomFeature> RoomFeatures;
        public List<Feature> Features;
        public List<Room> Rooms;

        public SelectList SelectListFloors { get; set; }
        public SelectList SelectListFeatures { get; set; }

        public User CurrentUser;
        public Conference CurrentConference;
        public UserConferenceBinding UCBinding;

        public bool IsEditing;
        public bool AccessDenied;

        private readonly ICrudService<Room> _roomService;
        private readonly ICrudService<Floor> _floorService;
        private readonly ICrudService<Event> _eventService;
        private readonly ICrudService<RoomFeature> _roomFeatureService;
        private readonly ICrudService<Feature> _featureService;
        private readonly ICrudService<User> _userService;
        private readonly ICrudService<Conference> _conferenceService;
        private readonly ICrudService<UserConferenceBinding> _ucBindingService;
        private readonly SessionService _sessionService;

        public AdminLokalePageModel(ICrudService<Room> roomService, ICrudService<Floor> floorService,
            ICrudService<Event> eventService, ICrudService<RoomFeature> roomFeatureService,
            ICrudService<Feature> featureService, ICrudService<User> userService, ICrudService<Conference> conferenceService,
            ICrudService<UserConferenceBinding> ucBindingService, SessionService sessionService)
        {
            _sessionService = sessionService;
            _userService = userService;
            _conferenceService = conferenceService;
            _ucBindingService = ucBindingService;

            _roomService = roomService;
            _floorService = floorService;
            _eventService = eventService;
            _roomFeatureService = roomFeatureService;
            _featureService = featureService;
            

            _roomService.Init(ModelTypes.Room);
            _floorService.Init(ModelTypes.Floor);
            _eventService.Init(ModelTypes.Event);
            _featureService.Init(ModelTypes.Feature);
            _userService.Init(ModelTypes.User);
            _conferenceService.Init(ModelTypes.Conference);
            _ucBindingService.Init(ModelTypes.UserConferenceBinding);
            _roomFeatureService.Init_Composite(ModelTypes.Feature, ModelTypes.Room, ModelTypes.RoomFeature);
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

            UCBinding = _ucBindingService.GetAll().Result.FindAll(bind => bind.UserId.Equals(CurrentUser.UserId))
                .Find(bind => bind.ConferenceId.Equals(CurrentConference.ConferenceId));

            if (UCBinding?.UserType != UserType.Admin && UCBinding?.UserType != UserType.SuperUser)
                return OnGetDeniedAsync();

            Room = new Room();
            Floors = await _floorService.GetAll();
            Features = await _featureService.GetAll();
            //RoomFeatures = _roomFeatureService.GetAll().Result.FindAll(rf => rf.RoomId.Equals(Room.RoomId));

            SelectListFloors = new SelectList(Floors.FindAll(f => f.VenueId.Equals(CurrentConference.VenueId)), nameof(Floor.FloorId),
                nameof(Floor.Name));
            SelectListFeatures = new SelectList(Features, nameof(Feature.FeatureId), nameof(Feature.Name), RoomFeatures);

            return Page();
        }
        public async Task<IActionResult> OnGetEditAsync(int rId)
        {
            if (_sessionService.GetUserId(HttpContext.Session) != null)
                CurrentUser = await _userService.GetFromId((int)_sessionService.GetUserId(HttpContext.Session));
            else
                return OnGetDeniedAsync();

            if (_sessionService.GetConferenceId(HttpContext.Session) != null)
                CurrentConference = await _conferenceService.GetFromId((int)_sessionService.GetConferenceId(HttpContext.Session));
            else
                return RedirectToPage("/Index");

            UCBinding = _ucBindingService.GetAll().Result.FindAll(bind => bind.UserId.Equals(CurrentUser.UserId))
                .Find(bind => bind.ConferenceId.Equals(CurrentConference.ConferenceId));

            if (UCBinding?.UserType != UserType.Admin && UCBinding?.UserType != UserType.SuperUser)
                return OnGetDeniedAsync();

            Room = await _roomService.GetFromId(rId);
            Floors = await _floorService.GetAll();
            Features = await _featureService.GetAll();
            Rooms = await _roomService.GetAll();
            RoomFeatures = _roomFeatureService.GetAll().Result.FindAll(room => room.RoomId.Equals(Room.RoomId));

            foreach (RoomFeature rf in RoomFeatures)
            {
                Room.Features.Add(rf.FeatureId, true);
            }


            EventsInRoom = Room.RoomId != 0
                ? new List<Event>(_eventService.GetAll().Result.FindAll(e => e.RoomId.Equals(Room.RoomId)))
                : new List<Event>();

            SelectListFloors = new SelectList(Floors.FindAll(f => f.VenueId.Equals(CurrentConference.VenueId)), nameof(Floor.FloorId), nameof(Floor.Name));
            SelectListFeatures = new SelectList(Features, nameof(Feature.FeatureId), nameof(Feature.Name), RoomFeatures);

            foreach (var item in SelectListFeatures)
            {
                foreach (var rf in RoomFeatures)
                {
                    if (item.Value == rf.FeatureId.ToString())
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
            IsEditing = true;
            return Page();
        }

        public IActionResult OnGetDeniedAsync()
        {
            AccessDenied = true;
            return Page();
        }

        public async Task<IActionResult> OnPostImageAsync(bool edit)
        {
            var file = Path.Combine("wwwroot\\", "rooms", Upload.FileName);
            await using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }

            await OnGetAsync();

            Room.Image = Upload.FileName;

            IsEditing = edit;
            return Page();
        }

        public IActionResult OnPostCreate(string imageName, int venueId)
        {
            Room.VenueId = venueId;
            Room.Image = imageName;
            Room.Floor = _floorService.GetFromId(Room.FloorId).Result.Name;
            Room.Events = _eventService.GetAll().Result.FindAll(e => e.RoomId.Equals(Room.RoomId));
            foreach (int fId in SelectedFeatures)
            {
                Room.Features ??= new Dictionary<int, bool>();

                Feature f = _featureService.GetFromId(fId).Result;
                Room.Features.Add(f.FeatureId, true);
            }


            lock (_createRoomLock)
            {
                _roomService.Create(Room).Wait();

                if (Room.Features != null)
                {
                    if (Room.Features.Count != 0)
                    {
                        int maxId = 0;
                        maxId = _roomService.GetAll().Result.OrderByDescending(r => r.RoomId).First().RoomId;

                        foreach (int featureId in Room.Features.Keys)
                        {

                            RoomFeature rf = new RoomFeature
                            { FeatureId = featureId, RoomId = maxId, IsAvailable = true };

                            _roomFeatureService.Create(rf).Wait();
                        }
                    }
                }
            }

            return RedirectToPage("/Admin/RoomTest/Index");
        }
        public async Task<IActionResult> OnPostUpdateAsync(string imageName, int venueId)
        {
            
            Room.Floor = _floorService.GetFromId(Room.FloorId).Result.Name;
            Room.Events = _eventService.GetAll().Result.FindAll(e => e.RoomId.Equals(Room.RoomId));
            Room.VenueId = venueId;
            Room.Image = imageName;
            RoomFeatures = _roomFeatureService.GetAll().Result.FindAll(room => room.RoomId.Equals(Room.RoomId));
            foreach (int fId in SelectedFeatures)
            {
                Room.Features ??= new Dictionary<int, bool>();

                Feature f = _featureService.GetFromId(fId).Result;
                Room.Features.Add(f.FeatureId, true);
            }

            foreach (var rf in RoomFeatures)
            {
                await _roomFeatureService.Delete(rf.FeatureId, rf.RoomId);
            }
            await _roomService.Update(Room);

            if (Room.Features?.Count > 0)
            {
                foreach (int featureId in Room.Features.Keys)
                {
                    RoomFeature rf = new RoomFeature();
                    rf.FeatureId = featureId;
                    rf.RoomId = Room.RoomId;
                    rf.IsAvailable = true;

                    _roomFeatureService.Create(rf).Wait();
                }
            }

            return RedirectToPage("/Admin/RoomTest/Index");
        }
    }
}
