using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Admin.EventThemeTest
{
    public class DeleteEventThemeModel : PageModel
    {
        [BindProperty] public EventTheme EventTheme { get; set; }
        private ICrudService<EventTheme> eventThemeService;
        public DeleteEventThemeModel(ICrudService<EventTheme> etService)
        {
            eventThemeService = etService;
            eventThemeService.Init_Composite(ModelTypes.Theme, ModelTypes.Event, ModelTypes.EventTheme);
        }
        public async Task OnGetAsync(int tId, int eId)
        {
            EventTheme = await eventThemeService.GetFromId(tId, eId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await eventThemeService.Delete(EventTheme.ThemeId, EventTheme.EventId);
            return RedirectToPage("Index");
        }
    }
}
