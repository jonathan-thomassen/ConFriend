using System;
using System.Collections.Generic;
using System.Windows.Markup;
using ConFriend.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ConFriend.Services
{
    public enum SQLType { 
        GetAll,
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
        internal T Item;

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

        private string GetValues(string values)
        {
            string[] output = values.Split("=");
            string str = "";
            for (int i = 1; i < output.Length; i+=2)
            {
                int v = output[i].IndexOf(',');
                if (v == -1)
                {
                    str += output[i]; 
                }
                else{
                    str += output[i].Substring(0, v);
                }
               
            }
            return str;
        }

        public string QueryBuilder(SQLType command, string condition, string values)
        {
            
            switch (command)
            {
                case SQLType.Custom:
                    return condition;
                case SQLType.GetSingle:
                    if (condition == "n") return "Error";
                    return $"SELECT * FROM [{_name}] WHERE {condition}";
                case SQLType.Create:
                    if (values == "n") return "Error";
                    //extrapolates the values from the SQL Model data  "RowName = value," => value,
                    return $"INSERT INTO [{_name}] VALUES ({GetValues(values)})";
                case SQLType.Update:
                    if (condition == "n") return "Error";
                    if (values == "n") return "Error";
                    return $"UPDATE [{_name}] SET {values} WHERE {condition} ";
                case SQLType.Delete:
                    if (condition == "n") return "Error";
                    return $"DELETE FROM [{_name}] WHERE {condition}";
                case SQLType.GetAll:
                default:
                    return $"SELECT * FROM [{_name}]";
            }
        }

        public bool SQLCommand(SQLType command,string condition = "n", string values = "n")
        {
            string test = QueryBuilder(command, condition, values);
            if (test == "Error") return false;
            OpenDB(test);
            try
            {
                _command.Connection.Open();

                switch (command)
                {
                    case SQLType.GetAll:
                    case SQLType.Custom:
                        _reader = _command.ExecuteReader();
                        onRead();
                        break;
                    case SQLType.GetSingle:
                        _reader = _command.ExecuteReader();
                        onRead();
                        Item = Items[0];
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