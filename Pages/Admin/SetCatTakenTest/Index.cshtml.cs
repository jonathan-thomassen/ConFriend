using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConFriend.Interfaces;
using ConFriend.Models;


namespace ConFriend.Pages.SetCatTakenTest
{
    public class IndexModel : PageModel
    {
        private readonly ICrudService<SeatCategoryTaken> _seatCategoryService;


        [BindProperty(SupportsGet = true)]
        public List<SeatCategoryTaken> SeatCategorylist { get; private set; }

        [BindProperty]
        public SeatCategoryTaken SeatType { get; set; }

        public IndexModel(ICrudService<SeatCategoryTaken> ss)
        {
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