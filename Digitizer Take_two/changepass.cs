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
    public partial class changepass : Form
    {
        public changepass()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                changepwd();
            }
            catch
            {
                MessageBox.Show("An error occured, contact the system administrator");
            }
        }

        private void changepwd()
        {
            if((textBox1 .Text == textBox2 .Text ) && (textBox1 .Text !=""))
            {
                string sql = "UPDATE gnrlusers SET usrpswrd='"+ textBox1 .Text +" ' WHERE usrname='"+globals.usernamme+"'" ;

                con_db changepwd = new con_db();
                if (changepwd.Update(sql))
                {
                    MessageBox.Show("Password Change Successfull");
                    Dispose();
                }
                
            }

            else
            {
                label3.Visible = true;
                textBox2.Clear();
                textBox1.Focus();
                textBox1.SelectAll();
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label3.Visible = false;
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
    }
}
