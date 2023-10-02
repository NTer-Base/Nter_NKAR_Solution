using N_Ter.Base;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class manage_tasks : System.Web.UI.Page
    {
        public string SortField;
        public string SortDirection;
        public int RefreshFreq;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            SortField = (objSes.ListSort == 1 ? "2" : "9");
            SortDirection = (objSes.ListSortDir ? "asc" : "desc");
            RefreshFreq = objSes.RefFreq;

            if (IsPostBack == false)
            {
                LoadWorkflows(objSes);
                txtFrom.Text = string.Format("{0:" + Constants.DateFormat + "}", DateTime.Today.Date.AddMonths(-1));
                txtTo.Text = string.Format("{0:" + Constants.DateFormat + "}", DateTime.Today.Date);

                ltrEL1.Text = objSes.EL1;
                ltrEL2.Text = objSes.EL2 + " Name";
                divStatusChange.Visible = false;
                divCompletedStatusChange.Visible = false;
                divResults.Visible = false;
            }
            cboWorkflow.Attributes.Add("onChange", "WorkflowChanged();");
            cmdCompletedChangeState.Attributes.Add("onClick", "return ValidateChange(1);");
            cmdChangeState.Attributes.Add("onClick", "return ValidateChange(2);");
            cmdDiscardedChangeState.Attributes.Add("onClick", "return ValidateChange(3);");
        }

        private void LoadWorkflows(SessionObject objSes)
        {
            DS_Workflow ds = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).ReadAll();
            foreach (DS_Workflow.tblwalkflowRow row in ds.tblwalkflow)
            {
                row.Exrta_Field_Naming = row.Walkflow_ID.ToString() + "|" + row.Schedule_ID.ToString();
            }
            cboWorkflow.DataSource = ds.tblwalkflow.OrderBy(x => x.Workflow_Name);
            cboWorkflow.DataTextField = "Workflow_Name";
            cboWorkflow.DataValueField = "Exrta_Field_Naming";
            cboWorkflow.DataBind();
        }

        protected void cmdShowCompleted_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            PrepareSearch(1);
            SearchCompletedTasks(objSes);
        }

        protected void cmdShow_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            PrepareSearch(2);
            SearchTasks(objSes);
        }

        protected void cmdShowDiscard_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            PrepareSearch(3);
            SearchDiscardedTasks(objSes);
        }

        private void PrepareSearch(int SearchType)
        {
            string SearchText = "";
            if (Convert.ToInt32(cboWorkflow.SelectedItem.Value.Split('|')[1]) > 0)
            {
                SearchText = "0|" + cboWorkflow.SelectedItem.Value.Split('|')[0];
                if (Convert.ToInt32(hndScheduleLineID.Value) == 0)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Invalid Schedule');", true);
                }
                else
                {
                    SearchText = SearchText + "|" + hndScheduleLineID.Value;
                    ViewState["sh"] = SearchText + "|" + SearchType.ToString();
                }
            }
            else
            {
                SearchText = "1|" + cboWorkflow.SelectedItem.Value.Split('|')[0];

                DateTime FromDate = DateTime.Today.Date;
                DateTime ToDate = DateTime.Today.Date;
                Common_Actions objCom = new Common_Actions();
                
                if (objCom.ValidateDate(txtFrom.Text, ref FromDate) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Invalid From Date');", true);
                }
                else if (objCom.ValidateDate(txtTo.Text, ref ToDate) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Invalid To Date');", true);
                }
                else
                {
                    SearchText = SearchText + "|" + txtFrom.Text + "|" + txtTo.Text;
                    ViewState["sh"] = SearchText + "|" + SearchType.ToString();
                }
            }
        }

        private void SearchCompletedTasks(SessionObject objSes)
        {
            divStatusChange.Visible = false;
            divCompletedStatusChange.Visible = false;
            divDiscardedStatusChange.Visible = false;
            divResults.Visible = false;

            hndSelected_Tasks.Value = "";

            string[] SearchParts = Convert.ToString(ViewState["sh"]).Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow dsWF = objWF.Read(Convert.ToInt32(SearchParts[1]));
            ltrEx1.Text = dsWF.tblwalkflow[0].Exrta_Field_Naming;
            ltrEx2.Text = dsWF.tblwalkflow[0].Exrta_Field2_Naming;

            divCompletedStatusChange.Visible = true;
            divResults.Visible = true;
            ltrTasksType.Text = "Completed Tasks";

            cboCompletedStatus.DataSource = dsWF.tblworkflow_steps;
            cboCompletedStatus.DataTextField = "Step_Status";
            cboCompletedStatus.DataValueField = "Workflow_Step_ID";
            cboCompletedStatus.DataBind();

            cboCompletedStatus.Items.Add(new ListItem("[Deleted (Cannot Undo)]", "0"));
        }

        private void SearchTasks(SessionObject objSes)
        {
            divStatusChange.Visible = false;
            divCompletedStatusChange.Visible = false;
            divDiscardedStatusChange.Visible = false;
            divResults.Visible = false;

            hndSelected_Tasks.Value = "";

            string[] SearchParts = Convert.ToString(ViewState["sh"]).Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow dsWF = objWF.Read(Convert.ToInt32(SearchParts[1]));
            ltrEx1.Text = dsWF.tblwalkflow[0].Exrta_Field_Naming;
            ltrEx2.Text = dsWF.tblwalkflow[0].Exrta_Field2_Naming;

            divStatusChange.Visible = true;
            divResults.Visible = true;
            ltrTasksType.Text = "Non Completed Tasks";

            cboStatus.DataSource = dsWF.tblworkflow_steps;
            cboStatus.DataTextField = "Step_Status";
            cboStatus.DataValueField = "Workflow_Step_ID";
            cboStatus.DataBind();

            cboStatus.Items.Add(new ListItem("[Inactive]", "-1"));
            cboStatus.Items.Add(new ListItem("[Completed]", "-2"));
            cboStatus.Items.Add(new ListItem("[Discarded]", "-4"));
            cboStatus.Items.Add(new ListItem("[Deleted (Cannot Undo)]", "-3"));
        }

        private void SearchDiscardedTasks(SessionObject objSes)
        {
            divStatusChange.Visible = false;
            divCompletedStatusChange.Visible = false;
            divDiscardedStatusChange.Visible = false;
            divResults.Visible = false;

            hndSelected_Tasks.Value = "";

            string[] SearchParts = Convert.ToString(ViewState["sh"]).Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow dsWF = objWF.Read(Convert.ToInt32(SearchParts[1]));
            ltrEx1.Text = dsWF.tblwalkflow[0].Exrta_Field_Naming;
            ltrEx2.Text = dsWF.tblwalkflow[0].Exrta_Field2_Naming;

            divDiscardedStatusChange.Visible = true;
            divResults.Visible = true;
            ltrTasksType.Text = "Discarded Tasks";

            cboDiscardedStatus.DataSource = dsWF.tblworkflow_steps;
            cboDiscardedStatus.DataTextField = "Step_Status";
            cboDiscardedStatus.DataValueField = "Workflow_Step_ID";
            cboDiscardedStatus.DataBind();

            cboDiscardedStatus.Items.Add(new ListItem("[Deleted (Cannot Undo)]", "0"));
        }

        protected void cmdChangeState_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Tasks objTasks = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            int[] Task_IDs = hndSelected_Tasks.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(n => Convert.ToInt32(n)).ToArray();

            if (Task_IDs.Length > 0)
            {
                if (cboStatus.SelectedItem.Value == "-1")
                {
                    if (objTasks.Deactivate(Task_IDs) == true)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Task Status Successfully Updated');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('All Task Status could not be Updated');", true);
                    }
                }
                else if (cboStatus.SelectedItem.Value == "-2")
                {
                    bool TasksUpdated = true;
                    foreach (int Task_ID in Task_IDs)
                    {
                        if (objTasks.ChangeTaskStep(Task_ID, -1) == false)
                        {
                            TasksUpdated = false;
                        }
                    }
                    if (TasksUpdated == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('All Task Status could not be Updated');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Task Status Successfully Updated');", true);
                    }
                }
                else if (cboStatus.SelectedItem.Value == "-3")
                {
                    Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
                    if (objTasks.DeleteTasks(Task_IDs, objCus) == true)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Tasks Successfully Deleted');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('All Tasks could not be Deleted');", true);
                    }
                }
                else if (cboStatus.SelectedItem.Value == "-4")
                {
                    bool TasksUpdated = true;
                    foreach (int Task_ID in Task_IDs)
                    {
                        if (objTasks.ChangeTaskStep(Task_ID, -2) == false)
                        {
                            TasksUpdated = false;
                        }
                    }
                    if (TasksUpdated == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('All Task Status could not be Updated');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Task Status Successfully Updated');", true);
                    }
                }
                else
                {
                    bool TasksUpdated = true;
                    foreach (int Task_ID in Task_IDs)
                    {
                        if (objTasks.ChangeTaskStep(Task_ID, Convert.ToInt32(cboStatus.SelectedValue)) == false)
                        {
                            TasksUpdated = false;
                        }
                    }
                    if (TasksUpdated == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('All Task Status could not be Updated');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Task Status Successfully Updated');", true);
                    }
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('No Tasks Selected');", true);
            }
            SearchTasks(objSes);
        }

        protected void cmdConfirmCompStatusChange_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Tasks objTasks = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            int[] Task_IDs = hndSelected_Tasks.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(n => Convert.ToInt32(n)).ToArray();

            if (Task_IDs.Length > 0)
            {
                if (cboCompletedStatus.SelectedValue == "0")
                {
                    Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
                    if (objTasks.DeleteTasks(Task_IDs, objCus) == true)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Tasks Successfully Deleted');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('All Tasks could not be Deleted');", true);
                    }
                }
                else
                {
                    bool TasksUpdated = true;
                    foreach (int Task_ID in Task_IDs)
                    {
                        if (objTasks.ChangeTaskStep(Task_ID, Convert.ToInt32(cboCompletedStatus.SelectedValue)) == false)
                        {
                            TasksUpdated = false;
                        }
                    }
                    if (TasksUpdated == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('All Task Status could not be Updated');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Task Status Successfully Updated');", true);
                    }
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('No Tasks Selected');", true);
            }
            SearchCompletedTasks(objSes);
        }

        protected void cmdConfirmDiscardedChangeState_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Tasks objTasks = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            int[] Task_IDs = hndSelected_Tasks.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(n => Convert.ToInt32(n)).ToArray();

            if (Task_IDs.Length > 0)
            {
                if (cboDiscardedStatus.SelectedValue == "0")
                {
                    Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
                    if (objTasks.DeleteTasks(Task_IDs, objCus) == true)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Tasks Successfully Deleted');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('All Tasks could not be Deleted');", true);
                    }
                }
                else
                {
                    bool TasksUpdated = true;
                    foreach (int Task_ID in Task_IDs)
                    {
                        if (objTasks.ChangeTaskStep(Task_ID, Convert.ToInt32(cboDiscardedStatus.SelectedValue)) == false)
                        {
                            TasksUpdated = false;
                        }
                    }
                    if (TasksUpdated == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('All Task Status could not be Updated');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Task Status Successfully Updated');", true);
                    }
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('No Tasks Selected');", true);
            }
            SearchDiscardedTasks(objSes);
        }
    }
}