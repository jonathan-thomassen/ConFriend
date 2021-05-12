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

        public async Task OnGetAsync()
        {
            SeatCategorylist = await _seatCategoryService.GetAll();
        }
        public async Task<List<SeatCategoryTaken>> GetSeatlistAsync()
        {
            SeatCategorylist = await _seatCategoryService.GetAll();
            return SeatCategorylist;
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            _seatCategoryService.Delete(id).Wait();
            SeatCategorylist = await _seatCategoryService.GetAll();
            return Page();
        }
        public async Task<IActionResult> OnPostSaveAsync(int? id, int? id2)
        {
            if (id != null)
                await _seatCategoryService.Update(SeatType);
            else
                await _seatCategoryService.Create(SeatType);
            Task.WaitAll();
            SeatCategorylist = await _seatCategoryService.GetAll();
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