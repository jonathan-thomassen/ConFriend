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
        private readonly ICrudService<Speaker> SpeakerServis;
    

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
            SpeakerServis = speaks;
      
        }

        public IActionResult OnGet()
        {
            if (Id == null) return Page();
            //speak = SpeakerServis.GetFromId(Id.GetValueOrDefault())
            //     ?? new Speaker();
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

            newSpeaker.FirstName = speak.FirstName;
            newSpeaker.LastName = speak.LastName;
            newSpeaker.Email = speak.Email;
            newSpeaker.Image = speak.Image;
            newSpeaker.Description = speak.Description;
            newSpeaker.Title = speak.Title;

            if (IsNewEntry)
            {
                //SpeakerServis.Create(newSpeaker);
            }
            else {

                //SpeakerServis.Update(speak);
            }

            return RedirectToPage("/Admin/Speaker/speakerCreate");
        }
      
    }
}
