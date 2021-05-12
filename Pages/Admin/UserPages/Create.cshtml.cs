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
        public new User User { get; set; }

        public CreateModel(ICrudService<User> userService)
        {
            _userService = userService;
            _userService.Init(ModelTypes.User);
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            User.PasswordRepeat = null;
            await _userService.Create(User);
            return RedirectToPage("UserIndex");
        }
    }
}
