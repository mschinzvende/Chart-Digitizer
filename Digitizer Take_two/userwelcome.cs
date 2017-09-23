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
    public partial class userwelcome : Form 
    {
        public userwelcome()
        {
            InitializeComponent();
        }

        private void userwelcome_Load(object sender, EventArgs e)
        {
            labelwelcome.Text = "Welcome " + globals.usernamme + ", Select an option to proceed";
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dispose ();
            Station_Check_In newfrom = new Station_Check_In();
            newfrom.ShowDialog();
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
            
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Return)
            {
                button4.PerformClick();


                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            globals.Timing(this.labeltime);
        }

        private void viewProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myprofile newform = new myprofile();
            Hide();
            newform.ShowDialog();
            Show();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            changepass newform = new changepass();
            newform.ShowDialog();
            Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

    }
}
