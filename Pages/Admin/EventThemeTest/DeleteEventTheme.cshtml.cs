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
        public void OnGet(int tId, int eId)
        {
            EventTheme = eventThemeService.GetFromId(tId, eId);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            eventThemeService.Delete(EventTheme.ThemeId, EventTheme.EventId);
            return RedirectToPage("Index");
        }
    }
}
