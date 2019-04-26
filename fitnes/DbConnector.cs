using MySql.Data.MySqlClient;
using System;
using System.Data;

using System.Windows.Forms;

namespace fitnes
{
    public static class DbConnector
    {
        private const string dbServer = "127.0.0.1";
        private const string dbDatabase = "fitnes";
        private const string dbUsersID = "root";
        private const string dbPassword = "";

        private static readonly MySqlConnection Connection;

        static DbConnector()
        {
            var connectionString = new MySqlConnectionStringBuilder
            {
                //AllowZeroDateTime = true, 
                //ConvertZeroDateTime = true,
                Server = dbServer,
                Database = dbDatabase,
                UserID = dbUsersID,
                Password = dbPassword
            };

            var connection = new MySqlConnection(connectionString.ConnectionString);

            try
            {
                connection.Open();
            }
            catch
            {
                MessageBox.Show("нет подключения");
                Environment.Exit(0);
            }

            Connection = connection;
        }

        public static DataTable ExecuteQuery(string query) // SELECT 
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            var dt = new DataTable();
            var command = new MySqlCommand(query, Connection);

            using (var dr = command.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    dt.Load(dr);
                }
            }

            return dt;
        }

        public static int ExecuteUpdate(string query) // INSERT, DELETE, UPDATE
        {
            var command = new MySqlCommand(query, Connection);
            command.ExecuteScalar();
            return (int)command.LastInsertedId;
        }

        public static string ExecuteScalar(string query) // SELECT одно поле
        {
            var command = new MySqlCommand(query, Connection);
            return command.ExecuteScalar().ToString();
        }
    }
}
