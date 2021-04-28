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
        void OnRead();
    }
}
