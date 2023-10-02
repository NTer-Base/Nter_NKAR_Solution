using N_Ter.Base;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class task_bulk : System.Web.UI.Page
    {
        private Task_Controls_Main objCtrlList = new Task_Controls_Main();

        public string HelpScript = "";
        public string HelpPanelResizeScript = "";
        public string Required_Fields = "";
        public string Custom_Scripts = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            if (IsPostBack == false)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Request.QueryString["bck"])) == false)
                {
                    ViewState["prev"] = Convert.ToString(Request.QueryString["bck"]);
                }

                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                if (string.IsNullOrEmpty(Convert.ToString(Request.QueryString["tids"])) == false)
                {
                    Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
                    N_Ter.Customizable.Master_Tables objMasters = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);
                    string Task_IDs = objURL.Decrypt(Convert.ToString(Request.QueryString["tids"]));
                    Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

                    DS_Users dsAltUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).ReadAssignedUsers(objSes.UserID);
                    List<int> Task_ID_List = new List<int>();

                    bool Can_Access_Tasks = true;
                    foreach (string Task_ID in Task_IDs.Split('|'))
                    {
                        if (Task_ID.Trim() != "")
                        {
                            if (objTask.Can_Access_Task(Convert.ToInt32(Task_ID), objSes.UserID, dsAltUsers.tblusers, objCus) == false)
                            {
                                Can_Access_Tasks = false;
                            }
                            else
                            {
                                Task_ID_List.Add(Convert.ToInt32(Task_ID));
                            }
                        }
                    }
                    if (Can_Access_Tasks)
                    {
                        Task_ID_List.Sort();
                        if (Task_ID_List.Count == 1)
                        {
                            Response.Redirect("task.aspx?tid=" + objURL.Encrypt(Task_ID_List[0].ToString()));
                        }
                        else
                        {
                            ViewState["tids"] = string.Join("|", Task_ID_List);
                        }
                    }
                    else
                    {
                        Response.Redirect("no_access.aspx?");
                    }
                }
                else
                {
                    Response.Redirect("error.aspx?");
                }
            }

            LoadTask(objSes);

            cmdSubmit.Attributes.Add("onClick", "return ValidatRequiredFields();");
            cmdSubmitSpecial.Attributes.Add("onClick", "return ValidatRequiredFields();");
        }

        private void LoadTask(SessionObject objSes)
        {
            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();

            string Task_IDs = Convert.ToString(ViewState["tids"]);
            List<int> Task_ID_List = Task_IDs.Split('|').Select(x => Convert.ToInt32(x)).ToList();

            DS_Tasks dsHistoryUpdate;
            List<DS_Tasks.tbltask_historyRow> drLastTaskHistory;
            ltrTaskDetails.Text = "<table class=\"table\">" + "\r\n" +
                                    "<thead>" + "\r\n" +
                                        "<tr>" + "\r\n" +
                                            "<th>#</th>" + "\r\n" +
                                            "<th>" + objSes.EL2 + " Name</th>" + "\r\n" +
                                            "<th>Last Posted</th>" + "\r\n" +
                                            "<th>Created Date</th>" + "\r\n" +
                                            "<th>Due Date</th>" + "\r\n" +
                                        "</tr>" + "\r\n" +
                                    "</thead>" + "\r\n" +
                                    "<tbody>" + "\r\n";

            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            List<DS_Tasks> All_Tasks = new List<DS_Tasks>();
            foreach (int Task_ID in Task_ID_List)
            {
                dsHistoryUpdate = objTask.Read(Task_ID, false, false, false);
                if (dsHistoryUpdate.tbltasks.Rows.Count > 0)
                {
                    drLastTaskHistory = dsHistoryUpdate.tbltask_history.OrderByDescending(x => x.Task_Update_ID).ToList();
                    if (IsPostBack == false)
                    {
                        objTask.Update_Task_Lock(Task_ID, drLastTaskHistory[0].Task_Update_ID, objSes.UserID);
                    }
                    ltrTaskDetails.Text = ltrTaskDetails.Text + "<tr>" + "\r\n" +
                                                                    "<td>" + dsHistoryUpdate.tbltasks[0].Task_Number + "</td>" + "\r\n" +
                                                                    "<td>" + dsHistoryUpdate.tbltasks[0].Display_Name + "</td>" + "\r\n" +
                                                                    "<td>" + dsHistoryUpdate.tbltasks[0].Edited_User + "</td>" + "\r\n" +
                                                                    "<td>" + string.Format("{0:" + Constants.DateFormat + "}", dsHistoryUpdate.tbltasks[0].Task_Date) + "</td>" + "\r\n" +
                                                                    "<td>" + string.Format("{0:" + Constants.DateFormat + "}", dsHistoryUpdate.tbltasks[0].ETB_Value) + "</td>" + "\r\n" +
                                                                "</tr>" + "\r\n";
                }
                All_Tasks.Add(dsHistoryUpdate);
            }
            ltrTaskDetails.Text = ltrTaskDetails.Text + "</tbody>" + "\r\n" +
                                                    "</table>" + "\r\n";
            bool ProcessBulk = true;
            if (All_Tasks.Count > 0)
            {
                int Current_Step = All_Tasks[0].tbltasks[0].Current_Step_ID;
                foreach (DS_Tasks dsTask in All_Tasks)
                {
                    if (dsTask.tbltasks[0].Current_Step_ID != Current_Step)
                    {
                        ProcessBulk = false;
                    }
                }
            }
            if (ProcessBulk == true)
            {
                DS_Tasks dsSampleTask = All_Tasks[0];
                if (dsSampleTask.tbltasks[0].Current_Step_ID != -1 && dsSampleTask.tbltasks[0].Current_Step_ID != -2)
                {
                    if (IsPostBack == false)
                    {
                        ViewState["csid"] = dsSampleTask.tbltasks[0].Current_Step_ID;
                    }
                    else
                    {
                        foreach (DS_Tasks dsTsk in All_Tasks)
                        {
                            dsTsk.tbltasks[0].Current_Step_ID = Convert.ToInt32(ViewState["csid"]);
                        }
                    }

                    ltrWorkflowName.Text = dsSampleTask.tbltasks[0].Workflow_Name;
                    ltrCurrentStep.Text = dsSampleTask.tbltasks[0].Step_Status;

                    Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                    DS_Workflow dsWF = objWF.Read(dsSampleTask.tbltasks[0].Walkflow_ID, false, false);

                    List<DS_Workflow.tblworkflow_stepsRow> drCurrentStep = dsWF.tblworkflow_steps.Where(x => x.Workflow_Step_ID == dsSampleTask.tbltasks[0].Current_Step_ID).ToList();

                    if (drCurrentStep.Count > 0 && drCurrentStep[0].Allow_Task_Cancellation == true)
                    {
                        string Task_ID_Special = "";
                        foreach (int Task_ID in Task_ID_List)
                        {
                            Task_ID_Special = Task_ID_Special + Task_ID.ToString() + ",";
                        }
                        if (Task_ID_Special.Trim() != "")
                        {
                            Task_ID_Special = Task_ID_Special.Substring(0, Task_ID_Special.Length - 1);
                        }
                        DS_Tasks dsSubTasks = objTask.GetActiveSubTasks(Task_ID_Special);
                        if (dsSubTasks.tbltasks.Rows.Count > 0)
                        {
                            pnlConfirmCancel.Visible = false;
                            cmdCancel_ModalPopupExtender.Enabled = false;
                            cmdCancel.Visible = false;
                        }
                        else
                        {
                            pnlConfirmCancel.Visible = true;
                            cmdCancel_ModalPopupExtender.Enabled = true;
                            cmdCancel.Visible = true;
                        }
                    }
                    else
                    {
                        pnlConfirmCancel.Visible = false;
                        cmdCancel_ModalPopupExtender.Enabled = false;
                        cmdCancel.Visible = false;
                    }

                    if (IsPostBack == false)
                    {
                        if (drCurrentStep[0].Show_Reject_Button == false || dsWF.tblworkflow_steps[0].Workflow_Step_ID == dsSampleTask.tbltasks[0].Current_Step_ID)
                        {
                            cmdReject.Visible = false;
                        }

                        DS_Workflow.tblworkflow_stepsRow drStep = dsWF.tblworkflow_steps.Newtblworkflow_stepsRow();
                        drStep.Workflow_Step_ID = -1;
                        drStep.Step_Status = "Completed";
                        dsWF.tblworkflow_steps.Rows.Add(drStep);

                        DS_Workflow.tblworkflow_stepsRow drStep2 = dsWF.tblworkflow_steps.Newtblworkflow_stepsRow();
                        drStep2.Workflow_Step_ID = -2;
                        drStep2.Step_Status = "Cancelled";
                        dsWF.tblworkflow_steps.Rows.Add(drStep2);

                        bool isApproverStep = false;

                        if (drCurrentStep.Count > 0 && drCurrentStep[0].isApproverStep)
                        {
                            isApproverStep = true;
                        }

                        if (isApproverStep == true)
                        {
                            divAnyStep.Visible = true;
                            bool blnAddToList = false;
                            DS_Workflow dsWF_Copy = new DS_Workflow();
                            DS_Workflow.tblworkflow_stepsRow drWF_Copy;

                            foreach (DS_Workflow.tblworkflow_stepsRow rowWF in dsWF.tblworkflow_steps)
                            {
                                if (rowWF.Workflow_Step_ID == dsSampleTask.tbltasks[0].Current_Step_ID)
                                {
                                    blnAddToList = true;
                                }
                                if (blnAddToList == true && rowWF.isApproverStep == false)
                                {
                                    blnAddToList = false;
                                }
                                if (blnAddToList == true)
                                {
                                    drWF_Copy = dsWF_Copy.tblworkflow_steps.Newtblworkflow_stepsRow();
                                    drWF_Copy.ItemArray = rowWF.ItemArray;
                                    dsWF_Copy.tblworkflow_steps.Rows.Add(drWF_Copy);
                                }
                            }
                            if (dsWF_Copy.tblworkflow_steps.Rows.Count > 0)
                            {
                                cboSteps.DataSource = dsWF_Copy.tblworkflow_steps;
                                cboSteps.DataValueField = "Workflow_Step_ID";
                                cboSteps.DataTextField = "Step_Status";
                                cboSteps.DataBind();
                            }
                            else
                            {
                                divAnyStep.Visible = false;
                            }
                        }
                        else
                        {
                            divAnyStep.Visible = false;
                        }
                    }

                    DS_Workflow dsCurentStep = objWF.ReadStep(dsSampleTask.tbltasks[0].Current_Step_ID);
                    if (dsCurentStep.tblsub_workflows.Where(x => x.isStart && !x.isAutomatic).Count() > 0)
                    {
                        divSubWorkflowError.Visible = true;
                    }
                    else
                    {
                        divSubWorkflowError.Visible = false;
                    }

                    LoadTaskDetails(All_Tasks, objWF.Read(dsSampleTask.tbltasks[0].Walkflow_ID, false, false), objSes);
                    LoadCustomScripts(dsSampleTask, objSes);
                }
                else
                {
                    Response.Redirect("error.aspx?");
                }
            }
            else
            {
                Response.Redirect("error.aspx?");
            }
        }

        private void LoadTaskDetails(List<DS_Tasks> All_Tasks, DS_Workflow dsWF, SessionObject objSes)
        {
            N_Ter.Common.Script_Generator objScripts = new N_Ter.Common.Script_Generator();
            DS_Tasks dsSampleTask = All_Tasks[0];

            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow dsLastSteps = objWF.ReadLastStepsForWF(dsSampleTask.tbltasks[0].Walkflow_ID);

            bool ProgressOK = false;
            foreach (DS_Workflow.tblworkflow_stepsRow row in dsLastSteps.tblworkflow_steps)
            {
                if (dsSampleTask.tbltasks[0].Sort_order <= row.Sort_order)
                {
                    dsSampleTask.tbltasks[0].Progress_Value = (int)((double)(dsSampleTask.tbltasks[0].Sort_order - 1) / (double)row.Sort_order * (double)100);
                    ProgressOK = true;
                    break;
                }
            }
            if (ProgressOK == false)
            {
                dsSampleTask.tbltasks[0].Progress_Value = (int)((double)(dsSampleTask.tbltasks[0].Sort_order - 1) / (double)dsWF.tblworkflow_steps.Rows.Count * (double)100);
            }
            divProgress.Attributes["style"] = "width: " + dsSampleTask.tbltasks[0].Progress_Value + "%";

            DS_Workflow dsWorkflow = objWF.ReadStep(dsSampleTask.tbltasks[0].Current_Step_ID);

            if (dsWorkflow.tblworkflow_step_fields.Where(x => x.Validate_With_Field_ID > 0).Count() > 0)
            {
                divFieldValidationsError.Visible = true;
            }
            else
            {
                divFieldValidationsError.Visible = false;
            }

            if (dsWorkflow.tblworkflow_formulas.Rows.Count > 0)
            {
                divStepFormulaError.Visible = true;
            }
            else
            {
                divStepFormulaError.Visible = false;
            }

            string strRequiredFieldValidation = "";
            Required_Fields = "function ValidatRequiredFields() {" + "\r\n" +
                                    "remove_field_erros();" + "\r\n" +
                                    "var ret = true;" + "\r\n";

            Required_Fields = Required_Fields + ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostScripts(dsSampleTask, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            string strOldFieldValidation = "";

            objCtrlList.Task_ID = dsSampleTask.tbltasks[0].Task_ID;
            objCtrlList.Current_Step_ID = dsSampleTask.tbltasks[0].Current_Step_ID;

            pnlStepData.Controls.Clear();

            int ControlIndex = 0;
            List<string> Help_Texts = new List<string>();

            N_Ter.Customizable.Master_Tables objMasterTables = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);
            N_Ter.Common.Common_Task_Actions objTskAct = new N_Ter.Common.Common_Task_Actions();

            if (dsWorkflow.tblworkflow_step_fields.Where(x => x.Workflow_Step_Field_Cat_ID == 0).Count() > 0)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                int rowWidth = 0;

                pnlStepData.CssClass = "row";
                foreach (DS_Workflow.tblworkflow_step_fieldsRow rowStepField in dsWorkflow.tblworkflow_step_fields)
                {
                    divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, All_Tasks, rowStepField, ref objCtrlList, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                    if (rowStepField.Help_Text.Trim() != "")
                    {
                        Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                        ControlIndex++;
                    }
                    if (rowWidth == 12)
                    {
                        pnlStepData.Controls.Add(divMainRowControl);
                        divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                        divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                        rowWidth = 0;
                    }
                }
                if (rowWidth > 0)
                {
                    pnlStepData.Controls.Add(divMainRowControl);
                }
            }
            else
            {
                pnlStepData.CssClass = "panel-group no-margin-b";
                int tabIndex = 0;
                foreach (DS_Workflow.tblworkflow_step_field_catsRow rowCats in dsWorkflow.tblworkflow_step_field_cats)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl divOuterPanel = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                    divOuterPanel.Attributes.Add("class", "panel");
                    System.Web.UI.HtmlControls.HtmlGenericControl divPanelHeader = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                    divPanelHeader.Attributes.Add("class", "panel-heading");
                    System.Web.UI.HtmlControls.HtmlGenericControl aPanelHeader = new System.Web.UI.HtmlControls.HtmlGenericControl("a");
                    if (tabIndex == 0)
                    {
                        aPanelHeader.Attributes.Add("class", "accordion-toggle");
                    }
                    else
                    {
                        aPanelHeader.Attributes.Add("class", "accordion-toggle collapsed");
                    }
                    aPanelHeader.Attributes.Add("data-toggle", "collapse");
                    aPanelHeader.Attributes.Add("data-parent", "#" + pnlStepData.ClientID);
                    aPanelHeader.Attributes.Add("href", "#collapse" + tabIndex);
                    aPanelHeader.InnerHtml = rowCats.Workflow_Step_Field_Cat;
                    divPanelHeader.Controls.Add(aPanelHeader);
                    divOuterPanel.Controls.Add(divPanelHeader);

                    System.Web.UI.HtmlControls.HtmlGenericControl divPanelBody = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                    divPanelBody.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    divPanelBody.ID = "collapse" + tabIndex;
                    if (tabIndex == 0)
                    {
                        divPanelBody.Attributes.Add("class", "panel-collapse in");
                    }
                    else
                    {
                        divPanelBody.Attributes.Add("class", "panel-collapse collapse");
                    }
                    System.Web.UI.HtmlControls.HtmlGenericControl divPanelInner = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                    divPanelInner.Attributes.Add("class", "panel-body");
                    System.Web.UI.HtmlControls.HtmlGenericControl divPanelInnerRow = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                    divPanelInnerRow.Attributes.Add("class", "row");

                    System.Web.UI.HtmlControls.HtmlGenericControl divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                    divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                    int rowWidth = 0;

                    foreach (DS_Workflow.tblworkflow_step_fieldsRow rowStepField in dsWorkflow.tblworkflow_step_fields.Where(x => x.Workflow_Step_Field_Cat_ID == rowCats.Workflow_Step_Field_Cat_ID))
                    {
                        divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, All_Tasks, rowStepField, ref objCtrlList, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                        if (rowStepField.Help_Text.Trim() != "")
                        {
                            Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                            ControlIndex++;
                        }
                        if (rowWidth == 12)
                        {
                            divPanelInnerRow.Controls.Add(divMainRowControl);
                            divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                            rowWidth = 0;
                        }
                    }
                    if (rowWidth > 0)
                    {
                        divPanelInnerRow.Controls.Add(divMainRowControl);
                    }

                    divPanelInner.Controls.Add(divPanelInnerRow);
                    divPanelBody.Controls.Add(divPanelInner);
                    divOuterPanel.Controls.Add(divPanelBody);

                    pnlStepData.Controls.Add(divOuterPanel);
                    tabIndex++;
                }
            }

            Required_Fields = Required_Fields + strRequiredFieldValidation + "return ret;" + "\r\n" +
                                                                          "}";
            if (Help_Texts.Count > 0)
            {
                hndHelp_ModalPopupExtender.Enabled = true;
                pnlHelp.Visible = true;
                objScripts.LoadHelpScripts(ref HelpScript, ref HelpPanelResizeScript, Help_Texts);
            }
            else
            {
                HelpScript = "";
                HelpPanelResizeScript = "";
                hndHelp_ModalPopupExtender.Enabled = false;
                pnlHelp.Visible = false;
            }
        }

        private void LoadCustomScripts(DS_Tasks ds, SessionObject objSes)
        {
            string CusScript = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).LoadTaskRelatedScripts(ds, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            Custom_Scripts = CusScript;
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SessionObject objSes = (SessionObject)Session["dt"];
                NextStepSubmit(objCtrlList, objSes);
                Response.Redirect(GetPreviousPage());
            }
            catch (Exception ex)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + ex.Message + "');", true);
            }
        }

        protected void cmdSubmitSpecial_Click(object sender, EventArgs e)
        {
            try
            {
                SessionObject objSes = (SessionObject)Session["dt"];
                DirectSubmit(objCtrlList, objSes);
                Response.Redirect(GetPreviousPage());
            }
            catch (Exception ex)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + ex.Message + "');", true);
            }
        }

        private bool ValidateTaskPost(int Session_Step_ID, int Page_Step_ID, int Current_Step_ID, string Task_Number, ref bool isMultiPost, SessionObject objSes)
        {
            bool ret = false;
            if (Session_Step_ID != Current_Step_ID)
            {
                throw new Exception("There is a missmatch on the Posted Data of the Task : " + Task_Number + ". Please contact the System Administrator");
            }
            else if (Page_Step_ID != Current_Step_ID)
            {
                DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).ReadStep(Page_Step_ID);
                if (dsWF.tblworkflow_steps.Rows.Count > 0)
                {
                    if (dsWF.tblworkflow_steps[0].Allow_Multipost)
                    {
                        isMultiPost = true;
                        ret = true;
                    }
                    else
                    {
                        throw new Exception("The Task : " + Task_Number + " is already submitted by another user.");
                    }
                }
                else
                {
                    throw new Exception("The Task : " + Task_Number + " is already submitted by another user.");
                }
            }
            else
            {
                ret = true;
            }
            return ret;
        }

        private void DirectSubmit(Task_Controls_Main objList, SessionObject objSes)
        {
            string Task_IDs = Convert.ToString(ViewState["tids"]);
            List<int> Task_ID_List = new List<int>();

            DS_Tasks dsCurrentTask;
            int Task_Position = 0;

            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
            N_Ter.Customizable.Master_Tables objMasterTables = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);

            ActionValidated objAct;

            foreach (string Task_ID in Task_IDs.Split('|'))
            {
                if (Task_ID.Trim() != "")
                {
                    dsCurrentTask = objTask.Read(Convert.ToInt32(Task_ID), false, false, false);

                    bool isMultiPost = false;
                    if (ValidateTaskPost(objList.Current_Step_ID, Convert.ToInt32(ViewState["csid"]), dsCurrentTask.tbltasks[0].Current_Step_ID, dsCurrentTask.tbltasks[0].Task_Number, ref isMultiPost, objSes))
                    {
                        //Validate Filled Details 
                        string Invalid_Fields = "";
                        Common_Task_Actions objCommAct = new Common_Task_Actions();
                        objCommAct.PrepareControlsDataSet(null, objSes, false, ref objList);
                        bool All_Fields_Validated = objCommAct.Validate_Filled_Data(objList, ref Invalid_Fields);

                        if (All_Fields_Validated == false)
                        {
                            throw new Exception("Task : " + dsCurrentTask.tbltasks[0].Task_Number + " Data Format Error - Please check the Following Values<br/>" + Invalid_Fields);
                        }
                        else
                        {
                            objAct = objCus.CustomTaskPostValidations(dsCurrentTask.tbltasks[0].Task_ID, dsCurrentTask.tbltasks[0].Walkflow_ID, dsCurrentTask.tbltasks[0].Current_Step_ID, objList, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                            if (objAct.Validated == true)
                            {
                                objCommAct.PrepareControlsDataSet(dsCurrentTask, objSes, true, ref objList);

                                SaveStep(dsCurrentTask, objTask, Task_Position, objList, objSes.UserID, isMultiPost, objSes);

                                if (isMultiPost == false)
                                {
                                    DS_Tasks.tbltask_historyRow drLastTaskHistory = dsCurrentTask.tbltask_history.OrderByDescending(x => x.Task_Update_ID).First();
                                    objTask.UpdateStepFinishedDetails(drLastTaskHistory.Task_Update_ID, objSes.UserID, false);
                                    objTask.UpdateTaskHistory(Convert.ToInt32(Task_ID), Convert.ToInt32(cboSteps.SelectedItem.Value), objSes.UserID, objSes.FullName, true, objCus, objMasterTables);

                                    DS_Tasks dsNewTask = objTask.Read(Convert.ToInt32(Task_ID), false, false, false);
                                    if (dsNewTask.tbltasks[0].IsIs_Auto_SubmitNull() == false && dsNewTask.tbltasks[0].Is_Auto_Submit)
                                    {
                                        Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                                        while (dsNewTask.tbltasks[0].IsIs_Auto_SubmitNull() == false && dsNewTask.tbltasks[0].Is_Auto_Submit)
                                        {
                                            if (objCommAct.AutoSubmit(dsNewTask, objTask, objWF, objCus, objMasterTables, objSes.UserID, objSes.FullName))
                                            {
                                                dsNewTask = objTask.Read(Convert.ToInt32(Task_ID), false, false, false);
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    objTask.Update_Task_Lock(Convert.ToInt32(Task_ID), 0, 0);
                                }
                            }
                        }
                    }
                    Task_Position++;
                }
            }
        }

        private void NextStepSubmit(Task_Controls_Main objList, SessionObject objSes)
        {
            try
            {
                string Task_IDs = Convert.ToString(ViewState["tids"]);

                DS_Tasks dsCurrentTask;
                int Task_Position = 0;

                Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
                N_Ter.Customizable.Master_Tables objMasterTables = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);

                ActionValidated objAct;

                foreach (string Task_ID in Task_IDs.Split('|'))
                {
                    if (Task_ID.Trim() != "")
                    {
                        dsCurrentTask = objTask.Read(Convert.ToInt32(Task_ID), false, false, false);

                        objAct = objCus.CustomTaskPostValidations(dsCurrentTask.tbltasks[0].Task_ID, dsCurrentTask.tbltasks[0].Walkflow_ID, dsCurrentTask.tbltasks[0].Current_Step_ID, objList, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                        if (objAct.Validated == true)
                        {
                            bool isMultiPost = false;
                            if (ValidateTaskPost(objList.Current_Step_ID, Convert.ToInt32(ViewState["csid"]), dsCurrentTask.tbltasks[0].Current_Step_ID, dsCurrentTask.tbltasks[0].Task_Number, ref isMultiPost, objSes))
                            {
                                Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                                DS_Workflow dsCurrentStep = objWF.ReadStep(dsCurrentTask.tbltasks[0].Current_Step_ID);

                                //check whether the task is completed
                                if (dsCurrentStep.tblworkflow_steps.Rows.Count == 0)
                                {
                                    throw new Exception("Submit Failed for Task : " + dsCurrentTask.tbltasks[0].Task_Number + ", Task is currently not active.");
                                }
                                else
                                {
                                    //check whether the creator cannot submit
                                    if (dsCurrentStep.tblworkflow_steps[0].Creator_Cannot_Submit == true && dsCurrentTask.tbltasks[0].Creator_ID == objSes.UserID)
                                    {
                                        throw new Exception("Task : " + dsCurrentTask.tbltasks[0].Task_Number + ", You cannot Approve/Validate a task you created");
                                    }
                                    else
                                    {
                                        //check to finish all sub tasks
                                        bool FinishSubsOK = true;
                                        List<DS_Workflow.tblsub_workflowsRow> drSubs = dsCurrentStep.tblsub_workflows.Where(x => x.isStart == false).ToList();
                                        if (drSubs.Count > 0)
                                        {
                                            DS_Tasks dsSubTasks = objTask.GetActiveSubTasks(Convert.ToInt32(Task_ID));
                                            if (dsSubTasks.tbltasks.Where(x => drSubs.Select(y => y.Walkflow_ID).Contains(x.Walkflow_ID)).Count() > 0)
                                            {
                                                FinishSubsOK = false;
                                            }
                                        }
                                        if (FinishSubsOK == false)
                                        {
                                            throw new Exception("Task : " + dsCurrentTask.tbltasks[0].Task_Number + " - Cannot Submit - There are Sub Tasks which should to be complated");
                                        }
                                        else
                                        {
                                            //Validate Filled Details
                                            string Invalid_Fields = "";
                                            Common_Task_Actions objCommAct = new Common_Task_Actions();
                                            objCommAct.PrepareControlsDataSet(null, objSes, false, ref objList);
                                            bool All_Fields_Validated = objCommAct.Validate_Filled_Data(objList, ref Invalid_Fields);

                                            if (All_Fields_Validated == false)
                                            {
                                                throw new Exception("Task : " + dsCurrentTask.tbltasks[0].Task_Number + " Data Format Error - Please check the Following Values<br/>" + Invalid_Fields);
                                            }
                                            else
                                            {
                                                objCommAct.PrepareControlsDataSet(dsCurrentTask, objSes, true, ref objList);

                                                SaveStep(dsCurrentTask, objTask, Task_Position, objList, objSes.UserID, isMultiPost, objSes);

                                                if (isMultiPost == false)
                                                {
                                                    DS_Tasks.tbltask_historyRow drLastTaskHistory = dsCurrentTask.tbltask_history.OrderByDescending(x => x.Task_Update_ID).First();
                                                    DateTime LastUpdateDate = drLastTaskHistory.Posted_Date;

                                                    int NextStepID = objCommAct.FindNextStep(LastUpdateDate, objWF, dsCurrentTask, dsCurrentStep, objList, objSes.UserID);

                                                    objTask.UpdateStepFinishedDetails(drLastTaskHistory.Task_Update_ID, objSes.UserID, false);

                                                    objTask.UpdateTaskHistory(Convert.ToInt32(Task_ID), NextStepID, objSes.UserID, objSes.FullName, true, objCus, objMasterTables);

                                                    if (NextStepID > 0)
                                                    {
                                                        //check for auto submit steps
                                                        DS_Tasks dsNewTask = objTask.Read(Convert.ToInt32(Task_ID), false, false, false);
                                                        if (dsNewTask.tbltasks[0].IsIs_Auto_SubmitNull() == false && dsNewTask.tbltasks[0].Is_Auto_Submit)
                                                        {
                                                            while (dsNewTask.tbltasks[0].IsIs_Auto_SubmitNull() == false && dsNewTask.tbltasks[0].Is_Auto_Submit)
                                                            {
                                                                if (objCommAct.AutoSubmit(dsNewTask, objTask, objWF, objCus, objMasterTables, objSes.UserID, objSes.FullName))
                                                                {
                                                                    dsNewTask = objTask.Read(Convert.ToInt32(Task_ID), false, false, false);
                                                                }
                                                                else
                                                                {
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    objTask.Update_Task_Lock(Convert.ToInt32(Task_ID), 0, 0);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        Task_Position++;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void cmdRejectConfirm_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
            N_Ter.Customizable.Master_Tables objMasters = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);

            string Task_IDs = Convert.ToString(ViewState["tids"]);

            DS_Tasks dsCurrentTask;
            int Task_Position = 0;
            List<DS_Tasks.tbltask_historyRow> drLastTaskHistory;

            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            try
            {
                Common_Task_Actions objComAct = new Common_Task_Actions();

                foreach (string Task_ID in Task_IDs.Split('|'))
                {
                    if (Task_ID.Trim() != "")
                    {
                        dsCurrentTask = objTask.Read(Convert.ToInt32(Task_ID), false, false, false);
                        bool isMultiPost = false;
                        if (ValidateTaskPost(objCtrlList.Current_Step_ID, Convert.ToInt32(ViewState["csid"]), dsCurrentTask.tbltasks[0].Current_Step_ID, dsCurrentTask.tbltasks[0].Task_Number, ref isMultiPost, objSes))
                        {
                            objComAct.PrepareControlsDataSet(dsCurrentTask, objSes, true, ref objCtrlList);

                            SaveStep(dsCurrentTask, objTask, Task_Position, objCtrlList, objSes.UserID, isMultiPost, objSes);

                            if (isMultiPost == false)
                            {
                                drLastTaskHistory = dsCurrentTask.tbltask_history.OrderByDescending(x => x.Task_Update_ID).ToList();
                                objTask.UpdateStepFinishedDetails(drLastTaskHistory[0].Task_Update_ID, objSes.UserID, true);

                                Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                                DS_Workflow dsWF = objWF.Read(dsCurrentTask.tbltasks[0].Walkflow_ID, false, false);

                                int StepID = objWF.ReadLastWorkflowStep(Convert.ToInt32(Task_ID));
                                objTask.UpdateTaskHistory(Convert.ToInt32(Task_ID), StepID, objSes.UserID, objSes.FullName, false, objCus, objMasters);
                            }
                            else
                            {
                                objTask.Update_Task_Lock(Convert.ToInt32(Task_ID), 0, 0);
                            }
                        }
                        Task_Position++;
                    }
                }
                Response.Redirect(GetPreviousPage());
            }
            catch (Exception ex)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + ex.Message + "');", true);
            }
        }

        protected void cmdReleaseConfirm_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            string Task_IDs = Convert.ToString(ViewState["tids"]);

            DS_Tasks dsEachTask;
            List<DS_Tasks.tbltask_historyRow> drLastTaskHistory;

            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            foreach (string Task_ID in Task_IDs.Split('|'))
            {
                if (Task_ID.Trim() != "")
                {
                    dsEachTask = objTask.Read(Convert.ToInt32(Task_ID), false, false, false);
                    drLastTaskHistory = dsEachTask.tbltask_history.OrderByDescending(x => x.Task_Update_ID).ToList();
                    objTask.Update_Task_Lock(Convert.ToInt32(Task_ID), drLastTaskHistory[0].Task_Update_ID, 0);
                }
            }
            Response.Redirect(GetPreviousPage());
        }

        protected void cmdCancelTaskConform_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
            N_Ter.Customizable.Master_Tables objMasters = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);

            string Task_IDs = Convert.ToString(ViewState["tids"]);

            DS_Tasks dsEachTask;
            int Task_Position = 0;

            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            Common_Task_Actions objComAct = new Common_Task_Actions();

            foreach (string Task_ID in Task_IDs.Split('|'))
            {
                if (Task_ID.Trim() != "")
                {
                    dsEachTask = objTask.Read(Convert.ToInt32(Task_ID), false, false, false);

                    bool isMultiPost = false;
                    if (ValidateTaskPost(objCtrlList.Current_Step_ID, Convert.ToInt32(ViewState["csid"]), dsEachTask.tbltasks[0].Current_Step_ID, dsEachTask.tbltasks[0].Task_Number, ref isMultiPost, objSes))
                    {
                        objComAct.PrepareControlsDataSet(dsEachTask, objSes, true, ref objCtrlList);

                        SaveStep(dsEachTask, objTask, Task_Position, objCtrlList, objSes.UserID, isMultiPost, objSes);
                        if (isMultiPost == false)
                        {
                            objTask.UpdateTaskHistory(Convert.ToInt32(Task_ID), -2, objSes.UserID, objSes.FullName, false, objCus, objMasters);
                        }
                        else
                        {
                            objTask.Update_Task_Lock(Convert.ToInt32(Task_ID), 0, 0);
                        }
                    }
                    Task_Position++;
                }
            }
            Response.Redirect(GetPreviousPage());
        }

        private void SaveStep(DS_Tasks ds, Tasks objTasks, int Task_Position, Task_Controls_Main objList, int User_ID, bool isMultiPost, SessionObject objSes)
        {
            Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
            N_Ter.Customizable.Master_Tables objMasters = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);

            List<DS_Tasks.tbltask_historyRow> drLastUpdate = ds.tbltask_history.OrderByDescending(x => x.Task_Update_ID).ToList();

            if (isMultiPost)
            {
                drLastUpdate = drLastUpdate.Where(l => l.Workflow_Step_ID == Convert.ToInt32(ViewState["csid"])).ToList();
            }

            int TaskUpdateID = drLastUpdate[0].Task_Update_ID;

            DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(ds.tbltasks[0].Walkflow_ID);
            DS_Master_Tables dsExMaster1 = null;
            DS_Master_Tables dsExMaster2 = null;
            if (dsWF.tblwalkflow[0].Exrta_Field_Type == 3 && dsWF.tblwalkflow[0].Extra_Field_Master_Table_ID > 0)
            {
                dsExMaster1 = objMasters.GetMasterTable(dsWF.tblwalkflow[0].Extra_Field_Master_Table_ID);
            }
            if (dsWF.tblwalkflow[0].Exrta_Field2_Type == 3 && dsWF.tblwalkflow[0].Extra_Field2_Master_Table_ID > 0)
            {
                dsExMaster2 = objMasters.GetMasterTable(dsWF.tblwalkflow[0].Extra_Field2_Master_Table_ID);
            }
            List<DS_Master_Tables.tblDataRow> drM;

            List<Task_Field_Update> lstTaskUpdates = new List<Task_Field_Update>();
            List<Task_Extra_Field_Updates> lstTaskExtraFieldUpdates = new List<Task_Extra_Field_Updates>();
            List<Task_Doc_Custom_Actions> lstTaskDocUploadActions = new List<Task_Doc_Custom_Actions>();

            int Item_ID = 0;
            string ItemValue = "";

            foreach (Task_Controls item in objList.Controls)
            {
                ItemValue = "";
                if (item.UI_Type == UI_Types.FileUploads)
                {
                    if (item.File_Path != "")
                    {
                        lstTaskDocUploadActions.Add(new Task_Doc_Custom_Actions
                        {
                            Task_ID = ds.tbltasks[0].Task_ID,
                            Workflow_ID = ds.tbltasks[0].Walkflow_ID,
                            Current_Step_ID = ds.tbltasks[0].Current_Step_ID,
                            Workflow_Step_Field_ID = item.Field_ID,
                            File_Path = item.File_Path,
                        });
                        ItemValue = item.Item_Value;
                        lstTaskUpdates.Add(new Task_Field_Update
                        {
                            Task_Update_ID = TaskUpdateID,
                            Workflow_Step_Field_ID = item.Field_ID,
                            Field_Value = ItemValue,
                            User_ID = User_ID
                        });
                    }
                    else
                    {
                        lstTaskUpdates.Add(new Task_Field_Update
                        {
                            Task_Update_ID = TaskUpdateID,
                            Workflow_Step_Field_ID = item.Field_ID,
                            Field_Value = ItemValue,
                            User_ID = User_ID
                        });
                    }
                }
                else if (item.UI_Type == UI_Types.DropDowns)
                {
                    lstTaskUpdates.Add(new Task_Field_Update
                    {
                        Task_Update_ID = TaskUpdateID,
                        Workflow_Step_Field_ID = item.Field_ID,
                        Field_Value = item.Item_Value,
                        User_ID = User_ID
                    });
                    ItemValue = item.Item_Value;
                }
                else if (item.UI_Type == UI_Types.Checkbox)
                {
                    lstTaskUpdates.Add(new Task_Field_Update
                    {
                        Task_Update_ID = TaskUpdateID,
                        Workflow_Step_Field_ID = item.Field_ID,
                        Field_Value = item.Item_Value,
                        User_ID = User_ID
                    });
                    ItemValue = item.Item_Value;
                }
                else
                {
                    string[] CopiedData = item.Item_Value.Split('|');
                    if (CopiedData.Length > 1)
                    {
                        lstTaskUpdates.Add(new Task_Field_Update
                        {
                            Task_Update_ID = TaskUpdateID,
                            Workflow_Step_Field_ID = item.Field_ID,
                            Field_Value = (CopiedData.Length > Task_Position ? CopiedData[Task_Position] : CopiedData[CopiedData.Length - 1]),
                            User_ID = User_ID
                        });
                        ItemValue = (CopiedData.Length > Task_Position ? CopiedData[Task_Position] : CopiedData[CopiedData.Length - 1]);
                    }
                    else
                    {
                        lstTaskUpdates.Add(new Task_Field_Update
                        {
                            Task_Update_ID = TaskUpdateID,
                            Workflow_Step_Field_ID = item.Field_ID,
                            Field_Value = item.Item_Value,
                            User_ID = User_ID
                        });
                        ItemValue = item.Item_Value;
                    }
                }

                if (item.Copy_To_EF1 == true)
                {
                    Item_ID = 0;
                    if (dsWF.tblwalkflow[0].Exrta_Field_Type == 3 && dsWF.tblwalkflow[0].Extra_Field_Master_Table_ID > 0)
                    {
                        drM = dsExMaster1.tblData.Where(x => x.Data.Trim() == ItemValue.Trim()).ToList();
                        if (drM.Count > 0)
                        {
                            Item_ID = drM[0].Data_ID;
                        }
                    }
                    lstTaskExtraFieldUpdates.Add(new Task_Extra_Field_Updates
                    {
                        Task_ID = ds.tbltasks[0].Task_ID,
                        Extra_Field_Value = ItemValue,
                        Extra_Field_ID = Item_ID,
                        Extra_Field_Order = 1
                    });
                }
                if (item.Copy_To_EF2 == true)
                {
                    Item_ID = 0;
                    if (dsWF.tblwalkflow[0].Exrta_Field2_Type == 3 && dsWF.tblwalkflow[0].Extra_Field2_Master_Table_ID > 0)
                    {
                        drM = dsExMaster2.tblData.Where(x => x.Data.Trim() == ItemValue.Trim()).ToList();
                        if (drM.Count > 0)
                        {
                            Item_ID = drM[0].Data_ID;
                        }
                    }
                    lstTaskExtraFieldUpdates.Add(new Task_Extra_Field_Updates
                    {
                        Task_ID = ds.tbltasks[0].Task_ID,
                        Extra_Field_Value = ItemValue,
                        Extra_Field_ID = Item_ID,
                        Extra_Field_Order = 2
                    });
                }
            }
            objTasks.AddStepTaskUpdate(lstTaskUpdates, lstTaskExtraFieldUpdates, lstTaskDocUploadActions, objCus);
        }

        private string GetPreviousPage()
        {
            Session["msg"] = "All Tasks Successfully Submitted";

            string ReturnURL = "task_list.aspx?";
            if (ViewState["prev"] != null)
            {
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                ReturnURL = objURL.Decrypt(Convert.ToString(ViewState["prev"]));
            }
            return ReturnURL;
        }
    }
}