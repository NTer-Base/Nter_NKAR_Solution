using N_Ter.Base;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Linq;

namespace N_Ter_Tasks
{
    public partial class dashboard_performance : System.Web.UI.Page
    {
        public string MainChartScript = "";
        public string UserChartScript = "";
        public string UserChart2Script = "";
        public string ClientChartScript = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                SessionObject objSes = (SessionObject)Session["dt"];
                LoadWorkflows(objSes);
                txtFrom.Text = string.Format("{0:" + Constants.DateFormat + "}", DateTime.Today.Date.AddMonths(-1));
                txtTo.Text = string.Format("{0:" + Constants.DateFormat + "}", DateTime.Today.Date);
                ltrEL2.Text = objSes.EL2;
                ShowChart(objSes);
            }
        }

        private void LoadWorkflows(SessionObject objSes)
        {
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.ReadAll();
            foreach (DS_Workflow.tblwalkflowRow row in ds.tblwalkflow)
            {
                row.Workflow_Name = row.Workflow_Category_Name + " - " + row.Workflow_Name;
            }
            cboWorkflows.DataSource = ds.tblwalkflow.OrderBy(x => x.Workflow_Category_Name).ThenBy(y => y.Workflow_Name);
            cboWorkflows.DataTextField = "Workflow_Name";
            cboWorkflows.DataValueField = "Walkflow_ID";
            cboWorkflows.DataBind();
            cboWorkflows.Items.Insert(0, new System.Web.UI.WebControls.ListItem("[All Workflows]", "0"));
        }

        private void ShowChart(SessionObject objSes)
        {
            DateTime FromDate = DateTime.ParseExact(txtFrom.Text, Constants.DateFormat, null);
            DateTime ToDate = DateTime.ParseExact(txtTo.Text, Constants.DateFormat, null);

            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            Users objUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
            Entity_Level_2 objEL2 = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type);

            N_Ter.Common.Common_Task_Actions objCom = new N_Ter.Common.Common_Task_Actions(objTask);
            PerformanceDashboard Dsh = objCom.ReadPerformanceDashboardData(objSes, objUsers, objEL2, FromDate, ToDate, Convert.ToInt32(cboWorkflows.SelectedValue));

            MainChartScript = Dsh.MainChart;
            UserChartScript = Dsh.UserChart;
            UserChart2Script = Dsh.UserChart2;
            ClientChartScript = Dsh.ClientChart;

            lblTotalStart.InnerHtml = Dsh.TotalStarted;
            lblTotalCompleted.InnerHtml = Dsh.TotalCompletd;
            lblAvgPerDay.InnerHtml = Dsh.AveragePerDay;

            lblTotalStart2.InnerHtml = Dsh.TotalStarted;
            lblTotalCompleted2.InnerHtml = Dsh.TotalCompletd;
            lblTotalInactive.InnerHtml = Dsh.TotalInactive;
            lblTotalUnclaimed.InnerHtml = Dsh.TotalUnclaimed;
            lblTotalClaimed.InnerHtml = Dsh.TotalClaimedByYou;
        }
    }
}