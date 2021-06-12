using Microsoft.AspNetCore.Http;

namespace ConFriend.Services
{
    public class ScriptSessionService
    {
        public string GetScriptData(ISession session)
        {
            return session.GetString("jsData");
        }
    }
}
