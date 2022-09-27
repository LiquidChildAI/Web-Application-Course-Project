using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class all_employees_report : System.Web.UI.Page
    {
        DB_Handler_DAL db;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                db = new DB_Handler_DAL();               
                create_table();
                
            }
            catch (Exception exception)
            {
                Response.Write(exception.ToString());
            }
        }

        private void create_table()
        {
            try
            {
                System.Data.DataTable dataSourceTable = new System.Data.DataTable();
                dataSourceTable.Columns.Add(new System.Data.DataColumn("ID Number", typeof(string)));
                dataSourceTable.Columns.Add(new System.Data.DataColumn("First Name", typeof(string)));
                dataSourceTable.Columns.Add(new System.Data.DataColumn("Last Name", typeof(string)));
                dataSourceTable.Columns.Add(new System.Data.DataColumn("Username", typeof(string)));
                dataSourceTable.Columns.Add(new System.Data.DataColumn("Password", typeof(string)));
                dataSourceTable.Columns.Add(new System.Data.DataColumn("User Type (admin / regular)", typeof(string)));

                string all_users = db.getAllUsername();
                string single_user;
                while (all_users.Length > 0)
                {
                    single_user = all_users.Substring(0, all_users.IndexOf(" ") );
                    all_users = all_users.Substring(all_users.IndexOf(" ") + 1);

                    System.Data.DataRow row = dataSourceTable.NewRow();
                    
                    string employee = db.getEmployeeStringByUsername(single_user);

                    row[0] = employee.Substring(0, employee.IndexOf(" "));

                    employee = employee.Substring(employee.IndexOf(" ") + 1);

                    row[1] = employee.Substring(0, employee.IndexOf(" "));

                    employee = employee.Substring( employee.IndexOf(" ") + 1);

                    row[2] = employee.Substring(0, employee.IndexOf(" "));

                    employee = employee.Substring(employee.IndexOf(" ") + 1 );

                    row[3] = employee.Substring(0, employee.IndexOf(" ") );

                    employee = employee.Substring( employee.IndexOf(" ") + 1);

                    row[4] = employee;
                    if (db.isAdmin(single_user))
                        row[5] = "admin user";
                    else
                    {
                        row[5] = "regular user";
                    }
                    dataSourceTable.Rows.Add(row);
                }
                gvAllEmployees.DataSource = dataSourceTable;
                gvAllEmployees.DataBind();
            }
            catch (Exception exception)
            {
                Response.Write("Error Occured During Compute");

            }
        }
    }
}