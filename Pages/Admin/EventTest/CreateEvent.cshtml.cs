using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Admin.EventTest
{
    public class CreateEventModel : PageModel
    {
        [BindProperty] public Event NewEvent { get; set; }
        public bool done { get; set; }
        private ICrudService<Event> eventService;
        public CreateEventModel(ICrudService<Event> eService)
        {
            eventService = eService;
            NewEvent = new Event();
            done = false;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            eventService.Create(NewEvent);
            done = true;
            return Page();
        }
    }
}
