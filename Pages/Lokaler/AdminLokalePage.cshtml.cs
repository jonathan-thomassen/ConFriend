using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConFriend.Pages.Lokaler
{
    public class AdminLokalePageModel : PageModel
    {
        [BindProperty] public int RoomId { get; set; } 
        private int tempVenueId = 1;
        [BindProperty] public bool IsEditing { get; set; }
        private readonly object _createRoomLock = new object();

        [BindProperty] public Room Room { get; set; }
        [BindProperty] public IFormFile Upload { get; set; }
        [BindProperty] public List<int> SelectedFeatures { get; set; }

        public List<Event> Events;
        public List<Event> EventsInRoom;
        public List<Floor> Floors;
        public List<RoomFeature> RoomFeatures;
        public List<Feature> Features;
        public List<Room> Rooms;

        public SelectList SelectListFloors { get; set; }
        public SelectList SelectListFeatures { get; set; }

        private readonly ICrudService<Room> _roomService;
        private readonly ICrudService<Floor> _floorService;
        private readonly ICrudService<Event> _eventService;
        private readonly ICrudService<RoomFeature> _roomFeatureService;
        private readonly ICrudService<Feature> _featureService;

        public AdminLokalePageModel(ICrudService<Room> roomService, ICrudService<Floor> floorService,
            ICrudService<Event> eventService, ICrudService<RoomFeature> roomFeatureService,
            ICrudService<Feature> featureService)
        {
            _roomService = roomService;
            _floorService = floorService;
            _eventService = eventService;
            _roomFeatureService = roomFeatureService;
            _featureService = featureService;

            _roomService.Init(ModelTypes.Room);
            _floorService.Init(ModelTypes.Floor);
            _eventService.Init(ModelTypes.Event);
            _featureService.Init(ModelTypes.Feature);
            _roomFeatureService.Init_Composite(ModelTypes.Feature, ModelTypes.Room, ModelTypes.RoomFeature);
        }

        public async Task OnGetAsync()
        {
            Room = new Room();
            Events = new List<Event>();
            Floors = await _floorService.GetAll();
            Features = await _featureService.GetAll();
            Rooms = await _roomService.GetAll();
            RoomFeatures = _roomFeatureService.GetAll().Result.FindAll(room => room.RoomId.Equals(Room.RoomId));

            EventsInRoom = Room.RoomId != 0
                ? new List<Event>(Events.FindAll(e => e.RoomId.Equals(Room.RoomId)))
                : new List<Event>();

            SelectListFloors = new SelectList(Floors.FindAll(f => f.VenueId.Equals(tempVenueId)), nameof(Floor.FloorId),
                nameof(Floor.Name));
            SelectListFeatures = new SelectList(Features, nameof(Feature.FeatureId), nameof(Feature.Name), RoomFeatures);
        }
        public async Task OnGetEditAsync(int rId)
        {
            RoomId = rId;
            Room = await _roomService.GetFromId(rId);
            Events = _eventService.GetAll().Result.FindAll(e=>e.RoomId.Equals(rId));
            Floors = await _floorService.GetAll();
            Features = await _featureService.GetAll();
            Rooms = await _roomService.GetAll();
            RoomFeatures = _roomFeatureService.GetAll().Result.FindAll(room => room.RoomId.Equals(Room.RoomId));

            foreach (RoomFeature rf in RoomFeatures)
            {
                Room.Features.Add(rf.FeatureId, true);
            }


            EventsInRoom = Room.RoomId != 0
                ? new List<Event>(Events.FindAll(e => e.RoomId.Equals(Room.RoomId)))
                : new List<Event>();

            SelectListFloors = new SelectList(Floors.FindAll(f => f.VenueId.Equals(tempVenueId)), nameof(Floor.FloorId), nameof(Floor.Name));
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
        }

        public async Task<IActionResult> OnPostImageAsync(int id)
        {
            var file = Path.Combine("wwwroot\\", "rooms", Upload.FileName);
            await using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }

            await OnGetAsync();

            Room.Image = Upload.FileName;

            IsEditing = true;
            RoomId = id;
            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync(string imageName)
        {
            Room.VenueId = tempVenueId;
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
                int maxId = 0;
                _roomService.Create(Room).Wait();

                maxId = _roomService.GetAll().Result.OrderByDescending(r => r.RoomId).First().RoomId;

                foreach (int featureId in Room.Features.Keys)
                {

                    RoomFeature rf = new RoomFeature {FeatureId = featureId, RoomId = maxId, IsAvailable = true};

                    _roomFeatureService.Create(rf).Wait();
                }
            }

            return RedirectToPage("/Admin/RoomTest/Index");
        }
        public async Task<IActionResult> OnPostUpdateAsync(string imageName, int id)
        {
            Room.RoomId = id;
            Room.Floor = _floorService.GetFromId(Room.FloorId).Result.Name;
            Room.Events = _eventService.GetAll().Result.FindAll(e => e.RoomId.Equals(Room.RoomId));
            Room.VenueId = tempVenueId;
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
            foreach (int featureId in Room.Features.Keys)
            {
                RoomFeature rf = new RoomFeature();
                rf.FeatureId = featureId;
                rf.RoomId = Room.RoomId;
                rf.IsAvailable = true;

                _roomFeatureService.Create(rf).Wait();
            }
            return RedirectToPage("/Admin/RoomTest/Index");
        }
    }
}
