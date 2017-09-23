using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Digitizer_Take_two
{
    public partial class myprofile : Form
    {
        public myprofile()
        {
            InitializeComponent();
        }

        private void myprofile_Load(object sender, EventArgs e)
        {
            getmyprofile();
        }

        private void getmyprofile()
        {
            string sql = "SELECT * FROM gnrlusers WHERE usrname='" +globals.usernamme  + "' ";
               // MessageBox .Show(""+ sql ); //testing sql

                con_db populateprof = new con_db();
                populateprof.OpenConnection();
                MySqlDataReader read = populateprof.Readinfo(sql);
                while (read.Read())
                {
                    label1.Text = ""+read["usrname"].ToString ();
                    label6.Text ="" +read["phone"].ToString();
                    label3.Text = ""+read["dob"].ToString();
                    label4.Text =""+ read["gender"].ToString();
                    label5.Text ="" +read["email"].ToString();
                    
                }
                populateprof.CloseConnection();

        }
    }
}
