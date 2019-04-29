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

public partial class registered_courses : System.Web.UI.Page
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
            divContainer.Visible = false;
           
            try
            {
                //string StrCourseStatus = "SELECT * FROM tblregistration";
                //dbContext.BindDropDownlist(StrCourseStatus, ref ddlCourseStatus);

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
                string strcmd = "SELECT B.userId, D.emailId, A.courseId, A.courseNumber, A.courseName, A.courseLevel, A.credits, C.deptName, D.fName, D.lName, B.courseStatus from tblcourse as A " +
                    "LEFT OUTER JOIN tblregistration as B on B.courseId = A.courseId " +
                    "LEFT OUTER JOIN tbldepartment as C on C.deptId = A.deptId " +
                    "LEFT OUTER JOIN tbluser as D on D.userId = B.userId " +
                    "WHERE B.status = 1";

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

       
        ddlCourseStatus.SelectedValue = "--Select--";

        //btn_save.Enabled = true;
        btn_update.Enabled = false;

        div_msg.Visible = false;
        div_msg.Attributes["class"] = "";
        div_msg.InnerHtml = "";
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySql_ConnectionString"].ConnectionString))
            {               
                connection.Open();
                
                //update existing Executive details
                string strcmd = "UPDATE tblregistration SET courseStatus = "+ ddlCourseStatus.SelectedValue +" WHERE userId = "+ lblUserId.Text + " and courseId = " + lblCourseId.Text + "";
                MySqlCommand cmd = new MySqlCommand(strcmd, connection);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    clear_data();
                    FillGrid();
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-success";
                    div_msg.InnerHtml = "Course Status Changed successfully";
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
    //protected void btn_drop_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySql_ConnectionString"].ConnectionString.ToString()))
    //        {
    //            int cId = 0;
    //            DataTable dt2 = new DataTable();
    //            string strcmd1 = "SELECT courseId FROM tblcourse where courseName = '" + ddlCourseName.SelectedItem + "'";
    //            connection.Open();
    //            MySqlCommand cmd2 = new MySqlCommand(strcmd1, connection);
    //            MySqlDataReader dr1 = cmd2.ExecuteReader();
    //            if (dr1.HasRows)
    //            {
    //                dr1.Read();
    //                cId = Convert.ToInt32(dr1["courseId"]);
    //            }
    //            dr1.Close();
    //            //update existing Executive details
    //            string strcmd = "UPDATE tblregistration SET status = 0 WHERE courseId = " + cId + " and userId = " + Convert.ToInt32(Session["userId"]) + "";
    //            MySqlCommand cmd = new MySqlCommand(strcmd, connection);
    //            int i = cmd.ExecuteNonQuery();
    //            if (i > 0)
    //            {
    //                clear_data();
    //                FillGrid();
    //                div_msg.Visible = true;
    //                div_msg.Attributes["class"] = "alert alert-success";
    //                div_msg.InnerHtml = "Course droped successfully";
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Error("" + ex);
    //    }
    //}
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        //call clear_data method
        clear_data();
    }
    protected void gridRegistration_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fill data gridview to controls
        lblUserId.Text = gridRegistration.SelectedRow.Cells[2].Text;
        lblEmail.Text = gridRegistration.SelectedRow.Cells[5].Text;
        lblCourseId.Text = gridRegistration.SelectedRow.Cells[1].Text;
        //btn_save.Enabled = false;
        btn_update.Enabled = true;
        divContainer.Visible = true;
    }
    protected void gridRegistration_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //pagening change update gridview
        gridRegistration.PageIndex = e.NewPageIndex;
        FillGrid();
    }
}
