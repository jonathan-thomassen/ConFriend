using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConFriend.Pages.SetCatTakenTest
{
    public class IndexModel : PageModel
    {
        private readonly ICrudService<SeatCategoryTaken> _seatCategoryService;
        private readonly ICrudService<Event> _eventService;
        private readonly ICrudService<User> _userService;

        [BindProperty(SupportsGet = true)]
        public List<SeatCategoryTaken> SeatCategorylist { get; private set; }

        [BindProperty]
        public SeatCategoryTaken SeatType { get; set; }

        public List<User> MyUsers { get; set; }
        public List<Event> MyEvents { get; set; }

        public SelectList SelectEventList;
        public SelectList SelectUserList;

        public IndexModel(ICrudService<SeatCategoryTaken> ss, ICrudService<Event> es, ICrudService<User> us)
        {
            _eventService = es;
            _userService = us;
            _eventService.Init(ModelTypes.Event);
            _userService.Init(ModelTypes.User);
            _seatCategoryService = ss;
            _seatCategoryService.Init_Composite(ModelTypes.SeatCategory, ModelTypes.Event, ModelTypes.SeatCategoryTaken);
       
        }

        public void OnGet()
        {
            SeatCategorylist = _seatCategoryService.GetAll();
        }
        public List<SeatCategoryTaken> GetSeatlist()
        {
            SeatCategorylist = _seatCategoryService.GetAll();
            return SeatCategorylist;
        }

        public IActionResult OnPostDelete(int id)
        {
            _seatCategoryService.Delete(id);

            SeatCategorylist = _seatCategoryService.GetAll();
            return Page();
        }
        public IActionResult OnPostSave(int? id, int? id2)
        {
            if (id != null)
                _seatCategoryService.Update(SeatType);
            else
                _seatCategoryService.Create(SeatType);
            SeatCategorylist = _seatCategoryService.GetAll();
            return Page();
        }

        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {

                return Page();
            }
            return Page();
            //RedirectToPage("/Admin/Speaker/speakerCreate");
        }

    }
}