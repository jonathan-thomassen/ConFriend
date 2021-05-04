using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConFriend.Interfaces;
using ConFriend.Models;

namespace ConFriend.Pages.Admin.UserPages
{
    public class UserIndexModel : PageModel
    {
        private readonly ICrudService<User> _userService;

        public List<User> Users { get; private set; }
        
        public UserIndexModel(ICrudService<User> userService)
        {
            _userService = userService;
            Users = _userService.GetAll();
        }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
