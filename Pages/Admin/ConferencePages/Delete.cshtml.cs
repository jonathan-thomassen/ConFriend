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

        public IActionResult OnGet(int? id)
        {
            if (id == null)
                return NotFound();

            Conference = _conferenceService.GetFromId((int)id);
            Venues = _venueService.GetAll();

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
                return NotFound();

            _conferenceService.Delete((int)id);
            return RedirectToPage("ConferenceIndex");
        }
    }
}
