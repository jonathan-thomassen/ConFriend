using System;
using System.Globalization;

namespace ConFriend.Models
{
    public class Enrollment : IModel
    {
        public int EnrollmentId { get; set; }
        public DateTime? SignUpTime { get; set; }
        public int? UserId { get; set; }
        public int? EventId { get; set; }

        public string ToSQL()
        {
            CultureInfo culture = new CultureInfo("en-US");
            return $"EventId = {EventId}, UserId = {UserId}, SignUpTime = '{SignUpTime?.ToString(culture)}'";
        }

        public string Identity()
        {
            return $"EnrollmentId = {EnrollmentId}";
        }
    
     
    }
}
