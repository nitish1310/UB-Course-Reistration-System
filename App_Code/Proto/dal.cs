using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Web.Mail;
using System.IO;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Imaging;



/// <summary>
/// Summary description for dal
/// </summary>
public class dal
{
    //public string strcon = "Database=test_engine;UID=test_engine;PWD=test_engine123;Data Source='P-IV';Persist Security Info=False;";
    public string strcon;
    public int imgHeight, imgWidth;

    SqlConnection con;
    static private Byte[] m_Key = new Byte[8];
    static private Byte[] m_IV = new Byte[8]; 

	public dal()
	{
        //ConnectionStringSettings cn = new ConnectionStringSettings();
        strcon = ConfigurationManager.ConnectionStrings["career_portalConnectionString"].ConnectionString.ToString();
		//
		// TODO: Add constructor logic here
		//        
	}

    public void image_process(string srvpath, int maxWidth, int maxHeight)
    {
        System.Drawing.Image picture;
        picture = System.Drawing.Image.FromFile(srvpath);

        //			double maxWidth = 600;
        //			double maxHeight = 600;			
        double imgHeight1, imgWidth1;

        imgHeight1 = picture.Height;
        imgWidth1 = picture.Width;

        if (imgWidth1 > maxWidth || imgHeight1 > maxHeight)
        {
            //Determine what dimension is off by more
            double deltaWidth = imgWidth1 - maxWidth;
            double deltaHeight = imgHeight1 - maxHeight;

            double scaleFactor;

            if (deltaHeight > deltaWidth)
            {
                //Scale by the height
                scaleFactor = Convert.ToDouble(maxHeight / imgHeight1);
            }
            else
            {
                //Scale by the Width
                scaleFactor = Convert.ToDouble(maxWidth / imgWidth1);
            }

            imgWidth = Convert.ToInt32(imgWidth1 * scaleFactor);
            imgHeight = Convert.ToInt32(imgHeight1 * scaleFactor);
        }
        else
        {
            imgWidth = Convert.ToInt32(imgWidth1);
            imgHeight = Convert.ToInt32(imgHeight1);
        }

    }

    //		public int ExecNonQuery_chat(string s_query)
    //		{
    //			string Connchat = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("/chat_room/chat.mdb");
    //			OleDbConnection con2;
    //
    //			con2 = new OleDbConnection(Connchat);
    //			// Check connectionstate it is open or close
    //			if(con2.State==ConnectionState.Closed)
    //			{
    //				// call function Open to Open the database connection 
    //				con2.Open();
    //			}
    //
    //			// Create Object Of SqlCommand and pass Query (s_query) and Connection Object (con)
    //			OleDbCommand cmd2 = new OleDbCommand(s_query,con2);
    //
    //			// call Function ExecuteNonQuery for perform add.edit or delete operation on database
    //
    //			int intEffectedRows =cmd2.ExecuteNonQuery();
    //
    //			// Call Function ConClose for Close the Database Connection
    //
    //			if(con2.State==ConnectionState.Open)
    //			{
    //				// call function Close to close the database connection 
    //				con2.Close();
    //			}
    //
    //			// Return Integer Value (Number of rows effected in database)
    //
    //			return intEffectedRows;
    //		}



    //********************************************************************************************
    //FunctionName    : ExecQuery
    //InputParameter  : s_query
    //returnValue     : none
    //Description     : Function Call When User want Add / Edit / Or Delete Operation On Database
    //*******************************************************************************************

    public void ExecQuery(string s_query)
    {

        // Call function  ConOpen for Open the Database Connection

        ConOpen();

        // Create Object Of SqlCommand and pass Query (s_query) and Connection Object (con)

        SqlCommand cmd = new SqlCommand(s_query, con);

        // call Function ExecuteNonQuery for perform add.edit or delete operation on database

        cmd.ExecuteNonQuery();

        // Call Function ConClose for Close the Database Connection

        ConClose();
    }

