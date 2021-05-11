using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Admin.EventThemeTest
{
    public class CreateEventThemeModel : PageModel
    {
        [BindProperty] public EventTheme EventTheme { get; set; }
        private ICrudService<EventTheme> eventThemeService;
        public CreateEventThemeModel(ICrudService<EventTheme> etService)
        {
            eventThemeService = etService;
            eventThemeService.Init_Composite(ModelTypes.Theme, ModelTypes.Event, ModelTypes.EventTheme);
            EventTheme = new EventTheme();
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
            eventThemeService.Create(EventTheme);
            return RedirectToPage("Index");
        }
    }
}
