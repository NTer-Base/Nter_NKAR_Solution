using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Web.UI;

namespace N_Ter_Tasks
{
    public partial class change_password : System.Web.UI.Page
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
                if (objSes.PWExpiry < DateTime.Now)
                {
                    divRenew.Visible = true;
                    divLogin.Visible = false;
                }
                else
                {
                    divRenew.Visible = false;
                    divLogin.Visible = true;
                }

                cmdSubmit.OnClientClick = "return ValidateChangePW('" + txtCurrentPassword.ClientID + "', '" + txtNewPassword.ClientID + "', '" + txtRetypePassword.ClientID + "');";
            }
            else
            {
                Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority) + (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath));
            }
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Users objUser = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
            DS_Users ds = objUser.Read(objSes.UserID);
            DS_Users.tblusersRow dr = ds.tblusers[0];

            if (txtCurrentPassword.Text.Trim() == "")
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Current Password is required');", true);
            }
            else
            {
                if (objUser.ValidateUser(dr.Username.ToString().Trim(), txtCurrentPassword.Text.Trim()))
                {
                    if (txtRetypePassword.Text.Trim() == "")
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Retype Password is required');", true);
                    }
                    if (!txtNewPassword.Text.Trim().Equals(txtRetypePassword.Text.Trim()))
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Password doesn't match');", true);
                    }
                    else
                    {
                        if (!objUser.ChangePassword(objSes.UserID, txtNewPassword.Text))
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('User Password could not be Updated');", true);
                        }
                        else
                        {
                            UpdatePWExpiry(objSes);
                            objUser.UpdateLoginUserTime(objSes.UserID);

                            divRenew.Visible = false;
                            divLogin.Visible = true;

                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('User Password successfully Updated');", true);
                        }
                    }
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Current Password is invalid');", true);
                }
            }
        }

        private void UpdatePWExpiry(SessionObject objSes)
        {
            Settings objSettings = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type);
            DS_Settings dsSettings = objSettings.Read();

            N_Ter.Common.Common_Actions objComAct = new N_Ter.Common.Common_Actions();

            objSes.PWExpiry = objComAct.AddPeriodType(DateTime.Now, dsSettings.tblsettings[0].PW_Expires_After);

            Session["dt"] = objSes;
        }

        protected void cmdLogin_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
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
}