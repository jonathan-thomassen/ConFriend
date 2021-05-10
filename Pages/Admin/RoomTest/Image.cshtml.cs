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
        public string RoomImagePath;

        public ImageModel(ICrudService<Room> roomService)
        {
            _roomService = roomService;
            _roomService.Init(ModelTypes.Room);
        }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                Room = _roomService.GetFromId((int) id);
            }
        }

        public IActionResult OnPost(int? id)
        {
            if (id != null)
            {
                Room = _roomService.GetFromId((int)id);
                var file = Path.Combine("wwwroot\\", "uploads", Upload.FileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    Upload.CopyTo(fileStream);
                }

                Room.Image = Upload.FileName;
                _roomService.Update(Room);
            }
            return Page();
        }
    }
}
