using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

public partial class CDFMaster : System.Web.UI.MasterPage
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        int userType = Convert.ToInt32(Session["userType"]);
        try
        {
            if (userType == 1)
            {
                
                    lbl_firstname.Text = Session["fName"].ToString();
                    lbl_lastname.Text = Session["lName"].ToString();
                   
               
               
            }
            else
            {
                Response.Redirect("~/login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
}
