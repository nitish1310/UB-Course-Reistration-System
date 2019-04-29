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

public partial class course_details : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context dbContext = new db_context();
    int courseId;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            courseId = Convert.ToInt32(Request.QueryString["id"]);


            string StrDepartment = "select deptId, deptName from tbldepartment";
            dbContext.BindDropDownlist(StrDepartment, ref ddlDepartment);

            string StrCourse = "select courseId, courseNumber from tblcourse";
            dbContext.BindDropDownlist(StrCourse, ref ddlCourseNumber);
            // dbContext.BindDropDownlist(StrCourse, ref ddlCourseStatus);

            string StrClassroom = "select classroomId, classroomNumber from tblclassroom";
            dbContext.BindDropDownlist(StrClassroom, ref ddlClassRoom);


            string constr = ConfigurationManager.ConnectionStrings["MySql_ConnectionString"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                
                string strcmd = "SELECT A.courseId, A.courseNumber, A.courseName, A.courseLevel, A.credits, B.deptName, A.status as status, A.term, A.credits, B.deptName from tblcourse as A " +
                    "LEFT OUTER JOIN tbldepartment as B on B.deptId = A.deptId " +
                    "WHERE A.courseId = " + courseId + " " ;
                DataSet ds = dbContext.ExecDataSet(strcmd);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDepartment.SelectedValue = ds.Tables[0].Rows[0][5].ToString();
                    ddlCourseNumber.SelectedValue = ds.Tables[0].Rows[0]["courseNumber"].ToString();
                    txtCourseName.Text = ds.Tables[0].Rows[0]["courseName"].ToString();
                    ddlCourseStatus.SelectedValue = ds.Tables[0].Rows[0]["status"].ToString();
                    ddlCourseLevel.SelectedValue = ds.Tables[0].Rows[0]["courseLevel"].ToString();
                    ddlCourseCredits.SelectedValue = ds.Tables[0].Rows[0]["credits"].ToString();
                    ddlCourseTerm.SelectedValue = ds.Tables[0].Rows[0]["term"].ToString();
                    
                    if (ds.Tables[0].Rows[0]["status"].ToString() == "1")
                    {
                        lblCourseStatus.Text = "Active";
                        btnCourseStatus.Text = "DEACTIVE";
                        btnCourseStatus.CssClass = "btn btn-danger btn-sm btn-block";
                    }
                    else
                    {
                        lblCourseStatus.Text = "Deactive";
                        btnCourseStatus.Text = "ACTIVE";
                        btnCourseStatus.CssClass = "btn btn-success btn-sm btn-block";
                    }
                }

                //MySqlCommand cmd = new MySqlCommand(strcmd, con);
                //con.Open();
                //MySqlDataReader dr = cmd.ExecuteReader();
                //if (dr.HasRows)
                //{
                //    dr.Read();
                //    ddlDepartment.SelectedValue = dr["deptName"].ToString();
                //    ddlCourseNumber.SelectedValue = dr["courseNumber"].ToString();
                //}
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySql_ConnectionString"].ConnectionString))
            {
                int cId = 0;
               
                connection.Open();
       
                //update course details
                string strcmd = "UPDATE tblcourse SET status = 0 WHERE courseId = " + cId + " and userId = " + Convert.ToInt32(Session["userId"]) + "";
                MySqlCommand cmd = new MySqlCommand(strcmd, connection);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-success";
                    div_msg.InnerHtml = "Course updated successfully";
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerHtml = "Something went wrong. Please try again......";
        }
    }

    protected void btnCourseStatus_Click(object sender, EventArgs e)
    {
        string queryUpdateStatus;
        try
        {
            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySql_ConnectionString"].ConnectionString))
            {
                if (lblCourseStatus.Text == "Active")
                {
                    queryUpdateStatus = "update tblcourse set status = 0 and userId = " + Session["userId"] + " where courseId='" + courseId + "'";
                    MySqlCommand cmd = new MySqlCommand(queryUpdateStatus, connection);
                    connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    
                }
                else if (lblCourseStatus.Text == "Deactive")
                {
                    queryUpdateStatus = "update tblcourse set status = 1 and userId = " + Session["userId"] + " where courseId='" + courseId + "'";
                    MySqlCommand cmd = new MySqlCommand(queryUpdateStatus, connection);
                    connection.Open();
                    int i = cmd.ExecuteNonQuery();
                }
                Response.Redirect(Request.RawUrl);
            }
            
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);

        }
    
        
    }
}