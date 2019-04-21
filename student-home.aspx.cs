using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class home : System.Web.UI.Page
{ 
//{
//    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
//    db_context dbContext = new db_context();
//    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        try
//        {
//            //Check sessions of uid and dheyaemail of user
//            if (Session["uid"] != null && Session["dheyaEmail"] != null)
//            {
//                //CDF own referrals month-wise
//                chart_bind();
//                //All CDF referrals month-wise
//                chart_bind2();
//                //CDF record count
//                bindgrid();
//                //News Feed Method
//                newsFeed();
//                //New Updates from Dheya (individual)
//                dheyaUpdates();

//                string constr = ConfigurationManager.ConnectionStrings["CRM_ConnectionString"].ConnectionString;
//                using (MySqlConnection con = new MySqlConnection(constr))
//                {
//                    DataTable dt2 = new DataTable();
//                    string strcmd1 = "SELECT count(id) FROM suitecrm.leads as A inner join suitecrm.leads_cstm as B on A.id=B.id_c where A.refered_by='" + Session["dheyaEmail"].ToString() + "' and deleted=0";
//                    con.Open();
//                    MySqlCommand cmd = new MySqlCommand(strcmd1, con);
//                    MySqlDataReader dr1 = cmd.ExecuteReader();
//                    //Check if table has rows for required query
//                    if (dr1.HasRows)
//                    {
//                        dr1.Read();
//                        Int32 s = dr1.GetInt32(0);
//                        leads_count.Text = " " + s;
//                        //session_count.Text = " " + s * 0;
//                        // revenue_count.Text = "" + s * 0;
//                    }
//                }
//                using (SqlConnection connection = new SqlConnection(connectionString))
//                {
//                    connection.Open();
//                    //Check payment status of respective user from tblPayment table
//                    string query_visit = "  select COUNT(log_id) as count from tblLog where uId='" + Session["uid"].ToString() + "' and log_type = 'in'";
//                    SqlCommand cmdvisit = new SqlCommand(query_visit, connection);
//                    visits_count.Text = cmdvisit.ExecuteScalar().ToString();


//                    try
//                    {
//                        string queryLastVisit = " SELECT TOP 1 log_time FROM (select top 2 log_time, log_id from tblLog where log_type = 'in' and uId = '" + Session["uid"].ToString() + "' order by log_id desc)t ORDER BY log_id";
//                        SqlCommand cmdLastVisit = new SqlCommand(queryLastVisit, connection);
//                        string date1 = cmdLastVisit.ExecuteScalar().ToString();
//                        DateTime dt = Convert.ToDateTime(date1);
//                        visits_count.ToolTip = "Last Vist: " + dt.ToString("dd'/'MM'/'yyyy HH:mm:ss tt");
//                    }
//                    catch (Exception ex)
//                    {
//                        Log.Error(ex);
//                    }

//                    int uid = Convert.ToInt32(HttpContext.Current.Session["uid"].ToString());


//                    string str = ""
//                        + "select count(uId) FROM tblUserMaster where uId=" + uid + ";"
//                        + "select count(uId)FROM tblUserDetails where uId = " + uid + ";"
//                        + "select count(DISTINCT uId) FROM tblEducation where uId = " + uid + ";"
//                        + "select count(DISTINCT uId) FROM tblExperience where uId = " + uid + "";

//                    SqlDataAdapter da = new SqlDataAdapter(str, connection);
//                    DataSet ds = new DataSet();
//                    da.Fill(ds);
//                    double count = 0;
//                    double total = 4;
//                    double profileComplete = 0;
//                    //profile complete status
//                    for (int i = 0; i < total; i++)
//                    {
//                        count = count + Convert.ToInt32(ds.Tables[i].Rows[0][0]);
//                    }
//                    profileComplete = (count / total) * 100;
//                    profilec.Text = profileComplete.ToString();
//                }
//            }
//            else
//            {
//                Response.Redirect("~/login.aspx", false);
//            }
//        }
//        catch (Exception ex)
//        {
//            Log.Error(ex);
//        }
//    }
//    //Get data for referrals (own)
//    private DataTable GetData()
//    {
//        try
//        {
//            string constr1 = ConfigurationManager.ConnectionStrings["CRM_ConnectionString"].ConnectionString;
//            using (MySqlConnection conn = new MySqlConnection(constr1))
//            {
//                DataTable dt = new DataTable();
//                // conn.Open();
//                string cmd = "Select count(id) as Count, MONTHNAME(date_entered) AS Month From suitecrm.leads where deleted = 0 and refered_by ='" + Session["dheyaEmail"].ToString() + "' AND Year(date_entered) = YEAR(CURDATE()) group by MONTHNAME(date_entered) order by MONTH(date_entered) asc";
//                MySqlDataAdapter adp = new MySqlDataAdapter(cmd, conn);
//                adp.Fill(dt);

//                return dt;
//            }
//        }
//        catch (Exception ex)
//        {
//            Log.Error(ex);
//            return null;
//        }
//    }
//    //Get data for referrals (All)
//    private DataTable GetData2()
//    {
//        try
//        {
//            string constr2 = ConfigurationManager.ConnectionStrings["CRM_ConnectionString"].ConnectionString;
//            using (MySqlConnection conn = new MySqlConnection(constr2))
//            {
//                DataTable dt = new DataTable();
//                // conn.Open();
//                string cmd = "Select count(id) *5 as Count, MONTHNAME(date_entered) AS Month From suitecrm.leads where deleted = 0 and lead_source = '5' AND Year(date_entered) = YEAR(CURDATE()) group by MONTHNAME(date_entered) order by MONTH(date_entered) asc";
//                MySqlDataAdapter adp = new MySqlDataAdapter(cmd, conn);
//                adp.Fill(dt);

//                return dt;
//            }
//        }
//        catch (Exception ex)
//        {
//            Log.Error(ex);
//            return null;
//        }
//    }
//    //CDF own referrals month-wise
//    private void chart_bind()
//    {
//        StringBuilder str = new StringBuilder();
//        DataTable dt = new DataTable();
//        try
//        {
//            dt = GetData();
//            str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*,*bar*]});
//            google.setOnLoadCallback(drawChart);
//            function drawChart() {
//            var data = new google.visualization.DataTable();
//            data.addColumn('string', 'Month');
//            data.addColumn('number', 'Count');

//            data.addRows(" + dt.Rows.Count + ");");

//            for (int i = 0; i <= dt.Rows.Count - 1; i++)
//            {
//                str.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt.Rows[i]["Month"].ToString() + "');");
//                str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["Count"].ToString() + ") ;");

//            }
//            str.Append("var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));");
//            str.Append("chart.draw(data, {width: '95%', height: 220, curveType: 'function',");
//            str.Append("legend: 'none',");
//            str.Append("hAxis: {title: 'Year', titleTextStyle: {color: 'green'}},fontSize: 12,");
//            str.Append("vAxis: {title: 'Count', titleTextStyle: {color: 'green'}}");
//            str.Append("}); }");
//            str.Append("</script>");
//            lt.Text = str.ToString().Replace('*', '"');
//        }
//        catch (Exception ex)
//        {
//            Log.Error(ex);
//        }
//    }
//    //All CDF referrals month-wise
//    private void chart_bind2()
//    {
//        StringBuilder str = new StringBuilder();
//        DataTable dt = new DataTable();
//        try
//        {
//            dt = GetData2();
//            str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*,*bar*]});
//            google.setOnLoadCallback(drawChart);
//            function drawChart() {
//            var data = new google.visualization.DataTable();
//            data.addColumn('string', 'Month');
//            data.addColumn('number', 'Count');

//            data.addRows(" + dt.Rows.Count + ");");

//            for (int i = 0; i <= dt.Rows.Count - 1; i++)
//            {
//                str.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt.Rows[i]["Month"].ToString() + "');");
//                str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["Count"].ToString() + ") ;");

//            }
//            str.Append("   var chart = new google.visualization.ColumnChart(document.getElementById('chart_div2'));");
//            str.Append(" chart.draw(data, {width: '95%', height: 220, curveType: 'function',");
//            str.Append("legend: 'none',");
//            str.Append("hAxis: {title: 'Year', titleTextStyle: {color: 'green'}},fontSize: 12,");
//            str.Append("vAxis: {title: 'Count', titleTextStyle: {color: 'green'}}");
//            str.Append("}); }");
//            str.Append("</script>");
//            lt2.Text = str.ToString().Replace('*', '"');
//        }
//        catch (Exception ex)
//        {
//            Log.Error(ex);
//        }
//    }
//    //CDF records
//    [WebMethod(EnableSession = true)]
//    public static List<object> GetChartData()
//    {
//        try
//        {
//            if (HttpContext.Current.Session["uid"] != null && HttpContext.Current.Session["dheyaEmail"] != null)
//            {
//                int uid = Convert.ToInt32(HttpContext.Current.Session["uid"].ToString());

//                string connectionString1 = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
//                using (SqlConnection con = new SqlConnection(connectionString1))
//                {

//                    string str = ""
//                        + "select count(uId) FROM tblUserMaster where uId=" + uid + ";"
//                        + "select count(uId)FROM tblUserDetails where uId = " + uid + ";"
//                        + "select count(DISTINCT uId) FROM tblEducation where uId = " + uid + ";"
//                        + "select count(DISTINCT uId) FROM tblExperience where uId = " + uid + "";

//                    SqlDataAdapter da = new SqlDataAdapter(str, con);
//                    DataSet ds = new DataSet();
//                    da.Fill(ds);

//                    double count = 0;
//                    double total = 4;
//                    double profileComplete = 0;
//                    double profileInComplete = 0;
//                    for (int i = 0; i < total; i++)
//                    {
//                        count = count + Convert.ToInt32(ds.Tables[i].Rows[0][0]);
//                    }

//                    profileComplete = (count / total) * 100;
//                    profileInComplete = 100 - profileComplete;

//                    List<object> chartData = new List<object>();
//                    chartData.Add(new object[]
//                    {"Profile", "Status" });
//                    chartData.Add(new object[]
//                        {"Complete",profileComplete});
//                    chartData.Add(new object[]
//                       {"In Complete",profileInComplete});
//                    return chartData;
//                }
//            }
//            else
//            {
//                HttpContext.Current.Response.Redirect("~/login.aspx", false);
//                return null;
//            }
//        }
//        catch (Exception ex)
//        {
//            Log.Error(ex);
//            return null;
//        }
//    }
//    //CDF record count
//    private void bindgrid()
//    {
//        try
//        {
//            string constr = ConfigurationManager.ConnectionStrings["CRM_ConnectionString"].ConnectionString;
//            using (MySqlConnection con = new MySqlConnection(constr))
//            {
//                string strcmd = "select first_name as 'First Name',last_name as 'Last Name',phone_mobile as 'Contact Number',ea.email_address as 'Email Address',date_entered as 'Date',city_c as 'City',lead_source as 'Lead Source',status as 'Lead Status', cust.lead_category_c as 'Lead Category',lead_source_description as 'Description' FROM suitecrm.leads left join suitecrm.leads_cstm cust on cust.id_c=suitecrm.leads.id  LEFT JOIN suitecrm.email_addr_bean_rel eabl  ON eabl.bean_id = leads.id AND eabl.deleted=0 LEFT JOIN suitecrm.email_addresses ea ON (ea.id = eabl.email_address_id) and ea.deleted=0 where suitecrm.leads.refered_by='" + Session["dheyaEmail"].ToString() + "' and suitecrm.leads.deleted=0";
//                MySqlDataAdapter da = new MySqlDataAdapter(strcmd, con);
//                DataSet ds = new DataSet();
//                da.Fill(ds);

//                if (ds.Tables[0].Rows.Count > 0)
//                {
//                    GridView1.DataSource = ds;
//                    GridView1.DataBind();
//                    div_leadstatus.Visible = false;
//                }
//                else
//                {
//                    div_leadstatus.Visible = true;
//                    div_leadstatus.Attributes["class"] = "alert alert-warning";
//                    div_leadstatus.InnerText = "Currently you don't have any referral lead status";
//                    //DataTable dt = new DataTable();
//                    //dt.Columns.Add("First Name");
//                    //dt.Columns.Add("Last Name");
//                    //dt.Columns.Add("Date");
//                    //dt.Columns.Add("Lead Status");
//                    //dt.Columns.Add("Description");
//                    //ds.Tables[0].Rows.Add("", "", null, "","");

//                    //GridView1.DataSource = ds;
//                    //GridView1.DataBind();
//                    //GridView1.DataSource = null;
//                    //GridView1.DataBind();
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            Log.Error(ex);
//        }
//    }

//    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
//    {
//        GridView1.PageIndex = e.NewPageIndex;
//        bindgrid();
//    }

//    private void newsFeed()
//    {
//        try
//        {
//            using (SqlConnection connection1 = new SqlConnection(connectionString))
//            {
//                string query_newsfeed = "Select top 3 title,description,DateDiff(day, dateCreated, (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30'))) as days From tblNewsFeed order by dateCreated desc";
//                SqlCommand command = new SqlCommand(query_newsfeed, connection1);
//                connection1.Open();
//                SqlDataReader sdr = command.ExecuteReader();
//                if (sdr.HasRows)
//                {
//                    int i = 1;
//                    while (sdr.Read())
//                    {
//                        if (i == 1)
//                        {
//                            lbl_title1.Text = sdr["title"].ToString();
//                            lbl_content1.Text = sdr["description"].ToString();
//                            lbl_hr1.Text = sdr["days"].ToString();
//                            i++;
//                        }
//                        else if (i == 2)
//                        {
//                            lbl_title2.Text = sdr["title"].ToString();
//                            lbl_content2.Text = sdr["description"].ToString();
//                            lbl_hr2.Text = sdr["days"].ToString();
//                            i++;
//                        }
//                        else
//                        {
//                            lbl_title3.Text = sdr["title"].ToString();
//                            lbl_content3.Text = sdr["description"].ToString();
//                            lbl_hr3.Text = sdr["days"].ToString();

//                        }
//                    }
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            Log.Error(ex);
//        }
//    }

//    private void dheyaUpdates()
//    {
//        try
//        {
//            lblDheyaUpdates.Attributes.Add("readonly", "readonly");
//            using (SqlConnection con = new SqlConnection(connectionString))
//            {
//                string querydheyaUpdates = "Select uId,dheyaUpdates From tblUserDetails where uId = " + Session["uid"] + "";
//                SqlCommand command = new SqlCommand(querydheyaUpdates, con);
//                con.Open();
//                SqlDataReader dr = command.ExecuteReader();
//                if (dr.HasRows)
//                {
//                    dr.Read();
//                    if (dr["dheyaUpdates"] != DBNull.Value)
//                    {
//                        lblDheyaUpdates.Text = dr["dheyaUpdates"].ToString();
//                    }
//                    else
//                    {
//                        lblDheyaUpdates.Text = "No updates found.";
//                    }
//                }                          
//            }
            
//        }
//        catch (Exception ex)
//        {
//            Log.Error(ex);
//        }
//    }
}
