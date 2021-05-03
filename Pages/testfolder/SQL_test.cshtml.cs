using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConFriend.Interfaces;
using ConFriend.Models;

namespace ConFriend.Pages.debug
{
  
    public class SQL_testModel : PageModel
    {
        private readonly ICrudService<User> userService;

        public List<User> UserList { get; private set; }

        [BindProperty]
        public int UserId { get; set; }
        public User TestUser { get; private set; }

        public SQL_testModel(ICrudService<User> uService)
        {
            this.userService = uService;
            TestUser = new User();
        }

        public void OnGet()
        {
            User user = new User();
            user.UserId = 2;
            user.FirstName = "Kasper";
            user.LastName = "Jensen";
            user.Email = "asdf";
            user.Password = "qwertyu";
            userService.Update(user);
            UserList = userService.GetAll();
        }

        public IActionResult OnPostGetUser()
        {
            User user = new User();
            user.UserId = 2;
            user.FirstName = "Kasper";
            user.LastName = "Jensen";
            user.Email = "asdf";
            user.Password = "qwertyu";
            userService.Update(user);
            UserList = userService.GetAll();

            TestUser = userService.GetFromId(UserId);

            return Page();
        }
    }
}
