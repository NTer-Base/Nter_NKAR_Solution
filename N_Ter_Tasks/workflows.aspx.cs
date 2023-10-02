using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class workflows : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                SessionObject objSes = (SessionObject)Session["dt"];
                ltrEL2s.Text = objSes.EL2P;
                gvEntity_L2.Columns[2].HeaderText = objSes.EL2 + " Name";
                ltrEL2.Text = objSes.EL2;

                hndWorkflowID.Value = "0";
                LoadSchedules(objSes);
                RefreshGrid(objSes);
                LoadMaterTableNames(objSes);
                LoadDocumentProjects(objSes);
                LoadWorkflowCategories(objSes);
            }

            cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndWorkflowID.ClientID + "').value = '0'; ClearControls();");
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidateWorkflow('" + txtName.ClientID + "', '" + cboDeadlineType.ClientID + "', '" + txtNumberOfDays.ClientID + "', '" + cboSchedule.ClientID + "', '" + cboWorkflowCategory.ClientID + "');";
            cboDeadlineType.Attributes.Add("onChange", "CheckDeadlineType();");
            cboSchedule.Attributes.Add("onChange", "CheckAutoStart();");
            cboExtraType.Attributes.Add("onChange", "SelectExtraFieldType();");
            cboExtra2Type.Attributes.Add("onChange", "SelectExtraFieldType();");
            cboWorkflowType.Attributes.Add("onChange", "CheckAutoStart();");
            cmdCreateDuplicate.OnClientClick = "return ValidateDuplicateWF('" + txtNewWorkflowName.ClientID + "');";
            cboWorkflowType.Attributes.Add("onChange", "CheckWFType();");
        }

        private void LoadEL2s(SessionObject objSes)
        {
            Entity_Level_2 objEL2 = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type);
            DS_Entity_Level_2 ds = objEL2.ReadAll();
            foreach (DS_Entity_Level_2.tblentity_level_2Row row in ds.tblentity_level_2)
            {
                if (row.Entity_L1_Name != "-")
                {
                    row.Display_Name = row.Entity_L1_Name + " - " + row.Display_Name;
                }
            }

            gvEntity_L2.SelectedIndex = -1;
            gvEntity_L2.DataSource = ds.tblentity_level_2;
            gvEntity_L2.DataBind();
            if (ds.tblentity_level_2.Rows.Count > 0)
            {
                gvEntity_L2.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void LoadMaterTableNames(SessionObject objSes)
        {
            N_Ter.Customizable.Custom_Lists objCus = new N_Ter.Customizable.Custom_Lists();
            DS_Master_Tables ds = objCus.LoadMasterTableNames(objSes.EL2P);
            cboExtraMasterTable.DataSource = ds.tblTables;
            cboExtraMasterTable.DataTextField = "Table_Name";
            cboExtraMasterTable.DataValueField = "Table_ID";
            cboExtraMasterTable.DataBind();
            cboExtraMasterTable.Items.Insert(0, new ListItem("[Not Selected]", "0"));

            cboExtra2MasterTable.DataSource = ds.tblTables;
            cboExtra2MasterTable.DataTextField = "Table_Name";
            cboExtra2MasterTable.DataValueField = "Table_ID";
            cboExtra2MasterTable.DataBind();
            cboExtra2MasterTable.Items.Insert(0, new ListItem("[Not Selected]", "0"));
        }

        private void LoadWorkflowCategories(SessionObject objSes)
        {
            Workflow_Categories objWF = ObjectCreator.GetWorkflow_Categories(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.ReadAll();
            cboWorkflowCategory.DataValueField = ds.tblworkflow_categories.Workflow_Category_IDColumn.ColumnName;
            cboWorkflowCategory.DataTextField = ds.tblworkflow_categories.Workflow_Category_NameColumn.ColumnName;
            cboWorkflowCategory.DataSource = ds.tblworkflow_categories;
            cboWorkflowCategory.DataBind();
            cboWorkflowCategory.Items.Insert(0, new ListItem("[Not Selected]", "0"));
        }

        private void LoadDocumentProjects(SessionObject objSes)
        {
            Document_Projects objWF = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
            DS_Doc_Project ds = objWF.ReadAll();
            cboDocumentProject.DataValueField = ds.tbldocument_project.Document_Project_IDColumn.ColumnName;
            cboDocumentProject.DataTextField = ds.tbldocument_project.Doc_Project_NameColumn.ColumnName;
            cboDocumentProject.DataSource = ds.tbldocument_project;
            cboDocumentProject.DataBind();
            cboDocumentProject.Items.Insert(0, new ListItem("[N/A]", "0"));
        }

        private void LoadSchedules(SessionObject objSes)
        {
            Workflow_Schedules objWF = ObjectCreator.GetWorkflow_Schedules(objSes.Connection, objSes.DB_Type);
            DS_Workflow_Scedules ds = objWF.ReadAll();
            cboSchedule.DataValueField = ds.tblworkflow_schedules.Schedule_IDColumn.ColumnName;
            cboSchedule.DataTextField = ds.tblworkflow_schedules.Schedule_NameColumn.ColumnName;
            cboSchedule.DataSource = ds.tblworkflow_schedules;
            cboSchedule.DataBind();
            cboSchedule.Items.Insert(0, new ListItem("[N/A]", "0"));
        }

        private void RefreshGrid(SessionObject objSes)
        {
            LoadEL2s(objSes);

            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.ReadAll();
            gvMain.SelectedIndex = -1;
            gvMain.DataSource = ds.tblwalkflow;
            gvMain.DataBind();
            if (ds.tblwalkflow.Rows.Count > 0)
            {
                gvMain.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                N_Ter.Common.Common_Actions objComAct = new N_Ter.Common.Common_Actions();

                SessionObject objSes = (SessionObject)Session["dt"];
                Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);

                if (e.Row.Cells[1].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdEdit = (System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[1].Controls[1];
                    cmdEdit.Attributes.Add("onclick", "$('#" + hndWorkflowID.ClientID + "').val('" + e.Row.Cells[0].Text + "'); LoadValues(); return false;");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    DS_Workflow dsAct = objWF.ReadActionCounts(Convert.ToInt32(e.Row.Cells[0].Text));
                    Literal ltrActions = (Literal)e.Row.Cells[2].Controls[1];
                    ltrActions.Text = objComAct.GetWorkflowActionMenu(objURL.Encrypt(e.Row.Cells[0].Text), Convert.ToInt32(e.Row.Cells[0].Text), e.Row.Cells[4].Text.Replace("&#39;", "\\'"), " btn-xs", " btn-success", "", dsAct.tblaction_counts[0]);
                }
                if (e.Row.Cells[3].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = (System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[3].Controls[1];
                    cmdDelete.Attributes.Add("onclick", "$('#" + hndWorkflowID.ClientID + "').val('" + e.Row.Cells[0].Text + "');");
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objWF.Delete(Convert.ToInt32(hndWorkflowID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Successfully Deleted');", true);
                RefreshGrid(objSes);
                hndWorkflowID.Value = "0";
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);

            if (txtExtraFieldName.Text.Trim() == "")
            {
                txtExtraSelection.Text = "";
                cboExtraType.SelectedIndex = 0;
                cboExtraMasterTable.SelectedIndex = 0;
            }
            if (txtExtraField2Name.Text.Trim() == "")
            {
                txtExtra2Selection.Text = "";
                cboExtra2Type.SelectedIndex = 0;
                cboExtra2MasterTable.SelectedIndex = 0;
            }

            bool DeadlineOK = true;
            int DeadlineType = Convert.ToInt32(cboDeadlineType.SelectedValue);

            int NumberOfDays = 0;
            int ScheduleID = 0;
            switch (DeadlineType)
            {
                case 1:
                    int.TryParse(txtNumberOfDays.Text, out NumberOfDays);
                    break;
                case 2:
                    ScheduleID = Convert.ToInt32(cboSchedule.SelectedValue);
                    if (ScheduleID == 0)
                    {
                        DeadlineOK = false;
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Please Select a Schedule');", true);
                    }
                    break;
                case 3:
                    if (int.TryParse(txtNumberOfDays.Text, out NumberOfDays) == false || NumberOfDays == 0)
                    {
                        DeadlineOK = false;
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Invalid Number of Days to Complete a Task');", true);
                    }
                    break;
            }
            if (DeadlineOK == true)
            {
                int RetentionAmount;
                if (!int.TryParse(txtRetentionAmount.Text, out RetentionAmount))
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Invalid Retention Amount');", true);
                }
                else
                {
                    if (hndWorkflowID.Value == "0")
                    {
                        int Max_ID = objWF.Insert(txtName.Text, objSes.UserID, Convert.ToInt32(countMultipleTasks.Text), txtDocTypes.Text, txtCommType.Text,
                            txtExtraFieldName.Text, Convert.ToInt32(cboExtraType.SelectedItem.Value), txtExtraSelection.Text, Convert.ToInt32(cboExtraMasterTable.SelectedItem.Value),
                            chkExtra1TaskStart.Checked, txtExtraField2Name.Text, Convert.ToInt32(cboExtra2Type.SelectedItem.Value), txtExtra2Selection.Text,
                            Convert.ToInt32(cboExtra2MasterTable.SelectedItem.Value), chkExtra2TaskStart.Checked, ScheduleID, DeadlineType, NumberOfDays,
                            Convert.ToInt32(cboDocumentProject.SelectedValue), Convert.ToInt32(cboWorkflowCategory.SelectedValue), chkAllowAssignSubordinates.Checked,
                            Convert.ToInt32(cboWorkflowType.SelectedValue), chkShowParent.Checked, chkShowSubDocs.Checked, chkShowSubComments.Checked, txtHelpTaskPost.Text,
                            txtHelpTaskStart.Text, txtLabelEL2.Text, txtLabelDueDate.Text, txtLabelDueTime.Text, txtLabelTaskQueue.Text, chkShowQueue.Checked,
                            Convert.ToInt32(cboRetentionType.SelectedItem.Value), RetentionAmount, chkChangeTLOnChanges.Checked, GetAvailableUserEL2s());
                        if (Max_ID > 0)
                        {
                            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                            Response.Redirect("workflow.aspx?fid=" + objURL.Encrypt(Convert.ToString(Max_ID)));
                        }
                        else
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow could not be saved');", true);
                        }
                    }
                    else
                    {
                        if (objWF.Update(Convert.ToInt32(hndWorkflowID.Value), txtName.Text, Convert.ToInt32(countMultipleTasks.Text), txtDocTypes.Text,
                            txtCommType.Text, txtExtraFieldName.Text, Convert.ToInt32(cboExtraType.SelectedItem.Value), txtExtraSelection.Text,
                            Convert.ToInt32(cboExtraMasterTable.SelectedItem.Value), chkExtra1TaskStart.Checked, txtExtraField2Name.Text,
                            Convert.ToInt32(cboExtra2Type.SelectedItem.Value), txtExtra2Selection.Text, Convert.ToInt32(cboExtra2MasterTable.SelectedItem.Value),
                            chkExtra2TaskStart.Checked, ScheduleID, DeadlineType, NumberOfDays, Convert.ToInt32(cboDocumentProject.SelectedValue),
                            Convert.ToInt32(cboWorkflowCategory.SelectedValue), chkAllowAssignSubordinates.Checked, Convert.ToInt32(cboWorkflowType.SelectedValue),
                            chkShowParent.Checked, chkShowSubDocs.Checked, chkShowSubComments.Checked, txtHelpTaskPost.Text, txtHelpTaskStart.Text, txtLabelEL2.Text,
                            txtLabelDueDate.Text, txtLabelDueTime.Text, txtLabelTaskQueue.Text, chkShowQueue.Checked, Convert.ToInt32(cboRetentionType.SelectedItem.Value), 
                            RetentionAmount, chkChangeTLOnChanges.Checked, GetAvailableUserEL2s()))
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Details Successfully Updated');", true);
                            hndWorkflowID.Value = "0";
                        }
                        else
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow could not be saved');", true);
                        }
                    }
                }
            }
            RefreshGrid(objSes);
        }

        private DS_Workflow.tblwalkflow_entity_l2DataTable GetAvailableUserEL2s()
        {
            DS_Workflow ds = new DS_Workflow();
            DS_Workflow.tblwalkflow_entity_l2Row dr;

            int No_Of_Units;

            foreach (GridViewRow row in gvEntity_L2.Rows)
            {
                if (row.Cells[1].HasControls())
                {
                    CheckBox chkSelect = (CheckBox)row.Cells[1].Controls[1];
                    foreach (string el2 in hndEl2.Value.Split('|'))
                    {
                        if (chkSelect.Attributes["data-id"].Trim() == el2.Trim())
                        {
                            No_Of_Units = 0;
                            TextBox txtNOU = (TextBox)row.Cells[3].Controls[1];
                            int.TryParse(txtNOU.Text, out No_Of_Units);

                            dr = ds.tblwalkflow_entity_l2.Newtblwalkflow_entity_l2Row();
                            dr.Walkflow_ID = 0;
                            dr.Entity_L2_ID = Convert.ToInt32(el2);
                            dr.No_Of_Units = No_Of_Units;
                            ds.tblwalkflow_entity_l2.Rows.Add(dr);
                        }
                    }
                }
            }
            return ds.tblwalkflow_entity_l2;
        }

        protected void gvEntity_L2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].HasControls())
                {
                    System.Web.UI.WebControls.CheckBox chkSelect = ((System.Web.UI.WebControls.CheckBox)e.Row.Cells[1].Controls[1]);
                    chkSelect.Attributes.Add("data-id", e.Row.Cells[0].Text);
                    chkSelect.Attributes.Add("data-hnd", "hndEl2");
                }
            }
        }

        protected void cmdCreateDuplicate_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Workflow_ID = Convert.ToInt32(hndWorkflowID.Value);

            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            if (objWF.CreateDuplicateWorkflow(Workflow_ID, txtNewWorkflowName.Text))
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Duplicate Workflow Successfully Updated');", true);
                hndWorkflowID.Value = "0";
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Duplicate Workflow could not be saved');", true);
            }
            RefreshGrid(objSes);
        }
    }
}