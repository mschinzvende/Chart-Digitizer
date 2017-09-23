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
    public partial class chartmngmnt : Form
    {
        public chartmngmnt()
        {
            InitializeComponent();
        }


        private void Getallcharts(string sql,bool decide= true )  //Queries for all charts in the system
        {
           
            con_db getcharts = new con_db();
            getcharts.OpenConnection();
            MySqlDataReader readcharts = getcharts.Readinfo(sql);

            while (readcharts.Read())
            {
                if (decide)
                {
                    listCharts.Items.Add(readcharts["chart_number"]);
                }
                else
                {
                    label11.Text = "" + readcharts["chart_number"];
                    label12.Text =""+ readcharts["st_code"];
                    //label13.Text= ""+ readcharts["date_digitized"];
                    label14.Text=""+ readcharts["season"];
                    string on =""+ readcharts["date_on"];
                    string off = "" +readcharts["date_off"];

                    label16.Text = ""+on ;
                    label18.Text = "" + off;

                    DateTime don = Convert.ToDateTime(on).Date;
                    DateTime doff = Convert.ToDateTime(off).Date;

                    string diff = doff.Subtract(don).ToString();
                    
                    label15.Text = ""+diff;
                }
            }
            getcharts.CloseConnection();
            
        }

        private void Getforchatrt(string sql)  // Queries for a specific chart to display in chart area
        {
            con_db getcharts = new con_db();
            getcharts.OpenConnection();
            MySqlDataReader readcharts = getcharts.Readinfo(sql);
            chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            while (readcharts.Read())
            {
                
                //MessageBox.Show("Got point " + readcharts["X(actualcoord)"].ToString()+", "+ readcharts["Y(actualcoord)"].ToString());
              //for testing  MessageBox.Show("" + readcharts["st_code"] + ", " + readcharts["st_name"]);
                chart1.Series["Series1"].Points.AddXY(readcharts["X(actualcoord)"].ToString(), readcharts["Y(actualcoord)"].ToString());
                
            }
            getcharts.CloseConnection();
        }

    
       
        private void chartmngmnt_Load(object sender, EventArgs e)
        {
           
           Getallcharts("SELECT * From charts");
           listCharts.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Visible = true;
            textchartno.Visible = true;
            textchartno.Focus();
            textchartno.SelectAll();
        }

        private void textperstation_TextChanged(object sender, EventArgs e)
        {

            
            listCharts.Items.Clear();
            Getallcharts("SELECT * FROM charts WHERE st_code = '" + textperstation.Text + "'");
            
        }

        private void textchartno_TextChanged(object sender, EventArgs e)
        {
            textperstation.Clear();
            listCharts.Items.Clear();
            Getallcharts("SELECT * FROM charts WHERE chart_number='" + textchartno.Text + "'");
        }

        private void textchartno_Leave(object sender, EventArgs e)
        {
            label3.Visible = false;
            textchartno.Visible = false;
        }

        private void listCharts_SelectedIndexChanged(object sender, EventArgs e)
        {
            globals.selected_chart = listCharts.SelectedItem.ToString();
           Getallcharts ("SELECT * FROM charts WHERE chart_number='" + listCharts.SelectedItem.ToString()+"'",false );
            Getforchatrt("SELECT X(actualcoord),Y(actualcoord) FROM coordinates WHERE chart_number='"+ listCharts .SelectedItem .ToString ()+"' ORDER BY cid DESC");
            // Getforchatrt("SELECT c.* ,X(actualcoord),Y(actualcoord), s.st_name FROM charts c,coordinates p,stations s  WHERE c.chart_number='" + listCharts.SelectedItem.ToString() + "' AND p.chart_number ='" + listCharts.SelectedItem.ToString() + "' AND c.st_code = s.st_code ");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            con_db del_chart = new con_db();
            del_chart.OpenConnection();
            string sql = "DELETE FROM charts WHERE chart_number='" + listCharts.SelectedItem.ToString() + "'";

            MessageBox.Show("Are you sure you want to delete the chart");
          
            

        }

        private void button4_Click(object sender, EventArgs e)

        {
            
            Hide();
            chart_edit newform = new chart_edit();
            newform.ShowDialog();
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

    
    }
}
