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

        
    public partial class Station_Check_In : Form
    {
   
        public Station_Check_In()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con_db sttncheck = new con_db();
                int stcode = int.Parse(textStationCode.Text);
                string sql = "SELECT * FROM stations WHERE st_code=" + stcode;
                if (sttncheck.SelectVerifyStation(sql))
                {

                    //MessageBox.Show("The station exists in the database"+stcode);
                    globals.stationcode = stcode;
                    Hide();

                    StationParameters newform = new StationParameters();
                    newform.ShowDialog();
                    Show();
                    textStationCode.Clear();
                    textStationCode.Focus();
                }
                else
                {

                    labelnotexist.Text = "Station does not exist !!!";
                    labelnotexist.Visible = true;
                    textStationCode.Focus();
                    textStationCode.SelectAll();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Enter Code in the correct format");
            }

            }
    
        

        private void Station_Check_In_Load(object sender, EventArgs e)
        {
            textStationCode.Focus();
        }

        private void textStationCode_TextChanged(object sender, EventArgs e)
        {
            labelnotexist.Visible = false;
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