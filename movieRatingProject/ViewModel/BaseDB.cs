using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;



namespace ViewModel
{
    public class BaseDB
    {
        private string connectionString;
        protected SqlConnection _connection;
        protected SqlCommand _command;
        protected SqlDataReader _reader;
        protected string _tableName;

        public BaseDB(string tableName)
        {
            _tableName = tableName;
            connectionString = @"Data Source=LAPTOP-8DA9GF4V\SQLEXPRESS;Initial Catalog=MovieDB;Integrated Security=True";
            _connection = new SqlConnection(connectionString);
            _command = new SqlCommand();
            _command.Connection = _connection;
        }
    }
}
