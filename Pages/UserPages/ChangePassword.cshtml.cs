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

namespace ConFriend.Pages.UserPages
{
    public class ChangePasswordModel : PageModel
    {
        private readonly ICrudService<User> _userService;
        private readonly ICrudService<Conference> _conService;
        private readonly ICrudService<UserConferenceBinding> _userConService;
        private readonly SessionService _sessionService;

        [FromRoute]
        public long? Id { get; set; }

        public bool IsNewUser
        {
            get { return Id == null; }
        }
     

        public bool EmailExist;
        public User CurrentUser;
        public int? CurrentUserId;
        public bool? PasswordUpdated;
        public bool? WrongPassword;

        [BindProperty, Required(ErrorMessage = "Feltet skal udfyldes.")] public string UserName { get; set; }
        [BindProperty, Required(ErrorMessage = "Feltet skal udfyldes.")] public string UserEmail { get; set; }
        [BindProperty] public string? UserPrefs { get; set; }
        [BindProperty, Required(ErrorMessage = "Feltet skal udfyldes."), DataType(DataType.Password)] public string? CurrentPassword { get; set; }
        [BindProperty, Required(ErrorMessage = "Feltet skal udfyldes."), DataType(DataType.Password), MinLength(6, ErrorMessage = "Kodeordet skal være på minimum ni tegn.")] public string NewPassword { get; set; }
        [BindProperty, Required(ErrorMessage = "Feltet skal udfyldes."), DataType(DataType.Password), Compare(nameof(NewPassword), ErrorMessage = "Kodeordene er ikke ens.")] public string NewPasswordRepeat { get; set; }

        public ChangePasswordModel(ICrudService<User> userService, ICrudService<UserConferenceBinding> userCon, ICrudService<Conference> con, SessionService sessionService)
        {
            _conService = con;
            _userConService = userCon;

            _userService = userService;

            _userService.Init(ModelTypes.User);
            _userConService.Init(ModelTypes.UserConferenceBinding);
            _conService.Init(ModelTypes.Conference);

            _sessionService = sessionService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
           
            CurrentUserId = _sessionService.GetUserId(HttpContext.Session);
         

            if (CurrentUserId > 0)
                CurrentUser = await _userService.GetFromId((int)_sessionService.GetUserId(HttpContext.Session));


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.ClearValidationState(nameof(NewPasswordRepeat));
            if (NewPasswordRepeat == NewPassword)
                ModelState.MarkFieldValid(nameof(NewPasswordRepeat));
            else
                ModelState.AddModelError(nameof(NewPasswordRepeat), "Kodeordene er ikke ens.");

            bool EmailValid = true;
            if (UserEmail.Length < 5) EmailValid = false;
            if (UserEmail.Split('.').Length != 2) EmailValid = false;
            if (UserEmail.Split('@').Length != 2) EmailValid = false;
            if (UserEmail.IndexOf('.') < UserEmail.IndexOf('@')) EmailValid = false;

            if (EmailValid)
                ModelState.MarkFieldValid(nameof(UserEmail));
            else
                ModelState.AddModelError(nameof(UserEmail), "Ugyldig Email");

            if (UserName.Length>0)
                ModelState.MarkFieldValid(nameof(UserName));
            
            if (IsNewUser)
                ModelState.Remove(nameof(CurrentPassword));
            //ModelState.MarkFieldValid(nameof(CurrentPassword));

            if (!ModelState.IsValid) return Page();

            if (IsNewUser) {

                CurrentUser = new User();

                CurrentUser.Password = NewPassword;
                int firstnameInd = UserName.IndexOf(' ') + 1;
                if (firstnameInd != 0)
                {

                    CurrentUser.FirstName = UserName.Substring(0, firstnameInd - 1);
                    CurrentUser.LastName = UserName.Substring(firstnameInd, UserName.Length - firstnameInd);
                }
                else
                {
                    CurrentUser.FirstName = UserName;
                    CurrentUser.LastName = UserName;
                }
                CurrentUser.Email = UserEmail;
                CurrentUser.Preference = UserPrefs.Split(',').ToList();

                int? ConferenceIdtest = _sessionService.GetConferenceId(HttpContext.Session);
                if (ConferenceIdtest == null) return BadRequest();
                




                _userService.ClearItemData();
                User test = await _userService.GetFromField(CurrentUser.Identity(UserEmail));
                if (test != null){
                    EmailExist = true;
                    return Page();
                }
                await _userService.Create(CurrentUser);
               
                UserConferenceBinding _UserCon = new UserConferenceBinding();

                _UserCon.ConferenceId = (int)ConferenceIdtest;
               
                test = await _userService.GetFromField(CurrentUser.Identity(UserEmail));

                _UserCon.UserId = test.UserId;
                await _userConService.Create(_UserCon);

                //_UserCon.ConferenceId = currentConferenceId;
                // _userConService
                _sessionService.SetUserId(HttpContext.Session, test.UserId);

                return RedirectToPage("/index");
            }

            CurrentUserId = _sessionService.GetUserId(HttpContext.Session);

            if (CurrentUserId > 0)
            {
                CurrentUser = await _userService.GetFromId((int)_sessionService.GetUserId(HttpContext.Session));
                if (CurrentUser.Password == CurrentPassword)
                {
                    CurrentUser.Password = NewPassword;
                    PasswordUpdated = await _userService.Update(CurrentUser);
                }
                else
                    WrongPassword = true;
            }
            else
            {
                return BadRequest();
            }

            return Page();
        }
    }
}
