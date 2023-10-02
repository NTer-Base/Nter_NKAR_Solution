using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using N_Ter.Base;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;

namespace N_Ter_Tasks
{
    public partial class n_ter_base : System.Web.UI.MasterPage
    {
        public int Growl_Duration = 3000;
        public string Date_Format_Java = "";
        public string Extra_JS = "";

        protected void Page_Init(object sender, System.EventArgs e)
        {
            if (Session["dt"] == null)
            {
                SessionObject objSes = new SessionObject();

                ConnectionStringSettings settings = default(ConnectionStringSettings);
                settings = ConfigurationManager.ConnectionStrings["nterConnectionString"];
                objSes.Connection = settings.ConnectionString;

                if (settings.ProviderName == "MySql.Data.MySqlClient")
                {
                    objSes.DB_Type = 1;
                }
                else if (settings.ProviderName == "System.Data.SqlClient")
                {
                    objSes.DB_Type = 2;
                }
                else
                {
                    objSes.DB_Type = 3;
                }

                Settings objSettings = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type);
                Application["ver"] = objSettings.InitiateSyetemUpdates(Convert.ToDecimal(Application["ver"]));

                DS_Settings dsSettings = objSettings.Read();
                objSes.Task_List = dsSettings.tblsettings[0].Default_Task_List;
                objSes.Enable_Alerts = dsSettings.tblsettings[0].Enable_Alerts;
                objSes.EL1 = dsSettings.tblsettings[0].Entity_Level_1_Name;
                objSes.EL1P = dsSettings.tblsettings[0].Entity_Level_1_Name_Plural;
                objSes.EL2 = dsSettings.tblsettings[0].Entity_Level_2_Name;
                objSes.EL2P = dsSettings.tblsettings[0].Entity_Level_2_Name_Plural;
                objSes.Currency_Sbl = dsSettings.tblsettings[0].Currency_Symbol;
                objSes.Guest_Group = dsSettings.tblsettings[0].User_Group_Guest;
                objSes.ListSort = dsSettings.tblsettings[0].Task_List_Sort_Field;
                objSes.ListSortDir = dsSettings.tblsettings[0].Task_List_Sort_Dir;
                objSes.TaskHtryDir = dsSettings.tblsettings[0].Task_History_Sort_Dir;
                objSes.TaskDocDir = dsSettings.tblsettings[0].Task_Docs_Sort_Dir;
                objSes.EnableHrMsg = dsSettings.tblsettings[0].Enable_Hourly_Messages;
                objSes.EnableReading = dsSettings.tblsettings[0].Enable_Reading_Documents;
                objSes.Task_List_View = dsSettings.tblsettings[0].Task_List_View;
                objSes.TL_Task_List_View = dsSettings.tblsettings[0].TL_Task_List_View;
                objSes.Show_Help = dsSettings.tblsettings[0].Show_Help;
                objSes.GrowlDur = dsSettings.tblsettings[0].Alert_Duration;
                objSes.Task_Screen_Type = dsSettings.tblsettings[0].Task_Screen_Type;
                DS_Extra_Sections dsEtr = new N_Ter.Customizable.Custom_Lists().LoadExtraSections();
                List<DS_Extra_Sections.tblSectionsRow> drEtr = dsEtr.tblSections.Where(x => x.Section_ID == dsSettings.tblsettings[0].Alternative_Start_Page).ToList();
                if (drEtr.Count > 0)
                {
                    objSes.AltStart = drEtr[0].Page_Name;
                }
                else
                {
                    objSes.AltStart = "";
                }
                N_Ter.Common.Common_Actions objComAct = new N_Ter.Common.Common_Actions();
                objSes.RefFreq = objComAct.GetRefreshFrequencySeconds(dsSettings.tblsettings[0].Screen_Refresh_Freq);
                objSes.UnitMins = objComAct.GetUnitMinutes(dsSettings.tblsettings[0].Unit_Duration);
                objSes.WebRoot = Request.Url.GetLeftPart(UriPartial.Authority) + (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath);
                string PhysicalRootFolder = Server.MapPath("~/nter_app_uploads");
                objSes.PhysicalRoot = PhysicalRootFolder.Substring(0, PhysicalRootFolder.Length - 17);

                Session["dt"] = objSes;
            }

            CurrentPage = GetCurrentPageName();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ThemeName = "theme-default";
            help_panel.ClientIDMode = System.Web.UI.ClientIDMode.Static;

            Date_Format_Java = Constants.DateFormatJava;

            if (Session["dt"] != null)
            {
                SessionObject objSes = (SessionObject)Session["dt"];
                Growl_Duration = objSes.GrowlDur;
                ShowHelp = objSes.Show_Help;
            }

            if (!IsPostBack && Request.QueryString["msg"] != null)
            {
                System.Web.UI.ClientScriptManager cs = Page.ClientScript;
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                cs.RegisterStartupScript(this.GetType(), "STBase", "ShowSuccess('" + objURL.Decrypt(Request.QueryString["msg"]) + "');", true);
            }

            if (Session["msg"] != null)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('" + Session["msg"] + "');", true);
                Session.Remove("msg");
            }

            CleanSession();
        }

        private void CleanSession()
        {
            try
            {
                List<string> LayoutSessionPages = new List<string>();
                //LayoutSessionPages.Add("data_export.aspx");

                SessionObject objSes = (SessionObject)Session["dt"];
                Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
                objCus.CustomExcludesFromSessionClenup(ref LayoutSessionPages);

                if (!LayoutSessionPages.Contains(CurrentPage))
                {
                    if (Session["ctrl"] != null)
                    {
                        Session.Remove("ctrl");
                    }
                    if (Session["files"] != null)
                    {
                        Session.Remove("files");
                    }
                }

                //any other cleaning
            }
            catch (Exception ex)
            {
                string PhysicalRootFolder = Server.MapPath("~/nter_app_uploads");
                System.IO.File.AppendAllText(PhysicalRootFolder + "\\errors.txt", string.Format("{0:" + N_Ter.Common.Constants.DateFormat + " hh:mm}", DateTime.Now) + " : " + ex.Message + " - " + ex.StackTrace + Environment.NewLine);
            }
        }

        private string GetCurrentPageName()
        {
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            return sRet;
        }

        public string PageClass { get; set; }

        public string ThemeName { get; set; }

        public string CurrentPage { get; set; }

        public bool ShowHelp
        {
            set { 
                help_panel.Visible = value;
                if (value == true)
                {
                    if (Extra_JS.Trim() == "")
                    {
                        SessionObject objSes = (SessionObject)Session["dt"];

                        Page.Header.Controls.Add(new LiteralControl("<link href=\"assets/stylesheets/help.min.css\" rel=\"stylesheet\" />"));
                        Extra_JS = "<script src=\"assets/javascripts/help.min.js\"></script>";
                        N_Ter.Customizable.Custom_Lists objExtra = new N_Ter.Customizable.Custom_Lists();
                        ltrHelp.Text = new Common_Actions().GetHelp(ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type), CurrentPage, Request.QueryString["rpt"], objExtra.LoadReports(), objExtra.LoadExtraSections());
                    }
                }
            }
        }
    }
}