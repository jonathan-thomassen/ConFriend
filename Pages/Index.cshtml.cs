using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using ConFriend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ConFriend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICrudService<User> _userService;
        private readonly ICrudService<UserConferenceBinding> _ucBindingService;
        private readonly SessionService _sessionService;

        public new User User;
        public UserType UserType;

        public IndexModel(ICrudService<User> userService, ICrudService<UserConferenceBinding> ucBindingService, SessionService sessionService)
        {
            _userService = userService;
            _ucBindingService = ucBindingService;

            _userService.Init(ModelTypes.User);
            _ucBindingService.Init(ModelTypes.UserConferenceBinding);

            _sessionService = sessionService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (_sessionService.GetConferenceId(HttpContext.Session) == null)
                _sessionService.SetConferenceId(HttpContext.Session, 1);

            var userId = _sessionService.GetUserId(HttpContext.Session);
            var conId = _sessionService.GetConferenceId(HttpContext.Session);

            if (conId != null && userId != null)
            {
                int? bindingId = _ucBindingService.GetAll().Result.FindAll(binding => binding.UserId.Equals(userId)).Find(binding => binding.ConferenceId.Equals(conId))?.UserConferenceBindingId;
                if (bindingId != null)
                    User = await _userService.GetFromId((int)userId);
                    UserType = _ucBindingService.GetFromId((int)bindingId).Result.UserType;
                return Page();
            }

            return Page();
        }

        public async Task OnPostAsync()
        {

        }
    }
}