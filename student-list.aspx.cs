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

public partial class student_list : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //create a object db_context  class for database related method.
    db_context dbContext = new db_context();

    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.SelectedIndex = -1;
        if (!IsPostBack)
        {
            lbl_msg.Visible = false;
           BindGridView();
         
            try
            {
                //string StrQuery2 = "select Distinct B.id,B.name,A.cityid from tblUserMaster as A  Inner Join tblCitiesMaster as B on A.cityid = B.id order by B.name";
                //dbContext.BindDropDownlist(StrQuery2, ref ddl_city);

                //string StrQueryExe = "select id,exeName from tblExecutive where status ='ACTIVE'";
                //dbContext.BindDropDownlist(StrQueryExe, ref ddl_ename);

                //string StrQueryBatch = " select id,batchName from tblTrainingBatch where date < (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) order by date desc";
                //dbContext.BindDropDownlist(StrQueryBatch, ref ddl_batch);

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                // if condition fails then user will get following message
                //div_msg.Visible = true;
                //div_msg.Attributes["class"] = "alert alert-danger";
                //div_msg.InnerText = "Something wrong on form loading. Please Try again." + ex.Message;
            }
        }
    }

    protected void btn_preview_Click(object sender, EventArgs e)
    {
        try
        {
            clear();
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

    //protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    //{
    ////    string sortingDirection = string.Empty;
    ////    if (direction == SortDirection.Ascending)
    ////    {
    ////        direction = SortDirection.Descending;
    ////        sortingDirection = "Desc";
    ////    }
    ////    else
    ////    {
    ////        direction = SortDirection.Ascending;
    ////        sortingDirection = "Asc";

    ////    }
    ////    DataView sortedView = new DataView(BindGridView());
    ////    sortedView.Sort = e.SortExpression + " " + sortingDirection;
    ////    Session["SortedView"] = sortedView;
    ////    GridView1.DataSource = sortedView;
    ////    GridView1.DataBind();
    //}

    //public SortDirection direction
    //{
    //    get
    //    {
    //        if (ViewState["directionState"] == null)
    //        {
    //            ViewState["directionState"] = SortDirection.Ascending;
    //        }
    //        return (SortDirection)ViewState["directionState"];
    //    }
    //    set
    //    {
    //        ViewState["directionState"] = value;
    //    }
    //}

    void BindGridView()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["MySql_ConnectionString"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                //Show preview leads of respective user
                string strcmd = "SELECT A.userId, D.emailId, B.courseId, B.courseNumber, B.courseName, B.courseLevel, B.credits, C.deptName, D.fName, D.lName, A.courseStatus from tblregistration as A " +
                    "LEFT OUTER JOIN tblcourse as B on B.courseId = A.courseId " +
                    "LEFT OUTER JOIN tbldepartment as C on C.deptId = B.deptId " +
                    "LEFT OUTER JOIN tbluser as D on D.userId = A.userId " +
                    "WHERE A.status = 1 and D.userType = 1 ";
                    //if text box txt_name is not empty then like operator will be find data with avlible text name
            if (txt_name.Text != "")
                {
                    strcmd += " AND (D.fName like '%" + txt_name.Text.Trim() + "%'  or D.lName like '%" + txt_name.Text.Trim() + "%' or D.emailId like '%" + txt_name.Text.Trim() + "%') ";
                }

                //data is order by desc
                strcmd += "group by A.userId, D.fName, D.lName, D.emailId, B.courseId, B.courseNumber, B.courseName, B.courseLevel, B.credits, C.deptName, A.courseStatus order by A.userId desc ";
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


    private DataTable BindGridView(string strcmd)
    {
        try
        {
            //create a dataset object and fill it 
            DataSet ds = dbContext.ExecDataSet(strcmd);
            int row_count = ds.Tables[0].Rows.Count;
            lbl_rowcount.Text = "Total - " + row_count.ToString();
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            lbl_msg.Visible = true;
            lbl_msg.Text = "Something went wrong. Please try again......";
            return null;
        }
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

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        clear();
    }

    private void clear()
    {
        try
        {
            //ddl_city.SelectedIndex = 0;
            //ddl_ename.SelectedIndex = 0;
            //ddl_batch.SelectedIndex = 0;
            //// ddl_cdfAproveStatus.SelectedIndex = 0;
            //ddl_cdfLevel.SelectedIndex = 0;
            //ddl_refundStatus.SelectedIndex = 0;
            //ddl_testApproveStatus.SelectedIndex = 0;
            //ddl_testCompStatus.SelectedIndex = 0;
            //ddl_rating.SelectedIndex = 0;
            //txt_from.Text = "";
            //txt_to.Text = "";

        }
        catch (Exception ex)
        {
            Log.Error(ex);

        }

    }
}