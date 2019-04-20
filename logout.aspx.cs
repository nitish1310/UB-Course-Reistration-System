using log4net;
using System;

public partial class logout : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //create a object Db_context class for database connecton and database related operation
    db_context dbContext = new db_context();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["uid"] != null)
            {
                string strcmd1 = "insert into tblLog (uId,log_type,log_time) values ( '" + Session["uid"] + "','out','" + DateTime.Now + "')";
                int i = dbContext.ExecNonQuery(strcmd1);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);  
        }
       
        Session.Clear();     
        Response.Redirect("login.aspx");
    }
}