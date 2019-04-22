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
            using (SqlConnection con = new SqlConnection(strcon))
            {
                // Call function  ConOpen for Open the Database Connection

                //ConOpen();

                // Create Object DataAdapter Class and pass(sqlquerry,connection)

                SqlDataAdapter da = new SqlDataAdapter(s_query, con);

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

    //SP

    //public DataSet getDataSet(string strProcName, string[,] inParam)
    //{

    //    // Call function  ConOpen for Open the Database Connection
    //    ConOpen();
    //    //Create SqlCommand Object and Pass StoreProcedureName and Connection Object
    //    SqlCommand cmd = new SqlCommand(strProcName, con);
    //    //Specify That Command Type StoreProcedure
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    //Add Parameter from string Array

    //    for (int i = 0; i <= inParam.GetUpperBound(0); i++)
    //    {

    //        //Create ParameterObject
    //        SqlParameter sp = new SqlParameter();
    //        //Add Parameter Name
    //        sp.ParameterName = inParam[i, 0];
    //        //Add SqlDataType for Parameter
    //        if (inParam[i, 1].Equals("Int"))
    //        {
    //            sp.SqlDbType = SqlDbType.Int;
    //        }
    //        if (inParam[i, 1].Equals("Float"))
    //        {
    //            sp.SqlDbType = SqlDbType.Float;
    //        }
    //        if (inParam[i, 1].Equals("Char"))
    //        {
    //            sp.SqlDbType = SqlDbType.Char;
    //        }
    //        if (inParam[i, 1].Equals("VarChar"))
    //        {
    //            sp.SqlDbType = SqlDbType.VarChar;
    //        }
    //        if (inParam[i, 1].Equals("Text"))
    //        {
    //            sp.SqlDbType = SqlDbType.Text;
    //        }
    //        if (inParam[i, 1].Equals("DateTime"))
    //        {
    //            sp.SqlDbType = SqlDbType.DateTime;
    //        }
    //        //Assign Value to Parameter Object
    //        sp.Value = inParam[i, 2];
    //        //Parameter Add SqlCommmand Object
    //        cmd.Parameters.Add(sp);
    //    }

    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    //Call Execute Reader Function and Return DataReader Object
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);

    //    ConClose();

    //    return ds;

    //}

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
                ddl.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddl.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddl.DataBind();
                //ddl.Items.Insert(0, "--Select--");
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

    //interest test

    //generate Full Interest Test factors and Update Test status
    public Boolean generateInterestFactor_64(int c_id, int batid, int testid)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                try
                {
                    SqlDataAdapter getans = new SqlDataAdapter("SELECT c_id FROM tblInterestCandAnswers WHERE batid=" + batid + " and c_id = " + c_id, con);
                    DataSet dsans = new DataSet();
                    getans.Fill(dsans);

                    SqlDataAdapter getfactor = new SqlDataAdapter("SELECT c_id FROM tblInterestCandFactors WHERE batid=" + batid + " and c_id = " + c_id, con);
                    DataSet dsfactor = new DataSet();
                    getfactor.Fill(dsfactor);

                    if (dsfactor.Tables[0].Rows.Count == 0 && dsans.Tables[0].Rows.Count == 64)
                    {
                        //II factor Table
                        DataTable factortbl = new DataTable();
                        factortbl.Columns.Add("c_id", typeof(int));
                        factortbl.Columns.Add("factor_no", typeof(int));
                        factortbl.Columns.Add("score", typeof(int));
                        factortbl.Columns.Add("rating", typeof(string));
                        factortbl.Columns.Add("P_rating", typeof(float));
                        factortbl.Columns.Add("batid", typeof(int));

                        //get All Factors
                        SqlDataAdapter getFactors = new SqlDataAdapter("SELECT * FROM tblInterestFactors", con);
                        DataSet dsfactors = new DataSet();
                        getFactors.Fill(dsfactors);

                        //get All norming
                        SqlDataAdapter getnorming = new SqlDataAdapter("Select * FROM tblInterestNorming", con);
                        DataSet dsnorming = new DataSet();
                        getnorming.Fill(dsnorming);

                        //get All Factors                       
                        SqlDataAdapter getRating = new SqlDataAdapter("Select * FROM tblInterestRating", con);
                        DataSet dsRating = new DataSet();
                        getRating.Fill(dsRating);

                        //get candidate details                       
                        SqlDataAdapter getCanddetails = new SqlDataAdapter("Select * FROM tblUserMaster where uId=" + c_id, con);
                        DataSet dscandDetails = new DataSet();
                        getCanddetails.Fill(dscandDetails);

                        int age = DateTime.Now.Year - Convert.ToDateTime(dscandDetails.Tables[0].Rows[0]["dob"]).Year;
                        string gender = dscandDetails.Tables[0].Rows[0]["gender"].ToString();


                        //get count of factors from tbl_II_cand_answers
                        for (int fno = 1; fno <= 16; fno++)
                        {
                            #region counting_factor
                            SqlDataAdapter getfactormarks = new SqlDataAdapter("SELECT sum(A.marks) as marks , B.factor FROM tblInterestCandAnswers A, tblInterestFactors B WHERE A.batid=" + batid + " and c_id =" + c_id + " AND A.factor_no = " + fno + " AND A.factor_no = B.factor_no group by B.factor ", con);
                            DataSet dscandmarks = new DataSet();
                            getfactormarks.Fill(dscandmarks);

                            int score = 0;
                            string factor = "";

                            if (dscandmarks.Tables[0].Rows.Count > 0)
                            {
                                score = Convert.ToInt32(dscandmarks.Tables[0].Rows[0][0]) * 2;
                                if (score >= 9)
                                    score = 8;
                                factor = dscandmarks.Tables[0].Rows[0][1].ToString();
                            }

                            double P_rating = 0.0;
                            string rating = "";


                            if (Convert.ToInt32(age.ToString()) >= 15 && Convert.ToInt32(age.ToString()) <= 21)
                            {
                                #region age_b/w_15-21

                                DataRow[] PRatings = dsnorming.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =" + age);


                                if (PRatings.Length > 0 && PRatings[0][4 + score].ToString() != null)
                                {
                                    P_rating = Convert.ToDouble(PRatings[0][4 + score].ToString());
                                }

                                DataRow[] ratings = dsRating.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =" + age);


                                if (ratings.Length > 0 && ratings[0][4 + score].ToString() != null)
                                {
                                    rating = ratings[0][4 + score].ToString();
                                }


                                #endregion
                            }
                            else
                            {
                                if (age < 15)//age is less than 15
                                {
                                    #region age_less_15

                                    DataRow[] PRatings = dsnorming.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =15");

                                    if (PRatings.Length > 0 && PRatings[0][4 + score].ToString() != null)
                                    {
                                        P_rating = Convert.ToDouble(PRatings[0][4 + score].ToString());
                                    }

                                    DataRow[] ratings = dsRating.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =15");


                                    if (ratings.Length > 0 && ratings[0][4 + score].ToString() != null)
                                    {
                                        rating = ratings[0][4 + score].ToString();
                                    }


                                    #endregion
                                }
                                if (age > 21)//age is >21
                                {
                                    #region age_greter_21

                                    DataRow[] PRatings = dsnorming.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =21");


                                    if (PRatings.Length > 0 && PRatings[0][4 + score].ToString() != null)
                                    {
                                        P_rating = Convert.ToDouble(PRatings[0][4 + score].ToString());
                                    }

                                    DataRow[] ratings = dsRating.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =21");


                                    if (ratings.Length > 0 && ratings[0][4 + score].ToString() != null)
                                    {
                                        rating = ratings[0][4 + score].ToString();
                                    }

                                    #endregion
                                }
                            }

                            factortbl.Rows.Add(c_id, fno, score, rating, P_rating, batid);


                            #endregion
                        }

                        if (factortbl.Rows.Count == 16)
                        {
                            using (var bulkcopy = new SqlBulkCopy(strcon, SqlBulkCopyOptions.KeepIdentity))
                            {
                                foreach (DataColumn dc in factortbl.Columns)
                                {
                                    bulkcopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                                }
                                bulkcopy.BulkCopyTimeout = 100000;
                                bulkcopy.DestinationTableName = "tblInterestCandFactors";
                                bulkcopy.WriteToServer(factortbl);
                            }
                        }

                        //insert top 3 factors
                        SqlDataAdapter strcmdtop3 = new SqlDataAdapter("SELECT top 3 k.i_factor FROM tblInterestTestReportDiscription as k INNER JOIN tblInterestCandFactors  as R on R.factor_no=k.factor_no WHERE batid=" + batid + " and (c_id = " + c_id + ") order by R.P_rating desc", con);
                        DataSet dstop3 = new DataSet();
                        strcmdtop3.Fill(dstop3);

                        if (dstop3.Tables[0].Rows.Count > 0)
                        {
                            SqlCommand cmdtop3 = null;
                            SqlDataAdapter strcmdchktop3 = new SqlDataAdapter("SELECT * FROM tblCandidateTop3 WHERE c_id = " + c_id, con);
                            DataSet dschktop3 = new DataSet();
                            strcmdchktop3.Fill(dschktop3);

                            if (dschktop3.Tables[0].Rows.Count == 0)
                                cmdtop3 = new SqlCommand("insert into tblCandidateTop3 values(" + c_id + ",'NULL','NULL','NULL','NULL','NULL','NULL','" + dstop3.Tables[0].Rows[0][0].ToString() + "','" + dstop3.Tables[0].Rows[1][0].ToString() + "','" + dstop3.Tables[0].Rows[2][0].ToString() + "','NULL','NULL','NULL')", con);
                            else
                                cmdtop3 = new SqlCommand("update tblCandidateTop3 set  interest1='" + dstop3.Tables[0].Rows[0][0].ToString() + "',interest2='" + dstop3.Tables[0].Rows[1][0].ToString() + "',interest3='" + dstop3.Tables[0].Rows[2][0].ToString() + "' where c_id=" + c_id, con);
                            int intEffectedRows = cmdtop3.ExecuteNonQuery();
                        }

                        //update test status

                        SqlDataAdapter getstatus = new SqlDataAdapter("SELECT * FROM tblUserTestMaster WHERE batid=" + batid + " and uId = " + c_id + " and testid=" + testid, con);
                        DataSet dsstatus = new DataSet();
                        getstatus.Fill(dsstatus);
                        if (dsstatus.Tables[0].Rows.Count == 0)
                        {
                            string strcmd = "INSERT INTO tblUserTestMaster(uId,testid,batid,testStatus,factorStatus,dateofcomplete) VALUES (@uId,@testid,@batid,@testStatus,@factorStatus,@dateofcomplete)";
                            SqlCommand cmd = new SqlCommand(strcmd, con);
                            cmd.Parameters.AddWithValue("@uId", c_id);
                            cmd.Parameters.AddWithValue("@testid", testid);
                            cmd.Parameters.AddWithValue("@batid", batid);
                            cmd.Parameters.AddWithValue("@testStatus", "Complete");
                            cmd.Parameters.AddWithValue("@factorStatus", "Complete");
                            cmd.Parameters.AddWithValue("@dateofcomplete", DateTime.Now);
                            int EffectedRows = cmd.ExecuteNonQuery();
                        }
                        getstatus.Dispose();
                        dsstatus.Dispose();


                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("" + ex);
                    return false;
                }
            }

        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            return false;
        }

    }

    //Update PD Test status
    public Boolean updatePDTestStatus(int c_id, int batid, int testid)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                SqlDataAdapter getstatus = new SqlDataAdapter("SELECT * FROM tblUserTestMaster WHERE batid=" + batid + " and uId = " + c_id + " and testid=" + testid, con);
                DataSet dsstatus = new DataSet();
                getstatus.Fill(dsstatus);
                if (dsstatus.Tables[0].Rows.Count == 0)
                {
                    string strcmd = "INSERT INTO tblUserTestMaster(uId,testid,batid,testStatus,factorStatus,dateofcomplete) VALUES (@uId,@testid,@batid,@testStatus,@factorStatus,@dateofcomplete)";
                    SqlCommand cmd = new SqlCommand(strcmd, con);
                    cmd.Parameters.AddWithValue("@uId", c_id);
                    cmd.Parameters.AddWithValue("@testid", testid);
                    cmd.Parameters.AddWithValue("@batid", batid);
                    cmd.Parameters.AddWithValue("@testStatus", "Complete");
                    cmd.Parameters.AddWithValue("@factorStatus", "Complete");
                    cmd.Parameters.AddWithValue("@dateofcomplete", DateTime.Now);
                    con.Open();
                    int EffectedRows = cmd.ExecuteNonQuery();
                }
                else
                {
                    string strcmd = "update tblUserTestMaster set testStatus=@testStatus,factorStatus=@factorStatus,dateofcomplete=@dateofcomplete WHERE batid=" + batid + " and uId = " + c_id + " and testid=" + testid + "";
                    SqlCommand cmd = new SqlCommand(strcmd, con);
                    cmd.Parameters.AddWithValue("@testStatus", "Complete");
                    cmd.Parameters.AddWithValue("@factorStatus", "Complete");
                    cmd.Parameters.AddWithValue("@dateofcomplete", DateTime.Now);
                    con.Open();
                    int EffectedRows = cmd.ExecuteNonQuery();
                }
                getstatus.Dispose();
                dsstatus.Dispose();
                return true;
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            return false;
        }
    }


    //NCDAP Test Complete * prodid =5
    public bool AllTestComplete(int c_id, int batid)
    {
        bool flag = false;

        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                SqlDataAdapter getPDstatus = new SqlDataAdapter("SELECT id FROM tblPersonalityCandAnswers WHERE batid=" + batid + " and c_id = " + c_id, con);
                DataSet dsPDstatus = new DataSet();
                getPDstatus.Fill(dsPDstatus);

                SqlDataAdapter getITstatus = new SqlDataAdapter("SELECT id FROM tblInterestCandFactors WHERE batid=" + batid + " and c_id = " + c_id, con);
                DataSet dsITstatus = new DataSet();
                getITstatus.Fill(dsITstatus);

                if (dsPDstatus.Tables[0].Rows.Count == 24 && dsITstatus.Tables[0].Rows.Count == 16)
                {
                    string strcmd = "update tblUserProductMaster set testStatus='Complete',dateoftestcomplete='" + DateTime.Now + "' where uId=" + c_id + " and prodid=7";
                    SqlCommand cmd = new SqlCommand(strcmd, con);
                    con.Open();
                    int EffectedRows = cmd.ExecuteNonQuery();


                    SqlCommand cmd1 = new SqlCommand("select uId,fname,lname,contactNo,email,password,userTypeId from tblUserMaster where  uId=" + c_id, con);
                    SqlDataReader sdr = cmd1.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();

                        string fname = sdr["fname"].ToString();
                        string contactNo = sdr["contactNo"].ToString();
                        string email = sdr["email"].ToString();



                        string body = string.Empty;
                        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["NCDAPTest"])))
                        {
                            body = reader.ReadToEnd();
                        }
                        string link = ConfigurationManager.AppSettings["NCDAPInterestTestReportlink"].ToString() + EncryptData(c_id.ToString());

                        body = body.Replace("{UserName}", fname);
                        body = body.Replace("{ReportLink}", link);
                        if (SendEmail(email, ConfigurationManager.AppSettings["NCDAPTestsubject"].ToString(), body))
                        {
                            //send OTP on User SMS
                            string SMSText = ConfigurationManager.AppSettings["NCDAPInterestTestSMS"].ToString();
                            sendSms(contactNo, SMSText);
                        }

                        flag = true;
                    }
                }
                getPDstatus.Dispose();
                dsPDstatus.Dispose();
                getITstatus.Dispose();
                dsITstatus.Dispose();
            }
            return flag;
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            return flag;
        }
    }


    // Other methods

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



    //encrypt data
    public string EncryptData(string clearText)
    {
        try
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                    clearText = HttpUtility.UrlEncode(clearText);
                }
            }
            return clearText;
        }
        catch (Exception)
        {
            return null;
        }
    }

    //decrypt data
    public string Datadecrypt(string cipherText)
    {
        try
        {
            cipherText = HttpUtility.UrlDecode(cipherText);
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        catch (Exception)
        {
            return null;
        }
    }

    // Date Convertor
    public string DateConvert(string date)
    {
        try
        {
            string[] tempdate = date.Split('/');
            if (tempdate.Length == 3)
            {
                string dd = tempdate[0];
                string mm = tempdate[1];
                string yy = tempdate[2];
                yy = yy.Substring(0, 4);
                return mm + "/" + dd + "/" + yy;
            }
            else
            {
                return date;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public bool AllTestCompleteKYAndPD(int uid, int batid)
    {
        bool flag = false;
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string str = "select COUNT(id) from tblUserTestMaster where batid =3 and testStatus='Complete' and factorStatus='Complete' and (testid=9 or testid=10) and uid=" + uid + "";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count >= 2)
                {
                    string strcmd = "update tblUserProductMaster set testStatus='Complete',dateoftestcomplete='" + DateTime.Now + "' where uId=" + uid + " and prodid=7";
                    SqlCommand cmd2 = new SqlCommand(strcmd, con);
                    int EffectedRows = cmd2.ExecuteNonQuery();
                    flag = true;    

                    string fname="", email="", contactNo="", exeEmail="";
                    //string strdata = "select fname, email, contactNo from tblUserMaster where uId ="+uid+"";
                    string strdata = "select u.fname, u.email, u.contactNo,ISNULL(e.exeEmail,'No') as exeEmail from tblUserMaster as u Left Outer Join tblRelation as R on u.uId = R.uId Left Outer Join tblExecutive as e on R.executiveId = e.id where u.uId =" + uid + "";
                    SqlCommand cmd3 = new SqlCommand(strdata, con);
                    SqlDataReader dr = cmd3.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        fname=dr["fname"].ToString();
                        email=dr["email"].ToString();
                        contactNo=dr["contactNo"].ToString();
                        if (dr["exeEmail"].ToString() != "No")
                        {
                            exeEmail = dr["exeEmail"].ToString();
                        }
                        else
                        {
                            exeEmail = null;
                        }

                    //create Body
                    //string body = this.PopulateBodyTestComplete(System.Web.HttpContext.Current.Session["userName"].ToString());
                        string body = this.PopulateBodyTestComplete(fname);

                    //Send email
                    //var task = new Thread(() => datacontext.sendemail(System.Web.HttpContext.Current.Session["email"].ToString(), null, null, ConfigurationManager.AppSettings["CDFTestCompleteEmailSubject"], body));
                    var task = new Thread(() => datacontext.sendemail(email, null, exeEmail, ConfigurationManager.AppSettings["CDFTestCompleteEmailSubject"], body));
                    task.Start();

                    //send email
                    string SMSText = ConfigurationManager.AppSettings["CDFTestCompleteSmsTemplate"].ToString();
                    SMSText = SMSText.Replace("{CDF}", fname);
                    datacontext.sendSms(contactNo, SMSText);
                    }
                    dr.Close();
                }
            }
            return flag;
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            return flag;
        }
    }

    private string PopulateBodyTestComplete(string userName)
    {
        try
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["CDFTestCompleteEmailTemplatePath"])))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            return body;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //KY_ Test
    public Boolean generateKYTestFactor(int c_id, int batid, int testid)
    {
        Boolean flag = false;
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {

                SqlDataAdapter getans = new SqlDataAdapter("SELECT c_id FROM tblKYCandAnswers WHERE batid=" + batid + " and  c_id = " + c_id, con);
                DataSet dsans = new DataSet();
                getans.Fill(dsans);

                SqlDataAdapter getfactor = new SqlDataAdapter("SELECT c_id FROM tblKYCandFactors WHERE batid=" + batid + " and c_id = " + c_id, con);
                DataSet dsfactor = new DataSet();
                getfactor.Fill(dsfactor);

                if (dsfactor.Tables[0].Rows.Count == 0 && dsans.Tables[0].Rows.Count == 90)
                {
                    //KY factor Table
                    DataTable factortbl = new DataTable();
                    factortbl.Columns.Add("c_id", typeof(int));
                    factortbl.Columns.Add("factor_no", typeof(int));
                    factortbl.Columns.Add("score", typeof(int));
                    factortbl.Columns.Add("rating", typeof(string));
                    factortbl.Columns.Add("P_rating", typeof(float));
                    factortbl.Columns.Add("measure", typeof(string));
                    factortbl.Columns.Add("New_P_rating", typeof(float));
                    factortbl.Columns.Add("batid", typeof(int));

                    //get All Factors
                    SqlDataAdapter getFactors = new SqlDataAdapter("SELECT * FROM tblKYFactors", con);
                    DataSet dsfactors = new DataSet();
                    getFactors.Fill(dsfactors);

                    //get All norming
                    SqlDataAdapter getnorming = new SqlDataAdapter("Select * FROM tblPersonalityNorming", con);
                    DataSet dsnorming = new DataSet();
                    getnorming.Fill(dsnorming);

                    //get All Factors                       
                    SqlDataAdapter getRating = new SqlDataAdapter("Select * FROM tblPersonalityRating", con);
                    DataSet dsRating = new DataSet();
                    getRating.Fill(dsRating);

                    //get candidate details                       
                    SqlDataAdapter getCanddetails = new SqlDataAdapter("Select * FROM tblUserMaster where uId=" + c_id, con);
                    DataSet dscandDetails = new DataSet();
                    getCanddetails.Fill(dscandDetails);

                    int age = DateTime.Now.Year - Convert.ToDateTime(dscandDetails.Tables[0].Rows[0]["dob"]).Year;
                    string gender = dscandDetails.Tables[0].Rows[0]["gender"].ToString();

                    //get count of factors from tblKYCandAnswers
                    for (int fno = 1; fno <= 9; fno++)
                    {
                        #region counting_factor
                        SqlDataAdapter getfactormarks = new SqlDataAdapter("SELECT sum(A.marks) , B.factor FROM tblKYCandAnswers as A, tblKYFactors as B WHERE A.batid=" + batid + " AND A.c_id = " + c_id + " AND A.factor_no =" + fno + " AND A.factor_no = B.factor_no group by B.factor", con);
                        DataSet dscandmarks = new DataSet();
                        getfactormarks.Fill(dscandmarks);

                        int score = 0;
                        string factor = "";

                        if (dscandmarks.Tables[0].Rows.Count > 0)
                        {
                            score = Convert.ToInt32(dscandmarks.Tables[0].Rows[0][0].ToString());
                            factor = dscandmarks.Tables[0].Rows[0][1].ToString();
                        }
                        dscandmarks.Clear();
                        dscandmarks.Dispose();

                        double P_rating = 0.0;
                        string rating = "";


                        //if (Convert.ToInt32(age.ToString()) >= 15 && Convert.ToInt32(age.ToString()) <= 21)
                        //{
                        //    #region age_b/w_15-21

                        //    DataRow[] PRatings = dsnorming.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =" + age);


                        //    if (PRatings.Length > 0 && PRatings[0][4 + score].ToString() != null)
                        //    {
                        //        P_rating = Convert.ToDouble(PRatings[0][4 + score].ToString());
                        //    }

                        //    DataRow[] ratings = dsRating.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =" + age);


                        //    if (ratings.Length > 0 && ratings[0][4 + score].ToString() != null)
                        //    {
                        //        rating = ratings[0][4 + score].ToString();
                        //    }


                        //    #endregion
                        //}
                        //else
                        //{
                        //    if (age < 15)//age is less than 15
                        //    {
                        //        #region age_less_15

                        //        DataRow[] PRatings = dsnorming.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =15");

                        //        if (PRatings.Length > 0 && PRatings[0][4 + score].ToString() != null)
                        //        {
                        //            P_rating = Convert.ToDouble(PRatings[0][4 + score].ToString());
                        //        }

                        //        DataRow[] ratings = dsRating.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =15");


                        //        if (ratings.Length > 0 && ratings[0][4 + score].ToString() != null)
                        //        {
                        //            rating = ratings[0][4 + score].ToString();
                        //        }


                        //        #endregion
                        //    }
                        //    if (age > 21)//age is >21
                        //    {
                        //        #region age_greter_21

                        //        DataRow[] PRatings = dsnorming.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =21");


                        //        if (PRatings.Length > 0 && PRatings[0][4 + score].ToString() != null)
                        //        {
                        //            P_rating = Convert.ToDouble(PRatings[0][4 + score].ToString());
                        //        }

                        //        DataRow[] ratings = dsRating.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =21");


                        //        if (ratings.Length > 0 && ratings[0][4 + score].ToString() != null)
                        //        {
                        //            rating = ratings[0][4 + score].ToString();
                        //        }

                        //        #endregion
                        //    }
                        //}


                        if ((age >= 15 && age <= 21) || (age >= 30 && age <= 46)) //15-21 and 30-46
                        {
                            #region age_b/w_15-21 and 30-46

                            DataRow[] PRatings = dsnorming.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =" + age);


                            if (PRatings.Length > 0 && PRatings[0][4 + score].ToString() != null)
                            {
                                P_rating = Convert.ToDouble(PRatings[0][4 + score].ToString());
                            }

                            DataRow[] ratings = dsRating.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =" + age);


                            if (ratings.Length > 0 && ratings[0][4 + score].ToString() != null)
                            {
                                rating = ratings[0][4 + score].ToString();
                            }


                            #endregion
                        }
                        else if (age >= 22 && age <= 29)//age is >=22 and <= 29
                        {
                            #region age_>=22 and <= 29

                            DataRow[] PRatings = dsnorming.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =30");

                            if (PRatings.Length > 0 && PRatings[0][4 + score].ToString() != null)
                            {
                                P_rating = Convert.ToDouble(PRatings[0][4 + score].ToString());
                            }

                            DataRow[] ratings = dsRating.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =30");

                            if (ratings.Length > 0 && ratings[0][4 + score].ToString() != null)
                            {
                                rating = ratings[0][4 + score].ToString();
                            }

                            #endregion
                        }
                        else if (age < 15) //age is less than 15
                        {
                            #region age_less_15

                            DataRow[] PRatings = dsnorming.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =15");

                            if (PRatings.Length > 0 && PRatings[0][4 + score].ToString() != null)
                            {
                                P_rating = Convert.ToDouble(PRatings[0][4 + score].ToString());
                            }

                            DataRow[] ratings = dsRating.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =15");


                            if (ratings.Length > 0 && ratings[0][4 + score].ToString() != null)
                            {
                                rating = ratings[0][4 + score].ToString();
                            }


                            #endregion
                        }
                        else if (age > 46) //age is grater than 46
                        {
                            #region age_grater_46

                            DataRow[] PRatings = dsnorming.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =46");

                            if (PRatings.Length > 0 && PRatings[0][4 + score].ToString() != null)
                            {
                                P_rating = Convert.ToDouble(PRatings[0][4 + score].ToString());
                            }

                            DataRow[] ratings = dsRating.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =46");


                            if (ratings.Length > 0 && ratings[0][4 + score].ToString() != null)
                            {
                                rating = ratings[0][4 + score].ToString();
                            }


                            #endregion
                        }



                        double New_P_rating = 0.0;
                        string measure = "";
                        if (P_rating <= 50)
                        {
                            measure = "Low";
                            New_P_rating = (50.00 - P_rating) * 2;
                        }
                        else
                        {
                            measure = "High";
                            New_P_rating = (P_rating - 50.00) * 2;
                        }

                        factortbl.Rows.Add(c_id, fno, score, rating, P_rating, measure, New_P_rating, batid);

                        #endregion
                    }

                    if (factortbl.Rows.Count == 9)
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                        using (var bulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.KeepIdentity))
                        {
                            foreach (DataColumn dc in factortbl.Columns)
                            {
                                bulkcopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                            }
                            bulkcopy.BulkCopyTimeout = 100000;
                            bulkcopy.DestinationTableName = "tblKYCandFactors";
                            bulkcopy.WriteToServer(factortbl);
                        }
                    }

                    //get top 3 personalities by New_p_rating
                    SqlDataAdapter gettop3 = new SqlDataAdapter("Select top 3 A.New_P_rating, B.factor, A.measure FROM tblKYCandFactors A,tblKYFactors B WHERE batid=" + batid + " and c_id =" + c_id + " AND A.factor_no = B.factor_no order by New_P_rating desc", con);
                    DataSet dstop3 = new DataSet();
                    gettop3.Fill(dstop3);

                    //get top 3 personalities by p_rating
                    SqlDataAdapter gettop3p = new SqlDataAdapter("Select top 3 A.P_rating, B.factor FROM tblKYCandFactors A,tblKYFactors B WHERE batid=" + batid + " and c_id =" + c_id + " AND A.factor_no = B.factor_no order by P_rating desc", con);
                    DataSet dstop3p = new DataSet();
                    gettop3p.Fill(dstop3p);

                    if (dstop3.Tables[0].Rows.Count > 0 && dstop3p.Tables[0].Rows.Count > 0)
                    {
                        SqlCommand cmdtop3 = null;
                        SqlDataAdapter strcmdchktop3 = new SqlDataAdapter("SELECT * FROM tblCandidateTop3 WHERE c_id = " + c_id, con);
                        DataSet dschktop3 = new DataSet();
                        strcmdchktop3.Fill(dschktop3);
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        if (dschktop3.Tables[0].Rows.Count == 0)
                            cmdtop3 = new SqlCommand("insert into tblCandidateTop3 values(" + c_id + ",'NULL','NULL','NULL','" + dstop3.Tables[0].Rows[0][1].ToString() + "-" + dstop3.Tables[0].Rows[0][2].ToString() + "','" + dstop3.Tables[0].Rows[1][1].ToString() + "-" + dstop3.Tables[0].Rows[1][2].ToString() + "','" + dstop3.Tables[0].Rows[2][1].ToString() + "-" + dstop3.Tables[0].Rows[2][2].ToString() + "','NULL','NULL','NULL','" + dstop3p.Tables[0].Rows[0][1].ToString() + "','" + dstop3p.Tables[0].Rows[1][1].ToString() + "','" + dstop3p.Tables[0].Rows[2][1].ToString() + "')", con);
                        else
                            cmdtop3 = new SqlCommand("update tblCandidateTop3 set  personality1='" + dstop3.Tables[0].Rows[0][1].ToString() + "-" + dstop3.Tables[0].Rows[0][2].ToString() + "',personality2='" + dstop3.Tables[0].Rows[1][1].ToString() + "-" + dstop3.Tables[0].Rows[1][2].ToString() + "',personality3='" + dstop3.Tables[0].Rows[2][1].ToString() + "-" + dstop3.Tables[0].Rows[2][2].ToString() + "',personality4='" + dstop3p.Tables[0].Rows[0][1].ToString() + "',personality5='" + dstop3p.Tables[0].Rows[1][1].ToString() + "',personality6='" + dstop3p.Tables[0].Rows[2][1].ToString() + "' where c_id=" + c_id, con);

                        int intEffectedRows = cmdtop3.ExecuteNonQuery();


                        SqlDataAdapter getstatus = new SqlDataAdapter("SELECT * FROM tblUserTestMaster WHERE batid=" + batid + " and uId = " + c_id + " and testid=" + testid, con);
                        DataSet dsstatus = new DataSet();
                        getstatus.Fill(dsstatus);
                        if (dsstatus.Tables[0].Rows.Count == 0)
                        {
                            string strcmd = "INSERT INTO tblUserTestMaster(uId,testid,batid,testStatus,factorStatus,dateofcomplete) VALUES (@uId,@testid,@batid,@testStatus,@factorStatus,@dateofcomplete)";
                            SqlCommand cmd = new SqlCommand(strcmd, con);
                            cmd.Parameters.AddWithValue("@uId", c_id);
                            cmd.Parameters.AddWithValue("@testid", testid);
                            cmd.Parameters.AddWithValue("@batid", batid);
                            cmd.Parameters.AddWithValue("@testStatus", "Complete");
                            cmd.Parameters.AddWithValue("@factorStatus", "Complete");
                            cmd.Parameters.AddWithValue("@dateofcomplete", DateTime.Now);
                            int EffectedRows = cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            string strcmd = "update tblUserTestMaster set testStatus=@testStatus,factorStatus=@factorStatus,dateofcomplete=@dateofcomplete WHERE batid=" + batid + " and uId = " + c_id + " and testid=" + testid+"";
                            SqlCommand cmd = new SqlCommand(strcmd, con);                           
                            cmd.Parameters.AddWithValue("@testStatus", "Complete");
                            cmd.Parameters.AddWithValue("@factorStatus", "Complete");
                            cmd.Parameters.AddWithValue("@dateofcomplete", DateTime.Now);
                            int EffectedRows = cmd.ExecuteNonQuery();
                        }

                        getstatus.Dispose();
                        dsstatus.Dispose();

                        if (intEffectedRows > 0)
                        {
                            flag = true;
                        }
                    }
                }
                return flag;
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            return flag;
        }
    }

}