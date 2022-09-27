using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class logs : System.Web.UI.Page
    {
        DB_Handler_DAL db;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                db = new DB_Handler_DAL();
                LinkedList<string> l = db.getAllActionLogs();
                string logs = "";
              for (int i=0; i< l.Count; i++)
              {
                  logs += l.ElementAt(i).ToString() + "\n"; 
               }
             
              tbLogs.Text = logs;
              
            }
            catch (Exception exception)
            {
            }
        }
    }
}