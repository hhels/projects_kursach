using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fitnes
{
    static class llogin //значение логина
    {
        public static string Value { get; set; }
    }

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

        public static DataTable ExecuteQuery(string query)
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
    }
}
