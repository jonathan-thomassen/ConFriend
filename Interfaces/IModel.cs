using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConFriend.Models
{
    public interface IModel
    {
        string ToSQL();

        int Identity();

        ModelTypes DataType { get; }

        //please also implement:
        //  string IdentitySQL { get; }
    }
}
