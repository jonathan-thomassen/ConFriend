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
        public List<Theme> GetThemes()
        {
            Themes = ThemeService.GetAll();
            return Themes;
        }
     
        public IActionResult OnPostDelete(int id)
        {
            ThemeService.Delete(id);

            Themes = ThemeService.GetAll();
            return Page();
        }
        public IActionResult OnPostSave(int id)
        {
            Theme newTheme = new Theme();
            newTheme.ThemeId = id;
            newTheme.Name = MyTheme.Name;
            ThemeService.Update(newTheme);
            Themes = ThemeService.GetAll();
            return Page();
        }
        public IActionResult OnPostSaveNew(int id)
        {
            Theme newTheme = new Theme();
            newTheme.ThemeId = id;
            newTheme.Name = MyTheme.Name;

            ThemeService.Create(newTheme);
         
            Themes = ThemeService.GetAll();
            return Page();
        }
        public IActionResult OnPost()
        {
            
            if (!ModelState.IsValid)
            {
                
                return Page();
            }
            return Page();
                //RedirectToPage("/Admin/Speaker/speakerCreate");
        }

    }
}
