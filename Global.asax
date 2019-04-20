<%@ Application Language="C#" %>
<%@ Import Namespace="System.Data.SqlClient" %>

<script RunAt="server">

    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    db_context dbContext = new db_context();

    void Application_Start(object sender, EventArgs e)
    {
        log4net.Config.XmlConfigurator.Configure();
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

        //Exception exception = Server.GetLastError();

        //Response.Clear();

        //HttpException httpException = exception as HttpException;

        //if (httpException != null)
        //{
        //    Log.Error(exception);
        //    switch (httpException.GetHttpCode())
        //    {
        //        case 404:
        //            // page not found 
        //            //Response.Redirect("~/Error_page.aspx",false);
        //            break;
        //        case 500:
        //            // server error 
        //            //routeData.Values.Add("action", "HttpError500"); 
        //            break;
        //        default:
        //            //routeData.Values.Add("action", "General"); 
        //            break;
        //    }



        //    // clear error on server 
        //    Server.ClearError();

        //}

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
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
    }

</script>
