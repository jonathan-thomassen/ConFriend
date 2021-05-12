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
            floorService.Init(ModelTypes.Floor);
        }
        public async Task OnGetAsync(int fId)
        {
            Floor = await floorService.GetFromId(fId);
        }

        public async Task<IActionResult> OnPostAsync(int fId)
        {
            await floorService.Delete(Floor.FloorId);
            return RedirectToPage("Index");
        }

    }
}
