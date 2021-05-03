using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConFriend.Models
{
    public interface IModel
    {
        string ToSQL();


        string Identity();

        //please also implement:
        //static string IdentitySQL
    }
}
