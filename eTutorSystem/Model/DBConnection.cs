using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace eTutorSystem.Model
{
    public class DBConnection
    {
        private static readonly string _connectionString = @"Data Source=SQL-SERVER;Initial Catalog=Elite Falcons;Integrated Security=SSPI"; //creates a new public static object of OleDbConnection called connectionstring which holds the paramater database type and its source which is collected from the DBPath variable
        //private static readonly string _connectionString = @"Data Source=JAKE-PC;Initial Catalog=Elite Falcons;Integrated Security=True"; //creates a new public static object of OleDbConnection called connectionstring which holds the paramater database type and its source which is collected from the DBPath variable
        
        private static DBConnection instance = new DBConnection();
        SqlConnection conn;

        public static string ConnectionString
        {
            get { return _connectionString; }
        }

        public SqlConnection Conn
        {
            get { return conn; }
        }

        private DBConnection()
        {
            conn = new SqlConnection();
            conn.ConnectionString = _connectionString;
        }

        public static DBConnection getInstance()
        {
            return instance;
        }
    }
}