using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages
{
    public class EnrollmentModel : PageModel
    {
        private readonly ICrudService<Enrollment> EnrollmentSevice;
        private readonly ICrudService<User> UserSevice;
        private readonly ICrudService<Event> EventSevice;

        [FromRoute]
        public int? Id { get; set; }

        public bool IsNewEntry
        {
            get { return Id == null; }
        }

        [BindProperty(SupportsGet = true)]
        public List<Enrollment> Enrollments { get; private set; }

        [BindProperty]
        public Enrollment enrollment { get; set; }

        public EnrollmentModel(ICrudService<Enrollment> enroll, ICrudService<User> userSevice, ICrudService<Event> eventSevice)
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
            Enrollments = EnrollmentSevice.GetAll();
        }
        public List<Enrollment> GetThemes()
        {
            Enrollments = EnrollmentSevice.GetAll();
            return Enrollments;
        }

        public IActionResult OnPostDelete(int id)
        {
            EnrollmentSevice.Delete(id);

            Enrollments = EnrollmentSevice.GetAll();
            return Page();
        }
        public IActionResult OnPostSave(int id)
        {
            Enrollment newEnrollment = new Enrollment();
            //need to be set on server side, so users cant cheet with the time
            newEnrollment.SignUpTime = DateTime.Now;
            newEnrollment.EnrollmentId = id;

            EnrollmentSevice.Update(newEnrollment);
            Enrollments = EnrollmentSevice.GetAll();
            return Page();
        }
        public IActionResult OnPostSaveNew(int id)
        {
            Enrollment newEnrollment = new Enrollment();
      
            EnrollmentSevice.Create(newEnrollment);

            Enrollments = EnrollmentSevice.GetAll();
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