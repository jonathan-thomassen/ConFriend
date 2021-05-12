using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ConFriend.Interfaces
{
    public interface ISQLService<T>
    {
        string QueryBuilder();
        Task<bool> SqlCommand();
        void OpenDB(String queryString);
        void CloseDB();
        T OnRead();

       // private abstract void onRead();
    }
}
