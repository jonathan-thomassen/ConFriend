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
        private readonly SessionService _sessionService;

        public User CurrentUser;
        public int? CurrentUserId;
        public bool? PasswordUpdated;
        public bool? WrongPassword;

        [BindProperty, Required(ErrorMessage = "Feltet skal udfyldes."), DataType(DataType.Password)] public string CurrentPassword { get; set; }
        [BindProperty, Required(ErrorMessage = "Feltet skal udfyldes."), DataType(DataType.Password), MinLength(6, ErrorMessage = "Kodeordet skal være på minimum seks tegn.")] public string NewPassword { get; set; }
        [BindProperty, Required(ErrorMessage = "Feltet skal udfyldes."), DataType(DataType.Password), Compare(nameof(NewPassword), ErrorMessage = "Kodeordene er ikke ens.")] public string NewPasswordRepeat { get; set; }

        public ChangePasswordModel(ICrudService<User> userService, SessionService sessionService)
        {
            _userService = userService;

            _userService.Init(ModelTypes.User);

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
            CurrentUserId = _sessionService.GetUserId(HttpContext.Session);

            if (CurrentUserId > 0)
            {
                CurrentUser = await _userService.GetFromId((int)_sessionService.GetUserId(HttpContext.Session));


                //OBS: I RazorPages kan man ikke bruge Compare i OnPost (Det giver en Error i ModelState). Derfor skal ValidationState sættes manuelt for felter der bruger Compare.
                ModelState.ClearValidationState(nameof(NewPasswordRepeat));
                if (NewPasswordRepeat == NewPassword)
                    ModelState.MarkFieldValid(nameof(NewPasswordRepeat));
                else
                    ModelState.AddModelError(nameof(NewPasswordRepeat), "Kodeordene er ikke ens.");

                if (ModelState.IsValid)
                {
                    if (CurrentUser.Password == CurrentPassword)
                    {
                        CurrentUser.Password = NewPassword;
                        PasswordUpdated = await _userService.Update(CurrentUser);
                    }
                    else
                        WrongPassword = true;
                }
            }
            else
            {
                return BadRequest();
            }

            return Page();
        }
    }
}
