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
    public class ThemeListModel : PageModel
    {
        private readonly ICrudService<Theme> ThemeService;

        [FromRoute]
        public int? Id { get; set; }

        public bool IsNewEntry
        {
            get { return Id == null; }
        }

        [BindProperty(SupportsGet = true)]
        public List<Theme> Themes { get; private set; }

        [BindProperty]
        public Theme MyTheme { get; set; }

        public ThemeListModel(ICrudService<Theme> theme)
        {
            ThemeService = theme;
            this.ThemeService.Init(ModelTypes.Theme);
        }

        public void OnGet()
        {
            Themes = ThemeService.GetAll();
        }

        public IActionResult OnPostDelete(int htnr)
        {
            ThemeService.Delete(htnr);

            Themes = ThemeService.GetAll();
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Theme newTheme = new Theme();
            newTheme.ThemeId = Id ?? 0;
            newTheme.Name = MyTheme.Name;

            if (IsNewEntry)
            {
                ThemeService.Create(newTheme);
            }
            else
            {
                ThemeService.Update(newTheme);
            }

            return Page();
                //RedirectToPage("/Admin/Speaker/speakerCreate");
        }

    }
}
