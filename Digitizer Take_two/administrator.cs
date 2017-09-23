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
    public partial class administrator : Form
    {
        public administrator()
        {
            InitializeComponent();
        }

        private void administrator_Load(object sender, EventArgs e)
        {
            label6.Text = "WELCOME " + globals.usernamme ;
            
        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Hide();
            Station_Management newform = new Station_Management();
            newform.ShowDialog();
            Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
            User_manager newform = new User_manager();
            newform.ShowDialog();
            Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            globals.Timing(this.label7);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Hide();
            chartmngmnt newfrm = new chartmngmnt();
            newfrm.ShowDialog();

            Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Hide();
            adminProfile newform = new adminProfile();
            newform.ShowDialog();
            Show();
        }

    }
}