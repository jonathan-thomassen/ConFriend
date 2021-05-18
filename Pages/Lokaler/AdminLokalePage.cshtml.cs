using System;
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
        private int tempVenueId = 1;
        public bool IsEditing { get; set; } 

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
            Events = await _eventService.GetAll();
            Floors = await _floorService.GetAll();
            Features = await _featureService.GetAll();
            Rooms = await _roomService.GetAll();
            RoomFeatures = _roomFeatureService.GetAll().Result.FindAll(room => room.RoomId.Equals(Room.RoomId));
            //if (RoomFeatures.Count != 0)
            //{
            //    foreach (var roomFeature in RoomFeatures)
            //    {
            //        Room.Features.Add(_featureService.GetFromId(roomFeature.FeatureId).Result.Name,
            //            roomFeature.IsAvailable);
            //    }
            //}

            EventsInRoom = Room.RoomId != 0
                ? new List<Event>(Events.FindAll(e => e.RoomId.Equals(Room.RoomId)))
                : new List<Event>();

            SelectListFloors = new SelectList(Floors.FindAll(f => f.VenueId.Equals(tempVenueId)), nameof(Floor.FloorId),
                nameof(Floor.Name));
            SelectListFeatures = new SelectList(Features, nameof(Feature.FeatureId), nameof(Feature.Name), RoomFeatures);
        }
        public async Task OnGetEditAsync(int rId)
        {
            Room = await _roomService.GetFromId(rId);
            Events = await _eventService.GetAll();
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
            SelectedFeatures = new List<int>();
            IsEditing = true;
        }

        public async Task<IActionResult> OnPostImageAsync()
        {
            var file = Path.Combine("wwwroot\\", "events", Upload.FileName);
            await using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }

            await OnGetAsync();

            Room.Image = Upload.FileName;

            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            Room.VenueId = tempVenueId;
            Room.Floor = _floorService.GetFromId(Room.FloorId).Result.Name;
            Room.Events = _eventService.GetAll().Result.FindAll(e => e.RoomId.Equals(Room.RoomId));
            foreach (int fId in SelectedFeatures)
            {
                Room.Features ??= new Dictionary<int, bool>();

                Feature f = _featureService.GetFromId(fId).Result;
                Room.Features.Add(f.FeatureId, true);
            }
            await _roomService.Create(Room);

            int maxId = 0;
            foreach (Room r in _roomService.GetAll().Result)
            {
                if (r.RoomId > maxId)
                {
                    maxId = r.RoomId;
                }
            }
            foreach (int featureId in Room.Features.Keys)
            {

                RoomFeature rf = new RoomFeature();
                rf.FeatureId = featureId;
                rf.RoomId = maxId;
                rf.IsAvailable = true;

                _roomFeatureService.Create(rf).Wait();
            }

            return RedirectToPage("/Admin/RoomTest/Index");
        }
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            Room.Floor = _floorService.GetFromId(Room.FloorId).Result.Name;
            Room.Events = _eventService.GetAll().Result.FindAll(e => e.RoomId.Equals(Room.RoomId));
            Room.VenueId = tempVenueId;
            foreach (int fId in SelectedFeatures)
            {
                Room.Features ??= new Dictionary<int, bool>();

                Feature f = _featureService.GetFromId(fId).Result;
                Room.Features.Add(f.FeatureId, true);
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
