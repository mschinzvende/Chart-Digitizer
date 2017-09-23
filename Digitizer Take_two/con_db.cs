using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
namespace Digitizer_Take_two
{
  partial  class con_db
    {
      
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

       public con_db() 
        {
            MakeConnection();
        }

        public void MakeConnection()
        {

            try
            {
                server = "127.0.0.1";
                database = "digitizer";
                uid = "root";
                password = "";

                string connectionstring;
                connectionstring = "SERVER=" + server + ";" + "DATABASE=" +
              database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + "";

                connection = new MySqlConnection(connectionstring);
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
            }
            
        }
        //open connection to database
        public  bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
           
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        internal  bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public string Getserverdetail()
        {
            return this.server;
        }
    }

    

}

