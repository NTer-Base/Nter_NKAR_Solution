using N_Ter.Structures;
using System;


namespace N_Ter_Tasks
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Init(object sender, System.EventArgs e)
        {
            if (Session["dt"] != null)
            {
                SessionObject objSes = (SessionObject)Session["dt"];
                if (objSes != null)
                {
                    if (objSes.AuthType == "db" || objSes.AuthType == "cookie" || objSes.AuthType == "ad")
                    {
                        if (Request.Cookies["n_ter_tasks_cookie"] != null)
                        {
                            Response.Cookies["n_ter_tasks_cookie"].Expires = DateTime.Now.AddDays(-1);
                        }
                        Session.RemoveAll();
                        Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority) + (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath));
                    }
                    else if (objSes.AuthType == "azure")
                    {
                        Response.Redirect("logout_azure_ad.aspx?");
                    }
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

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}