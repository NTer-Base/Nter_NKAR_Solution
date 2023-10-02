using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;

namespace N_Ter_Tasks
{
    public partial class account_locked : System.Web.UI.Page
    {
        protected void Page_Init(object sender, System.EventArgs e)
        {
            n_ter_base pg = (n_ter_base)this.Master;
            pg.PageClass = "page-signup";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            if (objSes.UserID > 0)
            {
                if (!objSes.isLocked)
                {
                    DS_Pages ds = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type).ReadPages(objSes.UserID);
                    if (ds.tblpages.Rows.Count > 0)
                    {
                        Response.Redirect("~/" + ds.tblpages[0].Page_Name + "?");
                    }
                    else
                    {
                        Response.Redirect("~/error.aspx?");
                    }
                }
            }
            else
            {
                Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority) + (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath));
            }
        }
    }
}