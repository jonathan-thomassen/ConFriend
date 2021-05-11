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
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public List<EventTheme> EventThemes { get; private set; }

        private readonly ICrudService<EventTheme> _eventThemeService;

        public IndexModel(ICrudService<EventTheme> etService)
        {
            _eventThemeService = etService;
            _eventThemeService.Init_Composite(ModelTypes.Theme, ModelTypes.Event, ModelTypes.EventTheme);
            EventThemes = _eventThemeService.GetAll();
        }
        public void OnGet()
        {
        }
    }
}
