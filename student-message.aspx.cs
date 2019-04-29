using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class student_message : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Session["userId"]);
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["MySql_ConnectionString"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string strcmd = "SELECT A.studentId, A.message from tblnotofication as A  where A.studentId=" + userId + "";
                MySqlCommand cmd = new MySqlCommand(strcmd, con);
                con.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                  lblMessage.Text = dr["message"].ToString();

                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
}