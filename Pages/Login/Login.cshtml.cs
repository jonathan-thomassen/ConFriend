using System.Collections.Generic;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using ConFriend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly ICrudService<User> _userService;
        private readonly SessionService _sessionService;

        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Password { get; set; }

        private List<User> _users;
        public string WrongInput = "";

        public LoginModel(ICrudService<User> userService, SessionService sessionService)
        {
            _userService = userService;
            _userService.Init(ModelTypes.User);

            _sessionService = sessionService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //Bør ikke være mulig for bruger der allerede er logget ind at benytte denne side:
            if (_sessionService.GetUserId(HttpContext.Session) > 0)
                return RedirectToPage("/Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _users = await _userService.GetAll();
            User user = _users.Find(u => u.Email.Equals(Email));

            if (user != null)
            {
                if (Password.Equals(user.Password))
                {
                    _sessionService.SetUserId(HttpContext.Session, user.UserId);
                    return RedirectToPage("/Index");
                }
                WrongInput = "Forkert brugernavn/kodeord.";
                return Page();
            }

            WrongInput = "Forkert brugernavn/kodeord.";
            return Page();
        }
    
        
    }
}
