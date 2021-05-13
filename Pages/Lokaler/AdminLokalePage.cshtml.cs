using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConFriend.Pages.Lokaler
{
    public class AdminLokalePageModel : PageModel
    {
        private int tempVenueId = 1;

        [BindProperty] public Room Room { get; set; }

        public List<Event> Events;
        public List<Event> EventsInRoom;
        public List<Floor> Floors;
        public SelectList SelectListFloors { get; set; }

        private ICrudService<Room> _roomService;
        private ICrudService<Floor> _floorService;
        private ICrudService<Event> _eventService;

        public AdminLokalePageModel(ICrudService<Room> roomService, ICrudService<Floor> floorService, ICrudService<Event> eventService)
        {
            _roomService = roomService;
            _floorService = floorService;
            _eventService = eventService;

            _roomService.Init(ModelTypes.Room);
            _floorService.Init(ModelTypes.Floor);
            _eventService.Init(ModelTypes.Event);
        }

        public async Task OnGetAsync()
        {
            Room = new Room();
            Events = await _eventService.GetAll();
            Floors = await _floorService.GetAll();
            EventsInRoom = Room.RoomId != 0 ? new List<Event>(Events.FindAll(e => e.RoomId.Equals(Room.RoomId))) : new List<Event>();

            SelectListFloors = new SelectList(Floors.FindAll(f => f.VenueId.Equals(tempVenueId)), nameof(Floor.FloorId), nameof(Floor.Name));

        }
    }
}
