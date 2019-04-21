using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class login : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    div_msg.Visible = false;
        //    this.Form.DefaultButton = "btn_login";

        //    if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
        //    {
        //        txt_username.Text = Request.Cookies["UserName"].Value;
        //        txt_password.Attributes["value"] = Request.Cookies["Password"].Value;
        //    }
        //}
    }
    protected void btn_login_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            try
            {
                //login query
                string connectionString = ConfigurationManager.ConnectionStrings["MySql_ConnectionString"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    string str = "select fName, lName, emailId, password, userType from tbluser where userType = 3 and emailId = @emailId and password = @password ";

                    MySqlCommand cmd = new MySqlCommand(str, con);
                    cmd.Parameters.AddWithValue("@emailId", txt_username.Text);
                    cmd.Parameters.AddWithValue("@password", txt_password.Text);
                    con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        //Remember username and password code
                        //if (chkRememberMe.Checked)
                        //{
                        //    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                        //    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                        //}
                        //else
                        //{
                        //    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                        //    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

                        //}
                        //Response.Cookies["UserName"].Value = txt_username.Text.Trim();
                        //Response.Cookies["Password"].Value = txt_password.Text.Trim();

                        dr.Read();
                        {
                            string userType = dr["userType"].ToString();
                            //User Sessions                               
                           
                            if (Convert.ToInt32(dr["userType"]) == 1)
                            {
                                Response.Redirect("student-home.aspx", false);
                            }
                            else if(Convert.ToInt32(dr["userType"]) == 3)
                            {
                                Response.Redirect("admin-home.aspx", false);
                            }
                            else
                            {
                                Response.Redirect("advisor-home.aspx", false);
                            }
                            Session["emailId"] = dr["emailId"].ToString();
                            Session["userType"] = dr["userType"];

                            Session["fName"] = dr["fName"].ToString();
                            Session["lName"] = dr["lName"].ToString();
                        }         
                               
                        dr.Close();
                    }
                    else
                    {
                        div_msg.Visible = true;
                        div_msg.Attributes["class"] = "alert alert-danger";
                        div_msg.InnerText = "Wrong Username or Password....Please Try Again.";
                    }
                  
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
            }
        }
    }

    protected void forgotpassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("forgotpassword.aspx", false);
    }

}