    //********************************************************************************************
    //FunctionName    : ExecNonQuery
    //InputParameter  : s_query
    //returnValue     : integer Value
    //Description     : Function Call When User want Add / Edit / Delete Operation on Database If Function  
    //                  0 Add / Edit / Delete Operation Failed Otherwise Returns number of rows effected
    //*******************************************************************************************

    public int ExecNonQuery(string s_query)
    {

        // Call function  ConOpen for Open the Database Connection

        ConOpen();

        // Create Object Of SqlCommand and pass Query (s_query) and Connection Object (con)
        SqlCommand cmd = new SqlCommand(s_query, con);

        // call Function ExecuteNonQuery for perform add.edit or delete operation on database

        int intEffectedRows = cmd.ExecuteNonQuery();

        // Call Function ConClose for Close the Database Connection

        ConClose();

        // Return Integer Value (Number of rows effected in database)

        return intEffectedRows;
    }

    //********************************************************************************************
    //FunctionName    : ExecScal
    //InputParameter  : s_query
    //returnValue     : string Value
    //Description     : Function Call When User want only one record ( first record of first row 
    //					and first column) from database and return string value
    //*******************************************************************************************

    public string ExecScal(string s_query)
    {

        // Call function  ConOpen for Open the Database Connection

        ConOpen();

        // Create Object Of SqlCommand and pass Query (s_query) and Connection Object (con)
        SqlCommand cmd = new SqlCommand(s_query, con);

        // call Function ExecuteSclar for getting Data Of First Column of First Row
        string str = cmd.ExecuteScalar().ToString();

        // Call Function ConClose for Close the Database Connection
        ConClose();

        // Return String Value 
        return str;
    }
    //********************************************************************************************
    //FunctionName    : ExecDataSet
    //InputParameter  : s_query
    //returnValue     : DataSet dr
    //Description     : Function Call When User Read The information from database in DisConnected 
    //					function return DataSet
    //*******************************************************************************************


    public DataSet ExecDataSet(string s_query)
    {
        // Call function  ConOpen for Open the Database Connection

        ConOpen();

        // Create Object DataAdapter Class and pass(sqlquerry,connection)

        SqlDataAdapter da = new SqlDataAdapter(s_query, con);

        // Create DataSet Object

        DataSet ds = new DataSet();

        //DataAdapter Object Fill DataSet
        da.Fill(ds);
        //da.Update(
        // Call Function ConClose for Close the Database Connection
        ConClose();

        //Return DataSet
        return ds;
    }

    //********************************************************************************************
    //FunctionName    : ExecDataReader
    //InputParameter  : s_query
    //returnValue     : SqlDataReader dr
    //Description     : Function Call When User Read The information from database
    //					function return SqlDataReader
    //*******************************************************************************************


    public SqlDataReader ExecDataReader(string s_query)
    {
        // Call function  ConOpen for Open the Database Connection
        ConOpen();

        // Create Object of DataReader Class
        SqlDataReader dr;

        // Create Object Of SqlCommand and pass Query (s_query) and Connection Object (con)
        SqlCommand cmd = new SqlCommand(s_query, con);

        //Call ExecuteReader Function with Command Object and Return DataReader Class Object
        dr = cmd.ExecuteReader();

        //Return DataReader Class Object
        return dr;
    }

    //********************************************************************************************
    //FunctionName    : ConClose
    //InputParameter  : none
    //returnValue     : none
    //Description     : Function Call When User Want to close the database connection
    //*******************************************************************************************


    public void ConClose()
    {
        // Check connectionstate it is open or close
        if (con.State == ConnectionState.Open)
        {
            // call function Close to close the database connection 
            con.Close();
        }
    }


    //********************************************************************************************
    //FunctionName    : ConOpen
    //InputParameter  : none
    //returnValue     : none
    //Description     : Function Call When User Want to open the database connection
    //AddedBy         : Vinay                      AddedOn   : 15/06/2006
    //UpdatedBy       :                            UpdatedOn :
    //Reason          :
    //*******************************************************************************************


