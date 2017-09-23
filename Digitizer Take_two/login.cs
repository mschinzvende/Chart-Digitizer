using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Digitizer_Take_two
{
    public partial class login : Form
    { 
       

        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                string username = textBox1.Text;
                string password = textBox2.Text;

                string sqlgnrluser = "SELECT * FROM gnrlusers WHERE usrname='" + username + "' AND usrpswrd='" + password + "'";
                string sqladmin = "SELECT * FROM admns WHERE usrname='" + username + "'AND usrpswrd='" + password + "'";

                con_db loginuora = new con_db();

                if (loginuora.SelectVerifyStation(sqlgnrluser))
                {
                    // MessageBox.Show("user success"); //testing access
                    globals.usernamme = username;
                    Hide();

                    userwelcome newform = new userwelcome();
                    newform.ShowDialog();
                    Show();
                    textBox1.Clear();
                    textBox2.Clear();


                }
                else if (loginuora.SelectVerifyStation(sqladmin))
                {
                    // MessageBox.Show("admin access"); //testing access
                    globals.usernamme = username;
                    Hide();

                    administrator newform = new administrator();
                    newform.ShowDialog();
                    Show();
                    textBox1.Clear();
                    textBox2.Clear();
                }
                else
                {
                    labelerror.Text = "Invalid login parameters!!!";
                    labelerror.Visible = true;
                    textBox1.Focus();
                    textBox1.SelectAll();
                    textBox2.Clear();

                }
            }
    
        

        private void login_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            con_db atload = new con_db();
            

            if (atload.OpenConnection())
            {
                label3.ForeColor = Color.Blue;
                string codetail = atload.Getserverdetail();
                label3.Text = "Connected to server at " + codetail;
                atload.CloseConnection();
            }

            else
            {
                label3.ForeColor = Color.Red;
                label3.Text = "Connection to server Failed";
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           labelerror.Visible = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //labelerror.Visible = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Return)
            {
                button1.PerformClick();


                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

       
    }
    
}
