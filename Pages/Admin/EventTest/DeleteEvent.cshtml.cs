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
    public class DeleteEventModel : PageModel
    {
        private ICrudService<Event> eventService;
        [BindProperty] public Event Event { get; set; }

        public DeleteEventModel(ICrudService<Event> eService)
        {
            eventService = eService;
            eventService.Init(ModelTypes.Event);
        }
        public async Task OnGetAsync(int eId)
        {
            Event = await eventService.GetFromId(eId);
        }

        public async Task<IActionResult> OnPostAsync(int eId)
        {  await eventService.Delete(Event.EventId);
            return RedirectToPage("Index");
        }
    }
}
