using System;
using System.Collections.Generic;
using ConFriend.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ConFriend.Services
{
    public abstract class SQLService<T> : Connection, ISQLService<T>
    {
        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataReader _reader;
        private string _name;
        internal List<T> Items;
        public SqlDataReader Reader
        {
            get { return _reader;}
            private set { _reader = value; }
        }   

        public SQLService(IConfiguration configuration, string name) : base(configuration)
        {
            _name = name;
        }

        public SQLService(string connectionString, string name) : base(connectionString)
        {
            _name = name;
        }

        public string QueryBuilder()
        {
            return "SELECT * FROM User";
        }

        public bool SqlCommand()
        {
            OpenDB(QueryBuilder());
            try
            {
                _command.Connection.Open();
                _reader = _command.ExecuteReader();
                onRead();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            CloseDB();
            return true;
        }

        public void OpenDB(string queryString)
        {
            _connection = new SqlConnection(connectionString);
            _command = new SqlCommand(queryString, _connection);
        }

        public void CloseDB()
        {
            _connection.Dispose();
            _command.Dispose(); 
        }

        public abstract void OnRead();

        private void onRead()
        {
            while (Reader.Read())
            {
                OnRead();
            }
        }
    }
}