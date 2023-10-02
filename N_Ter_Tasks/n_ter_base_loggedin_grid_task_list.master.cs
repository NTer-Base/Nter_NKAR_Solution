using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using N_Ter.Base;

namespace N_Ter_Tasks
{
    public partial class n_ter_base_loggedin_grid_task_list : System.Web.UI.MasterPage
    {
        public string CurrentPageURL;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                SessionObject objSes = (SessionObject)Session["dt"];

                Workflow_Categories objWorkflowCategories = ObjectCreator.GetWorkflow_Categories(objSes.Connection, objSes.DB_Type);
                DS_Workflow ds = objWorkflowCategories.ReadAllForUserStart(objSes.UserID);

                if (ds.tblworkflow_categories.Rows.Count > 0)
                {
                    divNewTask.Visible = true;
                }
                else
                {
                    divNewTask.Visible = false;
                }
            }
            cmdNewTask.Attributes.Add("onClick", "return MasterClearControls();");

            N_Ter.Common.Common_Actions objCom = new N_Ter.Common.Common_Actions();
            CurrentPageURL = objCom.GetReletiveURL(Request);
        }

        protected void cmdBulkSubmit_Click(object sender, EventArgs e)
        {
            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
            string URLTrail = "";

            string[] ID_Split;
            foreach (string ID in hndSelectedIds.Value.Split('|'))
            {
                ID_Split = ID.Split('_');
                if (ID_Split.Length > 1)
                {
                    if (ID_Split[0].Trim() != "")
                    {
                        URLTrail = URLTrail + ID_Split[0].Trim() + "|";
                    }
                }
            }

            N_Ter.Common.Common_Actions objCom = new N_Ter.Common.Common_Actions();
            string CurrentPageURL = objCom.GetReletiveURL(Request);

            Response.Redirect("task_bulk.aspx?tids=" + objURL.Encrypt(URLTrail) + "&bck=" + CurrentPageURL);
        }
    }
}