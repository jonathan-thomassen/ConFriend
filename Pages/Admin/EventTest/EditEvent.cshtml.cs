using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Admin.EventTest
{
    public class EditEventModel : PageModel
    {
        [BindProperty] public Event NewEvent { get; set; }
        private ICrudService<Event> eventService;
        public EditEventModel(ICrudService<Event> eService)
        {
            eventService = eService;
            eventService.Init(ModelTypes.Event);
        }
        public void OnGet(int eId)
        {
            NewEvent = eventService.GetFromId(eId);
        }

        public IActionResult OnPost()
        {
            eventService.Update(NewEvent);
            return RedirectToPage("Index");
        }
    }
}
