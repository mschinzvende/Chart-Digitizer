using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Digitizer_Take_two
{
    partial class Coordinates
    {
        private float scrnlw;
        private float scrnhi;
        private float reallw;
        private float realhi;




        public float getscrnlowest
        {
            get { return scrnlw; }

        }


        public float getscrnhist
        {
            get { return scrnhi; }

        }


        public Coordinates(float realorigin = 0)
        {

            this.reallw = realorigin;

        }
        public void Setlows(float lowscreen)
        {
            this.scrnlw = lowscreen;

            //   MessageBox.Show("low screen is"+ this.scrnlw ); //testing
        }

        public void Sethighs(float highscreen, float highreal)
        {
            this.scrnhi = highscreen;
            this.realhi = highreal;

            //  MessageBox.Show("Screen high is " + this.scrnhi +", Real high is"+ this.realhi ); //testing
        }

        public float CalculateX(float newpoint)
        {
            try
            {
                float temp1 = this.scrnhi - this.scrnlw;
                float temp2 = this.realhi / temp1;
                float coordinate = temp2 * (newpoint - this.scrnlw);
                return coordinate;
            }
            catch 
            {
                MessageBox.Show("An Error occured please restart the program!");
                return 0;
            }

        }

        public float CalculateY(float newpoint)
        {
            try
            {
                float scrnscale = this.scrnlw - this.scrnhi;
                float getunits = this.scrnlw - newpoint;
                float temp = this.realhi / scrnscale;
                float coordinate = temp * getunits;

                return coordinate;
            }

            catch 
            {
                MessageBox.Show("An error occured please restart the program!");
                return 0;
            }
        }
    }
}

    

