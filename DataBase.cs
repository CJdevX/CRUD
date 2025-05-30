using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WC_InventoryApplication
{
    class DataBase
    {
        private string server = "localhost";
        private string database = "wc_db_project";
        private string username = "root";
        private string password = "12345678";
        private string conString;

        private MySqlConnection conn;
        private MySqlCommand cmd;
        private MySqlDataReader reader;

        public DataBase()
        {
            conString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + username + ";" + "PASSWORD=" + password ;
            conn = new MySqlConnection(conString);
        }

        public MySqlDataReader GETdata(string query)
        {
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            return reader;
        }

        public int SENDdata(string query)
        {
            cmd = new MySqlCommand(query, conn);
            int x = cmd.ExecuteNonQuery();
            return x;
        }
        public void openConnection()
        {
            conn.Open();
        }
        public void closeConnection()
        {
            conn.Close();
        }
    }
}