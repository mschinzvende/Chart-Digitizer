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
    public partial class User_manager : Form
    {
        public User_manager()
        {
            InitializeComponent();
        }
           

        private void User_manager_Load(object sender, EventArgs e)
        {
            ListViewItem users = new ListViewItem();
            if (users.SubItems.Count >= 1)
            {
                button1.Enabled = true;
            }
            Getallusers();
            Getadmins();
        }

        public void Getallusers()
        {
            try
            {
                
                string sql = "SELECT * FROM gnrlusers";
                con_db getusers = new con_db();
                getusers.OpenConnection();
                MySqlDataReader read = getusers.Readinfo(sql);
                while (read.Read())
                {
                    ListViewItem row = new ListViewItem();
                    row.SubItems.Add("" + read["usrname"]);
                    row.SubItems.Add("" + read["usrpswrd"]);
                    row.SubItems.Add("" + read["dob"]);
                    row.SubItems.Add("" + read["gender"]);
                    row.SubItems.Add("" + read["email"]);
                    row.SubItems.Add("" + read["phone"]);
                    row.SubItems.Add("G/User");
                    listView1.Items.Add(row);
                }
                
                getusers.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Getadmins()
        {
            
            string sql = "SELECT * FROM admns";
            con_db getadmns = new con_db();
            getadmns.OpenConnection();
            MySqlDataReader read = getadmns.Readinfo(sql);
            while (read.Read())
            {
                ListViewItem row = new ListViewItem();
                row.SubItems.Add("" + read["usrname"]);
                row.SubItems.Add("" + read["usrpswrd"]);
                row.SubItems.Add("" + read["dob"]);
                row.SubItems.Add("" + read["gender"]);
                row.SubItems.Add("" + read["email"]);
                row.SubItems.Add("" + read["phone"]);
                row.SubItems.Add("Admin");
                listView1.Items.Add(row);

                //listBox2.Items.Add("" + read["usrname"]);// + "     |" + read["email"]);
                //dataGridView1.Rows.Add(read["usrname"]);
                //listBox1.Items.Add(read["usrname"].ToString());
            }
            getadmns.CloseConnection();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            adduser newform = new adduser();

            newform.ShowDialog();
            Show();
            Refreshall();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            textBox1.Visible = true;
            textBox1.Focus();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con_db deleteone = new con_db();
                string sql = "";
                ListViewItem record = listView1.SelectedItems[0];

                if (record.SubItems[7].Text == "Root user")
                {
                    MessageBox.Show("You cannot remove this user from the system");
                }
                else if (record.SubItems[7].Text == "Admin")
                {
                    if (DialogResult.Yes == MessageBox.Show("Are you sure u want to delete this user?", "Confirm deletion", MessageBoxButtons.YesNo))
                    {
                        sql = "DELETE FROM admns WHERE usrname='" + record.SubItems[1].Text + "'";
                        if (deleteone.Delete(sql))
                        {
                            MessageBox.Show("user deleted success!!");
                            Refreshall();
                        }
                    }
                }
                else if (record.SubItems[7].Text == "G/User")
                {
                    if (DialogResult.Yes == MessageBox.Show("Are you sure u want to delete this user?", "Confirm deletion", MessageBoxButtons.YesNo))
                    {
                        sql = "DELETE FROM gnrlusers WHERE usrname='" + record.SubItems[1].Text + "'";
                        if (deleteone.Delete(sql))
                        {
                            MessageBox.Show("user deleted success!!");
                            Refreshall();

                        }
                    }
                }


                else
                {
                    MessageBox.Show("User not found");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Cant perform operation. Make sure you have selected a specific user!","Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

            //MessageBox.Show("" + record.SubItems[1].Text + ", " + record.SubItems[7].Text);
/*
            if (listView1.SelectedIndices > -1)
            {
                 sql = "DELETE FROM gnrlusers WHERE usrname='" + listBox1.SelectedItem.ToString() + "'";
            }
            else if (listBox2.SelectedIndex > -1)
            {
                sql = "DELETE FROM admns WHERE usrname='" + listBox2.SelectedItem.ToString() + "'";
            }
        */
            

        public void Refreshall()
        {
            listView1.Items.Clear();
            Getadmins();
            Getallusers();
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
                  
        }

     

        private void listBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            
           // listBox2.ClearSelected();
        }

        private void listBox2_Enter(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            
           // listBox1.ClearSelected();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem record = listView1.SelectedItems[0];
                if (record.SubItems[7].Text == "Admin")
                {
                    globals.tablescope = "admns";
                    globals.targetuser = record.SubItems[1].Text;
                }

                else if (record.SubItems[7].Text == "G/User")
                {
                    globals.tablescope = "gnrlusers";
                    globals.targetuser = record.SubItems[1].Text;
                }

                Hide();
                useredit newform = new useredit();
                newform.ShowDialog();
                User_manager reload = new User_manager();
                reload.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Please make sure you have selected a user to edit!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
             
                string sql = "SELECT * FROM gnrlusers WHERE usrname='" + textBox1.Text + "' ";
                // MessageBox .Show(""+ sql ); //testing sql
                con_db populateforsearch = new con_db();
                populateforsearch.OpenConnection();
                MySqlDataReader read = populateforsearch.Readinfo(sql);
               
                while (read.Read())
                {
                   
                    ListViewItem row = new ListViewItem();
                    row.SubItems.Add("" + read["usrname"]);
                    row.SubItems.Add("" + read["usrpswrd"]);
                    row.SubItems.Add("" + read["dob"]);
                    row.SubItems.Add("" + read["gender"]);
                    row.SubItems.Add("" + read["email"]);
                    row.SubItems.Add("" + read["phone"]);
                    row.SubItems.Add("G/User");
                    listView1.Items.Add(row);
                    
                }
                //populateforsearch.CloseConnection();
                read.Close();
               /* con_db populateforsearch2 = new con_db();*/
                string sql2 = "SELECT * FROM admns WHERE usrname='" + textBox1.Text + "' ";
                MySqlDataReader read2 = populateforsearch.Readinfo(sql2);
                //populateforsearch.OpenConnection();

                while (read2.Read())
                {
                    
                    ListViewItem row = new ListViewItem();
                    row.SubItems.Add("" + read2["usrname"]);
                    row.SubItems.Add("" + read2["usrpswrd"]);
                    row.SubItems.Add("" + read2["dob"]);
                    row.SubItems.Add("" + read2["gender"]);
                    row.SubItems.Add("" + read2["email"]);
                    row.SubItems.Add("" + read2["phone"]);
                    row.SubItems.Add("Admin");
                    listView1.Items.Add(row);

                }
                //populateforsearch2.CloseConnection();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button4.Enabled = true;
            button3.Enabled = true;
        }

        
    }
}
