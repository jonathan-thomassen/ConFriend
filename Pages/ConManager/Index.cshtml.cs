using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using ConFriend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConFriend.Pages.ConManager
{
    public class IndexModel : PageModel
    {
        private readonly ICrudService<UserConferenceBinding> _ucBindingService;
        private readonly ICrudService<User> _userService;
        private readonly ICrudService<Conference> _conferenceService;
        private readonly ICrudService<Speaker> _speakerService;
        private readonly ICrudService<Venue> _venueService;
        private readonly ICrudService<Floor> _floorService;
        private readonly ICrudService<Theme> _themeService;
        private readonly ICrudService<Feature> _featureService;
        private readonly SessionService _sessionService;

        public User CurrentUser;
        public Conference CurrentConference;
        public UserConferenceBinding CurrentBinding;
        public Venue CurrentVenue;

        public bool? BindingCreated;
        public bool? ConNameChanged;
        public bool? VenueNameChanged;

        public List<User> Users;
        public List<Conference> Conferences;
        public List<Speaker> Speakers;
        public List<Venue> Venues;
        public List<Floor> Floors;
        public List<Theme> Themes;
        public List<Feature> Features;

        public SelectList SelectListUsers;
        public SelectList SelectListConferences;

        [BindProperty] public int UserId { get; set; }
        [BindProperty] public UserType UserType { get; set; }
        [BindProperty, MinLength(2)] public string ConferenceName { get; set; }
        [BindProperty, MinLength(2)] public string VenueName { get; set; }

        public IndexModel(ICrudService<UserConferenceBinding> ucBindingService, ICrudService<User> userService, ICrudService<Conference> conferenceService, ICrudService<Speaker> speakerService, ICrudService<Venue> venueService, ICrudService<Floor> floorService, ICrudService<Theme> themeService, ICrudService<Feature> featureService, SessionService sessionService)
        {
            _ucBindingService = ucBindingService;
            _userService = userService;
            _conferenceService = conferenceService;
            _speakerService = speakerService;
            _venueService = venueService;
            _floorService = floorService;
            _themeService = themeService;
            _featureService = featureService;

            _ucBindingService.Init(ModelTypes.UserConferenceBinding);
            _userService.Init(ModelTypes.User);
            _conferenceService.Init(ModelTypes.Conference);
            _speakerService.Init(ModelTypes.Speaker);
            _venueService.Init(ModelTypes.Venue);
            _floorService.Init(ModelTypes.Floor);
            _themeService.Init(ModelTypes.Theme);
            _featureService.Init(ModelTypes.Feature);

            _sessionService = sessionService;
        }

        public async Task<bool> Initialize()
        {
            int? userId = _sessionService.GetUserId(HttpContext.Session);
            int? conferenceId = _sessionService.GetConferenceId(HttpContext.Session);

            if (userId == null || conferenceId == null)
                return false;

            CurrentUser = await _userService.GetFromId((int)userId);
            CurrentConference = await _conferenceService.GetFromId((int)conferenceId);
            CurrentBinding = _ucBindingService.GetAll().Result.FindAll(binding => binding.UserId.Equals(CurrentUser.UserId)).Find(binding => binding.ConferenceId.Equals(CurrentConference.ConferenceId));

            if (CurrentBinding?.UserType != UserType.Admin && CurrentBinding?.UserType != UserType.SuperUser)
                return false;

            Users = await _userService.GetAll();
            SelectListUsers = new SelectList(Users, nameof(Models.User.UserId), nameof(Models.User.Email));

            Conferences = await _conferenceService.GetAll();
            Speakers = await _speakerService.GetAll();
            Venues = await _venueService.GetAll();
            Floors = await _floorService.GetAll();
            Features = await _featureService.GetAll();
            Themes = await _themeService.GetAll();

            CurrentVenue = Venues.Find(venue => venue.VenueId.Equals(CurrentConference.VenueId));
            return true;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (await Initialize())
            {
                ConferenceName = CurrentConference.Name;
                VenueName = CurrentVenue?.Name;

                return Page();
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostBindingAsync()
        {
            if (await Initialize())
            {
                if (ModelState.IsValid)
                {
                    UserConferenceBinding existingBinding = _ucBindingService.GetAll().Result
                        .FindAll(binding => binding.UserId.Equals(UserId))
                        .Find(binding => binding.ConferenceId.Equals(CurrentConference.ConferenceId));

                    UserConferenceBinding newBinding = new UserConferenceBinding
                    {
                        UserId = UserId,
                        ConferenceId = CurrentConference.ConferenceId,
                        UserType = UserType
                    };

                    if (existingBinding != null)
                    {
                        if (existingBinding.UserType != UserType)
                        {
                            newBinding.UserConferenceBindingId = existingBinding.UserConferenceBindingId;
                            BindingCreated = await _ucBindingService.Update(newBinding);
                        }
                    }
                    else
                    {
                        BindingCreated = await _ucBindingService.Create(newBinding);
                    }
                }

                return Page();
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostVenueNameAsync()
        {
            if (await Initialize())
            {
                if (ModelState.IsValid)
                {
                    CurrentVenue.Name = VenueName;
                    await _venueService.Update(CurrentVenue);
                }

                return Page();
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostConferenceNameAsync()
        {
            if (await Initialize())
            {
                if (ModelState.IsValid)
                {
                    CurrentConference.Name = ConferenceName;
                    await _conferenceService.Update(CurrentConference);
                }

                return Page();
            }

            return BadRequest();
        }
    }
}
