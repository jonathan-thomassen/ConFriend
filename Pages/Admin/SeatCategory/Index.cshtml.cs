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

        public void OnGet()
        {
            SeatCategorylist = _seatCategoryService.GetAll();
        }
        public List<SeatCategory> GetSeatlist()
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
        public IActionResult OnPostSave(int? id)
        {
            if(id != null)
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