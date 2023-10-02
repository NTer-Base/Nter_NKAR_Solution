using N_Ter.Base;
using N_Ter.Structures;
using System;
using System.Web.UI;

namespace N_Ter_Tasks
{
    public partial class entity_level_2_inactive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                SessionObject objSes = (SessionObject)Session["dt"];

                ltrEntityL1_Name.Text = objSes.EL1;
                ltrEntityL2_Code.Text = objSes.EL2 + " Code";
                ltrEntityL2_Name.Text = objSes.EL2 + " Name";

                ltrEL2.Text = objSes.EL2P;
                ltrEL2_2.Text = objSes.EL2P;
                ltrEL2_3.Text = objSes.EL2P;

                hndEL2_ID.Value = "0";
            }
        }

        protected void cmdActivateEL2_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Entity_Level_2 objEL2 = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type);
            if (objEL2.Activate(Convert.ToInt32(hndEL2_ID.Value)) == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Database Error - " + objSes.EL2 + " cannot be Activated');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('" + objSes.EL2 + " Successfully Deactivated');", true);
                hndEL2_ID.Value = "0";
            }
        }

        protected void cmdShowActive_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("entity_level_2.aspx?");
        }
    }
}