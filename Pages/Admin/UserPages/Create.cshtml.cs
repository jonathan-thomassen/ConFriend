using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Admin.UserPages
{
    public class CreateModel : PageModel
    {
        private readonly ICrudService<User> _userService;

        [BindProperty]
        public User User { get; set; }

        public CreateModel(ICrudService<User> uService)
        {
            _userService = uService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            _userService.Create(User);

            return RedirectToPage("UserIndex");
        }
    }
}
