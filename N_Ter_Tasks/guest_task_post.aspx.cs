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
    public partial class guest_task_post : System.Web.UI.Page
    {
        private Task_Controls_Main objCtrlList = new Task_Controls_Main();

        public string HelpScript = "";
        public string HelpPanelResizeScript = "";
        public string Required_Fields = "";
        public string Custom_Scripts = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            LoadTask(objSes);
        }

        private void LoadGuestHelp(DS_Workflow dsWF)
        {
            altHelp.Visible = false;
            if (dsWF.tblwalkflow.Rows.Count > 0 && dsWF.tblwalkflow[0].Guest_Help_Task_Post.Trim() != "")
            {
                altHelp.Visible = true;
                ltrGuestHelp.Text = dsWF.tblwalkflow[0].Guest_Help_Task_Post;
            }
        }

        private void LoadTask(SessionObject objSes)
        {
            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
            if (string.IsNullOrEmpty(Convert.ToString(Request.QueryString["tid"])) == false)
            {
                int Task_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["tid"])));
                if (string.IsNullOrEmpty(Convert.ToString(Request.QueryString["stp"])) == false)
                {
                    int Step_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["stp"])));

                    Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                    DS_Tasks dsTask = objTask.Read(Task_ID, false, false, false);
                    if (dsTask.tbltasks.Rows.Count > 0)
                    {
                        if (dsTask.tbltasks[0].Current_Step_ID == Step_ID)
                        {
                            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                            DS_Workflow dsWorkflow = objWF.ReadStep(dsTask.tbltasks[0].Current_Step_ID);

                            if (dsWorkflow.tblworkflow_steps.Rows.Count > 0)
                            {
                                LoadGuestHelp(dsWorkflow);
                                int Guest_User_Group = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type).GetGroupID(UserGroupTypes.Guest);
                                if (dsWorkflow.tblworkflow_steps[0].User_Group_Involved == Guest_User_Group)
                                {
                                    divError.Visible = false;
                                    pnlStepData.Visible = true;

                                    ViewState["tid"] = Task_ID;

                                    if (dsTask.tbltasks[0].Parent_Task_ID > 0 && dsWorkflow.tblwalkflow[0].Show_Parent_Task_History)
                                    {
                                        foreach (System.Data.DataTable tbl in dsTask.Tables)
                                        {
                                            tbl.Rows.Clear();
                                        }
                                        dsTask = objTask.Read(Task_ID, true, false, false);
                                    }

                                    LoadTaskDetails(dsTask, dsWorkflow, objSes);
                                    LoadCustomScripts(dsTask, objSes);

                                    if (dsWorkflow.tblworkflow_steps[0].Show_Documents == true || dsWorkflow.tblworkflow_steps[0].Show_History == true)
                                    {
                                        divPost.Attributes["class"] = "col-md-7";
                                        if (dsWorkflow.tblworkflow_steps[0].Show_Documents == true)
                                        {
                                            LoadDocuments(dsTask.tbltask_docs, dsTask.tbltask_attachment, dsTask.tbltask_comments, objSes);
                                        }
                                        else
                                        {
                                            divDocuments.Visible = false;
                                        }

                                        if (dsWorkflow.tblworkflow_steps[0].Show_History == true)
                                        {
                                            DS_Tasks dsAllSubTasks = objTask.GetSubTasks(Task_ID);
                                            LoadHistory(dsTask, dsAllSubTasks, objSes);
                                        }
                                        else
                                        {
                                            divHistory.Visible = false;
                                        }
                                    }
                                    else
                                    {
                                        divOther.Visible = false;
                                    }
                                    cmdSubmit.Attributes.Add("onClick", "return ValidatRequiredFields();");
                                }
                                else
                                {
                                    divNoError.Visible = false;
                                }
                            }
                            else
                            {
                                divNoError.Visible = false;
                            }
                        }
                        else
                        {
                            divNoError.Visible = false;
                        }
                    }
                    else
                    {
                        divNoError.Visible = false;
                    }
                }
                else
                {
                    divNoError.Visible = false;
                }
            }
            else
            {
                divNoError.Visible = false;
            }
        }

        private void LoadTaskDetails(DS_Tasks dsTask, DS_Workflow dsWorkflow, SessionObject objSes)
        {
            Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
            N_Ter.Common.Script_Generator objScripts = new N_Ter.Common.Script_Generator();
            string strRequiredFieldValidation = "";
            Required_Fields = "function ValidatRequiredFields() {" + "\r\n" +
                                      "remove_field_erros();" + "\r\n" +
                                      "var ret = true;" + "\r\n";

            Required_Fields = Required_Fields + objCus.CustomTaskPostScripts(dsTask, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            string strOldFieldValidation = "";

            pnlStepData.Controls.Clear();

            int ControlIndex = 0;
            List<string> Help_Texts = new List<string>();

            DS_Settings dsSettings = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type).Read();
            N_Ter.Customizable.Master_Tables objMasterTables = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);
            N_Ter.Common.Common_Task_Actions objTskAct = new N_Ter.Common.Common_Task_Actions();

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
                    divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref objCtrlList, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, false));
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
                        divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref objCtrlList, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, false));
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

            objCus.CustomTaskPostFormAdjustments(dsTask, ref objCtrlList, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
        }

        private void LoadCustomScripts(DS_Tasks dsTask, SessionObject objSes)
        {
            string CusScript = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).LoadTaskRelatedScripts(dsTask, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            Custom_Scripts = CusScript;
        }

        private void LoadHistory(DS_Tasks dsTask, DS_Tasks dsSubs, SessionObject objSes)
        {
            Users objUser = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);

            N_Ter.Common.Common_Actions objCom = new N_Ter.Common.Common_Actions();

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
            List<int> UpdateUserIDs;

            N_Ter.Common.Common_Task_Actions objCommAct = new N_Ter.Common.Common_Task_Actions();

            List<string> TaskProgress = new List<string>();

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
                                        (row.isParentRecord ? "<div class='history_parent badge badge-primary' title='' data-toggle='tooltip' data-placement='top' data-original-title='Parent Task'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
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

                TaskProgress.Add("<div class=\"comment no-border\">" + "\r\n" +
                                    "<img src=\"" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "\" alt=\"Profile Picture\" class=\"comment-avatar\">" + "\r\n" +
                                    "<div class=\"comment-body\">" + "\r\n" +
                                        "<div class=\"comment-text\">" + "\r\n" +
                                            "<div class=\"panel tl-body p8 \">" + "\r\n" +
                                                (row.isParentRecord ? "<div class='history_parent badge badge-primary' title='' data-toggle='tooltip' data-placement='top' data-original-title='Parent Task'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
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
                                  "</div>" + "\r\n");

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
                        SubTasksList = SubTasksList + drSubTask.Task_Number + " - " + drSubTask.Workflow_Name + "</br>";
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

            foreach (DS_Tasks.tbltask_commentsRow row in dsTask.tbltask_comments)
            {
                string cssClass = row.Comment_Type.Equals("1") ? "bg-danger" : row.Comment_Type.Equals("2") ? "bg-info" : "bg-success";
                HistoryText = "<div class=\"tl-entry\">" + "\r\n" +
                                "<div class=\"tl-time\">" + row.Comment_Date.Day + "|" + string.Format("{0:HH:mm}", row.Comment_Date) + "</div>" + "\r\n" +
                                    "<div class=\"tl-icon " + cssClass + "\">\r\n" +
                                        "<i class=\"fa fa-comments-o\"></i>\r\n" +
                                    "</div>" + "\r\n" +
                                "<div class=\"panel tl-body p8\">\r\n" +
                                (row.isParentRecord ? "<div class='history_parent badge badge-primary' title='' data-toggle='tooltip' data-placement='top' data-original-title='Parent Task'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
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
                            splits[i] = splits[i].Substring(0, EndIndex) + "</span>" + splits[i].Substring(EndIndex);
                        }
                        else
                        {
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

        private void LoadDocuments(DS_Tasks.tbltask_docsDataTable tbl, DS_Tasks.tbltask_attachmentDataTable tblAtt, DS_Tasks.tbltask_commentsDataTable tblComms, SessionObject objSes)
        {
            ltrAttachments.Text = "";
            string[] fileSplits;
            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();

            List<DS_Tasks.tbltask_docsRow> drDocs;
            List<DS_Tasks.tbltask_attachmentRow> drAtts;

            if (objSes.TaskDocDir)
            {
                drDocs = tbl.OrderBy(y => y.Task_Doc_Type).ThenBy(z => z.Uploaded_Date).ToList();
                drAtts = tblAtt.OrderBy(y => y.Created_Date).ToList();
            }
            else
            {
                drDocs = tbl.OrderBy(y => y.Task_Doc_Type).ThenByDescending(z => z.Uploaded_Date).ToList();
                drAtts = tblAtt.OrderByDescending(y => y.Created_Date).ToList();
            }

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
                    if (row.Uploaded_By == objSes.UserID || row.Access_Level >= objSes.AccLevel)
                    {
                        fileSplits = row.Doc_Path.Split('\\');
                        ltrAttachments.Text = ltrAttachments.Text + "<div class='comment'>" + "\r\n" +
                                                                    " <img src='" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "' alt='Profile Picture' class='comment-avatar'>" + "\r\n" +
                                                                    " <div class='comment-body'>" +
                                                                    "   <div class='comment-by'>" + row.Uploaded_By_Name + "</div>" + "\r\n" +
                                                                    "   <div class='comment-text'>" +
                                                                    "    <a href='document_preview.aspx?fid=" + objURL.Encrypt(Convert.ToString(row.Task_Doc_ID)) + "' target='_blank' class='media'>" +
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
                    if (row.Attached_By == objSes.UserID || row.Access_Level >= objSes.AccLevel)
                    {
                        fileSplits = row.Document_Path.Split('\\');
                        ltrAttachments.Text = ltrAttachments.Text + "<div class='comment'>" + "\r\n" +
                                                                   " <img src='" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "' alt='Profile Picture' class='comment-avatar'>" + "\r\n" +
                                                                   " <div class='comment-body'>" +
                                                                   "   <div class='comment-by'>" + row.Attached_By_Name + "</div>" + "\r\n" +
                                                                   "   <div class='comment-text'>" +
                                                                   "    <a href='document_preview_dp.aspx?fid=" + objURL.Encrypt(Convert.ToString(row.Document_ID)) + "' target='_blank' class='media'>" +
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
            }

            if (drDocs.Count > 0 || drAtts.Count > 0)
            {
                divNoDocs.Visible = false;
            }
            else
            {
                divNoDocs.Visible = true;
            }
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            try
            {
                if (NextStepSubmit(objSes))
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Your Submission is Successfull');", true);

                    LoadTask(objSes);
                    foreach (Task_Controls item in objCtrlList.Controls)
                    {
                        if (item.UI_Type == UI_Types.DropDowns)
                        {
                            DropDownList drp = (DropDownList)item.UI_Control;
                            drp.SelectedIndex = 0;
                        }
                        if (item.UI_Type == UI_Types.Checkbox)
                        {
                            CheckBox chk = (CheckBox)item.UI_Control;
                            chk.Checked = false;
                        }
                        else
                        {
                            TextBox txt = (TextBox)item.UI_Control;
                            txt.Text = "";
                        }
                    }
                }
                else
                {
                    LoadTask(objSes);
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Your Submission is not Successfull, Please contact the System's Administrator');", true);
                }
            }
            catch (Exception ex)
            {
                LoadTask(objSes);
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + ex.Message + "');", true);
            }
        }

        private bool NextStepSubmit(SessionObject objSes)
        {
            bool ret = true;

            DS_Settings dsSett = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type).Read();
            if (dsSett.tblsettings[0].User_Group_Guest > 0)
            {
                DS_Users dsUsr = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type).ReadUsers(dsSett.tblsettings[0].User_Group_Guest);
                if (dsUsr.tblusers.Rows.Count > 0)
                {
                    int User_ID = dsUsr.tblusers[0].User_ID;
                    string Full_Name = dsUsr.tblusers[0].Full_Name;

                    try
                    {
                        int Task_ID = Convert.ToInt32(ViewState["tid"]);

                        Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                        DS_Tasks dsCurrentTask = objTask.Read(Task_ID, false, false, false);

                        Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                        DS_Workflow dsCurrentStep = objWF.ReadStep(dsCurrentTask.tbltasks[0].Current_Step_ID);

                        if (dsCurrentStep.tblworkflow_steps[0].Creator_Cannot_Submit == true && dsCurrentTask.tbltasks[0].Creator_ID == User_ID)
                        {
                            throw new Exception("You cannot Approve/Validate a task you created");
                        }
                        else
                        {
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
                                objCommAct.PrepareControlsDataSet(null, objSes, false, ref objCtrlList);
                                bool All_Fields_Validated = objCommAct.Validate_Filled_Data(objCtrlList, ref Invalid_Fields);

                                if (All_Fields_Validated == false)
                                {
                                    throw new Exception("Data Format Error - Please check the Following Values<br/>" + Invalid_Fields);
                                }
                                else
                                {
                                    Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
                                    N_Ter.Customizable.Master_Tables objMasters = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);

                                    DS_Tasks.tbltask_historyRow drLastTaskHistory = dsCurrentTask.tbltask_history.OrderByDescending(x => x.Task_Update_ID).First();
                                    DateTime LastUpdateDate = drLastTaskHistory.Posted_Date;

                                    int NextStepID = objCommAct.FindNextStep(LastUpdateDate, objWF, dsCurrentTask, dsCurrentStep, objCtrlList, User_ID);

                                    objTask.UpdateStepFinishedDetails(drLastTaskHistory.Task_Update_ID, User_ID, false);

                                    objCommAct.PrepareControlsDataSet(dsCurrentTask, objSes, true, ref objCtrlList);

                                    SaveStep(dsCurrentTask, objTask, objCtrlList, User_ID, objSes);

                                    objTask.UpdateTaskHistory(Task_ID, NextStepID, User_ID, Full_Name, true, objCus, objMasters);

                                    if (NextStepID > 0)
                                    {
                                        //check for auto submit steps
                                        DS_Tasks dsNewTask = objTask.Read(Task_ID, false, false, false);
                                        DS_Workflow dsNewStep = objWF.ReadStep(dsNewTask.tbltasks[0].Current_Step_ID);

                                        while (dsNewTask.tbltasks[0].Current_Step_ID != -1 && dsNewTask.tbltasks[0].Current_Step_ID != -2 && dsNewStep.tblworkflow_steps[0].Is_Auto_Submit)
                                        {
                                            if (objCommAct.AutoSubmit(dsNewTask, objTask, objWF, objCus, objMasters, User_ID, Full_Name))
                                            {
                                                dsNewTask = objTask.Read(Task_ID, false, false, false);
                                                dsNewStep = objWF.ReadStep(dsNewTask.tbltasks[0].Current_Step_ID);
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    Response.Redirect("guest_error.aspx?");
                }
            }
            else
            {
                Response.Redirect("guest_error.aspx?");
            }
            return ret;
        }

        private void SaveStep(DS_Tasks ds, Tasks objTask, Task_Controls_Main objList, int User_ID, SessionObject objSes)
        {
            Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
            N_Ter.Customizable.Master_Tables objMasters = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);

            List<DS_Tasks.tbltask_historyRow> drLastUpdate = ds.tbltask_history.OrderByDescending(x => x.Task_Update_ID).ToList();
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

            if (Request.QueryString["idt"] != null)
            {
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                string GuestIdentity = objURL.Decrypt(Convert.ToString(Request.QueryString["idt"]));
                lstTaskUpdates.Add(new Task_Field_Update
                {
                    Task_Update_ID = TaskUpdateID,
                    Workflow_Step_Field_ID = -1,
                    Field_Value = GuestIdentity,
                    User_ID = User_ID
                });
            }

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
    }
}