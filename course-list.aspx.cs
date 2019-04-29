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

public partial class course_list : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context dbContext = new db_context();
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.SelectedIndex = -1;
        if (!IsPostBack)
        {
            lbl_msg.Visible = false;
            BindGridView();

            string StrDepartment = "select deptId, deptName from tbldepartment";
            dbContext.BindDropDownlist(StrDepartment, ref ddlDepartment);
            dbContext.BindDropDownlist(StrDepartment, ref ddlCDepartment);

            string StrCourse = "select courseId, courseNumber from tblcourse";
            dbContext.BindDropDownlist(StrCourse, ref ddlCourseNumber);


        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;

        BindGridView();

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "StudentDetails")
        {
            string id = e.CommandArgument.ToString();
            if (IsValid)
            {
                try
                {
                    //  Response.Redirect("candidate-details.aspx?id=" + dbContext.EncryptData(id) + "", false);
                    Page.ClientScript.RegisterStartupScript(
           this.GetType(), "OpenWindow", "window.open('login.aspx','_newtab');", true);

                    //ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('Default.aspx');", true);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
        }

    }



    void BindGridView()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["MySql_ConnectionString"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                //Show preview leads of respective user
                string strcmd = "SELECT A.courseId, A.courseNumber, A.courseName, A.courseLevel, A.credits, B.deptName, A.status, A.term from tblcourse as A " +                
                    "LEFT OUTER JOIN tbldepartment as B on B.deptId = A.deptId " +
                    "WHERE A.status = 1 " +
                    "group by A.courseId, A.courseNumber, A.courseName, A.courseLevel, A.credits, B.deptName, A.status, A.term order by A.userId desc ";
                MySqlDataAdapter da = new MySqlDataAdapter(strcmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();

                    // div_msg.Visible = false;
                }
                else
                {
                    //div_msg.Visible = true;
                    //div_msg.InnerText = "No records found...... ";
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            //div_msg.Visible = true;
            //div_msg.InnerText = "Something went wrong. Please try again......";
        }
    }

    protected void btn_preview_Click(object sender, EventArgs e)
    {

    }

    protected void btn_advance_preview_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                BindGridView();
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            lbl_msg.Visible = true;
            lbl_msg.Text = "Something went wrong. Please try again......";
        }
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        ////check rows count
        if (GridView1.Rows.Count == 0)
        {
            lbl_msg.Visible = true;
            lbl_msg.Text = "There are no Records for the selected status......";
        }
        //check rows is grater than  zero
        if (GridView1.Rows.Count > 0)
        {
            lbl_msg.Visible = false;
        }
    }

    protected void btn_clear_Click(object sender, EventArgs e)
    {
    }

    void clearData()
    {

    }

    protected void btnAddCourse_Click(object sender, EventArgs e)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySql_ConnectionString"].ConnectionString))
            {        
                    string strcmd = "INSERT INTO tblcourse (courseNumber, courseName, courseLevel, credits, deptId, term, userId) values ('" + txtCourseNumber.Text + "', " +
                    " '" + txtCourseName.Text + "', '" + ddlCLevel.SelectedValue + "', " + ddlCourseCredits.SelectedValue + ", " + ddlCDepartment.SelectedIndex + ", " +
                    " '" + ddlCourseTerm.SelectedValue + "', " + Convert.ToInt32(Session["userId"]) + ")";
                    MySqlCommand cmd = new MySqlCommand(strcmd, connection);
                connection.Open();
                    int i = cmd.ExecuteNonQuery();

                strcmd = " ";
                strcmd = "INSERT INTO tblsection (sectionNumber, courseName, courseLevel, credits, deptId, term, userId) values ('" + txtCourseNumber.Text + "', " +
                    " '" + txtCourseName.Text + "', '" + ddlCLevel.SelectedValue + "', " + ddlCourseCredits.SelectedValue + ", " + ddlCDepartment.SelectedIndex + ", " +
                    " '" + ddlCourseTerm.SelectedValue + "', " + Convert.ToInt32(Session["userId"]) + ")";
                MySqlCommand cmd = new MySqlCommand(strcmd, connection);
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    {
                        clearData();
                       BindGridView();

                        div_msg.Visible = true;
                        div_msg.Attributes["class"] = "alert alert-success";
                        div_msg.InnerHtml = "Course successfully added";
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
            lbl_msg.Visible = true;
            lbl_msg.Text = "Something went wrong. Please try again......";
        }
    }
}