using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ConFriend.Models;
using Microsoft.AspNetCore.Http;

namespace ConFriend.Services
{
    public class SessionService
    {
        public void SetUserId(ISession session, int userId)
        {
            session.SetInt32("Id", userId);
        }

        public int? GetUserId(ISession session)
        {
            return session.GetInt32("Id");
        }
    }
}
