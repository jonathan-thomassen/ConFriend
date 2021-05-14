using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Events
{
    public class EventModel : PageModel
    {
        private readonly ICrudService<Event> _eventService;
        private readonly ICrudService<Room> _roomService;
        private readonly ICrudService<Speaker> _speakerService;

        public Event Event;
        public List<Room> Rooms;
        public List<Speaker> Speakers;

        public EventModel(ICrudService<Event> eventService, ICrudService<Room> roomService, ICrudService<Speaker> speakerService)
        {
            _eventService = eventService;
            _roomService = roomService;
            _speakerService = speakerService;

            _eventService.Init(ModelTypes.Event);
            _roomService.Init(ModelTypes.Room);
            _speakerService.Init(ModelTypes.Speaker);
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return Page();

            Event = await _eventService.GetFromId((int) id);
            Rooms = await _roomService.GetAll();
            Speakers = await _speakerService.GetAll();

            return Page();
        }

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    return Page();
        //}
    }
}
