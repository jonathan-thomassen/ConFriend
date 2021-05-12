using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Admin.RoomTest
{
    public class ImageModel : PageModel
    {
        private ICrudService<Room> _roomService;

        [BindProperty]
        public IFormFile Upload { get; set; }

        public Room Room;

        public ImageModel(ICrudService<Room> roomService)
        {
            _roomService = roomService;
            _roomService.Init(ModelTypes.Room);
        }

        public async Task OnGetAsync(int? id)
        {
            if (id != null)
            {
                Room = await _roomService.GetFromId((int) id);
            }
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id != null)
            {
                Room = await _roomService.GetFromId((int)id);
                var file = Path.Combine("wwwroot\\", "rooms", Upload.FileName);
                await using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }

                Room.Image = Upload.FileName;
                await _roomService.Update(Room);
            }
            return Page();
        }
    }
}
