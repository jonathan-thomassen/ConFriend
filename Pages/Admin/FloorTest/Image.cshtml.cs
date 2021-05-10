using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Admin.FloorTest
{
    public class ImageModel : PageModel
    {
        private ICrudService<Floor> _floorService;

        [BindProperty]
        public IFormFile Upload { get; set; }

        public Floor Floor;
        public string FloorImagePath;

        public ImageModel(ICrudService<Floor> floorService)
        {
            _floorService = floorService;
            _floorService.Init(ModelTypes.Floor);
        }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                Floor = _floorService.GetFromId((int)id);
            }
        }

        public IActionResult OnPost(int? id)
        {
            if (id != null)
            {
                Floor = _floorService.GetFromId((int)id);
                var file = Path.Combine("wwwroot\\", "floors", Upload.FileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    Upload.CopyTo(fileStream);
                }

                Floor.Image = Upload.FileName;
                _floorService.Update(Floor);
            }
            return Page();
        }
    }
}