    public void ConOpen()
    {
        // Creatye object of Connection class and pass connection string (strcon)
        con = new SqlConnection(strcon);
        // Check connectionstate it is open or close
        if (con.State == ConnectionState.Closed)
        {
            // call function Open to Open the database connection 
            con.Open();
        }
    }

    //********************************************************************************************
    //FunctionName    : DateConvert
    //InputParameter  : string Date
    //returnValue     : string 
    //Description     : Function Call When User Want to Convert Date From one format to another format 
    //					( from mm/dd/yy to dd/mm/yy or from dd/mm/yy to mm.dd.yy ) and return it in string format
    //AddedBy         : Vinay                      AddedOn   : 15/06/2006
    //UpdatedBy       : Sachin Patil              UpdatedOn  :19/06/2017
    //Reason          :
    //*******************************************************************************************

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

    //********************************************************************************************
    //FunctionName    : getFileSaveName
    //InputParameter  : FileName,ServerPath,FolderName
    //returnValue     : none
    //Description     : Function Call When User Want to Upload the File  
    //					( from mm/dd/yy to dd/mm/yy or from dd/mm/yy to mm.dd.yy ) and return it in string format
    //AddedBy         : Vinay                      AddedOn   : 15/06/2006
    //UpdatedBy       :                            UpdatedOn :
    //Reason          :
    //*******************************************************************************************


    public void getFileSaveName(ref string strFile, ref string strSerPath, string FolderName)
    {

        strSerPath = strSerPath + "/" + FolderName + "/";
        string datetime = System.DateTime.Now.ToString().Trim();
        datetime = datetime.Replace('/', '_');
        datetime = datetime.Replace(' ', '_');
        datetime = datetime.Replace(':', '_');
        strFile = datetime + strFile;
        

    }


    //********************************************************************************************
    //FunctionName    : AddEditData
    //InputParameter  : string StoreProcedure name,string array of input parameter
    //returnValue     : int
    //Description     : Function Call When User Want to AddEdir Records in database  
    //					
    //AddedBy         : Vinay                      AddedOn   : 15/06/2006
    //UpdatedBy       :                            UpdatedOn :
    //Reason          :
    //*******************************************************************************************


    public int AddEditData(string strProcName, string[,] inParam)
    {
        // Call function  ConOpen for Open the Database Connection
        ConOpen();
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
        ConClose();
        //Return No of Affected Row If SqlCommand Execute against DataBase Successfully
        return row;
    }
    //********************************************************************************************
    //FunctionName    : getSqlReader
    //InputParameter  : string StoreProcedure name,string array of input parameter
    //returnValue     : SqlDataReader
    //Description     : Function Call When User Want Records in DataReader Object  
    //AddedBy         : Vinay                      AddedOn   : 15/06/2006
    //UpdatedBy       :                            UpdatedOn :
    //Reason          :
    //*******************************************************************************************
    public SqlDataReader getSqlReader(string strProcName, string[,] inParam)
    {

        // Call function  ConOpen for Open the Database Connection
        ConOpen();
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
        //Call Execute Reader Function and Return DataReader Object
        return cmd.ExecuteReader();

    }


