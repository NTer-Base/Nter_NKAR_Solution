using N_Ter.Base;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class task : Page
    {
        private Task_Controls_Main objCtrlList = new Task_Controls_Main();
        private Task_Controls_Main objAddonCtrlList = new Task_Controls_Main();
        private Task_Controls_Main objDPCtrlList = new Task_Controls_Main();
        private System.Collections.ArrayList objHashTagsList = new System.Collections.ArrayList();

        private controls_customizable.cuz_task_post objCuz = null;

        private int MainControlsCount = 0;
        public string LoadingScripts = "";
        public string NewEL2Script = "";
        public string NewEL2Script2 = "";
        public string HelpScript = "";
        public string AddonHelpScript = "";
        public string DocHelpScript = "";
        public string Required_Fields_dp = "";
        public string Required_Fields = "";
        public string Required_Addon_Fields = "";
        public string Formula_Fields = "";
        public string ChartScript = "";
        public string ChartLoadScript = "";
        public string Old_Field_Validation = "";
        public string Old_Addon_Field_Validation = "";
        public string Custom_Scripts = "";
        public string Next_Step_Script = "";
        public string Sub_Task_From_Doc_Script = "";
        public string Sub_Task_Script = "";
        public string Task_Deadlines_Script = "";
        public string Dock_JS_Init = "";
        public string Dock_CSS_Init = "";
        public string Dock_CSS_1 = "";
        public string Dock_CSS_2 = "";
        public string Task_Script = "";
        public string Chat_Arrange_Script = "";
        public string CurrentPageURL = "";

        public string StepListPanelResizeScript = "";
        public string ChatPanelResizeScript = "";
        public string EL2PanelResizeScript = "";
        public string QueuePanelResizeScript = "";
        public string HelpPanelResizeScript = "";
        public string AddonPanelResizeScript = "";
        public string DocHelpPanelResizeScript = "";
        public string AddonHelpPanelResizeScript = "";
        public string UpdateEL2ResizeScript = "";
        public string TaskDeadlineResizeScript = "";
        public string DocReplaceResizeScript = "";

        public int RefFreq = 0;
        public int ListSort = 0;
        public bool ListSortDir = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            RefFreq = objSes.RefFreq;
            ListSort = objSes.ListSort;
            ListSortDir = objSes.ListSortDir;
            CurrentPageURL = new Common_Actions().GetReletiveURL(Request);

            ((BoundField)gvDocumentsFind.Columns[3]).DataFormatString = "{0:" + Constants.DateFormat + " HH:mm}";

            if (IsPostBack == false)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Request.QueryString["bck"])) == false)
                {
                    ViewState["prev"] = Convert.ToString(Request.QueryString["bck"]);
                }

                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                if (string.IsNullOrEmpty(Convert.ToString(Request.QueryString["tid"])) == false)
                {
                    Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);

                    int Task_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["tid"])));
                    Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

                    DS_Users dsAltUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).ReadAssignedUsers(objSes.UserID);
                    if (objTask.Can_Access_Task(Task_ID, objSes.UserID, dsAltUsers.tblusers, objCus) == true)
                    {
                        ltrEL1.Text = objSes.EL1;
                        ltrEL2.Text = objSes.EL2;
                        ltrEL2_2.Text = objSes.EL2;
                        ltrEL2_3.Text = objSes.EL2;
                        ltrEL2_4.Text = objSes.EL2P;
                        ltrEL2_6.Text = objSes.EL2;
                        ltrEL2_7.Text = objSes.EL2P;
                        ltrEL2_8.Text = objSes.EL2;
                        ltrEL2_9.Text = objSes.EL2;
                        ltrEL2_10.Text = objSes.EL2;
                        ltrEL2_11.Text = objSes.EL2;
                        ltrEL2_12.Text = objSes.EL2;
                        ltrEL2_14.Text = objSes.EL2;
                        ltrEL2_15.Text = objSes.EL2;
                        cmdAddEL2.Text = "Add " + objSes.EL2;
                        gvDocumentsFind.Columns[4].HeaderText = objSes.EL2 + " Name";
                        ViewState["tid"] = Task_ID;
                        hndTaskID.Value = Convert.ToString(Task_ID);
                    }
                    else
                    {
                        Response.Redirect("task_info.aspx?tid=" + objURL.Encrypt(Convert.ToString(Task_ID)));
                    }
                }
                else
                {
                    Response.Redirect("error.aspx?");
                }

            }

            cmdSubmit.Attributes.Add("onClick", "document.getElementById('" + hndSubmitType.ClientID + "').value = '1'; document.getElementById('" + hndAttachDoc_ID.ClientID + "').value = '0'; return ValidateAgainstField();");

            LoadTask(objSes);

            cmdSubmitSpecial.Attributes.Add("onClick", "return ValidateAgainstFieldSpecial();");
            cmdExtraSave.Attributes.Add("onClick", "return ValidateExtraFieldUpdate('" + txtExtraFieldValue.ClientID + "');");
            cmdExtra2Save.Attributes.Add("onClick", "return ValidateExtraField2Update('" + txtExtraFieldValue.ClientID + "');");
            cmdUpdateDueDate.Attributes.Add("onClick", "return ValidateDueDate('" + txtDueDate.ClientID + "');");
            cmdSaveComment.Attributes.Add("onClick", "return ValidateComment('" + txtCommentMain.ClientID + "');");
            cmdAddDocComment.Attributes.Add("onClick", "return ValidateComment('" + txtDocComment.ClientID + "');");
            cmdAddSub.Attributes.Add("onClick", "javascript:document.getElementById('" + hndSubmitType.ClientID + "').value = '0'; document.getElementById('" + hndAttachDoc_ID.ClientID + "').value = '0'; document.getElementById('" + txtNoSubOfTasks.ClientID + "').value = '1'; getSubWFDetails();");
            cmdCloseMain.Attributes.Add("onClick", "return ValidateAgainstField();");
            hndMessageMyID.Value = Convert.ToString(objSes.UserID);
            cmdSendMessage.Attributes.Add("onClick", "javascript:return ValidateTaskMessage('" + hndMessageThreadID.ClientID + "', '" + cboMessageUsers.ClientID + "', '" + txtTitle.ClientID + "', '" + txtMessage.ClientID + "');");
            cmdUploadDoc.OnClientClick = "return false;";
            cboTags.Attributes.Add("onChange", "LoadTagdComments();");
            cboDocType.Attributes.Add("onChange", "LoadDocuments();");
            cboDocCategories.Attributes.Add("onChange", "LoadDocuments();");
            cmdDP_Upload.Attributes.Add("onclick", "return ValidatRequiredFieldsDP();");
            cmdSaveAddon.Attributes.Add("onClick", "return ValidateAgainstAddonField();");
            cboSubWorkflow.Attributes.Add("onChange", "getSubWFDetails();");
            cboSubWFForDocs.Attributes.Add("onChange", "getSubTaskFromDoc();");
            cmdRecalcStepDeadlines.Attributes.Add("onClick", "return RecalcStepDeadlines();");
            cmdResetStepDeadlines.Attributes.Add("onClick", "return ResetStepDeadlines();");
            cmdSaveStepDeadlines.Attributes.Add("onClick", "return ValidateStepDeadlines();");
            cmdReplaceFile.Attributes.Add("onClick", "return ValidateReplaceFile('" + hndReplaceFile.ClientID + "', '" + cboFileSource.ClientID + "', '" + cboFileResult.ClientID + "', '" + fulNewFile.ClientID + "');");

            divSourceDoc.ClientIDMode = ClientIDMode.Static;
            divSourceDoc_toggler.ClientIDMode = ClientIDMode.Static;
            divOutDoc.ClientIDMode = ClientIDMode.Static;
            divOutDoc_toggler.ClientIDMode = ClientIDMode.Static;
            divHistory.ClientIDMode = ClientIDMode.Static;
            divHistory_toggler.ClientIDMode = ClientIDMode.Static;
        }

        private void LoadProjDocuments(int Document_Project_ID, int Entity_L2_ID, SessionObject objSes)
        {
            if (IsPostBack == false)
            {
                Document_Projects objDocProj = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
                DS_Doc_Project dsDocProj = objDocProj.Read(Document_Project_ID);
                int intTagCount = 0;
                foreach (DS_Doc_Project.tbldocument_project_indexesRow row in dsDocProj.tbldocument_project_indexes)
                {
                    BoundField col = new BoundField();
                    col.HeaderText = row.Tag_Name;
                    col.DataField = "doctag_" + row.Document_Project_Index_ID;
                    gvDocumentsFind.Columns.Add(col);
                    intTagCount++;
                    if (intTagCount > 2)
                    {
                        break;
                    }
                }
            }

            Documents objDoc = ObjectCreator.GetDocuments(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            DS_Documents ds = objDoc.ReadAllForEL2(Document_Project_ID, Entity_L2_ID, objSes.AccLevel);
            gvDocumentsFind.DataSource = ds.tbldocuments;
            gvDocumentsFind.DataBind();
            if (ds.tbldocuments.Rows.Count > 0)
            {
                gvDocumentsFind.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void LoadDPTags(int Document_Project_ID)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            Common_Task_Actions objTskAct = new Common_Task_Actions();

            divTags.Controls.Clear();

            Document_Projects objDP = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
            DS_Doc_Project ds = objDP.ReadWithLabels(Document_Project_ID);

            ltrDocProjectName.Text = ds.tbldocument_project[0].Doc_Project_Name;

            string strRequiredFieldValidationDP = "";
            Required_Fields_dp = "function ValidatRequiredFieldsDP() {" + "\r\n" +
                                    "remove_field_erros();" + "\r\n" +
                                    "var ret = ValidateFileUpload('" + fulDocumentDP.ClientID + "');" + "\r\n";

            DS_Documents dsTemp = new DS_Documents();

            Script_Generator objScripts = new Script_Generator();
            N_Ter.Customizable.Master_Tables objMasters = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);
            int ControlIndex = 0;
            List<string> Help_Texts = new List<string>();

            divTags.Attributes.Add("class", "row");

            System.Web.UI.HtmlControls.HtmlGenericControl divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
            int rowWidth = 0;

            foreach (DS_Doc_Project.tbldocument_project_indexesRow row in ds.tbldocument_project_indexes)
            {
                divTags.Controls.Add(objTskAct.GetDocObject(objScripts, IsPostBack, objMasters, dsTemp.tbldocument_tags, objSes.Currency_Sbl, row, ref objDPCtrlList, ref strRequiredFieldValidationDP, ref rowWidth, ControlIndex, "GetDocHelp", true, false));
                if (row.Help_Text.Trim() != "")
                {
                    Help_Texts.Add(row.Tag_Name + "|" + row.Help_Text);
                    ControlIndex++;
                }
                if (rowWidth == 12)
                {
                    divTags.Controls.Add(divMainRowControl);
                    divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                    divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                    rowWidth = 0;
                }
            }
            if (rowWidth > 0)
            {
                divTags.Controls.Add(divMainRowControl);
            }

            Required_Fields_dp = Required_Fields_dp + strRequiredFieldValidationDP + "return ret;" + "\r\n" +
                                                                "}" + "\r\n";
            if (Help_Texts.Count > 0)
            {
                hndDocHelp_ModalPopupExtender.Enabled = true;
                pnlDocHelp.Visible = true;
                objScripts.LoadDocHelpScripts(ref DocHelpScript, ref DocHelpPanelResizeScript, Help_Texts);
            }
            else
            {
                DocHelpScript = "";
                DocHelpPanelResizeScript = "";
                hndDocHelp_ModalPopupExtender.Enabled = false;
                pnlDocHelp.Visible = false;
            }
        }

        private void LoadTask(SessionObject objSes)
        {
            Script_Generator objScripts = new Script_Generator();
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            DS_Tasks dsCurrentTask = objTask.Read(Task_ID, false, false, false);

            if (dsCurrentTask.tbltasks.Rows.Count > 0 && dsCurrentTask.tbltasks[0].Current_Step_ID != -1 && dsCurrentTask.tbltasks[0].Current_Step_ID != -2)
            {
                if (IsPostBack == false)
                {
                    ViewState["wid"] = dsCurrentTask.tbltasks[0].Walkflow_ID;
                    ViewState["csid"] = dsCurrentTask.tbltasks[0].Current_Step_ID;
                }
                else
                {
                    dsCurrentTask.tbltasks[0].Walkflow_ID = Convert.ToInt32(ViewState["wid"]);
                    dsCurrentTask.tbltasks[0].Current_Step_ID = Convert.ToInt32(ViewState["csid"]);
                }

                Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                DS_Workflow dsWFStep = objWF.ReadStep(dsCurrentTask.tbltasks[0].Current_Step_ID);

                if (IsPostBack == false)
                {
                    ViewState["stay"] = dsWFStep.tblworkflow_steps[0].Remain_In_Task;
                }

                if (dsWFStep.tblworkflow_steps[0].IsHelp_TextNull() || dsWFStep.tblworkflow_steps[0].Help_Text.Trim() == "")
                {
                    divHelpText.Visible = false;
                }
                else
                {
                    divHelpText.Visible = true;
                    ltrHelpText.Text = dsWFStep.tblworkflow_steps[0].Help_Text.Replace("\r\n", "<br />");
                }

                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();

                ltrCustomScreens.Text = "";
                if (dsWFStep.tblworkflow_steps[0].Show_Custom_Screens)
                {
                    if (dsWFStep.tblworkflow_step_links.Rows.Count > 0)
                    {
                        DS_Extra_Sections dsExSec = new N_Ter.Customizable.Custom_Lists().LoadExtraSections();
                        List<DS_Extra_Sections.tblSectionsRow> dr;
                        foreach (DS_Workflow.tblworkflow_step_linksRow row in dsWFStep.tblworkflow_step_links)
                        {
                            dr = dsExSec.tblSections.Where(x => x.Section_ID == row.Screen_ID).ToList();
                            if (dr.Count > 0)
                            {
                                ltrCustomScreens.Text = ltrCustomScreens.Text + "<li><a href='" + dr[0].Page_Name + "?tid=" + objURL.Encrypt(Convert.ToString(Task_ID)) + "'><span class='btn-label menu_icon fa " + dr[0].Section_Icon + "'></span>" + dr[0].Section_Name_Menu + "</a></li>";
                                MainControlsCount++;
                            }
                        }
                    }
                }

                int EL2CreatedHistoryCount = dsCurrentTask.tbltask_history.Where(x => x.Workflow_Step_ID == dsCurrentTask.tbltasks[0].Current_Step_ID && x.Entity_L2_Created == true).Count();
                bool AddNewEL2 = false;
                divAddEL2Alert2.Visible = false;
                if (dsWFStep.tblworkflow_steps[0].Create_New_Entity_L2 == false || EL2CreatedHistoryCount > 0)
                {
                    pnlEL2.Visible = false;
                    divAddEL2Alert.Visible = false;
                    cmdAddEL2_ModalPopupExtender.Enabled = false;
                    NewEL2Script = "";
                    NewEL2Script2 = "";
                    EL2PanelResizeScript = "";
                    hndRequireEL2.Value = "0";
                    ViewState["el2"] = "0";
                    if (dsWFStep.tblworkflow_steps[0].Deactivate_Entity_L2 == true)
                    {
                        divAddEL2Alert2.Visible = true;
                    }
                }
                else
                {
                    AddNewEL2 = true;
                    pnlEL2.Visible = true;
                    divAddEL2Alert.Visible = true;
                    cmdAddEL2_ModalPopupExtender.Enabled = true;
                    cboEntity_Level_2.Attributes.Add("onChange", "changeParents();");
                    txtDisplayName.Attributes.Add("onChange", "changeDN();");
                    hndEL2_ID.Value = "0";
                    if (IsPostBack == false)
                    {
                        LoadEntity_Level_1s(objSes);
                        LoadEntity_Level_2s(objSes);
                        LoadTemplates(objSes);
                        LoadUsers(objSes);
                        LoadWorkflows(objWF);
                    }
                    objScripts.LoadNewEL2Scripts(ref NewEL2Script, ref NewEL2Script2, ref EL2PanelResizeScript, cboEntity_Level_2.ClientID, txtDisplayName.ClientID,
                        hndRequireEL2.ClientID, hndWorkflows.ClientID, divUserGroups.ClientID, objSes);
                    hndRequireEL2.Value = "1";
                    ViewState["el2"] = "1";
                    cmdSaveEL2.Attributes.Add("onClick", "return ValidateEL2('" + txtDisplayName.ClientID + "', '" + txtLegalName.ClientID + "', '" + txtE_Mail.ClientID + "', '" + txtFolder.ClientID + "', '" + txtEntityCode.ClientID + "', '" + cboEntity_Level_1.ClientID + "', '" + hndEL2_ID.ClientID + "', '" + cboEntity_Level_2.ClientID + "', false);");
                }

                if (IsPostBack == false)
                {
                    DS_Tasks.tbltask_historyRow[] drLastTaskHistory = dsCurrentTask.tbltask_history.OrderByDescending(x => x.Task_Update_ID).ToArray();
                    objTask.Update_Task_Lock(Task_ID, drLastTaskHistory[0].Task_Update_ID, objSes.UserID);
                    ViewState["tuid"] = drLastTaskHistory[0].Task_Update_ID;
                }

                DS_Workflow dsWF = objWF.Read(dsCurrentTask.tbltasks[0].Walkflow_ID, false, false);

                if (dsWF.tblwalkflow[0].Document_Project_ID > 0)
                {
                    pnlAddDocLink.Visible = true;
                    cmdDocLink_ModalPopupExtender.Enabled = true;
                    cmdDocLink.Visible = true;
                    cmdDocLinkR_ModalPopupExtender.Enabled = true;
                    cmdDocLinkR.Visible = true;

                    pnlDP_Upload.Visible = true;
                    cmdDocLinkUP_ModalPopupExtender.Enabled = true;
                    cmdDocLinkUP.Visible = true;
                    cmdDocLinkUPR_ModalPopupExtender.Enabled = true;
                    cmdDocLinkUPR.Visible = true;

                    cmdDocFilter.Visible = true;
                    lstSeperator1.Visible = true;
                    cmdDocFilterClick.Attributes.Clear();
                    cmdDocFilterClick.Attributes.Add("href", "doc_filter.aspx?tid=" + objURL.Encrypt(Convert.ToString(Task_ID)) + "&s=1");

                    cmdDocFilterR.Visible = true;
                    lstSeperator1R.Visible = true;
                    cmdDocFilterRClick.Attributes.Clear();
                    cmdDocFilterRClick.Attributes.Add("href", "doc_filter.aspx?tid=" + objURL.Encrypt(Convert.ToString(Task_ID)) + "&s=2");

                    pnlAnchor.Visible = true;
                    lstAnchor.Visible = true;
                    hndAnchor_ModalPopupExtender.Enabled = true;
                    MainControlsCount++;
                    ltrDocAnchor.Text = Request.Url.GetLeftPart(UriPartial.Authority) + (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath) + "/doc_upload_external.aspx?t=" + objURL.Encrypt(Convert.ToString(Task_ID));
                    LoadProjDocuments(dsWF.tblwalkflow[0].Document_Project_ID, dsCurrentTask.tbltasks[0].Entity_L2_ID, objSes);
                    LoadDPTags(dsWF.tblwalkflow[0].Document_Project_ID);
                    hndDoc_Proj_ID.Value = Convert.ToString(dsWF.tblwalkflow[0].Document_Project_ID);
                }
                else
                {
                    pnlAddDocLink.Visible = false;
                    cmdDocLink.Visible = false;
                    cmdDocLink_ModalPopupExtender.Enabled = false;
                    cmdDocLinkR.Visible = false;
                    cmdDocLinkR_ModalPopupExtender.Enabled = false;

                    pnlDP_Upload.Visible = false;
                    cmdDocLinkUP_ModalPopupExtender.Enabled = false;
                    cmdDocLinkUP.Visible = false;
                    cmdDocLinkUPR_ModalPopupExtender.Enabled = false;
                    cmdDocLinkUPR.Visible = false;

                    cmdDocFilter.Visible = false;
                    lstSeperator1.Visible = false;
                    cmdDocFilterR.Visible = false;
                    lstSeperator1R.Visible = false;

                    pnlAnchor.Visible = false;
                    lstAnchor.Visible = false;
                    hndAnchor_ModalPopupExtender.Enabled = false;
                }

                if (dsWF.tblworkflow_steps.Where(x => x.User_Group_Involved == objSes.Guest_Group).Count() > 0)
                {
                    ltrGuestPost.Text = Request.Url.GetLeftPart(UriPartial.Authority) + (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath) + "/guest_task_post.aspx?tid=" + objURL.Encrypt(Convert.ToString(dsCurrentTask.tbltasks[0].Task_ID)) + "&stp=" + objURL.Encrypt(Convert.ToString(dsCurrentTask.tbltasks[0].Current_Step_ID));
                    hndGuestPost_ModalPopupExtender.Enabled = true;
                    lstGuestPost.Visible = true;
                    pnlGuestPost.Visible = true;
                    MainControlsCount++;
                }
                else
                {
                    hndGuestPost_ModalPopupExtender.Enabled = false;
                    lstGuestPost.Visible = false;
                    pnlGuestPost.Visible = false;
                }

                DS_Tasks dsSubTasks = objTask.GetActiveSubTasks(Task_ID);
                if (dsWFStep.tblworkflow_steps[0].Allow_Task_Cancellation == true)
                {
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
                    if (dsWFStep.tblworkflow_steps[0].Show_Reject_Button == false || dsCurrentTask.tbltask_history.Rows.Count <= 1)
                    {
                        cmdReject.Visible = false;
                    }
                    else
                    {
                        int Previous_Step = 0;
                        bool isCurrentStep = true;
                        foreach (DS_Tasks.tbltask_historyRow drHist in dsCurrentTask.tbltask_history.OrderByDescending(x => x.Task_Update_ID))
                        {
                            if (isCurrentStep)
                            {
                                isCurrentStep = false;
                            }
                            else
                            {
                                if (drHist.isRejected == false)
                                {
                                    Previous_Step = drHist.Workflow_Step_ID;
                                    break;
                                }
                            }
                        }

                        if (Previous_Step > 0)
                        {
                            List<DS_Workflow.tblworkflow_stepsRow> drPrevStep = dsWF.tblworkflow_steps.Where(x => x.Workflow_Step_ID == Previous_Step).ToList();
                            if (drPrevStep.Count > 0)
                            {
                                cmdReject.Attributes["data-original-title"] = "Return to : " + drPrevStep[0].Step_Status;
                            }
                            else
                            {
                                cmdReject.Attributes["data-original-title"] = "Error in Detecting Previuos Step";
                            }
                        }
                        else
                        {
                            cmdReject.Attributes["data-original-title"] = "Error in Detecting Previuos Step";
                        }
                    }

                    DS_Workflow.tblworkflow_stepsRow drStep = dsWF.tblworkflow_steps.Newtblworkflow_stepsRow();
                    drStep.Workflow_Step_ID = -1;
                    drStep.isApproverStep = false;
                    drStep.Step_Status = "Completed";
                    dsWF.tblworkflow_steps.Rows.Add(drStep);

                    DS_Workflow.tblworkflow_stepsRow drStep2 = dsWF.tblworkflow_steps.Newtblworkflow_stepsRow();
                    drStep2.Workflow_Step_ID = -2;
                    drStep2.isApproverStep = false;
                    drStep2.Step_Status = "Cancelled";
                    dsWF.tblworkflow_steps.Rows.Add(drStep2);

                    bool isApproverStep = false;

                    ViewState["da"] = dsWFStep.tblworkflow_steps[0].Allow_Delete_Own_Addons;
                    ViewState["dc"] = dsWFStep.tblworkflow_steps[0].Allow_Delete_Own_Comments;
                    ViewState["dd"] = dsWFStep.tblworkflow_steps[0].Allow_Delete_Own_Documents;

                    if (dsWFStep.tblworkflow_steps[0].isApproverStep)
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
                            if (rowWF.Workflow_Step_ID == dsCurrentTask.tbltasks[0].Current_Step_ID)
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

                    txtDueDate.Text = dsCurrentTask.tbltasks[0].IsETB_ValueNull() ? "" : string.Format("{0:" + Constants.DateFormat + "}", dsCurrentTask.tbltasks[0].ETB_Value);
                    cboDueTime.Text = dsCurrentTask.tbltasks[0].IsETB_ValueNull() ? "" : string.Format("{0:HH:mm}", dsCurrentTask.tbltasks[0].ETB_Value);
                    LoadFileTypes(dsWF.tblwalkflow[0]);
                    LoadCommentTypes(dsWF.tblwalkflow[0]);
                }

                List<DS_Workflow.tblsub_workflowsRow> drAvailableSubs = dsWFStep.tblsub_workflows.Where(x => x.isStart && !x.isAutomatic).ToList();
                if (drAvailableSubs.Count > 0)
                {
                    ArrangeSubWorkflowCreation(drAvailableSubs, objScripts, dsWFStep, dsWF, objWF, objSes);
                }
                else
                {
                    ViewState.Remove("subWFID");

                    cmdCloseMain.Visible = false;
                    cmdAddSub.Visible = false;
                    cmdAddSub_ModalPopupExtender.Enabled = false;
                    cmdAddSub_Close_ModalPopupExtender.Enabled = false;
                    hndSubTask_ModalPopupExtender.Enabled = false;
                    pnlSubTaskStart.Visible = false;

                    hndIsStartSubFromAtt.Value = "0";
                    hndSubTaskFromDoc.Visible = false;
                    hndSubTaskFromDoc_ModalPopupExtender.Enabled = false;
                    pnlSubTaskFromDoc.Visible = false;
                }

                if (dsWFStep.tblworkflow_steps[0].Addon_ID != 0)
                {
                    cmdAddAddon.Visible = true;
                    cmdAddonDivider.Visible = true;
                    pnlAddon.Visible = true;
                    cmdAddAddon_ModalPopupExtender.Enabled = true;
                    ltrAddonName.Text = dsWFStep.tblworkflow_addons[0].Addon_Name;
                    ltrAddonName2.Text = dsWFStep.tblworkflow_addons[0].Addon_Name;
                    switch (dsWFStep.tblworkflow_addons[0].Screen_Size)
                    {
                        case 1:
                            AddonPanelResizeScript = "AdjustPopupSize(80, 1200, 'at_model_addon');";
                            break;
                        case 2:
                            AddonPanelResizeScript = "AdjustPopupSize(80, 800, 'at_model_addon');";
                            break;
                        case 3:
                            AddonPanelResizeScript = "AdjustPopupSize(80, 500, 'at_model_addon');";
                            break;
                    }
                    LoadAddonFields(objScripts, dsCurrentTask, dsWFStep, IsPostBack, objSes);
                }
                else
                {
                    cmdAddAddon.Visible = false;
                    cmdAddonDivider.Visible = false;
                    pnlAddon.Visible = false;
                    cmdAddAddon_ModalPopupExtender.Enabled = false;
                }

                if (dsWFStep.tblworkflow_steps[0].Can_Edit_EL2 == true)
                {
                    pnlUpdateEL2.Visible = true;
                    lstEditEL2.Visible = true;
                    cmdUpdateEL2_ModalPopupExtender.Enabled = true;
                    UpdateEL2ResizeScript = "AdjustPopupSize(80, 600, 'at_model_uel2');";
                    if (IsPostBack == false)
                    {
                        LoadUpdate_Entity_Level_2(objSes);
                        cboUpdateEL2.SelectedValue = Convert.ToString(dsCurrentTask.tbltasks[0].Entity_L2_ID);
                    }
                    MainControlsCount++;
                }
                else
                {
                    pnlUpdateEL2.Visible = false;
                    lstEditEL2.Visible = false;
                    cmdUpdateEL2_ModalPopupExtender.Enabled = false;
                    UpdateEL2ResizeScript = "";
                }

                if (dsWFStep.tblworkflow_steps[0].Allow_Change_Task_Queue == true)
                {
                    pnlUpdateQueue.Visible = true;
                    lstUpdateQueue.Visible = true;
                    hndUpdateQueue_ModalPopupExtender.Enabled = true;
                    QueuePanelResizeScript = "AdjustPopupSize(80, 600, 'at_model_queue');";
                    if (IsPostBack == false)
                    {
                        LoadUpdateQueue(objSes);
                        cboQueue.SelectedValue = Convert.ToString(dsCurrentTask.tbltasks[0].Queue_ID);
                    }
                    MainControlsCount++;
                }
                else
                {
                    pnlUpdateQueue.Visible = false;
                    lstUpdateQueue.Visible = false;
                    hndUpdateQueue_ModalPopupExtender.Enabled = false;
                    QueuePanelResizeScript = "";
                }

                lblTaskNo.Text = dsCurrentTask.tbltasks[0].Task_Number;
                if (dsCurrentTask.tbltasks[0].Queue_ID == 0)
                {
                    lblQueue.Text = "General Queue";
                }
                else
                {
                    DS_Task_Queues dsQ = ObjectCreator.GetTask_Queues(objSes.Connection, objSes.DB_Type).Read(dsCurrentTask.tbltasks[0].Queue_ID);
                    if (dsQ.tbltask_queues.Rows.Count > 0)
                    {
                        lblQueue.Text = dsQ.tbltask_queues[0].Queue_Name;
                    }
                    else
                    {
                        lblQueue.Text = "";
                    }
                }

                lblTaskNumber.Text = " : " + dsCurrentTask.tbltasks[0].Task_Number + (dsCurrentTask.tbltasks[0].isFlagged ? "&nbsp;&nbsp;<i class='fa fa-flag text-danger'></i>" : "");
                lblWorkflow.Text = dsCurrentTask.tbltasks[0].Workflow_Name;
                if (dsCurrentTask.tbltasks[0].Creator_On_Behalf_ID == 0 || dsCurrentTask.tbltasks[0].Creator_ID == dsCurrentTask.tbltasks[0].Creator_On_Behalf_ID)
                {
                    lblTaskCreator.Text = dsCurrentTask.tbltasks[0].Created_By;
                }
                else
                {
                    DS_Users dsOnBehalf = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).Read(dsCurrentTask.tbltasks[0].Creator_On_Behalf_ID);
                    lblTaskCreator.Text = dsOnBehalf.tblusers[0].First_Name + " " + dsOnBehalf.tblusers[0].Last_Name + " (on behalf of " + dsCurrentTask.tbltasks[0].Created_By + ")";
                }
                lblEL2Name.Text = dsCurrentTask.tbltasks[0].Display_Name;
                lblCurrentStatus.Text = dsCurrentTask.tbltasks[0].Step_Status;
                lblTaskDate.Text = string.Format("{0:" + Constants.DateFormat + " HH:mm}", dsCurrentTask.tbltasks[0].Task_Date);
                lblDueDate.Text = dsCurrentTask.tbltasks[0].IsETB_ValueNull() ? "" : string.Format("{0:" + Constants.DateFormat + " HH:mm}", dsCurrentTask.tbltasks[0].ETB_Value);

                FillTaskData(Task_ID, ref dsCurrentTask, dsWF, objTask);

                DS_Tasks dsAllSubTasks = objTask.GetSubTasks(Task_ID);

                bool RefreshTask = false;
                LoadTaskDetails(objScripts, dsCurrentTask, AddNewEL2, dsWFStep, objSes, ref RefreshTask);
                if (RefreshTask)
                {
                    FillTaskData(Task_ID, ref dsCurrentTask, dsWF, objTask);
                }

                if (dsWFStep.tblworkflow_steps[0].Show_Documents)
                {
                    LoadAttachments(dsWF.tblwalkflow[0].Document_Project_ID, dsCurrentTask.tbltask_docs, dsCurrentTask.tbltask_attachment, dsCurrentTask.tbltask_comments, objSes);
                }
                else
                {
                    divSourceDoc.Visible = false;
                    cmdViewDocs_ModalPopupExtender.Enabled = false;
                    pnlDocView.Visible = false;
                    cmdDocLink_ModalPopupExtender.Enabled = false;
                    pnlAddDocLink.Visible = false;
                    cmdDocLinkUP_ModalPopupExtender.Enabled = false;
                    pnlDP_Upload.Visible = false;
                    cmdAddDocument_ModalPopupExtender.Enabled = false;
                    pnlUpload.Visible = false;

                    divOutDoc.Visible = false;
                    cmdViewDocsR_ModalPopupExtender.Enabled = false;
                    cmdDocLinkR_ModalPopupExtender.Enabled = false;
                    cmdDocLinkUPR_ModalPopupExtender.Enabled = false;
                    cmdAddDocumentR_ModalPopupExtender.Enabled = false;
                }

                if (dsWFStep.tblworkflow_steps[0].Show_History)
                {
                    LoadHistory(objScripts, dsCurrentTask, dsAllSubTasks, objSes);
                }
                else
                {
                    divHistory.Visible = false;
                    cmdAddComment_ModalPopupExtender.Enabled = false;
                    pnlComment.Visible = false;
                    cmdViewComment_ModalPopupExtender.Enabled = false;
                    pnlViewComment.Visible = false;

                    cmdViewProgress.Visible = false;
                    cmdViewProgress_ModalPopupExtender.Enabled = false;
                    pnlTaskProgress.Visible = false;

                    divTaskDuration.Visible = false;
                }

                divSourceDoc_toggler.Visible = false;
                divOutDoc_toggler.Visible = false;
                divHistory_toggler.Visible = false;

                if (dsWFStep.tblworkflow_steps[0].Show_Documents == false && dsWFStep.tblworkflow_steps[0].Show_History == false)
                {
                    divPost.Attributes["class"] = "col-sm-12";
                }
                else if (objSes.Task_Screen_Type == 2)
                {
                    Dock_CSS_Init = "<link href=\"assets/stylesheets/task_screen_docks.min.css\" rel=\"stylesheet\" />";
                    Dock_JS_Init = "<script src=\"assets/javascripts/task_screen_docks.min.js\"></script>";
                    divPost.Attributes["class"] = "col-sm-12";
                    divSupportPane.Attributes["class"] = "";

                    Dock_CSS_1 = " scroll_sp";
                    Dock_CSS_2 = "documents_scroll styled-bar scroll_sp";

                    divSourceDoc_toggler.Visible = true;
                    divOutDoc_toggler.Visible = true;
                    divHistory_toggler.Visible = true;
                }

                Common_Task_Actions objCommAct = new Common_Task_Actions();
                if (dsCurrentTask.tbltask_timeline.Count == 0)
                {
                    objCommAct.GenerateTaskTimeLine(ref dsCurrentTask, objWF);
                    objTask.UpdateTaskTimeline(Task_ID, dsCurrentTask.tbltask_timeline);
                }

                if (dsWFStep.tblworkflow_steps[0].Allow_Change_Step_Deadlines)
                {
                    Task_Deadlines_Script = objScripts.StepDeadlinesScript(Task_ID);
                    TaskDeadlineResizeScript = "AdjustPopupSize(80, 800, 'at_model_sdl');";
                    lstStepDeadlines.Visible = true;
                    pnlStepDeadline.Visible = true;
                    hndStepDeadline.Visible = true;
                    hndStepDeadline_ModalPopupExtender.Enabled = true;
                    MainControlsCount++;
                }
                else
                {
                    Task_Deadlines_Script = "";
                    TaskDeadlineResizeScript = "";
                    lstStepDeadlines.Visible = false;
                    pnlStepDeadline.Visible = false;
                    hndStepDeadline.Visible = false;
                    hndStepDeadline_ModalPopupExtender.Enabled = false;
                }

                if (dsWFStep.tblworkflow_steps[0].Show_Timeline)
                {
                    LoadTaskTimelineChart(dsCurrentTask, objSes);
                }
                else
                {
                    divTaskTimeline.Visible = false;
                    hndStepsList_ModalPopupExtender.Enabled = false;
                    pnlStepsList.Visible = false;
                    hndStepsList.Visible = false;
                }
                
                LoadExtraFeilds(dsWFStep, objSes);
                LoadSubTasks(dsSubTasks, Task_ID, dsCurrentTask.tbltasks[0].Task_Number + " - " + dsCurrentTask.tbltasks[0].Workflow_Name + " - " + dsCurrentTask.tbltasks[0].Display_Name);

                if (dsWFStep.tblworkflow_steps[0].Show_Chats)
                {
                    LoadTaskMessages(dsCurrentTask.tbltasks[0].Walkflow_ID, dsCurrentTask.tbltasks[0].Entity_L2_ID, Task_ID, objSes);
                }
                else
                {
                    divTaskChats.Visible = false;
                    lstNewChat_ModalPopupExtender.Enabled = false;
                    pnlChat.Visible = false;
                    hndMessageThreadID.Visible = false;
                    hndMessageMyID.Visible = false;
                    hndMessageMemberID.Visible = false;
                }
                LoadTaskRelatedData(dsCurrentTask, objSes);
                LoadCustomScripts(dsCurrentTask, objSes);
                LoadEL2_Hiararchy(dsCurrentTask.tbltasks[0].Entity_L2_ID, objSes);

                if (dsWFStep.tblworkflow_steps[0].Task_Details_Header.Trim() != "")
                {
                    ltrTaskDetailsPanelName.Text = dsWFStep.tblworkflow_steps[0].Task_Details_Header.Trim();
                }
                else
                {
                    ltrTaskDetailsPanelName.Text = "Task Details";
                }

                if (dsWFStep.tblworkflow_steps[0].Task_Post_Header.Trim() != "")
                {
                    ltrTaskPostPanelName.Text = dsWFStep.tblworkflow_steps[0].Task_Post_Header.Trim();
                }
                else
                {
                    ltrTaskPostPanelName.Text = "Task Posting";
                }

                string FlowchartSteps = "";
                string Flowchart = "";
                objCommAct.LoadFlowchart(dsCurrentTask.tbltasks[0].Walkflow_ID, dsCurrentTask.tbltasks[0].Current_Step_ID, objWF, objSes.EL2, ref FlowchartSteps, ref Flowchart);
                ltrFlowChartSteps.Text = FlowchartSteps;
                ltrFlowChart.Text = Flowchart;

                if (MainControlsCount > 0)
                {
                    divMainControls.Visible = true;
                }
                else
                {
                    divMainControls.Visible = false;
                }
            }
            else
            {
                Response.Redirect("error.aspx?");
            }
        }

        private void ArrangeSubWorkflowCreation(List<DS_Workflow.tblsub_workflowsRow> drAvailableSubs, Script_Generator objScripts, DS_Workflow dsWFStep, DS_Workflow dsWF, Workflow objWF, SessionObject objSes)
        {
            Document_Projects_Workflows objDPWF = ObjectCreator.GetDocument_Project_Workflow(objSes.Connection, objSes.DB_Type);
            List<DS_Workflow.tblsub_workflowsRow> drAvailableDocSubs = new List<DS_Workflow.tblsub_workflowsRow>();
            foreach (DS_Workflow.tblsub_workflowsRow row in drAvailableSubs)
            {
                DS_DP_Workflow dsDPWF = objDPWF.ReadAllForWF(row.Walkflow_ID);
                if (dsDPWF.tbldocument_project_workflows.Where(x => x.Document_Project_ID == dsWF.tblwalkflow[0].Document_Project_ID).Count() > 0)
                {
                    drAvailableDocSubs.Add(row);
                }
            }            
            if (drAvailableDocSubs.Count == 0)
            {
                hndIsStartSubFromAtt.Value = "0";
                hndSubTaskFromDoc.Visible = false;
                hndSubTaskFromDoc_ModalPopupExtender.Enabled = false;
                pnlSubTaskFromDoc.Visible = false;
            }
            else
            {
                hndIsStartSubFromAtt.Value = "1";
                hndSubTaskFromDoc.Visible = true;
                hndSubTaskFromDoc_ModalPopupExtender.Enabled = true;
                pnlSubTaskFromDoc.Visible = true;

                if (!IsPostBack)
                {
                    cboSubWFForDocs.DataSource = drAvailableDocSubs;
                    cboSubWFForDocs.DataTextField = "Workflow_Name";
                    cboSubWFForDocs.DataValueField = "Walkflow_ID";
                    cboSubWFForDocs.DataBind();
                }
                
                objScripts.LoadSubTaskFromDocScript(ref Sub_Task_From_Doc_Script, hndAttachDoc_ID.ClientID, cboSubWFForDocs.ClientID);
            }

            cmdCloseMain.Visible = true;
            cmdAddSub.Visible = true;
            cmdAddSub_ModalPopupExtender.Enabled = true;
            cmdAddSub_Close_ModalPopupExtender.Enabled = true;
            hndSubTask_ModalPopupExtender.Enabled = true;
            pnlSubTaskStart.Visible = true;

            if (!IsPostBack)
            {
                cboSubWorkflow.DataSource = drAvailableSubs;
                cboSubWorkflow.DataTextField = "Workflow_Name";
                cboSubWorkflow.DataValueField = "Walkflow_ID";
                cboSubWorkflow.DataBind();
            }

            cmdSubmit.Text = "Create Sub Tasks & Submit";
            cmdSubmit.Click -= new EventHandler(cmdSubmit_Click);
            cmdSubmit.Attributes["onClick"] = "document.getElementById('" + hndSubmitType.ClientID + "').value = '1'; document.getElementById('" + hndAttachDoc_ID.ClientID + "').value = '0'; document.getElementById('" + txtNoSubOfTasks.ClientID + "').value = '1'; getSubWFDetails(); return CloseMainForSubTasks();";
            cmdInitiateSubTask.Attributes.Add("onClick", "return CreationSubTask();");

            objScripts.LoadSubTaskCreationScript(ref Sub_Task_Script, txtNoSubOfTasks.ClientID, txtSubTaskExtraFieldValue1.ClientID, txtSubTaskExtraFieldValue2.ClientID, cboSubWorkflow.ClientID);
        }

        private void LoadTaskPartially(Script_Generator objScripts, SessionObject objSes, bool LoadTimeline = false)
        {
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            DS_Tasks dsCurrentTask = objTask.Read(Task_ID, false, false, false);

            if (dsCurrentTask.tbltasks.Rows.Count > 0 && dsCurrentTask.tbltasks[0].Current_Step_ID != -1 && dsCurrentTask.tbltasks[0].Current_Step_ID != -2)
            {
                if (IsPostBack == false)
                {
                    ViewState["wid"] = dsCurrentTask.tbltasks[0].Walkflow_ID;
                    ViewState["csid"] = dsCurrentTask.tbltasks[0].Current_Step_ID;
                }
                else
                {
                    dsCurrentTask.tbltasks[0].Walkflow_ID = Convert.ToInt32(ViewState["wid"]);
                    dsCurrentTask.tbltasks[0].Current_Step_ID = Convert.ToInt32(ViewState["csid"]);
                }

                Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                DS_Workflow dsWFStep = objWF.ReadStep(dsCurrentTask.tbltasks[0].Current_Step_ID);

                int EL2CreatedHistoryCount = dsCurrentTask.tbltask_history.Where(x => x.Workflow_Step_ID == dsCurrentTask.tbltasks[0].Current_Step_ID && x.Entity_L2_Created == true).Count();
                bool AddNewEL2 = false;
                if (dsWFStep.tblworkflow_steps[0].Create_New_Entity_L2 == true && EL2CreatedHistoryCount == 0)
                {
                    AddNewEL2 = true;
                }

                DS_Workflow dsWF = objWF.Read(dsCurrentTask.tbltasks[0].Walkflow_ID, false, false);
                FillTaskData(Task_ID, ref dsCurrentTask, dsWF, objTask);

                bool RefreshTask = false;
                LoadTaskDetails(objScripts, dsCurrentTask, AddNewEL2, dsWFStep, objSes, ref RefreshTask);
                if (RefreshTask)
                {
                    FillTaskData(Task_ID, ref dsCurrentTask, dsWF, objTask);
                }

                if (LoadTimeline == true)
                {
                    if (dsWFStep.tblworkflow_steps[0].Show_Timeline)
                    {
                        LoadTaskTimelineChart(dsCurrentTask, objSes);
                    }
                    else
                    {
                        divTaskTimeline.Visible = false;
                        hndStepsList_ModalPopupExtender.Enabled = false;
                        pnlStepsList.Visible = false;
                        hndStepsList.Visible = false;
                    }
                }
            }
            else
            {
                Response.Redirect("error.aspx?");
            }
        }

        private void LoadEL2_Hiararchy(int Entity_L2_ID, SessionObject objSes)
        {
            DS_Entity_Level_2 dsEL2 = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type).ReadHierarchy(Entity_L2_ID);
            if (dsEL2.tblentity_level_2_parents.Rows.Count > 0 || dsEL2.tblentity_level_2_children.Rows.Count > 0)
            {
                pnlEL2Hierarchy.Visible = true;
                lstEL2Hierarchy.Visible = true;
                cmdEL2Hierarchy_ModalPopupExtender.Enabled = true;
                MainControlsCount++;

                ltrCurrEL2.Text = dsEL2.tblentity_level_2[0].Display_Name;
                ltrSubordinateEl2.Text = "";
                foreach (DS_Entity_Level_2.tblentity_level_2_childrenRow row in dsEL2.tblentity_level_2_children)
                {
                    ltrSubordinateEl2.Text = ltrSubordinateEl2.Text + row.Display_Name + "<br />";
                }
                if (ltrSubordinateEl2.Text != "")
                {
                    ltrSubordinateEl2.Text = ltrSubordinateEl2.Text.Substring(0, ltrSubordinateEl2.Text.Length - 6);
                }

                ltrParentEL2.Text = "";
                for (int i = 0; i < dsEL2.tblentity_level_2_parents.Rows.Count; i++)
                {
                    ltrParentEL2.Text = ltrParentEL2.Text + "<div class=\"tl-entry left\">" +
                                                                "<div class=\"tl-icon bg-success\"><i class=\"fa fa-check\"></i></div>" +
                                                                "<div class=\"panel tl-body\">" +
                                                                    "<h4 class=\"text-info\">Parent Level " + (i + 1) + "</h4>" +
                                                                    dsEL2.tblentity_level_2_parents[i].Display_Name +
                                                                "</div>" +
                                                            "</div>";
                }
            }
            else
            {
                pnlEL2Hierarchy.Visible = false;
                lstEL2Hierarchy.Visible = false;
                cmdEL2Hierarchy_ModalPopupExtender.Enabled = false;
            }
        }

        private void LoadTaskRelatedData(DS_Tasks ds, SessionObject objSes)
        {
            string CusCode = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).LoadTaskRelatedData(ds, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            if (CusCode.Trim() == "")
            {
                pnlSpecialData.Visible = false;
                lstSpecialData.Visible = false;
                cmdSpecialData_ModalPopupExtender.Enabled = false;
                divSpecialDataMain.Visible = false;
            }
            else
            {
                divSpecialData.InnerHtml = CusCode;
                divSpecialDataMain.InnerHtml = CusCode;
                MainControlsCount++;
            }
        }

        private void LoadCustomScripts(DS_Tasks ds, SessionObject objSes)
        {
            string CusScript = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).LoadTaskRelatedScripts(ds, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            Custom_Scripts = CusScript;
        }

        private void LoadSubTasks(DS_Tasks ds, int ParentTask_ID, string ParentTaskDetails)
        {
            if (ds.tbltasks.Count > 0)
            {
                ltrSubTaskCount.Text = ds.tbltasks.Count + " Sub Tasks Attached";
                cmdOpenSub.Visible = true;
                hndSubStack.Value = "";
                hndParentForSubs.Value = ParentTask_ID.ToString();
                cmdOpenSub.Attributes.Add("onClick", "return clearSubStack(" + ViewState["tid"] + ", '" + ParentTaskDetails + "');");
            }
            else
            {
                cmdOpenSub.Visible = false;
            }
        }

        private void LoadExtraFeilds(DS_Workflow dsWFStep, SessionObject objSes)
        {
            Tasks objTK = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            string[] arr = objTK.ReadExtraField(Convert.ToInt32(ViewState["tid"]), true);

            if (!string.IsNullOrEmpty(arr[0].Trim(' ')))
            {
                lblExtraFieldName.Text = arr[0];
                ltrExtraFieldName.Text = arr[0];
                ltrExtraFieldTitle.Text = "Edit " + arr[0];
                ltrExtraFieldTitle2.Text = arr[0];
                lblExtraFieldValue.Text = arr[1];
                divExtraField.Visible = true;

                if (dsWFStep.tblworkflow_steps[0].Can_Edit_Extra_Field == true)
                {
                    if (dsWFStep.tblwalkflow[0].Exrta_Field_Type == 3 && dsWFStep.tblwalkflow[0].Extra_Field_Master_Table_ID > 0)
                    {
                        if (IsPostBack == false)
                        {
                            DS_Master_Tables dsM = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type).GetMasterTable(dsWFStep.tblwalkflow[0].Extra_Field_Master_Table_ID);
                            foreach (DS_Master_Tables.tblDataRow drData in dsM.tblData)
                            {
                                cboExtraFieldValue.Items.Add(drData.Data);
                            }
                            if (cboExtraFieldValue.Items.Count > 0)
                            {
                                cboExtraFieldValue.SelectedIndex = 0;
                                txtExtraFieldValue.Text = cboExtraFieldValue.SelectedItem.Text;
                            }
                        }
                        txtExtraFieldValue.CssClass = "form-control hide";
                        cboExtraFieldValue.CssClass = "form-control";
                        cboExtraFieldValue.Attributes.Add("onChange", "$('#" + txtExtraFieldValue.ClientID + "').val($('#" + cboExtraFieldValue.ClientID + "').val());");
                    }
                    else if (dsWFStep.tblwalkflow[0].Exrta_Field_Type == 2)
                    {
                        if (IsPostBack == false)
                        {
                            string ExtraFieldSelction = dsWFStep.tblwalkflow[0].Extra_Field_Selection;
                            foreach (string strSelection in ExtraFieldSelction.Split('|'))
                            {
                                if (strSelection.Trim() != "")
                                {
                                    cboExtraFieldValue.Items.Add(strSelection);
                                }
                            }
                            if (cboExtraFieldValue.Items.Count > 0)
                            {
                                cboExtraFieldValue.SelectedIndex = 0;
                                txtExtraFieldValue.Text = cboExtraFieldValue.SelectedItem.Text;
                            }
                        }
                        txtExtraFieldValue.CssClass = "form-control hide";
                        cboExtraFieldValue.CssClass = "form-control";
                        cboExtraFieldValue.Attributes.Add("onChange", "$('#" + txtExtraFieldValue.ClientID + "').val($('#" + cboExtraFieldValue.ClientID + "').val());");
                    }
                    else
                    {
                        txtExtraFieldValue.CssClass = "form-control";
                        cboExtraFieldValue.CssClass = "form-control hide";
                    }

                    if (IsPostBack == false)
                    {
                        txtExtraFieldValue.Text = arr[1];
                        cboExtraFieldValue.Text = arr[1];
                    }
                    pnlExtraField.Visible = true;
                    lstExtraField1.Visible = true;
                    cmdUpdateExtraField.Visible = true;
                    cmdUpdateExtraField_ModalPopupExtender.Enabled = true;
                    MainControlsCount++;
                }
                else
                {
                    pnlExtraField.Visible = false;
                    lstExtraField1.Visible = false;
                    cmdUpdateExtraField.Visible = false;
                    cmdUpdateExtraField_ModalPopupExtender.Enabled = false;
                }
            }
            else
            {
                lblExtraFieldName.Text = string.Empty;
                ltrExtraFieldName.Text = string.Empty;
                ltrExtraFieldTitle.Text = string.Empty;
                ltrExtraFieldTitle2.Text = string.Empty;
                lblExtraFieldValue.Text = string.Empty;
                divExtraField.Visible = false;

                if (IsPostBack == false)
                {
                    txtExtraFieldValue.Text = string.Empty;
                }
                pnlExtraField.Visible = false;
                lstExtraField1.Visible = false;
                cmdUpdateExtraField.Visible = false;
                cmdUpdateExtraField_ModalPopupExtender.Enabled = false;
            }
            if (!string.IsNullOrEmpty(arr[2].Trim(' ')))
            {
                lblExtraField2Name.Text = arr[2];
                ltrExtraField2Name.Text = arr[2];
                ltrExtraField2Title.Text = "Edit " + arr[2];
                ltrExtraField2Title2.Text = arr[2];
                lblExtraField2Value.Text = arr[3];
                divExtraField2.Visible = true;

                if (dsWFStep.tblworkflow_steps[0].Can_Edit_Extra_Field == true)
                {
                    if (dsWFStep.tblwalkflow[0].Exrta_Field2_Type == 3 && dsWFStep.tblwalkflow[0].Extra_Field2_Master_Table_ID > 0)
                    {
                        if (IsPostBack == false)
                        {
                            DS_Master_Tables dsM = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type).GetMasterTable(dsWFStep.tblwalkflow[0].Extra_Field2_Master_Table_ID);
                            foreach (DS_Master_Tables.tblDataRow drData in dsM.tblData)
                            {
                                cboExtraField2Value.Items.Add(drData.Data);
                            }
                            if (cboExtraField2Value.Items.Count > 0)
                            {
                                cboExtraField2Value.SelectedIndex = 0;
                                txtExtraField2Value.Text = cboExtraField2Value.SelectedItem.Text;
                            }
                        }
                        txtExtraField2Value.CssClass = "form-control hide";
                        cboExtraField2Value.CssClass = "form-control";
                        cboExtraField2Value.Attributes.Add("onChange", "$('#" + txtExtraField2Value.ClientID + "').val($('#" + cboExtraField2Value.ClientID + "').val());");
                    }
                    else if (dsWFStep.tblwalkflow[0].Exrta_Field2_Type == 2)
                    {
                        if (IsPostBack == false)
                        {
                            string ExtraField2Selction = dsWFStep.tblwalkflow[0].Extra_Field2_Selection;
                            foreach (string strSelection in ExtraField2Selction.Split('|'))
                            {
                                if (strSelection.Trim() != "")
                                {
                                    cboExtraField2Value.Items.Add(strSelection);
                                }
                            }
                            if (cboExtraField2Value.Items.Count > 0)
                            {
                                cboExtraField2Value.SelectedIndex = 0;
                                txtExtraField2Value.Text = cboExtraField2Value.SelectedItem.Text;
                            }
                        }
                        txtExtraField2Value.CssClass = "form-control hide";
                        cboExtraField2Value.CssClass = "form-control";
                        cboExtraField2Value.Attributes.Add("onChange", "$('#" + txtExtraField2Value.ClientID + "').val($('#" + cboExtraField2Value.ClientID + "').val());");
                    }
                    else
                    {
                        txtExtraField2Value.CssClass = "form-control";
                        cboExtraField2Value.CssClass = "form-control hide";
                    }

                    if (IsPostBack == false)
                    {
                        txtExtraField2Value.Text = arr[3];
                        cboExtraField2Value.Text = arr[3];
                    }
                    pnlExtraField2.Visible = true;
                    lstExtraField2.Visible = true;
                    cmdUpdateExtraField2.Visible = true;
                    cmdUpdateExtraField2_ModalPopupExtender.Enabled = true;
                    MainControlsCount++;
                }
                else
                {
                    pnlExtraField2.Visible = false;
                    lstExtraField2.Visible = false;
                    cmdUpdateExtraField2.Visible = false;
                    cmdUpdateExtraField2_ModalPopupExtender.Enabled = false;
                }
            }
            else
            {
                lblExtraField2Name.Text = string.Empty;
                ltrExtraField2Name.Text = string.Empty;
                ltrExtraField2Title.Text = string.Empty;
                ltrExtraField2Title2.Text = string.Empty;
                lblExtraField2Value.Text = string.Empty;
                divExtraField2.Visible = false;

                if (IsPostBack == false)
                {
                    txtExtraField2Value.Text = string.Empty;
                }
                pnlExtraField2.Visible = false;
                lstExtraField2.Visible = false;
                cmdUpdateExtraField2.Visible = false;
                cmdUpdateExtraField2_ModalPopupExtender.Enabled = false;
            }

            if (dsWFStep.tblworkflow_steps[0].Can_Edit_Due_Date == true)
            {
                pnlDueDate.Visible = true;
                lstDueDate.Visible = true;
                cmdEditDueDate.Visible = true;
                cmdEditDueDate_ModalPopupExtender.Enabled = true;
                MainControlsCount++;
            }
            else
            {
                pnlDueDate.Visible = false;
                lstDueDate.Visible = false;
                cmdEditDueDate.Visible = false;
                cmdEditDueDate_ModalPopupExtender.Enabled = false;
            }
        }

        private void LoadFileTypes(DS_Workflow.tblwalkflowRow dr)
        {
            cboFileTypesUpload.Items.Clear();
            cboDocCategories.Items.Clear();
            string[] strTypes = dr.Workflow_Doc_Types.Split('|');

            if (strTypes.Length > 0)
            {
                foreach (string str in strTypes)
                {
                    if (str.Trim() != "")
                    {
                        cboFileTypesUpload.Items.Add(new ListItem(str, str));
                        cboDocCategories.Items.Add(new ListItem(str, str));
                    }
                }
            }

            cboFileTypesUpload.Items.Insert(cboFileTypesUpload.Items.Count - 1 < 0 ? 0 : cboFileTypesUpload.Items.Count, new ListItem("Other Documents", "Other Documents"));
            cboDocCategories.Items.Insert(cboDocCategories.Items.Count - 1 < 0 ? 0 : cboDocCategories.Items.Count, new ListItem("Other Documents", "Other Documents"));
            cboDocCategories.Items.Insert(0, new ListItem("[All Document Categories]", "0"));
            cboDocCategories.Items.Add(new ListItem("Attachments", "Attachments"));
        }

        private void LoadCommentTypes(DS_Workflow.tblwalkflowRow dr)
        {
            cboCommentType.Items.Clear();
            cboDocCommentType.Items.Clear();
            string[] strTypes = dr.Workflow_Comment_Types.Split('|');

            if (strTypes.Length > 0)
                foreach (string str in strTypes)
                {
                    if (str.Trim() != "")
                    {
                        cboCommentType.Items.Add(new ListItem(str, str));
                        cboDocCommentType.Items.Add(new ListItem(str, str));
                    }
                }

            cboCommentType.Items.Insert(cboCommentType.Items.Count - 1 < 0 ? 0 : cboCommentType.Items.Count, new ListItem("Other Comments", "Other Comments"));
            cboDocCommentType.Items.Insert(cboDocCommentType.Items.Count - 1 < 0 ? 0 : cboDocCommentType.Items.Count, new ListItem("Other Comments", "Other Comments"));
        }

        private void LoadTaskDetails(Script_Generator objScripts, DS_Tasks dsTask, bool AddNewEL2, DS_Workflow dsWorkflow, SessionObject objSes, ref bool RefreshTaskData)
        {
            if (dsWorkflow.tblworkflow_steps[0].Show_Custom_Post)
            {
                pnlCuzPost.Controls.Clear();

                pnlStepData.Visible = false;
                objCuz = (controls_customizable.cuz_task_post)Page.LoadControl("~/controls_customizable/cuz_task_post.ascx");

                objCuz.PostElements.AddNewEL2 = AddNewEL2;
                objCuz.PostElements.Task_ID = dsTask.tbltasks[0].Task_ID;
                objCuz.PostElements.Workflow_ID = dsTask.tbltasks[0].Walkflow_ID;
                objCuz.PostElements.CurrentStep_ID = dsTask.tbltasks[0].Current_Step_ID;

                pnlCuzPost.Controls.Add(objCuz);

                Required_Fields = objCuz.PostElements.RequiredFieldsValidationScript;
                Old_Field_Validation = objCuz.PostElements.OldFieldsValidationScript;
                LoadingScripts = LoadingScripts + objCuz.PostElements.LoadingScript;
                Formula_Fields = objCuz.PostElements.FormulaFieldsScript;
                Next_Step_Script = objCuz.PostElements.NextStepScript;                

                if (objCuz.PostElements.HaveHelpText)
                {
                    hndHelp_ModalPopupExtender.Enabled = true;
                    pnlHelp.Visible = true;
                    HelpScript = objCuz.PostElements.HelpScript;
                    HelpPanelResizeScript = objCuz.PostElements.HelpPanelResizeScript;
                }
                else
                {
                    HelpScript = "";
                    HelpPanelResizeScript = "";
                    hndHelp_ModalPopupExtender.Enabled = false;
                    pnlHelp.Visible = false;
                }

                if (objCuz.PostElements.ShowSubmitFooter)
                {
                    divSubmitFooter.Visible = true;
                    if (objCuz.PostElements.ShowSaveFields)
                    {
                        cmdSave.Visible = true;
                    }
                    else
                    {
                        cmdSave.Visible = false;
                    }
                }
                else
                {
                    divSubmitFooter.Visible = false;
                }
                objCtrlList = objCuz.PostElements.ControlsSet;
                if (objCuz.PostElements.RefreshTask)
                {
                    RefreshTaskData = true;
                }
            }
            else
            {
                pnlStepData.Visible = true;
                pnlCuzPost.Visible = false;

                bool blnValidateOldFieds = false;
                string strOldFieldValidation = "";
                if (dsWorkflow.tblworkflow_step_fields.Where(x => x.Validate_With_Field_ID > 0).Count() > 0)
                {
                    blnValidateOldFieds = true;
                    Old_Field_Validation = "function ValidateWithOldField() {" + "\r\n";
                }
                else
                {
                    Old_Field_Validation = "function ValidateWithOldField() {" + "\r\n" +
                                                "return true;" + "\r\n" +
                                            "}";
                }

                string strRequiredFieldValidation = "";
                if (AddNewEL2 == true)
                {
                    Required_Fields = "function ValidatRequiredFields() {" + "\r\n" +
                                            "remove_field_erros();" + "\r\n" +
                                            "var ret = checkEL2()" + "\r\n";
                }
                else
                {
                    Required_Fields = "function ValidatRequiredFields() {" + "\r\n" +
                                            "remove_field_erros();" + "\r\n" +
                                            "var ret = true;" + "\r\n";
                }

                Required_Fields = Required_Fields + ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostScripts(dsTask, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

                objCtrlList.Task_ID = dsTask.tbltasks[0].Task_ID;
                objCtrlList.Current_Step_ID = dsTask.tbltasks[0].Current_Step_ID;

                pnlStepData.Controls.Clear();

                int ControlIndex = 0;
                List<string> Help_Texts = new List<string>();

                N_Ter.Customizable.Master_Tables objMasterTables = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);
                Common_Task_Actions objTskAct = new Common_Task_Actions();

                List<DS_Tasks> dsTasks = new List<DS_Tasks>();
                dsTasks.Add(dsTask);

                if (dsWorkflow.tblworkflow_step_fields.Where(x => x.Workflow_Step_Field_Cat_ID == 0).Count() > 0)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                    divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                    int rowWidth = 0;

                    pnlStepData.CssClass = "row";
                    foreach (DS_Workflow.tblworkflow_step_fieldsRow rowStepField in dsWorkflow.tblworkflow_step_fields)
                    {
                        divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref objCtrlList, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
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
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref objCtrlList, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
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

                if (blnValidateOldFieds == true)
                {
                    if (strOldFieldValidation.Length > 0)
                    {
                        strOldFieldValidation = strOldFieldValidation.Substring(5);
                        Old_Field_Validation = Old_Field_Validation + strOldFieldValidation + "else {" + "\r\n" +
                                                                                                    "return true;" + "\r\n" +
                                                                                               "}" + "\r\n" +
                                                                                           "}";
                    }
                    else
                    {
                        Old_Field_Validation = Old_Field_Validation + "return true;" + "\r\n" +
                                                                  "}";
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

                ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostFormAdjustments(dsTask, ref objCtrlList, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

                objScripts.LoadFormulaScripts(ref LoadingScripts, ref Formula_Fields, objTskAct.GetControlNamePrefix(FieldOrigin.Task_Field), dsWorkflow.tblworkflow_formulas);
                objScripts.LoadNextStepScript(ref Next_Step_Script, Convert.ToInt32(ViewState["tid"]), Convert.ToInt32(ViewState["wid"]), Convert.ToInt32(ViewState["csid"]), dsWorkflow, objCtrlList);
            }
        }

        private void LoadAddonFields(Script_Generator objScripts, DS_Tasks dsTask, DS_Workflow dsWFStep, bool PagePostBack, SessionObject objSes)
        {
            bool blnValidateOldFieds = false;
            string strOldFieldValidation = "";
            if (dsWFStep.tblworkflow_addon_fields.Where(x => x.Validate_With_Field_ID > 0).Count() > 0)
            {
                blnValidateOldFieds = true;
                Old_Addon_Field_Validation = "function ValidateWithOldAddonField() {" + "\r\n";
            }
            else
            {
                Old_Addon_Field_Validation = "function ValidateWithOldAddonField() {" + "\r\n" +
                                            "return true;" + "\r\n" +
                                        "}";
            }

            string strRequiredFieldValidation = "";
            Required_Addon_Fields = "function ValidatRequiredAddonFields() {" + "\r\n" +
                                        "remove_field_erros();" + "\r\n" +
                                        "var ret = true;" + "\r\n";

            objAddonCtrlList.Task_ID = dsTask.tbltasks[0].Task_ID;
            objAddonCtrlList.Current_Step_ID = dsTask.tbltasks[0].Current_Step_ID;
            objAddonCtrlList.Addon_ID = dsWFStep.tblworkflow_steps[0].Addon_ID;

            pnlAddonFields.Controls.Clear();

            int ControlIndex = 0;
            List<string> Help_Texts = new List<string>();

            N_Ter.Customizable.Master_Tables objMasterTables = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);
            Common_Task_Actions objTskAct = new Common_Task_Actions();

            List<DS_Tasks> dsTasks = new List<DS_Tasks>();
            dsTasks.Add(dsTask);

            System.Web.UI.HtmlControls.HtmlGenericControl divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
            int rowWidth = 0;

            pnlAddonFields.CssClass = "row";
            foreach (DS_Workflow.tblworkflow_addon_fieldsRow rowStepAddonField in dsWFStep.tblworkflow_addon_fields)
            {
                divMainRowControl.Controls.Add(objTskAct.GetTaskAddonObject(objScripts, PagePostBack, objMasterTables, objSes.Currency_Sbl, dsWFStep, dsTasks, rowStepAddonField, ref objAddonCtrlList, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetAddonHelp", true));
                if (rowStepAddonField.Help_Text.Trim() != "")
                {
                    Help_Texts.Add(rowStepAddonField.Field_Name + "|" + rowStepAddonField.Help_Text);
                    ControlIndex++;
                }
                if (rowWidth == 12)
                {
                    pnlAddonFields.Controls.Add(divMainRowControl);
                    divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                    divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                    rowWidth = 0;
                }
            }
            if (rowWidth > 0)
            {
                pnlAddonFields.Controls.Add(divMainRowControl);
            }

            if (blnValidateOldFieds == true)
            {
                if (strOldFieldValidation.Length > 0)
                {
                    strOldFieldValidation = strOldFieldValidation.Substring(5);
                    Old_Addon_Field_Validation = Old_Addon_Field_Validation + strOldFieldValidation + "else {" + "\r\n" +
                                                                                                "return true;" + "\r\n" +
                                                                                           "}" + "\r\n" +
                                                                                       "}";
                }
                else
                {
                    Old_Addon_Field_Validation = Old_Addon_Field_Validation + "return true;" + "\r\n" +
                                                              "}";
                }
            }

            Required_Addon_Fields = Required_Addon_Fields + strRequiredFieldValidation + "return ret;" + "\r\n" +
                                                                "}";

            if (Help_Texts.Count > 0)
            {
                hndAddonHelp_ModalPopupExtender.Enabled = true;
                pnlAddonHelp.Visible = true;
                objScripts.LoadAddonHelpScripts(ref AddonHelpScript, ref AddonHelpPanelResizeScript, Help_Texts);
            }
            else
            {
                AddonHelpScript = "";
                AddonHelpPanelResizeScript = "";
                hndAddonHelp_ModalPopupExtender.Enabled = false;
                pnlAddonHelp.Visible = false;
            }
        }

        private void LoadAttachments(int Document_Project_ID, DS_Tasks.tbltask_docsDataTable tbl, DS_Tasks.tbltask_attachmentDataTable tblAtt, DS_Tasks.tbltask_commentsDataTable tblComms, SessionObject objSes)
        {
            bool AllowDeleteDocuments = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).isAllowed(AllowedAreas.Delete_Docs, objSes.UserID);
            if (AllowDeleteDocuments)
            {
                AllowDeleteDocuments = Convert.ToBoolean(ViewState["dd"]);
            }

            if (AllowDeleteDocuments)
            {
                if (!IsPostBack)
                {
                    FillReplaceFileDropDown(tbl, objSes);
                }
                
                DocReplaceResizeScript = "AdjustPopupSize(80, 800, 'at_model_replace_file');";
                cmdReplaceDocument.Visible = true;
                cmdReplaceDocumentR.Visible = true;
                pnlReplaceFile.Visible = true;
                hndReplaceFile_ModalPopupExtender.Enabled = true;
            }
            else
            {
                DocReplaceResizeScript = "";
                cmdReplaceDocument.Visible = false;
                cmdReplaceDocumentR.Visible = false;
                pnlReplaceFile.Visible = false;
                hndReplaceFile_ModalPopupExtender.Enabled = false;
            }

            bool CanStartSubTasks = false;
            if (Convert.ToInt32(hndIsStartSubFromAtt.Value) == 1)
            {
                CanStartSubTasks = true;
            }
            ltrAttachments.Text = "";
            string[] fileSplits;
            int FileCommentCount = 0;
            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();

            List<DS_Tasks.tbltask_docsRow> drDocs = tbl.Where(x => x.Is_Result == false && (x.Uploaded_By == objSes.UserID || x.Access_Level >= objSes.AccLevel)).ToList();
            List<DS_Tasks.tbltask_attachmentRow> drAtts = tblAtt.Where(x => x.Is_Result == false && (x.Attached_By == objSes.UserID || x.Access_Level >= objSes.AccLevel)).ToList();

            if (objSes.TaskDocDir)
            {
                drDocs = drDocs.OrderBy(y => y.Task_Doc_Type).ThenBy(z => z.Uploaded_Date).ToList();
                drAtts = drAtts.OrderBy(y => y.Created_Date).ToList();
            }
            else
            {
                drDocs = drDocs.OrderBy(y => y.Task_Doc_Type).ThenByDescending(z => z.Uploaded_Date).ToList();
                drAtts = drAtts.OrderByDescending(y => y.Created_Date).ToList();
            }

            ltrTaskDocCount.Text = "(" + Convert.ToString(drDocs.Count + drAtts.Count) + ")";

            if (drDocs.Count > 0)
            {
                string CurrDocType = string.Empty;
                foreach (DS_Tasks.tbltask_docsRow row in drDocs)
                {
                    if (row.Task_Doc_Type.Trim() != CurrDocType)
                    {
                        ltrAttachments.Text = ltrAttachments.Text + "<div class='comment no-padding-b'>" + "\r\n" +
                                                                    " <div class='comment-body no-margin-hr'>" +
                                                                    "   <div class='comment-text'>" +
                                                                    "    <b>" + row.Task_Doc_Type + "</b>" +
                                                                    "   </div>" + " </div>\r\n" +
                                                                    "</div>";
                        CurrDocType = row.Task_Doc_Type.Trim();
                    }

                    FileCommentCount = tblComms.Where(y => y.Task_Doc_ID == row.Task_Doc_ID).Count();
                    fileSplits = row.Doc_Path.Split('\\');
                    ltrAttachments.Text = ltrAttachments.Text + "<div class='comment'>" + "\r\n" +
                                                                " <img src='" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "' alt='Profile Picture' class='comment-avatar'>" + "\r\n" +
                                                                " <div class='comment-body'>" +
                                                                    (row.isParentRecord ? "<div class='ttip badge badge-primary pull-right' title='From Parent Task' data-placement='left'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                                    (row.isChildRecord ? "<div class='ttip badge badge-info pull-right' title='From Sub Task' data-placement='left'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                                    (AllowDeleteDocuments && row.isParentRecord == false && row.isChildRecord == false && row.Uploaded_By == objSes.UserID ? "<button class='ttip btn btn-danger btn-sm btn-rounded pull-right' onclick='return DeleteAttachment(" + row.Task_Doc_ID + ");' title='Delete Document' data-placement='left'><i class='fa fa-trash-o'></i></button>" + "\r\n" : "") +
                                                                "   <div class='comment-by'>" + row.Uploaded_By_Name + "</div>" + "\r\n" +
                                                                "   <div class='comment-text'>" +
                                                                "    <button class='ttip btn btn-primary btn-sm btn-rounded' onclick='return showDocComments(" + row.Task_Doc_ID + ");' title='Comments' data-placement='top'><i class='fa fa-comment'></i>" + (FileCommentCount > 0 ? "<span class='badge badge-pill badge_small'>" + FileCommentCount + "</span>" : "") + "</button>&nbsp;<a href='document_preview.aspx?fid=" + objURL.Encrypt(Convert.ToString(row.Task_Doc_ID)) + "' target='_blank' class='media'>" +
                                                                       row.Doc_Number + (row.Is_Re_Upload ? " - (R) " : "") + " - <b>" + fileSplits[fileSplits.Length - 1] + "</b>" +
                                                                "    </a>" + "\r\n" +
                                                                "   </div>" +
                                                                "   <div class='comment-actions'>&nbsp;" +
                                                                "     <span class='pull-right'>" + string.Format("{0:" + Constants.DateFormat + " HH:mm}", row.Uploaded_Date) + "</span>" + "\r\n" +
                                                                "   </div>\r\n" +
                                                                " </div>\r\n" +
                                                                "</div>";
                }
            }

            if (drAtts.Count > 0)
            {
                ltrAttachments.Text = ltrAttachments.Text + "<div class='comment no-padding-b'>" + "\r\n" +
                                                            " <div class='comment-body no-margin-hr'>" +
                                                            "   <div class='comment-text'>" +
                                                            "    <b>Attachments</b>" +
                                                            "   </div>" + " </div>\r\n" +
                                                            "</div>";
                foreach (DS_Tasks.tbltask_attachmentRow row in drAtts)
                {
                    FileCommentCount = tblComms.Where(y => y.Task_Attachment_ID == row.Task_Doc_ID).Count();
                    fileSplits = row.Document_Path.Split('\\');
                    ltrAttachments.Text = ltrAttachments.Text + "<div class='comment'>" + "\r\n" +
                                                               " <img src='" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "' alt='Profile Picture' class='comment-avatar'>" + "\r\n" +
                                                               " <div class='comment-body'>" +
                                                                   (row.isParentRecord ? "<div class='ttip badge badge-primary pull-right' title='From Parent Task' data-placement='left'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                                   (row.isChildRecord ? "<div class='ttip badge badge-info pull-right' title='From Sub Task' data-placement='left'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                                   (AllowDeleteDocuments && row.isParentRecord == false && row.isChildRecord == false && row.Attached_By == objSes.UserID ? "<button class='ttip btn btn-danger btn-sm btn-rounded pull-right' onclick='return showLinkDel(" + row.Task_Doc_ID + ");' title='Unlink Document' data-placement='left'><i class='fa fa-unlink'></i></button>" + "\r\n" : "") +
                                                               "   <div class='comment-by'>" + row.Attached_By_Name + "</div>" + "\r\n" +
                                                               "   <div class='comment-text'>" +
                                                               "    <button class='ttip btn btn-primary btn-sm btn-rounded' onclick='return showAttachComments(" + row.Task_Doc_ID + ");' title='Comments' data-placement='top'><i class='fa fa-comment'></i>" + (FileCommentCount > 0 ? "<span class='badge badge-pill badge_small'>" + FileCommentCount + "</span>" : "") + "</button>&nbsp;<button class='ttip btn btn-success btn-sm btn-rounded' onclick='return showAttachInfo(" + row.Document_ID + ");' title='Document Info' data-placement='top'><i class='fa fa-info'></i></button>&nbsp;" + (CanStartSubTasks && Document_Project_ID == row.Document_Project_ID ? "<button class='ttip btn btn-primary btn-sm btn-rounded' onclick='return subTaskFromDoc(" + row.Document_ID + ");' title='Create Sub Task' data-placement='top'><i class='fa fa-tasks'></i></button>&nbsp;" : "") + "<a href='document_preview_dp.aspx?fid=" + objURL.Encrypt(Convert.ToString(row.Document_ID)) + "' target='_blank' class='media'>" +
                                                                      row.Document_No + " - <b>" + fileSplits[fileSplits.Length - 1] + "</b>" +
                                                               "    </a>" + "\r\n" +
                                                               "   </div>" +
                                                               "   <div class='comment-actions'>&nbsp;" +
                                                               "      <span class='pull-right'>" + string.Format("{0:" + Constants.DateFormat + " HH:mm}", row.Created_Date) + "</span>" + "\r\n" +
                                                               "   </div>\r\n" +
                                                               " </div>\r\n" +
                                                               "</div>";
                }
            }

            ltrAttachmentsR.Text = "";

            drDocs = tbl.Where(x => x.Is_Result && (x.Uploaded_By == objSes.UserID || x.Access_Level >= objSes.AccLevel)).ToList();
            drAtts = tblAtt.Where(x => x.Is_Result && (x.Attached_By == objSes.UserID || x.Access_Level >= objSes.AccLevel)).ToList();

            if (objSes.TaskDocDir)
            {
                drDocs = drDocs.OrderBy(y => y.Task_Doc_Type).ThenBy(z => z.Uploaded_Date).ToList();
                drAtts = drAtts.OrderBy(y => y.Created_Date).ToList();
            }
            else
            {
                drDocs = drDocs.OrderBy(y => y.Task_Doc_Type).ThenByDescending(z => z.Uploaded_Date).ToList();
                drAtts = drAtts.OrderByDescending(y => y.Created_Date).ToList();
            }

            ltrTaskDocCountR.Text = "(" + Convert.ToString(drDocs.Count + drAtts.Count) + ")";

            if (drDocs.Count > 0)
            {
                string CurrDocType = string.Empty;
                foreach (DS_Tasks.tbltask_docsRow row in drDocs)
                {
                    if (row.Task_Doc_Type.Trim() != CurrDocType)
                    {
                        ltrAttachmentsR.Text = ltrAttachmentsR.Text + "<div class='comment no-padding-b'>" + "\r\n" +
                                                                    " <div class='comment-body no-margin-hr'>" +
                                                                    "   <div class='comment-text'>" +
                                                                    "    <b>" + row.Task_Doc_Type + "</b>" +
                                                                    "   </div>" + " </div>\r\n" +
                                                                    "</div>";
                        CurrDocType = row.Task_Doc_Type.Trim();
                    }
                    fileSplits = row.Doc_Path.Split('\\');
                    FileCommentCount = tblComms.Where(y => y.Task_Doc_ID == row.Task_Doc_ID).Count();
                    ltrAttachmentsR.Text = ltrAttachmentsR.Text + "<div class='comment'>" + "\r\n" +
                                                                " <img src='" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "' alt='Profile Picture' class='comment-avatar'>" + "\r\n" +
                                                                " <div class='comment-body'>" +
                                                                    (row.isParentRecord ? "<div class='ttip badge badge-primary pull-right' title='From Parent Task' data-placement='left' ><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                                    (row.isChildRecord ? "<div class='ttip badge badge-info pull-right' title='From Sub Task' data-placement='left' ><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                                    (AllowDeleteDocuments && row.isParentRecord == false && row.isChildRecord == false && row.Uploaded_By == objSes.UserID ? "<button class='ttip btn btn-danger btn-sm btn-rounded pull-right' onclick='return DeleteAttachment(" + row.Task_Doc_ID + ");' title='Delete Document' data-placement='left' ><i class='fa fa-trash-o'></i></button>" + "\r\n" : "") +
                                                                "   <div class='comment-by'>" + row.Uploaded_By_Name + "</div>" + "\r\n" +
                                                                "   <div class='comment-text'>" +
                                                                "    <button class='ttip btn btn-primary btn-sm btn-rounded' onclick='return showDocComments(" + row.Task_Doc_ID + ");' title='Comments' data-placement='top' ><i class='fa fa-comment'></i>" + (FileCommentCount > 0 ? "<span class='badge badge-pill badge_small'>" + FileCommentCount + "</span>" : "") + "</button>&nbsp;<a href='document_preview.aspx?fid=" + objURL.Encrypt(Convert.ToString(row.Task_Doc_ID)) + "' target='_blank' class='media'>" +
                                                                       row.Doc_Number + (row.Is_Re_Upload ? " - (R) " : "") + " - <b>" + fileSplits[fileSplits.Length - 1] + "</b>" +
                                                                "    </a>" + "\r\n" +
                                                                "   </div>" +
                                                                "  <div class='comment-actions'>&nbsp;" +
                                                                "   <span class='pull-right'>" + string.Format("{0:" + Constants.DateFormat + " HH:mm}", row.Uploaded_Date) + "</span>" + "\r\n" +
                                                                "  </div>\r\n" +
                                                                " </div>\r\n" +
                                                                "</div>";
                }
            }

            if (drAtts.Count > 0)
            {
                ltrAttachmentsR.Text = ltrAttachmentsR.Text + "<div class='comment no-padding-b'>" + "\r\n" +
                                                            " <div class='comment-body no-margin-hr'>" +
                                                            "   <div class='comment-text'>" +
                                                            "    <b>Attachments</b>" +
                                                            "   </div>" + " </div>\r\n" +
                                                            "</div>";
                foreach (DS_Tasks.tbltask_attachmentRow row in drAtts)
                {
                    FileCommentCount = tblComms.Where(y => y.Task_Attachment_ID == row.Task_Doc_ID).Count();
                    fileSplits = row.Document_Path.Split('\\');
                    ltrAttachmentsR.Text = ltrAttachmentsR.Text + "<div class='comment'>" + "\r\n" +
                                                               " <img src='" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "' alt='Profile Picture' class='comment-avatar'>" + "\r\n" +
                                                               " <div class='comment-body'>" +
                                                                   (row.isParentRecord ? "<div class='ttip badge badge-primary pull-right' title='From Parent Task' data-placement='left' ><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                                   (row.isChildRecord ? "<div class='ttip badge badge-info pull-right' title='From Sub Task' data-placement='left' ><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                                   (AllowDeleteDocuments && row.isParentRecord == false && row.isChildRecord == false && row.Attached_By == objSes.UserID ? "<button class='ttip btn btn-danger btn-sm btn-rounded pull-right' onclick='return showLinkDel(" + row.Task_Doc_ID + ");' title='Unlink Document' data-placement='left'><i class='fa fa-unlink'></i></button>" + "\r\n" : "") +
                                                               "   <div class='comment-by'>" + row.Attached_By_Name + "</div>" + "\r\n" +
                                                               "   <div class='comment-text'>" +
                                                               "    <button class='ttip btn btn-primary btn-sm btn-rounded' onclick='return showAttachComments(" + row.Task_Doc_ID + ");' title='Comments' data-placement='top'><i class='fa fa-comment'></i>" + (FileCommentCount > 0 ? "<span class='badge badge-pill badge_small'>" + FileCommentCount + "</span>" : "") + "</button>&nbsp;<button class='ttip btn btn-success btn-sm btn-rounded' onclick='return showAttachInfo(" + row.Document_ID + ");' title='Document Info' data-placement='top'><i class='fa fa-info'></i></button>&nbsp;" + (CanStartSubTasks && Document_Project_ID == row.Document_Project_ID ? "<button class='ttip btn btn-primary btn-sm btn-rounded' onclick='return subTaskFromDoc(" + row.Document_ID + ");' title='Create Sub Task' data-placement='top'><i class='fa fa-tasks'></i></button>&nbsp;" : "") + "<a href='document_preview_dp.aspx?fid=" + objURL.Encrypt(Convert.ToString(row.Document_ID)) + "' target='_blank' class='media'>" +
                                                                      row.Document_No + " - <b>" + fileSplits[fileSplits.Length - 1] + "</b>" +
                                                               "    </a>" + "\r\n" +
                                                               "   </div>" +
                                                               "  <div class='comment-actions'>&nbsp;" +
                                                               "   <span class='pull-right'>" + string.Format("{0:" + Constants.DateFormat + " HH:mm}", row.Created_Date) + "</span>" + "\r\n" +
                                                               "  </div>\r\n" +
                                                               " </div>\r\n" +
                                                               "</div>";
                }
            }
        }

        private void FillReplaceFileDropDown(DS_Tasks.tbltask_docsDataTable tbl, SessionObject objSes)
        {
            cboFileSource.Items.Clear();
            cboFileSource.Items.Add(new ListItem("[Select a File]", "0"));
            foreach (DS_Tasks.tbltask_docsRow drDoc in tbl.Where(x => x.Is_Result == false && x.Uploaded_By == objSes.UserID))
            {
                cboFileSource.Items.Add(new ListItem(drDoc.Doc_Number + " - " + drDoc.Doc_Path.Split('\\').Last(), drDoc.Task_Doc_ID.ToString()));
            }
            cboFileResult.Items.Clear();
            cboFileResult.Items.Add(new ListItem("[Select a File]", "0"));
            foreach (DS_Tasks.tbltask_docsRow drDoc in tbl.Where(x => x.Is_Result && x.Uploaded_By == objSes.UserID))
            {
                cboFileResult.Items.Add(new ListItem(drDoc.Doc_Number + " - " + drDoc.Doc_Path.Split('\\').Last(), drDoc.Task_Doc_ID.ToString()));
            }
        }

        private void LoadHistory(Script_Generator objScripts, DS_Tasks dsTask, DS_Tasks dsSubs, SessionObject objSes)
        {
            Users objUser = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
            if (objUser.isAllowed(AllowedAreas.Task_Script_Extract, objSes.UserID))
            {
                Task_Script = objScripts.TaskScript(dsTask.tbltasks[0].Task_ID);
                MainControlsCount++;
            }
            else
            {
                lstTaskScript.Visible = false;
            }
            bool AllowDeleteComments = objUser.isAllowed(AllowedAreas.Delete_Comments, objSes.UserID);
            if (AllowDeleteComments == true)
            {
                AllowDeleteComments = Convert.ToBoolean(ViewState["dc"]);
            }
            bool AllowDeleteAddons = objUser.isAllowed(AllowedAreas.Delete_Addons, objSes.UserID);
            if (AllowDeleteAddons == true)
            {
                AllowDeleteAddons = Convert.ToBoolean(ViewState["da"]);
            }

            Common_Actions objCom = new Common_Actions();
            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();

            List<TaskHistoryItem> HistList = new List<TaskHistoryItem>();

            string TaskUpdateFields = "";
            string UserDetails = "";
            string HistoryText = "";
            string SubTasksList = "";
            string SubTaskOwnerName = "";
            string SubTaskOwnerImage = "";
            int SubTaskOwnerID = 0;
            int WeekEndDayCount = 0;
            DateTime SubTaskDate = DateTime.Today.Date;

            List<DS_Tasks.tbltasksRow> drSubs;
            List<DS_Tasks.tbltask_history_durationsRow> drTimeSlots;
            List<DS_Tasks.tbltask_update_fieldsRow> drFields;
            List<DS_Tasks.tbltask_update_fieldsRow> drUserFields;
            List<DS_Tasks.tbltask_update_addon_fieldsRow> drAddonFields;
            List<int> UpdateUserIDs;

            Common_Task_Actions objCommAct = new Common_Task_Actions();

            List<TaskHistoryItem> TaskProgress = new List<TaskHistoryItem>();

            int[] Workflow_Step_IDs = dsTask.tbltask_history.Select(x => x.Workflow_Step_ID).Distinct().ToArray();
            DS_Workflow dsWFStepCats = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).ReadFieldStepCats(Workflow_Step_IDs);

            foreach (DS_Tasks.tbltask_historyRow row in dsTask.tbltask_history)
            {
                TaskUpdateFields = "";
                drFields = dsTask.tbltask_update_fields.Where(x => x.Task_Update_ID == row.Task_Update_ID && x.Show_In_History == true).ToList();
                if (drFields.Count > 0)
                {
                    UpdateUserIDs = drFields.Select(y => y.User_ID).Distinct().ToList();
                    if (UpdateUserIDs.Count > 1)
                    {
                        TaskUpdateFields = "";

                        DS_Users dsUsrs = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).Read(UpdateUserIDs);
                        List<DS_Users.tblusersRow> drUsr;
                        foreach (int User_ID in UpdateUserIDs)
                        {
                            drUsr = dsUsrs.tblusers.Where(u => u.User_ID == User_ID).ToList();
                            if (drUsr.Count > 0)
                            {
                                TaskUpdateFields = TaskUpdateFields + "<div class=\"mt10 mb5 text-info\"><img src=\"" + (drUsr[0].IsImage_PathNull() ? "assets/images/user.png" : drUsr[0].Image_Path) + "\" alt=\"\" style=\"width:20px;height:20px;\" class=\"rounded\">&nbsp;&nbsp;" + drUsr[0].First_Name + " " + drUsr[0].Last_Name + "</div>";
                            }
                            else
                            {
                                TaskUpdateFields = TaskUpdateFields + "<div class=\"mt10 mb5 text-info\"><img src=\"assets/images/user.png\" alt=\"\" style=\"width:20px;height:20px;\" class=\"rounded\">&nbsp;&nbsp;Unknown User</div>";
                            }

                            drUserFields = drFields.Where(z => z.User_ID == User_ID).ToList();
                            TaskUpdateFields = TaskUpdateFields + objCommAct.GetTaskUpdateForHistory(drUserFields, dsWFStepCats.tblworkflow_step_field_cats.Where(x => x.Workflow_Step_ID == row.Workflow_Step_ID).ToList(), objSes);
                        }
                    }
                    else
                    {
                        TaskUpdateFields = objCommAct.GetTaskUpdateForHistory(drFields, dsWFStepCats.tblworkflow_step_field_cats.Where(x => x.Workflow_Step_ID == row.Workflow_Step_ID).ToList(), objSes);
                    }
                }

                if (!row.IsStep_Completed_By_NameNull())
                {
                    UserDetails = "<h5 class='text-warning mt'><img class=\"rounded\" src=\"" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "\" alt=\"Profile picture\" style=\"width: 25px;height: 25px;margin-top: -2px;\">" + "\r\n" +
                                  "<span class=\"text-info\">" + row.Step_Completed_By_Name + "</span></h5>" + "\r\n";
                }
                else
                {
                    UserDetails = "";
                }

                if (row.IsStep_Finished_DateNull())
                {
                    if (row.Posted_Date.AddDays(2) < DateTime.Now)
                    {
                        WeekEndDayCount = Enumerable.Range(0, (int)((DateTime.Today.Date.AddDays(-1) - row.Posted_Date.Date.AddDays(1)).TotalDays) + 1).Select(n => row.Posted_Date.Date.AddDays(1).AddDays(n)).Where(x => x.DayOfWeek == DayOfWeek.Saturday || x.DayOfWeek == DayOfWeek.Sunday).Count();
                        row.Waiting_Hours = (DateTime.Now - row.Posted_Date).TotalHours - (WeekEndDayCount * 24);
                    }
                    else
                    {
                        row.Waiting_Hours = (DateTime.Now - row.Posted_Date).TotalHours;
                    }
                }
                else
                {
                    if (row.Posted_Date.AddDays(2) < row.Step_Finished_Date)
                    {
                        WeekEndDayCount = Enumerable.Range(0, (int)((row.Step_Finished_Date.Date.AddDays(-1) - row.Posted_Date.Date.AddDays(1)).TotalDays) + 1).Select(n => row.Posted_Date.Date.AddDays(1).AddDays(n)).Where(x => x.DayOfWeek == DayOfWeek.Saturday || x.DayOfWeek == DayOfWeek.Sunday).Count();
                        row.Waiting_Hours = (row.Step_Finished_Date - row.Posted_Date).TotalHours - (WeekEndDayCount * 24);
                    }
                    else
                    {
                        row.Waiting_Hours = (row.Step_Finished_Date - row.Posted_Date).TotalHours;
                    }
                }

                drTimeSlots = dsTask.tbltask_history_durations.Where(x => x.Task_Update_ID == row.Task_Update_ID).ToList();
                row.Task_Hours = 0;
                foreach (DS_Tasks.tbltask_history_durationsRow drTimeSlot in drTimeSlots)
                {
                    if (drTimeSlot.IsEnd_Date_TimeNull())
                    {
                        if (drTimeSlot.Start_Date_Time.AddDays(2) < DateTime.Now)
                        {
                            WeekEndDayCount = Enumerable.Range(0, (int)((DateTime.Today.Date.Date.AddDays(-1) - drTimeSlot.Start_Date_Time.AddDays(1)).TotalDays) + 1).Select(n => drTimeSlot.Start_Date_Time.Date.AddDays(1).AddDays(n)).Where(x => x.DayOfWeek == DayOfWeek.Saturday || x.DayOfWeek == DayOfWeek.Sunday).Count();
                            row.Task_Hours = row.Task_Hours + (DateTime.Now - drTimeSlot.Start_Date_Time).TotalHours - (WeekEndDayCount * 24);
                        }
                        else
                        {
                            row.Task_Hours = row.Task_Hours + (DateTime.Now - drTimeSlot.Start_Date_Time).TotalHours;
                        }
                    }
                    else
                    {
                        if (drTimeSlot.Start_Date_Time.AddDays(2) < drTimeSlot.End_Date_Time)
                        {
                            WeekEndDayCount = Enumerable.Range(0, (int)((drTimeSlot.End_Date_Time.Date.AddDays(-1) - drTimeSlot.Start_Date_Time.AddDays(1)).TotalDays) + 1).Select(n => drTimeSlot.Start_Date_Time.Date.AddDays(1).AddDays(n)).Where(x => x.DayOfWeek == DayOfWeek.Saturday || x.DayOfWeek == DayOfWeek.Sunday).Count();
                            row.Task_Hours = row.Task_Hours + (drTimeSlot.End_Date_Time - drTimeSlot.Start_Date_Time).TotalHours - (WeekEndDayCount * 24);
                        }
                        else
                        {
                            row.Task_Hours = row.Task_Hours + (drTimeSlot.End_Date_Time - drTimeSlot.Start_Date_Time).TotalHours;
                        }
                    }
                }

                row.Waiting_Hours = row.Waiting_Hours - row.Task_Hours;

                if (row.Waiting_Hours < 0)
                {
                    row.Waiting_Hours = 0;
                }
                if (row.Task_Hours < 0)
                {
                    row.Task_Hours = 0;
                }

                TimeSpan WaitingHours = TimeSpan.FromHours(row.Waiting_Hours);
                TimeSpan TaskHours = TimeSpan.FromHours(row.Task_Hours);

                HistoryText = "<div class=\"tl-entry\">" + "\r\n" +
                                    "<div class=\"tl-time\">" + row.Posted_Date.Day + "|" + string.Format("{0:HH:mm}", row.Posted_Date) + "</div>" + "\r\n" +
                                    "<div class=\"tl-icon bg-dark-gray\">" + "\r\n" +
                                        "<i class=\"fa fa-check\"></i>" + "\r\n" +
                                    "</div>" + "\r\n" +
                                    "<div class='panel tl-body p8'>" + "\r\n" +
                                        (row.isParentRecord ? "<div class='history_parent ttip badge badge-primary' title='From Parent Task' data-placement='right'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                        UserDetails +
                                        "<div class='well well-md p8' style='margin: 0;'><b>" + row.Step_Status + "</b><br/>\r\n" +
                                        TaskUpdateFields.Replace("\r\n", "</br>") + "</div>\r\n" +
                                        "<div class='padding-sm-hr padding-xs-vr text-info'>Inactive Time : <b>" + WaitingHours.Days + " Days, " + WaitingHours.Hours + " Hrs, " + WaitingHours.Minutes + " Mns" + "</b></br>Active Time : <b>" + TaskHours.Days + " Days, " + TaskHours.Hours + " Hrs, " + TaskHours.Minutes + " Mns" + "</b></div>" + "\r\n" +
                                    "</div>" + "\r\n" +
                                "</div>" + "\r\n";
                TaskHistoryItem HistItem = new TaskHistoryItem();
                HistItem.ItemDate = row.Posted_Date;
                HistItem.ItemCode = HistoryText;
                HistList.Add(HistItem);

                TaskHistoryItem ProgressItem = new TaskHistoryItem();
                ProgressItem.ItemDate = row.Posted_Date;
                ProgressItem.ItemCode = "<div class=\"comment no-border\">" + "\r\n" +
                                            "<img src=\"" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "\" alt=\"Profile Picture\" class=\"comment-avatar\">" + "\r\n" +
                                            "<div class=\"comment-body\">" + "\r\n" +
                                                "<div class=\"comment-text\">" + "\r\n" +
                                                    "<div class=\"panel tl-body p8 \">" + "\r\n" +
                                                        (row.isParentRecord ? "<div class='history_parent ttip badge badge-primary' title='From Parent Task' data-placement='right'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                        (row.IsStep_Completed_By_NameNull() ? "" : "<h5 class=\"text-warning mt\">" + "\r\n" +
                                                            "<span class=\"text-info\">" + row.Step_Completed_By_Name + "</span>" + "\r\n" +
                                                        "</h5>" + "\r\n") +
                                                        "<div class='well well-md p8' style='margin: 0;'><b>" + row.Step_Status + "</b><br/>\r\n" +
                                                            TaskUpdateFields.Replace("\r\n", "</br>") + "\r\n" +
                                                        "</div>\r\n" +
                                                        "<div class='padding-sm-hr padding-xs-vr text-info'>Inactive Time : <b>" + WaitingHours.Days + " Days, " + WaitingHours.Hours + " Hrs, " + WaitingHours.Minutes + " Mns" + "</b></br>Active Time : <b>" + TaskHours.Days + " Days, " + TaskHours.Hours + " Hrs, " + TaskHours.Minutes + " Mns" + "</b></div>" + "\r\n" +
                                                    "</div>" + "\r\n" +
                                                "</div>" + "\r\n" +
                                                "<div class=\"comment-actions mb10\">" + "\r\n" +
                                                    "<span class=\"pull-right text-sm\">" + string.Format("{0:" + Constants.DateFormat + " HH:mm}", row.Posted_Date) + "</span>" + "\r\n" +
                                                "</div>" + "\r\n" +
                                            "</div>" + "\r\n" +
                                          "</div>" + "\r\n";
                TaskProgress.Add(ProgressItem);

                drSubs = dsSubs.tbltasks.Where(x => x.Started_History_ID == row.Task_Update_ID).OrderBy(y => y.Creator_ID).ThenBy(z => z.Task_Date).ToList();
                if (drSubs.Count > 0)
                {
                    SubTasksList = "";
                    SubTaskOwnerID = drSubs[0].Creator_ID;
                    SubTaskOwnerName = drSubs[0].Created_By;
                    SubTaskOwnerImage = drSubs[0].IsCreated_By_ImageNull() ? "assets/images/user.png" : drSubs[0].Created_By_Image;
                    SubTaskDate = drSubs[0].Task_Date;

                    foreach (DS_Tasks.tbltasksRow drSubTask in drSubs)
                    {
                        if (drSubTask.Creator_ID != SubTaskOwnerID)
                        {
                            UserDetails = "<h5 class='text-warning mt'><img class=\"rounded\" src=\"" + SubTaskOwnerImage + "\" alt=\"Profile picture\" style=\"width: 25px;height: 25px;margin-top: -2px;\">" + "\r\n" +
                                              "<span class=\"text-info\">" + SubTaskOwnerName + "</span></h5>" + "\r\n";

                            HistoryText = "<div class=\"tl-entry\">" + "\r\n" +
                                                "<div class=\"tl-time\">" + SubTaskDate.Day + "|" + string.Format("{0:HH:mm}", SubTaskDate) + "</div>" + "\r\n" +
                                                "<div class=\"tl-icon bg-dark-gray\">" + "\r\n" +
                                                    "<i class=\"fa fa-sitemap\"></i>" + "\r\n" +
                                                "</div>" + "\r\n" +
                                                "<div class='panel tl-body p8'>" + "\r\n" +
                                                    UserDetails +
                                                    "<div class='well well-md p8' style='margin: 0;'><b>Creation of (" + SubTasksList.Split(new string[] { "</br>" }, StringSplitOptions.RemoveEmptyEntries).Length + ") Sub Tasks</b><br/>\r\n" +
                                                     SubTasksList + "</div>\r\n" +
                                                "</div>" + "\r\n" +
                                            "</div>" + "\r\n";
                            TaskHistoryItem HistSubTaskItem = new TaskHistoryItem();
                            HistSubTaskItem.ItemDate = SubTaskDate;
                            HistSubTaskItem.ItemCode = HistoryText;
                            HistList.Add(HistSubTaskItem);

                            SubTasksList = "";
                            SubTaskOwnerID = drSubTask.Creator_ID;
                            SubTaskOwnerName = drSubTask.Created_By;
                            SubTaskOwnerImage = drSubTask.IsCreated_By_ImageNull() ? "assets/images/user.png" : drSubTask.Created_By_Image;
                        }
                        if (drSubTask.Current_Step_ID == -1 || drSubTask.Current_Step_ID == -2)
                        {
                            SubTasksList = SubTasksList + "<a href=\"task_info.aspx?tid=" + objURL.Encrypt(drSubTask.Task_ID.ToString()) + "\">" + drSubTask.Task_Number + " - " + drSubTask.Workflow_Name + "</a></br>";
                        }
                        else
                        {
                            if (ViewState["prev"] == null)
                            {
                                SubTasksList = SubTasksList + "<a href=\"task.aspx?tid=" + objURL.Encrypt(drSubTask.Task_ID.ToString()) + "\">" + drSubTask.Task_Number + " - " + drSubTask.Workflow_Name + "</a></br>";
                            }
                            else
                            {
                                SubTasksList = SubTasksList + "<a href=\"task.aspx?tid=" + objURL.Encrypt(drSubTask.Task_ID.ToString()) + "&bck=" + Convert.ToString(ViewState["prev"]) + "\">" + drSubTask.Task_Number + " - " + drSubTask.Workflow_Name + "</a></br>";
                            }
                        }
                        SubTaskDate = drSubTask.Task_Date;
                    }
                }

                if (SubTasksList.Trim() != "")
                {
                    UserDetails = "<h5 class='text-warning mt'><img class=\"rounded\" src=\"" + SubTaskOwnerImage + "\" alt=\"Profile picture\" style=\"width: 25px;height: 25px;margin-top: -2px;\">" + "\r\n" +
                                              "<span class=\"text-info\">" + SubTaskOwnerName + "</span></h5>" + "\r\n";

                    HistoryText = "<div class=\"tl-entry\">" + "\r\n" +
                                        "<div class=\"tl-time\">" + SubTaskDate.Day + "|" + string.Format("{0:HH:mm}", SubTaskDate) + "</div>" + "\r\n" +
                                        "<div class=\"tl-icon bg-dark-gray\">" + "\r\n" +
                                            "<i class=\"fa fa-sitemap\"></i>" + "\r\n" +
                                        "</div>" + "\r\n" +
                                        "<div class='panel tl-body p8'>" + "\r\n" +
                                            UserDetails +
                                            "<div class='well well-md p8' style='margin: 0;'><b>Creation of (" + SubTasksList.Split(new string[] { "</br>" }, StringSplitOptions.RemoveEmptyEntries).Length + ") Sub Tasks</b><br/>\r\n" +
                                             SubTasksList + "</div>\r\n" +
                                        "</div>" + "\r\n" +
                                    "</div>" + "\r\n";
                    TaskHistoryItem HistSubTaskItem = new TaskHistoryItem();
                    HistSubTaskItem.ItemDate = SubTaskDate;
                    HistSubTaskItem.ItemCode = HistoryText;
                    HistList.Add(HistSubTaskItem);
                    SubTasksList = "";
                }
            }

            ltrTaskProgress.Text = "";
            if (objSes.TaskHtryDir)
            {
                foreach (TaskHistoryItem TaskProgresItem in TaskProgress.OrderBy(x => x.ItemDate))
                {
                    ltrTaskProgress.Text = ltrTaskProgress.Text + TaskProgresItem.ItemCode;
                }
            }
            else
            {
                foreach (TaskHistoryItem TaskProgresItem in TaskProgress.OrderByDescending(x => x.ItemDate))
                {
                    ltrTaskProgress.Text = ltrTaskProgress.Text + TaskProgresItem.ItemCode;
                }
            }

            TaskProgress.Clear();
            foreach (DS_Tasks.tbltask_addonRow row in dsTask.tbltask_addon)
            {
                TaskUpdateFields = "";
                drAddonFields = dsTask.tbltask_update_addon_fields.Where(x => x.Task_Addon_ID == row.Task_Addon_ID).ToList();
                if (drAddonFields.Count > 0)
                {
                    TaskUpdateFields = objCommAct.GetTaskAddonUpdateForHistory(drAddonFields, objSes);
                }

                if (!row.IsPosted_By_NameNull())
                {
                    UserDetails = "<h5 class='text-warning mt'><img class=\"rounded\" src=\"" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "\" alt=\"Profile picture\" style=\"width: 25px;height: 25px;margin-top: -2px;\">" + "\r\n" +
                                  "<span class=\"text-info\">" + row.Posted_By_Name + "</span></h5>" + "\r\n";
                }
                else
                {
                    UserDetails = "";
                }

                HistoryText = "<div class=\"tl-entry\">" + "\r\n" +
                                    "<div class=\"tl-time\">" + row.Posted_Date.Day + "|" + string.Format("{0:HH:mm}", row.Posted_Date) + "</div>" + "\r\n" +
                                    "<div class=\"tl-icon bg-dark-gray\">" + "\r\n" +
                                        "<i class=\"fa fa-anchor\"></i>" + "\r\n" +
                                    "</div>" + "\r\n" +
                                    "<div class='panel tl-body p8'>" + "\r\n" +
                                        (row.isParentRecord ? "<div class='history_parent ttip badge badge-primary' title='From Parent Task' data-placement='right'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                        UserDetails +
                                        (AllowDeleteAddons && row.isParentRecord == false && row.Posted_By == objSes.UserID ? "<button class='ttip btn btn-danger btn-sm btn-rounded history_delete' onclick='return DeleteAddon(" + row.Task_Addon_ID + ");' title='Delete Addon' data-placement='left'><i class='fa fa-trash-o'></i></button>" + "\r\n" : "") +
                                        "<div class='well well-md p8' style='margin: 0;'><b>" + row.Addon_Name + "</b><br/>\r\n" +
                                        TaskUpdateFields.Replace("\r\n", "</br>") + "</div>\r\n" +
                                    "</div>" + "\r\n" +
                                "</div>" + "\r\n";
                TaskHistoryItem HistItem = new TaskHistoryItem();
                HistItem.ItemDate = row.Posted_Date;
                HistItem.ItemCode = HistoryText;
                HistList.Add(HistItem);

                TaskHistoryItem AddonProgressItem = new TaskHistoryItem();
                AddonProgressItem.ItemDate = row.Posted_Date;
                AddonProgressItem.ItemCode = "<div class=\"comment no-border\">" + "\r\n" +
                                            "<img src=\"" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "\" alt=\"Profile Picture\" class=\"comment-avatar\">" + "\r\n" +
                                            "<div class=\"comment-body\">" + "\r\n" +
                                                "<div class=\"comment-text\">" + "\r\n" +
                                                    "<div class=\"panel tl-body p8 \">" + "\r\n" +
                                                        (row.isParentRecord ? "<div class='history_parent ttip badge badge-primary' title='From Parent Task' data-placement='right'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                        (AllowDeleteAddons && row.isParentRecord == false && row.Posted_By == objSes.UserID ? "<button class='ttip btn btn-danger btn-sm btn-rounded history_delete' onclick='return DeleteAddon(" + row.Task_Addon_ID + ");' title='Delete Addon' data-placement='left'><i class='fa fa-trash-o'></i></button>" + "\r\n" : "") +
                                                        (row.IsPosted_By_NameNull() ? "" : "<h5 class=\"text-warning mt\">" + "\r\n" +
                                                            "<span class=\"text-info\">" + row.Posted_By_Name + "</span>" + "\r\n" +
                                                        "</h5>" + "\r\n") +
                                                        "<div class='well well-md p8' style='margin: 0;'><b>" + row.Addon_Name + "</b><br/>\r\n" +
                                                            TaskUpdateFields.Replace("\r\n", "</br>") + "\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>" + "\r\n" +
                                                "</div>" + "\r\n" +
                                                "<div class=\"comment-actions mb10\">" + "\r\n" +
                                                    "<span class=\"pull-right text-sm\">" + string.Format("{0:" + Constants.DateFormat + " HH:mm}", row.Posted_Date) + "</span>" + "\r\n" +
                                                "</div>" + "\r\n" +
                                            "</div>" + "\r\n" +
                                          "</div>" + "\r\n";

                TaskProgress.Add(AddonProgressItem);
            }

            ltrTaskAddons.Text = "";
            if (objSes.TaskHtryDir)
            {
                foreach (TaskHistoryItem TaskAddonProgresItem in TaskProgress.OrderBy(x => x.ItemDate))
                {
                    ltrTaskAddons.Text = ltrTaskAddons.Text + TaskAddonProgresItem.ItemCode;
                }
            }
            else
            {
                foreach (TaskHistoryItem TaskAddonProgresItem in TaskProgress.OrderByDescending(x => x.ItemDate))
                {
                    ltrTaskAddons.Text = ltrTaskAddons.Text + TaskAddonProgresItem.ItemCode;
                }
            }

            TimeSpan TotalWaitingHours = TimeSpan.FromHours(dsTask.tbltask_history.Sum(x => x.Waiting_Hours));
            TimeSpan TotalTaskHours = TimeSpan.FromHours(dsTask.tbltask_history.Sum(x => x.Task_Hours));
            lblDuration.Text = TotalWaitingHours.Days + " Days, " + TotalWaitingHours.Hours + " Hrs, " + TotalWaitingHours.Minutes + " Mns (Inactive)</br>" + TotalTaskHours.Days + " Days, " + TotalTaskHours.Hours + " Hrs, " + TotalTaskHours.Minutes + " Mns (Active)";

            if (objSes.UnitMins > 0)
            {
                int Units = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type).ReadUnits(dsTask.tbltasks[0].Entity_L2_ID, dsTask.tbltasks[0].Walkflow_ID);
                if (Units > 0)
                {
                    ltrUnits.Text = Math.Round(TotalTaskHours.TotalMinutes / objSes.UnitMins, 2).ToString() + " / " + Units.ToString();
                    if (TotalTaskHours.TotalMinutes / objSes.UnitMins > Units)
                    {
                        divTimeUnitNo.Attributes["class"] = "col-sm-8 text-lg text-danger";
                    }
                }
                else
                {
                    divTimeUnit.Visible = false;
                }
            }
            else
            {
                divTimeUnit.Visible = false;
            }

            foreach (DS_Tasks.tbltask_commentsRow row in dsTask.tbltask_comments)
            {
                string cssClass = row.Comment_Type.Equals("1") ? "bg-danger" : row.Comment_Type.Equals("2") ? "bg-info" : "bg-success";
                HistoryText = "<div class=\"tl-entry\">" + "\r\n" +
                                "<div class=\"tl-time\">" + row.Comment_Date.Day + "|" + string.Format("{0:HH:mm}", row.Comment_Date) + "</div>" + "\r\n" +
                                "<div class=\"tl-icon " + cssClass + "\">\r\n" +
                                     "<i class=\"fa fa-comments-o\"></i>\r\n" +
                                "</div>" + "\r\n" +
                                "<div class=\"panel tl-body p8\">\r\n" +
                                    (row.isParentRecord ? "<div class='history_parent ttip badge badge-primary' title='From Parent Task' data-placement='right'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                    (row.isChildRecord ? "<div class='history_parent ttip badge badge-info' title='From Sub Task' data-placement='right'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                    (AllowDeleteComments && row.isParentRecord == false && row.isChildRecord == false && row.Commented_By == objSes.UserID ? "<button class='ttip btn btn-danger btn-sm btn-rounded history_delete' onclick='return DeleteComment(" + row.Task_Comment_ID + ");' title='Delete Comment' data-placement='left'><i class='fa fa-trash-o'></i></button>" + "\r\n" : "") +
                                    "<h5 class='text-warning mt'>" +
                                        "<img class=\"rounded\" src=\"" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "\" alt=\"Profile picture\" style=\"width: 25px;height: 25px;margin-top: -2px;\">" + "\r\n" +
                                        "<span class=\"text-info\">" + row.Commented_By_Name + "</span>\r\n" +
                                    "</h5>" +
                                    "<div class=\"well well-md p8\" style=\"margin: 0;\">" + DecorateHashTags(row.Comment, false).Replace("\r\n", "</br>") + "</div>\r\n" +
                                "</div>" + "\r\n" +
                               "</div>" + "\r\n";
                TaskHistoryItem HistItem = new TaskHistoryItem();
                HistItem.ItemDate = row.Comment_Date;
                HistItem.ItemCode = HistoryText;
                HistList.Add(HistItem);
            }

            foreach (DS_Tasks.tbltask_api_resultsRow row in dsTask.tbltask_api_results)
            {
                HistoryText = "<div class=\"tl-entry\">\r\n" +
                                "<div class=\"tl-time\">" + row.API_Call_Date.Day + "|" + string.Format("{0:HH:mm}", row.API_Call_Date) + "</div>\r\n" +
                                "<div class=\"tl-icon bg-dark-gray\">\r\n" +
                                     "<i class=\"fa fa-bolt\"></i>\r\n" +
                                "</div>\r\n" +
                                "<div class=\"panel tl-body p8\">\r\n" +
                                    (row.isParentRecord ? "<div class='history_parent ttip badge badge-primary' title='From Parent Task' data-placement='right'><i class='fa fa-exclamation'></i></div>\r\n" : "") +
                                   "<h5 class='text-warning mt'>" +
                                      "<img class=\"rounded\" src=\"" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "\" alt=\"Profile picture\" style=\"width: 25px;height: 25px;margin-top: -2px;\">\r\n" +
                                      "<span class=\"text-info\">" + row.Called_By_Name + "</span>\r\n" +
                                   "</h5>" +
                                   "<div class='well well-md p8' style='margin: 0;'>\r\n" +
                                      "<b>API Called : " + row.API_Call_Name + "</b><br/><span class='text-danger'>Result : </span>" + row.API_Call_Result.Replace("\r\n", "</br>") + "\r\n" +
                                   "</div>\r\n" +
                                "</div>\r\n" +
                               "</div>\r\n";
                TaskHistoryItem HistItem = new TaskHistoryItem();
                HistItem.ItemDate = row.API_Call_Date;
                HistItem.ItemCode = HistoryText;
                HistList.Add(HistItem);
            }

            int MonthNo = 0;
            int YearNo = 0;

            ltrHistory.Text = "";

            List<TaskHistoryItem> Hist;

            if (objSes.TaskHtryDir)
            {
                Hist = HistList.OrderBy(x => x.ItemDate).ToList();
            }
            else
            {
                Hist = HistList.OrderByDescending(x => x.ItemDate).ToList();
            }

            foreach (TaskHistoryItem HistItm in Hist)
            {
                if (MonthNo != HistItm.ItemDate.Month || YearNo != HistItm.ItemDate.Year)
                {
                    ltrHistory.Text = ltrHistory.Text + "<div class=\"tl-header now bg-primary\">" + "\r\n" +
                                                         objCom.getMonth(HistItm.ItemDate.Month) + " " + HistItm.ItemDate.Year + "\r\n" +
                                                        "</div>" + "\r\n";
                    MonthNo = HistItm.ItemDate.Month;
                    YearNo = HistItm.ItemDate.Year;
                }
                ltrHistory.Text = ltrHistory.Text + HistItm.ItemCode;
            }

            LoadTaskComents(dsTask.tbltask_comments, objSes);
        }

        private void LoadTaskComents(DS_Tasks.tbltask_commentsDataTable tbl, SessionObject objSes)
        {
            Users objUser = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
            bool AllowDeleteComments = objUser.isAllowed(AllowedAreas.Delete_Comments, objSes.UserID);
            if (AllowDeleteComments == true)
            {
                AllowDeleteComments = Convert.ToBoolean(ViewState["dc"]);
            }

            string strCommentsOnly = "";
            if (tbl.Rows.Count > 0)
            {
                string CurrCommType = string.Empty;
                List<DS_Tasks.tbltask_commentsRow> Coms;
                if (objSes.TaskHtryDir)
                {
                    Coms = tbl.OrderBy(x => x.Comment_Category).ThenBy(y => y.Comment_Date).ToList();
                }
                else
                {
                    Coms = tbl.OrderBy(x => x.Comment_Category).ThenByDescending(y => y.Comment_Date).ToList();
                }
                foreach (DS_Tasks.tbltask_commentsRow row in Coms)
                {
                    if (row.Comment_Category.Trim() != CurrCommType)
                    {
                        strCommentsOnly = strCommentsOnly + "<div class='comment no-padding-b  no-border'>" + "\r\n" +
                                                                    " <div class='comment-body no-margin-hr'>" +
                                                                    "   <div class='comment-text'>" +
                                                                    "    <b>" + row.Comment_Category + "</b>" +
                                                                    "   </div>" + " </div>\r\n" +
                                                                    "</div>";
                        CurrCommType = row.Comment_Category.Trim();
                    }
                    string cssClass = row.Comment_Type.Equals("1") ? "timeline-label_p1" : row.Comment_Type.Equals("2") ? "timeline-label_p2" : "";
                    strCommentsOnly = strCommentsOnly + "<div class='comment no-border'>" + "\r\n" +
                                                                " <img src='" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "' alt='Profile Picture' class='comment-avatar'>" + "\r\n" +
                                                                " <div class='comment-body'>" +
                                                                "   <div class='comment-text'>" +
                                                                "       <div class=\"panel tl-body p8 \">\r\n" +
                                                                            (row.isParentRecord ? "<div class='history_parent ttip badge badge-primary' title='From Parent Task' data-placement='right'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                                            (row.isChildRecord ? "<div class='history_parent ttip badge badge-info' title='From Sub Task' data-placement='right'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                                            (AllowDeleteComments && row.isParentRecord == false && row.isChildRecord == false && row.Commented_By == objSes.UserID ? "<button class='ttip btn btn-danger btn-sm btn-rounded history_delete' onclick='return DeleteComment(" + row.Task_Comment_ID + ");' title='Delete Comment' data-placement='left'><i class='fa fa-trash-o'></i></button>" + "\r\n" : "") +
                                                                "           <h5 class='text-warning mt'>" +
                                                                "               <span class=\"text-info\">" + row.Commented_By_Name + "</span>\r\n" +
                                                                "           </h5>" +
                                                                "           <div class=\"well well-md p8 " + cssClass + "\" style=\"margin: 6px 0 0 0;\">" + DecorateHashTags(row.Comment, true).Replace("\r\n", "</br>") + "</div>\r\n" +
                                                                "       </div>" + "\r\n" +
                                                                "   </div>" +
                                                                "   <div class='comment-actions'>" +
                                                                "       <span class='pull-right text-sm'>" + string.Format("{0:" + Constants.DateFormat + " HH:mm}", row.Comment_Date) + "</span>" + "\r\n" +
                                                                "   </div>\r\n" +
                                                                " </div>\r\n" +
                                                                "</div>";
                }
            }
            divCommentsOnly.InnerHtml = strCommentsOnly;
            objHashTagsList.Sort();
            string Tag = "";
            cboTags.Items.Clear();
            foreach (string str in objHashTagsList)
            {
                if (Tag != str)
                {
                    cboTags.Items.Add(new ListItem(str, str));
                    Tag = str;
                }
            }
            cboTags.Items.Insert(0, new ListItem("[All Comments]", "0"));
        }

        private string DecorateHashTags(string InputText, bool FillTagsDropDown)
        {
            string[] splits = InputText.Split('#');
            string outputText = "";
            int EndIndex = 0;
            char[] SearchEndFor = { ' ', ',', '-', ':', ';' };
            if (splits.Length > 1)
            {
                for (int i = 0; i < splits.Length; i++)
                {
                    if (i > 0)
                    {
                        EndIndex = splits[i].IndexOfAny(SearchEndFor);
                        if (EndIndex > 0)
                        {
                            if (FillTagsDropDown == true)
                            {
                                if (!objHashTagsList.Contains("#" + splits[i].Substring(0, EndIndex)))
                                {
                                    objHashTagsList.Add("#" + splits[i].Substring(0, EndIndex));
                                }
                            }
                            splits[i] = splits[i].Substring(0, EndIndex) + "</span>" + splits[i].Substring(EndIndex);
                        }
                        else
                        {
                            if (FillTagsDropDown == true)
                            {
                                if (!objHashTagsList.Contains(objHashTagsList.Add("#" + splits[i])))
                                {
                                    objHashTagsList.Add("#" + splits[i]);
                                }
                            }
                            splits[i] = splits[i] + "</span>";
                        }
                        splits[i] = "<span class='text-info'>#" + splits[i];
                    }
                    outputText = outputText + splits[i];
                }
            }
            else
            {
                outputText = InputText;
            }
            return outputText;
        }

        private void LoadTaskTimelineChart(DS_Tasks dsCurrentTask, SessionObject objSes)
        {
            StepListPanelResizeScript = "AdjustPopupSize(80, 600, 'at_model_stli');";
            ChartLoadScript = "Morris.Line({\r\n" +
                                    "element: 'progress_chart',\r\n" +
                                    "data: task_data,\r\n" +
                                    "xkey: 't_date',\r\n" +
                                    "ykeys: ['forecasted_val', 'actual_val'],\r\n" +
                                    "labels: ['Forecast', 'Actual'],\r\n" +
                                    "hideHover: 'auto',\r\n" +
                                    "lineColors: ['#075181', '#5bb0e8'],\r\n" +
                                    "fillOpacity: 0.2,\r\n" +
                                    "behaveLikeLine: true,\r\n" +
                                    "lineWidth: 1,\r\n" +
                                    "pointSize: 2,\r\n" +
                                    "gridLineColor: '#cfcfcf',\r\n" +
                                    "xLabels: 'day',\r\n" +
                                    "resize: true\r\n" +
                                "});";

            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow dsWFMain = objWF.Read(dsCurrentTask.tbltasks[0].Walkflow_ID, false, false);

            DS_Workflow.tblworkflow_stepsRow drCurrentStep = dsWFMain.tblworkflow_steps.Where(x => x.Workflow_Step_ID == dsCurrentTask.tbltasks[0].Current_Step_ID).FirstOrDefault();
            DS_Workflow.tblworkflow_stepsRow drLastStep = dsWFMain.tblworkflow_steps.Where(x => x.Sort_order >= drCurrentStep.Sort_order && x.Is_Last_Step == true).FirstOrDefault();

            if (drLastStep == null)
            {
                drLastStep = dsWFMain.tblworkflow_steps[dsWFMain.tblworkflow_steps.Rows.Count - 1];
            }

            DS_Workflow dsWF = new DS_Workflow();
            DS_Workflow.tblworkflow_stepsRow drWFStep;

            foreach (DS_Workflow.tblworkflow_stepsRow rowWF in dsWFMain.tblworkflow_steps)
            {
                if (dsCurrentTask.tbltask_timeline.Where(x => x.Workflow_Step_ID == rowWF.Workflow_Step_ID).Count() > 0 || rowWF.Workflow_Step_ID == dsCurrentTask.tbltasks[0].Current_Step_ID)
                {
                    if (rowWF.Sort_order <= drLastStep.Sort_order)
                    {
                        if (rowWF.Sort_order < drCurrentStep.Sort_order)
                        {
                            if (dsCurrentTask.tbltask_history.Where(x => x.Workflow_Step_ID == rowWF.Workflow_Step_ID).Count() > 0)
                            {
                                drWFStep = dsWF.tblworkflow_steps.Newtblworkflow_stepsRow();
                                drWFStep.ItemArray = rowWF.ItemArray;
                                dsWF.tblworkflow_steps.Rows.Add(drWFStep);
                            }
                        }
                        else
                        {
                            drWFStep = dsWF.tblworkflow_steps.Newtblworkflow_stepsRow();
                            drWFStep.ItemArray = rowWF.ItemArray;
                            dsWF.tblworkflow_steps.Rows.Add(drWFStep);
                        }
                    }
                }
            }

            int TotalStepWeight = dsWF.tblworkflow_steps.Sum(x => x.Step_Weight);
            int CurrentStepWeight = 0;
            for (int i = 0; i < dsWF.tblworkflow_steps.Rows.Count; i++)
            {
                if (dsWF.tblworkflow_steps[i].Workflow_Step_ID == dsCurrentTask.tbltasks[0].Current_Step_ID)
                {
                    break;
                }
                CurrentStepWeight += dsWF.tblworkflow_steps[i].Step_Weight;
            }

            dsCurrentTask.tbltasks[0].Progress_Value = (int)((double)((double)CurrentStepWeight / (double)TotalStepWeight * (double)100));

            divProgress.Attributes["style"] = "width: " + dsCurrentTask.tbltasks[0].Progress_Value + "%";
            if (dsCurrentTask.tbltasks[0].IsETB_ValueNull() == false && dsCurrentTask.tbltasks[0].Hours_Before_Conclusion > 0 && dsCurrentTask.tbltasks[0].ETB_Value.AddHours(-(dsCurrentTask.tbltasks[0].Hours_Before_Conclusion)).Date <= DateTime.Today.Date)
            {
                divProgress.Attributes["class"] = "progress-bar progress-bar-danger";
            }
            else if (dsCurrentTask.tbltasks[0].IsETB_ValueNull() == false && dsCurrentTask.tbltasks[0].ETB_Value < DateTime.Now)
            {
                divProgress.Attributes["class"] = "progress-bar progress-bar-danger";
            }
            else
            {
                divProgress.Attributes["class"] = "progress-bar progress-bar-success";
            }

            DateTime FromDate = dsCurrentTask.tbltasks[0].Task_Date;
            DateTime ToDate = dsCurrentTask.tbltasks[0].ETB_Value;

            DateTime dtTLMax = dsCurrentTask.tbltask_timeline.Select(x => x.Step_Deadline).Max();
            if (dtTLMax > ToDate)
            {
                ToDate = dtTLMax;
            }

            DateTime dtPostMax = dsCurrentTask.tbltask_history[dsCurrentTask.tbltask_history.Rows.Count - 1].Posted_Date;
            if (dtPostMax > ToDate)
            {
                ToDate = dtPostMax;
            }

            if (ToDate < DateTime.Now)
            {
                ToDate = DateTime.Now;
            }

            for (int i = 0; i < dsCurrentTask.tbltask_timeline.Count; i++)
            {
                dsCurrentTask.tbltask_timeline[i].Step_Index = i + 1;
            }

            DS_Tasks.tbltask_timelineRow drTL = dsCurrentTask.tbltask_timeline.Newtbltask_timelineRow();
            drTL.Step_Index = 0;
            drTL.Step_Status = "Start";
            drTL.Step_Deadline = FromDate.Date;
            drTL.Workflow_Step_ID = 0;
            dsCurrentTask.tbltask_timeline.Rows.InsertAt(drTL, 0);

            string Forecasted_Value = null;
            string Actual_Value = null;

            ChartScript = "var task_data = [" + "\r\n";

            for (DateTime day = FromDate.Date; day.Date <= ToDate.Date; day = day.AddDays(1))
            {
                Forecasted_Value = null;
                List<DS_Tasks.tbltask_timelineRow> drStep = dsCurrentTask.tbltask_timeline.Where(x => x.Step_Deadline.Date == day).OrderByDescending(y => y.Step_Index).ToList();
                if (drStep.Count > 0)
                {
                    Forecasted_Value = drStep[0].Step_Index.ToString();
                }

                Actual_Value = null;
                List<DS_Tasks.tbltask_historyRow> drTH = dsCurrentTask.tbltask_history.Where(x => !x.IsStep_Finished_DateNull() && x.Step_Finished_Date.Date == day).OrderByDescending(y => y.Task_Update_ID).ToList();
                if (drTH.Count > 0)
                {
                    foreach (DS_Tasks.tbltask_historyRow row in drTH)
                    {
                        drStep = dsCurrentTask.tbltask_timeline.Where(x => x.Workflow_Step_ID == row.Workflow_Step_ID).ToList();
                        if (drStep.Count > 0)
                        {
                            Actual_Value = drStep[0].Step_Index.ToString();
                            break;
                        }
                    }
                }
                if (Actual_Value == null && FromDate.Date == day.Date)
                {
                    Actual_Value = "0";
                }

                ChartScript = ChartScript + "{ t_date: '" + day.ToString("yyyy-MM-dd") + "', forecasted_val: " + (Forecasted_Value == null ? "null" : Forecasted_Value) + ", actual_val: " + (Actual_Value == null ? "null" : Actual_Value) + " }";
                if (day.Date == ToDate.Date)
                {
                    ChartScript = ChartScript + "\r\n";
                }
                else
                {
                    ChartScript = ChartScript + "," + "\r\n";
                }
            }

            ChartScript = ChartScript + "];";

            LoadStepsList(dsCurrentTask.tbltask_timeline);
        }

        private void LoadStepsList(DS_Tasks.tbltask_timelineDataTable tblTL)
        {
            pnlStepsListBody.Controls.Clear();

            HtmlGenericControl divTableControl = new HtmlGenericControl("div");
            divTableControl.Attributes.Add("class", "table-responsive no-margin-b");
            Table tblSteps = new Table();
            tblSteps.CssClass = "table table-striped table-hover grid_table mb";

            TableHeaderRow tblHead = new TableHeaderRow();
            tblHead.TableSection = TableRowSection.TableHeader;
            TableHeaderCell thIndex = new TableHeaderCell();
            thIndex.Width = 70;
            thIndex.Text = "Index";
            tblHead.Cells.Add(thIndex);

            TableHeaderCell thStep = new TableHeaderCell();
            thStep.Text = "Step";
            tblHead.Cells.Add(thStep);

            tblSteps.Rows.Add(tblHead);

            foreach (DS_Tasks.tbltask_timelineRow row in tblTL)
            {
                TableRow tblRow = new TableRow();
                tblRow.TableSection = TableRowSection.TableBody;

                TableCell tdIndex = new TableCell();
                tdIndex.Width = 70;
                tdIndex.Text = row.Step_Index.ToString();
                tblRow.Cells.Add(tdIndex);

                TableCell tdStep = new TableCell();
                tdStep.Text = row.Step_Status;
                tblRow.Cells.Add(tdStep);

                tblSteps.Rows.Add(tblRow);
            }

            divTableControl.Controls.Add(tblSteps);

            pnlStepsListBody.Controls.Add(divTableControl);
        }

        private bool CheckForDuplicateCodes(DS_Workflow dsSWF, List<string> codes, Tasks objTask, int Parent_Task_ID)
        {
            bool ret = true;

            if (dsSWF.tblwalkflow[0].Extra_Field_Task_Start == true && dsSWF.tblwalkflow[0].Exrta_Field_Naming.Trim() != "")
            {
                if (codes.Count > 0)
                {
                    string DuplicateText = objTask.IsExtraFieldValueDuplicatedInSubTasks(Parent_Task_ID, codes.ToArray());
                    if (!DuplicateText.Equals("-1"))
                    {
                        ret = false;
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey35", "ShowError('" + "Following " + dsSWF.tblwalkflow[0].Exrta_Field_Naming.Trim() + "(s) are already in the System.<br/>" + DuplicateText + "');", true);
                    }
                }
                else
                {
                    ret = false;
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey35", "ShowError('" + dsSWF.tblwalkflow[0].Exrta_Field_Naming.Trim() + "' is Required);", true);
                }
            }
            return ret;
        }

        private bool UserAllowedToCreateTasks(DS_Workflow dsSWF, Tasks objTask, SessionObject objSes)
        {
            bool ret = true;
            if (dsSWF.tblworkflow_steps.Rows.Count > 0)
            {
                int CurrentTaskCount = objTask.CurrentTaskCount(objSes.UserID, dsSWF.tblwalkflow[0].Walkflow_ID);
                if (CurrentTaskCount >= dsSWF.tblwalkflow[0].Number_Of_Concurrent_Tasks_Allowed)
                {
                    Users objUser = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
                    DS_Users dsUser = objUser.Read(objSes.UserID);
                    if (dsUser.tblusers[0].Override_Restrictions == false)
                    {
                        ret = false;
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey2", "ShowError('You have exceeded the number of tasks allowed from a single workflow');", true);
                    }
                    else
                    {
                        objUser.DisableOverride(objSes.UserID);
                    }
                }
            }
            else
            {
                ret = false;
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey2", "ShowError('Selected Workflow does not have any Steps';", true);
            }
            return ret;
        }

        protected void gvDocumentsFind_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                if (e.Row.Cells[1].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdEdit = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[1].Controls[1]);
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndLinkDocID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; saveLink(); return false;");
                }
            }
        }

        protected void cmdRejectConfirm_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            DS_Tasks dsCurrentTask = objTask.Read(Task_ID, false, false, false);

            try
            {
                bool isMultiPost = false;
                if (ValidateTaskPost(Task_ID, objCtrlList.Task_ID, Convert.ToInt32(ViewState["csid"]), dsCurrentTask.tbltasks[0].Current_Step_ID, ref isMultiPost, objSes))
                {
                    Common_Task_Actions objComAct = new Common_Task_Actions();
                    objComAct.PrepareControlsDataSet(dsCurrentTask, objSes, true, ref objCtrlList);

                    SaveStep(dsCurrentTask, objTask, objCtrlList, objSes.UserID, isMultiPost, objSes);

                    if (isMultiPost == false)
                    {
                        Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
                        N_Ter.Customizable.Master_Tables objMasters = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);

                        DS_Tasks.tbltask_historyRow[] drLastTaskHistory = dsCurrentTask.tbltask_history.OrderByDescending(x => x.Task_Update_ID).ToArray();
                        objTask.UpdateStepFinishedDetails(drLastTaskHistory[0].Task_Update_ID, objSes.UserID, true);

                        Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                        DS_Workflow dsWF = objWF.Read(dsCurrentTask.tbltasks[0].Walkflow_ID, false, false);

                        int StepID = objWF.ReadLastWorkflowStep(Task_ID);

                        objTask.UpdateTaskHistory(Task_ID, StepID, objSes.UserID, objSes.FullName, false, objCus, objMasters);
                    }
                    else
                    {
                        objTask.Update_Task_Lock(Task_ID, 0, 0);
                    }
                    ValidateNextPage(false, objSes);
                }
            }
            catch (Exception ex)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey3", "ShowError('" + ex.Message + "');", true);
            }
        }

        protected void cmdSubmitSpecial_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            ActionValidated objAct = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostValidations(Convert.ToInt32(ViewState["tid"]), Convert.ToInt32(ViewState["wid"]), Convert.ToInt32(ViewState["csid"]), objCtrlList, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            if (objAct.Validated == true)
            {
                if (SaveEL2(objSes) == true)
                {
                    try
                    {
                        DirectSubmit(objCtrlList, objSes);
                        ValidateNextPage(false, objSes);
                    }
                    catch (Exception ex)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey4", "ShowError('" + ex.Message + "');", true);
                    }
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey4", "ShowError('" + objAct.Reason + "');", true);
            }
        }

        protected void cmdContinueSpecial_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            ActionValidated objAct = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostValidations(Convert.ToInt32(ViewState["tid"]), Convert.ToInt32(ViewState["wid"]), Convert.ToInt32(ViewState["csid"]), objCtrlList, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            if (objAct.Validated == true)
            {
                if (SaveEL2(objSes) == true)
                {
                    try
                    {
                        DirectSubmit(objCtrlList, objSes);
                        ValidateNextPage(false, objSes);
                    }
                    catch (Exception ex)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey5", "ShowError('" + ex.Message + "');", true);
                    }
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey5", "ShowError('" + objAct.Reason + "');", true);
            }
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            ActionValidated objAct = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostValidations(Convert.ToInt32(ViewState["tid"]), Convert.ToInt32(ViewState["wid"]), Convert.ToInt32(ViewState["csid"]), objCtrlList, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            if (objAct.Validated == true)
            {
                if (SaveEL2(objSes) == true)
                {
                    try
                    {
                        NextStepSubmit(objCtrlList, objSes);
                        ValidateNextPage(false, objSes);
                    }
                    catch (Exception ex)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey6", "ShowError('" + ex.Message + "');", true);
                    }
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey6", "ShowError('" + objAct.Reason + "');", true);
            }
        }

        protected void cmdGotoTaskList_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            ValidateNextPage(false, objSes);
        }

        protected void cmdCloseMain_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            ActionValidated objAct = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostValidations(Convert.ToInt32(ViewState["tid"]), Convert.ToInt32(ViewState["wid"]), Convert.ToInt32(ViewState["csid"]), objCtrlList, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            if (objAct.Validated == true)
            {
                if (SaveEL2(objSes))
                {
                    try
                    {
                        NextStepSubmit(objCtrlList, objSes);
                        ValidateNextPage(false, objSes);
                    }
                    catch (Exception ex)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey7", "ShowError('" + ex.Message + "');", true);
                    }
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey7", "ShowError('" + objAct.Reason + "');", true);
            }
        }

        protected void cmdContinue_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            ActionValidated objAct = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostValidations(Convert.ToInt32(ViewState["tid"]), Convert.ToInt32(ViewState["wid"]), Convert.ToInt32(ViewState["csid"]), objCtrlList, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            if (objAct.Validated == true)
            {
                if (SaveEL2(objSes))
                {
                    try
                    {
                        NextStepSubmit(objCtrlList, objSes);
                        ValidateNextPage(false, objSes);
                    }
                    catch (Exception ex)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey8", "ShowError('" + ex.Message + "');", true);
                    }
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey8", "ShowError('" + objAct.Reason + "');", true);
            }
        }

        protected void cmdInitiateSubTask_Click(object sender, EventArgs e)
        {
            int SubWF_ID = Convert.ToInt32(cboSubWorkflow.SelectedItem.Value);
            SessionObject objSes = (SessionObject)Session["dt"];

            if (SaveEL2(objSes))
            {
                Workflow objWorkflow = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                DS_Workflow dsSWF = objWorkflow.Read(SubWF_ID, false, false);

                List<string> strExtraField1 = new List<string>();
                List<string> strExtraField2 = new List<string>();
                int ExtraField_ID = 0;
                int ExtraField2_ID = 0;

                if (dsSWF.tblwalkflow[0].Extra_Field_Task_Start == true && dsSWF.tblwalkflow[0].Exrta_Field_Naming.Trim() != "")
                {
                    strExtraField1 = txtSubTaskExtraFieldValue1.Text.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    strExtraField1 = strExtraField1.Where(x => x.Trim() != "").ToList();
                }
                if (dsSWF.tblwalkflow[0].Extra_Field2_Task_Start == true && dsSWF.tblwalkflow[0].Exrta_Field2_Naming.Trim() != "")
                {
                    strExtraField2 = txtSubTaskExtraFieldValue2.Text.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    strExtraField2 = strExtraField2.Where(x => x.Trim() != "").ToList();
                }

                int NumberOfTasks = Convert.ToInt32(txtNoSubOfTasks.Text);

                Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                int Parent_Task_ID = Convert.ToInt32(ViewState["tid"]);
                if (UserAllowedToCreateTasks(dsSWF, objTask, objSes))
                {
                    try
                    {
                        DS_Tasks dsParentTask = objTask.Read(Parent_Task_ID, false, false, false);

                        DateTime DueDate = DateTime.Today.Date;
                        bool CreateTask = true;

                        Common_Actions objCom = new Common_Actions();

                        if (dsParentTask.tbltasks[0].Schedule_Line_ID > 0)
                        {
                            Workflow_Schedules objWFS = ObjectCreator.GetWorkflow_Schedules(objSes.Connection, objSes.DB_Type);
                            DS_Workflow_Scedules.tblworkflow_schedules_dataRow drLine = objWFS.ReadScheduleDataLine(dsParentTask.tbltasks[0].Schedule_Line_ID);
                            DueDate = drLine.Date_Data;
                        }
                        else if (objCom.ValidateDateTime(lblDueDate.Text.Trim() == "" ? string.Format("{0:" + Constants.DateFormat + " HH:mm}", DateTime.Now) : lblDueDate.Text, ref DueDate) == false)
                        {
                            CreateTask = false;
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey9", "ShowError('Invalid Due Date');", true);
                        }
                        if (CreateTask == true)
                        {
                            List<DS_Master_Tables.tblDataRow> drM;

                            DS_Tasks.tbltask_historyRow drLastTaskHistory = dsParentTask.tbltask_history.OrderByDescending(x => x.Task_Update_ID).First();

                            string strCode1 = "";
                            string strCode2 = "";
                            string NewTaskNumber = "";
                            int SubTaskNumber = objTask.ReadLastSubTaskNumber(Parent_Task_ID);

                            Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
                            N_Ter.Customizable.Master_Tables objMasters = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);

                            DS_Master_Tables dsExMaster = null;
                            if (dsSWF.tblwalkflow[0].Exrta_Field_Type == 3 && dsSWF.tblwalkflow[0].Extra_Field_Master_Table_ID > 0)
                            {
                                dsExMaster = objMasters.GetMasterTable(dsSWF.tblwalkflow[0].Extra_Field_Master_Table_ID);
                            }

                            DS_Master_Tables dsExMaster2 = null;
                            if (dsSWF.tblwalkflow[0].Exrta_Field2_Type == 3 && dsSWF.tblwalkflow[0].Extra_Field2_Master_Table_ID > 0)
                            {
                                dsExMaster2 = objMasters.GetMasterTable(dsSWF.tblwalkflow[0].Extra_Field2_Master_Table_ID);
                            }

                            int Current_Step_ID = dsSWF.tblworkflow_steps[0].Workflow_Step_ID;
                            string AditionalComment = "";
                            int User_ID = objSes.UserID;

                            for (int i = SubTaskNumber; i < Convert.ToInt32(txtNoSubOfTasks.Text) + SubTaskNumber; i++)
                            {
                                strCode1 = "";
                                strCode2 = "";
                                ExtraField_ID = 0;
                                ExtraField2_ID = 0;

                                NewTaskNumber = dsParentTask.tbltasks[0].Task_Number + "_" + (i + 1);
                                if (i - SubTaskNumber < strExtraField1.Count && strExtraField1[i - SubTaskNumber] != null)
                                {
                                    strCode1 = strExtraField1[i - SubTaskNumber];
                                }
                                else if (strExtraField1.Count > 0)
                                {
                                    strCode1 = strExtraField1[strExtraField1.Count - 1];
                                }
                                else
                                {
                                    strCode1 = "";
                                }

                                if (i - SubTaskNumber < strExtraField2.Count && strExtraField2[i - SubTaskNumber] != null)
                                {
                                    strCode2 = strExtraField2[i - SubTaskNumber];
                                }
                                else if (strExtraField2.Count > 0)
                                {
                                    strCode2 = strExtraField2[strExtraField2.Count - 1];
                                }
                                else
                                {
                                    strCode2 = "";
                                }

                                List<StepPostData> InitStepPosts = new List<StepPostData>();

                                ActionValidated Act = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskStartValidations(ref User_ID, ref SubWF_ID, ref Current_Step_ID, ref strCode1, ref strCode2, ref AditionalComment, ref InitStepPosts, dsParentTask.tbltasks[0].Entity_L2_ID, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

                                if (Act.Validated == true)
                                {
                                    bool CustomValidationsOK = true;
                                    if (SubWF_ID != dsSWF.tblwalkflow[0].Walkflow_ID)
                                    {
                                        DS_Workflow dsSWFTemp = objWorkflow.Read(SubWF_ID, false, false);
                                        if (dsSWFTemp.tblwalkflow.Rows.Count > 0)
                                        {
                                            if (dsSWFTemp.tblwalkflow[0].Exrta_Field_Type == 3 && dsSWFTemp.tblwalkflow[0].Extra_Field_Master_Table_ID > 0)
                                            {
                                                DS_Master_Tables dsExMasterTemp = objMasters.GetMasterTable(dsSWFTemp.tblwalkflow[0].Extra_Field_Master_Table_ID);
                                                drM = dsExMasterTemp.tblData.Where(x => x.Data.Trim() == strCode1.Trim()).ToList();
                                                if (drM.Count > 0)
                                                {
                                                    ExtraField_ID = drM[0].Data_ID;
                                                }
                                            }

                                            if (dsSWFTemp.tblwalkflow[0].Exrta_Field2_Type == 3 && dsSWFTemp.tblwalkflow[0].Extra_Field2_Master_Table_ID > 0)
                                            {
                                                DS_Master_Tables dsExMaster2Temp = objMasters.GetMasterTable(dsSWFTemp.tblwalkflow[0].Extra_Field2_Master_Table_ID);
                                                drM = dsExMaster2Temp.tblData.Where(x => x.Data.Trim() == strCode2.Trim()).ToList();
                                                if (drM.Count > 0)
                                                {
                                                    ExtraField2_ID = drM[0].Data_ID;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            CustomValidationsOK = false;
                                        }
                                    }
                                    else
                                    {
                                        if (dsSWF.tblwalkflow[0].Exrta_Field_Type == 3 && dsSWF.tblwalkflow[0].Extra_Field_Master_Table_ID > 0)
                                        {
                                            drM = dsExMaster.tblData.Where(x => x.Data.Trim() == strCode1.Trim()).ToList();
                                            if (drM.Count > 0)
                                            {
                                                ExtraField_ID = drM[0].Data_ID;
                                            }
                                        }

                                        if (dsSWF.tblwalkflow[0].Exrta_Field2_Type == 3 && dsSWF.tblwalkflow[0].Extra_Field2_Master_Table_ID > 0)
                                        {
                                            drM = dsExMaster2.tblData.Where(x => x.Data.Trim() == strCode2.Trim()).ToList();
                                            if (drM.Count > 0)
                                            {
                                                ExtraField2_ID = drM[0].Data_ID;
                                            }
                                        }
                                    }
                                    if (CustomValidationsOK)
                                    {
                                        int MaxID = objTask.InsertSub(NewTaskNumber, SubWF_ID, User_ID, Current_Step_ID, Parent_Task_ID, drLastTaskHistory.Task_Update_ID, DueDate, dsParentTask.tbltasks[0].Entity_L2_ID, dsParentTask.tbltasks[0].Schedule_Line_ID, strCode1, strCode2, ExtraField_ID, ExtraField2_ID, objSes.FullName, 0, dsParentTask.tbltasks[0].Queue_ID, objCus, objMasters);
                                        if (Convert.ToInt32(hndAttachDoc_ID.Value) > 0)
                                        {
                                            objTask.AddLink(MaxID, Convert.ToInt32(hndAttachDoc_ID.Value), objSes.UserID, false);
                                        }

                                        if (AditionalComment.Trim() != "")
                                        {
                                            objTask.AddComment(MaxID, 1, AditionalComment, "3", "Startup Validations");
                                        }

                                        if (InitStepPosts.Count > 0)
                                        {
                                            Common_Task_Actions objComTsk = new Common_Task_Actions();
                                            foreach (StepPostData PostData in InitStepPosts)
                                            {
                                                if (PostData.Queue_ID > 0)
                                                {
                                                    objTask.UpdateTaskQueue(MaxID, PostData.Queue_ID);
                                                }
                                                if (PostData.Next_Step_ID > 0)
                                                {
                                                    objComTsk.SaveStep(MaxID, PostData.TaskFields, PostData.TaskExtraFields, PostData.TaskDocActions, objTask, objSes, objCus);
                                                    objTask.UpdateTaskHistory(MaxID, PostData.Next_Step_ID, objSes.UserID, objSes.FullName, true, objCus, objMasters);
                                                }
                                                if (PostData.TaskTempSave.Count > 0)
                                                {
                                                    foreach (Task_Field_Temp_Save TempSave in PostData.TaskTempSave)
                                                    {
                                                        objTask.SaveTaskUpdate(MaxID, TempSave.Workflow_Step_Field_ID, TempSave.Field_Value);
                                                    }
                                                }
                                            }
                                        }

                                        DS_Tasks dsNewTask = objTask.Read(MaxID, false, false, false);
                                        if (dsNewTask.tbltasks[0].IsIs_Auto_SubmitNull() == false && dsNewTask.tbltasks[0].Is_Auto_Submit)
                                        {
                                            Common_Task_Actions objCommAct = new Common_Task_Actions();
                                            while (dsNewTask.tbltasks[0].IsIs_Auto_SubmitNull() == false && dsNewTask.tbltasks[0].Is_Auto_Submit)
                                            {
                                                if (objCommAct.AutoSubmit(dsNewTask, objTask, objWorkflow, objCus, objMasters, objSes.UserID, objSes.FullName))
                                                {
                                                    dsNewTask = objTask.Read(MaxID, false, false, false);
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
                                        ClientScriptManager cs = Page.ClientScript;
                                        cs.RegisterStartupScript(this.GetType(), "AKey10" + i.ToString(), "ShowError('Custom Validaions Failed');", true);
                                    }
                                }
                                else
                                {
                                    ClientScriptManager cs = Page.ClientScript;
                                    cs.RegisterStartupScript(this.GetType(), "AKey10" + i.ToString(), "ShowError('" + Act.Reason + "');", true);
                                }
                            }

                            if (hndSubmitType.Value == "1")
                            {
                                ValidateNextPage(false, objSes);
                            }
                            else
                            {
                                DS_Tasks dsSubs = objTask.GetActiveSubTasks(Parent_Task_ID);
                                LoadSubTasks(dsSubs, Parent_Task_ID, dsParentTask.tbltasks[0].Task_Number + " - " + dsParentTask.tbltasks[0].Workflow_Name + " - " + dsParentTask.tbltasks[0].Display_Name);

                                DS_Workflow dsWF = objWorkflow.Read(dsParentTask.tbltasks[0].Walkflow_ID, false, false);
                                DS_Tasks dsTask = null;
                                FillTaskData(Parent_Task_ID, ref dsTask, dsWF, objTask);

                                Script_Generator objScript = new Script_Generator();

                                LoadHistory(objScript, dsTask, dsSubs, objSes);
                                ClientScriptManager cs = Page.ClientScript;
                                cs.RegisterStartupScript(this.GetType(), "AKey11", "ShowSuccess('Sub Task(s) Successfully Added');", true);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey12", "ShowError('" + ex.Message.Replace("\r\n", "<br />") + "');", true);
                    }
                }
            }
        }

        protected void cmdExtra2Save_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            DS_Tasks dsTsk = objTask.Read(Task_ID, false, false, false);

            DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(dsTsk.tbltasks[0].Walkflow_ID);
            int ExtraField2_ID = 0;
            if (dsWF.tblwalkflow[0].Exrta_Field2_Type == 3 && dsWF.tblwalkflow[0].Extra_Field2_Master_Table_ID > 0)
            {
                DS_Master_Tables dsM = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type).GetMasterTable(dsWF.tblwalkflow[0].Extra_Field2_Master_Table_ID);
                List<DS_Master_Tables.tblDataRow> drM = dsM.tblData.Where(x => x.Data.Trim() == txtExtraField2Value.Text.Trim()).ToList();
                if (drM.Count > 0)
                {
                    ExtraField2_ID = drM[0].Data_ID;
                }
            }

            if (objTask.UpdateExtraField2(Task_ID, txtExtraField2Value.Text, ExtraField2_ID) == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey15", "ShowError('Database Error : Cannot Add the " + lblExtraField2Name.Text + "');", true);
            }
            else
            {
                Script_Generator objScripts = new Script_Generator();
                LoadTaskPartially(objScripts, objSes);
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey15", "ShowSuccess('" + lblExtraField2Name.Text + " Successfully Updated');", true);
                lblExtraField2Value.Text = txtExtraField2Value.Text;
            }
        }

        protected void cmdExtraSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            DS_Tasks dsTsk = objTask.Read(Task_ID, false, false, false);

            DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(dsTsk.tbltasks[0].Walkflow_ID);
            int ExtraField_ID = 0;
            if (dsWF.tblwalkflow[0].Exrta_Field_Type == 3 && dsWF.tblwalkflow[0].Extra_Field_Master_Table_ID > 0)
            {
                DS_Master_Tables dsM = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type).GetMasterTable(dsWF.tblwalkflow[0].Extra_Field_Master_Table_ID);
                List<DS_Master_Tables.tblDataRow> drM = dsM.tblData.Where(x => x.Data.Trim() == txtExtraFieldValue.Text.Trim()).ToList();
                if (drM.Count > 0)
                {
                    ExtraField_ID = drM[0].Data_ID;
                }
            }

            if (objTask.UpdateExtraField(Task_ID, txtExtraFieldValue.Text, ExtraField_ID) == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey16", "ShowError('Database Error : Cannot Add the " + lblExtraFieldName.Text + "');", true);
            }
            else
            {
                Script_Generator objScripts = new Script_Generator();
                LoadTaskPartially(objScripts, objSes);
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey16", "ShowSuccess('" + lblExtraFieldName.Text + " Successfully Updated');", true);
                lblExtraFieldValue.Text = txtExtraFieldValue.Text;
            }
        }

        protected void cmdChangeEL2_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            if (objTask.UpdateTaskEL2(Task_ID, Convert.ToInt32(cboUpdateEL2.SelectedItem.Value), objSes.UserID) == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey17", "ShowError('Database Error : Cannot Update the " + ltrEL2.Text + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey17", "ShowSuccess('" + ltrEL2.Text + " Successfully Updated');", true);
                lblEL2Name.Text = cboUpdateEL2.SelectedItem.Text;
                LoadEL2_Hiararchy(Convert.ToInt32(cboUpdateEL2.SelectedItem.Value), objSes);
            }
        }

        protected void cmdUpdateDueDate_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            DateTime DueDate = DateTime.Today.Date;
            Common_Actions objCom = new Common_Actions();
            
            if (objCom.ValidateDateTime(txtDueDate.Text + " " + cboDueTime.Text, ref DueDate) == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey18", "ShowError('Invalid Due Date');", true);
            }
            else
            {
                Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                if (objTask.UpdateDueDate(Task_ID, DueDate, objSes.UserID) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey18", "ShowError('Database Error : Cannot Add the Due Date');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey18", "ShowSuccess('Due Date Successfully Updated');", true);
                    lblDueDate.Text = string.Format("{0:" + Constants.DateFormat + " HH:mm}", DueDate);
                    Script_Generator objScripts = new Script_Generator();
                    LoadTaskPartially(objScripts, objSes, true);
                }
            }
        }

        protected void cmdUploadHnd_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            DS_Tasks dsTask = objTask.Read(Task_ID, false, false, false);
            DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(dsTask.tbltasks[0].Walkflow_ID, false, false);
            FillTaskData(Task_ID, ref dsTask, dsWF, objTask);

            LoadAttachments(dsWF.tblwalkflow[0].Document_Project_ID, dsTask.tbltask_docs, dsTask.tbltask_attachment, dsTask.tbltask_comments, objSes);
            FillReplaceFileDropDown(dsTask.tbltask_docs, objSes);
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(this.GetType(), "AKey19", "ShowSuccess('Files Successfully Uploaded');", true);
        }

        protected void cmdSaveAddLinkhnd_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            int Document_ID = Convert.ToInt32(hndLinkDocID.Value);
            bool isResult = (hndIsResultLink.Value == "1" ? true : false);

            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            if (objTask.AddLink(Task_ID, Document_ID, objSes.UserID, isResult) == true)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey20", "ShowSuccess('Document Link Successfully Linked');", true);

                DS_Tasks dsTask = objTask.Read(Task_ID, false, false, false);
                DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(dsTask.tbltasks[0].Walkflow_ID, false, false);
                FillTaskData(Task_ID, ref dsTask, dsWF, objTask);

                LoadAttachments(dsWF.tblwalkflow[0].Document_Project_ID, dsTask.tbltask_docs, dsTask.tbltask_attachment, dsTask.tbltask_comments, objSes);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey20", "ShowError('The file is already Linked');", true);
            }
        }

        protected void cmdSaveComment_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            if (objTask.AddComment(Task_ID, objSes.UserID, txtCommentMain.Text, cboPriority.SelectedValue, cboCommentType.SelectedItem.Text) == true)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey21", "ShowSuccess('Comment Successfully Added');", true);

                DS_Tasks dsTask = objTask.Read(Task_ID, false, false, false);
                DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(dsTask.tbltasks[0].Walkflow_ID, false, false);
                FillTaskData(Task_ID, ref dsTask, dsWF, objTask);

                DS_Tasks dsSubs = objTask.GetActiveSubTasks(Task_ID);
                Script_Generator objScripts = new Script_Generator();

                LoadHistory(objScripts, dsTask, dsSubs, objSes);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey21", "ShowError('Database Error : Cannot Add the Comment');", true);
            }
        }

        protected void cmdCancelTaskConform_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            DS_Tasks dsCurrentTask = objTask.Read(Task_ID, false, false, false);

            try
            {
                bool isMultiPost = false;
                if (ValidateTaskPost(Task_ID, objCtrlList.Task_ID, Convert.ToInt32(ViewState["csid"]), dsCurrentTask.tbltasks[0].Current_Step_ID, ref isMultiPost, objSes))
                {
                    Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
                    N_Ter.Customizable.Master_Tables objMasters = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);

                    Common_Task_Actions objComAct = new Common_Task_Actions();
                    objComAct.PrepareControlsDataSet(dsCurrentTask, objSes, true, ref objCtrlList);

                    SaveStep(dsCurrentTask, objTask, objCtrlList, objSes.UserID, isMultiPost, objSes);

                    if (isMultiPost == false)
                    {
                        objTask.UpdateTaskHistory(Task_ID, -2, objSes.UserID, objSes.FullName, false, objCus, objMasters);
                    }
                    else
                    {
                        objTask.Update_Task_Lock(Task_ID, 0, 0);
                    }
                    ValidateNextPage(true, objSes);
                }
            }
            catch (Exception ex)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey22", "ShowError('" + ex.Message + "');", true);
            }
        }

        protected void cmdReleaseConfirm_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            DS_Tasks ds = objTask.Read(Task_ID, false, false, false);
            DS_Tasks.tbltask_historyRow[] drLastTaskHistory = ds.tbltask_history.OrderByDescending(x => x.Task_Update_ID).ToArray();

            objTask.Update_Task_Lock(Task_ID, drLastTaskHistory[0].Task_Update_ID, 0);

            ValidateNextPage(true, objSes);
        }

        private bool ValidateTaskPost(int Page_Task_ID, int Session_Task_ID, int Page_Step_ID, int Current_Step_ID, ref bool isMultiPost, SessionObject objSes)
        {
            bool ret = false;
            if (Page_Task_ID != Session_Task_ID)
            {
                throw new Exception("There is a missmatch on the Posted Data. Please contact the System Administrator");
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
                        throw new Exception("The Task is already submitted by another user.");
                    }
                }
                else
                {
                    throw new Exception("The Task is already submitted by another user.");
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
            int Task_ID = Convert.ToInt32(ViewState["tid"]);

            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            DS_Tasks dsCurrentTask = objTask.Read(Task_ID, false, false, false);

            bool isMultiPost = false;
            if (ValidateTaskPost(Task_ID, objList.Task_ID, Convert.ToInt32(ViewState["csid"]), dsCurrentTask.tbltasks[0].Current_Step_ID, ref isMultiPost, objSes))
            {
                Common_Task_Actions objCommAct = new Common_Task_Actions();
                objCommAct.PrepareControlsDataSet(null, objSes, false, ref objList);

                string Invalid_Fields = "";
                bool All_Fields_Validated = objCommAct.Validate_Filled_Data(objList, ref Invalid_Fields);

                if (All_Fields_Validated == false)
                {
                    throw new Exception("Data Format Error - Please check the Following Values<br/>" + Invalid_Fields);
                }
                else
                {
                    objCommAct.PrepareControlsDataSet(dsCurrentTask, objSes, true, ref objList);

                    SaveStep(dsCurrentTask, objTask, objList, objSes.UserID, isMultiPost, objSes);

                    if (isMultiPost == false)
                    {
                        Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
                        N_Ter.Customizable.Master_Tables objMasters = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);

                        DS_Tasks.tbltask_historyRow drLastTaskHistory = dsCurrentTask.tbltask_history.OrderByDescending(x => x.Task_Update_ID).First();
                        objTask.UpdateStepFinishedDetails(drLastTaskHistory.Task_Update_ID, objSes.UserID, false);

                        objTask.UpdateTaskHistory(Task_ID, Convert.ToInt32(cboSteps.SelectedItem.Value), objSes.UserID, objSes.FullName, true, objCus, objMasters);

                        //check for auto submit steps
                        DS_Tasks dsNewTask = objTask.Read(Task_ID, false, false, false);

                        if (dsNewTask.tbltasks[0].IsIs_Auto_SubmitNull() == false && dsNewTask.tbltasks[0].Is_Auto_Submit)
                        {
                            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                            while (dsNewTask.tbltasks[0].IsIs_Auto_SubmitNull() == false && dsNewTask.tbltasks[0].Is_Auto_Submit)
                            {
                                if (objCommAct.AutoSubmit(dsNewTask, objTask, objWF, objCus, objMasters, objSes.UserID, objSes.FullName))
                                {
                                    dsNewTask = objTask.Read(Task_ID, false, false, false);
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
                        objTask.Update_Task_Lock(Task_ID, 0, 0);
                    }
                    ValidateNextPage(false, objSes);
                }
            }
        }

        private void ValidateNextPage(bool ForceLeave, SessionObject objSes)
        {
            Session["msg"] = "Task Successfully Submitted";

            if (ForceLeave == true)
            {
                Response.Redirect(GetPreviousPage());
            }
            else
            {
                if (Convert.ToBoolean(ViewState["stay"]) == false)
                {
                    Response.Redirect(GetPreviousPage());
                }
                else
                {
                    int Task_ID = Convert.ToInt32(ViewState["tid"]);
                    Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                    DS_Tasks dsTask = objTask.Read(Task_ID, false, false, false);
                    if (dsTask.tbltasks.Rows.Count > 0)
                    {
                        N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                        if (dsTask.tbltasks[0].Current_Step_ID == -1 || dsTask.tbltasks[0].Current_Step_ID == -2)
                        {
                            Response.Redirect("task_info.aspx?tid=" + objURL.Encrypt(Convert.ToString(Task_ID)));
                        }
                        else
                        {
                            Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);

                            DS_Users dsAltUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).ReadAssignedUsers(objSes.UserID);
                            if (objTask.Can_Access_Task(Task_ID, objSes.UserID, dsAltUsers.tblusers, objCus) == true)
                            {
                                if (ViewState["prev"] == null)
                                {
                                    Response.Redirect("task.aspx?tid=" + objURL.Encrypt(Convert.ToString(Task_ID)));
                                }
                                else
                                {
                                    Response.Redirect("task.aspx?tid=" + objURL.Encrypt(Convert.ToString(Task_ID)) + "&bck=" + Convert.ToString(ViewState["prev"]));
                                }
                            }
                            else
                            {
                                Response.Redirect(GetPreviousPage());
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect(GetPreviousPage());
                    }
                }
            }
        }

        private string GetPreviousPage()
        {
            string ReturnURL = "task_list.aspx?";
            if (ViewState["prev"] != null)
            {
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                ReturnURL = objURL.Decrypt(Convert.ToString(ViewState["prev"]));
            }
            return ReturnURL;
        }

        private void NextStepSubmit(Task_Controls_Main objList, SessionObject objSes)
        {
            try
            {
                int Task_ID = Convert.ToInt32(ViewState["tid"]);

                Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                DS_Tasks dsCurrentTask = objTask.Read(Task_ID, false, false, false);

                bool isMultiPost = false;
                if (ValidateTaskPost(Task_ID, objList.Task_ID, Convert.ToInt32(ViewState["csid"]), dsCurrentTask.tbltasks[0].Current_Step_ID, ref isMultiPost, objSes))
                {
                    Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                    DS_Workflow dsCurrentStep = objWF.ReadStep(dsCurrentTask.tbltasks[0].Current_Step_ID);

                    //check whether the task is completed
                    if (dsCurrentStep.tblworkflow_steps.Rows.Count == 0)
                    {
                        throw new Exception("Submit Failed, Task is currently not active.");
                    }
                    else
                    {
                        //check whether the creator cannot submit
                        if (dsCurrentStep.tblworkflow_steps[0].Creator_Cannot_Submit == true && dsCurrentTask.tbltasks[0].Creator_ID == objSes.UserID)
                        {
                            throw new Exception("You cannot Approve/Validate a task you created");
                        }
                        else
                        {
                            //check to finish all sub tasks
                            bool FinishSubsOK = true;
                            List<DS_Workflow.tblsub_workflowsRow> drSubs = dsCurrentStep.tblsub_workflows.Where(x => x.isStart == false).ToList();
                            if (drSubs.Count > 0)
                            {
                                DS_Tasks dsSubTasks = objTask.GetActiveSubTasks(Task_ID);
                                if (dsSubTasks.tbltasks.Where(x => drSubs.Select(y => y.Walkflow_ID).Contains(x.Walkflow_ID)).Count() > 0)
                                {
                                    FinishSubsOK = false;
                                }
                            }
                            if (FinishSubsOK == false)
                            {
                                throw new Exception("Cannot Submit - There are Sub Tasks which should to be complated");
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
                                    throw new Exception("Data Format Error - Please check the Following Values<br/>" + Invalid_Fields);
                                }
                                else
                                {
                                    objCommAct.PrepareControlsDataSet(dsCurrentTask, objSes, true, ref objList);
                                    SaveStep(dsCurrentTask, objTask, objList, objSes.UserID, isMultiPost, objSes);

                                    if (isMultiPost == false)
                                    {
                                        Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
                                        N_Ter.Customizable.Master_Tables objMasters = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);

                                        DS_Tasks.tbltask_historyRow drLastTaskHistory = dsCurrentTask.tbltask_history.OrderByDescending(x => x.Task_Update_ID).First();
                                        DateTime LastUpdateDate = drLastTaskHistory.Posted_Date;

                                        int NextStepID;

                                        if (dsCurrentStep.tblworkflow_steps[0].Show_Custom_Post && objCuz.PostElements.NextStep > 0)
                                        {
                                            NextStepID = objCuz.PostElements.NextStep;
                                        }
                                        else
                                        {
                                            NextStepID = objCommAct.FindNextStep(LastUpdateDate, objWF, dsCurrentTask, dsCurrentStep, objList, objSes.UserID);
                                        }

                                        objTask.UpdateStepFinishedDetails(drLastTaskHistory.Task_Update_ID, objSes.UserID, false);

                                        objTask.UpdateTaskHistory(Task_ID, NextStepID, objSes.UserID, objSes.FullName, true, objCus, objMasters);

                                        if (NextStepID > 0)
                                        {
                                            //check for auto submit steps
                                            DS_Tasks dsNewTask = objTask.Read(Task_ID, false, false, false);
                                            if (dsNewTask.tbltasks[0].IsIs_Auto_SubmitNull() == false && dsNewTask.tbltasks[0].Is_Auto_Submit)
                                            {
                                                while (dsNewTask.tbltasks[0].IsIs_Auto_SubmitNull() == false && dsNewTask.tbltasks[0].Is_Auto_Submit)
                                                {
                                                    if (objCommAct.AutoSubmit(dsNewTask, objTask, objWF, objCus, objMasters, objSes.UserID, objSes.FullName))
                                                    {
                                                        dsNewTask = objTask.Read(Task_ID, false, false, false);
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
                                        objTask.Update_Task_Lock(Task_ID, 0, 0);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void SaveStep(DS_Tasks ds, Tasks objTask, Task_Controls_Main objList, int User_ID, bool isMultiPost, SessionObject objSes)
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

            int Item_ID = 0;
            string ItemValue = "";

            List<Task_Field_Update> lstTaskUpdates = new List<Task_Field_Update>();
            List<Task_Extra_Field_Updates> lstTaskExtraFieldUpdates = new List<Task_Extra_Field_Updates>();
            List<Task_Doc_Custom_Actions> lstTaskDocUploadActions = new List<Task_Doc_Custom_Actions>();

            foreach (Task_Controls item in objList.Controls.Where(x => x.UI_Type != UI_Types.OtherTypes))
            {
                ItemValue = "";
                if (item.UI_Type == UI_Types.FileUploads)
                {
                    if (item.File_Path.Trim() != "")
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
                    lstTaskUpdates.Add(new Task_Field_Update
                    {
                        Task_Update_ID = TaskUpdateID,
                        Workflow_Step_Field_ID = item.Field_ID,
                        Field_Value = item.Item_Value,
                        User_ID = User_ID
                    });
                    ItemValue = item.Item_Value;
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
            objTask.AddStepTaskUpdate(lstTaskUpdates, lstTaskExtraFieldUpdates, lstTaskDocUploadActions, objCus);
        }

        private void LoadTaskMessages(int Walkflow_ID, int Entity_L2_ID, int Task_ID, SessionObject objSes)
        {
            ChatPanelResizeScript = "AdjustPopupSize(167, 1200, 'at_model_chat');";
            Chat_Arrange_Script = "$(\".clickable-row\").click(function () {\r\n" +
                                        "$find('mpuMessage').show();\r\n" +
                                        "LoadMessage($(this).data(\"href\"));\r\n" +
                                    "});\r\n";

            Messages objMessages = ObjectCreator.GetMessages(objSes.Connection, objSes.DB_Type);
            if (IsPostBack == false)
            {
                DS_Messages ds = objMessages.ReadUsersForTask(Walkflow_ID, Entity_L2_ID, objSes.UserID);
                cboMessageUsers.DataSource = ds.tblusers;
                cboMessageUsers.DataTextField = "Full_Name";
                cboMessageUsers.DataValueField = "User_ID";
                cboMessageUsers.DataBind();
                cboMessageUsers.Items.Insert(0, new ListItem("[All Involved]", "-1"));
                cboMessageUsers.Items.Insert(0, new ListItem("[Not Selected]", "0"));
            }
            LoadThreads(objMessages, Task_ID, objSes);
        }

        private void LoadThreads(Messages objMsgs, int Task_ID, SessionObject objSes)
        {
            DS_Messages ds = objMsgs.ReadThreadsForTask(Task_ID, objSes.UserID);

            if (ds.tblmessage_threads.Rows.Count > 0)
            {
                ltrThreads.Text = "<div class=\"table-responsive table-light no-margin-b\"><table id=\"message_table\" class=\"table table-striped table-hover grid_table row-pointer no-margin-b non_full_width_table\" data-size=\"non_full_width_table\">" + "\r\n" +
                                        "<thead>" + "\r\n" +
                                            "<tr>" + "\r\n" +
                                                "<th>Subject</th>" + "\r\n" +
                                                "<th class=\"hidden-xs w40\"></th>" + "\r\n" +
                                                "<th class=\"w100\">With</th>" + "\r\n" +
                                                "<th class=\"text-center w100\">Date</th>" + "\r\n" +
                                            "</tr>" + "\r\n" +
                                        "</thead>" + "\r\n" +
                                        "<tbody>" + "\r\n";
                foreach (DS_Messages.tblmessage_threadsRow row in ds.tblmessage_threads)
                {
                    ltrThreads.Text = ltrThreads.Text + "<tr class=\"" + (row.New_Message_Count > 0 ? "message-read " : "message-unread ") + "clickable-row\" data-href='" + row.Thread_ID + "'>" + "\r\n" +
                                                            "<td>" + row.Title + "</td>" + "\r\n" +
                                                            "<td class=\"hidden-xs w40\">" + "\r\n" +
                                                            (row.New_Message_Count > 0 ? "<span class=\"label label-info mr10 fs11\">" + row.New_Message_Count + "</span>" + "\r\n" : "") +
                                                            "</td>" + "\r\n" +
                                                            "<th class=\"w100\">" + row.Other_Member + "</th>" + "\r\n" +
                                                            "<td class=\"text-center w100\">" + string.Format("{0:" + Constants.DateFormat + "}", row.Last_Message_Date) + "</td>" + "\r\n" +
                                                        "</tr>" + "\r\n";
                }
                ltrThreads.Text = ltrThreads.Text + "</tbody>" + "\r\n" +
                                                "</table></div>" + "\r\n";
            }
            else
            {
                ltrThreads.Text = "";
            }
        }

        protected void cmdSendMessage_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Messages objMessage = ObjectCreator.GetMessages(objSes.Connection, objSes.DB_Type);
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            if (hndMessageThreadID.Value == "0")
            {
                if (cboMessageUsers.SelectedItem.Value == "-1")
                {
                    DS_Tasks dsTS = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading).Read(Task_ID, false, false, false);
                    DS_Messages dsUsers = objMessage.ReadUsersForTask(dsTS.tbltasks[0].Walkflow_ID, dsTS.tbltasks[0].Entity_L2_ID, objSes.UserID);
                    bool AllProcesses = true;
                    foreach (DS_Messages.tblusersRow row in dsUsers.tblusers)
                    {
                        if (objMessage.AddThread(objSes.UserID, row.User_ID, txtTitle.Text, txtMessage.Text, objSes.UserID, objSes.PhysicalRoot, objSes.WebRoot, Task_ID) == false)
                        {
                            AllProcesses = false;
                        }
                    }

                    if (AllProcesses == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey23", "ShowError('All Messages could not be Sent');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey23", "ShowSuccess('Messages Successfully Sent');", true);

                        Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                        DS_Tasks ds = objTask.Read(Task_ID, false, false, false);

                        LoadTaskMessages(ds.tbltasks[0].Walkflow_ID, ds.tbltasks[0].Entity_L2_ID, Task_ID, objSes);
                    }
                }
                else
                {
                    if (objMessage.AddThread(objSes.UserID, Convert.ToInt32(cboMessageUsers.SelectedValue), txtTitle.Text, txtMessage.Text, objSes.UserID, objSes.PhysicalRoot, objSes.WebRoot, Task_ID) == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey23", "ShowError('Message cannot be Sent');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey23", "ShowSuccess('Message Successfully Sent');", true);

                        Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                        DS_Tasks ds = objTask.Read(Task_ID, false, false, false);

                        LoadTaskMessages(ds.tbltasks[0].Walkflow_ID, ds.tbltasks[0].Entity_L2_ID, Task_ID, objSes);
                    }
                }
            }
            else
            {
                if (objMessage.AddMessage(Convert.ToInt32(hndMessageThreadID.Value), objSes.UserID, Convert.ToInt32(hndMessageMemberID.Value), txtMessage.Text, objSes.UserID, objSes.PhysicalRoot, objSes.WebRoot, Task_ID) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey23", "ShowError('Message cannot be Sent');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey23", "ShowSuccess('Message Successfully Sent');", true);

                    Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                    DS_Tasks ds = objTask.Read(Task_ID, false, false, false);

                    LoadTaskMessages(ds.tbltasks[0].Walkflow_ID, ds.tbltasks[0].Entity_L2_ID, Task_ID, objSes);
                }
            }
        }

        protected void cmdAddDocComment_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            bool ActionOK = true;
            string DocNumber = "";

            if (Convert.ToString(hndIsDocument.Value) == "1")
            {
                try
                {
                    DocNumber = objTask.ReadTaskDocument(Convert.ToInt32(hndDocCommentID.Value)).Doc_Number;
                }
                catch (Exception)
                {
                    DocNumber = "Error";
                }
                ActionOK = objTask.AddComment(Task_ID, objSes.UserID, DocNumber + " :\r\n" + txtDocComment.Text, cboDocCommentPriority.SelectedValue, cboDocCommentType.SelectedItem.Text, Convert.ToInt32(hndDocCommentID.Value), 0);
            }
            else
            {
                try
                {
                    DocNumber = objTask.ReadTaskAttachment(Convert.ToInt32(hndDocCommentID.Value)).Document_No;
                }
                catch (Exception)
                {
                    DocNumber = "Error";
                }
                ActionOK = objTask.AddComment(Task_ID, objSes.UserID, DocNumber + " :\r\n" + txtDocComment.Text, cboDocCommentPriority.SelectedValue, cboDocCommentType.SelectedItem.Text, 0, Convert.ToInt32(hndDocCommentID.Value));
            }

            if (ActionOK == true)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey24", "ShowSuccess('Comment Successfully Added');", true);

                DS_Tasks dsTask = objTask.Read(Task_ID, false, false, false);
                DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(dsTask.tbltasks[0].Walkflow_ID, false, false);
                FillTaskData(Task_ID, ref dsTask, dsWF, objTask);

                DS_Tasks dsSubs = objTask.GetActiveSubTasks(Task_ID);
                Script_Generator objScripts = new Script_Generator();

                LoadHistory(objScripts, dsTask, dsSubs, objSes);
                LoadAttachments(dsWF.tblwalkflow[0].Document_Project_ID, dsTask.tbltask_docs, dsTask.tbltask_attachment, dsTask.tbltask_comments, objSes);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey24", "ShowError('Database Error : Cannot Add the Comment');", true);
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            int Current_Step_ID = Convert.ToInt32(ViewState["csid"]);
            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            if (objTask.DeleteTaskUpdate(Task_ID, Current_Step_ID))
            {
                Common_Task_Actions objAct = new Common_Task_Actions();
                objAct.PrepareControlsDataSet(null, objSes, false, ref objCtrlList);

                foreach (Task_Controls item in objCtrlList.Controls)
                {
                    if (item.UI_Type == UI_Types.DropDowns || item.UI_Type == UI_Types.Checkbox || item.UI_Type == UI_Types.TextBoxes)
                    {
                        objTask.SaveTaskUpdate(Task_ID, item.Field_ID, item.Item_Value);
                    }
                }
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey25", "ShowSuccess('Task Step Details Successfully Saved');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey25", "ShowError('Task Step Details could not be Saved');", true);
            }
        }

        private void LoadTemplates(SessionObject objSes)
        {
            string TemplatePath = objSes.PhysicalRoot + "\\nter_app_uploads\\company_letter_head";
            System.IO.DirectoryInfo TemplatesList = new System.IO.DirectoryInfo(TemplatePath);
            cboLetterHead.Items.Clear();
            string FileName = "";
            foreach (System.IO.FileInfo Template in TemplatesList.GetFiles())
            {
                FileName = Template.Name.Substring(0, Template.Name.IndexOf('.'));
                cboLetterHead.Items.Add(new ListItem(FileName, FileName));
            }
        }

        private void LoadEntity_Level_1s(SessionObject objSes)
        {
            Entity_Level_1 objRef = ObjectCreator.GetEntity_Level_1(objSes.Connection, objSes.DB_Type);
            DS_Entity_Level_1 ds = objRef.ReadAll();
            cboEntity_Level_1.DataSource = ds.tblentity_level_1;
            cboEntity_Level_1.DataTextField = ds.tblentity_level_1.Display_NameColumn.ColumnName;
            cboEntity_Level_1.DataValueField = ds.tblentity_level_1.Entity_L1_IDColumn.ColumnName;
            cboEntity_Level_1.DataBind();
            cboEntity_Level_1.Items.Insert(0, new ListItem("[Not Selected]", "0"));
        }

        private void LoadEntity_Level_2s(SessionObject objSes)
        {
            Entity_Level_2 objRef = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type);
            DS_Entity_Level_2 ds = objRef.ReadAll();
            cboEntity_Level_2.DataSource = ds.tblentity_level_2;
            cboEntity_Level_2.DataTextField = ds.tblentity_level_2.Display_NameColumn.ColumnName;
            cboEntity_Level_2.DataValueField = ds.tblentity_level_2.Entity_L2_IDColumn.ColumnName;
            cboEntity_Level_2.DataBind();
            cboEntity_Level_2.Items.Insert(0, new ListItem("[Main " + objSes.EL2 + "]", "0"));
        }

        private void LoadUpdate_Entity_Level_2(SessionObject objSes)
        {
            Entity_Level_2 objRef = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type);
            DS_Entity_Level_2 ds = objRef.ReadForUser(objSes.UserID);
            cboUpdateEL2.DataSource = ds.tblentity_level_2;
            cboUpdateEL2.DataTextField = ds.tblentity_level_2.Display_NameColumn.ColumnName;
            cboUpdateEL2.DataValueField = ds.tblentity_level_2.Entity_L2_IDColumn.ColumnName;
            cboUpdateEL2.DataBind();
        }

        private void LoadUpdateQueue(SessionObject objSes)
        {
            Task_Queues objQueue = ObjectCreator.GetTask_Queues(objSes.Connection, objSes.DB_Type);
            DS_Task_Queues ds = objQueue.ReadAll();
            cboQueue.DataSource = ds.tbltask_queues;
            cboQueue.DataTextField = ds.tbltask_queues.Queue_NameColumn.ColumnName;
            cboQueue.DataValueField = ds.tbltask_queues.Queue_IDColumn.ColumnName;
            cboQueue.DataBind();

            cboQueue.Items.Insert(0, new ListItem("[Only on Main Tasks List]", "0"));
        }

        private void LoadUsers(SessionObject objSes)
        {
            Users objUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
            DS_Users ds = objUsers.ReadAllActiveWithGroups();

            gvUsers.SelectedIndex = -1;
            gvUsers.DataSource = ds.tblusers;
            gvUsers.DataBind();
            if (ds.tblusers.Rows.Count > 0)
            {
                gvUsers.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void LoadWorkflows(Workflow objWF)
        {
            DS_Workflow ds = objWF.ReadAll();

            gvWorkflows.SelectedIndex = -1;
            gvWorkflows.DataSource = ds.tblwalkflow;
            gvWorkflows.DataBind();
            if (ds.tblwalkflow.Rows.Count > 0)
            {
                gvWorkflows.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].HasControls())
                {
                    CheckBox chkSelect = ((CheckBox)e.Row.Cells[1].Controls[1]);
                    chkSelect.Attributes.Add("data-id", e.Row.Cells[0].Text);
                    chkSelect.Attributes.Add("data-hnd", "hndUsers");
                }
            }
        }

        protected void gvWorkflows_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].HasControls())
                {
                    CheckBox chkSelect = ((CheckBox)e.Row.Cells[1].Controls[1]);
                    chkSelect.Attributes.Add("data-id", e.Row.Cells[0].Text);
                    chkSelect.Attributes.Add("data-hnd", "hndWorkflows");
                }
            }
        }

        private bool SaveEL2(SessionObject objSes)
        {
            bool ret = true;

            if (Convert.ToString(ViewState["el2"]) == "1")
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey26", "ShowError('Please add a " + objSes.EL2 + " before Submit');", true);
                ret = false;
            }
            else
            {
                Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                DS_Tasks dsCurrentTask = objTask.Read(Convert.ToInt32(ViewState["tid"]), false, false, false);

                Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                DS_Workflow dsWFStep = objWF.ReadStep(dsCurrentTask.tbltasks[0].Current_Step_ID);

                if (dsWFStep.tblworkflow_steps[0].Deactivate_Entity_L2 == true)
                {
                    Entity_Level_2 objEL2 = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type);
                    ActionDone Deac = objEL2.Deactivate(dsCurrentTask.tbltasks[0].Entity_L2_ID);
                    if (Deac.Done == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey26", "ShowError('The attached " + objSes.EL2 + " is marked to be Deactivated, But " + Deac.Reason + "');", true);
                        ret = false;
                    }
                }
            }
            return ret;
        }

        private DS_Entity_Level_2.tbluser_entity_l2DataTable GetAvailableUsers()
        {
            DS_Entity_Level_2 ds = new DS_Entity_Level_2();
            DS_Entity_Level_2.tbluser_entity_l2Row dr;
            foreach (string user in hndUsers.Value.Split('|'))
            {
                if (user.Trim() != "")
                {
                    dr = ds.tbluser_entity_l2.Newtbluser_entity_l2Row();
                    dr.Entity_L2_ID = 0;
                    dr.User_ID = Convert.ToInt32(user);
                    ds.tbluser_entity_l2.Rows.Add(dr);
                }
            }
            return ds.tbluser_entity_l2;
        }

        private DS_Entity_Level_2.tblwalkflow_entity_l2DataTable GetAvailableWorkflows()
        {
            DS_Entity_Level_2 ds = new DS_Entity_Level_2();
            DS_Entity_Level_2.tblwalkflow_entity_l2Row dr;
            foreach (string wf in hndWorkflows.Value.Split('|'))
            {
                if (wf.Trim() != "")
                {
                    dr = ds.tblwalkflow_entity_l2.Newtblwalkflow_entity_l2Row();
                    dr.Entity_L2_ID = 0;
                    dr.Walkflow_ID = Convert.ToInt32(wf);
                    ds.tblwalkflow_entity_l2.Rows.Add(dr);
                }
            }
            return ds.tblwalkflow_entity_l2;
        }

        protected void cmdSaveEL2_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            DS_Tasks dsCurrentTask = objTask.Read(Task_ID, false, false, false);

            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow dsWFStep = objWF.ReadStep(dsCurrentTask.tbltasks[0].Current_Step_ID);

            int EL2CreatedHistoryCount = dsCurrentTask.tbltask_history.Where(x => x.Workflow_Step_ID == dsCurrentTask.tbltasks[0].Current_Step_ID && x.Entity_L2_Created == true).Count();

            if (dsWFStep.tblworkflow_steps[0].Create_New_Entity_L2 == true && EL2CreatedHistoryCount == 0)
            {
                Entity_Level_2 objEL2 = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type);
                int LastEL2_ID = objEL2.Insert(txtFolder.Text, txtEntityCode.Text, txtDisplayName.Text, txtLegalName.Text, txtDescription.Text, txtPHStreet.Text, txtPHTown.Text,
                    txtPHState.Text, txtMainContact.Text, txtPhone.Text, txtE_Mail.Text, txtWebSite.Text, Convert.ToInt32(cboEntity_Level_1.SelectedItem.Value),
                    Convert.ToInt32(cboEntity_Level_2.SelectedItem.Value), cboLetterHead.SelectedItem.Text, GetAvailableUsers(), GetAvailableWorkflows());
                if (LastEL2_ID == -1)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey27", "ShowError('" + objSes.EL2 + " could not be saved');", true);
                }
                else
                {
                    DS_Tasks.tbltask_historyRow[] drLastTaskHistory = dsCurrentTask.tbltask_history.OrderByDescending(x => x.Task_Update_ID).ToArray();
                    objTask.UpdateTaskEL2(Task_ID, LastEL2_ID, objSes.UserID);
                    objTask.UpdateStepEL2Details(drLastTaskHistory[0].Task_Update_ID);
                    LoadTask(objSes);
                }
            }
            else
            {
                LoadTask(objSes);
            }
        }

        protected void cmdDP_Upload_Click(object sender, EventArgs e)
        {
            try
            {
                SessionObject objSes = (SessionObject)Session["dt"];
                int Task_ID = Convert.ToInt32(ViewState["tid"]);
                Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                DS_Tasks dsCurrentTask = objTask.Read(Task_ID, false, false, false);

                int Document_Project_ID = Convert.ToInt32(hndDoc_Proj_ID.Value);
                Documents objDocs = ObjectCreator.GetDocuments(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

                DS_Entity_Level_2.tblentity_level_2Row drComp = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type).Read(dsCurrentTask.tbltasks[0].Entity_L2_ID);

                string PathPart = "\\" + drComp.Folder_Name + "\\Document_Repos\\" + DateTime.Today.Year + "\\" + DateTime.Today.Month;

                string fn = System.IO.Path.GetFileName(fulDocumentDP.PostedFile.FileName);
                string[] fnSP = fn.Split('.');

                string SaveLocation = objSes.PhysicalRoot + "\\nter_app_uploads\\client_docs" + PathPart;
                string SaveLocationDB = "nter_app_uploads\\client_docs" + PathPart;
                if (System.IO.Directory.Exists(SaveLocation) == false)
                {
                    System.IO.Directory.CreateDirectory(SaveLocation);
                }

                string NewFileName = "doc_" + Common_Actions.TimeStampForFileName();
                if (txtFileNameDP.Text.Trim() != "")
                {
                    NewFileName = txtFileNameDP.Text.Trim() + "_" + Common_Actions.TimeStampForFileName();
                }
                SaveLocation = SaveLocation + "\\" + NewFileName + "." + fnSP[fnSP.Length - 1];
                SaveLocationDB = SaveLocationDB + "\\" + NewFileName + "." + fnSP[fnSP.Length - 1];

                fulDocumentDP.PostedFile.SaveAs(SaveLocation);
                int MaxID = objDocs.Insert(SaveLocationDB, Document_Project_ID, objSes.UserID, drComp.Entity_L2_ID, SaveLocation, Convert.ToInt32(cboAccessLevelDP.SelectedItem.Value));
                if (MaxID > 0)
                {
                    Common_Task_Actions objCommAct = new Common_Task_Actions();
                    DS_Documents ds = objCommAct.PrepareDocControlsDataSet(MaxID, objDPCtrlList, true);

                    if (objDocs.UpdateTags(MaxID, ds.tbldocument_tags) == true)
                    {
                        bool isResult = (hndIsResultLink.Value == "1" ? true : false);
                        if (objTask.AddLink(Task_ID, MaxID, objSes.UserID, isResult) == false)
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey28", "ShowError('Database Error : Cannot attach file details to task');", true);
                        }
                        else
                        {
                            DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(dsCurrentTask.tbltasks[0].Walkflow_ID);
                            DS_Tasks dsTask = null;
                            FillTaskData(Task_ID, ref dsTask, dsWF, objTask);

                            LoadAttachments(dsWF.tblwalkflow[0].Document_Project_ID, dsTask.tbltask_docs, dsTask.tbltask_attachment, dsTask.tbltask_comments, objSes);
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey28", "ShowSuccess('Document Successfully Uploaded');", true);
                        }
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey28", "ShowError('Database Error : Cannot save the Document Tags');", true);
                    }
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey28", "ShowError('Database Error : Cannot save the file details');", true);
                }
            }
            catch (Exception Exc)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey28", "ShowError('Error: " + Exc.Message + "');", true);
            }
        }

        protected void cmdSaveAddon_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            SaveAddon(objSes);
        }

        protected void cmdAddonContinue_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            SaveAddon(objSes);
        }

        private void SaveAddon(SessionObject objSes)
        {
            Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
            N_Ter.Customizable.Master_Tables objMasters = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);

            string Invalid_Fields = "";

            Common_Task_Actions objCommAct = new Common_Task_Actions();
            objCommAct.PrepareControlsDataSet(null, objSes, false, ref objAddonCtrlList);
            bool All_Fields_Validated = objCommAct.Validate_Filled_Data(objAddonCtrlList, ref Invalid_Fields);

            if (All_Fields_Validated == true)
            {
                Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);

                DS_Tasks dsCurrentTask = objTask.Read(objAddonCtrlList.Task_ID, false, false, false);
                objCommAct.PrepareControlsDataSet(dsCurrentTask, objSes, true, ref objAddonCtrlList);

                ActionValidated objCusVal = objCus.CustomAddonPostValidations(objAddonCtrlList, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                if (objCusVal.Validated)
                {
                    int Task_Addon_ID = objTask.AddTaskAddon(objAddonCtrlList.Task_ID, objAddonCtrlList.Current_Step_ID, objSes.UserID, objAddonCtrlList.Addon_ID);
                    if (Task_Addon_ID > 0)
                    {
                        DS_Workflow dsWF = objWF.Read(dsCurrentTask.tbltasks[0].Walkflow_ID, false, false);

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

                        int Item_ID = 0;
                        string ItemValue = "";

                        List<Task_Field_Update> lstTaskAddonFieldUpdates = new List<Task_Field_Update>();
                        List<Task_Extra_Field_Updates> lstTaskExtraFieldUpdates = new List<Task_Extra_Field_Updates>();
                        List<Task_Doc_Custom_Actions> lstTaskAddonDocUploadActions = new List<Task_Doc_Custom_Actions>();

                        foreach (Task_Controls item in objAddonCtrlList.Controls.Where(x => x.UI_Type != UI_Types.OtherTypes))
                        {
                            ItemValue = "";
                            if (item.UI_Type == UI_Types.FileUploads)
                            {
                                if (item.File_Path != "")
                                {
                                    lstTaskAddonDocUploadActions.Add(new Task_Doc_Custom_Actions
                                    {
                                        Task_ID = dsCurrentTask.tbltasks[0].Task_ID,
                                        Workflow_ID = dsCurrentTask.tbltasks[0].Walkflow_ID,
                                        Current_Step_ID = dsCurrentTask.tbltasks[0].Current_Step_ID,
                                        Workflow_Step_Field_ID = item.Field_ID,
                                        File_Path = item.File_Path
                                    });
                                    ItemValue = item.Item_Value;
                                    lstTaskAddonFieldUpdates.Add(new Task_Field_Update
                                    {
                                        Task_Update_ID = Task_Addon_ID,
                                        Workflow_Step_Field_ID = item.Field_ID,
                                        Field_Value = ItemValue
                                    });
                                }
                                else
                                {
                                    lstTaskAddonFieldUpdates.Add(new Task_Field_Update
                                    {
                                        Task_Update_ID = Task_Addon_ID,
                                        Workflow_Step_Field_ID = item.Field_ID,
                                        Field_Value = ItemValue
                                    });
                                }
                            }
                            else if (item.UI_Type == UI_Types.DropDowns)
                            {
                                lstTaskAddonFieldUpdates.Add(new Task_Field_Update
                                {
                                    Task_Update_ID = Task_Addon_ID,
                                    Workflow_Step_Field_ID = item.Field_ID,
                                    Field_Value = item.Item_Value,
                                });
                                ItemValue = item.Item_Value;
                            }
                            else if (item.UI_Type == UI_Types.Checkbox)
                            {
                                lstTaskAddonFieldUpdates.Add(new Task_Field_Update
                                {
                                    Task_Update_ID = Task_Addon_ID,
                                    Workflow_Step_Field_ID = item.Field_ID,
                                    Field_Value = item.Item_Value,
                                });
                                ItemValue = item.Item_Value;
                            }
                            else
                            {
                                lstTaskAddonFieldUpdates.Add(new Task_Field_Update
                                {
                                    Task_Update_ID = Task_Addon_ID,
                                    Workflow_Step_Field_ID = item.Field_ID,
                                    Field_Value = item.Item_Value
                                });
                                ItemValue = item.Item_Value;
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
                                    Task_ID = dsCurrentTask.tbltasks[0].Task_ID,
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
                                    Task_ID = dsCurrentTask.tbltasks[0].Task_ID,
                                    Extra_Field_Value = ItemValue,
                                    Extra_Field_ID = Item_ID,
                                    Extra_Field_Order = 2
                                });
                            }
                        }

                        objTask.AddTaskAddonUpdate(lstTaskAddonFieldUpdates, lstTaskExtraFieldUpdates, lstTaskAddonDocUploadActions, objCus);

                        System.Threading.Thread objThread1 = new System.Threading.Thread(delegate () { objCus.CustomAddonPostActions(objAddonCtrlList, objSes.UserID, objSes.PhysicalRoot, objSes.WebRoot, objSes.Currency_Sbl, objSes.EnableReading); });
                        objThread1.Priority = System.Threading.ThreadPriority.BelowNormal;
                        objThread1.Start();

                        DS_Workflow dsWFStep = objWF.ReadStep(dsCurrentTask.tbltasks[0].Current_Step_ID);
                        Script_Generator objScripts = new Script_Generator();

                        LoadAddonFields(objScripts, dsCurrentTask, dsWFStep, false, objSes);

                        DS_Tasks dsTask = null;
                        FillTaskData(objAddonCtrlList.Task_ID, ref dsTask, dsWF, objTask);

                        DS_Tasks dsSubs = objTask.GetActiveSubTasks(objAddonCtrlList.Task_ID);
                        LoadHistory(objScripts, dsTask, dsSubs, objSes);
                        LoadAttachments(dsWF.tblwalkflow[0].Document_Project_ID, dsTask.tbltask_docs, dsTask.tbltask_attachment, dsTask.tbltask_comments, objSes);

                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey29", "ShowSuccess('Task " + dsWFStep.tblworkflow_addons[0].Addon_Name + " Successfully Saved');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey29", "ShowError('Something Went Wrong, Please try again Later');", true);
                    }
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey29", "ShowError('" + objCusVal.Reason + "');", true);
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey29", "ShowError('Data Format Error - Please check the Following Values<br/>" + Invalid_Fields + "');", true);
            }
        }

        protected void cmdDelAttchmentYes_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            bool AllowDelete = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).isAllowed(AllowedAreas.Delete_Docs, objSes.UserID);
            if (AllowDelete == true)
            {
                AllowDelete = Convert.ToBoolean(ViewState["dd"]);
                if (AllowDelete == true)
                {
                    int TaskDocID = 0;
                    if (!string.IsNullOrEmpty(hndDeleteAttachment.Value.Trim(' ')) && int.TryParse(hndDeleteAttachment.Value, out TaskDocID))
                    {
                        int Task_ID = Convert.ToInt32(ViewState["tid"]);
                        Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                        DS_Tasks dsCurrentTask = objTask.Read(Task_ID, false, false, false);
                        List<DS_Tasks.tbltask_docsRow> drCurrentDoc = dsCurrentTask.tbltask_docs.Where(x => x.Task_Doc_ID == TaskDocID).ToList();

                        if (drCurrentDoc.Count > 0 && drCurrentDoc[0].Uploaded_By == objSes.UserID)
                        {
                            DeleteReason objDel = objTask.DeleteAttachment(TaskDocID);

                            if (objDel.Deleted == true)
                            {
                                DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(dsCurrentTask.tbltasks[0].Walkflow_ID);
                                DS_Tasks dsTask = null;
                                FillTaskData(Task_ID, ref dsTask, dsWF, objTask);

                                LoadAttachments(dsWF.tblwalkflow[0].Document_Project_ID, dsTask.tbltask_docs, dsTask.tbltask_attachment, dsTask.tbltask_comments, objSes);
                                FillReplaceFileDropDown(dsTask.tbltask_docs, objSes);
                                ClientScriptManager cs = Page.ClientScript;
                                cs.RegisterStartupScript(this.GetType(), "AKey30", "ShowSuccess('Task Document Successfully Deleted');", true);
                            }
                            else
                            {
                                ClientScriptManager cs = Page.ClientScript;
                                cs.RegisterStartupScript(this.GetType(), "AKey30", "ShowError('" + objDel.Reason + "');", true);
                            }
                        }
                        else
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey30", "ShowError('Task Document Cannot be Deleted');", true);
                        }
                    }
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey30", "ShowError('Current Step doesn's allow you to Delete Documents');", true);
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey30", "ShowError('You are not allowed to Delete Documents');", true);
            }
        }

        protected void cmdDeleteDocLink_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            bool AllowDelete = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).isAllowed(AllowedAreas.Delete_Docs, objSes.UserID);
            if (AllowDelete == true)
            {
                AllowDelete = Convert.ToBoolean(ViewState["dd"]);

                if (AllowDelete == true)
                {
                    int DelLink_ID = Convert.ToInt32(hndDeleteLink.Value);
                    Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

                    int Task_ID = Convert.ToInt32(ViewState["tid"]);
                    DS_Tasks dsCurrentTask = objTask.Read(Task_ID, false, false, false);
                    List<DS_Tasks.tbltask_attachmentRow> drCurrentLink = dsCurrentTask.tbltask_attachment.Where(x => x.Task_Doc_ID == DelLink_ID).ToList();

                    if (drCurrentLink.Count > 0 && drCurrentLink[0].Attached_By == objSes.UserID)
                    {
                        DeleteReason del = objTask.DeleteLink(DelLink_ID);
                        if (del.Deleted == false)
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey31", "ShowError('" + del.Reason + "');", true);
                        }
                        else
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey31", "ShowSuccess('Linked Document Successfully Removed');", true);
                            hndDeleteLink.Value = "0";

                            DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(dsCurrentTask.tbltasks[0].Walkflow_ID, false, false);
                            DS_Tasks dsTask = null;
                            FillTaskData(Task_ID, ref dsTask, dsWF, objTask);

                            LoadAttachments(dsWF.tblwalkflow[0].Document_Project_ID, dsTask.tbltask_docs, dsTask.tbltask_attachment, dsTask.tbltask_comments, objSes);
                        }
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey31", "ShowError('Linked Document Cannot be Removed');", true);
                    }
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey31", "ShowError('Current Step doesn's allow you to Remove Links');", true);
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey31", "ShowError('You are not allowed to Remove Links');", true);
            }
        }

        protected void cmdDeleteAddon_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            bool AllowDelete = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).isAllowed(AllowedAreas.Delete_Addons, objSes.UserID);
            if (AllowDelete == true)
            {
                AllowDelete = Convert.ToBoolean(ViewState["da"]);
                if (AllowDelete == true)
                {
                    int Task_ID = Convert.ToInt32(ViewState["tid"]);

                    Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                    DS_Tasks dsCurrentTask = objTask.Read(Task_ID, false, false, false);

                    List<DS_Tasks.tbltask_addonRow> drAddon = dsCurrentTask.tbltask_addon.Where(x => x.Task_Addon_ID == Convert.ToInt32(hndDeleteAddon.Value)).ToList();
                    if (drAddon.Count > 0 && drAddon[0].Posted_By == objSes.UserID)
                    {
                        DeleteReason del = objTask.DeleteTaskAddon(Convert.ToInt32(hndDeleteAddon.Value));
                        if (del.Deleted == false)
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey32", "ShowError('" + del.Reason + "');", true);
                        }
                        else
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey32", "ShowSuccess('Task Addon Successfully Deleted');", true);

                            DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(dsCurrentTask.tbltasks[0].Walkflow_ID, false, false);
                            DS_Tasks dsTask = null;
                            FillTaskData(Task_ID, ref dsTask, dsWF, objTask);

                            DS_Tasks dsSubs = objTask.GetActiveSubTasks(Task_ID);
                            Script_Generator objScripts = new Script_Generator();

                            LoadHistory(objScripts, dsTask, dsSubs, objSes);
                        }
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey32", "ShowError('Task Addon Cannot be Deleted');", true);
                    }
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey32", "ShowError('Current Step doesn't allow you to Delete Task Addons');", true);
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey32", "ShowError('You are not allowed to Delete Task Addons');", true);
            }
        }

        protected void cmdDeleteComment_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            bool AllowDelete = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).isAllowed(AllowedAreas.Delete_Comments, objSes.UserID);
            if (AllowDelete == true)
            {
                AllowDelete = Convert.ToBoolean(ViewState["dc"]);
                if (AllowDelete == true)
                {
                    int Task_ID = Convert.ToInt32(ViewState["tid"]);

                    Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                    DS_Tasks dsCurrentTask = objTask.Read(Task_ID, false, false, false);

                    List<DS_Tasks.tbltask_commentsRow> drComments = dsCurrentTask.tbltask_comments.Where(x => x.Task_Comment_ID == Convert.ToInt32(hndDeleteComment.Value)).ToList();
                    if (drComments.Count > 0 && drComments[0].Commented_By == objSes.UserID)
                    {
                        DeleteReason del = objTask.DeleteTaskComment(Convert.ToInt32(hndDeleteComment.Value));
                        if (del.Deleted == false)
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey33", "ShowError('" + del.Reason + "');", true);
                        }
                        else
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey33", "ShowSuccess('Task Comment Successfully Deleted');", true);

                            DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(dsCurrentTask.tbltasks[0].Walkflow_ID, false, false);
                            DS_Tasks dsTask = null;
                            FillTaskData(Task_ID, ref dsTask, dsWF, objTask);

                            DS_Tasks dsSubs = objTask.GetActiveSubTasks(Task_ID);
                            Script_Generator objScripts = new Script_Generator();

                            LoadHistory(objScripts, dsTask, dsSubs, objSes);
                        }
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey33", "ShowError('Task Comment Cannot be Deleted');", true);
                    }
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey33", "ShowError('Current Step doesn't allow you to Delete Task Comments');", true);
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey33", "ShowError('You are not allowed to Delete Task Comments');", true);
            }
        }

        protected void cmdChaangeQueue_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            if (objTask.UpdateTaskQueue(Task_ID, Convert.ToInt32(cboQueue.SelectedItem.Value)))
            {
                lblQueue.Text = cboQueue.SelectedItem.Text;
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey34", "ShowSuccess('Task Queue Successfully Updated');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey34", "ShowError('Task Queue could not be Updated');", true);
            }
        }

        protected void cmdSubTaskFromDoc_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            int Document_ID = Convert.ToInt32(hndAttachDoc_ID.Value);
            DS_Documents dsDoc = ObjectCreator.GetDocuments(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading).Read(Document_ID);
            if (dsDoc.tbldocuments.Rows.Count > 0)
            {
                DS_Doc_Project dsDP = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type).Read(dsDoc.tbldocuments[0].Document_Project_ID);
                if (dsDP.tbldocument_project.Rows.Count > 0)
                {
                    int SubWF_ID = Convert.ToInt32(cboSubWFForDocs.SelectedItem.Value);
                    Workflow objWorkflow = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                    DS_Workflow dsSWF = objWorkflow.Read(SubWF_ID);
                    if (dsSWF.tblwalkflow.Rows.Count > 0)
                    {
                        Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                        int Parent_Task_ID = Convert.ToInt32(ViewState["tid"]);

                        if (UserAllowedToCreateTasks(dsSWF, objTask, objSes))
                        {
                            DS_DP_Workflow dsDPWF = ObjectCreator.GetDocument_Project_Workflow(objSes.Connection, objSes.DB_Type).ReadAllForWF(SubWF_ID);
                            List<DS_DP_Workflow.tbldocument_project_workflowsRow> drAutos = dsDPWF.tbldocument_project_workflows.Where(x => x.Document_Project_ID == dsDP.tbldocument_project[0].Document_Project_ID).ToList();
                            if (drAutos.Count > 0)
                            {
                                Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
                                N_Ter.Customizable.Master_Tables objMasters = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);

                                DS_Master_Tables dsExMaster = null;
                                if (dsSWF.tblwalkflow[0].Exrta_Field_Type == 3 && dsSWF.tblwalkflow[0].Extra_Field_Master_Table_ID > 0)
                                {
                                    dsExMaster = objMasters.GetMasterTable(dsSWF.tblwalkflow[0].Extra_Field_Master_Table_ID);
                                }

                                DS_Master_Tables dsExMaster2 = null;
                                if (dsSWF.tblwalkflow[0].Exrta_Field2_Type == 3 && dsSWF.tblwalkflow[0].Extra_Field2_Master_Table_ID > 0)
                                {
                                    dsExMaster2 = objMasters.GetMasterTable(dsSWF.tblwalkflow[0].Extra_Field2_Master_Table_ID);
                                }

                                bool HasEF1 = false;
                                bool HasEF2 = false;

                                if (dsSWF.tblwalkflow[0].Extra_Field_Task_Start == true && dsSWF.tblwalkflow[0].Exrta_Field_Naming.Trim() != "")
                                {
                                    HasEF1 = true;
                                }
                                if (dsSWF.tblwalkflow[0].Extra_Field2_Task_Start == true && dsSWF.tblwalkflow[0].Exrta_Field2_Naming.Trim() != "")
                                {
                                    HasEF2 = true;
                                }

                                string strEF1, strEF2;
                                int intEF1, intEF2;
                                List<DS_Documents.tbldocument_tagsRow> drTags;

                                string strError1 = "";
                                string strError2 = "";
                                List<DS_Master_Tables.tblDataRow> drM;

                                int Current_Step_ID = dsSWF.tblworkflow_steps[0].Workflow_Step_ID;
                                int User_ID = objSes.UserID;

                                int SubTasksCount = 0;
                                DS_Tasks dsParentTask = objTask.Read(Parent_Task_ID, false, false, false);

                                Common_Actions objCom = new Common_Actions();

                                foreach (DS_DP_Workflow.tbldocument_project_workflowsRow row in drAutos)
                                {
                                    strEF1 = "";
                                    if (HasEF1 && row.Extra_Field_1_Index_ID != 0)
                                    {
                                        if (row.Extra_Field_1_Index_ID == -1)
                                        {
                                            strEF1 = dsDoc.tbldocuments[0].Document_No;
                                        }
                                        else
                                        {
                                            drTags = dsDoc.tbldocument_tags.Where(x => x.Document_Project_Index_ID == row.Extra_Field_1_Index_ID).ToList();
                                            if (drTags.Count > 0)
                                            {
                                                strEF1 = drTags[0].Tag_Value;
                                            }
                                            else
                                            {
                                                strEF1 = "-";
                                            }
                                        }
                                    }
                                    strEF2 = "";
                                    if (HasEF2 && row.Extra_Field_2_Index_ID != 0)
                                    {
                                        if (row.Extra_Field_2_Index_ID == -1)
                                        {
                                            strEF2 = dsDoc.tbldocuments[0].Document_No;
                                        }
                                        else
                                        {
                                            drTags = dsDoc.tbldocument_tags.Where(x => x.Document_Project_Index_ID == row.Extra_Field_2_Index_ID).ToList();
                                            if (drTags.Count > 0)
                                            {
                                                strEF2 = drTags[0].Tag_Value;
                                            }
                                            else
                                            {
                                                strEF2 = "-";
                                            }
                                        }
                                    }
                                    List<string> EF1s = new List<string>();
                                    EF1s.Add(strEF1);

                                    DateTime DueDate = DateTime.Today.Date;
                                    bool CreateTask = true;

                                    if (dsParentTask.tbltasks[0].Schedule_Line_ID > 0)
                                    {
                                        Workflow_Schedules objWFS = ObjectCreator.GetWorkflow_Schedules(objSes.Connection, objSes.DB_Type);
                                        DS_Workflow_Scedules.tblworkflow_schedules_dataRow drLine = objWFS.ReadScheduleDataLine(dsParentTask.tbltasks[0].Schedule_Line_ID);
                                        DueDate = drLine.Date_Data;
                                    }
                                    else if (objCom.ValidateDateTime(lblDueDate.Text.Trim() == "" ? string.Format("{0:" + Constants.DateFormat + " HH:mm}", DateTime.Now) : lblDueDate.Text, ref DueDate) == false)
                                    {
                                        CreateTask = false;
                                        ClientScriptManager cs = Page.ClientScript;
                                        cs.RegisterStartupScript(this.GetType(), "AKey9" + row.DP_WF_ID, "ShowError('Invalid Due Date');", true);
                                    }
                                    if (CreateTask)
                                    {
                                        DS_Tasks.tbltask_historyRow drLastTaskHistory = dsParentTask.tbltask_history.OrderByDescending(x => x.Task_Update_ID).First();

                                        int SubTaskNumber = objTask.ReadLastSubTaskNumber(Parent_Task_ID);
                                        string NewTaskNumber = dsParentTask.tbltasks[0].Task_Number + "_" + (SubTaskNumber + 1);

                                        string AditionalComment = "";

                                        List<StepPostData> InitStepPosts = new List<StepPostData>();
                                        ActionValidated Act = objCus.CustomTaskStartValidations(ref User_ID, ref SubWF_ID, ref Current_Step_ID, ref strEF1, ref strEF2, ref AditionalComment, ref InitStepPosts, dsParentTask.tbltasks[0].Entity_L2_ID, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

                                        if (Act.Validated == true)
                                        {
                                            intEF1 = 0;
                                            intEF2 = 0;

                                            if (SubWF_ID != dsSWF.tblwalkflow[0].Walkflow_ID)
                                            {
                                                DS_Workflow dsSWFTemp = objWorkflow.Read(SubWF_ID);
                                                if (dsSWFTemp.tblwalkflow.Rows.Count > 0)
                                                {
                                                    if (dsSWFTemp.tblwalkflow[0].Exrta_Field_Type == 3 && dsSWFTemp.tblwalkflow[0].Extra_Field_Master_Table_ID > 0)
                                                    {
                                                        DS_Master_Tables dsExMasterTemp = objMasters.GetMasterTable(dsSWFTemp.tblwalkflow[0].Extra_Field_Master_Table_ID);
                                                        drM = dsExMasterTemp.tblData.Where(x => x.Data.Trim() == strEF1.Trim()).ToList();
                                                        if (drM.Count > 0)
                                                        {
                                                            intEF1 = drM[0].Data_ID;
                                                        }
                                                    }

                                                    if (dsSWFTemp.tblwalkflow[0].Exrta_Field2_Type == 3 && dsSWFTemp.tblwalkflow[0].Extra_Field2_Master_Table_ID > 0)
                                                    {
                                                        DS_Master_Tables dsExMaster2Temp = objMasters.GetMasterTable(dsSWFTemp.tblwalkflow[0].Extra_Field2_Master_Table_ID);
                                                        drM = dsExMaster2Temp.tblData.Where(x => x.Data.Trim() == strEF2.Trim()).ToList();
                                                        if (drM.Count > 0)
                                                        {
                                                            intEF2 = drM[0].Data_ID;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    CreateTask = false;
                                                }
                                            }
                                            else
                                            {
                                                if (dsSWF.tblwalkflow[0].Exrta_Field_Type == 3 && dsSWF.tblwalkflow[0].Extra_Field_Master_Table_ID > 0)
                                                {
                                                    drM = dsExMaster.tblData.Where(x => x.Data.Trim() == strEF1.Trim()).ToList();
                                                    if (drM.Count > 0)
                                                    {
                                                        intEF1 = drM[0].Data_ID;
                                                    }
                                                }

                                                if (dsSWF.tblwalkflow[0].Exrta_Field2_Type == 3 && dsSWF.tblwalkflow[0].Extra_Field2_Master_Table_ID > 0)
                                                {
                                                    drM = dsExMaster2.tblData.Where(x => x.Data.Trim() == strEF2.Trim()).ToList();
                                                    if (drM.Count > 0)
                                                    {
                                                        intEF2 = drM[0].Data_ID;
                                                    }
                                                }
                                            }
                                            if (CreateTask)
                                            {
                                                int MaxID = objTask.InsertSub(NewTaskNumber, SubWF_ID, User_ID, Current_Step_ID, Parent_Task_ID, drLastTaskHistory.Task_Update_ID, DueDate, dsParentTask.tbltasks[0].Entity_L2_ID, dsParentTask.tbltasks[0].Schedule_Line_ID, strEF1, strEF2, intEF1, intEF2, objSes.FullName, 0, dsParentTask.tbltasks[0].Queue_ID, objCus, objMasters);
                                                SubTasksCount++;
                                                if (Convert.ToInt32(hndAttachDoc_ID.Value) > 0)
                                                {
                                                    objTask.AddLink(MaxID, Convert.ToInt32(hndAttachDoc_ID.Value), objSes.UserID, false);
                                                }

                                                if (AditionalComment.Trim() != "")
                                                {
                                                    objTask.AddComment(MaxID, 1, AditionalComment, "3", "Startup Validations");
                                                }

                                                if (InitStepPosts.Count > 0)
                                                {
                                                    Common_Task_Actions objComTsk = new Common_Task_Actions();
                                                    foreach (StepPostData PostData in InitStepPosts)
                                                    {
                                                        if (PostData.Queue_ID > 0)
                                                        {
                                                            objTask.UpdateTaskQueue(MaxID, PostData.Queue_ID);
                                                        }
                                                        if (PostData.Next_Step_ID > 0)
                                                        {
                                                            objComTsk.SaveStep(MaxID, PostData.TaskFields, PostData.TaskExtraFields, PostData.TaskDocActions, objTask, objSes, objCus);
                                                            objTask.UpdateTaskHistory(MaxID, PostData.Next_Step_ID, objSes.UserID, objSes.FullName, true, objCus, objMasters);
                                                        }
                                                        if (PostData.TaskTempSave.Count > 0)
                                                        {
                                                            foreach (Task_Field_Temp_Save TempSave in PostData.TaskTempSave)
                                                            {
                                                                objTask.SaveTaskUpdate(MaxID, TempSave.Workflow_Step_Field_ID, TempSave.Field_Value);
                                                            }
                                                        }
                                                    }
                                                }

                                                DS_Tasks dsNewTask = objTask.Read(MaxID, false, false, false);
                                                if (dsNewTask.tbltasks[0].IsIs_Auto_SubmitNull() == false && dsNewTask.tbltasks[0].Is_Auto_Submit)
                                                {
                                                    Common_Task_Actions objCommAct = new Common_Task_Actions();
                                                    while (dsNewTask.tbltasks[0].IsIs_Auto_SubmitNull() == false && dsNewTask.tbltasks[0].Is_Auto_Submit)
                                                    {
                                                        if (objCommAct.AutoSubmit(dsNewTask, objTask, objWorkflow, objCus, objMasters, objSes.UserID, objSes.FullName))
                                                        {
                                                            dsNewTask = objTask.Read(MaxID, false, false, false);
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
                                                strError2 = "Some Tasks Failed Custom Validations";
                                            }
                                        }
                                        else
                                        {
                                            strError2 = "Some Tasks Failed Custom Validations";
                                        }
                                    }
                                    else
                                    {
                                        strError1 = "Some Tasks Violated Data Formats";
                                    }
                                }
                                if (SubTasksCount > 0)
                                {
                                    DS_Tasks dsSubs = objTask.GetActiveSubTasks(Parent_Task_ID);
                                    LoadSubTasks(dsSubs, Parent_Task_ID, dsParentTask.tbltasks[0].Task_Number + " - " + dsParentTask.tbltasks[0].Workflow_Name + " - " + dsParentTask.tbltasks[0].Display_Name);
                                    DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(dsParentTask.tbltasks[0].Walkflow_ID, false, false);
                                    DS_Tasks dsTask = null;
                                    FillTaskData(Parent_Task_ID, ref dsTask, dsWF, objTask);

                                    Script_Generator objScripts = new Script_Generator();
                                    LoadHistory(objScripts, dsTask, dsSubs, objSes);
                                    ClientScriptManager cs = Page.ClientScript;
                                    cs.RegisterStartupScript(this.GetType(), "AKey40", "ShowSuccess('" + SubTasksCount + " Sub Task(s) Created');", true);
                                }

                                if (strError1.Trim() != "")
                                {
                                    ClientScriptManager cs = Page.ClientScript;
                                    cs.RegisterStartupScript(this.GetType(), "AKey42", "ShowError('" + strError1 + "');", true);
                                }
                                if (strError2.Trim() != "")
                                {
                                    ClientScriptManager cs = Page.ClientScript;
                                    cs.RegisterStartupScript(this.GetType(), "AKey43", "ShowError('" + strError2 + "');", true);
                                }
                            }
                            else
                            {
                                ClientScriptManager cs = Page.ClientScript;
                                cs.RegisterStartupScript(this.GetType(), "AKey38", "ShowError('There are No Eligibal Sub Workflows for this Document');", true);
                            }
                        }
                        else
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey39", "ShowError('User Not Allowed to Create Sub Tasks');", true);
                        }
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey37", "ShowError('Error Reading the Sub Workflow');", true);
                    }
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey36", "ShowError('Error Reading the Document Project');", true);
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey35", "ShowError('Error Reading the Document');", true);
            }
        }

        private void FillTaskData(int Task_ID, ref DS_Tasks dsTask, DS_Workflow dsWF, Tasks objTask)
        {
            if (dsTask == null)
            {
                dsTask = objTask.Read(Task_ID, dsWF.tblwalkflow[0].Show_Parent_Task_History, dsWF.tblwalkflow[0].Show_SubTask_Documents, dsWF.tblwalkflow[0].Show_SubTask_Comments);
            }
            else
            {
                if ((dsTask.tbltasks[0].Parent_Task_ID > 0 && dsWF.tblwalkflow[0].Show_Parent_Task_History) || dsWF.tblwalkflow[0].Show_SubTask_Documents || dsWF.tblwalkflow[0].Show_SubTask_Comments)
                {
                    foreach (System.Data.DataTable tbl in dsTask.Tables)
                    {
                        tbl.Rows.Clear();
                    }
                    dsTask = objTask.Read(Task_ID, dsWF.tblwalkflow[0].Show_Parent_Task_History, dsWF.tblwalkflow[0].Show_SubTask_Documents, dsWF.tblwalkflow[0].Show_SubTask_Comments);
                }
            }
        }

        protected void cmdSaveStepDeadlines_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            Common_Actions objCom = new Common_Actions();

            string[] StepTLSplits = hndStepDeadline.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            int Task_ID = objCtrlList.Task_ID;
            DS_Tasks ds = new DS_Tasks();

            foreach (string StepTL in StepTLSplits)
            {
                if (StepTL.Trim() != "")
                {
                    int Step_ID;
                    DateTime dtDeadline = DateTime.Today;

                    string[] StepTLData = StepTL.Split('^');

                    if (int.TryParse(StepTLData[0], out Step_ID))
                    {
                        if (objCom.ValidateDate(StepTLData[1], ref dtDeadline))
                        {
                            DS_Tasks.tbltask_timelineRow dr = ds.tbltask_timeline.Newtbltask_timelineRow();
                            dr.Task_ID = Task_ID;
                            dr.Workflow_Step_ID = Step_ID;
                            dr.Step_Deadline = dtDeadline;
                            ds.tbltask_timeline.Rows.Add(dr);
                        }
                    }
                }
            }
            if (ds.tbltask_timeline.Count > 0)
            {
                Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                if (objTask.UpdateTaskTimeline(Task_ID, ds.tbltask_timeline))
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey44", "ShowSuccess('Task Timeline Successfully Saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey44", "ShowError('Task Timeline could not be Saved');", true);
                }
            }
        }

        protected void cmdReplaceFile_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            bool AllowDelete = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).isAllowed(AllowedAreas.Delete_Docs, objSes.UserID);
            if (AllowDelete == true)
            {
                AllowDelete = Convert.ToBoolean(ViewState["dd"]);
                if (AllowDelete == true)
                {
                    int Doc_ID = 0;
                    if (hndReplaceFile.Value.Trim() == "0")
                    {
                        Doc_ID = Convert.ToInt32(cboFileSource.SelectedItem.Value);
                    }
                    else
                    {
                        Doc_ID = Convert.ToInt32(cboFileResult.SelectedItem.Value);
                    }

                    int Task_ID = Convert.ToInt32(ViewState["tid"]);
                    Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                    DS_Tasks dsTS = objTask.Read(Task_ID, false, false, false);

                    List<DS_Tasks.tbltask_docsRow> drDoc = dsTS.tbltask_docs.Where(x => x.Task_Doc_ID == Doc_ID).ToList();

                    if (drDoc.Count > 0)
                    {
                        DS_Entity_Level_2.tblentity_level_2Row drComp = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type).Read(dsTS.tbltasks[0].Entity_L2_ID);
                        string SaveLocation = objSes.PhysicalRoot + "\\nter_app_uploads\\client_docs\\" + drComp.Folder_Name + "\\Workflow_Uploads\\" + DateTime.Today.Year + "\\" + DateTime.Today.Month;
                        string SaveLocationDB = "nter_app_uploads\\client_docs\\" + drComp.Folder_Name + "\\Workflow_Uploads\\" + DateTime.Today.Year + "\\" + DateTime.Today.Month;

                        if (System.IO.Directory.Exists(SaveLocation) == false)
                        {
                            System.IO.Directory.CreateDirectory(SaveLocation);
                        }

                        string FileName = drDoc[0].Doc_Path.Split('\\').Last();
                        FileName = FileName.Substring(0, FileName.LastIndexOf('.'));
                        string NewExtension = fulNewFile.PostedFile.FileName.Split('.').Last();
                        FileName = FileName + "." + NewExtension;

                        fulNewFile.PostedFile.SaveAs(SaveLocation + "\\" + FileName);
                        if (objTask.UpdateFilePath(drDoc[0].Task_Doc_ID, SaveLocationDB + "\\" + FileName, SaveLocation + "\\" + FileName))
                        {
                            DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(dsTS.tbltasks[0].Walkflow_ID);
                            DS_Tasks dsTask = null;
                            FillTaskData(Task_ID, ref dsTask, dsWF, objTask);

                            LoadAttachments(dsWF.tblwalkflow[0].Document_Project_ID, dsTask.tbltask_docs, dsTask.tbltask_attachment, dsTask.tbltask_comments, objSes);
                            FillReplaceFileDropDown(dsTask.tbltask_docs, objSes);
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey40", "ShowSuccess('File Replaced Successfully');", true);
                        }
                        else
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey45", "ShowError('File Upload Error');", true);
                        }
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey45", "ShowError('Data Missmatch, Please reload the Task');", true);
                    }
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey45", "ShowError('Current Step doesn's allow you to Delete Documents');", true);
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey45", "ShowError('You are not allowed to Delete Documents');", true);
            }
        }
    }
}