using System;

namespace ConFriend.Models
{
    public class Enrollment : IModel
    {
        public int EnrollmentId { get; set; }
        public DateTime SignUpTime { get; set; }
        public User User { get; set; }
        public Event Event { get; set; }
        public string ToSQL()
        {
            return $"EventId = {Event.EventId}, UserId = {User.UserId}, SignUpTime = {SignUpTime}";
        }

        public string Identity()
        {
            return $"EnrollmentId = {EnrollmentId}";
        }
        public static string IdentitySQL = "EnrollmentId =";
    }
}
