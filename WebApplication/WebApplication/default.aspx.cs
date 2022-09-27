using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication
{
    public partial class _default : System.Web.UI.Page
    {
        //const string usersTable = "users_and_passwords";
        //const string signningTable = "Signning";
        //const string DB_STRING = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Idan\Desktop\WebApplication\WebApplication\App_Data\SigningDB.mdf;Integrated Security=True";
        //const string DB_STRING = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Projects\WebApplication\WebApplication\App_Data\Signatures.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
        //SqlConnection conn;// = new SqlConnection(DB_STRING);//';//ConfigurationManager.ConnectionStrings[""].ConnectionString);
        DB_Handler_DAL db;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //conn = new SqlConnection(DB_STRING);
                db = new DB_Handler_DAL();
                
            }
            catch (Exception execption)
            {
                //Response.Write("Error Connecting to DB.\n" + execption.ToString());
                lLastSigned.Text = "Error Occured, Please Try Again";
            }

        }

        protected void userBClick_Click(object sender, EventArgs e)
        {
            lSigned.Text = "";
            lLastSigned.Text = "";
            try
            {
                
                if (db.checkValidation(tusername.Text, tpassword.Text))
                {
                    //lLastSigned.Text = db.lastSigned(tusername.Text);
                    if (adminCheckBox.Checked ) // Admin page
                    {
                        if (db.isAdmin(tusername.Text))
                        {
                            db.addActionLog("Admin Login, user: " + tusername.Text);
                            Response.Redirect("admin.aspx?user=" + tusername.Text);
                        }
                        else
                        {
                            lSigned.Text = "Not an Admin user";
                            lLastSigned.Text = "Not an Admin user";
                        }
                    }
                    else
                    {
                        
                            string signed_today = db.lastSignedToday(tusername.Text);
                            if (signed_today != null) // Already signed today
                            {
                                if (signed_today.CompareTo("IN") == 0)
                                {
                                    db.insertToSignning(tusername.Text, "o");
                                    lSigned.Text = "NOW:  Sign at: " + DateTime.Now + " , Status = OUT "; // Label update                        
                                    db.addActionLog("Signed in, user: " + tusername.Text);
                                }
                                else if (signed_today.CompareTo("OUT") == 0)
                                {
                                    db.insertToSignning(tusername.Text, "i");
                                    lSigned.Text = "NOW:  Sign at: " + DateTime.Now + " , Status = IN "; // Label update                   
                                    db.addActionLog("Signed out, user: " + tusername.Text);
                                }
                            }
                            else // Not signed today
                            {
                                db.insertToSignning(tusername.Text, "i");
                                lSigned.Text = "NOW:  Sign at: " + DateTime.Now + " , Status = IN "; // Label update                   
                                db.addActionLog("Signed in, user: " + tusername.Text);
                            }                        
                    adminCheckBox.Checked = false;
                    }
                }
                else
                {                    
                    lSigned.Text = "Error username or password";
                    db.addActionLog("Login failed, user: " + tusername.Text + " password: " + tpassword.Text);
                }
            }
            catch (Exception exception)
            {
                //Response.Write("Error Occured.\n" + exception.ToString());
                lLastSigned.Text = "Error Occured,Please try again";
                db.addActionLog("DB Error at 'userBClick_Click'");
            }
        }
        protected void adminCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
      

    }
}


