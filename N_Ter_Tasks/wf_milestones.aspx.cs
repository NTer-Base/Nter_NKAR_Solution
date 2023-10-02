using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class wf_milestones : System.Web.UI.Page
    {
        protected void Page_Init(object sender, System.EventArgs e)
        {
            n_ter_base_loggedin_grid_wf pg = (n_ter_base_loggedin_grid_wf)this.Master;
            pg.PageClass = "main-menu-animated page-mail";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (IsPostBack == false)
            {
                if (Request.QueryString["fid"] == null)
                {
                    Response.Redirect("error.aspx?");
                }
                else
                {
                    SessionObject objSes = (SessionObject)Session["dt"];
                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    int WFID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["fid"])));
                    ViewState["fid"] = WFID;

                    Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                    DS_Workflow ds = objWF.Read(WFID, false, false);
                    ltrWorkflowName.Text = ds.tblwalkflow[0].Workflow_Name;
                    NewMilestone(objSes);
                }
            }
            cmdSaveMS.OnClientClick = "return ValidateWorkflowMilestone('" + txtMilestoneName.ClientID + "', '" + txtMilestoneWeight.ClientID + "');";
        }

        private void NewMilestone(SessionObject objSes)
        {
            hndWFMSID.Value = "0";
            ltrMSMode.Text = "New Milestone";
            pnlMS.Visible = true;
            txtMilestoneName.Text = "";
            txtMilestoneWeight.Text = "";
            cmdSaveMS.Text = "Create";
            cmdDeleteMS.Visible = false;
            cmdDeleteMS_ModalPopupExtender.Enabled = false;
            pnlMSDelete.Visible = false;
            Workflow_Milestones objMS = ObjectCreator.GetWorkflow_Milestones(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objMS.ReadAll(Convert.ToInt32(ViewState["fid"]));
            LoadStepNumbers(ds.tblworkflow_milestones.Count + 1);
            RefreshGrid(ds.tblworkflow_milestones);
            cboSortOrder.SelectedValue = Convert.ToString(cboSortOrder.Items.Count);
        }

        private void LoadStepNumbers(int NumberOfSteps)
        {
            cboSortOrder.Items.Clear();
            for (int i = 1; i <= NumberOfSteps; i++)
            {
                cboSortOrder.Items.Add(new ListItem("Milestone " + i.ToString(), i.ToString()));
            }
        }

        private void RefreshGrid(DS_Workflow.tblworkflow_milestonesDataTable tbl)
        {
            ltrMSs.Text = "";

            foreach (DS_Workflow.tblworkflow_milestonesRow row in tbl)
            {
                if (Convert.ToInt32(hndWFMSID.Value) == row.Workflow_Milestone_ID)
                {
                    ltrMSs.Text = ltrMSs.Text + "<li class=\"active-item group\">" + "\r\n" +
                                                    "<div class='pull-right'><i class='fa fa-bars'></i></div>" + "\r\n" +
                                                    "<a href=\"#\" onclick=\"document.getElementById('" + hndWFMSID.ClientID + "').value = '" + row.Workflow_Milestone_ID + "'; LoadMS(); return false;\">" + "\r\n" +
                                                        "<span data-id=\"" + row.Workflow_Milestone_ID + "\" class=\"text-bold ms_no\">Milestone " + row.Sort_Order + "</span><br />" + "\r\n" +
                                                        "<span class=\"text-sm\">" + row.Milestone_Name + "</span>" + "\r\n" +
                                                    "</a>" + "\r\n" +
                                                "</li>" + "\r\n";
                }
                else
                {
                    ltrMSs.Text = ltrMSs.Text + "<li class=\"group\">" + "\r\n" +
                                                    "<div class='pull-right'><i class='fa fa-bars'></i></div>" + "\r\n" +
                                                    "<a href=\"#\" onclick=\"document.getElementById('" + hndWFMSID.ClientID + "').value = '" + row.Workflow_Milestone_ID + "'; LoadMS(); return false;\">" + "\r\n" +
                                                        "<span data-id=\"" + row.Workflow_Milestone_ID + "\" class=\"text-bold ms_no\">Milestone " + row.Sort_Order + "</span><br />" + "\r\n" +
                                                        "<span class=\"text-sm\">" + row.Milestone_Name + "</span>" + "\r\n" +
                                                    "</a>" + "\r\n" +
                                                "</li>" + "\r\n";
                }
            }

            pnlMS.Attributes.Remove("style");
            pnlMS.Attributes.Add("style", "min-height: " + ((tbl.Rows.Count + 1) * 60) + "px");
        }

        protected void cmdNewMS_ServerClick(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            NewMilestone(objSes);
        }

        protected void cmdLoadMS_Click(object sender, EventArgs e)
        {
            pnlMS.Visible = true;
            SessionObject objSes = (SessionObject)Session["dt"];
            LoadMileStone(objSes);
        }

        private void LoadMileStone(SessionObject objSes)
        {
            cmdSaveMS.Text = "Save";
            Workflow_Milestones objMS = ObjectCreator.GetWorkflow_Milestones(objSes.Connection, objSes.DB_Type);
            DS_Workflow.tblworkflow_milestonesRow drMS = objMS.Read(Convert.ToInt32(hndWFMSID.Value));
            txtMilestoneName.Text = drMS.Milestone_Name;
            txtMilestoneWeight.Text = drMS.Milestone_Weight.ToString();
            ltrMSMode.Text = "Edit Milestone : " + drMS.Milestone_Name;
            cmdDeleteMS.Visible = true;
            cmdDeleteMS_ModalPopupExtender.Enabled = true;
            pnlMSDelete.Visible = true;
            DS_Workflow dsMSs = objMS.ReadAll(Convert.ToInt32(ViewState["fid"]));
            LoadStepNumbers(dsMSs.tblworkflow_milestones.Rows.Count);
            RefreshGrid(dsMSs.tblworkflow_milestones);
            try
            {
                cboSortOrder.SelectedValue = Convert.ToString(drMS.Sort_Order);
            }
            catch (Exception)
            {
                cboSortOrder.SelectedIndex = 0;
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            pnlMS.Visible = false;
            hndWFMSID.Value = "0";
            Workflow_Milestones objMS = ObjectCreator.GetWorkflow_Milestones(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objMS.ReadAll(Convert.ToInt32(ViewState["fid"]));
            RefreshGrid(ds.tblworkflow_milestones);
        }

        protected void cmdMSDeleteOK_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow_Milestones objMS = ObjectCreator.GetWorkflow_Milestones(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objMS.Delete(Convert.ToInt32(hndWFMSID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                DS_Workflow ds = objMS.ReadAll(Convert.ToInt32(ViewState["fid"]));
                RefreshGrid(ds.tblworkflow_milestones);
                NewMilestone(objSes);

                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Milestone Successfully Deleted');", true);
            }
        }

        protected void cmdSaveMS_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow_Milestones objMS = ObjectCreator.GetWorkflow_Milestones(objSes.Connection, objSes.DB_Type);
            if (hndWFMSID.Value == "0")
            {
                byte[] bytes = System.Text.Encoding.Default.GetBytes(txtMilestoneName.Text);
                txtMilestoneName.Text = System.Text.Encoding.UTF8.GetString(bytes);
                if (objMS.Insert(txtMilestoneName.Text, Convert.ToInt32(txtMilestoneWeight.Text), Convert.ToInt32(cboSortOrder.SelectedItem.Value), Convert.ToInt32(ViewState["fid"])) == true)
                {
                    DS_Workflow ds = objMS.ReadAll(Convert.ToInt32(ViewState["fid"]));
                    RefreshGrid(ds.tblworkflow_milestones);
                    NewMilestone(objSes);

                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Milestone succesfully Created');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Milestone cannot be Created');", true);
                }
            }
            else
            {
                if (objMS.Update(Convert.ToInt32(hndWFMSID.Value), txtMilestoneName.Text, Convert.ToInt32(txtMilestoneWeight.Text), Convert.ToInt32(cboSortOrder.SelectedItem.Value)) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Milestone cannot be Updated');", true);
                }
                else
                {
                    DS_Workflow ds = objMS.ReadAll(Convert.ToInt32(ViewState["fid"]));
                    RefreshGrid(ds.tblworkflow_milestones);
                    NewMilestone(objSes);

                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Milestone succesfully Updated');", true);
                }
            }
        }
    }
}