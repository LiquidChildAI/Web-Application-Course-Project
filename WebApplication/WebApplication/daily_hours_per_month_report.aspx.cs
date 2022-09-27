using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class daily_hours_per_month_report : System.Web.UI.Page
    {
        DB_Handler_DAL db;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                db = new DB_Handler_DAL();
                tbMonth.MaxLength = 2; // MM
                tbYear.MaxLength = 4; // YYYY
            }
            catch (Exception exception)
            {
                Response.Write(exception.ToString());
            }
        }

        protected void bCreateReport_Click(object sender, EventArgs e)
        {
            try
            {
                string month = tbMonth.Text;
                string year = tbYear.Text;
                string username_for_report = tbUsername.Text;
                if (db.isUsernameExists(username_for_report))
                {
                    if (month.Length > 0 && year.Length > 0 && username_for_report.Length > 0)
                    {

                       // lNotification.Text = month + " " + year + " " + username_for_report;
                        lNotification.Text = " :) ";
                        lNotification.ForeColor = System.Drawing.Color.Red;
                        create_table(month, year, username_for_report);
                        lReportHeader.Text = "Daily Working Hours for (" + month + "/" + year + ") : " + username_for_report.ToString();
                    }
                    else
                    {

                        lNotification.Text = "יש למלא את כל השדות";
                        lNotification.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lNotification.Text = "Username Doesn't Exists!";
                    lNotification.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception exception)
            {
                Response.Write("Error Occured During Compute");
            }

        }
        private void create_table(string month,string year, string username)
        {
            try
            {
                System.Data.DataTable dataSourceTable = new System.Data.DataTable();
                dataSourceTable.Columns.Add(new System.Data.DataColumn("תאריך", typeof(string)));
                dataSourceTable.Columns.Add(new System.Data.DataColumn("משך זמן עבודה", typeof(string)));

                string working_date = "";
                string day = "";

                TimeSpan total_monthly_hours = new TimeSpan(0, 0, 0, 0, 0);
                TimeSpan result;

                for (int i = 1; i <= 30; i++)
                {
                    System.Data.DataRow row = dataSourceTable.NewRow();
                    day = i.ToString();

                    if (i <= 9)
                    {
                        day = "0" + i;
                    }

                    //working_date = year + "-" + day + "-" + month;
                    working_date = day + "/" + month + "/" + year;
                    result = db.dailyHoursForWorker(username, working_date);
                    total_monthly_hours += result;
                    row[0] = day + "-" + month + "-" + year;// working_date;
                    row[1] = result.ToString();
                    dataSourceTable.Rows.Add(row);
                }
                /*********** NEW LINE *******/
                lReportHeader.Text = lReportHeader.Text.ToString() + Environment.NewLine + "Total Monthly Hours: " + total_monthly_hours.ToString();
                /***********/
                //gvDailyHoursPerMonth.AutoGenerateColumns = true;
                gvDailyHoursPerMonth.DataSource = dataSourceTable;
                gvDailyHoursPerMonth.DataBind();
            }
            catch (Exception exception)
            {
                Response.Write("Error Occured During Compute");

            }
        }

        
    }
}