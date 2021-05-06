using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConFriend.Interfaces;
using ConFriend.Models;

namespace ConFriend.Pages
{
    public class speakerCreateModel : PageModel
    {
        private readonly ICrudService<Speaker> SpeakerService;
    

        [FromRoute]
        public int? Id { get; set; }

        public bool IsNewEntry
        {
            get { return Id == null; }
        }

        [BindProperty]
        public Speaker speak { get; set; }

        public speakerCreateModel(ICrudService<Speaker> speaks)
        {
            SpeakerService = speaks;
            this.SpeakerService.Init(ModelTypes.Speaker);
        }

        public IActionResult OnGet()
        {
            if (Id == null) return Page();
            speak = SpeakerService.GetFromId(Id.GetValueOrDefault()) ?? new Speaker();
            return Page();
        }
        //public async Task<IActionResult> OnGetAsync()
        //{

        //}

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Speaker newSpeaker = new Speaker();
            newSpeaker.SpeakerId = Id ?? 0;
            newSpeaker.FirstName = speak.FirstName;
            newSpeaker.LastName = speak.LastName;
            newSpeaker.Email = speak.Email;
            newSpeaker.Image = speak.Image;
            newSpeaker.Description = speak.Description;
            newSpeaker.Title = speak.Title;

            if (IsNewEntry)
            {
                SpeakerService.Create(newSpeaker);
            }
            else {

                SpeakerService.Update(newSpeaker);
            }

            return RedirectToPage("/Admin/Speaker/speakerCreate");
        }
      
    }
}
