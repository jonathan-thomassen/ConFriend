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
        [BindProperty(SupportsGet = true)] public string FilterCriteria { get; set; }
        public List<Room> Rooms { get; private set; }

        private ICrudService<Room> roomService;

        public IndexModel(ICrudService<Room> rService)
        {
            this.roomService = rService;
        }
        public void OnGet()
        {
            Rooms = roomService.GetAll();
        }
    }
}
