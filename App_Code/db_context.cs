using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;

public class db_context
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public string strcon;
    data_context datacontext = new data_context();
    public db_context()
    {
        strcon = ConfigurationManager.ConnectionStrings["MySql_ConnectionString"].ConnectionString.ToString();
    }

    //public void ConOpen()
    //{
    //    // Creatye object of Connection class and pass connection string (strcon)
    //    con = new SqlConnection(strcon);
    //    // Check connectionstate it is open or close
    //    if (con.State == ConnectionState.Closed)
    //    {
    //        // call function Open to Open the database connection 
    //        con.Open();
    //    }
    //}

    //public void ConClose()
    //{
    //    // Check connectionstate it is open or close
    //    if (con.State == ConnectionState.Open)
    //    {
    //        // call function Close to close the database connection 
    //        con.Close();
    //    }
    //}


    //Query 
    public SqlDataReader ExecDataReader(string s_query)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                // Call function  ConOpen for Open the Database Connection
                //ConOpen();
                con.Open();
                // Create Object of DataReader Class
                SqlDataReader dr;

                // Create Object Of SqlCommand and pass Query (s_query) and Connection Object (con)
                SqlCommand cmd = new SqlCommand(s_query, con);

                //Call ExecuteReader Function with Command Object and Return DataReader Class Object
                dr = cmd.ExecuteReader();

                //Return DataReader Class Object
                return dr;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int ExecNonQuery(string s_query)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                // Call function  ConOpen for Open the Database Connection

                //ConOpen();
                con.Open();

                // Create Object Of SqlCommand and pass Query (s_query) and Connection Object (con)
                SqlCommand cmd = new SqlCommand(s_query, con);

                // call Function ExecuteNonQuery for perform add.edit or delete operation on database

                int intEffectedRows = cmd.ExecuteNonQuery();

                // Call Function ConClose for Close the Database Connection

                //ConClose();

                // Return Integer Value (Number of rows effected in database)

                return intEffectedRows;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet ExecDataSet(string s_query)
    {
        try
        {
            using (MySqlConnection con = new MySqlConnection(strcon))
            {
                // Call function  ConOpen for Open the Database Connection

                //ConOpen();

                // Create Object DataAdapter Class and pass(sqlquerry,connection)

                MySqlDataAdapter da = new MySqlDataAdapter(s_query, con);

                // Create DataSet Object

                DataSet ds = new DataSet();

                //DataAdapter Object Fill DataSet
                da.Fill(ds);
                //da.Update(
                // Call Function ConClose for Close the Database Connection
                //ConClose();

                //Return DataSet
                return ds;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string ExecScal(string s_query)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                // Call function  ConOpen for Open the Database Connection

                //ConOpen();
                con.Open();

                // Create Object Of SqlCommand and pass Query (s_query) and Connection Object (con)
                SqlCommand cmd = new SqlCommand(s_query, con);

                // call Function ExecuteSclar for getting Data Of First Column of First Row
                string str = cmd.ExecuteScalar().ToString();

                // Call Function ConClose for Close the Database Connection
                //ConClose();

                // Return String Value 
                return str;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool Setbulkdata(DataTable table, string tablename)
    {
        try
        {
            using (var bulkcopy = new SqlBulkCopy(strcon, SqlBulkCopyOptions.KeepIdentity))
            {
                foreach (DataColumn dc in table.Columns)
                {
                    bulkcopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                }
                bulkcopy.BulkCopyTimeout = 100000;
                bulkcopy.DestinationTableName = tablename;
                bulkcopy.WriteToServer(table);

                return true;
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            return false;
        }


    }

    public int ExecNonQuerypara(SqlCommand SqlCommand)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {

                // Call function  ConOpen for Open the Database Connection

                //ConOpen();
                con.Open();

                // Create Object Of SqlCommand and pass SqlCommand (SqlCommand) 
                SqlCommand cmd = SqlCommand;
                cmd.Connection = con;

                // call Function ExecuteNonQuery for perform add.edit or delete operation on database

                int intEffectedRows = cmd.ExecuteNonQuery();

                // Call Function ConClose for Close the Database Connection

                //ConClose();

                // Return Integer Value (Number of rows effected in database)

                return intEffectedRows;
            }

        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

   

    public DataSet getDataSet(string strProcName, string[,] inParam)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();

                //Create SqlCommand Object and Pass StoreProcedureName and Connection Object
                SqlCommand cmd = new SqlCommand(strProcName, con);

                //Specify That Command Type StoreProcedure
                cmd.CommandType = CommandType.StoredProcedure;
                //Add Parameter from string Array

                for (int i = 0; i <= inParam.GetUpperBound(0); i++)
                {
                    //Create ParameterObject
                    SqlParameter sp = new SqlParameter();
                    //Add Parameter Name
                    sp.ParameterName = inParam[i, 0];
                    //Add SqlDataType for Parameter
                    if (inParam[i, 1].Equals("Int"))
                    {
                        sp.SqlDbType = SqlDbType.Int;
                    }
                    if (inParam[i, 1].Equals("Float"))
                    {
                        sp.SqlDbType = SqlDbType.Float;
                    }
                    if (inParam[i, 1].Equals("Char"))
                    {
                        sp.SqlDbType = SqlDbType.Char;
                    }
                    if (inParam[i, 1].Equals("VarChar"))
                    {
                        sp.SqlDbType = SqlDbType.VarChar;
                    }
                    if (inParam[i, 1].Equals("Text"))
                    {
                        sp.SqlDbType = SqlDbType.Text;
                    }
                    if (inParam[i, 1].Equals("DateTime"))
                    {
                        sp.SqlDbType = SqlDbType.DateTime;
                    }
                    //Assign Value to Parameter Object
                    sp.Value = inParam[i, 2];
                    //Parameter Add SqlCommmand Object
                    cmd.Parameters.Add(sp);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //Call Execute Reader Function and Return DataReader Object
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            return null;
        }

    }

    public int AddEditData(string strProcName, string[,] inParam)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                // Call function  ConOpen for Open the Database Connection
                //ConOpen();
                con.Open();
                //Create SqlCommand Object and Pass StoreProcedureName and Connection Object
                SqlCommand cmd = new SqlCommand(strProcName, con);
                //Specify That Command Type StoreProcedure
                cmd.CommandType = CommandType.StoredProcedure;
                //Add Parameter from string Array
                for (int i = 0; i <= inParam.GetUpperBound(0); i++)
                {
                    //Create ParameterObject
                    SqlParameter sp = new SqlParameter();
                    //Add Parameter Name
                    sp.ParameterName = inParam[i, 0];
                    //Add SqlDataType for Parameter
                    if (inParam[i, 1].Equals("Int"))
                    {
                        sp.SqlDbType = SqlDbType.Int;
                    }
                    if (inParam[i, 1].Equals("Float"))
                    {
                        sp.SqlDbType = SqlDbType.Float;
                    }
                    if (inParam[i, 1].Equals("Char"))
                    {
                        sp.SqlDbType = SqlDbType.Char;
                    }
                    if (inParam[i, 1].Equals("VarChar"))
                    {
                        sp.SqlDbType = SqlDbType.VarChar;
                    }
                    if (inParam[i, 1].Equals("Text"))
                    {
                        sp.SqlDbType = SqlDbType.Text;
                    }
                    if (inParam[i, 1].Equals("DateTime"))
                    {
                        sp.SqlDbType = SqlDbType.DateTime;
                    }
                    //Assign Value to Parameter Object
                    sp.Value = inParam[i, 2];
                    //Parameter Add SqlCommmand Object
                    cmd.Parameters.Add(sp);
                }

                //Execute Command against DataBase Return No of Affected Row
                int row = cmd.ExecuteNonQuery();
                // Call function  ConClose for Close the Database Connection
                // ConClose();
                //Return No of Affected Row If SqlCommand Execute against DataBase Successfully
                return row;
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }


    }

    //Control class 

    public void BindDropDownlist(string StrQuery, ref DropDownList ddl)
    {
        try
        {
            using (MySqlConnection con = new MySqlConnection(strcon))
            {
                //SqlConnection con = new SqlConnection(strcon);
               // SqlCommand cmd = new SqlCommand(StrQuery, con);
                MySqlDataAdapter da = new MySqlDataAdapter(StrQuery,con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ddl.DataSource = ds;
                ddl.SelectedValue = null;
                ddl.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddl.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddl.DataBind();
                ddl.Items.Insert(0, "--Select--");
                ds.Tables.Clear();
                ds.Dispose();
                da.Dispose();
                //cmd.Dispose();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    //Send mail function
    public Boolean SendEmail(string recepientEmail, string subject, string body)
    {
        try
        {
            using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage())
            {
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["DisplayName"]);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(recepientEmail));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                smtp.Send(mailMessage);
                return true;
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            return false;
        }
    }

    //Send SMS Function
    public Boolean sendSms(string mob, string msg)
    {
        string result = "";
        WebRequest request = null;
        HttpWebResponse response = null;
        try
        {
            string userid = ConfigurationManager.AppSettings["SMSUserId"].ToString();  //  "2000167436";
            string passwd = ConfigurationManager.AppSettings["SMSPassword"].ToString();  //  "xzreMXXv5";
            string url =
        "http://enterprise.smsgupshup.com/GatewayAPI/rest?method=sendMessage&send_to=" +
        mob + "&msg=" + msg + "&userid=" + userid + "&password=" + passwd + "&v=1.1&msg_type=TEXT&auth_scheme=PLAIN";

            request = WebRequest.Create(url);
            response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader reader = new System.IO.StreamReader(stream, ec);
            result = reader.ReadToEnd();
            reader.Close();
            stream.Close();
            return true;
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            return false;
        }
    }



    


    

}