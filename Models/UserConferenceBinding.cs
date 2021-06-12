using ConFriend.Interfaces;

namespace ConFriend.Models
{
    public class UserConferenceBinding : IModel
    {
        public int UserConferenceBindingId;
        public int UserId;
        public int ConferenceId;
        public UserType UserType;

        public string ToSQL()
        {
            return $"UserId = {UserId}, ConferenceId = {ConferenceId}, UserType = {(int)UserType}";
        }

        public string Identity()
        {
            return $"UserConferenceBindingId = {UserConferenceBindingId}";
        }
    }
}
