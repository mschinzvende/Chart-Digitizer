using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
namespace Digitizer_Take_two
{
    public partial class upload : System.Windows.Forms.Form
    {
        Coordinates X = new Coordinates(0);
        Coordinates Y = new Coordinates(0);
       public static  Stack mystackx = new Stack();
        public static Stack mystacky = new Stack();
      
       
        int calibtrack = 0;
        public upload()
        {
            InitializeComponent();
            
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            labelstatus.Text = "Import Chart Onto Workspace";
            chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mark the lowest point ");

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
            if (calibtrack == 1) 
            {
                
                X.Setlows(Cursor.Position.X );
                Y.Setlows(Cursor.Position.Y);
                calibtrack++;
                labelstatus.Text = "Mark The Highest X Point / Extreme Right";
                MessageBox.Show("Mark The Highest X Point / Extreme Right");
            }
       

            else if (calibtrack == 2) 
            {
                float realx = 168;
                X.Sethighs(Cursor.Position .X , realx );
                calibtrack++;
                labelstatus.Text = "Mark The Highest Y Point / Topmost";
                MessageBox.Show("Mark The Highest Y Point / Topmost");
            }

            else if (calibtrack == 3)
            {
                float realy = 100;
                Y.Sethighs(Cursor.Position.Y, realy);
                calibtrack++;
                labelstatus.Text = "Start Marking Along The Trace";
                MessageBox.Show("Start Marking Along The Trace");
            }

            else if (calibtrack == 4)
            {
                if (Cursor.Position.X > X.getscrnhist || Cursor.Position.X < X.getscrnlowest)
                {
                    MessageBox.Show("X coordinate out of bounds!!!");
                }
                else if (Cursor.Position.Y < Y.getscrnhist || Cursor.Position.Y > Y.getscrnlowest)
                {
                    MessageBox.Show("Y coordinate out of bounds!!!");
                }
                else
                {
                    float xcord = X.CalculateX(Cursor.Position.X);
                    float ycord = Y.CalculateY(Cursor.Position.Y);
                    labelstatus .Text="Digitized ("+xcord +", "+ycord +"). Mark Next Point, Click Done to Complete";

                    mystackx.Push(getxdate (xcord));
                    mystacky.Push(ycord);
                    chart1.Series["Series1"].Points.AddXY(xcord , ycord);

                   // listBox1.Items.Add(xcord + " , " + ycord );
                    
                }
            }
            else 
            {
                MessageBox.Show("Upload first");
            }

        }
      
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (calibtrack == 0)
                {
                    string filename;

                    openFileDialog1.ShowDialog();
                    openFileDialog1.Filter = "JPEG files|*.jpg|PNG files|*.png|GIF Files|*.gif|TIFF Files|*.tif|BMP Files|*.bmp";

                    filename = openFileDialog1.FileName;

                    Image img = Image.FromFile(filename);
                    pictureBox1.Image = img;

                    calibtrack++;

                    labelstatus.Text = "Mark The Lowest Point";
                    MessageBox.Show("Mark The Lowest Point");
                }
                else
                {
                    MessageBox.Show("Current Chart Unfinished");
                }
            }
            catch
            {
                MessageBox.Show("An unexpected error occured, Contact the administrator!");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox .Show("X :"+ Cursor.Position.X + Environment .NewLine + "Y :" +Cursor.Position.Y );
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if ((mystackx.Count < 0) || (mystacky.Count < 0))
                {
                    MessageBox.Show("You did not digitize anything");
                }
                else
                {
                    Hide();
                    verifyplot newfrm = new verifyplot();
                    newfrm.ShowDialog();
                    Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                labelstatus.Text = "You removed point (" + mystackx.Pop() + ", " + mystacky.Pop() + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private DateTime   getxdate(float x_coordinate)
        {
            DateTime doff = Convert.ToDateTime(globals. x_date ).Date;
            var newdate= doff.AddHours(x_coordinate);

            globals.x_date = newdate.ToString ();

            return newdate;

        }
    }
}

