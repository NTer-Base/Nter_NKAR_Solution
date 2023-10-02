using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Linq;

namespace N_Ter_Tasks
{
    public partial class reports : System.Web.UI.Page
    {
        private controls_customizable.cuz_reports objCuz = null;

        protected void Page_Init(object sender, System.EventArgs e)
        {
            if (Request.QueryString["rpt"] == null)
            {
                Response.Redirect("error.aspx?");
            }
            else
            {
                SessionObject objSes = (SessionObject)Session["dt"];
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                int Report_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["rpt"])));

                Users objUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
                if (objUsers.isReportAvailable(objSes.UserID, Report_ID) == false)
                {
                    Response.Redirect("no_access.aspx?");
                }
                else
                {
                    DS_Reports ds = new N_Ter.Customizable.Custom_Lists().LoadReports();
                    ltrReportName.Text = ds.tblReports.Where(x => x.Report_ID == Report_ID).First().Report_Name;

                    objCuz = (controls_customizable.cuz_reports)Page.LoadControl("~/controls_customizable/cuz_reports.ascx");
                    objCuz.Report_ID = Report_ID;
                    divContent.Controls.Clear();
                    divContent.Controls.Add(objCuz);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}