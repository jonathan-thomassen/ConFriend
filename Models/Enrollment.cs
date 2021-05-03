using System;

namespace ConFriend.Models
{
    public class Enrollment : IModel
    {
        public int EnrollmentId { get; set; }
        public DateTime SignUpTime { get; set; }
        public User User { get; set; }
        public Event Event { get; set; }
    }
}
