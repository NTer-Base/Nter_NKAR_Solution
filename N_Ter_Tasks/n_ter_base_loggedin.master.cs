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
    public partial class n_ter_base_loggedin : System.Web.UI.MasterPage
    {
        public string AlertsScript = "";
        public string HourlyAlertsScript = "";
        public string RefreshFreq;
        public string CurrentPage;
        public string ReletiveURL = "";
        public string BackURL = "";
        public string StartPageScript = "";

        protected void Page_Init(object sender, System.EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            n_ter_base pg = (n_ter_base)this.Master;
            pg.PageClass = "main-menu-animated";

            if (objSes.UserID <= 0)
            {
                N_Ter.Common.Common_Actions objCom = new N_Ter.Common.Common_Actions();
                Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority) + (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath) + "/?fwd=" + objCom.GetReletiveURL(Request));
            }
            else
            {
                if (objSes.isLocked)
                {
                    Response.Redirect("account_locked.aspx?");
                }
                else if (objSes.PWExpiry < DateTime.Now)
                {
                    Response.Redirect("change_password.aspx?");
                }
                else
                {
                    CurrentPage = pg.CurrentPage;
                    if (CurrentPage.Contains("task_list"))
                    {
                        N_Ter.Common.Common_Actions objCom = new N_Ter.Common.Common_Actions();
                        ReletiveURL = objCom.GetReletiveURL(Request);
                    }
                    else if (Request.QueryString["bck"] != null)
                    {
                        BackURL = Convert.ToString(Request.QueryString["bck"]);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            if (objSes.AltStart.Trim() == "")
            {
                StartPageScript = "clearDropDown(['" + cboMasterDueTime.ClientID + "', '" + cboMasterWorkflowCategory.ClientID + "', 'MasterWorkflowList']);\r\n" +
                                  "clearDateTextBox(['" + txtMasterDueDate.ClientID + "']);\r\n" +
                                  "clearTextBox(['" + txtMasterExtraField1.ClientID + "', '" + txtMasterExtraField2.ClientID + "']);\r\n" +
                                  "$('#" + txtMasterDueDate.ClientID + "').keyup();\r\n" +
                                  "$find('mpuMasterNewTask').show();\r\n" +
                                  "LoadMasterWorkflows();\r\n";
            }
            else
            {
                StartPageScript = "openPage('" + objSes.AltStart + "');";
            }
            RefreshFreq = objSes.RefFreq.ToString();

            Users objUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
            string PageName = CurrentPage;
            DS_Extra_Sections dsXtr = new N_Ter.Customizable.Custom_Lists().LoadExtraSections();
            if (objUsers.isPageAvailable(objSes.UserID, PageName) == true)
            {
                BuildAlerts(objSes);
                BuildMessages(objSes);
                BuildUserDetails(objSes);
                GetNoticeBoard(objSes);
            }
            else if (objUsers.isOtherSectionAvailable(objSes.UserID, PageName, dsXtr) == true)
            {
                BuildAlerts(objSes);
                BuildMessages(objSes);
                BuildUserDetails(objSes);
                GetNoticeBoard(objSes);
            }
            else
            {
                Response.Redirect("no_access.aspx?");
            }

            if (IsPostBack == false)
            {
                ltrMasterEL2.Text = objSes.EL2;
                LoadWorkflowCategories(objSes);
                LoadTaskQueues(objSes);
            }
            if (objSes.Enable_Alerts == true)
            {
                AlertsScript = "DisplayNotifications();";
            }
            else
            {
                AlertsScript = "";
            }
            if (objSes.EnableHrMsg)
            {
                HourlyAlertsScript = "hourlylogout();";
            }
            else
            {
                HourlyAlertsScript = "";
                cmdAutoLogoutHidden.Visible = false;
                cmdAutoLogoutHidden_ModalPopupExtender.Enabled = false;
                pnlAutoLogout.Visible = false;
            }
            cmdMasterStartTask.Attributes.Add("onClick", "return ValidateStartTask('" + hndMasterWorkflowID.ClientID + "', '" + hndMasterEL2.ClientID + "', '" + hndMasterDeadlineType.ClientID + "', '" + hndMasterScheduleElementID.ClientID + "', '" + txtMasterDueDate.ClientID + "', '" + objSes.EL2 + "');");
            cboMasterWorkflowCategory.Attributes.Add("onChange", "LoadMasterWorkflows();");
        }

        private void LoadWorkflowCategories(SessionObject objSes)
        {
            Workflow_Categories objWorkflowCategories = ObjectCreator.GetWorkflow_Categories(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWorkflowCategories.ReadAllForUserStart(objSes.UserID);
            cboMasterWorkflowCategory.DataSource = ds.tblworkflow_categories;
            cboMasterWorkflowCategory.DataValueField = "Workflow_Category_ID";
            cboMasterWorkflowCategory.DataTextField = "Workflow_Category_Name";
            cboMasterWorkflowCategory.DataBind();

            if (ds.tblworkflow_categories.Rows.Count > 0)
            {
                divAddTasks.Visible = true;
                pnlMasterNewTask.Visible = true;
            }
            else
            {
                divAddTasks.Visible = false;
                pnlMasterNewTask.Visible = false;
                hndMasterNewTask_ModalPopupExtender.Enabled = false;
            }
        }

        private void LoadTaskQueues(SessionObject objSes)
        {
            Task_Queues objQue = ObjectCreator.GetTask_Queues(objSes.Connection, objSes.DB_Type);
            DS_Task_Queues ds = objQue.ReadAll();
            cboMasterQueue.DataSource = ds.tbltask_queues;
            cboMasterQueue.DataValueField = "Queue_ID";
            cboMasterQueue.DataTextField = "Queue_Name";
            cboMasterQueue.DataBind();

            cboMasterQueue.Items.Insert(0, new ListItem("[Only on My Tasks List]", "0"));
        }

        protected void cmdMasterStartTask_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow objWorkflow = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow dsWF = objWorkflow.Read(Convert.ToInt32(hndMasterWorkflowID.Value.Split('|')[0]), false, false);

            int User_ID = objSes.UserID;
            if (dsWF.tblwalkflow[0].Allow_Assigning_For_Subordinates == true)
            {
                if (hndMasterSubordinateID.Value.Trim() != "" && Convert.ToInt32(hndMasterSubordinateID.Value) > 0)
                {
                    User_ID = Convert.ToInt32(hndMasterSubordinateID.Value);
                }
            }

            Tasks objTasks = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            bool CreateTask = true;
            if (dsWF.tblworkflow_steps.Rows.Count == 0)
            {
                CreateTask = false;
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKeyM2", "ShowError('Selected Workflow does not have any Steps');", true);
            }
            else
            {
                int CurrentTaskCount = objTasks.CurrentTaskCount(User_ID, Convert.ToInt32(hndMasterWorkflowID.Value.Split('|')[0]));
                if (CurrentTaskCount >= dsWF.tblwalkflow[0].Number_Of_Concurrent_Tasks_Allowed)
                {
                    Users objUser = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
                    DS_Users dsUser = objUser.Read(User_ID);
                    if (dsUser.tblusers[0].Override_Restrictions == false)
                    {
                        CreateTask = false;
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKeyM2_1", "ShowError('You have exceeded the number of tasks allowed from a single workflow');", true);
                    }
                    else
                    {
                        objUser.DisableOverride(User_ID);
                    }
                }
            }

            if (CreateTask)
            {
                DateTime DueDate = DateTime.Now;
                if (dsWF.tblwalkflow[0].Deadline_Type == 1)
                {
                    Common_Actions objCom = new Common_Actions();
                    if (objCom.ValidateDateTime(txtMasterDueDate.Text + " " + cboMasterDueTime.SelectedItem.Text, ref DueDate) == false)
                    {
                        CreateTask = false;
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKeyM2_2", "ShowError('Invalid Due Date');", true);
                    }
                    else if (DueDate < DateTime.Now)
                    {
                        CreateTask = false;
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKeyM2_3", "ShowError('Invalid Due Date');", true);
                    }
                }
                else if (dsWF.tblwalkflow[0].Deadline_Type == 2)
                {
                    if (Convert.ToInt32(hndMasterScheduleElementID.Value) > 0)
                    {
                        Workflow_Schedules objWFS = ObjectCreator.GetWorkflow_Schedules(objSes.Connection, objSes.DB_Type);
                        DS_Workflow_Scedules.tblworkflow_schedules_dataRow drLine = objWFS.ReadScheduleDataLine(Convert.ToInt32(hndMasterScheduleElementID.Value));
                        DueDate = drLine.Date_Data;
                    }
                    else
                    {
                        CreateTask = false;
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKeyM2_4", "ShowError('Please Select a Schedule');", true);
                    }
                }
                else if (dsWF.tblwalkflow[0].Deadline_Type == 3)
                {
                    if (DueDate.Minute > 30)
                    {
                        DueDate = DueDate.AddHours(1).AddMinutes(-DueDate.Minute);
                    }
                    else if (DueDate.Minute > 0)
                    {
                        DueDate = DueDate.AddMinutes(-DueDate.Minute).AddMinutes(30);
                    }
                    DueDate = DueDate.AddDays(dsWF.tblwalkflow[0].Number_Of_Days);
                }

                if (CreateTask)
                {
                    int Workflow_ID = Convert.ToInt32(hndMasterWorkflowID.Value.Split('|')[0]);
                    int Current_Step_ID = dsWF.tblworkflow_steps[0].Workflow_Step_ID;
                    string ExtraField1 = txtMasterExtraField1.Text;
                    string ExtraField2 = txtMasterExtraField2.Text;
                    int ExtraField_ID = 0;
                    int ExtraField2_ID = 0;
                    string AditionalComment = "";

                    List<StepPostData> InitStepPosts = new List<StepPostData>();

                    Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
                    N_Ter.Customizable.Master_Tables objMasters = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);

                    ActionValidated Act = objCus.CustomTaskStartValidations(ref User_ID, ref Workflow_ID, ref Current_Step_ID, ref ExtraField1, ref ExtraField2, ref AditionalComment, ref InitStepPosts, Convert.ToInt32(hndMasterEL2.Value), objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

                    if (Act.Validated == true)
                    {
                        if (Workflow_ID != dsWF.tblwalkflow[0].Walkflow_ID)
                        {
                            dsWF = objWorkflow.Read(Workflow_ID, false, false);
                            if (dsWF.tblwalkflow.Rows.Count == 0)
                            {
                                CreateTask = false;
                            }
                        }
                        if (CreateTask)
                        {
                            if (dsWF.tblwalkflow[0].Exrta_Field_Type == 3 && dsWF.tblwalkflow[0].Extra_Field_Master_Table_ID > 0)
                            {
                                DS_Master_Tables dsM = objMasters.GetMasterTable(dsWF.tblwalkflow[0].Extra_Field_Master_Table_ID);
                                List<DS_Master_Tables.tblDataRow> drM = dsM.tblData.Where(x => x.Data.Trim() == ExtraField1.Trim()).ToList();
                                if (drM.Count > 0)
                                {
                                    ExtraField_ID = drM[0].Data_ID;
                                }
                            }

                            if (dsWF.tblwalkflow[0].Exrta_Field2_Type == 3 && dsWF.tblwalkflow[0].Extra_Field2_Master_Table_ID > 0)
                            {
                                DS_Master_Tables dsM = objMasters.GetMasterTable(dsWF.tblwalkflow[0].Extra_Field2_Master_Table_ID);
                                List<DS_Master_Tables.tblDataRow> drM = dsM.tblData.Where(x => x.Data.Trim() == ExtraField2.Trim()).ToList();
                                if (drM.Count > 0)
                                {
                                    ExtraField2_ID = drM[0].Data_ID;
                                }
                            }

                            int MaxID = objTasks.Insert(Workflow_ID, User_ID, objSes.UserID, Current_Step_ID, DueDate, Convert.ToInt32(hndMasterEL2.Value), Convert.ToInt32(hndMasterScheduleElementID.Value), ExtraField1, ExtraField2, ExtraField_ID, ExtraField2_ID, objSes.FullName, objSes.UserID, Convert.ToInt32(cboMasterQueue.SelectedItem.Value), objCus, objMasters);

                            if (AditionalComment.Trim() != "")
                            {
                                objTasks.AddComment(MaxID, 1, AditionalComment, "3", "Startup Validations");
                            }

                            if (InitStepPosts.Count > 0)
                            {
                                N_Ter.Common.Common_Task_Actions objComTsk = new N_Ter.Common.Common_Task_Actions();
                                foreach (StepPostData PostData in InitStepPosts)
                                {
                                    if (PostData.Queue_ID > 0)
                                    {
                                        objTasks.UpdateTaskQueue(MaxID, PostData.Queue_ID);
                                    }
                                    if (PostData.Next_Step_ID > 0)
                                    {
                                        objComTsk.SaveStep(MaxID, PostData.TaskFields, PostData.TaskExtraFields, PostData.TaskDocActions, objTasks, objSes, objCus);
                                        objTasks.UpdateTaskHistory(MaxID, PostData.Next_Step_ID, objSes.UserID, objSes.FullName, true, objCus, objMasters);
                                    }
                                    if (PostData.TaskTempSave.Count > 0)
                                    {
                                        foreach (Task_Field_Temp_Save TempSave in PostData.TaskTempSave)
                                        {
                                            objTasks.SaveTaskUpdate(MaxID, TempSave.Workflow_Step_Field_ID, TempSave.Field_Value);
                                        }
                                    }
                                }
                            }

                            DS_Tasks dsNewTask = objTasks.Read(MaxID, false, false, false);
                            DS_Workflow dsNewStep = objWorkflow.ReadStep(dsNewTask.tbltasks[0].Current_Step_ID);

                            N_Ter.Common.Common_Task_Actions objCommAct = new N_Ter.Common.Common_Task_Actions();
                            while (dsNewTask.tbltasks[0].Current_Step_ID != -1 && dsNewTask.tbltasks[0].Current_Step_ID != -2 && dsNewStep.tblworkflow_steps[0].Is_Auto_Submit)
                            {
                                if (objCommAct.AutoSubmit(dsNewTask, objTasks, objWorkflow, objCus, objMasters, objSes.UserID, objSes.FullName))
                                {
                                    dsNewTask = objTasks.Read(MaxID, false, false, false);
                                    dsNewStep = objWorkflow.ReadStep(dsNewTask.tbltasks[0].Current_Step_ID);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (MaxID > 0)
                            {
                                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                                if (ReletiveURL.Trim() != "")
                                {
                                    Response.Redirect("task.aspx?tid=" + objURL.Encrypt(Convert.ToString(MaxID)) + "&bck=" + ReletiveURL);
                                }
                                else if (BackURL.Trim() != "")
                                {
                                    Response.Redirect("task.aspx?tid=" + objURL.Encrypt(Convert.ToString(MaxID)) + "&bck=" + BackURL);
                                }
                                else
                                {
                                    Response.Redirect("task.aspx?tid=" + objURL.Encrypt(Convert.ToString(MaxID)));
                                }
                            }
                            else
                            {
                                ClientScriptManager cs = Page.ClientScript;
                                cs.RegisterStartupScript(this.GetType(), "AKeyM2_5", "ShowError('Task could not be created');", true);
                            }
                        }
                        else
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKeyM2_5", "ShowError('Custom Validations Failed');", true);
                        }
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKeyM2_6", "ShowError('" + Act.Reason + "');", true);
                    }
                }
            }
        }

        private void BuildUserDetails(SessionObject objSes)
        {
            int User_ID = objSes.UserID;
            Users objUser = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
            DS_Users.tblusersRow dr = objUser.Read(User_ID).tblusers[0];
            ltrUserName.Text = dr.First_Name + " " + dr.Last_Name;
            if (dr.IsImage_PathNull() || dr.Image_Path.Trim() == "")
            {
                imgUserImage.ImageUrl = "assets/images/user.png";
            }
            else
            {
                imgUserImage.ImageUrl = dr.Image_Path;
            }
        }

        public void BuildAlerts(SessionObject objSes)
        {
            Tasks objTasks = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            DS_Tasks ds = objTasks.ReadAll(objSes.UserID, true, ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type));

            DS_Users dsAltUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).ReadAssignedUsers(objSes.UserID);
            DS_Tasks dsTemp = new DS_Tasks();

            foreach (DS_Users.tblusersRow rowUser in dsAltUsers.tblusers)
            {
                dsTemp.tbltasks.Clear();
                dsTemp = objTasks.ReadAll(rowUser.User_ID, true, ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type));
                ds.tbltasks.Merge(dsTemp.tbltasks);
            }

            if (ds.tbltasks.Rows.Count > 0)
            {
                ltrNotificationCount.Text = Convert.ToString(ds.tbltasks.Rows.Count);
            }
            else
            {
                ltrNotificationCount.Text = "";
            }

            string strAlerts = "";
            string strBackURL = "";
            if (ReletiveURL.Trim() != "")
            {
                strBackURL = "&bck=" + ReletiveURL;
            }
            else if (BackURL.Trim() != "")
            {
                strBackURL = "&bck=" + BackURL;
            }
            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
            int intIndex = 0;
            foreach (DS_Tasks.tbltasksRow row in ds.tbltasks.OrderByDescending(x => x.Posted_Date))
            {
                strAlerts = strAlerts + "<div class=\"notification\">" + "\r\n" +
                                             "<div class=\"notification-title text-primary notification-link\"><a href=\"task.aspx?tid=" + objURL.Encrypt(Convert.ToString(row.Task_ID)) + strBackURL + "\">" + row.Task_Number + "</a></div>" + "\r\n" +
                                             "<div class=\"notification-description\">" + (row.Workflow_Name.Length > 30 ? row.Workflow_Name.Substring(0, 30) + "..." : row.Workflow_Name) + "</div>" + "\r\n" +
                                             "<div class=\"notification-ago notification-date\">" + string.Format("{0:" + Constants.DateFormat + "}", row.Posted_Date) + "</div>" + "\r\n" +
                                         "</div>" + "\r\n";
                intIndex++;
                if (intIndex == 10)
                {
                    break;
                }
            }
            if (ds.tbltasks.Rows.Count > 10)
            {
                strAlerts = strAlerts + "<div class=\"notification\">" + "\r\n" +
                                             "<div class=\"notification-description\">....</div>" + "\r\n" +
                                         "</div>" + "\r\n";
            }
            ltrAlerts.Text = strAlerts;

            DS_Tasks dsInactive = objTasks.ReadAll(objSes.UserID, false, ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type));

            foreach (DS_Users.tblusersRow rowUser in dsAltUsers.tblusers)
            {
                dsTemp.tbltasks.Clear();
                dsTemp = objTasks.ReadAll(rowUser.User_ID, false, ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type));
                dsInactive.tbltasks.Merge(dsTemp.tbltasks);
            }

            BuildMenu(ds.tbltasks.Rows.Count, dsInactive.tbltasks.Rows.Count, objSes);
        }

        private void BuildMessages(SessionObject objSes)
        {
            Messages objMessages = ObjectCreator.GetMessages(objSes.Connection, objSes.DB_Type);
            DS_Messages ds = objMessages.ReadPendingMessages(objSes.UserID);

            if (ds.tblmessages.Rows.Count > 0)
            {
                int intIndex = 0;
                foreach (DS_Messages.tblmessagesRow row in ds.tblmessages)
                {
                    ltrMessages.Text += "<div class=\"message\">" + "\r\n" +
                                            "<img src=\"" + (row.IsImage_PathNull() ? "assets/images/pixel-admin/avatar.png" : row.Image_Path.Replace("\\", "/")) + "\" alt=\"\" class=\"message-avatar\"/>" + "\r\n" +
                                            "<a href=\"messages.aspx?\" class=\"message-subject\">" + (row.Message.Length > 20 ? row.Message.Substring(0, 20) + ".." : row.Message) + "</a>" + "\r\n" +
                                            "<div class=\"message-description\">" + "\r\n" +
                                                "<a href=\"#\">" + row.Full_Name + "</a>" + "\r\n" +
                                                "&nbsp;&nbsp;·&nbsp;&nbsp;" + "\r\n" +
                                                "" + string.Format("{0:" + Constants.DateFormat + "}", row.Posted_Date_Time) + "" + "\r\n" +
                                            "</div>" + "\r\n" +
                                        "</div>";
                    intIndex++;
                    if (intIndex == 10)
                    {
                        break;
                    }
                }
                if (ds.tblmessages.Rows.Count > 10)
                {
                    ltrMessages.Text += "<div class=\"message\">" + "\r\n" +
                                            "<div class=\"message-description\">....</div>" + "\r\n" +
                                        "</div>";
                }

                ltrMessageCount.Text = ds.tblmessages.Count.ToString();
            }
            else
            {
                ltrMessages.Text = "";
                ltrMessageCount.Text = "";
            }
        }

        private void GetNoticeBoard(SessionObject objSes)
        {
            int NoticeCount = ObjectCreator.GetNoticeBoard(objSes.Connection, objSes.DB_Type).ReadPendingCount(objSes.UserID);
            if (NoticeCount > 0)
            {
                ltrNBCount.Text = NoticeCount.ToString();
            }
            else
            {
                ltrNBCount.Text = "";
            }
        }

        private void BuildMenu(int TaskCount, int InactiveTaskCount, SessionObject objSes)
        {
            Settings objSett = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type);
            DS_Pages dsPages = objSett.ReadPages();

            DS_Extra_Sections dsEtrCat = new N_Ter.Customizable.Custom_Lists().LoadExtraCategories();
            DS_Extra_Sections dsEtr = new N_Ter.Customizable.Custom_Lists().LoadExtraSections();

            AdjustAlternativePages(ref dsPages, dsEtr);
            string PageName = CurrentPage;

            DS_Pages.tblpagesRow[] drCurrentPage = dsPages.tblpages.Where(x => x.Page_Name == PageName).ToArray();
            DS_Pages.tblpagesRow[] drCatPages;
            int PageIndex = 0;
            string HighlightPage = "";
            bool isReferencedPage = false;

            if (drCurrentPage.Length > 0)
            {
                PageIndex = drCurrentPage[0].Page_Index;
                if (drCurrentPage[0].IsPage_Assigned_ToNull() || drCurrentPage[0].Page_Assigned_To.Trim() == "")
                {
                    HighlightPage = drCurrentPage[0].Page_Name;
                }
                else
                {
                    isReferencedPage = true;
                    HighlightPage = drCurrentPage[0].Page_Assigned_To;
                    PageIndex = dsPages.tblpages.Where(x => x.Page_Name == drCurrentPage[0].Page_Assigned_To).First().Page_Index;
                }

                if (Request.QueryString["rpt"] != null)
                {
                    HighlightPage = HighlightPage + "?rpt=" + Request.QueryString["rpt"];
                }
            }

            string strMenu = "";

            string[] MenuItems;
            string[] PageNames;

            int QueryCount = Request.QueryString.Count;

            for (int i = 1; i <= 7; i++)
            {
                drCatPages = dsPages.tblpages.Where(x => x.Page_Index == i).ToArray();
                MenuItems = GetPages(drCatPages);
                PageNames = GetPageNames(drCatPages);
                switch (i)
                {
                    case 1:
                        if (PageIndex == i && (isReferencedPage == true || IsPostBack || QueryCount > 0))
                        {
                            strMenu = strMenu + "<li class=\"mm-dropdown open active\">" + "\r\n";
                        }
                        else
                        {
                            strMenu = strMenu + "<li class=\"mm-dropdown\">" + "\r\n";
                        }

                        strMenu = strMenu + "<a href=\"#\">" + "\r\n" +
                                                "<i class=\"menu-icon fa fa-dashboard\"></i>" + "\r\n" +
                                                "<span class=\"mm-text\">Dashboard</span>" + "\r\n" +
                                            "</a>" + "\r\n";
                        strMenu = strMenu + CreateSubMenu(MenuItems, PageNames, HighlightPage) + "\r\n";
                        break;
                    case 2:
                        if (PageIndex == i && (isReferencedPage == true || IsPostBack || QueryCount > 0))
                        {
                            strMenu = strMenu + "<li class=\"mm-dropdown open active\">" + "\r\n";
                        }
                        else
                        {
                            strMenu = strMenu + "<li class=\"mm-dropdown\">" + "\r\n";
                        }

                        strMenu = strMenu + "<a href=\"#\">" + "\r\n" +
                                                "<i class=\"menu-icon fa fa-comments\"></i>" + "\r\n" +
                                                "<span class=\"mm-text\">Tasks</span>" + "\r\n" +
                                            "</a>" + "\r\n";
                        string[] MenuCounts = { (TaskCount > 0 ? TaskCount.ToString() : ""), "", "", (InactiveTaskCount > 0 ? InactiveTaskCount.ToString() : "") };
                        string[] PageIDs = GetPageIDs(drCatPages);
                        strMenu = strMenu + CreateSubMenu(MenuItems, PageNames, HighlightPage, MenuCounts, PageIDs) + "\r\n";
                        break;
                    case 3:
                        if (PageIndex == i && (isReferencedPage == true || IsPostBack || QueryCount > 0))
                        {
                            strMenu = strMenu + "<li class=\"mm-dropdown open active\">" + "\r\n";
                        }
                        else
                        {
                            strMenu = strMenu + "<li class=\"mm-dropdown\">" + "\r\n";
                        }

                        strMenu = strMenu + "<a href=\"#\">" + "\r\n" +
                                                "<i class=\"menu-icon fa fa-files-o\"></i>" + "\r\n" +
                                                "<span class=\"mm-text\">Documents</span>" + "\r\n" +
                                            "</a>" + "\r\n";
                        strMenu = strMenu + CreateSubMenu(MenuItems, PageNames, HighlightPage) + "\r\n";
                        break;
                    case 4:
                        if (PageIndex == i && (isReferencedPage == true || IsPostBack || QueryCount > 0))
                        {
                            strMenu = strMenu + "<li class=\"mm-dropdown open active\">" + "\r\n";
                        }
                        else
                        {
                            strMenu = strMenu + "<li class=\"mm-dropdown\">" + "\r\n";
                        }

                        strMenu = strMenu + "<a href=\"#\">" + "\r\n" +
                                                "<i class=\"menu-icon fa fa-calendar\"></i>" + "\r\n" +
                                                "<span class=\"mm-text\">Calendars</span>" + "\r\n" +
                                            "</a>" + "\r\n";
                        strMenu = strMenu + CreateSubMenu(MenuItems, PageNames, HighlightPage) + "\r\n";
                        break;
                    case 5:
                        DS_Reports dsRpt = new N_Ter.Customizable.Custom_Lists().LoadReports();
                        if (dsRpt.tblReports.Rows.Count > 0)
                        {
                            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                            if (PageIndex == i && (isReferencedPage == true || IsPostBack || QueryCount > 0))
                            {
                                strMenu = strMenu + "<li class=\"mm-dropdown open active\">" + "\r\n";
                            }
                            else
                            {
                                strMenu = strMenu + "<li class=\"mm-dropdown\">" + "\r\n";
                            }
                            strMenu = strMenu + "<a href=\"#\">" + "\r\n" +
                                                    "<i class=\"menu-icon fa fa-file-text-o\"></i>" + "\r\n" +
                                                    "<span class=\"mm-text\">Reports</span>" + "\r\n" +
                                                "</a>" + "\r\n";
                            string[] MenuItemsRpt = new string[dsRpt.tblReports.Rows.Count];
                            string[] PageNamesRpt = new string[dsRpt.tblReports.Rows.Count];
                            for (int x = 0; x < dsRpt.tblReports.Rows.Count; x++)
                            {
                                MenuItemsRpt[x] = "reports.aspx?rpt=" + objURL.Encrypt(Convert.ToString(dsRpt.tblReports[x].Report_ID), 8);
                                PageNamesRpt[x] = dsRpt.tblReports[x].Report_Name_Menu;
                            }
                            strMenu = strMenu + CreateSubMenu(MenuItemsRpt, PageNamesRpt, HighlightPage) + "\r\n";
                        }
                        break;
                    case 6:
                        if (PageIndex == i && (isReferencedPage == true || IsPostBack || QueryCount > 0))
                        {
                            strMenu = strMenu + "<li class=\"mm-dropdown open active\">" + "\r\n";
                        }
                        else
                        {
                            strMenu = strMenu + "<li class=\"mm-dropdown\">" + "\r\n";
                        }

                        strMenu = strMenu + "<a href=\"#\">" + "\r\n" +
                                                "<i class=\"menu-icon fa fa-cog\"></i>" + "\r\n" +
                                                "<span class=\"mm-text\">Manage</span>" + "\r\n" +
                                            "</a>" + "\r\n";
                        strMenu = strMenu + CreateSubMenu(MenuItems, PageNames, HighlightPage) + "\r\n";
                        break;
                    case 7:
                        if (objSes.isAdmin == 1)
                        {
                            if (PageIndex == i && (isReferencedPage == true || IsPostBack || QueryCount > 0))
                            {
                                strMenu = strMenu + "<li class=\"mm-dropdown open active\">" + "\r\n";
                            }
                            else
                            {
                                strMenu = strMenu + "<li class=\"mm-dropdown\">" + "\r\n";
                            }
                            strMenu = strMenu + "<a href=\"#\">" + "\r\n" +
                                                    "<i class=\"menu-icon fa fa-cogs\"></i>" + "\r\n" +
                                                    "<span class=\"mm-text\">Admin</span>" + "\r\n" +
                                                "</a>" + "\r\n";
                            strMenu = strMenu + CreateSubMenu(MenuItems, PageNames, HighlightPage) + "\r\n";
                        }
                        break;
                }
                strMenu = strMenu + "</li>" + "\r\n";
            }
            ltrMenu.Text = strMenu;

            string strMenu2 = "";
            foreach (DS_Extra_Sections.tblCategoriesRow row in dsEtrCat.tblCategories)
            {
                List<DS_Extra_Sections.tblSectionsRow> drSec = dsEtr.tblSections.Where(x => x.Category_ID == row.Category_ID && (x.IsShow_In_NavNull() || x.Show_In_Nav == true)).ToList();
                if (drSec.Count > 0)
                {
                    HighlightPage = PageName;
                    if (drSec.Where(x => x.Page_Name.Trim() == HighlightPage.Trim()).ToList().Count() > 0)
                    {
                        strMenu2 = strMenu2 + "<li class=\"mm-dropdown open active\">" + "\r\n";
                    }
                    else
                    {
                        strMenu2 = strMenu2 + "<li class=\"mm-dropdown\">" + "\r\n";
                    }
                    strMenu2 = strMenu2 + "<a href=\"#\">" + "\r\n" +
                                            "<i class=\"menu-icon fa " + row.Icon_CSS + "\"></i>" + "\r\n" +
                                            "<span class=\"mm-text\">" + row.Category_Name + "</span>" + "\r\n" +
                                        "</a>" + "\r\n";
                    string[] MenuItemsExtra = new string[drSec.Count];
                    string[] PageNamesExtra = new string[drSec.Count];
                    for (int x = 0; x < drSec.Count; x++)
                    {
                        MenuItemsExtra[x] = drSec[x].Page_Name;
                        PageNamesExtra[x] = drSec[x].Section_Name_Menu;
                    }
                    strMenu2 = strMenu2 + CreateSubMenu(MenuItemsExtra, PageNamesExtra, HighlightPage) + "\r\n";
                    strMenu2 = strMenu2 + "</li>" + "\r\n";
                }
            }

            if (strMenu2.Trim() != "")
            {
                ltrMenu2.Text = strMenu2;
            }
            else
            {
                ulNav2.Visible = false;
            }
        }

        private void AdjustAlternativePages(ref DS_Pages dsPages, DS_Extra_Sections dsExtra)
        {
            List<DS_Extra_Sections.tblSectionsRow> drExtra;
            foreach (DS_Pages.tblpagesRow row in dsPages.tblpages)
            {
                if (row.Alternative_Page > 0)
                {
                    drExtra = dsExtra.tblSections.Where(x => x.Section_ID == row.Alternative_Page).ToList();
                    if (drExtra.Count > 0)
                    {
                        if (row.Page_Name.Trim() == CurrentPage.Trim())
                        {
                            Response.Redirect(drExtra[0].Page_Name + "?");
                        }
                        else
                        {
                            foreach (DS_Pages.tblpagesRow rowAssigned in dsPages.tblpages.Where(x => x.IsPage_Assigned_ToNull() == false && x.Page_Assigned_To.Trim() == row.Page_Name.Trim()))
                            {
                                rowAssigned.Page_Assigned_To = drExtra[0].Page_Name;
                            }
                            row.Page_Name = drExtra[0].Page_Name;
                        }
                    }
                }
            }
        }

        private string[] GetPageNames(DS_Pages.tblpagesRow[] Pages)
        {
            string[] ret = new string[Pages.Length];
            for (int i = 0; i < Pages.Length; i++)
            {
                ret[i] = Pages[i].Page_Desc_Menu;
            }
            return ret;
        }

        private string[] GetPages(DS_Pages.tblpagesRow[] Pages)
        {
            string[] ret = new string[Pages.Length];
            for (int i = 0; i < Pages.Length; i++)
            {
                ret[i] = Pages[i].Page_Name;
            }
            return ret;
        }

        private string[] GetPageIDs(DS_Pages.tblpagesRow[] Pages)
        {
            string[] ret = new string[Pages.Length];
            for (int i = 0; i < Pages.Length; i++)
            {
                ret[i] = Pages[i].Page_ID.ToString();
            }
            return ret;
        }

        private string CreateSubMenu(string[] MenuItems, string[] PageNames, string HighlightPage, string[] MenuCounts = null, string[] Page_IDs = null)
        {
            string SubManu = "<ul>" + "\r\n";
            for (int ii = 0; ii < MenuItems.Length; ii++)
            {
                if (HighlightPage == MenuItems[ii])
                {
                    SubManu = SubManu + "<li class=\"active\">" + "\r\n" +
                        "<a tabindex=\"-1\" href=\"" + (MenuItems[ii].Contains("?") ? MenuItems[ii] : MenuItems[ii] + "?") + "\"><span class=\"mm-text\">" + PageNames[ii] + "</span>" + (MenuCounts != null && MenuCounts[ii].Trim() != "" ? "<span id='mtCount_" + Page_IDs[ii] + "' class=\"label label-warning\">" + MenuCounts[ii] + "</span>" : "") + "</a>" + "\r\n" +
                                        "</li>" + "\r\n";
                }
                else
                {
                    SubManu = SubManu + "<li>" + "\r\n" +
                                            "<a tabindex=\"-1\" href=\"" + (MenuItems[ii].Contains("?") ? MenuItems[ii] : MenuItems[ii] + "?") + "\"><span class=\"mm-text\">" + PageNames[ii] + "</span>" + (MenuCounts != null && MenuCounts[ii].Trim() != "" ? "<span id='mtCount_" + Page_IDs[ii] + "' class=\"label label-warning\">" + MenuCounts[ii] + "</span>" : "") + "</a>" + "\r\n" +
                                        "</li>" + "\r\n";
                }
            }
            SubManu = SubManu + "</ul>" + "\r\n";
            return SubManu;
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
            Response.Redirect("search.aspx?s=" + objURL.Encrypt(txtSearch.Text));
        }

        protected void cmdLogout_Confirm_Click(object sender, EventArgs e)
        {
            Response.Redirect("logout.aspx?");
        }

        public string PageClass
        {
            set
            {
                n_ter_base pg = (n_ter_base)this.Master;
                pg.PageClass = value;
            }
        }

        public bool ShowHelp
        {
            set
            {
                n_ter_base pg = (n_ter_base)this.Master;
                pg.ShowHelp = value;
            }
        }
    }
}