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
            roomService.Init(ModelTypes.Room);
        }
        public async Task OnGetAsync(int rId)
        {
            Room = await roomService.GetFromId(rId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await roomService.Delete(Room.RoomId);
            return RedirectToPage("Index");
        }
    }
}
