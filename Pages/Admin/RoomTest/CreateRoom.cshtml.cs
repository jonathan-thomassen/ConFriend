using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConFriend.Pages.Admin.RoomTest
{
    public class CreateRoomModel : PageModel
    {
        [BindProperty] public Room NewRoom { get; set; }
        private Room _room;
        public List<Floor> Floors { get; set; }
        public List<Venue> Venues { get; set; }
        public SelectList SelectListFloors { get; set; }
        public SelectList SelectListVenues { get; set; }
        public bool VenueSet;

        private readonly ICrudService<Room> _roomService;
        private readonly ICrudService<Venue> _venueService;
        private readonly ICrudService<Floor> _floorService;

        public CreateRoomModel(ICrudService<Room> roomService, ICrudService<Floor> floorService, ICrudService<Venue> venueService)
        {
            _roomService = roomService;
            _floorService = floorService;
            _venueService = venueService;
        }
        public IActionResult OnGet()
        {
            Venues = _venueService.GetAll();
            SelectListVenues = new SelectList(Venues, nameof(Venue.VenueId), nameof(Venue.Name));
            VenueSet = false;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!VenueSet)
            {
                Floors = _floorService.GetAll();
                VenueSet = true;
                SelectListFloors = new SelectList(Floors.FindAll(floor => floor.VenueId.Equals(NewRoom.VenueId)), nameof(Floor.VenueId), nameof(Floor.Name));
                _room = NewRoom;

                return Page();
            }
            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _roomService.Create(NewRoom);
            return Page();
        }
    }
}
