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
    public partial class adduser : Form
    {
        public adduser()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string username = textusrername.Text;
                string password = textpassword.Text;
                string phone = textphone.Text;
                string dob = dateTimedob.Text;
                string gender = combogender.Text;
                string email = textemail.Text;
                string thequery = ""; //= "INSERT INTO admns (admName, admPass,phone,dob,gender,email) VALUES ('"+username+"','"+password +"','"+phone+"','"+dob+"','"+gender+"','"+email+"')";
                if ((textpassword.Text == textverify.Text) && (textpassword.Text != ""))
                {
                    if (comboaccess.Text == "Administrator")
                    {
                        thequery = "INSERT INTO admns (usrname, usrpswrd,phone,dob,gender,email) VALUES ('" + username + "','" + password + "','" + phone + "','" + dob + "','" + gender + "','" + email + "')";
                    }
                    else if (comboaccess.Text == "General User")
                    {
                        thequery = "INSERT INTO gnrlusers (usrname, usrpswrd,email,phone,gender,dob) VALUES ('" + username + "','" + password + "','" + email + "','" + phone + "','" + gender + "','" + dob + "')";
                    }
                    else
                    {
                        labelcantuser.Visible = true;
                    }
                    con_db adduser = new con_db();

                    if (adduser.Insert(thequery))
                    {
                        MessageBox.Show("User added successfully");
                        globals.Clearallboxes(this);
                    }
                    else
                    {
                        MessageBox.Show("User add failed!!");
                    }
                }
                else
                {
                    labelcantuser.Text = "Passwords Do Not Match Please Verify Your Password";
                    labelcantuser.Visible = true;
                }
            }
            catch 
            {
                MessageBox.Show("An Error occured please contact the system administrator");
                
            }

        }

        private void adduser_Load(object sender, EventArgs e)
        {

        }

        private void textpassword_TextChanged(object sender, EventArgs e)
        {
            labelcantuser.Visible = false;
            globals.Verifypasswords(textpassword, textverify);
        }

        private void textverify_TextChanged(object sender, EventArgs e)
        {
            labelcantuser.Visible = false;
            globals.Verifypasswords(textpassword, textverify);
        }

     

        private void textusrername_Leave(object sender, EventArgs e)
        {
            globals.ValidateText(textusrername);
        }

        private void textemail_Leave(object sender, EventArgs e)
        {
            globals.IsValidEmailId(textemail);
        }

        private void textphone_Leave(object sender, EventArgs e)
        {
            globals.ValidateNumbers(textphone);
          
        }

       
    }
}
