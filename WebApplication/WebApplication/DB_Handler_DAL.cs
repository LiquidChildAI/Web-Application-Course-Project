using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WebApplication
{
    public class DB_Handler_DAL
    {

        public const string DB_STRING = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Idan\Desktop\WebApplication\WebApplication\App_Data\SigningDB.mdf;Integrated Security=True";

        const string managementTable = "management_details";
        const string usersTable = "users_and_passwords";
        const string signningTable = "Signning";
        const string employeesTable = "Employees";
        const string reportsTable = "shifts_reports";
        const string actionLogsTable = "ActionsLogs";

        private SqlConnection con;



        public DB_Handler_DAL()
        {
            try
            {
                con = new SqlConnection(DB_STRING);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }



        public TimeSpan dailyHoursForWorker(string user, string day_date)
        {
            TimeSpan total_day_hours_in_millisecond = new TimeSpan(0, 0, 0, 0, 0);
            try
            {
                con.Open();
                SqlCommand get_cmd =
                new SqlCommand(
"SELECT t1.date,(t2.s_time - t1.s_time) as total "
 + "FROM( SELECT date,SUM(DATEDIFF(millisecond, '00:00:00:00',time)) as s_time,in_out FROM " + signningTable
+ " WHERE  date =convert(smalldatetime,'" + day_date + "',103) and  in_out ='i' and username='" + user + "' "
+ "GROUP BY date,in_out) as t1,"
+ "(SELECT date,SUM(DATEDIFF(millisecond, '00:00:00:00',time)) as s_time,in_out FROM " + signningTable
+ " WHERE  date =convert(smalldatetime,'" + day_date + "',103) and in_out ='o' and username='" + user + "' "
+ "GROUP BY date,in_out) as t2", con);


                SqlDataReader get_reader = get_cmd.ExecuteReader();

                if (get_reader.HasRows)
                {

                    get_reader.Read();
                    //return get_reader.GetDataTypeName(1).ToString();

                    total_day_hours_in_millisecond = TimeSpan.FromMilliseconds((double)(get_reader.GetInt32(1)));
                    get_reader.Close();
                    con.Close();
                    //return null;
                    return total_day_hours_in_millisecond;

                }
                else
                {
                    con.Close();
                    //return null;
                    return total_day_hours_in_millisecond;
                }
            }
            catch (SqlException sqlE)
            {
                con.Close();
                //return null;
                return total_day_hours_in_millisecond;
            }
        }



        public string monthlyHoursForWorker(string user, string from_date, string to_date)
        {
            try
            {
                con.Open();
                SqlCommand get_cmd =
                new SqlCommand("SELECT username,date,time FROM " + signningTable
                    + " WHERE username = '" + user + "' AND date ='" + from_date + "' "
                    + "OR date='" + to_date + "' "
                    // + "GROUP BY username,date,time "
                    + "ORDER BY date ASC,time ASC ", con);


                SqlDataReader get_reader = get_cmd.ExecuteReader();

                TimeSpan s_in = new TimeSpan();
                TimeSpan s_out = new TimeSpan();
                TimeSpan total = new TimeSpan(0, 0, 0, 0, 0);

                if (get_reader.HasRows)
                {
                    while (get_reader.Read())
                    {
                        s_in = get_reader.GetTimeSpan(2);

                        get_reader.Read();
                        s_out = get_reader.GetTimeSpan(2);

                        total += (s_out - s_in);
                    }

                    get_reader.Close();
                    con.Close();
                    return total.ToString();
                }
                else
                {
                    con.Close();
                    return null;
                }
            }
            catch (SqlException sqlE)
            {
                con.Close();
                return null;
            }
        }

        public LinkedList<string> getAllActionLogs()
        {
            LinkedList<string> l = new LinkedList<string>();
            try
            {
                con.Open();
                SqlCommand get_cmd = new SqlCommand("SELECT date_time,action_log "
                    + "FROM " + actionLogsTable, con);

                SqlDataReader get_reader = get_cmd.ExecuteReader();

                if (get_reader.HasRows)
                {
                    while (get_reader.Read())
                    {
                        l.AddFirst(get_reader.GetDateTime(0).ToString() + " " + get_reader.GetString(1));
                    }
                    get_reader.Close();
                    con.Close();
                    return l;
                }
                con.Close();
                return l;
            }
            catch (SqlException sqlE)
            {
                con.Close();
            }
            return l;
        }


        public int addActionLog(string log_string)
        {
            try
            {
                con.Open();
                SqlCommand insert_cmd = new SqlCommand("INSERT INTO " + actionLogsTable +
              " VALUES(GETDATE() , "
              + "'" + log_string + "') ", con);

                insert_cmd.ExecuteNonQuery();
                con.Close();
                return 1;
            }
            catch (SqlException sqlE)
            {
                con.Close();
                return -1;
            }
        }

        //Returns 'true' if the 'username' is an admin user, or 'false' otherwise.
        public Boolean isAdmin(string username)
        {
            con.Open();
            SqlCommand sign_cmd = new SqlCommand(
                "SELECT admin FROM " + usersTable
                + " WHERE username ='" + username + "'", con);

            SqlDataReader sign_reader = sign_cmd.ExecuteReader();
            sign_reader.Read();
            if (sign_reader.HasRows && sign_reader.GetString(0).CompareTo("a") == 0)
            {
                sign_reader.Close();
                con.Close();
                return true;
            }
            else
            {
                sign_reader.Close();
                con.Close();
                return false;
            }
        }

        public string todayDateFormat()
        {
            //
            string day = DateTime.Today.Date.Day.ToString();
            string month = DateTime.Today.Date.Month.ToString();
            string year = DateTime.Today.Date.Year.ToString();


            if (day.Length < 2)
            {
                day = "0" + day;
            }
            if (month.Length < 2)
            {
                month = "0" + month;
            }

            return (year + "-" + day + "-" + month);
        }

        public string lastSignedToday(string username)
        {
            try
            {
                con.Open();
                SqlCommand sign_cmd = new SqlCommand(
                    "SELECT TOP 1 in_out, MAX(date) AS \"max_date\" , MAX(time) AS \"max_time\" FROM " + signningTable
                    + " WHERE username ='" + username
                    + "' AND date = " + "CONVERT(DATE, GETDATE(), 108) "
                    + "GROUP BY in_out "
                    + "ORDER BY \"max_time\" DESC ", con);

                SqlDataReader sign_reader = sign_cmd.ExecuteReader();

                if (sign_reader.HasRows)
                {
                    sign_reader.Read();
                    if (sign_reader.GetString(0).CompareTo("o") == 0)
                    {
                        sign_reader.Close();
                        con.Close();
                        return "OUT";
                    }
                    else
                    {
                        sign_reader.Close();
                        con.Close();
                        return "IN";
                    }
                }
                else
                {
                    sign_reader.Close();
                    con.Close();
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public Boolean isSignedOutToday(string username)
        {
            con.Open();
            SqlCommand sign_cmd = new SqlCommand(
                "SELECT TOP 1 in_out, MAX(date) AS \"max_date\" , MAX(time) AS \"max_time\" FROM " + signningTable
                + " WHERE username ='" + username + "' AND date ='" + DateTime.Today.Date.ToString()
                + "' GROUP BY in_out "
                + "ORDER BY \"max_time\" DESC ", con);

            SqlDataReader sign_reader = sign_cmd.ExecuteReader();
            sign_reader.Read();
            if (sign_reader.HasRows && sign_reader.GetString(0).CompareTo("o") == 0)
            {

                sign_reader.Close();
                con.Close();
                return true;
            }
            else
            {
                sign_reader.Close();
                con.Close();
                return false;
            }
        }

        public String lastSigned(string username)
        {
            con.Open();
            
            /*string c = "SELECT TOP 1 in_out, MAX(date) AS \"max_date\" , MAX(time) AS \"max_time\" FROM " + signningTable
                + " WHERE username ='" + username + "'"
                + " GROUP BY in_out"
                + " ORDER BY \"max_date\" DESC , \"max_time\" DESC "
                + " HAVING max(date) as \"max_date\" , max(time) as \"max_time";
             * */

            SqlCommand sign_cmd = new SqlCommand(
                "SELECT TOP 1 in_out, MAX(date) AS \"max_date\" , MAX(time) AS \"max_time\" FROM " + signningTable
                + " WHERE username ='" + username + "'"
                + " GROUP BY in_out"
                + " ORDER BY \"max_date\" DESC , \"max_time\" DESC ", con);

            //SqlCommand sign_cmd = new SqlCommand( c, con);

            SqlDataReader sign_reader = sign_cmd.ExecuteReader();
            
            String ret_string = "";
            if (sign_reader.HasRows)
            {
                sign_reader.Read();
                String status;
                if (sign_reader.GetString(0).CompareTo("i") == 0)
                { status = "IN"; }
                else { status = "OUT"; } //sign_reader.GetDataTypeName(2)
                DateTime date = new DateTime(sign_reader.GetDateTime(1).Year, sign_reader.GetDateTime(1).Month, sign_reader.GetDateTime(1).Day, sign_reader.GetTimeSpan(2).Hours, sign_reader.GetTimeSpan(2).Minutes, sign_reader.GetTimeSpan(2).Seconds, sign_reader.GetTimeSpan(2).Milliseconds);
                //ret_string = "Last Signed at: " + sign_reader.GetDateTime(1).TimeOfDay.Add(sign_reader.GetTimeSpan(2)) + " " + sign_reader.GetTimeSpan(2) + " , Status = " + status;
                ret_string = "Last Signed at: " + date + " , Status = " + status;

            }
            else
            {
                ret_string = " First Signning in! ";
            }
            sign_reader.Close();
            con.Close();
            return ret_string;
        }


        public int deleteReportAtDate(string day,string month, string year)
        {
            int affected_rows = 0;
            try
            {
                con.Open();
                string c = "DELETE FROM " + reportsTable
                     + " WHERE DAY(date_time) = " + day + " AND MONTH(date_time) = " + month + " AND YEAR(date_time) = " + year;

                SqlCommand delete_cmd = new SqlCommand(c, con);
                affected_rows = delete_cmd.ExecuteNonQuery();
            }
            catch (SqlException sqlE)
            {
                con.Close();
                return affected_rows;
            }
            con.Close();
            return affected_rows;
        }

        public int add_report(string report_text)
        {

            try
            {
                con.Open();
                SqlCommand insert_cmd = new SqlCommand("INSERT INTO " + reportsTable +
                " VALUES(GETDATE() , '"
                + report_text + "') ", con);
                insert_cmd.ExecuteNonQuery();
                con.Close();
                return 1;
            }
            catch (SqlException sqlE)
            {
                return 0;
            }
        }

        public Boolean isSignRecordExists(string username)
        {
            con.Open();
            SqlCommand sign_cmd = new SqlCommand("Select * FROM " + signningTable + " WHERE username ='" + username + "'", con);
            SqlDataReader sign_reader = sign_cmd.ExecuteReader();
            sign_reader.Read();
            if (sign_reader.HasRows)
            {
                sign_reader.Close();
                con.Close();
                return true;
            }
            else
            {
                sign_reader.Close();
                con.Close();
                return false;
            }
        }

        public Boolean checkValidation(string username, string password)
        {
            con.Open();
            SqlCommand sign_cmd = new SqlCommand("Select * FROM " + usersTable
                + " WHERE username ='" + username + "' AND password ='" + password + "'", con);
            SqlDataReader sign_reader = sign_cmd.ExecuteReader();
            sign_reader.Read();
            if (sign_reader.HasRows)
            {
                sign_reader.Close();
                con.Close();
                return true;
            }
            else
            {
                sign_reader.Close();
                con.Close();
                return false;
            }
        }

        //Insert values into Signning table. (username, time and signning type (in/out) ).
        public int insertToSignning(string username, string type)
        {

            try
            {
                con.Open();

                string c = "INSERT INTO " + signningTable
                    + " VALUES('" + username + "' , "
                    + "CONVERT(DATE, GETDATE(), 108)"
                    + " ,  CONVERT(TIME, GETDATE(), 108)" + " , '" + type + "') ";

                //SqlCommand insert_cmd = new SqlCommand(
                SqlCommand insert_cmd = new SqlCommand(c, con);
                /*"INSERT INTO " + signningTable 
                    + " VALUES('" + username + "' , '" 
                    + DateTime.Now.Date.ToString().Substring(0,DateTime.Now.Date.ToString().IndexOf(" ")) + "' , '" + DateTime.Now.TimeOfDay.ToString() + "' , '" + type + "') ", con);
                
                                                           * */
                insert_cmd.ExecuteNonQuery();
                con.Close();
                return 1;
            }
            catch (SqlException sqlE)
            {
                throw sqlE;
            }
        }

        public int updateManagementDetails(int minMonthlyH, int maxMonthlyH, float hour_salary, float extra_hour_salary)
        {
            try
            {
                con.Open();
                SqlCommand update_cmd =
                new SqlCommand(" UPDATE " + managementTable +
                    " SET[min_monthly_hours]=[" + minMonthlyH + "], "
                    + "[max_monthly_hours] = [" + maxMonthlyH + "], "
                     + "[hour_salary] = [" + hour_salary + "], "
                      + "[extra_hours_salary] = [" + extra_hour_salary + "]"
                      , con);

                update_cmd.ExecuteNonQuery();
                con.Close();
                return 1;
            }
            catch (SqlException sqlE)
            {
                throw sqlE;
            }
        }


        public int updateMinMonthlyHours(string minMonthlyH)
        {
            int res = -1;
            try
            {
                con.Open();
                SqlCommand update_cmd =
                new SqlCommand("UPDATE " + managementTable
                    + " SET [min_monthly_hours] = " + minMonthlyH
                    , con);

                res = update_cmd.ExecuteNonQuery();
                con.Close();
                return res;
            }
            catch (SqlException sqlE)
            {
                con.Close();
                return res;
            }
        }

        public int updateMaxMonthlyHours(string maxMonthlyH)
        {
            int res = -1;
            try
            {
                con.Open();
                SqlCommand update_cmd =
                new SqlCommand("UPDATE " + managementTable
                    + " SET [max_monthly_hours] = " + maxMonthlyH
                    , con);

                res = update_cmd.ExecuteNonQuery();
                con.Close();
                return res;
            }
            catch (SqlException sqlE)
            {
                con.Close();
                return res;
            }
        }

        public int updateHourSalary(string hourSal)
        {
            int res = -1;
            try
            {
                con.Open();
                SqlCommand update_cmd =
                new SqlCommand("UPDATE " + managementTable
                    + " SET [hour_salary] = " + hourSal
                    , con);

                res = update_cmd.ExecuteNonQuery();
                con.Close();
                return res;
            }
            catch (SqlException sqlE)
            {
                con.Close();
                return res;
            }
        }
        public int updateExtraHourSalary(string extraHourSal)
        {
            int res = -1;
            try
            {
                con.Open();
                SqlCommand update_cmd =
                new SqlCommand("UPDATE " + managementTable
                    + " SET [extra_hours_salary] = " + extraHourSal
                    , con);

                res = update_cmd.ExecuteNonQuery();
                con.Close();
                return res;
            }
            catch (SqlException sqlE)
            {
                con.Close();
                return res;
            }
        }



        public int insertNewEmployeeToTable(string id, string fname, string lname, string username, string password)
        {
            if (!isIDExists(id))
            {
                try
                {
                    return insertNewEmployee(id, fname, lname, username, password);

                }
                catch (Exception exception)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }

        }

        public int inserToUsersTable(string username, string password, bool admin)
        {
            if (!isUsernameExists(username))
            {
                if (admin)
                    return inserToUsers(username, password, "a");
                else
                    return inserToUsers(username, password, "r");
            }
            else
            {
                return -1;
            }
        }

        public int inserToUsers(string username, string password, string admin)
        {
            try
            {
                con.Open();
                SqlCommand insert_cmd = new SqlCommand("INSERT INTO " + usersTable +
                " VALUES('" + username + "' , '" + password + "' , '" + admin + "') "
                , con);
                insert_cmd.ExecuteNonQuery();
                con.Close();
                return 1;
            }
            catch (SqlException sqlE)
            {
                throw sqlE;
                return -1;
            }

        }

        public int insertNewEmployee(string id, string fname, string lname, string username, string password)
        {
            try
            {
                con.Open();
                SqlCommand insert_cmd = new SqlCommand("INSERT INTO " + employeesTable +
                " VALUES('" + id + "' , '" + fname + "' , '" + lname + "' , '" + username + "' , '" + password
                + "') ", con);
                insert_cmd.ExecuteNonQuery();
                con.Close();
                return 1;
            }
            catch (SqlException sqlE)
            {
                throw sqlE;
            }
        }


        public Boolean isIDExists(string id)
        {
            try
            {
                con.Open();
                SqlCommand get_cmd =
                new SqlCommand("SELECT * FROM " + employeesTable
                    + " WHERE Id = '" + id + "'"
                    , con);

                SqlDataReader get_reader = get_cmd.ExecuteReader();
                get_reader.Read();
                if (get_reader.HasRows)
                {
                    get_reader.Close();
                    con.Close();
                    return true;
                }
            }
            catch (SqlException sqlE)
            {
                con.Close();
                return false;
            }
            con.Close();

            return false;



        }




        public string getEmployeeStringByUsername(string username)
        {
            string ret_string = "";
            try
            {
                con.Open();
                SqlCommand get_cmd =
                new SqlCommand("SELECT Id,first_name,last_name,username,password FROM " + employeesTable
                    + " WHERE username = '" + username + "'"
                    , con);

                SqlDataReader get_reader = get_cmd.ExecuteReader();
                if (get_reader.HasRows)
                {
                    get_reader.Read();
                    // return get_reader.GetInt32(0).ToString();// get_reader.GetDataTypeName(0).ToString();
                    ret_string = get_reader.GetInt32(0).ToString() + " "
                        + get_reader.GetString(1).ToString() + " "
                        + get_reader.GetString(2).ToString() + " "
                        + get_reader.GetString(3).ToString() + " "
                        + get_reader.GetString(4).ToString();

                    get_reader.Close();
                    con.Close();
                    return ret_string;
                }
            }
            catch (SqlException sqlE)
            {
                con.Close();
                //return ret_string;
            }
            con.Close();
            return ret_string;
        }


        public LinkedList<string> getReportsAtDate(string day,string month, string year)
        {
            LinkedList<string> l = new LinkedList<string>();
            try
            {
                if (month.Length < 2)
                {
                    month = "0" + month;
                }
                con.Open();
                SqlCommand get_cmd =
                new SqlCommand("SELECT shift_report FROM " + reportsTable
                    + " WHERE DAY(date_time) = " + day + " AND MONTH(date_time) = " + month + " AND YEAR(date_time) = " + year
                    + " ORDER BY date_time ASC"
                    , con);

                SqlDataReader get_reader = get_cmd.ExecuteReader();
                if (get_reader.HasRows)
                {
                    while (get_reader.Read())
                    {
                        l.AddFirst(get_reader.GetString(0));
                    }

                    get_reader.Close();

                }
            }
            catch (SqlException sqlE)
            {
                con.Close();
                return l;
            }
            con.Close();
            return l;
        }


        public LinkedList<string> allUsersAtShift(string fromH, string toH)
        {
            LinkedList<string> l = new LinkedList<string>();

            try
            {
                con.Open();
                /*
                SELECT Signning.username, MAX(Signning.time) as s_time
                FROM Signning
                WHERE in_out='i' AND date = CONVERT(DATE, GETDATE(), 108) AND Signning.time BETWEEN CONVERT(TIME, '18:00', 108) AND CONVERT(TIME, '18:30', 108) 
                GROUP BY username,in_out
                ORDER BY "s_time" DESC
                 

           string c = "SELECT " + signningTable + ".username, MAX(" + signningTable + ".time) as \"s_time\" "
            + " FROM " + signningTable
            + " WHERE in_out='i' AND date = CONVERT(DATE, GETDATE(), 108) AND "
            + signningTable + ".time BETWEEN CONVERT(TIME, '" + fromH + "', 108) AND CONVERT(TIME, '" + toH + "', 108) "
            + " GROUP BY username,in_out "
            + "ORDER BY \"s_time\" DESC";*/

                
                string c = "select t1.username "
+ " from (select username,MAX(time) as \"s_time\" , in_out "
      + " from " + signningTable
        + " where in_out = 'i' and date = CONVERT(date,getdate(),108) "
        + " group by username,in_out "
        + " having MAX(time) between CONVERT(time,'" + fromH + "'" + ",108) and CONVERT(time,'" + toH + "'" + ",108) ) as \"t1\" , "

+ " (select username,MAX(time) as \"s_time\",in_out "
+ " from " + signningTable
+ " where in_out = 'o' and date = CONVERT(date,getdate(),108) "
+ " group by username,in_out) as \"t2\" "


+ " where (t1.s_time > t2.s_time and t1.username = t2.username) "

+ " group by t1.username "

    ;
                
                
              

                /*
                select Signning.username
from Signning, (select username from Signning where in_out = 'i' group by username)  as t1
where Signning.username not in (t1.username)
group by Signning.username

                */
                SqlCommand get_cmd = new SqlCommand(c, con);
                SqlDataReader get_reader = get_cmd.ExecuteReader();

                if (get_reader.HasRows)
                {
                    while (get_reader.Read())
                    {
                        l.AddFirst(get_reader.GetString(0));
                    }

                    get_reader.Close();
                }
                con.Close();
                return l;
            }
            catch (Exception e)
            {
                return l;
            }
        }

        public TimeSpan mymonthlyHoursForWorker(string user, string month, string year)
        {
            TimeSpan total = new TimeSpan(0, 0, 0, 0, 0);
            try
            {
                string day;
                for (int i = 1; i <= 30; i++)
                {
                    day = i.ToString();
                    if (i <= 9)
                    {
                        day = "0" + i;
                    }
                    total += dailyHoursForWorker(user, year + "-" + day + "-" + month);
                }
                return total;
            }
            catch (SqlException sqlE)
            {
                con.Close();
                return total;
            }
        }


        public string getAllUsername()
        {
            string ret_string = "";
            try
            {
                con.Open();
                SqlCommand get_cmd =
                new SqlCommand("SELECT username FROM " + usersTable, con);

                SqlDataReader get_reader = get_cmd.ExecuteReader();
                if (get_reader.HasRows)
                {
                    while (get_reader.Read() && (ret_string += get_reader.GetString(0).ToString() + " ").Length > 0) ;


                    get_reader.Close();
                    con.Close();
                    return ret_string;
                }
            }
            catch (SqlException sqlE)
            {
                con.Close();
                //return ret_string;
            }
            con.Close();
            return ret_string;
        }

        public Boolean isUsernameExists(string username)
        {
            try
            {
                con.Open();
                SqlCommand get_cmd =
                new SqlCommand("SELECT * FROM " + usersTable
                    + " WHERE username = '" + username + "'"
                    , con);
                SqlDataReader get_reader = get_cmd.ExecuteReader();
                get_reader.Read();
                if (get_reader.HasRows)
                {
                    get_reader.Close();
                    con.Close();
                    return true;
                }
            }
            catch (SqlException sqlE)
            {
                con.Close();
                return false;
            }
            con.Close();

            return false;



        }
        /*
                public int updateMaxMonthlyHours(string maxMonthlyH)
                {
                    int res = -1;
                    try
                    {
                        con.Open();
                        SqlCommand update_cmd =
                        new SqlCommand("UPDATE " + managementTable
                            + " SET [max_monthly_hours] = " + maxMonthlyH
                            , con);

                        res = update_cmd.ExecuteNonQuery();
                        con.Close();
                        return res;
                    }
                    catch (SqlException sqlE)
                    {
                        con.Close();
                        return res;
                    }
                }
        
                public int updateHourSalary(string hour_salary)
                {
                    int res = -1;
                    try
                    {
                        con.Open();
                        SqlCommand update_cmd =
                        new SqlCommand("UPDATE " + managementTable
                            + " SET [hour_salary] = " + hour_salary
                            , con);

                        res = update_cmd.ExecuteNonQuery();
                        con.Close();
                        return res;
                    }
                    catch (SqlException sqlE)
                    {
                        con.Close();
                        return res;
                    }
                }

                public int updateExtraHourSalary(string extra_hours_salary)
                {
                    int res = -1;
                    try
                    {
                        con.Open();
                        SqlCommand update_cmd =
                        new SqlCommand("UPDATE " + managementTable
                            + " SET [extra_hours_salary] = " + extra_hours_salary
                            , con);

                        res = update_cmd.ExecuteNonQuery();
                        con.Close();
                        return res;
                    }
                    catch (SqlException sqlE)
                    {
                        con.Close();
                        return res;
                    }
                }
                */
        public Boolean getManagementDetails(ref double minMonthlyH, ref double maxMonthlyH, ref double hour_salary, ref double extra_hour_salary)
        {
            try
            {
                con.Open();
                SqlCommand get_cmd =
                new SqlCommand("SELECT * FROM " + managementTable, con);
                SqlDataReader get_reader = get_cmd.ExecuteReader();
                get_reader.Read();
                if (get_reader.HasRows)
                {

                    minMonthlyH = get_reader.GetInt32(0);

                    maxMonthlyH = get_reader.GetInt32(1);

                    //Console.WriteLine("FLOAAATT " + get_reader.GetFieldType(0).ToString());
                    //return get_reader.GetFieldType(2).ToString();
                    hour_salary = get_reader.GetDouble(2);
                    // return get_reader.GetFieldType(2).ToString();
                    extra_hour_salary = get_reader.GetDouble(3);

                    get_reader.Close();
                    con.Close();
                    return true;
                }
            }
            catch (SqlException sqlE)
            {

                con.Close();
                return false;
            }
            con.Close();
            //return null;
            return false;
        }

    }
}