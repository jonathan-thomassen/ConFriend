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
    public class VenueIndexModel : PageModel
    {
        private readonly ICrudService<Venue> _venueService;

        public List<Venue> Venues { get; private set; }
        
        public VenueIndexModel(ICrudService<Venue> venueService)
        {
            _venueService = venueService;
        }

        public IActionResult OnGet()
        {
            Venues = _venueService.GetAll();
            return Page();
        }
    }
}
