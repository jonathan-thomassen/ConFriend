using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Admin.ConferencePages
{
    public class EditModel : PageModel
    {
        private readonly ICrudService<Conference> _conferenceService;

        [BindProperty]
        public Conference Conference { get; set; }

        public EditModel(ICrudService<Conference>conferenceService)
        {
            _conferenceService = conferenceService;
        }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
                return NotFound();

            Conference = _conferenceService.GetFromId((int)id);

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (!ModelState.IsValid)
                return Page();
            if (id == null)
                return NotFound();

            Conference.ConferenceId = (int)id;
            _conferenceService.Update(Conference);
            return RedirectToPage("ConferenceIndex");
        }
    }
}
