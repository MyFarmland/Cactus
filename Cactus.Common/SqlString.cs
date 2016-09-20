using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Npgsql;
#if Linux 
using Mono.Data.Sqlite;
#else
using System.Data.SQLite;
#endif

namespace Cactus.Common
{
    public class SqlString
    {

        public static string MSSQLString = ConfigurationManager.ConnectionStrings["MSSQLString"].ConnectionString;
        public static string MySQLString = ConfigurationManager.ConnectionStrings["MySQLString"].ConnectionString;
        public static string PGSQLString = ConfigurationManager.ConnectionStrings["PGSQLString"].ConnectionString;
        public static string SQLiteString = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.ConnectionStrings["SQLiteString"].ConnectionString;

        public static IDbConnection GetSqlConnection(string sqlConnectionString = "")
        {
            if (string.IsNullOrEmpty(sqlConnectionString)) {
                sqlConnectionString = MSSQLString;
            }
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            conn.Open();
            return conn;
        }
        public static IDbConnection GetMySqlConnection(string sqlConnectionString = "")
        {
            if (string.IsNullOrEmpty(sqlConnectionString))
            {
                sqlConnectionString = MySQLString;
            }
            MySqlConnection conn = new MySqlConnection(sqlConnectionString);
            conn.Open();
            return conn;
        }
        public static IDbConnection GetPGSqlConnection(string sqlConnectionString = "")
        {
            if (string.IsNullOrEmpty(sqlConnectionString))
            {
                sqlConnectionString = PGSQLString;
            }
            NpgsqlConnection conn = new NpgsqlConnection(sqlConnectionString);
            conn.Open();
            return conn;
        }

        public static  IDbConnection GetSQLiteConnection(string sqlConnectionString = ""){
            if (string.IsNullOrEmpty(sqlConnectionString))
            {
                sqlConnectionString = SQLiteString;
            }
#if Linux
            SqliteConnection conn = new SqliteConnection(sqlConnectionString);
#else
            SQLiteConnection conn = new SQLiteConnection(sqlConnectionString);
#endif

            conn.Open();
            return conn;
        }
    }
}
