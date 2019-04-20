
using System.Globalization;
using System.Threading;

/// <summary>
/// Summary description for BaseClass
/// </summary>
public class BaseClass : System.Web.UI.Page
{
    public BaseClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    protected override void InitializeCulture()
    {
        if (Session["myapplication.language"] != null)
        {
            string selectedLanguage = Session["myapplication.language"] as string;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(selectedLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedLanguage);
        }
        // base.InitializeCulture();
    }
}
