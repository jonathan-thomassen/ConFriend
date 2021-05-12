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
    public class EnrollmentModel : PageModel
    {
        private readonly ICrudService<Enrollment> EnrollmentSevice;
        private readonly ICrudService<User> UserSevice;
        private readonly ICrudService<Event> EventSevice;

        [BindProperty(SupportsGet = true)]
        public List<Enrollment> Enrollments { get; private set; }

        public List<User> MyUsers;
        public List<Event> MyEvents;

  

        public EnrollmentModel(ICrudService<Enrollment> enroll, ICrudService<User> userSevice, ICrudService<Event> eventSevice)
        {
            EnrollmentSevice = enroll;
            UserSevice = userSevice;
            EventSevice = eventSevice;

            this.EnrollmentSevice.Init(ModelTypes.Enrollment);
            this.UserSevice.Init(ModelTypes.User);
            this.EventSevice.Init(ModelTypes.Event);
        }
        public async Task OnGetAsync()
        {
            Enrollments = await EnrollmentSevice.GetAll();
        }
        public async Task<List<Enrollment>> GetThemesAsync()
        {
            Enrollments = await EnrollmentSevice.GetAll();
            return Enrollments;
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            EnrollmentSevice.Delete(id).Wait();

            Enrollments = await EnrollmentSevice.GetAll();
            return Page();
        }
        public IActionResult OnPostEdit(int id)
        {
            
            //need to be set on server side, so users cant cheet with the time
            //newEnrollment.SignUpTime = DateTime.Now;
            //newEnrollment.EnrollmentId = id;

           // EnrollmentSevice.Update(newEnrollment);
           // Enrollments = EnrollmentSevice.GetAll();
            return Page();
        }
      
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return Page();
            //RedirectToPage("/Admin/Speaker/speakerCreate");
        }
    }
}
//Enrollment
//RoomSeatCategory
//SeatCategory
//SetCatTakenTest