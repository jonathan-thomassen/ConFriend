using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConFriend.Models
{
    public interface ICompositModel
    {
        int Identity_Comp();

        ModelTypes DataType_Comp { get; }
    }
}
