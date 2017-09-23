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
    public partial class useredit : Form  
    {
        public useredit()
        {
            InitializeComponent();
        }

        private void useredit_Load(object sender, EventArgs e)
        {
           Populate();
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

        private void Populate()
        {
            try
            {
                string sql = "SELECT * FROM " + globals.tablescope + " WHERE usrname='" + globals.targetuser + "' ";
                // MessageBox .Show(""+ sql ); //testing sql

                con_db populateforedit = new con_db();
                populateforedit.OpenConnection();
                MySqlDataReader read = populateforedit.Readinfo(sql);
                while (read.Read())
                {
                    textusrername.Text = read["usrname"].ToString();
                    textpassword.Text = read["usrpswrd"].ToString();
                    textverify.Text = read["usrpswrd"].ToString();
                    textphone.Text = read["phone"].ToString();
                    //dateTimedob.Text = read["dob"].ToString();
                    combogender.Text = read["gender"].ToString();
                    textemail.Text = read["email"].ToString();

                    if (globals.tablescope == "admns")
                    {
                        textacceslevel.Text = "Administrator";
                    }
                    else if (globals.tablescope == "gnrlusers")
                    {
                        textacceslevel.Text = "General User";
                    }

                    globals.rowid = int.Parse(read["usrID"].ToString());

                }
                populateforedit.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Populate();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            queryforupdate();
        }

        private void queryforupdate()
        {
            try
            {
                string username = textusrername.Text;

                string thequery = ""; //= "INSERT INTO admns (admName, admPass,phone,dob,gender,email) VALUES ('"+username+"','"+password +"','"+phone+"','"+dob+"','"+gender+"','"+email+"')";
                if ((textpassword.Text == textverify.Text) && (textpassword.Text != ""))
                {
                    if (textacceslevel.Text == "Administrator")
                    {
                        thequery = "UPDATE  `admns` SET  `usrname` =  '" + textusrername.Text + "',`usrpswrd` =  '" + textpassword.Text + "',`phone` =  '" + textphone.Text + "',`dob` =  '" + dateTimedob.Text + "',`gender` =  '" + combogender.Text + "' WHERE  `admns`.`usrID` =  '" + globals.rowid + "' LIMIT 1";
                        // thequery = "UPDATE admns SET usrname= '" + textusrername.Text + "', usrpswrd= '" + textpassword.Text + "',phone= '" + textphone.Text + "',dob= '" + dateTimedob.Text + "', gender= '" + combogender.Text + "', email= '" + textemail.Text + "' ";
                    }
                    else if (textacceslevel.Text == "General User")
                    {
                        thequery = "UPDATE  `gnrlusers` SET  `usrname` =  '" + textusrername.Text + "',`usrpswrd` =  '" + textpassword.Text + "',`phone` =  '" + textphone.Text + "',`dob` =  '" + dateTimedob.Text + "',`gender` =  '" + combogender.Text + "' WHERE  `gnrlusers`.`usrID` =  '" + globals.rowid + "' LIMIT 1";
                    }
                    else
                    {
                        labelcantuser.Visible = true;
                    }
                    con_db updateuser = new con_db();

                    if (updateuser.Update(thequery))
                    {
                        MessageBox.Show("User Updated successfully");
                        Populate();
                    }
                    else
                    {
                        MessageBox.Show("User Update failed!!");
                    }
                }
                else
                {
                    labelcantuser.Text = "Passwords Do Not Match Please Verify Your Password";
                    labelcantuser.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
