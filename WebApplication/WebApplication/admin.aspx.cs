using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication
{
    public partial class admin : System.Web.UI.Page
    {
        String username;
        DB_Handler_DAL db;

        double minMonthlyH = 0, maxMonthlyH = 0, hourSal = 0, extraHourSal = 0;

        protected void Page_Load(object sender, EventArgs e)
        {            
            try
            {
                db = new DB_Handler_DAL();
                username = Request.QueryString["user"];
                lHello.Text = "admin, Hello " + username;

                db.addActionLog("admin.asp logged , user: " + username);                
                db.getManagementDetails(ref minMonthlyH, ref maxMonthlyH, ref hourSal, ref extraHourSal);
                
                lMaxMonthlyHours.Text = " מקסימום שעות עבודה לחודש: " + maxMonthlyH;
                lMinMonthlyHours.Text = " מינימום שעות עבודה לחודש: " + minMonthlyH;
                lHourSalary.Text = "שכר לשעה : " + hourSal;
                lExtraHoursSalary.Text = "שכר לשעות נוספות : " + extraHourSal;
              
                db.addActionLog("admin.aspx: logged , user: " + username);
                db.addActionLog("admin.aspx: Management Details Loaded Successfully");
               
            }
            catch (Exception execption)
            {
                Response.Write("Error Connecting to DB.\n" + execption.ToString());
                db.addActionLog("admin.aspx: DB Error at 'Page_Load' ");
            }

        }

        protected void tMaxMonthlyHours_TextChanged(object sender, EventArgs e)
        {

        }

        protected void tMinMonthlyHours_TextChanged(object sender, EventArgs e)
        {

        }

        protected void tHourSalary_TextChanged(object sender, EventArgs e)
        {

        }

        protected void tExtraHourSalary_TextChanged(object sender, EventArgs e)
        {

        }

        protected void bAddReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (tReport.Text.Length > 0)
                {
                    db.add_report(tReport.Text);
                    db.addActionLog("admin.aspx: Report added , user: " + username);
                    lReportNotification.Text = " :) ";
                    tReport.Text = "";

                }
                else
                {
                    lReportNotification.Text = " Text is Empty ";
                }
            }
            catch (Exception exception)
            {
               // Response.Write("Error writing report.\n" + exception.ToString());
                db.addActionLog("admin.aspx: Error Writing Report , user: " + username);
                lReportNotification.Text = " Error writing report ";
            }
        }

        protected void tReport_TextChanged(object sender, EventArgs e)
        {

        }

        protected void bClean_Click(object sender, EventArgs e)
        {
            tReport.Text = "";
        }

        protected void bGeneralReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin_reports.aspx?user=" + username);
            db.addActionLog("admin.aspx: move to General Reports , user: " + username);
        }

        protected void bChangeMinMonthlyHours_Click(object sender, EventArgs e)
        {
            if (tMinMonthlyHours.Text.Length > 0)
            {
                db.updateMinMonthlyHours(tMinMonthlyHours.Text);
                db.addActionLog("admin.aspx: updated Min Monthly Hours to " + tMinMonthlyHours.Text +" , user: " + username);
            }
        }

        protected void tEID_TextChanged(object sender, EventArgs e)
        {

        }

        protected void tEFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        protected void tELastname_TextChanged(object sender, EventArgs e)
        {

        }

        protected void tEUsername_TextChanged(object sender, EventArgs e)
        {

        }

        protected void tEPassword_TextChanged(object sender, EventArgs e)
        {

        }

        protected void bESubmit_Click(object sender, EventArgs e)
        {
            string id = tEID.Text;
            string fname = tEFirstName.Text;
            string lname = tELastname.Text;
            string user = tEUsername.Text;
            string pass = tEPassword.Text;

            //Response.Write(id + " " + fname + " " + lname + " " + user + " " + pass);
            if (id.Length > 0 && fname.Length > 0 && lname.Length > 0 && user.Length > 0 && pass.Length > 0)
            {
                if ( db.isUsernameExists(user) )
                {
                    lAdminAddEmployee.Text = "Username Already Exists!";
                }
                else if (db.insertNewEmployeeToTable(id, fname, lname, user, pass) == 1 )
                {
                    //Response.Write("ADDED Successfully To Employees");
                    lAdminAddEmployee.Text = "ADDED Successfully To Employees"; 
                    db.addActionLog("admin.aspx: Employee added Successfully to Employees Table at 'bESubmit_Click'");
                    
                    if (db.inserToUsersTable(user, pass,cbEAdmin.Checked) == 1)
                    {
                        //Response.Write("ADDED Successfully To USERS");
                        db.addActionLog("admin.aspx: Employee added Successfully to Users Table at 'bESubmit_Click'");
                        lAdminAddEmployee.Text += " , ADDED Successfully To Users"; 
                    }
                    else
                    {
                        lAdminAddEmployee.Text = "Error adding to USERS";
                        db.addActionLog("admin.aspx: Error writing to Users Tables at 'bESubmit_Click'");
                    }
                }
                else
                {
                    lAdminAddEmployee.Text = "Check Employee Details , or DB Error";
                    db.addActionLog("admin.aspx: Error writing to Employees Tables at 'bESubmit_Click'");

                }
            }
            else
            {
                lAdminAddEmployee.Text = "One or more fields are empty";
                db.addActionLog("admin.aspx: Field with Empty String at 'bESubmit_Click'");
            }
        }

        protected void bChangeMaxMonthlyHours_Click(object sender, EventArgs e)
        {
            if (tMaxMonthlyHours.Text.Length > 0)
            {
                db.updateMaxMonthlyHours(tMaxMonthlyHours.Text);
                db.addActionLog("admin.aspx: updated Max Monthly Hours to " + tMaxMonthlyHours.Text + " , user: " + username);
            }
        }

        protected void bChangeHourSalary_Click(object sender, EventArgs e)
        {
            if (tHourSalary.Text.Length > 0)
            {
                db.updateHourSalary(tHourSalary.Text);
                db.addActionLog("admin.aspx: updated Max Monthly Hours to " + tHourSalary.Text + " , user: " + username);
            }
        }

        protected void bChangeExtraHourSalary_Click(object sender, EventArgs e)
        {
            if (tExtraHourSalary.Text.Length > 0)
            {
                db.updateExtraHourSalary(tExtraHourSalary.Text);
                db.addActionLog("admin.aspx: updated Max Monthly Hours to " + tExtraHourSalary.Text + " , user: " + username);
                
            }

        }
    }
}