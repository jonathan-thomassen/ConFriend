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
    public class ThemeCreatecshtmlModel : PageModel
    {
        private readonly ICrudService<Theme> ThemeService;

        [FromRoute]
        public int? Id { get; set; }

        public bool IsNewEntry
        {
            get { return Id == null; }
        }

        [BindProperty]
        public Theme MyTheme { get; set; }

        public ThemeCreatecshtmlModel(ICrudService<Theme> theme)
        {
            ThemeService = theme;
            this.ThemeService.Init(ModelTypes.Theme);
        }
        public void OnGet()
        {
        }
    }
}
