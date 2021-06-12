using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Admin.RoomTest
{
    public class DeleteRoomModel : PageModel
    {
        private ICrudService<Room> _roomService;
        private ICrudService<RoomFeature> _roomFeatureService;
        [BindProperty] public Room Room{ get; set; }

        public DeleteRoomModel(ICrudService<Room> rService, ICrudService<RoomFeature> rfService)
        {
            _roomService = rService;
            _roomFeatureService = rfService;
            _roomService.Init(ModelTypes.Room);
        }
        public async Task OnGetAsync(int rId)
        {
            Room = await _roomService.GetFromId(rId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Room.Features?.Count > 0)
            {
                foreach (var rf in _roomFeatureService.GetAll().Result.FindAll(rf => rf.RoomId.Equals(Room.RoomId)))
                {
                    await _roomFeatureService.Delete(rf.FeatureId, Room.RoomId);
                }
            }
            
            await _roomService.Delete(Room.RoomId);

            return RedirectToPage("Index");
        }
    }
}
