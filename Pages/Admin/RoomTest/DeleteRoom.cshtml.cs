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
    public class DeleteRoomModel : PageModel
    {
        private ICrudService<Room> roomService;
        [BindProperty] public Room Room{ get; set; }

        public DeleteRoomModel(ICrudService<Room> rService)
        {
            roomService = rService;
        }
        public void OnGet(int rId)
        {
            Room = roomService.GetFromId(rId);
        }

        public IActionResult OnPost()
        {
            roomService.Delete(Room.RoomId);
            return RedirectToPage("Index");
        }
    }
}
