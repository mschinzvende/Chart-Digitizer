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
    public partial class Station_Management : Form
    {
        public Station_Management()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            textBox1.Visible = true;
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                string station_name = textBox2.Text;
                string station_code = textBox3.Text;
                string thequery = "INSERT INTO stations (st_name, st_code) VALUES ('" + station_name + "','" + station_code + "')";
                con_db adduser = new con_db();

                if (adduser.Insert(thequery))
                {
                    MessageBox.Show("Station added successfully");
                    globals.Clearallboxes(this);
                    listView1.Items.Clear();
                    Getallstations();
                }
                else
                {
                    MessageBox.Show("Station add failed!!");
                }

            }
            catch
            {
                MessageBox.Show("An Error occured please contact the system administrator");

            }
        }

        private void Station_Management_Load(object sender, EventArgs e)
        {
            Getallstations();
        }

        public void Getallstations()
        {
            try
            {

                string sql = "SELECT * FROM stations";
                con_db getstations = new con_db();
                getstations.OpenConnection();
                MySqlDataReader read = getstations.Readinfo(sql);
                while (read.Read())
                {
                    ListViewItem row = new ListViewItem();
                    row.SubItems.Add("" + read["st_name"]);
                    row.SubItems.Add("" + read["st_code"]);
                    listView1.Items.Add(row);
                }

                getstations.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void Refreshall()
        {
            listView1.Items.Clear();
            Getallstations();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con_db deleteone = new con_db();
                string sql = "";
                ListViewItem record = listView1.SelectedItems[0];
                sql = "DELETE FROM stations WHERE st_code='" + record.SubItems[2].Text + "'";
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete this station?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {

                    if (deleteone.Delete(sql))
                    {
                        MessageBox.Show("user deleted success!!");
                        
                    }
                }

                else
                {
                    MessageBox.Show("User not found");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Cant perform operation. Make sure you have selected a specific user!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            try
            {
                ListViewItem record = listView1.SelectedItems[0];
                string sql = "SELECT * FROM stations WHERE st_code =" + record.SubItems[2].Text;
                con_db getstations = new con_db();
                getstations.OpenConnection();
                MySqlDataReader read = getstations.Readinfo(sql);
                while (read.Read())
                {

                    textBox2.Text = read["st_name"].ToString();
                    textBox3.Text = read["st_code"].ToString();

                }
                button5.Visible = true;
                getstations.CloseConnection();
            }
            catch (Exception)
            {
                MessageBox.Show("Please make a selecton first");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
           con_db updatestation = new con_db();
           string thequery = "UPDATE  `stations` SET  `st_name` =  '" + textBox2.Text + "',`st_code` =  '" + textBox3.Text  + "'";
           if (updatestation.Update(thequery))
           {
               MessageBox.Show("Station Update Successfull");
               
           }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM stations WHERE st_code='" + textBox1.Text + "' ";
            // MessageBox .Show(""+ sql ); //testing sql
            con_db populateforsearch = new con_db();
            populateforsearch.OpenConnection();
            MySqlDataReader read = populateforsearch.Readinfo(sql);

            while (read.Read())
            {

                ListViewItem row = new ListViewItem();
                row.SubItems.Add("" + read["st_name"]);
                row.SubItems.Add("" + read["st_code"]);
                listView1.Items.Add(row);

            }
            //populateforsearch.CloseConnection();
            read.Close();
        }
    }
}