    public DataSet getDataSet(string strProcName, string[,] inParam)
    {

        // Call function  ConOpen for Open the Database Connection
        ConOpen();
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
            if (inParam[i, 1].Equals("SmallInt"))
            {
                sp.SqlDbType = SqlDbType.SmallInt;
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
    public DataSet getDataSet(string strProcName)
    {

        // Call function  ConOpen for Open the Database Connection
        ConOpen();
        //Create SqlCommand Object and Pass StoreProcedureName and Connection Object
        SqlCommand cmd = new SqlCommand(strProcName, con);
        //Specify That Command Type StoreProcedure
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //Call Execute Reader Function and Return DataReader Object
        DataSet ds = new DataSet();
        da.Fill(ds);
        return ds;

    }
    public void sendMail(string strError, string strPath)
    {
        //MailMessage msg = new MailMessage();
        //msg.From = "dadivekar@vamnicom.org";
        //msg.Cc = "vinay@ikf.co.in";
        ////msg.Bcc="accounts@ikf.co.in,Billing.ikf.co.in";
        //msg.To = "lokesh@ikf.co.in";
        ////msg.To="vina y@ikf.co.in";

        //msg.BodyFormat = System.Web.Mail.MailFormat.Html;
        //msg.Subject = "Error : " + strPath;


        //msg.Body = strError;
        //SmtpMail.Send(msg);
    }

    //////////////////////////
    //Function to encrypt data
    public string EncryptData(String strKey, String strData)
    {
        string strResult;		//Return Result

        //1. String Length cannot exceed 90Kb. Otherwise, buffer will overflow. See point 3 for reasons
        if (strData.Length > 92160)
        {
            strResult = "Error. Data String too large. Keep within 90Kb.";
            return strResult;
        }

        //2. Generate the Keys
        if (!InitKey(strKey))
        {
            strResult = "Error. Fail to generate key for encryption";
            return strResult;
        }

        //3. Prepare the String
        //	The first 5 character of the string is formatted to store the actual length of the data.
        //	This is the simplest way to remember to original length of the data, without resorting to complicated computations.
        //	If anyone figure a good way to 'remember' the original length to facilite the decryption without having to use additional function parameters, pls let me know.
        strData = String.Format("{0,5:00000}" + strData, strData.Length);


        //4. Encrypt the Data
        byte[] rbData = new byte[strData.Length];
        ASCIIEncoding aEnc = new ASCIIEncoding();
        aEnc.GetBytes(strData, 0, strData.Length, rbData, 0);

        DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();

        ICryptoTransform desEncrypt = descsp.CreateEncryptor(m_Key, m_IV);


        //5. Perpare the streams:
        //	mOut is the output stream. 
        //	mStream is the input stream.
        //	cs is the transformation stream.
        MemoryStream mStream = new MemoryStream(rbData);
        CryptoStream cs = new CryptoStream(mStream, desEncrypt, CryptoStreamMode.Read);
        MemoryStream mOut = new MemoryStream();

        //6. Start performing the encryption
        int bytesRead;
        byte[] output = new byte[1024];
        do
        {
            bytesRead = cs.Read(output, 0, 1024);
            if (bytesRead != 0)
                mOut.Write(output, 0, bytesRead);
        } while (bytesRead > 0);

        //7. Returns the encrypted result after it is base64 encoded
        //	In this case, the actual result is converted to base64 so that it can be transported over the HTTP protocol without deformation.
        if (mOut.Length == 0)
            strResult = "";
        else
            strResult = Convert.ToBase64String(mOut.GetBuffer(), 0, (int)mOut.Length);

        return strResult;
    }

    //////////////////////////
    //Function to decrypt data
    public string DecryptData(String strKey, String strData)
    {
        string strResult;

        //1. Generate the Key used for decrypting
        if (!InitKey(strKey))
        {
            strResult = "Error. Fail to generate key for decryption";
            return strResult;
        }

        //2. Initialize the service provider
        int nReturn = 0;
        DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();
        ICryptoTransform desDecrypt = descsp.CreateDecryptor(m_Key, m_IV);

        //3. Prepare the streams:
        //	mOut is the output stream. 
        //	cs is the transformation stream.
        MemoryStream mOut = new MemoryStream();
        CryptoStream cs = new CryptoStream(mOut, desDecrypt, CryptoStreamMode.Write);

        //4. Remember to revert the base64 encoding into a byte array to restore the original encrypted data stream
        byte[] bPlain = new byte[strData.Length];
        try
        {
            bPlain = Convert.FromBase64CharArray(strData.ToCharArray(), 0, strData.Length);
        }
        catch (Exception)
        {
            strResult = "Error. Input Data is not base64 encoded.";
            return strResult;
        }

        long lRead = 0;
        long lTotal = strData.Length;

        try
        {
            //5. Perform the actual decryption
            while (lTotal >= lRead)
            {
                cs.Write(bPlain, 0, (int)bPlain.Length);
                //descsp.BlockSize=64
                lRead = mOut.Length + Convert.ToUInt32(((bPlain.Length / descsp.BlockSize) * descsp.BlockSize));
            };

            ASCIIEncoding aEnc = new ASCIIEncoding();
            strResult = aEnc.GetString(mOut.GetBuffer(), 0, (int)mOut.Length);

            //6. Trim the string to return only the meaningful data
            //	Remember that in the encrypt function, the first 5 character holds the length of the actual data
            //	This is the simplest way to remember to original length of the data, without resorting to complicated computations.
            String strLen = strResult.Substring(0, 5);
            int nLen = Convert.ToInt32(strLen);
            strResult = strResult.Substring(5, nLen);
            nReturn = (int)mOut.Length;

            return strResult;
        }
        catch (Exception)
        {
            strResult = "Error. Decryption Failed. Possibly due to incorrect Key or corrputed data";
            return strResult;
        }
    }

    /////////////////////////////////////////////////////////////
    //Private function to generate the keys into member variables
    static private bool InitKey(String strKey)
    {
        try
        {
            // Convert Key to byte array
            byte[] bp = new byte[strKey.Length];
            ASCIIEncoding aEnc = new ASCIIEncoding();
            aEnc.GetBytes(strKey, 0, strKey.Length, bp, 0);

            //Hash the key using SHA1
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            byte[] bpHash = sha.ComputeHash(bp);

            int i;
            // use the low 64-bits for the key value
            for (i = 0; i < 8; i++)
                m_Key[i] = bpHash[i];

            for (i = 8; i < 16; i++)
                m_IV[i - 8] = bpHash[i];

            return true;
        }
        catch (Exception)
        {
            //Error Performing Operations
            return false;
        }
    }

    public int StoreDataSet(DataSet ds,string tbl_name,string s_query)
    {
        // Call function  ConOpen for Open the Database Connection

        ConOpen();

        // Create Object DataAdapter Class and pass(sqlquerry,connection)

        SqlDataAdapter da = new SqlDataAdapter(s_query, con);

        SqlCommandBuilder mySqlCommandBuilder = new SqlCommandBuilder(da);
        
        int i = da.Update(ds);
                
        // Call Function ConClose for Close the Database Connection
        ConClose();

        return i;
    }

    public int UpdateValidity(string promoter, int duration, string status, string start_date)
    {
        ConOpen();
        SqlCommand cmd = new SqlCommand("UpdateValidity", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@duration", SqlDbType.Int).Value = duration;
        cmd.Parameters.Add("@promoter", SqlDbType.Int).Value =Convert.ToInt32(promoter.ToString());
        cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = status.ToString();
        cmd.Parameters.Add("@start_date", SqlDbType.VarChar).Value = start_date.ToString();
        int row = cmd.ExecuteNonQuery();
        ConClose();
        return row;
    }

    public int UpdateValidityIndividual(string promoter, string code, int duration, string status, string start_date)
    {
        ConOpen();
        SqlCommand cmd = new SqlCommand("UpdateValidityIndividualy", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@duration", SqlDbType.Int).Value = duration;
        cmd.Parameters.Add("@promoter", SqlDbType.Int).Value = Convert.ToInt32(promoter.ToString());
        cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = status.ToString();
        cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = code.ToString();
        cmd.Parameters.Add("@start_date", SqlDbType.VarChar).Value = start_date.ToString();
        int row = cmd.ExecuteNonQuery();
        ConClose();
        return row;
    }

    public int UpdateStatus()
    {
        ConOpen();
        SqlCommand cmd = new SqlCommand("Update_Status", con);
        cmd.CommandType = CommandType.StoredProcedure;
        int row = cmd.ExecuteNonQuery();
        ConClose();
        return row;
    }
}
