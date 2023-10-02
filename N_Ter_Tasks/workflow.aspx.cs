using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class workflow : System.Web.UI.Page
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
                    LoadCustomScreens(objSes);
                    LoadUserGroups(objSes);
                    LoadMasterTableNames(objSes);

                    ltrCreateEL2.Text = "Create New " + objSes.EL2 + " before Submit";
                    ltrDeactivateEL2.Text = "Deactivete " + objSes.EL2 + " on Submit";
                    ltrEL2.Text = objSes.EL2;

                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    int WFID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["fid"])));
                    ViewState["fid"] = WFID;

                    LoadMilestones(objSes);
                    LoadAddons(objSes);

                    Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                    DS_Workflow ds = objWF.Read(WFID, false, false);

                    LoadUploadDocTypes(ds);
                    ltrWorkflowName.Text = ds.tblwalkflow[0].Workflow_Name;

                    if (ds.tblwalkflow[0].Exrta_Field_Naming.Trim() != "")
                    {
                        ltrEx1Name.Text = ds.tblwalkflow[0].Exrta_Field_Naming.Trim();
                        if (ds.tblwalkflow[0].Exrta_Field_Type == 3 && ds.tblwalkflow[0].Extra_Field_Master_Table_ID > 0)
                        {
                            cboMasterTableParamType.Items.Add(new ListItem("Special Field 1 (" + ds.tblwalkflow[0].Exrta_Field_Naming + ")", "1"));
                            cboMasterTableParam2Type.Items.Add(new ListItem("Special Field 1 (" + ds.tblwalkflow[0].Exrta_Field_Naming + ")", "1"));
                        }
                    }
                    else
                    {
                        ltrEx1Name.Text = "Not Named Yet";
                    }
                    if (ds.tblwalkflow[0].Exrta_Field2_Naming.Trim() != "")
                    {
                        ltrEx2Name.Text = ds.tblwalkflow[0].Exrta_Field2_Naming.Trim();
                        if (ds.tblwalkflow[0].Exrta_Field2_Type == 3 && ds.tblwalkflow[0].Extra_Field2_Master_Table_ID > 0)
                        {
                            cboMasterTableParamType.Items.Add(new ListItem("Special Field 2 (" + ds.tblwalkflow[0].Exrta_Field2_Naming + ")", "2"));
                            cboMasterTableParam2Type.Items.Add(new ListItem("Special Field 2 (" + ds.tblwalkflow[0].Exrta_Field2_Naming + ")", "2"));
                        }
                    }
                    else
                    {
                        ltrEx2Name.Text = "Not Named Yet";
                    }
                    cboMasterTableParamType.Items.Add(new ListItem(objSes.EL2, "3"));
                    cboMasterTableParam2Type.Items.Add(new ListItem(objSes.EL2, "3"));

                    NewStep(objSes);
                }
            }
            cmdSaveStep.OnClientClick = "return ValidateWorkflowStep('" + txtStepName.ClientID + "', '" + txtStepWeight.ClientID + "');";
            chkStepLinks.Attributes.Add("onclick", "CheckScreenLinks();");

            cmdNewFieldCat.Attributes.Add("onClick", "javascript:document.getElementById('" + hndStepFieldCat_ID.ClientID + "').value = '0'; ClearStepFeldCats();");
            cmdResetFieldCat.Attributes.Add("onClick", "return ResetStepFeldCats();");
            cmdSaveFieldCat.Attributes.Add("onClick", "return ValidateWorkFlowFieldCat('" + txtStepFieldCat.ClientID + "');");

            cmdNewStepField.Attributes.Add("onClick", "javascript:document.getElementById('" + hndField_ID.ClientID + "').value = '0'; ClearControls();");
            cmdResetField.Attributes.Add("onClick", "return ResetControls();");
            cmdSaveField.Attributes.Add("onClick", "return ValidateWorkFlowField('" + txtFieldName.ClientID + "', '" + cboFieldType.ClientID + "', '" + chkAutoSubmit.ClientID + "', '" + txtDefaultText.ClientID + "', '" + txtDefaultTextMemo.ClientID + "', '" + cboMasterTable.ClientID + "');");
            cboFieldType.Attributes.Add("onChange", "ShowHideSections(false);");
            cboCopyDataFrom.Attributes.Add("onChange", "CheckCopyDisable(false);");

            cmdNewFieldCond.Attributes.Add("onClick", "javascript:document.getElementById('" + hndFieldCond_ID.ClientID + "').value = '0'; ClearConControls(true);");
            cmdResetCondition.Attributes.Add("onClick", "return ResetConControls();");
            cmdSaveCondition.Attributes.Add("onClick", "return ValidateWorkFlowCondition('" + txtEvaluateWith.ClientID + "');");
            txtHoursBeforeDeDate.Attributes.Add("onkeyup", "this.value = this.value.replace('.', '')");

            cmdDateDiffFormula.Attributes.Add("onClick", "javascript:document.getElementById('" + hndFormula_ID.ClientID + "').value = '0'; document.getElementById('" + hndFormula_Type.ClientID + "').value = '1'; ClearFormulaControls();");
            cmdDateAddFormula.Attributes.Add("onClick", "javascript:document.getElementById('" + hndFormula_ID.ClientID + "').value = '0'; document.getElementById('" + hndFormula_Type.ClientID + "').value = '2'; ClearFormulaControls();");
            cmdNumberFormula.Attributes.Add("onClick", "javascript:document.getElementById('" + hndFormula_ID.ClientID + "').value = '0'; document.getElementById('" + hndFormula_Type.ClientID + "').value = '3'; ClearFormulaControls();");
            cmdResetFormula.Attributes.Add("onClick", "return ResetFormulaControls();");
            cmdSaveFormula.Attributes.Add("onClick", "return ValidateWorkFlowFormula('" + hndFormula_Type.ClientID + "', '" + cboT1Date1.ClientID + "', '" + cboT1Date2.ClientID + "', '" + cboT2Date1.ClientID + "', '" + cboT2Result.ClientID + "', '" + cboT3Number1.ClientID + "', '" + cboT3Number2.ClientID + "', '" + cboT3Result.ClientID + "');");
            cmdCreateDuplicate.Attributes.Add("onClick", "return ValidateDuplicateStep('" + txtNewStep.ClientID + "')");

            cboConditionOn.Attributes.Add("onChange", "SelectOperators('" + cboConditionOn.ClientID + "', '" + cboOperator.ClientID + "');");
        }

        private void LoadCustomScreens(SessionObject objSes)
        {
            DS_Extra_Sections ds = new N_Ter.Customizable.Custom_Lists().LoadExtraSections();
            chkCustomScreens.DataSource = ds.tblSections.Where(x => x.IsisTask_SpecificNull() == false && x.isTask_Specific == true);
            chkCustomScreens.DataTextField = "Section_Name_Menu";
            chkCustomScreens.DataValueField = "Section_ID";
            chkCustomScreens.DataBind();
            if (ds.tblSections.Where(x => x.IsisTask_SpecificNull() == false && x.isTask_Specific == true).Count() > 0)
            {
                divLinkedNotAvailable.Visible = false;
            }
            else
            {
                divLinkedAvailable.Visible = false;
            }
        }

        private void LoadStepCombos(SessionObject objSes, DS_Workflow dsWF)
        {
            LoadNextSteps(objSes);
            LoadApproverSteps(objSes);
            LoadApproverStepsForConditions(objSes);
            LoadValidateFields(objSes);
            LoadEvalOKFields(objSes, dsWF);
            LoadFormulaCombos(objSes);
        }

        private void LoadAddons(SessionObject objSes)
        {
            Workflow_Addons objWFA = ObjectCreator.GetWorkflow_Addons(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWFA.ReadAll(Convert.ToInt32(ViewState["fid"]));
            cboAddon.DataValueField = ds.tblworkflow_addons.Addon_IDColumn.ColumnName;
            cboAddon.DataTextField = ds.tblworkflow_addons.Addon_NameColumn.ColumnName;
            cboAddon.DataSource = ds.tblworkflow_addons;
            cboAddon.DataBind();
            cboAddon.Items.Insert(0, new ListItem("[Not Applicable]", "0"));
        }

        private void LoadMilestones(SessionObject objSes)
        {
            Workflow_Milestones objMS = ObjectCreator.GetWorkflow_Milestones(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objMS.ReadAll(Convert.ToInt32(ViewState["fid"]));
            cboMilestone.DataSource = ds.tblworkflow_milestones;
            cboMilestone.DataTextField = "Milestone_Name";
            cboMilestone.DataValueField = "Workflow_Milestone_ID";
            cboMilestone.DataBind();
            cboMilestone.Items.Insert(0, new ListItem("[Not Applicable]", "0"));
        }

        private void LoadUserGroups(SessionObject objSes)
        {
            User_Groups objUG = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type);
            DS_Users ds = objUG.ReadAll();
            cboUserGroup.DataSource = ds.tbluser_groups;
            cboUserGroup.DataTextField = "User_Group_Name";
            cboUserGroup.DataValueField = "User_Group_ID";
            cboUserGroup.DataBind();
        }

        private void LoadMasterTableNames(SessionObject objSes)
        {
            N_Ter.Customizable.Custom_Lists objCus = new N_Ter.Customizable.Custom_Lists();
            DS_Master_Tables ds = objCus.LoadMasterTableNames(objSes.EL2P);
            cboMasterTable.DataSource = ds.tblTables;
            cboMasterTable.DataTextField = "Table_Name";
            cboMasterTable.DataValueField = "Table_ID";
            cboMasterTable.DataBind();
            cboMasterTable.Items.Insert(0, new ListItem("[Not Selected]", "0"));
        }

        private void LoadUploadDocTypes(DS_Workflow dsWF)
        {
            cboDocCat.Items.Clear();
            cboDocCat.Items.Add(new ListItem("[General Documents]", "General Documents"));
            foreach (string strCat in dsWF.tblwalkflow[0].Workflow_Doc_Types.Split('|'))
            {
                if (strCat.Trim() != "")
                {
                    cboDocCat.Items.Add(new ListItem(strCat, strCat));
                }
            }
        }

        private void RefreshGrid(DS_Workflow.tblworkflow_stepsDataTable tbl)
        {
            ltrSteps.Text = "";

            foreach (DS_Workflow.tblworkflow_stepsRow row in tbl)
            {
                if (Convert.ToInt32(hndWFStepID.Value) == row.Workflow_Step_ID)
                {
                    ltrSteps.Text = ltrSteps.Text + "<li class=\"active-item group\">" + "\r\n" +
                                                        "<div class='pull-right'><i class='fa fa-bars'></i></div>" + "\r\n" +
                                                        "<a href=\"#\" onclick=\"document.getElementById('" + hndWFStepID.ClientID + "').value = '" + row.Workflow_Step_ID + "'; LoadStep(); return false;\">" + "\r\n" +
                                                            "<span data-id=\"" + row.Workflow_Step_ID + "\" class=\"text-bold stp_no\">Step " + row.Sort_order + "</span><br />" + "\r\n" +
                                                            "<span class=\"text-sm\">" + row.Step_Status + "</span>" + "\r\n" +
                                                        "</a>" + "\r\n" +
                                                    "</li>" + "\r\n";
                }
                else
                {
                    ltrSteps.Text = ltrSteps.Text + "<li class='group'>" + "\r\n" +
                                                        "<div class='pull-right'><i class='fa fa-bars'></i></div>" + "\r\n" +
                                                        "<a href=\"#\" onclick=\"document.getElementById('" + hndWFStepID.ClientID + "').value = '" + row.Workflow_Step_ID + "'; LoadStep(); return false;\">" + "\r\n" +
                                                            "<span data-id=\"" + row.Workflow_Step_ID + "\" class=\"text-bold stp_no\">Step " + row.Sort_order + "</span><br />" + "\r\n" +
                                                            "<span class=\"text-sm\">" + row.Step_Status + "</span>" + "\r\n" +
                                                        "</a>" + "\r\n" +
                                                    "</li>" + "\r\n";
                }
            }

            pnlStep.Attributes.Remove("style");
            pnlStep.Attributes.Add("style", "min-height: " + ((tbl.Rows.Count + 2) * 65) + "px");
        }

        protected void cmdNewStep_ServerClick(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            NewStep(objSes);
        }

        private void NewStep(SessionObject objSes)
        {
            hndWFStepID.Value = "0";
            ltrStepMode.Text = "New Step";
            pnlStep.Visible = true;
            pnlStepFieldCats.Visible = false;
            pnlFields.Visible = false;
            pnlFieldConditions.Visible = false;
            pnlStepFormula.Visible = false;
            AutoStep(true);
            txtStepName.Text = "";
            txtHelpText.Text = "";
            txtStepWeight.Text = "";
            cboUserGroup.SelectedIndex = 0;
            cboMilestone.SelectedValue = "0";
            cboAddon.SelectedValue = "0";
            cboMoveToStep.SelectedValue = "0";
            cboNextStep.SelectedValue = "0";
            cboStepDuration.SelectedValue = "0";
            txtHoursBeforeDeDate.Text = "0";
            chkCheckHierarchy.Checked = false;
            chkShowToCreator.Checked = false;
            chkAllowMultipost.Checked = false;
            chkApproverStep.Checked = false;
            chkProductivity.Checked = false;
            chkCreator_Cannot_Submit.Checked = false;
            chkRemain.Checked = false;
            chkStepLinks.Checked = false;
            divLinkedScreens.Attributes["class"] = "row hide";
            foreach (ListItem itm in chkCustomScreens.Items)
            {
                itm.Selected = false;
            }
            chkLastStep.Checked = false;
            chkCreateEL2.Checked = false;
            chkDeactivateEL2.Checked = false;
            chkShowReject.Checked = true;
            chkAutoSubmit.Checked = false;
            chkEditExtraField.Checked = false;
            chkDeleteAddons.Checked = false;
            chkDeleteComments.Checked = false;
            chkDeleteDocuments.Checked = false;
            chkAllowCancellations.Checked = false;
            chkEditDueDate.Checked = false;
            chkEditQueue.Checked = false;
            chkAlloEditTL.Checked = false;
            chkEditEL2.Checked = false;
            chkShowHistory.Checked = true;
            chkShowDocuments.Checked = true;
            chkShowTimeline.Checked = true;
            chkShowChats.Checked = true;
            txtTaskDetailsHeader.Text = "";
            txtTaskPostHeader.Text = "";
            chkCustomPost.Checked = false;
            cmdSaveStep.Text = "Create";
            cmdDeleteStep.Visible = false;
            cmdDeleteStep_ModalPopupExtender.Enabled = false;
            pnlStepDelete.Visible = false;
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.Read(Convert.ToInt32(ViewState["fid"]), false, false);
            LoadStepCombos(objSes, ds);
            LoadStepNumbers(ds.tblworkflow_steps.Count + 1);
            RefreshGrid(ds.tblworkflow_steps);
            cboStepNumber.SelectedValue = Convert.ToString(cboStepNumber.Items[cboStepNumber.Items.Count - 1].Value);
            cmdDuplicateStep.Visible = false;
            cmdDuplicate_ModalPopupExtender.Enabled = false;

            N_Ter.Common.Common_Task_Actions objCommAct = new N_Ter.Common.Common_Task_Actions();
            string FlowchartSteps = "";
            string Flowchart = "";
            objCommAct.LoadFlowchart(ds.tblwalkflow[0].Walkflow_ID, 0, objWF, objSes.EL2, ref FlowchartSteps, ref Flowchart);
            ltrFlowChartSteps.Text = FlowchartSteps;
            ltrFlowChart.Text = Flowchart;
        }

        private void AutoStep(bool AutoStepOK)
        {
            if (AutoStepOK == false)
            {
                chkAutoSubmit.Enabled = false;
                iAutoSubmit.Visible = true;
            }
            else
            {
                chkAutoSubmit.Enabled = true;
                iAutoSubmit.Visible = false;
            }
        }

        protected void cmdLoadStep_Click(object sender, EventArgs e)
        {
            pnlStep.Visible = true;
            SessionObject objSes = (SessionObject)Session["dt"];
            LoadStep(objSes);
        }

        private void LoadStep(SessionObject objSes)
        {
            pnlFields.Visible = true;
            pnlStepFieldCats.Visible = true;
            pnlFieldConditions.Visible = true;
            pnlStepFormula.Visible = true;
            cmdSaveStep.Text = "Save";
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.ReadStep(Convert.ToInt32(hndWFStepID.Value));
            txtStepName.Text = ds.tblworkflow_steps[0].Step_Status;
            txtHelpText.Text = ds.tblworkflow_steps[0].Help_Text;
            cboMilestone.SelectedValue = ds.tblworkflow_steps[0].Workflow_Milestone_ID.ToString();
            cboAddon.SelectedValue = ds.tblworkflow_steps[0].Addon_ID.ToString();
            txtStepWeight.Text = ds.tblworkflow_steps[0].Step_Weight.ToString();
            ltrStepMode.Text = "Edit Step : " + ds.tblworkflow_steps[0].Step_Status;
            cboUserGroup.SelectedValue = Convert.ToString(ds.tblworkflow_steps[0].User_Group_Involved);
            try
            {
                cboMoveToStep.SelectedValue = Convert.ToString(ds.tblworkflow_steps[0].Move_To_If_Step_Duration_Fail);
            }
            catch (Exception)
            {
                cboMoveToStep.SelectedValue = "0";
            }
            try
            {
                cboNextStep.SelectedValue = Convert.ToString(ds.tblworkflow_steps[0].Next_Step_No_Conditions);
            }
            catch (Exception)
            {
                cboNextStep.SelectedValue = "0";
            }
            chkCheckHierarchy.Checked = ds.tblworkflow_steps[0].Check_User_Hierarchy;
            chkShowToCreator.Checked = ds.tblworkflow_steps[0].Show_Only_To_Creator;
            chkAllowMultipost.Checked = ds.tblworkflow_steps[0].Allow_Multipost;
            chkApproverStep.Checked = ds.tblworkflow_steps[0].isApproverStep;
            chkLastStep.Checked = ds.tblworkflow_steps[0].Is_Last_Step;
            chkCreateEL2.Checked = ds.tblworkflow_steps[0].Create_New_Entity_L2;
            chkDeactivateEL2.Checked = ds.tblworkflow_steps[0].Deactivate_Entity_L2;
            chkCreator_Cannot_Submit.Checked = ds.tblworkflow_steps[0].Creator_Cannot_Submit;
            chkRemain.Checked = ds.tblworkflow_steps[0].Remain_In_Task;
            chkStepLinks.Checked = ds.tblworkflow_steps[0].Show_Custom_Screens;
            if (ds.tblworkflow_steps[0].Show_Custom_Screens == true)
            {
                divLinkedScreens.Attributes["class"] = "row";
            }
            else
            {
                divLinkedScreens.Attributes["class"] = "row hide";
            }
            foreach (ListItem itm in chkCustomScreens.Items)
            {
                itm.Selected = false;
                foreach (DS_Workflow.tblworkflow_step_linksRow row in ds.tblworkflow_step_links)
                {
                    if (Convert.ToInt32(itm.Value) == row.Screen_ID)
                    {
                        itm.Selected = true;
                    }
                }
            }
            chkShowReject.Checked = ds.tblworkflow_steps[0].Show_Reject_Button;
            chkAutoSubmit.Checked = ds.tblworkflow_steps[0].Is_Auto_Submit;
            chkEditExtraField.Checked = ds.tblworkflow_steps[0].Can_Edit_Extra_Field;
            chkDeleteAddons.Checked = ds.tblworkflow_steps[0].Allow_Delete_Own_Addons;
            chkDeleteComments.Checked = ds.tblworkflow_steps[0].Allow_Delete_Own_Comments;
            chkDeleteDocuments.Checked = ds.tblworkflow_steps[0].Allow_Delete_Own_Documents;
            chkAllowCancellations.Checked = ds.tblworkflow_steps[0].Allow_Task_Cancellation;
            chkEditDueDate.Checked = ds.tblworkflow_steps[0].Can_Edit_Due_Date;
            chkEditQueue.Checked = ds.tblworkflow_steps[0].Allow_Change_Task_Queue;
            chkAlloEditTL.Checked = ds.tblworkflow_steps[0].Allow_Change_Step_Deadlines;
            chkShowHistory.Checked = ds.tblworkflow_steps[0].Show_History;
            chkShowDocuments.Checked = ds.tblworkflow_steps[0].Show_Documents;
            chkShowTimeline.Checked = ds.tblworkflow_steps[0].Show_Timeline;
            chkShowChats.Checked = ds.tblworkflow_steps[0].Show_Chats;
            txtTaskDetailsHeader.Text = ds.tblworkflow_steps[0].Task_Details_Header;
            txtTaskPostHeader.Text = ds.tblworkflow_steps[0].Task_Post_Header;
            chkCustomPost.Checked = ds.tblworkflow_steps[0].Show_Custom_Post;
            chkEditEL2.Checked = ds.tblworkflow_steps[0].Can_Edit_EL2;
            chkProductivity.Checked = ds.tblworkflow_steps[0].Is_Productive_Time;
            cboStepDuration.SelectedValue = Convert.ToString(ds.tblworkflow_steps[0].Step_Duration);
            cmdDeleteStep.Visible = true;
            cmdDeleteStep_ModalPopupExtender.Enabled = true;
            pnlStepDelete.Visible = true;
            DS_Workflow dsWF = objWF.Read(Convert.ToInt32(ViewState["fid"]), false, false);
            LoadStepNumbers(dsWF.tblworkflow_steps.Count);
            txtHoursBeforeDeDate.Text = Convert.ToString(ds.tblworkflow_steps[0].Hours_Before_Conclusion);

            LoadStepCombos(objSes, dsWF);
            LoadCopyFromFields(Convert.ToInt32(hndWFStepID.Value), objSes);
            LoadStepFieldCats(ds.tblworkflow_step_field_cats);

            RefreshGrid(dsWF.tblworkflow_steps);
            try
            {
                cboStepNumber.SelectedValue = Convert.ToString(ds.tblworkflow_steps[0].Sort_order);
            }
            catch (Exception)
            {
                cboStepNumber.SelectedValue = Convert.ToString(cboStepNumber.Items[0].Value);
            }
            RefreshStepFieldCats(ds.tblworkflow_step_field_cats);
            RefreshFields(ds.tblworkflow_step_fields);
            RefreshConditions(ds.tblworkflow_conditions);
            RefreshFormulas(ds.tblworkflow_formulas);
            cmdDuplicateStep.Visible = true;
            cmdDuplicate_ModalPopupExtender.Enabled = true;
            txtCurrentStep.Text = ds.tblworkflow_steps[0].Step_Status;
            txtNewStep.Text = "";

            N_Ter.Common.Common_Task_Actions objCommAct = new N_Ter.Common.Common_Task_Actions();
            string FlowchartSteps = "";
            string Flowchart = "";
            objCommAct.LoadFlowchart(ds.tblwalkflow[0].Walkflow_ID, 0, objWF, objSes.EL2, ref FlowchartSteps, ref Flowchart);
            ltrFlowChartSteps.Text = FlowchartSteps;
            ltrFlowChart.Text = Flowchart;
        }

        private void LoadStepFieldCats(DS_Workflow.tblworkflow_step_field_catsDataTable tbl)
        {
            cboFieldCategory.DataSource = tbl;
            cboFieldCategory.DataTextField = "Workflow_Step_Field_Cat";
            cboFieldCategory.DataValueField = "Workflow_Step_Field_Cat_ID";
            cboFieldCategory.DataBind();
            cboFieldCategory.Items.Insert(0, new ListItem("[N/A]", "0"));
        }

        private void LoadApproverSteps(SessionObject objSes)
        {
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.Read(Convert.ToInt32(ViewState["fid"]), true, false);

            DS_Workflow.tblworkflow_stepsRow dr = ds.tblworkflow_steps.Newtblworkflow_stepsRow();
            dr.Workflow_Step_ID = 0;
            dr.Step_Status = "[Not Applicable]";
            ds.tblworkflow_steps.Rows.InsertAt(dr, 0);

            cboMoveToStep.DataSource = ds.tblworkflow_steps;
            cboMoveToStep.DataTextField = "Step_Status";
            cboMoveToStep.DataValueField = "Workflow_Step_ID";
            cboMoveToStep.DataBind();
        }

        private void LoadNextSteps(SessionObject objSes)
        {
            int SelectedStep = 0;
            if (cboNextStep.Items.Count > 0)
            {
                SelectedStep = Convert.ToInt32(cboNextStep.SelectedItem.Value);
            }
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.Read(Convert.ToInt32(ViewState["fid"]), false, false);

            DS_Workflow.tblworkflow_stepsRow dr = ds.tblworkflow_steps.Newtblworkflow_stepsRow();
            dr.Workflow_Step_ID = 0;
            dr.Step_Status = "[Next in Sequence]";
            ds.tblworkflow_steps.Rows.InsertAt(dr, 0);

            cboNextStep.DataSource = ds.tblworkflow_steps;
            cboNextStep.DataTextField = "Step_Status";
            cboNextStep.DataValueField = "Workflow_Step_ID";
            cboNextStep.DataBind();

            cboNextStep.SelectedValue = SelectedStep.ToString();
        }

        private void LoadFormulaCombos(SessionObject objSes)
        {
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow dsDate = objWF.ReadDateFields(Convert.ToInt32(hndWFStepID.Value));
            DS_Workflow dsTime = objWF.ReadTimeFields(Convert.ToInt32(hndWFStepID.Value));
            DS_Workflow dsNumber = objWF.ReadNumberFields(Convert.ToInt32(hndWFStepID.Value));

            if (dsDate.tblworkflow_step_fields.Rows.Count < 2 || dsNumber.tblworkflow_step_fields.Rows.Count < 1)
            {
                cmdDateAddFormula_ModalPopupExtender.Enabled = false;
                cmdDateAddFormula.Attributes["onClick"] = "ShowError('No Enough Date and Number Fields to add a Formula'); return false;";

                cmdDateDiffFormula_ModalPopupExtender.Enabled = false;
                cmdDateDiffFormula.Attributes["onClick"] = "ShowError('No Enough Date and Number Fields to add a Formula'); return false;";
            }
            else
            {
                cmdDateAddFormula_ModalPopupExtender.Enabled = true;
                cmdDateDiffFormula_ModalPopupExtender.Enabled = true;
            }

            if (dsNumber.tblworkflow_step_fields.Rows.Count < 3)
            {
                cmdNumberFormula_ModalPopupExtender.Enabled = false;
                cmdNumberFormula.Attributes["onClick"] = "ShowError('No Enough Number Fields to add a Formula'); return false;";
            }
            else
            {
                cmdNumberFormula_ModalPopupExtender.Enabled = true;
            }
            cboT1Date1.DataSource = dsDate.tblworkflow_step_fields;
            cboT1Date1.DataTextField = "Field_Name";
            cboT1Date1.DataValueField = "Workflow_Step_Field_ID";
            cboT1Date1.DataBind();

            cboT1Date2.DataSource = dsDate.tblworkflow_step_fields;
            cboT1Date2.DataTextField = "Field_Name";
            cboT1Date2.DataValueField = "Workflow_Step_Field_ID";
            cboT1Date2.DataBind();

            cboT2Date1.DataSource = dsDate.tblworkflow_step_fields;
            cboT2Date1.DataTextField = "Field_Name";
            cboT2Date1.DataValueField = "Workflow_Step_Field_ID";
            cboT2Date1.DataBind();

            cboT2Result.DataSource = dsDate.tblworkflow_step_fields;
            cboT2Result.DataTextField = "Field_Name";
            cboT2Result.DataValueField = "Workflow_Step_Field_ID";
            cboT2Result.DataBind();

            cboT1Time1.DataSource = dsTime.tblworkflow_step_fields;
            cboT1Time1.DataTextField = "Field_Name";
            cboT1Time1.DataValueField = "Workflow_Step_Field_ID";
            cboT1Time1.DataBind();
            cboT1Time1.Items.Insert(0, new ListItem("-", "0"));

            cboT1Time2.DataSource = dsTime.tblworkflow_step_fields;
            cboT1Time2.DataTextField = "Field_Name";
            cboT1Time2.DataValueField = "Workflow_Step_Field_ID";
            cboT1Time2.DataBind();
            cboT1Time2.Items.Insert(0, new ListItem("-", "0"));

            cboT1Result.DataSource = dsNumber.tblworkflow_step_fields;
            cboT1Result.DataTextField = "Field_Name";
            cboT1Result.DataValueField = "Workflow_Step_Field_ID";
            cboT1Result.DataBind();

            cboT2Days.DataSource = dsNumber.tblworkflow_step_fields;
            cboT2Days.DataTextField = "Field_Name";
            cboT2Days.DataValueField = "Workflow_Step_Field_ID";
            cboT2Days.DataBind();

            cboT3Number1.DataSource = dsNumber.tblworkflow_step_fields;
            cboT3Number1.DataTextField = "Field_Name";
            cboT3Number1.DataValueField = "Workflow_Step_Field_ID";
            cboT3Number1.DataBind();

            cboT3Number2.DataSource = dsNumber.tblworkflow_step_fields;
            cboT3Number2.DataTextField = "Field_Name";
            cboT3Number2.DataValueField = "Workflow_Step_Field_ID";
            cboT3Number2.DataBind();

            cboT3Result.DataSource = dsNumber.tblworkflow_step_fields;
            cboT3Result.DataTextField = "Field_Name";
            cboT3Result.DataValueField = "Workflow_Step_Field_ID";
            cboT3Result.DataBind();
        }

        private void LoadStepNumbers(int NumberOfSteps)
        {
            cboStepNumber.Items.Clear();
            for (int i = 1; i <= NumberOfSteps; i++)
            {
                cboStepNumber.Items.Add(new ListItem("Step " + i.ToString(), i.ToString()));
            }
        }

        private void LoadEvalOKFields(SessionObject objSes, DS_Workflow dsWF)
        {
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.ReadEvalOKFields(Convert.ToInt32(hndWFStepID.Value));
            cboConditionOn.DataSource = ds.tblworkflow_step_fields;
            cboConditionOn.DataTextField = "Field_Name";
            cboConditionOn.DataValueField = "Default_Text";
            cboConditionOn.DataBind();

            N_Ter.Common.Common_Task_Actions objComAct = new N_Ter.Common.Common_Task_Actions();
            cboConditionOn.Items.Add(new ListItem(objSes.EL2, "-3_5"));

            if (dsWF.tblwalkflow[0].Exrta_Field_Naming.Trim() != "")
            {
                if (objComAct.isSpecialFieldConditionProof((ExtraFieldTypes)dsWF.tblwalkflow[0].Exrta_Field_Type))
                {
                    cboConditionOn.Items.Add(new ListItem("Special Field 1 (" + dsWF.tblwalkflow[0].Exrta_Field_Naming + ")", "-1_" + dsWF.tblwalkflow[0].Exrta_Field_Type + "_" + dsWF.tblwalkflow[0].Walkflow_ID));
                }
            }
            if (dsWF.tblwalkflow[0].Exrta_Field2_Naming.Trim() != "")
            {
                if (objComAct.isSpecialFieldConditionProof((ExtraFieldTypes)dsWF.tblwalkflow[0].Exrta_Field2_Type))
                {
                    cboConditionOn.Items.Add(new ListItem("Special Field 2 (" + dsWF.tblwalkflow[0].Exrta_Field2_Naming + ")", "-2_" + dsWF.tblwalkflow[0].Exrta_Field2_Type + "_" + dsWF.tblwalkflow[0].Walkflow_ID));
                }
            }
        }

        private void LoadCopyFromFields(int Workflow_Step_ID, SessionObject objSes)
        {
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.ReadOtherStepFields(Convert.ToInt32(ViewState["fid"]), Convert.ToInt32(hndWFStepID.Value));
            cboCopyDataFrom.DataSource = ds.tblworkflow_step_fields;
            cboCopyDataFrom.DataTextField = "Field_Name";
            cboCopyDataFrom.DataValueField = "Workflow_Step_Field_ID";
            cboCopyDataFrom.DataBind();
            cboCopyDataFrom.Items.Insert(0, new ListItem("[N/A]", "0"));
        }

        private void LoadApproverStepsForConditions(SessionObject objSes)
        {
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.Read(Convert.ToInt32(ViewState["fid"]), false, false);
            DS_Workflow.tblworkflow_stepsRow dr = ds.tblworkflow_steps.Newtblworkflow_stepsRow();
            dr.Workflow_Step_ID = -1;
            dr.Step_Status = "Completed";
            ds.tblworkflow_steps.Rows.Add(dr);
            cboMoveToIfTrue.DataSource = ds.tblworkflow_steps;
            cboMoveToIfTrue.DataTextField = "Step_Status";
            cboMoveToIfTrue.DataValueField = "Workflow_Step_ID";
            cboMoveToIfTrue.DataBind();
        }

        private void LoadValidateFields(SessionObject objSes)
        {
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.ReadOtherStepFields(Convert.ToInt32(ViewState["fid"]), Convert.ToInt32(hndWFStepID.Value));
            DS_Workflow.tblworkflow_step_fieldsRow dr = ds.tblworkflow_step_fields.Newtblworkflow_step_fieldsRow();
            dr.Workflow_Step_Field_ID = 0;
            dr.Field_Name = "[N/A]";
            ds.tblworkflow_step_fields.Rows.InsertAt(dr, 0);
            cboValidateField.DataSource = ds.tblworkflow_step_fields;
            cboValidateField.DataValueField = "Workflow_Step_Field_ID";
            cboValidateField.DataTextField = "Field_Name";
            cboValidateField.DataBind();
        }

        private void RefreshStepFieldCats(DS_Workflow.tblworkflow_step_field_catsDataTable tbl)
        {
            gvStepFieldCats.DataSource = tbl;
            gvStepFieldCats.DataBind();
            if (tbl.Rows.Count > 0)
            {
                gvStepFieldCats.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void RefreshFields(DS_Workflow.tblworkflow_step_fieldsDataTable tbl)
        {
            bool EnableAutoSubmit = true;
            foreach (DS_Workflow.tblworkflow_step_fieldsRow row in tbl)
            {
                switch (row.Field_Type)
                {
                    case 0:
                        row.Field_TypeSP = "Label Only";
                        break;
                    case 1:
                        row.Field_TypeSP = "Yes/No (Dropdown)";
                        EnableAutoSubmit = false;
                        break;
                    case 2:
                        row.Field_TypeSP = "Text";
                        if (row.Default_Text.Trim() == "")
                        {
                            EnableAutoSubmit = false;
                        }
                        break;
                    case 3:
                        row.Field_TypeSP = "Memo";
                        EnableAutoSubmit = false;
                        break;
                    case 4:
                        row.Field_TypeSP = "Number";
                        EnableAutoSubmit = false;
                        break;
                    case 5:
                        row.Field_TypeSP = "Selection";
                        EnableAutoSubmit = false;
                        break;
                    case 6:
                        row.Field_TypeSP = "Date";
                        EnableAutoSubmit = false;
                        break;
                    case 7:
                        row.Field_TypeSP = "Time";
                        EnableAutoSubmit = false;
                        break;
                    case 8:
                        row.Field_TypeSP = "Currency";
                        EnableAutoSubmit = false;
                        break;
                    case 9:
                        row.Field_TypeSP = "Percentage";
                        EnableAutoSubmit = false;
                        break;
                    case 10:
                        row.Field_TypeSP = "Master Table";
                        EnableAutoSubmit = false;
                        break;
                    case 11:
                        row.Field_TypeSP = "File Upload";
                        EnableAutoSubmit = false;
                        break;
                    case 12:
                        row.Field_TypeSP = "Time Span";
                        EnableAutoSubmit = false;
                        break;
                    case 13:
                        row.Field_TypeSP = "Yes/No (Switch)";
                        EnableAutoSubmit = false;
                        break;
                }
                switch (row.Field_Size)
                {
                    case 1:
                        row.Field_SizeSP = "Full Width";
                        break;
                    case 2:
                        row.Field_SizeSP = "1/2 Width";
                        break;
                    case 3:
                        row.Field_SizeSP = "1/3 Width";
                        break;
                    case 4:
                        row.Field_SizeSP = "2/3 Width";
                        break;
                    case 5:
                        row.Field_SizeSP = "1/4 Width";
                        break;
                    case 6:
                        row.Field_SizeSP = "3/4 Width";
                        break;
                    case 7:
                        row.Field_SizeSP = "1/6 Width";
                        break;
                    case 8:
                        row.Field_SizeSP = "5/6 Width";
                        break;
                }
            }
            gvStepFields.DataSource = tbl;
            gvStepFields.DataBind();
            if (tbl.Rows.Count > 0)
            {
                gvStepFields.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            AutoStep(EnableAutoSubmit);
        }

        private void RefreshConditions(DS_Workflow.tblworkflow_conditionsDataTable tbl)
        {
            foreach (DS_Workflow.tblworkflow_conditionsRow row in tbl)
            {
                switch (row.Operator_ID)
                {
                    case 1:
                        row.Operator_Name = "<";
                        break;
                    case 2:
                        row.Operator_Name = ">";
                        break;
                    case 3:
                        row.Operator_Name = "=";
                        break;
                    case 4:
                        row.Operator_Name = "<=";
                        break;
                    case 5:
                        row.Operator_Name = ">=";
                        break;
                    case 6:
                        row.Operator_Name = "!=";
                        break;
                }
            }
            gvStepConditions.DataSource = tbl;
            gvStepConditions.DataBind();
            if (tbl.Rows.Count > 0)
            {
                gvStepConditions.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void RefreshFormulas(DS_Workflow.tblworkflow_formulasDataTable tbl)
        {
            foreach (DS_Workflow.tblworkflow_formulasRow row in tbl)
            {
                switch (row.Formula_Type)
                {
                    case 1:
                        row.Formula_TypeSP = "Date (Date Diff)";
                        break;
                    case 2:
                        row.Formula_TypeSP = "Date (Add Days)";
                        break;
                    case 3:
                        row.Formula_TypeSP = "Numbers";
                        break;
                }
            }
            gvFormulas.DataSource = tbl;
            gvFormulas.DataBind();
            if (tbl.Rows.Count > 0)
            {
                gvFormulas.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvStepFields_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdEdit = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[1].Controls[1]);
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndField_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndField_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
                e.Row.Attributes.Add("data-id", e.Row.Cells[0].Text);
            }
        }

        protected void gvStepConditions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdEdit = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[1].Controls[1]);
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndFieldCond_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadConValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndFieldCond_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
                e.Row.Attributes.Add("data-id", e.Row.Cells[0].Text);
            }
        }

        protected void gvFormulas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdEdit = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[1].Controls[1]);
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndFormula_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadFormulaValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndFormula_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
            }
        }
        
        protected void cmdSaveStep_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            int HoursBeforeDueDate = 0;
            if (txtHoursBeforeDeDate.Text.Trim() != "")
            {
                HoursBeforeDueDate = Convert.ToInt32(txtHoursBeforeDeDate.Text);
            }
            if (hndWFStepID.Value == "0")
            {
                int MaxID = 0;
                byte[] bytes = System.Text.Encoding.Default.GetBytes(txtStepName.Text);
                txtStepName.Text = System.Text.Encoding.UTF8.GetString(bytes);
                MaxID = objWF.InsertStep(Convert.ToInt32(ViewState["fid"]), txtStepName.Text, Convert.ToInt32(txtStepWeight.Text), Convert.ToInt32(cboUserGroup.SelectedItem.Value), chkCheckHierarchy.Checked, Convert.ToInt32(cboMilestone.SelectedItem.Value), chkApproverStep.Checked, chkCreator_Cannot_Submit.Checked, Convert.ToInt32(cboNextStep.SelectedItem.Value), Convert.ToInt32(cboStepDuration.SelectedItem.Value), Convert.ToInt32(cboMoveToStep.SelectedItem.Value), chkProductivity.Checked, chkAutoSubmit.Checked, Convert.ToInt32(cboStepNumber.SelectedItem.Value), chkEditExtraField.Checked, chkEditDueDate.Checked, chkEditEL2.Checked, HoursBeforeDueDate, chkLastStep.Checked, chkCreateEL2.Checked, chkDeactivateEL2.Checked, txtHelpText.Text, chkDeleteComments.Checked, chkDeleteAddons.Checked, chkDeleteDocuments.Checked, chkAllowMultipost.Checked, Convert.ToInt32(cboAddon.SelectedValue), chkShowHistory.Checked, chkShowDocuments.Checked, chkAllowCancellations.Checked, chkRemain.Checked, chkEditQueue.Checked, chkStepLinks.Checked, chkShowToCreator.Checked, chkShowReject.Checked, chkCustomPost.Checked, chkAlloEditTL.Checked, chkShowTimeline.Checked, chkShowChats.Checked, txtTaskDetailsHeader.Text, txtTaskPostHeader.Text, GetLinedScreens());

                if (MaxID > 0)
                {
                    hndWFStepID.Value = Convert.ToString(MaxID);
                    DS_Workflow ds = objWF.Read(Convert.ToInt32(ViewState["fid"]), false, false);
                    RefreshGrid(ds.tblworkflow_steps);
                    LoadStepCombos(objSes, ds);
                    LoadStep(objSes);

                    N_Ter.Common.Common_Task_Actions objCommAct = new N_Ter.Common.Common_Task_Actions();
                    string FlowchartSteps = "";
                    string Flowchart = "";
                    objCommAct.LoadFlowchart(ds.tblwalkflow[0].Walkflow_ID, 0, objWF, objSes.EL2, ref FlowchartSteps, ref Flowchart);
                    ltrFlowChartSteps.Text = FlowchartSteps;
                    ltrFlowChart.Text = Flowchart;

                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Step succesfully Created');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Step cannot be Created');", true);
                }
            }
            else
            {
                if (objWF.UpdateStep(Convert.ToInt32(hndWFStepID.Value), txtStepName.Text, Convert.ToInt32(txtStepWeight.Text), Convert.ToInt32(cboUserGroup.SelectedItem.Value), chkCheckHierarchy.Checked, Convert.ToInt32(cboMilestone.SelectedItem.Value), chkApproverStep.Checked, chkCreator_Cannot_Submit.Checked, Convert.ToInt32(cboNextStep.SelectedItem.Value), Convert.ToInt32(cboStepDuration.SelectedItem.Value), Convert.ToInt32(cboMoveToStep.SelectedItem.Value), chkProductivity.Checked, chkAutoSubmit.Checked, Convert.ToInt32(cboStepNumber.SelectedItem.Value), chkEditExtraField.Checked, chkEditDueDate.Checked, chkEditEL2.Checked, HoursBeforeDueDate, chkLastStep.Checked, chkCreateEL2.Checked, chkDeactivateEL2.Checked, txtHelpText.Text, chkDeleteComments.Checked, chkDeleteAddons.Checked, chkDeleteDocuments.Checked, chkAllowMultipost.Checked, Convert.ToInt32(cboAddon.SelectedValue), chkShowHistory.Checked, chkShowDocuments.Checked, chkAllowCancellations.Checked, chkRemain.Checked, chkEditQueue.Checked, chkStepLinks.Checked, chkShowToCreator.Checked, chkShowReject.Checked, chkCustomPost.Checked, chkAlloEditTL.Checked, chkShowTimeline.Checked, chkShowChats.Checked, txtTaskDetailsHeader.Text, txtTaskPostHeader.Text, GetLinedScreens()) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Step cannot be Updated');", true);
                }
                else
                {
                    DS_Workflow ds = objWF.Read(Convert.ToInt32(ViewState["fid"]), false, false);
                    RefreshGrid(ds.tblworkflow_steps);
                    LoadStepCombos(objSes, ds);
                    NewStep(objSes);

                    N_Ter.Common.Common_Task_Actions objCommAct = new N_Ter.Common.Common_Task_Actions();
                    string FlowchartSteps = "";
                    string Flowchart = "";
                    objCommAct.LoadFlowchart(ds.tblwalkflow[0].Walkflow_ID, 0, objWF, objSes.EL2, ref FlowchartSteps, ref Flowchart);
                    ltrFlowChartSteps.Text = FlowchartSteps;
                    ltrFlowChart.Text = Flowchart;

                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Step succesfully Updated');", true);
                }
            }
        }

        private DS_Workflow.tblworkflow_step_linksDataTable GetLinedScreens()
        {
            DS_Workflow ds = new DS_Workflow();
            DS_Workflow.tblworkflow_step_linksRow dr;
            foreach (ListItem itm in chkCustomScreens.Items)
            {
                if (itm.Selected == true)
                {
                    dr = ds.tblworkflow_step_links.Newtblworkflow_step_linksRow();
                    dr.Workflow_Step_ID = 0;
                    dr.Screen_ID = Convert.ToInt32(itm.Value);
                    ds.tblworkflow_step_links.Rows.Add(dr);
                }
            }
            return ds.tblworkflow_step_links;
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            if (hndWFStepID.Value == "0")
            {
                NewStep(objSes);
            }
            else
            {
                LoadStep(objSes);
            }
        }

        protected void cmdStepDeleteOK_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objWF.DeleteStep(Convert.ToInt32(hndWFStepID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                DS_Workflow ds = objWF.Read(Convert.ToInt32(ViewState["fid"]), false, false);
                RefreshGrid(ds.tblworkflow_steps);
                NewStep(objSes);

                N_Ter.Common.Common_Task_Actions objCommAct = new N_Ter.Common.Common_Task_Actions();
                string FlowchartSteps = "";
                string Flowchart = "";
                objCommAct.LoadFlowchart(ds.tblwalkflow[0].Walkflow_ID, 0, objWF, objSes.EL2, ref FlowchartSteps, ref Flowchart);
                ltrFlowChartSteps.Text = FlowchartSteps;
                ltrFlowChart.Text = Flowchart;

                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Step Successfully Deleted');", true);
            }
        }

        protected void cmdDeleteField_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objWF.DeleteField(Convert.ToInt32(hndField_ID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Step Field Successfully Deleted');", true);

                DS_Workflow ds = objWF.ReadStep(Convert.ToInt32(hndWFStepID.Value));
                LoadStepCombos(objSes, ds);
                RefreshFields(ds.tblworkflow_step_fields);
                hndField_ID.Value = "0";
            }
        }

        protected void cmdDeleteFieldCond_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objWF.DeleteStepCondition(Convert.ToInt32(hndFieldCond_ID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Condition Successfully Deleted');", true);

                DS_Workflow ds = objWF.ReadStep(Convert.ToInt32(hndWFStepID.Value));
                LoadStepCombos(objSes, ds);
                RefreshConditions(ds.tblworkflow_conditions);

                N_Ter.Common.Common_Task_Actions objCommAct = new N_Ter.Common.Common_Task_Actions();
                string FlowchartSteps = "";
                string Flowchart = "";
                objCommAct.LoadFlowchart(ds.tblwalkflow[0].Walkflow_ID, 0, objWF, objSes.EL2, ref FlowchartSteps, ref Flowchart);
                ltrFlowChartSteps.Text = FlowchartSteps;
                ltrFlowChart.Text = Flowchart;

                hndFieldCond_ID.Value = "0";
            }
        }

        protected void cmdDeleteFieldFormula_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objWF.DeleteStepFormula(Convert.ToInt32(hndFormula_ID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Formula Successfully Deleted');", true);

                DS_Workflow ds = objWF.ReadStep(Convert.ToInt32(hndWFStepID.Value));
                LoadStepCombos(objSes, ds);
                RefreshFormulas(ds.tblworkflow_formulas);
                hndFormula_ID.Value = "0";
            }
        }

        protected void cmdSaveField_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            string Default_Doc_Cat = "";
            if (cboFieldType.SelectedValue == "11")
            {
                Default_Doc_Cat = cboDocCat.SelectedValue;
            }
            if (hndField_ID.Value == "0")
            {
                byte[] bytes = System.Text.Encoding.Default.GetBytes(txtFieldName.Text);
                txtFieldName.Text = System.Text.Encoding.UTF8.GetString(bytes);
                if (objWF.InsertField(Convert.ToInt32(hndWFStepID.Value), txtFieldName.Text, Convert.ToInt32(cboFieldType.SelectedItem.Value), Convert.ToInt32(cboFieldSize.SelectedItem.Value), txtSelectionText.Text, Convert.ToInt32(cboMasterTable.SelectedValue), Convert.ToInt32(cboMasterTableParamType.SelectedValue), Convert.ToInt32(cboMasterTableParam2Type.SelectedValue), txtDefaultTextMemo.Text, Convert.ToInt32(cboValidateField.SelectedItem.Value), chkFieldRequired.Checked, Convert.ToInt32(string.IsNullOrEmpty(txtFieldMaxLength.Text.Trim(' ')) ? "-1" : txtFieldMaxLength.Text), Convert.ToInt32(cboCopyDataFrom.SelectedItem.Value), chkDisableControl.Checked, Convert.ToInt32(cboFieldCategory.SelectedItem.Value), chkCopyEF1.Checked, chkCopyEF2.Checked, txtFieldHelpText.Text, chkTypable.Checked, Convert.ToInt32(cboAccessLevel.SelectedItem.Value), chkShowInHistory.Checked, txtCustomData.Text, chkAllowMltUpload.Checked, Default_Doc_Cat) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Step Field could not be Saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Step Field Successfully Saved');", true);

                    DS_Workflow ds = objWF.ReadStep(Convert.ToInt32(hndWFStepID.Value));
                    LoadStepCombos(objSes, ds);
                    RefreshFields(ds.tblworkflow_step_fields);
                }
            }
            else
            {
                if (objWF.UpdateField(Convert.ToInt32(hndField_ID.Value), txtFieldName.Text, Convert.ToInt32(cboFieldType.SelectedItem.Value), Convert.ToInt32(cboFieldSize.SelectedItem.Value), txtSelectionText.Text, Convert.ToInt32(cboMasterTable.SelectedValue), Convert.ToInt32(cboMasterTableParamType.SelectedValue), Convert.ToInt32(cboMasterTableParam2Type.SelectedValue), txtDefaultTextMemo.Text, Convert.ToInt32(cboValidateField.SelectedItem.Value), chkFieldRequired.Checked, Convert.ToInt32(string.IsNullOrEmpty(txtFieldMaxLength.Text.Trim(' ')) ? "-1" : txtFieldMaxLength.Text), Convert.ToInt32(cboCopyDataFrom.SelectedItem.Value), chkDisableControl.Checked, Convert.ToInt32(cboFieldCategory.SelectedItem.Value), chkCopyEF1.Checked, chkCopyEF2.Checked, txtFieldHelpText.Text, chkTypable.Checked, Convert.ToInt32(cboAccessLevel.SelectedItem.Value), chkShowInHistory.Checked, txtCustomData.Text, chkAllowMltUpload.Checked, Default_Doc_Cat) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Step Field could not be Saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Step Field Successfully Saved');", true);

                    DS_Workflow ds = objWF.ReadStep(Convert.ToInt32(hndWFStepID.Value));
                    LoadStepCombos(objSes, ds);
                    RefreshFields(ds.tblworkflow_step_fields);
                }
            }
        }

        protected void cmdSaveCondition_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            if (hndFieldCond_ID.Value == "0")
            {
                if (objWF.InsertStepCondition(Convert.ToInt32(cboConditionOn.SelectedItem.Value.Split('_')[0]), Convert.ToInt32(cboOperator.SelectedItem.Value), txtEvaluateWith.Text, Convert.ToInt32(cboMoveToIfTrue.SelectedItem.Value), Convert.ToInt32(hndWFStepID.Value)) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Condition could not be Saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Condition Successfully Saved');", true);

                    DS_Workflow ds = objWF.ReadStep(Convert.ToInt32(hndWFStepID.Value));
                    LoadStepCombos(objSes, ds);
                    RefreshConditions(ds.tblworkflow_conditions);

                    N_Ter.Common.Common_Task_Actions objCommAct = new N_Ter.Common.Common_Task_Actions();
                    string FlowchartSteps = "";
                    string Flowchart = "";
                    objCommAct.LoadFlowchart(ds.tblwalkflow[0].Walkflow_ID, 0, objWF, objSes.EL2, ref FlowchartSteps, ref Flowchart);
                    ltrFlowChartSteps.Text = FlowchartSteps;
                    ltrFlowChart.Text = Flowchart;
                }
            }
            else
            {
                if (objWF.UpdateStepCondition(Convert.ToInt32(hndFieldCond_ID.Value), Convert.ToInt32(cboConditionOn.SelectedItem.Value.Split('_')[0]), Convert.ToInt32(cboOperator.SelectedItem.Value), txtEvaluateWith.Text, Convert.ToInt32(cboMoveToIfTrue.SelectedItem.Value), Convert.ToInt32(hndWFStepID.Value)) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Condition could not be Saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Condition Successfully Saved');", true);

                    DS_Workflow ds = objWF.ReadStep(Convert.ToInt32(hndWFStepID.Value));
                    LoadStepCombos(objSes, ds);
                    RefreshConditions(ds.tblworkflow_conditions);

                    N_Ter.Common.Common_Task_Actions objCommAct = new N_Ter.Common.Common_Task_Actions();
                    string FlowchartSteps = "";
                    string Flowchart = "";
                    objCommAct.LoadFlowchart(ds.tblwalkflow[0].Walkflow_ID, 0, objWF, objSes.EL2, ref FlowchartSteps, ref Flowchart);
                    ltrFlowChartSteps.Text = FlowchartSteps;
                    ltrFlowChart.Text = Flowchart;
                }
            }
        }

        protected void cmdSaveFormula_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            if (hndFormula_ID.Value == "0")
            {
                if (hndFormula_Type.Value == "1")
                {
                    if (objWF.InsertStepFormula(Convert.ToInt32(hndWFStepID.Value), 1, Convert.ToInt32(cboT1Date1.SelectedItem.Value), Convert.ToInt32(cboT1Time1.SelectedItem.Value), Convert.ToInt32(cboT1Date2.SelectedItem.Value), Convert.ToInt32(cboT1Time2.SelectedItem.Value), 0, 0, Convert.ToInt32(cboT1Result.SelectedItem.Value), 0))
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Formula Successfully Saved');", true);

                        DS_Workflow ds = objWF.ReadStep(Convert.ToInt32(hndWFStepID.Value));
                        LoadStepCombos(objSes, ds);
                        RefreshFormulas(ds.tblworkflow_formulas);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Formula could not be Saved');", true);
                    }
                }
                else if (hndFormula_Type.Value == "2")
                {
                    if (objWF.InsertStepFormula(Convert.ToInt32(hndWFStepID.Value), 2, Convert.ToInt32(cboT2Date1.SelectedItem.Value), 0, 0, 0, Convert.ToInt32(cboT2Days.SelectedItem.Value), 0, Convert.ToInt32(cboT2Result.SelectedItem.Value), 0))
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Formula Successfully Saved');", true);

                        DS_Workflow ds = objWF.ReadStep(Convert.ToInt32(hndWFStepID.Value));
                        LoadStepCombos(objSes, ds);
                        RefreshFormulas(ds.tblworkflow_formulas);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Formula could not be Saved');", true);
                    }
                }
                else
                {
                    if (objWF.InsertStepFormula(Convert.ToInt32(hndWFStepID.Value), 3, 0, 0, 0, 0, Convert.ToInt32(cboT3Number1.SelectedItem.Value), Convert.ToInt32(cboT3Number2.SelectedItem.Value), Convert.ToInt32(cboT3Result.SelectedItem.Value), Convert.ToInt32(cboT3Operator.SelectedItem.Value)))
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Formula Successfully Saved');", true);

                        DS_Workflow ds = objWF.ReadStep(Convert.ToInt32(hndWFStepID.Value));
                        LoadStepCombos(objSes, ds);
                        RefreshFormulas(ds.tblworkflow_formulas);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Formula could not be Saved');", true);
                    }
                }
            }
            else
            {
                if (hndFormula_Type.Value == "1")
                {
                    if (objWF.UpdateStepFormula(Convert.ToInt32(hndFormula_ID.Value), Convert.ToInt32(cboT1Date1.SelectedItem.Value), Convert.ToInt32(cboT1Time1.SelectedItem.Value), Convert.ToInt32(cboT1Date2.SelectedItem.Value), Convert.ToInt32(cboT1Time2.SelectedItem.Value), 0, 0, Convert.ToInt32(cboT1Result.SelectedItem.Value), 0))
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Formula Successfully Updated');", true);

                        DS_Workflow ds = objWF.ReadStep(Convert.ToInt32(hndWFStepID.Value));
                        LoadStepCombos(objSes, ds);
                        RefreshFormulas(ds.tblworkflow_formulas);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Formula could not be Saved');", true);
                    }
                }
                else if (hndFormula_Type.Value == "2")
                {
                    if (objWF.UpdateStepFormula(Convert.ToInt32(hndFormula_ID.Value), Convert.ToInt32(cboT2Date1.SelectedItem.Value), 0, 0, 0, Convert.ToInt32(cboT2Days.SelectedItem.Value), 0, Convert.ToInt32(cboT2Result.SelectedItem.Value), 0))
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Formula Successfully Updated');", true);

                        DS_Workflow ds = objWF.ReadStep(Convert.ToInt32(hndWFStepID.Value));
                        LoadStepCombos(objSes, ds);
                        RefreshFormulas(ds.tblworkflow_formulas);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Formula could not be Saved');", true);
                    }
                }
                else
                {
                    if (objWF.UpdateStepFormula(Convert.ToInt32(hndFormula_ID.Value), 0, 0, 0, 0, Convert.ToInt32(cboT3Number1.SelectedItem.Value), Convert.ToInt32(cboT3Number2.SelectedItem.Value), Convert.ToInt32(cboT3Result.SelectedItem.Value), Convert.ToInt32(cboT3Operator.SelectedItem.Value)))
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Formula Successfully Updated');", true);

                        DS_Workflow ds = objWF.ReadStep(Convert.ToInt32(hndWFStepID.Value));
                        LoadStepCombos(objSes, ds);
                        RefreshFormulas(ds.tblworkflow_formulas);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Formula could not be Saved');", true);
                    }
                }
            }
        }

        protected void cmdSaveFieldCat_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            if (hndStepFieldCat_ID.Value == "0")
            {
                if (objWF.InsertFieldCategory(Convert.ToInt32(hndWFStepID.Value), txtStepFieldCat.Text) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Step Field Category could not be Saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Step Field Category Successfully Saved');", true);

                    DS_Workflow ds = objWF.ReadStep(Convert.ToInt32(hndWFStepID.Value));
                    RefreshStepFieldCats(ds.tblworkflow_step_field_cats);
                    LoadStepFieldCats(ds.tblworkflow_step_field_cats);
                }
            }
            else
            {
                if (objWF.UpdateFieldCategory(Convert.ToInt32(hndStepFieldCat_ID.Value), txtStepFieldCat.Text) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Step Field Category could not be Saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Step Field Category Successfully Saved');", true);

                    DS_Workflow ds = objWF.ReadStep(Convert.ToInt32(hndWFStepID.Value));
                    RefreshStepFieldCats(ds.tblworkflow_step_field_cats);
                    LoadStepFieldCats(ds.tblworkflow_step_field_cats);
                }
            }
        }

        protected void cmdDeleteFieldCat_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objWF.DeleteFieldCat(Convert.ToInt32(hndStepFieldCat_ID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Step Field Category Successfully Deleted');", true);

                DS_Workflow ds = objWF.ReadStep(Convert.ToInt32(hndWFStepID.Value));
                RefreshStepFieldCats(ds.tblworkflow_step_field_cats);
                LoadStepFieldCats(ds.tblworkflow_step_field_cats);
            }
        }

        protected void gvStepFieldCats_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdEdit = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[1].Controls[1]);
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndStepFieldCat_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadStepFieldCatValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndStepFieldCat_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
                e.Row.Attributes.Add("data-id", e.Row.Cells[0].Text);
            }
        }

        protected void cmdCreateDuplicate_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            if (objWF.CreateDuplicateWorkflowStep(Convert.ToInt32(ViewState["fid"]), Convert.ToInt32(hndWFStepID.Value), txtNewStep.Text))
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Duplicate Step Successfully Created');", true);
                txtNewStep.Text = "";
                DS_Workflow ds = objWF.Read(Convert.ToInt32(ViewState["fid"]), false, false);
                RefreshGrid(ds.tblworkflow_steps);
                LoadStepCombos(objSes, ds);
                LoadStep(objSes);

                N_Ter.Common.Common_Task_Actions objCommAct = new N_Ter.Common.Common_Task_Actions();
                string FlowchartSteps = "";
                string Flowchart = "";
                objCommAct.LoadFlowchart(ds.tblwalkflow[0].Walkflow_ID, 0, objWF, objSes.EL2, ref FlowchartSteps, ref Flowchart);
                ltrFlowChartSteps.Text = FlowchartSteps;
                ltrFlowChart.Text = Flowchart;
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Duplicate Step could not be Created');", true);
            }
        }
    }
}