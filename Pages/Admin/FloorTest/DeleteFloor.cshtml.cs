using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Admin.FloorTest
{
    public class DeleteFloorModel : PageModel
    {
        private ICrudService<Floor> floorService;
        [BindProperty] public Floor Floor { get; set; }

        public DeleteFloorModel(ICrudService<Floor> fService)
        {
            floorService = fService;
        }
        public void OnGet(int fId)
        {
            Floor = floorService.GetFromId(fId);
        }

        public IActionResult OnPost(int fId)
        {
            floorService.Delete(Floor.FloorId);
            return RedirectToPage("Index");
        }

    }
}
