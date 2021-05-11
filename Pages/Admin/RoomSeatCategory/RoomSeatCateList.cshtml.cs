using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages
{
    public class RoomSeatCateListModel : PageModel
    {
        private readonly ICrudService<RoomSeatCategory> _roomSeatCategoryService;

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public List<RoomSeatCategory> RoomSeatCategorys { get; private set; }

        public RoomSeatCateListModel(ICrudService<RoomSeatCategory> rfService)
        {
            _roomSeatCategoryService = rfService;
            _roomSeatCategoryService.Init_Composite(ModelTypes.RoomSeatCategory, ModelTypes.Room, ModelTypes.SeatCategory);
            RoomSeatCategorys = _roomSeatCategoryService.GetAll();

        }
        public string GetRoomName(int roomId)
        {
            return $"{roomId}:";
        }
        public string GetSeatCategory(int seatCategoryId)
        {
            return $"{seatCategoryId}:";
        }
    }
}
//   RoomId 
//SeatCategoryId
//Amount