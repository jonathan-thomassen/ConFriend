using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Admin.EventTest
{
    public class ImageModel : PageModel
    {
        private readonly ICrudService<Event> _eventService;

        [BindProperty]
        public IFormFile Upload { get; set; }

        public Event Event;

        public ImageModel(ICrudService<Event> eventService)
        {
            _eventService = eventService;
            _eventService.Init(ModelTypes.Event);
        }

        public async Task OnGetAsync(int? id)
        {
            if (id != null)
            {
                Event = await _eventService.GetFromId((int)id);
            }
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id != null)
            {
                Event = await _eventService.GetFromId((int)id);
                var file = Path.Combine("wwwroot\\", "events", Upload.FileName);
                await using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }

                Event.Image = Upload.FileName;
                await _eventService.Update(Event);
            }
            return Page();
        }
    }
}
