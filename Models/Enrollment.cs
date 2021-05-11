using System;

namespace ConFriend.Models
{
    public class Enrollment : IModel
    {
        public int EnrollmentId { get; set; }
        public DateTime SignUpTime { get; set; }
        public User User { get; set; }
        public Event Event { get; set; }
        public int userId { get; set; }
        public int eventId { get; set; }
        
        
        public string ToSQL()
        {
            return $"EventId = {eventId}, UserId = {userId}, SignUpTime = '{SignUpTime}'";
        }

        public string Identity()
        {
            return $"EnrollmentId = {EnrollmentId}";
        }
    
     
    }
}
