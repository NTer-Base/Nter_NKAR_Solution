using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Web;

namespace N_Ter_Tasks
{
    public partial class logout_azure_ad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.RemoveAll();
            string callbackUrl = Request.Url.GetLeftPart(UriPartial.Authority) + (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath) + "/login_azure_ad.aspx?";

            HttpContext.Current.GetOwinContext().Authentication.SignOut(
                new AuthenticationProperties { RedirectUri = callbackUrl },
                OpenIdConnectAuthenticationDefaults.AuthenticationType,
                CookieAuthenticationDefaults.AuthenticationType);
        }
    }
}