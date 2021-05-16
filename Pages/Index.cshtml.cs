using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using ConFriend.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ConFriend.Pages
{
    public class IndexModel : PageModel
    {
        private ICrudService<User> _userService;
        private SessionService _sessionService;

        public new User User;

        public IndexModel(ICrudService<User> userService, SessionService sessionService)
        {
            _userService = userService;
            _userService.Init(ModelTypes.User);

            _sessionService = sessionService;
        }

        public async Task OnGetAsync()
        {
            if (_sessionService.GetUserId(HttpContext.Session) != null)
                User = await _userService.GetFromId((int)_sessionService.GetUserId(HttpContext.Session));
        }

        public async Task OnPostAsync()
        {

        }
    }
}