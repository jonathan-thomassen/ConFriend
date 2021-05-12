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
    public class DeleteModel : PageModel
    {
        private readonly ICrudService<Venue> _venueService;

        [BindProperty]
        public Venue Venue { get; set; }

        public DeleteModel(ICrudService<Venue> venueService)
        {
            _venueService = venueService;
            venueService.Init(ModelTypes.Venue);
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Venue = await _venueService.GetFromId((int)id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            await _venueService.Delete((int)id);
            return RedirectToPage("VenueIndex");
        }
    }
}
