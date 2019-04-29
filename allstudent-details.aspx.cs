using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class allstudent_details : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    int userId;
    db_context dbContext = new db_context();
    string constr = ConfigurationManager.ConnectionStrings["MySql_ConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        int professorId = Convert.ToInt32(Session["userId"]);
        if (!this.IsPostBack)
        {
            //DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Id"), new DataColumn("Name"), new DataColumn("Country") });
            //dt.Rows.Add(1, "John Hammond", "United States");
            //dt.Rows.Add(2, "Mudassar Khan", "India");
            //dt.Rows.Add(3, "Suzanne Mathews", "France");
            //dt.Rows.Add(4, "Robert Schidner", "Russia");
            //GridView1.DataSource = dt;
            UserDetails();
            BindGridView();
            try
            {
                // Get user in using QueryString 
                userId = Convert.ToInt32(Request.QueryString["id"]);

            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                //Print Error msg when query string is not correct format 
                //divErr.Visible = true;
                //divErr.Attributes["class"] = "alert alert-danger";
                //divErr.InnerText = "Query string is not correct format..!!!";
            }



            


        }
    }

    protected void UserDetails()
    {
        try
        {
            userId = Convert.ToInt32(Request.QueryString["id"]);
            using (MySqlConnection connection = new MySqlConnection(constr))
            {
                
                string strcmd = "SELECT A.userId,  A.userType, A.fName, A.lName, A.emailId, A.password, A.streetNo, A.city, A.state, A.zipcode, A.contactNo, C.courseId, C.courseNumber, C.courseName, C.courseLevel, C.term, D.deptName, E.AcademicLevel from tbluser as A " +
                    "LEFT OUTER JOIN tblregistration as B on B.userId = A.userId " +
                    "LEFT OUTER JOIN tblcourse as C on C.courseId = B.courseId " +
                    "LEFT OUTER JOIN tbldepartment as D on D.deptId = C.deptId " +
                    "LEFT OUTER JOIN tblstudent as E on E.userId = A.userId " +
                    "WHERE A.userType = 1  AND A.userId = " + userId + "";

                MySqlCommand cmd = new MySqlCommand(strcmd, connection);
                connection.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    {
                        lblName.Text = dr["fName"].ToString() + " " + dr["lName"].ToString();
                        lblUsertype.Text = dr["userType"].ToString();
                        lblEmailId.Text = dr["emailId"].ToString();
                        lblPassword.Text = dr["password"].ToString();
                        lblAcademicLevel.Text = dr["AcademicLevel"].ToString();
                        lblStreetNo.Text = dr["streetNo"].ToString();
                        lblState.Text = dr["state"].ToString();
                        lblCity.Text = dr["city"].ToString();
                        lblZipcode.Text = dr["zipcode"].ToString();
                        lblContactNo.Text = dr["contactNo"].ToString();                        
                        lblCourseNumber.Text = dr["courseNumber"].ToString();
                        lblCourseName.Text = dr["courseName"].ToString();
                        lblCourseLevel.Text = dr["courseLevel"].ToString();
                        lblTerm.Text = dr["term"].ToString();
                        
                    }
                    dr.Close();
                }
                else
                {
                    //display when data is not fount        
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerText = "No record found..!!!";
                    //btn_editInfo.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong. Please Try again.";
            //btn_editInfo.Visible = false;
        }

    }

    protected void gridCourseStatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "false")
        {
            using (MySqlConnection connection = new MySqlConnection(constr))
            {
                connection.Open();
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string uid = commandArgs[0];
                string courseId = commandArgs[1];
                int count = 0;
                string strcmd1 = "update tblregistration set courseStatus = 1 where userId = @userId and courseId = @courseId";
                MySqlCommand cmd = new MySqlCommand(strcmd1, connection);
                cmd.Parameters.AddWithValue("@userId", uid);
                cmd.Parameters.AddWithValue("@courseId", courseId);
                count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                UserDetails();
                    BindGridView();
                }
            }


        }
        else
        {
            using (MySqlConnection connection = new MySqlConnection(constr))
            {
                connection.Open();
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string uid = commandArgs[0];
                string courseId = commandArgs[1];
                int count = 0;
                string strcmd1 = "update tblregistration set courseStatus = 0 where userId = @userId and courseId = @courseId";
                MySqlCommand cmd = new MySqlCommand(strcmd1, connection);
                cmd.Parameters.AddWithValue("@userId", uid);
                cmd.Parameters.AddWithValue("@courseId", courseId);
                count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                UserDetails();
                    BindGridView();
                }
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int professorId = Convert.ToInt32(Session["userId"]);
            using (MySqlConnection connection = new MySqlConnection(constr))
            {


                int count = 0;
                string strInsert = "INSERT INTO tblnotofication (professorId, studentId, message) values (@professorId, @userId, @message)";
                MySqlCommand cmd2 = new MySqlCommand(strInsert, connection);
                cmd2.Parameters.AddWithValue("@userId", userId);
                cmd2.Parameters.AddWithValue("@professorId", professorId);
                cmd2.Parameters.AddWithValue("@message", txtMessage.Text);
                connection.Open();
                count = cmd2.ExecuteNonQuery();
                if (count > 0)
                {
                    clear_data();
                    divError.Visible = true;
                    divError.Attributes["class"] = "alert alert-success";
                    divError.InnerHtml = "Message sent successfully";

                }
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            divError.Visible = true;
            divError.Attributes["class"] = "alert alert-danger";
            divError.InnerHtml = "Something went wrong. Please try again......";
        }
    }

    protected void gridCourseStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridCourseStatus.PageIndex = e.NewPageIndex;
    UserDetails();
        BindGridView();
    }

    protected void BindGridView()
    {
        try
        {
            userId = Convert.ToInt32(Request.QueryString["id"]);
            using (MySqlConnection connection = new MySqlConnection(constr))
            {
                connection.Open();
                //Select details id in tblUserMaster table
                //string strcmd = "SELECT id,uid ,amount,status ,case when approve is null then 'Pending' else 'Approved' end as approve,createdDate,modifiedDate,createdBy,updatedBy  FROM tblCustomPayment order by id desc";
                string strcmd = "SELECT B.userId, D.emailId, A.courseId, A.courseNumber, A.courseName, A.courseLevel, A.credits, C.deptName, D.fName, D.lName, case when courseStatus is False then 'Decline' else 'Approved' end as courseStatus from tblcourse as A " +
                    "LEFT OUTER JOIN tblregistration as B on B.courseId = A.courseId " +
                    "LEFT OUTER JOIN tbldepartment as C on C.deptId = A.deptId " +
                    "LEFT OUTER JOIN tbluser as D on D.userId = B.userId " +
                    "WHERE B.status = 1 and B.userId = " + userId + "";

                //create a dataset object and fill it 
                MySqlDataAdapter da = new MySqlDataAdapter(strcmd, connection);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridCourseStatus.DataSource = ds;
                    gridCourseStatus.DataBind();
                }
                else
                {
                    //div_msg.Visible = true;
                    //div_msg.InnerText = "No records found...... ";
                    gridCourseStatus.DataSource = null;
                    gridCourseStatus.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            //div_msg.Visible = true;
            //div_msg.Attributes["class"] = "alert alert-danger";
            //div_msg.InnerHtml = "Something went wrong. Please try again......";
        }
    }
    //clear all fields 
    void clear_data()
    {
        txtMessage.Text = "";
    }

    protected void btnEditInfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("update-info.aspx?id=" + userId);
    }

    protected void btnEditCourse_Click(object sender, EventArgs e)
    {
        Response.Redirect("update-course-info.aspx?id=" + userId);
    }
}