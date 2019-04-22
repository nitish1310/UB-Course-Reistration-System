using System;
using System.Web.UI.WebControls;
using System.Data;
using log4net;
using System.Configuration;
using MySql.Data.MySqlClient;

public partial class course_registration : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //create a object db_context  class for database related method.
    db_context dbContext = new db_context();

    protected void Page_Load(object sender, EventArgs e)
    {
        //only first time execuite on below code 
        if (!IsPostBack)
        {
            clear_data();
            FillGrid();
            try
            {
                //string StrDepartment = "SELECT deptId, deptName FROM tbldepartment";
                //dbContext.BindDropDownlist(StrDepartment, ref ddlDepartment);

                //string StrCourseLevel = "SELECT courseLevel FROM tblcourse where "+ ddlDepartment.SelectedValue + "";
                //dbContext.BindDropDownlist(StrCourseLevel, ref ddlCourseLevel);
                //dbContext.BindDropDownlist(StrCourseLevel, ref ddlCourseName);

               

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                
            }
        }

    }

    //This funcation will fill data in grid View 
    void FillGrid()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["MySql_ConnectionString"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                //Show preview leads of respective user
                string strcmd = "SELECT A.courseId, A.courseNumber, A.courseName, A.courseLevel, A.credits, B.userId, C.deptName from tblcourse as A " +
                    "LEFT OUTER JOIN tblregistration as B on B.courseId = A.courseId " +
                    "LEFT OUTER JOIN tbldepartment as C on C.deptId = A.deptId " +
                    "LEFT OUTER JOIN tbluser as D on D.userId = B.userId " +
                    "WHERE B.userId= " + Session["userId"] + " and B.status = 1";

                MySqlDataAdapter da = new MySqlDataAdapter(strcmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridRegistration.DataSource = ds;
                    gridRegistration.DataBind();

                    div_msg.Visible = false;
                }
                else
                {
                    div_msg.Visible = true;
                    div_msg.InnerText = "No records found...... ";
                    gridRegistration.DataSource = null;
                    gridRegistration.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.InnerText = "Something went wrong. Please try again......";
        }
    }
    //clear all fields 
    void clear_data()
    {
        
        ddlDepartment.SelectedValue = "--Select--";
        ddlCourseName.SelectedValue = "--Select--";
        ddlCourseLevel.SelectedValue = "--Select--";
       
        btn_save.Enabled = true;
        btn_drop.Enabled = false;

        div_msg.Visible = false;
        div_msg.Attributes["class"] = "";
        div_msg.InnerHtml = "";
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySql_ConnectionString"].ConnectionString.ToString()))
            {
                int courseID = 0;
                DataTable dt2 = new DataTable();
                string strcmd1 = "SELECT courseId FROM tblcourse where courseName = '" + ddlCourseName.SelectedItem + "'";
                connection.Open();
                MySqlCommand cmd2 = new MySqlCommand(strcmd1, connection);
                MySqlDataReader dr1 = cmd2.ExecuteReader();
                if (dr1.HasRows)
                {
                    dr1.Read();
                    courseID = Convert.ToInt32(dr1["courseId"]);
                }
                dr1.Close();
                string str = "select A.courseId from tblregistration as A " +
                    "INNER JOIN tblcourse as B on B.courseId = A.courseId " +
                    "where A.userId=" + Convert.ToInt32(Session["userId"]) + " and B.courseName = '" + ddlCourseName.SelectedItem + "'";
                MySqlCommand cmd = new MySqlCommand(str, connection);
                //connection.Open();
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                if (id == 0)
                {                      
                    //Insert a new Executive data 
                    string strcmd = "INSERT INTO tblregistration (type, userId, sectionId, courseId, status) values ('Campus','" + Convert.ToInt32(Session["userId"]) + "',1,'" + courseID + "', 1)";
                    cmd = new MySqlCommand(strcmd, connection);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        clear_data();
                        FillGrid();

                        div_msg.Visible = true;
                        div_msg.Attributes["class"] = "alert alert-success";
                        div_msg.InnerHtml = "Course successfully added";
                    }

                }
                else
                {
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerHtml = "This course already exists";
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
    protected void btn_drop_Click(object sender, EventArgs e)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySql_ConnectionString"].ConnectionString.ToString()))
            {
                int cId = 0;
                DataTable dt2 = new DataTable();
                string strcmd1 = "SELECT courseId FROM tblcourse where courseName = '" + ddlCourseName.SelectedItem + "'";
                connection.Open();
                MySqlCommand cmd2 = new MySqlCommand(strcmd1, connection);
                MySqlDataReader dr1 = cmd2.ExecuteReader();
                if (dr1.HasRows)
                {
                    dr1.Read();
                    cId = Convert.ToInt32(dr1["courseId"]);
                }
                dr1.Close();
                //update existing Executive details
                string strcmd = "UPDATE tblregistration SET status = 0 WHERE courseId = " + cId + " and userId = " + Convert.ToInt32(Session["userId"]) + "";
                MySqlCommand cmd = new MySqlCommand(strcmd, connection);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    clear_data();
                    FillGrid();
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-success";
                    div_msg.InnerHtml = "Course droped successfully";
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        //call clear_data method
        clear_data();
    }
    protected void gridRegistration_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fill data gridview to controls
        //txt_name.Text = gridRegistration.SelectedRow.Cells[2].Text;
        //txt_email.Text = gridRegistration.SelectedRow.Cells[3].Text;
        //drop_status.SelectedValue = gridRegistration.SelectedRow.Cells[4].Text;
        btn_save.Enabled = false;
        btn_drop.Enabled = true;
    }
    protected void gridRegistration_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //pagening change update gridview
        gridRegistration.PageIndex = e.NewPageIndex;
        FillGrid();
    }
}
