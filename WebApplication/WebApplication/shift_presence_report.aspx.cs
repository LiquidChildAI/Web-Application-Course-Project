using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class shift_presence_report : System.Web.UI.Page
    {
        DB_Handler_DAL db;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                db = new DB_Handler_DAL();
               // tbFromH.Text = ""; //System.DateTime.Now.TimeOfDay.ToString();
                //tbToHour.Text = "";// System.DateTime.Now.TimeOfDay.ToString();
            }
            catch (Exception exception)
            {
            }
        }

        protected void bCreate_Click(object sender, EventArgs e)
        {
            string fromH = tbFromH.Text;
            string toH = tbToHour.Text;
            //lNotification.Text = 
            try
            {
                LinkedList<string> l = db.allUsersAtShift(fromH, toH);
                if (l.Count > 0)
                {
                    lNotification.Text = " :) ";
                    lOnshift.Text = "Employees on Current Time Set";
                    lOnshift.Visible = true;
                    gvOnshift.Visible = true;
                    create_table(l);
                }
                else
                {
                    gvOnshift.Visible = false;
                    lOnshift.Visible = false;
                    lNotification.Text = "Shift is Empty";
                }
            }
            catch (Exception exception)
            {
            }
        }

        protected void tbFromH_TextChanged(object sender, EventArgs e)
        {

        }

        protected void tbToHour_TextChanged(object sender, EventArgs e)
        {

        }

        protected void gvOnshift_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void create_table(LinkedList<string> l)
        {
            try
            {
                System.Data.DataTable dataSourceTable = new System.Data.DataTable();
                dataSourceTable.Columns.Add(new System.Data.DataColumn("ID Number", typeof(string)));
                dataSourceTable.Columns.Add(new System.Data.DataColumn("First Name", typeof(string)));
                dataSourceTable.Columns.Add(new System.Data.DataColumn("Last Name", typeof(string)));
                            
                
              for (int i=0; i<l.Count; i++)
              {
                  string employee_user = l.ElementAt(i);
                  string employee = db.getEmployeeStringByUsername(employee_user);

                 System.Data.DataRow row = dataSourceTable.NewRow();

                 row[0] = employee.Substring(0, employee.IndexOf(" "));
                 employee = employee.Substring(employee.IndexOf(" ") + 1);

                 row[1] = employee.Substring(0, employee.IndexOf(" "));
                 employee = employee.Substring(employee.IndexOf(" ") + 1);

                 row[2] = employee.Substring(0, employee.IndexOf(" "));
                 

                 dataSourceTable.Rows.Add(row);
              }
               
                
                //gvDailyHoursPerMonth.AutoGenerateColumns = true;
                gvOnshift.DataSource = dataSourceTable;
                gvOnshift.DataBind();
            }
            catch (Exception exception)
            {
                Response.Write("Error Occured During Compute");

            }
        }

    }
}