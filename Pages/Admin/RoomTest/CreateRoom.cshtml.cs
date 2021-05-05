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
    public class CreateRoomModel : PageModel
    {
        [BindProperty] public Room NewRoom { get; set; }
        public bool done { get; set; }
        private ICrudService<Room> roomService;
        public CreateRoomModel(ICrudService<Room> rService)
        {
            roomService = rService;
            NewRoom = new Room();
            done = false;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            roomService.Create(NewRoom);
            done = true;
            return Page();
        }
    }
}
