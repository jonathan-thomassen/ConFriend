using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConFriend.Pages
{
    public class RoomSeatCateListModel : PageModel
    {
        private readonly ICrudService<RoomSeatCategory> _roomSeatCategoryService;
        private readonly ICrudService<Event> _eventService;
        private readonly ICrudService<Models.User> _userService;

        public List<User> MyUsers { get; set; }
        public List<Event> MyEvents { get; set; }

        public SelectList SelectEventList;
        public SelectList SelectUserList;

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public List<RoomSeatCategory> RoomSeatCategorys { get; private set; }

        public RoomSeatCateListModel(ICrudService<RoomSeatCategory> rfService, ICrudService<Event> eService, ICrudService<User> uService)
        {
            _roomSeatCategoryService = rfService;
            _eventService = eService;
            _userService = uService;
            _roomSeatCategoryService.Init(ModelTypes.Event);
            _roomSeatCategoryService.Init(ModelTypes.User);
            _roomSeatCategoryService.Init_Composite(ModelTypes.RoomSeatCategory, ModelTypes.Room, ModelTypes.SeatCategory);
            RoomSeatCategorys = _roomSeatCategoryService.GetAll().Result;

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