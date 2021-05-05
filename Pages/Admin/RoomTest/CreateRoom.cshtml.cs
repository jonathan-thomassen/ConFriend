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
        //public int VenueId;
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
                Venues = _venueService.GetAll();
                Floors = _floorService.GetAll();
                VenueSet = true;
                SelectListFloors = new SelectList(Floors.FindAll(floor => floor.VenueId.Equals(NewRoom.VenueId)), nameof(Floor.VenueId), nameof(Floor.Name));
                //VenueId = NewRoom.VenueId;

                return Page();
            }
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //NewRoom.VenueId = VenueId;
            _roomService.Create(NewRoom);
            return Page();
        }
    }
}
