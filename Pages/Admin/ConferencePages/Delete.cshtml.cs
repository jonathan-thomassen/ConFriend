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
    public class DeleteModel : PageModel
    {
        private readonly ICrudService<Conference> _conferenceService;
        private readonly ICrudService<Venue> _venueService;

        [BindProperty]
        public Conference Conference { get; set; }
        public List<Venue> Venues { get; set; }

        public DeleteModel(ICrudService<Conference> conferenceService, ICrudService<Venue> venueService)
        {
            _conferenceService = conferenceService;
            _venueService = venueService;
            _conferenceService.Init(ModelTypes.Conference);
            _venueService.Init(ModelTypes.Venue);
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Conference = await _conferenceService.GetFromId((int)id);
            Venues = await _venueService.GetAll();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            await _conferenceService.Delete((int)id);
            return RedirectToPage("ConferenceIndex");
        }
    }
}
