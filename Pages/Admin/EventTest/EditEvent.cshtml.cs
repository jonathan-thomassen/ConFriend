using System.Threading.Tasks;
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
        public async Task OnGetAsync(int eId)
        {
            NewEvent = await eventService.GetFromId(eId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await eventService.Update(NewEvent);
            return RedirectToPage("Index");
        }
    }
}
