using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Digitizer_Take_two
{
    public static class globals
    {
        public static int stationcode;
        public static string usernamme;
        public static int chartnumber;
        public static int stacksize;
        public static string tablescope;
        public static string targetuser;
        public static int rowid;
        public static string  selected_chart;
        public static string x_date;
        public static void Clearallboxes(Control tbox)
        {
            
                foreach (Control clrall in tbox.Controls)
                {
                    if (clrall is MaskedTextBox)
                    {
                        ((MaskedTextBox)clrall).Clear();
                    }

                    if (clrall is TextBox)
                    {
                        ((TextBox)clrall).Clear();

                    }


                    else
                        Clearallboxes(clrall);
                }
            
        }
        public static void Verifypasswords(Control text1, Control text2)
        {

            if (text1.Text != text2.Text)
            {
                text1.BackColor = Color.Tomato;
            }
            else if (text1.Text == text2.Text)
            {
                text1.BackColor = Color.Aquamarine;
                text2.BackColor = Color.Aquamarine;
            }
        }

        public static void Timing(Control label)
        {
            label.Text =DateTime.Now.AddSeconds(1).ToString ();
        }

        public static void IsValidEmailId(Control thetextbox)
        {
            
            
                string InputEmail = thetextbox.Text;
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(InputEmail);
                if (!match.Success)
                {
                    MessageBox.Show("Invalid E-mail address!!");
                    thetextbox.Select();
                }
            
           
            
         
        }

        public static void ValidateText(Control thetextbox)
        {
            
                string userinput = thetextbox.Text;
                if (string.IsNullOrWhiteSpace(userinput) || userinput.Any(Char.IsDigit))
                {
                    MessageBox.Show("Digits Not Allowed in this field");
                    thetextbox.Select();
                }
            
            
        }

        public static void ValidateNumbers(Control thetextbox)
        {
            
            
                string userinput = thetextbox.Text;
                if (string.IsNullOrWhiteSpace(userinput) || userinput.Any(Char.IsLetter))
                {
                    MessageBox.Show("Letters Not Allowed in this field");
                    thetextbox.Select();

                }
            
        }

       
    }
}
