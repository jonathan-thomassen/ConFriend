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

        public List<Floor> Floors { get; set; }
        public List<Venue> Venues { get; set; }
        public SelectList SelectListFloors { get; set; }
        public SelectList SelectListVenues { get; set; }
        public int VenueId { get; set; }
        public int FloorId { get; set; }

        private readonly ICrudService<Room> _roomService;
        private readonly ICrudService<Venue> _venueService;
        private readonly ICrudService<Floor> _floorService;

        public CreateRoomModel(ICrudService<Room> roomService, ICrudService<Floor> floorService, ICrudService<Venue> venueService)
        {
            _roomService = roomService;
            _floorService = floorService;
            _venueService = venueService;
            _roomService.Init(ModelTypes.Room);
            _venueService.Init(ModelTypes.Venue);
            _floorService.Init(ModelTypes.Floor);
        }

        public void OnGet()
        {
            Venues = _venueService.GetAll();
            Venues.Insert(0, new Venue());

            SelectListVenues = new SelectList(Venues, nameof(Venue.VenueId), nameof(Venue.Name));
        }

        public IActionResult OnPost(int? venueId, int? floorId)
        {
            if (floorId == 0)
            {
                Venues = _venueService.GetAll();
                Floors = _floorService.GetAll();
                Floors.Insert(0 , new Floor());
                SelectListFloors = new SelectList(Floors.FindAll(floor => floor.VenueId.Equals(venueId) || floor.VenueId == 0), nameof(Floor.FloorId), nameof(Floor.Name));
                NewRoom.VenueId = (int)venueId;
                VenueId = (int)venueId;

                ModelState.Clear();
                return Page();
            }

            NewRoom.VenueId = (int)venueId;
            NewRoom.FloorId = (int)floorId;

            if (!ModelState.IsValid)
                return RedirectToPage("Index");

            _roomService.Create(NewRoom);
            return RedirectToPage("Index");
        }

        public void OnPostClearVenueChoice()
        {
            OnGet();
        }
    }
}
