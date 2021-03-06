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
    public class CreateModel : PageModel
    {
        private readonly ICrudService<Venue> _venueService;

        [BindProperty]
        public Venue Venue { get; set; }

        public CreateModel(ICrudService<Venue> venueService)
        {
            _venueService = venueService;
            venueService.Init(ModelTypes.Venue);
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _venueService.Create(Venue);
            return RedirectToPage("VenueIndex");
        }
    }
}
