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
        private readonly ICrudService<Conference> _conferenceService;
        private readonly SessionService _sessionService;

        public new User User;
        public Conference Conference;
        public UserType UserType;

        public IndexModel(ICrudService<User> userService, ICrudService<UserConferenceBinding> ucBindingService, ICrudService<Conference> conferenceService, SessionService sessionService)
        {
            _userService = userService;
            _ucBindingService = ucBindingService;
            _conferenceService = conferenceService;

            _userService.Init(ModelTypes.User);
            _ucBindingService.Init(ModelTypes.UserConferenceBinding);
            _conferenceService.Init(ModelTypes.Conference);

            _sessionService = sessionService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (_sessionService.GetConferenceId(HttpContext.Session) == null)
                _sessionService.SetConferenceId(HttpContext.Session, 1);

            var userId = _sessionService.GetUserId(HttpContext.Session);
            var conId = _sessionService.GetConferenceId(HttpContext.Session);

            if (conId != null)
                Conference = await _conferenceService.GetFromId((int)conId);
            if (userId != null)
                User = await _userService.GetFromId((int)userId);

            if (conId != null && userId != null)
            {
                int? bindingId = _ucBindingService.GetAll().Result.FindAll(binding => binding.UserId.Equals(userId)).Find(binding => binding.ConferenceId.Equals(conId))?.UserConferenceBindingId;
                if (bindingId != null)
                    UserType = _ucBindingService.GetFromId((int) bindingId).Result.UserType;
            }

            return Page();
        }

        public async Task OnPostAsync()
        {

        }
    }
}