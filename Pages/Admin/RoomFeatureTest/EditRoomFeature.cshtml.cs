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
        public void OnGet(int fId, int rId)
        {
            RoomFeature = roomFeatureService.GetFromId(fId, rId);
        }
        public void OnGetDelete(int fId, int rId)
        {
            RoomFeature = roomFeatureService.GetFromId(fId, rId);
            IsDeleting = true;
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            roomFeatureService.Update(RoomFeature);
            return RedirectToPage("Index");
        }
        public IActionResult OnPostDelete()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            roomFeatureService.Delete(RoomFeature.FeatureId, RoomFeature.RoomId);
            return RedirectToPage("Index");
        }
    }
}
