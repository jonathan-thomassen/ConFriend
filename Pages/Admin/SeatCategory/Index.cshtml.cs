using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.SeatCate
{
    public class IndexModel : PageModel
    {
        private readonly ICrudService<SeatCategory> _seatCategoryService;


        [BindProperty(SupportsGet = true)]
        public List<SeatCategory> SeatCategorylist { get; private set; }

        [BindProperty]
        public SeatCategory SeatType { get; set; }

        public IndexModel(ICrudService<SeatCategory> ss)
        {
            _seatCategoryService = ss;
            _seatCategoryService.Init(ModelTypes.SeatCategory);
        }

        public async Task OnGetAsync()
        {
            SeatCategorylist = await _seatCategoryService.GetAll();
        }
        public async Task<List<SeatCategory>> GetSeatlistAsync()
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
        public async Task<IActionResult> OnPostSaveAsync(int? id)
        {
            if (id != null)
            {
                SeatType.SeatCategoryId = (int) id;
                await _seatCategoryService.Update(SeatType);
            }
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