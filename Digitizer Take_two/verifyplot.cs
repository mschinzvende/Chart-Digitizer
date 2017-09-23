using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Digitizer_Take_two
{
    public partial class verifyplot : Form 
    {
      
        public verifyplot()
        {
            
            InitializeComponent();
        }


       
        private void button1_Click(object sender, EventArgs e)
        {
            
            Loadchart();
           
        }


        private void Loadchart()
        {
            try
            {

                globals.stacksize = upload.mystacky.Count;
                int countforlisting = globals.stacksize - 1;
                chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                Object[] myarrayforx = upload.mystackx.ToArray();
                Object[] myarrayfory = upload.mystacky.ToArray();
                
                //listBox1.Items.Add("Date and Time           " + "|||" + "Rianfal (mm)");
                for (int i = 0; i < globals.stacksize; i++)
                {
                    chart1.Series["Series1"].Points.AddXY(myarrayforx[i], myarrayfory[i]);
                    ListViewItem row = new ListViewItem();
                    row.SubItems.Add("" + myarrayforx[countforlisting]);
                    row.SubItems.Add("" + myarrayfory[countforlisting]);
                    listView1.Items.Add(row);

                  //  listBox1.Items.Add("(" + myarrayforx[countforlisting] + ", " + myarrayfory[countforlisting] + ")");
                    countforlisting--;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
 
        }
        private void verifyplot_Load(object sender, EventArgs e)
        {
            Loadchart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con_db qrychart = new con_db();
                int size = upload.mystackx.Count;
                MessageBox.Show("size is " + size); //testing 
                for (int i = 0; i < size; i++)
                {

                    string sql = "INSERT INTO coordinates (chart_number, actualcoord) VALUES ('" + globals.chartnumber + "',GeomFromText('POINT(" + upload.mystackx.Pop() + " " + upload.mystacky.Pop() + ")'))";
                    qrychart.Insert(sql);
                    MessageBox.Show("Popped point "+upload.mystackx .Pop()+","+ upload.mystacky.Pop()); //Testing point entry
                }
                MessageBox.Show("Success!!!");
                Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < globals.stacksize; i++)
            {
                upload.mystackx.Pop();
                upload.mystacky.Pop();
            }
            MessageBox .Show("Size for x: " + upload.mystackx.Count+ Environment .NewLine +"Size for y: " +upload.mystacky .Count );
            upload redofrm = new upload();
            Dispose();
            redofrm.ShowDialog ();
        }
    }
}
