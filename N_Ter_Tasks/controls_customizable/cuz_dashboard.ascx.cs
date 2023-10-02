using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;

namespace N_Ter_Tasks.controls_customizable
{
    public partial class cuz_dashboard : System.Web.UI.UserControl
    {
        public string RefreshFreq;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            RefreshFreq = objSes.RefFreq.ToString();
            if (IsPostBack == false)
            {
                LoadWorkflowCategories(objSes);
                hndWorkflowID.Value = "0";
            }
            cboWF_Category.Attributes.Add("onChange", "LoadWorkflows();");
        }

        private void LoadWorkflowCategories(SessionObject objSes)
        {
            Workflow_Categories objWorkflowCategories = ObjectCreator.GetWorkflow_Categories(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWorkflowCategories.ReadAllForUser(objSes.UserID);
            cboWF_Category.DataSource = ds.tblworkflow_categories;
            cboWF_Category.DataValueField = "Workflow_Category_ID";
            cboWF_Category.DataTextField = "Workflow_Category_Name";
            cboWF_Category.DataBind();
        }

        protected void cmdShowReportMain_Click(object sender, EventArgs e)
        {

        }

        protected void cmdShowReport_Click(object sender, EventArgs e)
        {

        }
    }
}