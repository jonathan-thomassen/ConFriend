using System;
using System.Collections.Generic;
using ConFriend.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ConFriend.Services
{
    public enum SQLType { 
        GetAll,
        GetAllWhere,
        GetSingle,
        Update,
        Delete,
        Create,
        Custom
    }
    public abstract class SQLService<T> : Connection
    {
        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataReader _reader;
        private string _name;
        internal int RowsAltered;
        internal List<T> Items;
        internal object ObjItem;
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

        public string QueryBuilder(SQLType command,string condition)
        {

            switch (command)
            {
                case SQLType.GetAll:
                    return $"SELECT * FROM {_name} WHERE {condition}";
                case SQLType.GetAllWhere:
                    return $"SELECT * FROM {_name} WHERE {condition}";
                case SQLType.Custom:
                    return $"SELECT * FROM {_name} WHERE {condition}";
                case SQLType.GetSingle:
                    return $"SELECT * FROM {_name} WHERE {condition}";
                case SQLType.Create:
                    return $"SELECT * FROM {_name} WHERE {condition}";
                case SQLType.Update:
                    return $"SELECT * FROM {_name} WHERE {condition}";
                case SQLType.Delete:
                    return $"SELECT * FROM {_name} WHERE {condition}";
                default:
                    return "SELECT * FROM " + _name;
            }
        }

        public bool SQLCommand(SQLType command,string condition = "n")
        {
            OpenDB(QueryBuilder(command, condition));
            try
            {
                _command.Connection.Open();

                switch (command)
                {
                    case SQLType.GetAll:
                    case SQLType.GetAllWhere:
                    case SQLType.Custom:
                        _reader = _command.ExecuteReader();
                        onRead();
                        break;
                    case SQLType.GetSingle:
                        ObjItem = _command.ExecuteScalar();
                        break;
                    case SQLType.Create:
                    case SQLType.Update:
                    case SQLType.Delete:
                        RowsAltered = _command.ExecuteNonQuery();
                        break;
                    default:
                        break;
                }
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
        public abstract T OnRead();

        private void onRead()
        {
            Items = new List<T>();
            while (Reader.Read())
            {
                Items.Add(OnRead());
            }
        }
      
    }
}