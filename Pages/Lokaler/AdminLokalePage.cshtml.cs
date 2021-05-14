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

        [BindProperty] public Room Room { get; set; }
        [BindProperty] public IFormFile Upload { get; set; }

        public List<Event> Events;
        public List<Event> EventsInRoom;
        public List<Floor> Floors;
        public List<RoomFeature> RoomFeatures;
        public List<Feature> Features;
        public List<Room> Rooms;

        public SelectList SelectListFloors { get; set; }
        public MultiSelectList SelectListFeatures { get; set; }

        private readonly ICrudService<Room> _roomService;
        private readonly ICrudService<Floor> _floorService;
        private readonly ICrudService<Event> _eventService;
        private readonly ICrudService<RoomFeature> _roomFeatureService;
        private readonly ICrudService<Feature> _featureService;

        public AdminLokalePageModel(ICrudService<Room> roomService, ICrudService<Floor> floorService, ICrudService<Event> eventService, ICrudService<RoomFeature> roomFeatureService, ICrudService<Feature> featureService)
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
            _roomFeatureService.Init_Composite(ModelTypes.Feature,ModelTypes.Room, ModelTypes.RoomFeature);
        }

        public async Task OnGetAsync()
        {
            Room = new Room();
            Events = await _eventService.GetAll();
            Floors = await _floorService.GetAll();
            Features = await _featureService.GetAll();
            Rooms = await _roomService.GetAll();
            RoomFeatures = _roomFeatureService.GetAll().Result.FindAll(room => room.RoomId.Equals(Room.RoomId));
            if (RoomFeatures.Count != 0)
            {
                foreach (var roomFeature in RoomFeatures)
                {
                    Room.Features.Add(_featureService.GetFromId(roomFeature.FeatureId).Result.Name, roomFeature.IsAvailable);
                }
            }

            EventsInRoom = Room.RoomId != 0 ? new List<Event>(Events.FindAll(e => e.RoomId.Equals(Room.RoomId))) : new List<Event>();

            SelectListFloors = new SelectList(Floors.FindAll(f => f.VenueId.Equals(tempVenueId)), nameof(Floor.FloorId), nameof(Floor.Name));
            SelectListFeatures = new MultiSelectList(Features, nameof(Feature.Name), nameof(Feature.Name));
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
    }
}
