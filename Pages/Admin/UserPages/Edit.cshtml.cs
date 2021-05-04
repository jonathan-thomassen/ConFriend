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
    public class EditModel : PageModel
    {
        private readonly ICrudService<User> _userService;

        [BindProperty]
        public new User User { get; set; }

        public EditModel(ICrudService<User> uService)
        {
            _userService = uService;
        }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
                return NotFound();

            User = _userService.GetFromId((int)id);

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (!ModelState.IsValid)
                return Page();
            if (id == null)
                return NotFound();

            User.PasswordRepeat = null;
            User.UserId = (int)id;
            _userService.Update(User);
            return RedirectToPage("UserIndex");
        }
    }
}
