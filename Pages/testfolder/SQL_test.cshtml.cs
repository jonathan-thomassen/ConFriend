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
        private readonly ICrudService<User> userServis;

        public List<User> UserList { get; private set; }
        public SQL_testModel(ICrudService<User> uService)
        {
            this.userServis = uService;
        }
        public void OnGet()
        {
            UserList = userServis.GetAll();
   
        }
    }
}
