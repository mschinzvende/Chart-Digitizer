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

    public partial class StationParameters : Form
    {

        public StationParameters()
        {
            InitializeComponent();

        }
        /* public  void Control_KeyUp(object sender, KeyEventArgs e)
         {
             if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
             {
                 this.SelectNextControl((Control)sender, true, true, true, true);
             }

         }*/
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (get_indices())
                {
                    if (radioApendNo.Checked == true)
                    {
                        string season = comboBox1.SelectedText.ToString();
                        int chartno = int.Parse(textBox1.Text);
                        // int chartduration = int.Parse(textBox4.Text);
                        var dateon = dateTimePicker1.Value.ToString("yyyy/MM/dd");
                        var dateoff = dateTimePicker2.Value.ToString("yyyy/MM/dd");
                        var datedigitzed = System.DateTime.Now;
                        // var range = comboBox2.SelectedText;

                        con_db newentry = new con_db();
                        string sql = "INSERT INTO charts (chart_number,date_digitized,date_on,date_off,season,st_code) VALUES ('" + chartno + "','" + datedigitzed + "','" + dateon + "','" + dateoff + "','" + season + "','" + globals.stationcode + "')";
                        if (newentry.Insert(sql))
                        {
                            globals.chartnumber = chartno;
                            globals.x_date = dateon;
                            Hide();

                            upload newform = new upload();
                            newform.ShowDialog();
                            Show();
                            globals.Clearallboxes(this);

                        }
                        else { MessageBox.Show("data not updated"); }
                    }

                    else if (radioApendYes.Checked == true)
                    {
                        int chartno = int.Parse(textBox3.Text);
                        string sql = "SELECT * FROM charts WHERE chart_number ='" + chartno+ "'AND st_code ='"+globals.stationcode+"'";
                        con_db newentry = new con_db();
                        if (newentry.SelectVerifyStation(sql))
                        {
                            globals.chartnumber = chartno;
                            Hide();
                            upload newform = new upload();
                            newform.ShowDialog();
                            Show();
                        }
                        else
                        {
                            labelcantappend.Text = "Can not find chart to append";
                            labelcantappend.Visible = true;
                            textBox3.Focus();
                            textBox3.SelectAll();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
            }



        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void radioApendYes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                labelcantappend.Visible = false;
                textBox3.Clear();
                globals.Clearallboxes(this);

                if (radioApendYes.Checked == true)
                {
                    panelOptionApend.Enabled = true;
                    textBox3.Focus();
                }
                else
                {
                    panelOptionApend.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioApendNo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioApendNo.Checked == true)
            {
                panelOptionNew.Enabled = true;
                comboBox1.Focus();
            }
            else
            {
                panelOptionNew.Enabled = false;
            }
        }

        private void StationParameters_Load(object sender, EventArgs e)
        {
            radioApendNo.Checked = true;
            panelOptionNew.Enabled = true;
            panelOptionApend.Enabled = false;
            radioApendYes.Checked = false;
            label1.Text = "Hie " + globals.usernamme + ", you are currently checked in at station " + globals.stationcode;

        }



        private void button2_Click(object sender, EventArgs e)
        {

            con_db newconection = new con_db();
            if (newconection.OpenConnection() == true)
            {
                MessageBox.Show("Connection Successfull");
            }

            else
            {
                MessageBox.Show("Connection Unsuccessfull");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            labelcantappend.Visible = false;
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            globals.Timing(this.labeltime);
        }

        
        private bool   get_indices()
        {
           
                int[] pointarray = new int[100];
                int arrayindex = 0;
                con_db getindex = new con_db();
                string sql = "SELECT chart_index FROM charts ORDER BY chart_index DESC";


                getindex.OpenConnection();
                MySqlDataReader readindex = getindex.Readinfo(sql);

                while (readindex.Read())
                {
                    pointarray[arrayindex] = int.Parse("" + readindex["chart_index"]);
                    arrayindex++;
                }
                if (validate_dates(pointarray))
                {
                    return true;
                }
                else return false;
            
        }

        

        private bool validate_dates(int[] indexarray){

            try
            {

                con_db getlargestindex = new con_db();
                string newsql = "SELECT date_off FROM charts WHERE chart_index ='" + indexarray[0] + "'";
                getlargestindex.OpenConnection();
                MySqlDataReader newreadindex = getlargestindex.Readinfo(newsql);

                newreadindex.Read();

                if (!newreadindex.HasRows) {

                    return true;
                }

                string off = "" + newreadindex["date_off"];
                DateTime doff = Convert.ToDateTime(off).Date;

                var don = dateTimePicker1.Value;


                if (don.Subtract(doff).TotalDays >= 1)
                {
                    if (DialogResult.Yes == MessageBox.Show("River data for the past " + don.Subtract(doff).TotalDays + " days is will not be captured. Do you want to continue? ", "Loose data", MessageBoxButtons.YesNo))

                        return true;

                    else
                    {
                        return false;
                    }
                }
                else
                {
                    //MessageBox.Show("dates good");
                    return true;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false ;
            }

        }
    }
}