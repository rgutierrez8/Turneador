using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;



namespace Turneador.Data
{
    public class MasterConection
    {
        MySqlConnectionStringBuilder Builder = new MySqlConnectionStringBuilder();

        public bool Connection()
        {
            Builder.Port = 3306;
            Builder.Server = "sql10.freemysqlhosting.net";
            Builder.Database = "sql10453129";
            Builder.UserID = "sql10453129";
            Builder.Password = "hiqpBFcRrn";

            try
            {
                MySqlConnection conn = new MySqlConnection(Builder.ToString());
                conn.Open();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
