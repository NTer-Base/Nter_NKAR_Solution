using N_Ter.Structures;
using System;
using System.Web.UI;

namespace N_Ter_Tasks.controls_customizable
{
    public partial class cuz_reports : UserControl
    {
        N_Ter.Customizable.UI.Reports objReport = null;

        protected void Page_Init(object sender, System.EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            objReport = new N_Ter.Customizable.UI.Reports(Report_ID, objSes, Page, IsPostBack);
            objReport.RefreshReport += objReport_RefreshReport;
            divFilter.Controls.Clear();
            if (objReport.ReportFilter != null)
            {
                divFilter.Controls.Add(objReport.ReportFilter);
            }            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void objReport_RefreshReport(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            objReport.GenerateReport();

            if (objReport.Report != null)
            {
                divNoData.Visible = false;
                docViewMain.Visible = true;

                docViewMain.Report = objReport.Report;
                docViewMain.DataBind();
            }
            else
            {
                divNoData.Visible = true;
                docViewMain.Visible = false;
            }
        }

        public int Report_ID { get; set; }
    }
}