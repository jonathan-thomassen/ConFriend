using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using ConFriend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.UserPages
{
    public class UserDetailsModel : PageModel
    {
        private readonly ICrudService<User> _userService;
        private readonly ICrudService<UserConferenceBinding> _ucBindingService;
        private readonly ICrudService<Conference> _conferenceService;
        private readonly SessionService _sessionService;

        public int? CurrentUserId;
        public User CurrentUser;
        public List<UserConferenceBinding> UserBindings;
        public List<Conference> Conferences;

        public UserDetailsModel(ICrudService<User> userService, ICrudService<UserConferenceBinding> ucBindingService, ICrudService<Conference> conferenceService, SessionService sessionService)
        {
            _userService = userService;
            _ucBindingService = ucBindingService;
            _conferenceService = conferenceService;

            _userService.Init(ModelTypes.User);
            _ucBindingService.Init(ModelTypes.UserConferenceBinding);
            _conferenceService.Init(ModelTypes.Conference);

            _sessionService = sessionService;
        }

        public async Task OnGet()
        {
            CurrentUserId = _sessionService.GetUserId(HttpContext.Session);

            if (CurrentUserId != null)
            {
                CurrentUser = await _userService.GetFromId((int)CurrentUserId);
                UserBindings = _ucBindingService.GetAll().Result.FindAll(binding => binding.UserId.Equals(CurrentUserId));
                Conferences = await _conferenceService.GetAll();
            }
        }
    }
}
