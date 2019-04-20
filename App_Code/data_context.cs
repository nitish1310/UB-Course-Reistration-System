using log4net;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for data_context
/// </summary>
public class data_context
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public data_context()
    {
    }
    public Boolean sendemail(string to, string cc, string bcc, string subject, string body)
    {
        try
        {
            using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage())
            {
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["DisplayName"]);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                if (to != null)
                    mailMessage.To.Add(new MailAddress(to));
                if (cc != null)
                    mailMessage.CC.Add(new MailAddress(cc));
                if (bcc != null)
                    mailMessage.Bcc.Add(new MailAddress(bcc));
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
            Log.Error(ex);
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

    //public void email(string recepientEmail, string subject, string body)
    //{
    //    try
    //    {
    //        using (MailMessage mailMessage = new MailMessage())
    //        {
    //            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["DisplayName"]);
    //            mailMessage.Subject = subject;
    //            mailMessage.Body = body;
    //            mailMessage.IsBodyHtml = true;
    //            mailMessage.To.Add(new MailAddress(recepientEmail));
    //            SmtpClient smtp = new SmtpClient();
    //            smtp.Host = ConfigurationManager.AppSettings["Host"];
    //            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
    //            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
    //            NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
    //            NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
    //            smtp.UseDefaultCredentials = true;
    //            smtp.Credentials = NetworkCred;
    //            smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
    //            smtp.Send(mailMessage);
    //            smtp.Dispose();
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }
    //}

    //public void email(string recepientEmail, string cc, string subject, string body)
    //{
    //    try
    //    {
    //        using (MailMessage mailMessage = new MailMessage())
    //        {
    //            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["DisplayName"]);
    //            mailMessage.Subject = subject;
    //            mailMessage.Body = body;
    //            //add to cc
    //            MailAddress copy = new MailAddress(cc);
    //            mailMessage.CC.Add(copy);
    //            mailMessage.IsBodyHtml = true;
    //            mailMessage.To.Add(new MailAddress(recepientEmail));
    //            SmtpClient smtp = new SmtpClient();
    //            smtp.Host = ConfigurationManager.AppSettings["Host"];
    //            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
    //            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
    //            NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
    //            NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
    //            smtp.UseDefaultCredentials = true;
    //            smtp.Credentials = NetworkCred;
    //            smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
    //            smtp.Send(mailMessage);
    //            smtp.Dispose();
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }
    //}

    //public void email(string recepientEmail, string cc, string filepath, string subject, string body)
    //{

    //    /** Sent a email with PDF attach with attachment
    //    * recepientEmail:- email id whoes sent  
    //    * subject:- subject data
    //    * string body:- Email content 
    //    * attachment:- file attachment 
    //    * String subject is fatch data to web.confing file
    //    * 
    //    */
    //    try
    //    {

    //        using (MailMessage mailMessage = new MailMessage())
    //        {
    //            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["DisplayName"]);
    //            mailMessage.Subject = subject;
    //            mailMessage.Body = body;
    //            //add to cc
    //            if (cc != null)
    //            {
    //                MailAddress copy = new MailAddress(cc);
    //                mailMessage.CC.Add(copy);
    //            }
    //            mailMessage.IsBodyHtml = true;
    //            mailMessage.To.Add(new MailAddress(recepientEmail));
    //            SmtpClient smtp = new SmtpClient();
    //            smtp.Host = ConfigurationManager.AppSettings["Host"];
    //            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
    //            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
    //            NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
    //            NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
    //            smtp.UseDefaultCredentials = true;
    //            smtp.Credentials = NetworkCred;
    //            smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);

    //            Attachment data = new Attachment(
    //            HttpContext.Current.Server.MapPath(filepath),
    //            MediaTypeNames.Application.Octet);
    //            mailMessage.Attachments.Add(data);
    //            smtp.Send(mailMessage);
    //            smtp.Dispose();
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }
    //}

    public int GenerateRandomNo(int min, int max)
    {
        try
        {
            Random _rdm = new Random();
            return _rdm.Next(min, max);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

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

    public string Dataencrypt(string clearText)
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

}