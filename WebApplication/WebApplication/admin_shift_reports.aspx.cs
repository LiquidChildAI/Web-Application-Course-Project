using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class working_now_report : System.Web.UI.Page
    {
        DB_Handler_DAL db;//= new DB_Handler_DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                db = new DB_Handler_DAL();
                lNotification.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception exception)
            {
                lNotification.Text = "Error Occured";
            }
            
        }

        protected void bGetReports_Click(object sender, EventArgs e)
        {
            try
            {
                string month = tbMonth.Text;
                string year = tbYear.Text;
                string day = tbDay.Text;
                if (day.Length == 2 && month.Length == 2 && year.Length == 4)
                {
                    
                    LinkedList<string> l = db.getReportsAtDate(day,month, year);
                    if (l.Count > 0)
                    {
                        bDeleteReport.Visible = true;
                        string reports = "";
                        for (int i = 0; i < l.Count; i++)
                        {
                            reports += l.ElementAt(i).ToString() + "\n";
                        }
                        lReportsLogHeder.Visible = true;
                        lReports.Visible = true;
                        lReports.Text = reports;
                        lReportsLogHeder.Text = "Reports for: " + day+ "/ " + month + " / " + year;
                        lNotification.Text = " :) ";
                    }
                    else
                    {
                        bDeleteReport.Visible = false;
                        lReportsLogHeder.Visible = true;
                        lReports.Visible = false;
                        lNotification.Text = "";
                        lReportsLogHeder.Text = "No Reports";
                    }
                }
                else
                {
                    lReportsLogHeder.Text = "Fields Error";
                    // lReportsLogHeder.Visible = false;
                    lReports.Visible = false;
                    bDeleteReport.Visible = false;
                }
            }
            catch (Exception exception)
            {
                lNotification.Text = " Error Occured ";
            }
        }

        protected void tbMonth_TextChanged(object sender, EventArgs e)
        {

        }

        protected void tbYear_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lReports_TextChanged(object sender, EventArgs e)
        {

        }

        protected void bDeleteReport_Click(object sender, EventArgs e)
        {
             string month = tbMonth.Text;
             string year = tbYear.Text;
             string day = tbDay.Text;
            if (month.Length == 2 && year.Length == 4)
            {
                if (db.deleteReportAtDate(day,month, year) > 0)
                {
                    lNotification.Text = "Report Deleted";
                    lReports.Visible = false;
                    bDeleteReport.Visible = false;
                    lReportsLogHeder.Visible = false;
                }
               
            }
        }
    }
}