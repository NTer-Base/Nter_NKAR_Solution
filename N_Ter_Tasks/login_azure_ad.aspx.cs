using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using N_Ter.Base;
using N_Ter.Security;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace N_Ter_Tasks
{
    public partial class login_azure_ad : System.Web.UI.Page
    {
        protected void Page_Init(object sender, System.EventArgs e)
        {
            n_ter_base pg = (n_ter_base)this.Master;
            pg.PageClass = "page-signin";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Application["val"] == null)
            {
                string filename = System.IO.Path.GetFileName(Request.Path);

                if (filename.Trim() != "invalid_license.aspx")
                {
                    string strBasePath = Server.MapPath("~/assets");
                    strBasePath = strBasePath.Substring(0, strBasePath.Length - 6);

                    string SubscriotionKey = "";
                    if (N_Ter.Licence.Validate_N_Ter.IsValidLicenceKey(strBasePath, ref SubscriotionKey) == false)
                    {
                        Response.Redirect("invalid_license.aspx?");
                    }
                    else
                    {
                        if (SubscriotionKey.Trim() != "")
                        {
                            DateTime dtExpDate = new DateTime();
                            if (N_Ter.Licence.Validate_N_Ter.IsValidSubscriptionKey(strBasePath, SubscriotionKey, ref dtExpDate) == false)
                            {
                                Response.Redirect("subscription.aspx?");
                            }
                        }
                        else
                        {
                            Application["val"] = "true";
                        }
                    }
                }
            }

            SessionObject objSes = (SessionObject)Session["dt"];

            if (IsPostBack == false)
            {
                if (Request.IsAuthenticated)
                {
                    string Username = Context.User.Identity.Name;

                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Azure AD Authenticated, Login in to N-Ter - " + Username + "');", true);

                    Users objUser = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);

                    string[] usernameParts = Username.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                    if (usernameParts.Length > 1)
                    {
                        Username = usernameParts[1];
                    }

                    if (objUser.ValidateUsername(Username))
                    {
                        objSes.AuthType = "azure";
                        Login_User(objUser);
                    }
                    else
                    {
                        cs.RegisterStartupScript(this.GetType(), "AKey_1", "ShowError('You are not a Registered User in N-Ter');", true);
                    }
                }
                else
                {
                    HttpContext.Current.GetOwinContext().Authentication.Challenge(
                        new AuthenticationProperties { RedirectUri = "/login_azure_ad.aspx?" },
                        OpenIdConnectAuthenticationDefaults.AuthenticationType);
                }
            }
        }

        private void Login_User(Users objUser)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            ObjectCreator.GetTransactions_Log(objSes.Connection, objSes.DB_Type).AddEvent(objUser.UserRow.User_ID, true, "Logged in");
            Tasks objTasks = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            objSes.MaxHID = objTasks.GetMaxTaskHistoryID();
            objSes.UserID = objUser.UserRow.User_ID;
            objSes.FullName = objUser.UserRow.First_Name + " " + objUser.UserRow.Last_Name;
            objSes.SesKey = objUser.UserRow.Session_Key;
            objSes.AccLevel = objUser.UserRow.Access_Level;
            objSes.PWExpiry = DateTime.Today.AddMonths(1);
            objSes.isLocked = objUser.UserRow.isDeleted;

            Settings objSettings = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type);
            DS_Settings dsSettings = objSettings.Read();

            N_Ter.Common.Common_Actions objComAct = new N_Ter.Common.Common_Actions();
            bool UpdateLogintimeRequired = false;
            objComAct.CheckUserLocking(dsSettings, objUser, ref objSes, ref UpdateLogintimeRequired);
            if (UpdateLogintimeRequired)
            {
                objUser.UpdateLoginUserTime(objUser.UserRow.User_ID);
            }
            objComAct.AdjustUserSessionParams(objUser.UserRow.Default_Task_List, objUser.UserRow.Task_List_View, objUser.UserRow.TL_Task_List_View, objUser.UserRow.Show_Help, objUser.UserRow.Task_Screen_Type, ref objSes);
            objComAct.AdjustAdminRole(objUser, ref objSes);

            Session["dt"] = objSes;

            RunStartupProcess();

            DS_Pages ds = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type).ReadPages(objUser.UserRow.User_ID);
            if (ds.tblpages.Rows.Count > 0)
            {
                if (Request.QueryString["fwd"] != null)
                {
                    GoToSpecificPage(ds);
                }
                else
                {
                    Response.Redirect("~/" + ds.tblpages[0].Page_Name + "?");
                }
            }
            else
            {
                Response.Redirect("~/error.aspx?");
            }
        }

        private void GoToSpecificPage(DS_Pages dsPages)
        {
            URL_Manager objURL = new URL_Manager();
            string[] strFwURLParts = objURL.Decrypt(Convert.ToString(Request.QueryString["fwd"])).Split('?');
            string PageName = "";
            string Trail = "";
            if (strFwURLParts.Length > 0)
            {
                PageName = strFwURLParts[0];
                if (PageName.Trim() != "" && PageName != "/login_azure_ad.aspx")
                {
                    if (dsPages.tblpages.Where(x => x.Page_Name.Trim() == PageName.Trim().Substring(1)).Count() > 0)
                    {
                        if (strFwURLParts.Length > 1)
                        {
                            Trail = strFwURLParts[1];
                        }
                        System.Collections.Specialized.NameValueCollection nameValues = System.Web.HttpUtility.ParseQueryString(Trail);
                        if (nameValues.Count > 0)
                        {
                            Response.Redirect(PageName + "?" + nameValues.ToString());
                        }
                        else
                        {
                            Response.Redirect(PageName + "?");
                        }
                    }
                    else
                    {
                        Response.Redirect("~/" + dsPages.tblpages[0].Page_Name + "?");
                    }
                }
                else
                {
                    Response.Redirect("~/" + dsPages.tblpages[0].Page_Name + "?");
                }
            }
            else
            {
                Response.Redirect("~/" + dsPages.tblpages[0].Page_Name + "?");
            }
        }

        private void RunStartupProcess()
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            N_Ter.PeriodicalAutomations.Automations objAuto = new N_Ter.PeriodicalAutomations.Automations();
            objAuto.DailyAutomations(objSes);
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                HttpContext.Current.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = "/login_azure_ad.aspx?" },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
            else
            {
                string callbackUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/login_azure_ad.aspx?";

                HttpContext.Current.GetOwinContext().Authentication.SignOut(
                    new AuthenticationProperties { RedirectUri = callbackUrl },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType,
                    CookieAuthenticationDefaults.AuthenticationType);
            }
        }
    }
}