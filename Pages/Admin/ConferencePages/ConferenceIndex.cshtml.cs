using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Admin.ConferencePages
{
    public class ConferenceIndexModel : PageModel
    {
        private readonly ICrudService<Conference> _conferenceService;
        private readonly ICrudService<Venue> _venueService;

        public List<Conference> Conferences { get; private set; }
        public List<Venue> Venues { get; private set; }

        public ConferenceIndexModel(ICrudService<Conference> conferenceService, ICrudService<Venue> venueService)
        {
            _conferenceService = conferenceService;
            _venueService = venueService;
            _conferenceService.Init(ModelTypes.Conference);
            _venueService.Init(ModelTypes.Venue);
        }

        public IActionResult OnGet()
        {
            Conferences = _conferenceService.GetAll();
            Venues = _venueService.GetAll();
            return Page();
        }
    }
}
