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
        private readonly ICrudService<Speaker> SpeakerService;
        [FromRoute]
        public int? htrn { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public List<Speaker> Speakers { get; private set; }

     
        public speakerlistModel(ICrudService<Speaker> speakerService)
        {
            this.SpeakerService = speakerService;
            this.SpeakerService.Init(ModelTypes.Speaker);


        }
        //public void InitHotels()
        //{
        //    foreach (Speaker speak in Speakers)
        //    {
        //        List<Speaker> speaks = SpeakerService.GetAll();
             
        //    }
        //}

        public IActionResult OnPost()
        {
            return Page();
        }
        public void OnGet()
        {
            if (String.IsNullOrEmpty(FilterCriteria))
            {
                Speakers = SpeakerService.GetAll();
                 //speaker = SpeakerServis.GetFiltered(FilterCriteria);
            }
            Speakers = SpeakerService.GetAll();
        }
        public IActionResult OnPostDelete(int htnr)
        {
            SpeakerService.Delete(htnr);

            Speakers = SpeakerService.GetAll();
            return Page();
        }
    }
}
