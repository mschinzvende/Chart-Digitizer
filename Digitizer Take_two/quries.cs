using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
namespace Digitizer_Take_two
{
    partial class con_db
    {
        public bool  Insert(string statement)
        {
            try
            {
                string query = statement;

                //open connection
                if (this.OpenConnection() == true)
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    cmd.ExecuteNonQuery();


                    //close connection
                    this.CloseConnection();

                    return true;

                }
                else return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
                return false;
            }
        }

        public bool   SelectVerifyStation(string statement)
        {
            try
            {

                string query = statement;

                this.OpenConnection();

                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                if (dataReader.HasRows == true)
                {
                    this.CloseConnection();
                    return true;
                }
                else
                    this.CloseConnection();
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
                return false;
            }
            
           }

        public bool Selectforusers(string query)
        {
            try
            {

                this.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader datareader = cmd.ExecuteReader();

                if (datareader.HasRows == true)
                {

                    this.CloseConnection();

                    return true;
                }
                else
                    this.CloseConnection();
                return false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
                return false;
            }
               
            
        }
        
        public bool Update(string query)
        {

            try
            {
                // string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

                //Open connection
                if (this.OpenConnection() == true)
                {
                    //create mysql command
                    MySqlCommand cmd = new MySqlCommand();
                    //Assign the query using CommandText
                    cmd.CommandText = query;
                    //Assign the connection using Connection
                    cmd.Connection = connection;

                    //Execute query
                    cmd.ExecuteNonQuery();

                    //close connection
                    this.CloseConnection();
                    return true;
                }

                else return false;

            }
            catch (Exception ex) {
                MessageBox.Show("" + ex.Message);
                return false;
            }
            
        }


   
        public bool  Delete(string query)
        {

            try
            {

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return true;
                }

                else return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
                return false;
            }
                          
        }

        public DataTable Getdbaseinfo(string sql)
        {
            try
            {
                string sqlog = sql;
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataAdapter mydata = new MySqlDataAdapter();
                mydata.SelectCommand = cmd;
                DataTable ds = new DataTable();
                mydata.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
                return null;
            }
           
        }

        public MySqlDataReader Readinfo(string sql)
        {
            try
            {
                string sqlog = sql;
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
                return null;
            }
            
        }
        
    }
       
}


