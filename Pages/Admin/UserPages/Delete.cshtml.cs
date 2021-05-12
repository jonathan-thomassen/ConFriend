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
    public class DeleteModel : PageModel
    {
        private readonly ICrudService<User> _userService;

        [BindProperty]
        public new User User { get; set; }

        public DeleteModel(ICrudService<User> userService)
        {
            _userService = userService;
            _userService.Init(ModelTypes.User);
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            User = await _userService.GetFromId((int)id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            await _userService.Delete((int)id);
            return RedirectToPage("UserIndex");
        }
    }
}
