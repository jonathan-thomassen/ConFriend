using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConFriend.Pages.Admin.RoomFeatureTest
{
    public class CreateRoomFeatureModel : PageModel
    {
        [BindProperty] public RoomFeature RoomFeature { get; set; }
        private ICrudService<RoomFeature> roomFeatureService;
        public CreateRoomFeatureModel(ICrudService<RoomFeature> rfService)
        {
            roomFeatureService = rfService;
            roomFeatureService.Init_Composite(ModelTypes.Feature, ModelTypes.Room, ModelTypes.RoomFeature);
            RoomFeature = new RoomFeature();
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
            await roomFeatureService.Create(RoomFeature);
            return RedirectToPage("Index");
        }
    }
}
