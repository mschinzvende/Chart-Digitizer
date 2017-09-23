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
    public partial class chart_edit : Form
    {
        
        public chart_edit()
        {
            InitializeComponent();
        }

        private void chart_edit_Load(object sender, EventArgs e)
        {
            
            poptoeditchrt("SELECT * FROM charts WHERE chart_number='" + globals.selected_chart + "'");
        }

        private void poptoeditchrt(string thequery)
        {
            con_db getforedit = new con_db();
            getforedit.OpenConnection();


            MySqlDataReader readcharts = getforedit.Readinfo(thequery );

           while (readcharts.Read())
            {

                textBox1.Text = "" + readcharts["chart_number"];
                textBox2.Text = "" + readcharts["st_code"];
                textBox4.Text = "" + readcharts["season"];
                
            string date_digitized = "" + readcharts["date_digitized"];
              string dateon = "" + readcharts["date_on"];
              string dateoff = "" + readcharts["date_off"];
            DateTime ddgtzd = Convert.ToDateTime(date_digitized).Date;
              DateTime don = Convert.ToDateTime(dateon).Date;
              DateTime doff = Convert.ToDateTime(dateoff).Date;
               dateTimePicker1.Value = ddgtzd;
              dateTimePicker2.Value = don;
              dateTimePicker3.Value = doff;

              string diff = doff.Subtract(don).ToString();

              textBox5.Text = "" + diff;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var chartno = int.Parse(textBox1.Text);
            var station = textBox2.Text;
            var datedigitzed = dateTimePicker1.Value.ToString("yyyy/MM/dd");
            int season = int.Parse(textBox4.Text);
            var dateon = dateTimePicker2.Value.ToString("yyyy/MM/dd");
            var dateoff = dateTimePicker3.Value.ToString("yyyy/MM/dd");

            con_db save_chart = new con_db();
            string sql = "UPDATE  `charts` SET  `st_code` =  '" + station + "',`date_digitized` =  '" + datedigitzed + "',`season` =  '" + season + "',`date_on` =  '" + dateon + "',`date_off` =  '" + dateoff + "' WHERE  `chart_number` =  '"+ chartno+"' LIMIT 1";
            if (save_chart.Update(sql))
            {
                MessageBox.Show("Data Updated Successfully");

                Dispose();
                chartmngmnt newform = new chartmngmnt();
                newform.ShowDialog();
            }
            else
            {
                MessageBox.Show("Failed to Update data");
            }
            
        }
    }
}
