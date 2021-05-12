using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Admin.RoomFeatureTest
{
    public class EditRoomFeatureModel : PageModel
    {
        [BindProperty] public RoomFeature RoomFeature { get; set; }
        public bool IsDeleting { get; set; }    
        private ICrudService<RoomFeature> roomFeatureService;
        public EditRoomFeatureModel(ICrudService<RoomFeature> rfService)
        {
            roomFeatureService = rfService;
            roomFeatureService.Init_Composite(ModelTypes.Feature, ModelTypes.Room, ModelTypes.RoomFeature);
        }
        public async Task OnGetAsync(int fId, int rId)
        {
            RoomFeature = await roomFeatureService.GetFromId(fId, rId);
        }
        public async Task OnGetDeleteAsync(int fId, int rId)
        {
            RoomFeature = await roomFeatureService.GetFromId(fId, rId);
            IsDeleting = true;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await roomFeatureService.Update(RoomFeature);
            return RedirectToPage("Index");
        }
        public async Task<IActionResult> OnPostDeleteAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await roomFeatureService.Delete(RoomFeature.FeatureId, RoomFeature.RoomId);
            return RedirectToPage("Index");
        }
    }
}
