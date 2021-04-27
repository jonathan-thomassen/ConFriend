using System.Collections.Generic;
using SymFiend.Interfaces;

namespace SymFiend.Services
{
    public class SQLService<T> : ISQLService<T>
    {
        internal List<T> Items;

        public string QueryBuilder()
        {
            throw new System.NotImplementedException();
        }

        public bool SqlCommand()
        {
            throw new System.NotImplementedException();
        }

        public void OpenDB()
        {
            throw new System.NotImplementedException();
        }

        public void CloseDB()
        {
            throw new System.NotImplementedException();
        }

        public void OnRead()
        {
            throw new System.NotImplementedException();
        }
    }
}