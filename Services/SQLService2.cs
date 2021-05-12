using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Markup;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ConFriend.Services
{
    public enum SQLType
    {
        GetAll,
        GetSingle,
        Update,
        Delete,
        Create,
        JoinOn,
        Custom
    }
    public abstract class SQLService2<T> : Connection where T : IModel
    {
        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataReader _reader;
        private ModelMaker MyModelMaker;
        private string _name;
        internal int RowsAltered;
        internal List<T> Items;
        internal T Item;
        internal ModelTypes currentType;

        public SqlDataReader Reader
        {
            get { return _reader;}
            private set { _reader = value; }
        }   

        public SQLService2(IConfiguration configuration) : base(configuration)
        {
               MyModelMaker = new ModelMaker();
           
        }

        public SQLService2(string connectionString) : base(connectionString)
        {
  
            MyModelMaker = new ModelMaker();
        }
        public void init(ModelTypes type)
        {
           _name = type.ToString();
            currentType = type;
        }
    
        private string GetValues(string values)
        {
            string[] output = values.Split("=");
            string str = "";
            for (int i = 1; i < output.Length; i++)
            {
                int v = output[i].IndexOf(',');
                if (i != 1) str += ",";
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
                case SQLType.JoinOn:
                    string[] join = condition.Split('.');
                    return $"SELECT * FROM [{_name}] join [{join[0]}] on {join[0]}.{join[1]} = {_name}.{join[2]}";
                case SQLType.GetAll:
                default:
                    return $"SELECT * FROM [{_name}]";
            }
        }

        public async Task<bool> SQLCommand(SQLType command,string condition = "n", string values = "n")
        {
            string test = QueryBuilder(command, condition, values);
            if (test == "Error") return false;
            OpenDB(test);
            try
            {
                await _command.Connection.OpenAsync();

                switch (command)
                {
                    case SQLType.GetSingle:
                    case SQLType.GetAll:
                    case SQLType.Custom:
                        _reader = await _command.ExecuteReaderAsync();
                        onRead(currentType);
                        if (Items.Count > 0)
                            Item = Items[0];
                        break;
                    case SQLType.Create:
                    case SQLType.Update:
                    case SQLType.Delete:
                        RowsAltered = await _command.ExecuteNonQueryAsync();
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
            Task.WaitAll();
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

        private void onRead(ModelTypes type)
        {
            Items = MyModelMaker.OnRead<T>(type, Reader);
        }
      
    }
}