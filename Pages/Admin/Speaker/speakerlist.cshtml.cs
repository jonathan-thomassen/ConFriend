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
    public class speakerlistModel : PageModel
    {
        private readonly ICrudService<Speaker> SpeakerServis;
        [FromRoute]
        public int? htrn { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public List<Speaker> Speakers { get; private set; }

     
        public speakerlistModel(ICrudService<Speaker> speakerService)
        {
            this.SpeakerServis = speakerService;
            this.SpeakerServis.Init(ModelTypes.Speaker);


        }
        public void InitHotels()
        {
            foreach (Speaker speak in Speakers)
            {
                List<Speaker> speaks = SpeakerServis.GetAll();
             
            }
        }

        public IActionResult OnPost()
        {
            return Page();
        }
        public void OnGet()
        {
            if (String.IsNullOrEmpty(FilterCriteria))
            {
                Speakers = SpeakerServis.GetAll();
                 //speaker = SpeakerServis.GetFiltered(FilterCriteria);
            }
            Speakers = SpeakerServis.GetAll();
        }
        public IActionResult OnPostDelete(int htnr)
        {
            SpeakerServis.Delete(htnr);

            Speakers = SpeakerServis.GetAll();
            return Page();
        }
    }
}