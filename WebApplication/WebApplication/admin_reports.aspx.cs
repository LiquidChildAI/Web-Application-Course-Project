using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication
{
    public partial class admin_reports : System.Web.UI.Page
    {
        String username;        
        DB_Handler_DAL db;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                db = new DB_Handler_DAL();
                username = Request.QueryString["user"];
                lHello.Text = "admin, Hello " + username;                
                
            }
            catch (Exception exception)
            {
                Response.Write("Error:\n" + exception.ToString());
            }
        }
    }
}