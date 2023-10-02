using System;
using System.Web;
using System.Web.Http;

namespace N_Ter_Tasks
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            string PhysicalRootFolder = HttpContext.Current.Server.MapPath("~/nter_app_uploads");
            System.IO.File.AppendAllText(PhysicalRootFolder + "\\errors.txt", string.Format("{0:" + N_Ter.Common.Constants.DateFormat + " hh:mm}", DateTime.Now) + " : " + ex.Message + " - " + ex.StackTrace + Environment.NewLine);

            Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority) + (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath) + "/?fwd=" + new N_Ter.Common.Common_Actions().GetReletiveURL(Request));
        }
    }
}