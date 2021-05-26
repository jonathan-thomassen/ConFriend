using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Login
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGetAsync()
        {
            HttpContext.Session.Clear();

            return RedirectToPage("/Index");
        }
    }
}
