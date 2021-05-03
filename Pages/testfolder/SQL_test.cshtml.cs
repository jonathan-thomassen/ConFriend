using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConFriend.Interfaces;
using ConFriend.Models;

namespace ConFriend.Pages.testfolder
{
  
    public class SQL_testModel : PageModel
    {
        private readonly ICrudService<User> _userService;

        public List<User> UserList { get; private set; }

        [BindProperty]
        public int UserId { get; set; }
        public User TestUser { get; private set; }

        public SQL_testModel(ICrudService<User> uService)
        {
            _userService = uService;
            TestUser = new User();
            UserList = _userService.GetAll();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPostGetUser()
        {
            TestUser = _userService.GetFromId(UserId);

            return Page();
        }
    }
}
