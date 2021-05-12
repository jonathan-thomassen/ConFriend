using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConFriend.Pages.Admin.ConferencePages
{
    public class CreateModel : PageModel
    {
        private readonly ICrudService<Conference> _conferenceService;
        private readonly ICrudService<Venue> _venueService;

        [BindProperty]
        public Conference Conference { get; set; }
        public SelectList Venues { get; set; }

        public CreateModel(ICrudService<Conference> conferenceService, ICrudService<Venue> venueService)
        {
            _conferenceService = conferenceService;
            _venueService = venueService;
            _conferenceService.Init(ModelTypes.Conference);
            _venueService.Init(ModelTypes.Venue);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Venues = new SelectList(await _venueService.GetAll(), nameof(Venue.VenueId), nameof(Venue.Name));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _conferenceService.Create(Conference);
            return RedirectToPage("ConferenceIndex");
        }
    }
}
