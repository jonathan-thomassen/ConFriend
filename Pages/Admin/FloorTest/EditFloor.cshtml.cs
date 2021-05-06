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
    public class EditFloorModel : PageModel
    {
        [BindProperty] public Floor NewFloor { get; set; }
        public bool done { get; set; }
        private ICrudService<Floor> floorService;
        public EditFloorModel(ICrudService<Floor> fService)
        {
            floorService = fService;
            floorService.Init(ModelTypes.Floor);
            done = false;
        }
        public void OnGet(int fId)
        {
            NewFloor = floorService.GetFromId(fId);
        }

        public void OnPost()
        {
            floorService.Update(NewFloor);
            done = true;
        }
    }
}
