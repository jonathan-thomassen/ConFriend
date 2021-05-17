using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using ConFriend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConFriend.Pages.UCBindingPages
{
    public class CreateBindingModel : PageModel
    {
        private ICrudService<UserConferenceBinding> _ucBindingService;
        private ICrudService<User> _userService;
        private ICrudService<Conference> _conferenceService;
        private SessionService _sessionService;

        public User CurrentUser;
        public Conference CurrentConference;
        public UserConferenceBinding CurrentBinding;
        public bool? BindingCreated;

        public List<User> Users;
        public List<Conference> Conferences;

        public SelectList SelectListUsers;
        public SelectList SelectListConferences;

        [BindProperty] public int UserId { get; set; }
        [BindProperty] public int ConferenceId { get; set; }
        [BindProperty] public UserType UserType { get; set; }

        public CreateBindingModel(ICrudService<UserConferenceBinding> ucBindingService, ICrudService<User> userService, ICrudService<Conference> conferenceService, SessionService sessionService)
        {
            _ucBindingService = ucBindingService;
            _userService = userService;
            _conferenceService = conferenceService;

            _ucBindingService.Init(ModelTypes.UserConferenceBinding);
            _userService.Init(ModelTypes.User);
            _conferenceService.Init(ModelTypes.Conference);

            _sessionService = sessionService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            int? userId = _sessionService.GetUserId(HttpContext.Session);
            int? conferenceId = _sessionService.GetConferenceId(HttpContext.Session);

            if (userId == null || conferenceId == null)
                return Unauthorized();

            CurrentUser = await _userService.GetFromId((int)userId);
            CurrentConference = await _conferenceService.GetFromId((int)_sessionService.GetConferenceId(HttpContext.Session));
            CurrentBinding = _ucBindingService.GetAll().Result.FindAll(binding => binding.UserId.Equals(CurrentUser.UserId)).Find(binding => binding.ConferenceId.Equals(CurrentConference.ConferenceId));

            if (CurrentBinding?.UserType != UserType.Admin && CurrentBinding?.UserType != UserType.SuperUser)
                return Unauthorized();

            Users = await _userService.GetAll();
            Conferences = await _conferenceService.GetAll();
            
            SelectListUsers = new SelectList(Users, nameof(Models.User.UserId), nameof(Models.User.Email));
            SelectListConferences = new SelectList(Conferences, nameof(Conference.ConferenceId), nameof(Conference.Name));

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            UserConferenceBinding newBinding = new UserConferenceBinding
            {
                UserId = UserId,
                ConferenceId = ConferenceId,
                UserType = UserType
            };

            BindingCreated = await _ucBindingService.Create(newBinding);

            Users = await _userService.GetAll();
            Conferences = await _conferenceService.GetAll();

            SelectListUsers = new SelectList(Users, nameof(Models.User.UserId), nameof(Models.User.Email));
            SelectListConferences = new SelectList(Conferences, nameof(Conference.ConferenceId), nameof(Conference.Name));

            return Page();
        }
    }
}
