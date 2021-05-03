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
        [FromRoute]
        public long? htrn { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public List<Speaker> Speakers { get; private set; }

        private ICrudService<Speaker> speakerService;
        public speakerlistModel(ICrudService<Speaker> speakerService)
        {
            this.speakerService = speakerService;
      
        }
        public void InitHotels()
        {
            foreach (Speaker speak in Speakers)
            {
                List<Speaker> speaks = speakerService.GetAll();
             
            }
        }


        public void OnGet()
        {
            if (String.IsNullOrEmpty(FilterCriteria))
            {
                // speaker = speakerService.GetAll();
                // speaker = speakerService.GetFiltered(FilterCriteria);
            }


        }
    }
}
