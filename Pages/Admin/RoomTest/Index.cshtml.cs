using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
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

        private readonly ICrudService<Room> _roomService;
        private readonly ICrudService<Venue> _venueService;
        private readonly ICrudService<Floor> _floorService;

        public IndexModel(ICrudService<Room> roomService, ICrudService<Venue> venueService, ICrudService<Floor> floorService)
        {
            _roomService = roomService;
            _venueService = venueService;
            _floorService = floorService;
            _roomService.Init(ModelTypes.Room);
            _venueService.Init(ModelTypes.Venue);
            _floorService.Init(ModelTypes.Floor);
        }
        public IActionResult OnGet()
        {
            Rooms = _roomService.GetAll();
            Venues = _venueService.GetAll();
            Floors = _floorService.GetAll();
            return Page();
        }
    }
}
