using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class message : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["MySql_ConnectionString"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string queryMessage = "update tblnotofication set message ='" + txtMessage.Text + "'" +
                    "where studentId = 20";
                MySqlCommand cmd = new MySqlCommand(queryMessage, con);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    Response.Redirect(Request.RawUrl, false);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
}