using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages
{
    public class ThemeListModel : PageModel
    {
        public List<Theme> Themes { get; private set; }

        public void OnGet()
        {
        }
    }
}
