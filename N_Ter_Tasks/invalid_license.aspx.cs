using System;

namespace N_Ter_Tasks
{
    public partial class invalid_license : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            n_ter_base pg = (n_ter_base)this.Master;
            pg.PageClass = "page-signin";

            string strBasePath = Server.MapPath("~/assets");
            strBasePath = strBasePath.Substring(0, strBasePath.Length - 7);

            string SubscriptionKey = "";
            if (N_Ter.Licence.Validate_N_Ter.IsValidLicenceKey(strBasePath, ref SubscriptionKey) == true)
            {
                if (SubscriptionKey.Trim() != "")
                {
                    strBasePath = Server.MapPath("~/nter_app_uploads");
                    DateTime dtExpDate = DateTime.Today;
                    if (N_Ter.Licence.Validate_N_Ter.IsValidSubscriptionKey(strBasePath, SubscriptionKey, ref dtExpDate) == false)
                    {
                        Response.Redirect("subscription.aspx?");
                    }
                    else
                    {
                        Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority) + (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath));
                    }
                }
                else
                {
                    Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority) + (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath));
                }
            }

            txtAuthCode.ReadOnly = true;
            txtAuthCode.Text = new N_Ter.Licence.Generate_Keys().GetAuthorizationKey();
        }
    }
}