using System;
using Microsoft.Data.SqlClient;

namespace ConFriend.Interfaces
{
    public interface ISQLService<T>
    {
        string QueryBuilder();
        bool SqlCommand();
        void OpenDB(String queryString);
        void CloseDB();
        T OnRead();

       // private abstract void onRead();
    }
}
