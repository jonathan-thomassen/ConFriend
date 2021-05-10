using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Admin.EventTest
{
    public class IndexModel : PageModel
    {
        public List<Event> Events { get; private set; }

        private ICrudService<Event> eventService;

        public IndexModel(ICrudService<Event> eService)
        {
            eventService = eService;
            eventService.Init(ModelTypes.Event);
            Events = eventService.GetAll();
        }
        public void OnGet()
        {
            
        }
    }
}
