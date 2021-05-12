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
    public class EditRoomModel : PageModel
    {
        [BindProperty] public Room NewRoom { get; set; }
        public bool done { get; set; }
        private ICrudService<Room> roomService;
        public EditRoomModel(ICrudService<Room> rService)
        {
            roomService = rService;
            roomService.Init(ModelTypes.Room);
            done = false;
        }
        public async Task OnGetAsync(int rId)
        {
            NewRoom = await roomService.GetFromId(rId);
        }

        public async Task OnPostAsync()
        {
            await roomService.Update(NewRoom);
            done = true;
        }
    }
}
