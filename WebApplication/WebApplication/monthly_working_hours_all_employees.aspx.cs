using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class monthly_working_hours_all_employees : System.Web.UI.Page
    {
        DB_Handler_DAL db;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                db = new DB_Handler_DAL();
                lNotiftication.ForeColor = System.Drawing.Color.Red;
                

            }
            catch (Exception exception)
            {
                Response.Write(exception.ToString());
            }
        }

        private void create_table(string month,string year)
        {
            try
            {
                System.Data.DataTable dataSourceTable = new System.Data.DataTable();
                
                dataSourceTable.Columns.Add(new System.Data.DataColumn("Date", typeof(string)));
                dataSourceTable.Columns.Add(new System.Data.DataColumn("ID Number", typeof(string)));
                dataSourceTable.Columns.Add(new System.Data.DataColumn("First Name", typeof(string)));
                dataSourceTable.Columns.Add(new System.Data.DataColumn("Last Name", typeof(string)));
                dataSourceTable.Columns.Add(new System.Data.DataColumn("Username", typeof(string)));
                dataSourceTable.Columns.Add(new System.Data.DataColumn("Monthly Working Hours", typeof(string)));
                dataSourceTable.Columns.Add(new System.Data.DataColumn("עמד במכסת שעות", typeof(string)));
                dataSourceTable.Columns.Add(new System.Data.DataColumn("משכורת", typeof(string)));
                

                string all_users = db.getAllUsername();
                string single_user;
                double minMonthlyH = 0, maxMonthlyH = 0, hourSal = 0, extraHourSal = 0;
                db.getManagementDetails(ref minMonthlyH, ref maxMonthlyH, ref hourSal, ref extraHourSal);
                TimeSpan minMonthH = new TimeSpan((int)minMonthlyH, 0, 0);


                while (all_users.Length > 0)
                {
                    single_user = all_users.Substring(0, all_users.IndexOf(" "));
                    all_users = all_users.Substring(all_users.IndexOf(" ") + 1);

                    System.Data.DataRow row = dataSourceTable.NewRow();

                    string employee = db.getEmployeeStringByUsername(single_user);
                    row[0] = month + " - " + year;
                    row[1] = employee.Substring(0, employee.IndexOf(" "));

                    employee = employee.Substring(employee.IndexOf(" ") + 1);

                    row[2] = employee.Substring(0, employee.IndexOf(" "));

                    employee = employee.Substring(employee.IndexOf(" ") + 1);

                    row[3] = employee.Substring(0, employee.IndexOf(" "));

                    employee = employee.Substring(employee.IndexOf(" ") + 1);

                    row[4] = employee.Substring(0, employee.IndexOf(" "));

                    employee = employee.Substring(employee.IndexOf(" ") + 1);

                    
                     TimeSpan m_time =  db.mymonthlyHoursForWorker(row[4].ToString(),month,year);
                     row[5] = m_time.ToString();
                     
                    //(ref double minMonthlyH, ref double maxMonthlyH, ref double hour_salary, ref double extra_hour_salary)

                    
                    if ( m_time.Hours < minMonthH.Hours )
                    {
                        row[6] = "לא עמד במכסת שעות";
                    }
                    else
                    {
                        row[6] = "עמד במכסת שעות";
                    }

                    row[7] = calcSalary(m_time).ToString() + " $ ";
                   
                    dataSourceTable.Rows.Add(row);
                }
                gvMonthlyHours.DataSource = dataSourceTable;
                gvMonthlyHours.DataBind();
            }
            catch (Exception exception)
            {
                Response.Write("Error Occured During Compute");

            }
        }
        
        private double calcSalary(TimeSpan t)
        {
            double sal = 0;
            try
            {
                double minMonthlyH = 0, maxMonthlyH = 0, hourSal = 0, extraHourSal = 0;
                db.getManagementDetails(ref minMonthlyH, ref maxMonthlyH, ref hourSal, ref extraHourSal);

                double h = t.Hours;
                double m = t.Minutes;
                double s = t.Seconds;
                double ms = t.Milliseconds;

                double total_in_ms = ms + s*1000+ m * 60000 + h * 3600000;
                total_in_ms /= 3600000;

                double extra_h = total_in_ms - maxMonthlyH;
                if (extra_h < 0)
                {
                    extra_h = 0;

                }
                else
                {
                    h -= extra_h;
                }
                sal = hourSal * h + extra_h * extraHourSal;                
            }
            catch (Exception exception)
            {
                return sal;
            }
            return sal;
        }

        protected void bCreateReport_Click(object sender, EventArgs e)
        {
            string year = tbYear.Text;
            string month = tbMonth.Text;

            if (year.Length > 0 && month.Length > 0)
            {
                if (year.Length < 4 || month.Length < 2)
                {
                    lNotiftication.Text = "Year as: YYYY , Month as: MM";
                }

                else
                {
                    create_table(month, year);
                    lNotiftication.Text = " :) ";
                }
            }
            else
            {
                lNotiftication.Text = "Must fill All Fields";
                
            }

        }
    }
}