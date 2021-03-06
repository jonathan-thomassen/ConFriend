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
    public class CreateFloorModel : PageModel
    {
        [BindProperty] public Floor NewFloor { get; set; }
        public bool done { get; set; }
        private ICrudService<Floor> floorService;
        public CreateFloorModel(ICrudService<Floor> fService)
        {
            floorService = fService;
            floorService.Init(ModelTypes.Floor);
            NewFloor = new Floor();
            done = false;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await floorService.Create(NewFloor);
            done = true;
            return Page();
        }
    }
}
