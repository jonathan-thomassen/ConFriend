using System;
using System.Threading.Tasks;

namespace ConFriend.Interfaces
{
    public interface ISQLService<T>
    {
        string QueryBuilder();
        Task<bool> SqlCommand();
        void OpenDB(string queryString);
        void CloseDB();
        T OnRead();
    }
}