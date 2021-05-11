using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConFriend.Pages
{
    public class CreatEnrollmentModel : PageModel
    {
        private readonly ICrudService<Enrollment> EnrollmentSevice;
        private readonly ICrudService<User> UserSevice;
        private readonly ICrudService<Event> EventSevice;


        // private int UId = { get; set; }

        //private int EId = { get; set; }

        [FromRoute]
        public int? Id { get; set; }

        public bool IsNewEntry
        {
            get { return Id == null; }
        }
        [BindProperty]
        public Enrollment enrollment { get; set; }
        public List<User> MyUsers { get; set; }
        public List<Event> MyEvents { get; set; }

        public int ChosenUser { get; set; }
        public int ChosenEvent { get; set; }

        public SelectList SelectEventList;
        public SelectList SelectUserList;

        public CreatEnrollmentModel(ICrudService<Enrollment> enroll, ICrudService<User> userSevice, ICrudService<Event> eventSevice)
        {
            EnrollmentSevice = enroll;
            UserSevice = userSevice;
            EventSevice = eventSevice;

            this.EnrollmentSevice.Init(ModelTypes.Enrollment);
            this.UserSevice.Init(ModelTypes.User);
            this.EventSevice.Init(ModelTypes.Event);
        }
        public void OnGet()
        {
           
            MyUsers = UserSevice.GetAll();
            MyEvents = EventSevice.GetAll();

            //Venues.Insert(0, new Venue());
            // Speakers.Insert(0, new Speaker());

            SelectEventList = new SelectList(MyEvents, nameof(Event.EventId), nameof(Event.Name));
            SelectUserList = new SelectList(MyUsers, nameof(Models.User.UserId), nameof(Models.User.FullName));
          
        }
        public IActionResult OnPost(int venueId, int roomId)
        {
            ChosenUser = venueId;
            ChosenEvent = roomId;
            return Page();
        }
        public IActionResult OnPostSaveNew(int? EId, int? UId)
        {
           // enrollment.User = UserSevice.GetFromId(enrollment.userId);
            //enrollment.Event = EventSevice.GetFromId(enrollment.eventId);

            EnrollmentSevice.Create(enrollment);

            return RedirectToPage("Enrollment");

        }
    }
}
