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
    public partial class guest_task_creation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            if (Request.QueryString["w"] != null)
            {
                if (IsPostBack == false)
                {
                    try
                    {
                        N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                        int Workflow_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["w"])));
                        if (Request.QueryString["e"] != null)
                        {
                            ViewState["el2"] = objURL.Decrypt(Convert.ToString(Request.QueryString["e"]));
                            divEL2.Visible = false;
                        }
                        else
                        {
                            ViewState["el2"] = "0";
                            divEL2.Visible = true;
                        }
                        ltrEL2.Text = objSes.EL2;
                        LoadContent(objSes, Workflow_ID);
                    }
                    catch (Exception)
                    {
                        Response.Redirect("guest_error.aspx?");
                    }
                }
                cmdSubmit.Attributes.Add("onClick", "return ValidateStartTaskGuest('" + ViewState["wt"] + "', '" + cboScheduleName.ClientID + "', '" + txtDueDate.ClientID + "');");
            }
            else
            {
                Response.Redirect("guest_error.aspx?");
            }
        }

        private void LoadContent(SessionObject objSes, int Workflow_ID)
        {
            DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(Workflow_ID);
            if (dsWF.tblwalkflow.Rows.Count > 0)
            {
                ltrWFName.Text = dsWF.tblwalkflow[0].Workflow_Name;
                ltrWFName2.Text = dsWF.tblwalkflow[0].Workflow_Name;
                ViewState["wt"] = dsWF.tblwalkflow[0].Workflow_Type;

                DS_Settings dsSett = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type).Read();
                if (dsWF.tblworkflow_steps.OrderBy(x => x.Sort_order).First().User_Group_Involved == dsSett.tblsettings[0].User_Group_Guest)
                {
                    DS_Users dsUsrs = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type).ReadUsers(dsSett.tblsettings[0].User_Group_Guest);
                    if (dsUsrs.tblusers.Rows.Count > 0)
                    {
                        int CurrentTaskCount = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading).CurrentTaskCount(dsUsrs.tblusers[0].User_ID, dsWF.tblwalkflow[0].Walkflow_ID);

                        if (CurrentTaskCount < dsWF.tblwalkflow[0].Number_Of_Concurrent_Tasks_Allowed)
                        {
                            divMaxExceeded.Visible = false;

                            DS_Workflow.tblwalkflowRow drWF = dsWF.tblwalkflow[0];
                            DS_Entity_Level_2 dsEL2 = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type).ReadForUserWorkflow(dsUsrs.tblusers[0].User_ID, Convert.ToInt32(Workflow_ID));

                            if (dsEL2.tblentity_level_2.Rows.Count > 0)
                            {
                                LoadEl2(dsEL2.tblentity_level_2);

                                if (drWF.Exrta_Field_Naming.Trim() != "")
                                {
                                    ltrExtraField1.Text = drWF.Exrta_Field_Naming;
                                    if (drWF.Exrta_Field_Type == 1)
                                    {
                                        cboExtraField1.Visible = false;
                                    }
                                    else
                                    {
                                        txtExtraField1.Visible = false;
                                        if (drWF.Exrta_Field_Type == 2)
                                        {
                                            foreach (string item in drWF.Extra_Field_Selection.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                                            {
                                                if (item.Trim() != "")
                                                {
                                                    cboExtraField1.Items.Add(new ListItem(item, item));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            DS_Master_Tables dsM = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type).GetMasterTable(drWF.Extra_Field_Master_Table_ID);
                                            foreach (DS_Master_Tables.tblDataRow row in dsM.tblData)
                                            {
                                                if (row.Data.Trim() != "")
                                                {
                                                    cboExtraField1.Items.Add(new ListItem(row.Data, row.Data));
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    divExtraField1.Visible = false;
                                }

                                if (drWF.Exrta_Field2_Naming.Trim() != "")
                                {
                                    ltrExtraField2.Text = drWF.Exrta_Field2_Naming;
                                    if (drWF.Exrta_Field2_Type == 1)
                                    {
                                        cboExtraField2.Visible = false;
                                    }
                                    else
                                    {
                                        txtExtraField2.Visible = false;
                                        if (drWF.Exrta_Field2_Type == 2)
                                        {
                                            foreach (string item in drWF.Extra_Field2_Selection.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                                            {
                                                if (item.Trim() != "")
                                                {
                                                    cboExtraField2.Items.Add(new ListItem(item, item));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            DS_Master_Tables dsM = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type).GetMasterTable(drWF.Extra_Field2_Master_Table_ID);
                                            foreach (DS_Master_Tables.tblDataRow row in dsM.tblData)
                                            {
                                                if (row.Data.Trim() != "")
                                                {
                                                    cboExtraField2.Items.Add(new ListItem(row.Data, row.Data));
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    divExtraField2.Visible = false;
                                }

                                if (drWF.Deadline_Type == 1)
                                {
                                    divSchedule.Visible = false;
                                    txtDueDate.Text = string.Format("{0:" + Constants.DateFormat + "}", DateTime.Now.AddDays(drWF.Number_Of_Days));
                                    cboDueTime.SelectedValue = GetTimeDrValue(DateTime.Now);
                                }
                                else if (drWF.Deadline_Type == 2)
                                {
                                    divDueDate.Visible = false;
                                    divDueTime.Visible = false;

                                    DS_Workflow_Scedules dsSch = ObjectCreator.GetWorkflow_Schedules(objSes.Connection, objSes.DB_Type).ReadCurrentWithData(drWF.Schedule_ID);
                                    ltrScheduleName.Text = dsSch.tblworkflow_schedules[0].Schedule_Name;
                                    cboScheduleName.DataSource = dsSch.tblworkflow_schedules_data;
                                    cboScheduleName.DataTextField = "Entity_Data";
                                    cboScheduleName.DataValueField = "Schedule_Line_ID";
                                    cboScheduleName.DataBind();
                                }
                                else
                                {
                                    divSchedule.Visible = false;
                                    txtDueDate.Text = string.Format("{0:" + Constants.DateFormat + "}", DateTime.Now.AddDays(drWF.Number_Of_Days));
                                    cboDueTime.SelectedValue = GetTimeDrValue(DateTime.Now);
                                    txtDueDate.Enabled = false;
                                    cboDueTime.Enabled = false;
                                }

                                LoadGuestHelp(dsWF);
                                if (dsWF.tblwalkflow[0].Guest_Show_Queue_Task_Start)
                                {
                                    LoadTaskQueues(objSes);
                                    if (dsWF.tblwalkflow[0].Guest_Task_Queue_Label.Trim() != "")
                                    {
                                        ltrTaskQueue.Text = dsWF.tblwalkflow[0].Guest_Task_Queue_Label;
                                    }
                                    else
                                    {
                                        ltrTaskQueue.Text = "Task Queue";
                                    }
                                }
                                else
                                {
                                    divTaskQueue.Visible = false;
                                }

                                if (dsWF.tblwalkflow[0].Guest_Entity_L2_Label.Trim() != "")
                                {
                                    ltrEL2.Text = dsWF.tblwalkflow[0].Guest_Entity_L2_Label;
                                }

                                if (dsWF.tblwalkflow[0].Guest_Due_Date_Label.Trim() != "")
                                {
                                    ltrDueDate.Text = dsWF.tblwalkflow[0].Guest_Due_Date_Label;
                                }
                                else
                                {
                                    ltrDueDate.Text = "Due Date";
                                }

                                if (dsWF.tblwalkflow[0].Guest_Due_Time_Label.Trim() != "")
                                {
                                    ltrDueTime.Text = dsWF.tblwalkflow[0].Guest_Due_Time_Label;
                                }
                                else
                                {
                                    ltrDueTime.Text = "Due Time";
                                }
                            }
                            else
                            {
                                Response.Redirect("guest_error.aspx?");
                            }
                        }
                        else
                        {
                            divAllowed.Visible = false;
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
            }
            else
            {
                Response.Redirect("guest_error.aspx?");
            }
        }

        private string GetTimeDrValue(DateTime Time)
        {
            if (Time.Minute < 15)
            {
                return Time.Hour.ToString() + "0";
            }
            else if (Time.Minute < 45)
            {
                return Time.Hour.ToString() + "1";
            }
            else
            {
                return (Time.Hour + 1).ToString() + "0";
            }
        }

        private void LoadGuestHelp(DS_Workflow dsWF)
        {
            altHelp.Visible = false;
            if (dsWF.tblwalkflow[0].Guest_Help_Task_Start.Trim() != "")
            {
                altHelp.Visible = true;
                ltrGuestHelp.Text = dsWF.tblwalkflow[0].Guest_Help_Task_Start;
            }
        }

        private void LoadTaskQueues(SessionObject objSes)
        {
            Task_Queues objQue = ObjectCreator.GetTask_Queues(objSes.Connection, objSes.DB_Type);
            DS_Task_Queues ds = objQue.ReadAll();
            cboQueue.DataSource = ds.tbltask_queues;
            cboQueue.DataValueField = "Queue_ID";
            cboQueue.DataTextField = "Queue_Name";
            cboQueue.DataBind();

            cboQueue.Items.Insert(0, new ListItem("[Genaral Queue]", "0"));
        }

        private void LoadEl2(DS_Entity_Level_2.tblentity_level_2DataTable tbl)
        {
            cboEL2.DataSource = tbl;
            cboEL2.DataTextField = "Display_Name";
            cboEL2.DataValueField = "Entity_L2_ID";
            cboEL2.DataBind();
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            if (Request.QueryString["w"] != null)
            {
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                int Workflow_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["w"])));
                Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                DS_Workflow dsWF = objWF.Read(Workflow_ID);

                DS_Settings dsSett = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type).Read();
                DS_Users dsUsrs = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type).ReadUsers(dsSett.tblsettings[0].User_Group_Guest);
                if (dsUsrs.tblusers.Rows.Count > 0)
                {
                    int User_ID = dsUsrs.tblusers[0].User_ID;
                    Tasks objTasks = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                    bool CreateTask = true;
                    if (dsWF.tblworkflow_steps.Rows.Count == 0)
                    {
                        CreateTask = false;
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKeyM2", "ShowError('Selected Workflow does not have Steps');", true);
                    }
                    else
                    {
                        int CurrentTaskCount = objTasks.CurrentTaskCount(User_ID, Workflow_ID);
                        if (CurrentTaskCount >= dsWF.tblwalkflow[0].Number_Of_Concurrent_Tasks_Allowed)
                        {
                            Users objUser = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
                            DS_Users dsUser = objUser.Read(User_ID);
                            if (dsUser.tblusers[0].Override_Restrictions == false)
                            {
                                CreateTask = false;
                                ClientScriptManager cs = Page.ClientScript;
                                cs.RegisterStartupScript(this.GetType(), "AKeyM2_1", "ShowError('Maximum number of Tasks that can be created is Exceeded');", true);
                            }
                            else
                            {
                                objUser.DisableOverride(User_ID);
                            }
                        }
                    }

                    int Schedule_ID = 0;
                    if (CreateTask == true)
                    {
                        DateTime DueDate = DateTime.Now;
                        if (dsWF.tblwalkflow[0].Deadline_Type == 1)
                        {
                            Common_Actions objCom = new Common_Actions();
                            if (objCom.ValidateDateTime(txtDueDate.Text + " " + cboDueTime.SelectedItem.Text, ref DueDate) == false)
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
                            if (Convert.ToInt32(cboScheduleName.SelectedItem.Value) > 0)
                            {
                                Schedule_ID = Convert.ToInt32(cboScheduleName.SelectedItem.Value);
                                DS_Workflow_Scedules.tblworkflow_schedules_dataRow drLine = ObjectCreator.GetWorkflow_Schedules(objSes.Connection, objSes.DB_Type).ReadScheduleDataLine(Convert.ToInt32(cboScheduleName.SelectedItem.Value));
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
                            if (DueDate.Minute < 15)
                            {
                                DueDate = DueDate.AddHours(1).AddMinutes(-DueDate.Minute);
                            }
                            else if (DueDate.Minute < 45)
                            {
                                DueDate = DueDate.AddMinutes(-DueDate.Minute).AddMinutes(30);
                            }
                            else
                            {
                                DueDate = DueDate.AddMinutes(-DueDate.Minute).AddHours(1);
                            }
                            DueDate = DueDate.AddDays(dsWF.tblwalkflow[0].Number_Of_Days);
                        }

                        if (CreateTask == true)
                        {
                            int Current_Step_ID = dsWF.tblworkflow_steps[0].Workflow_Step_ID;
                            string ExtraField1 = "";
                            if (dsWF.tblwalkflow[0].Exrta_Field_Naming.Trim() != "")
                            {
                                if (dsWF.tblwalkflow[0].Exrta_Field_Type > 1)
                                {
                                    ExtraField1 = cboExtraField1.SelectedItem.Text;
                                }
                                else if (dsWF.tblwalkflow[0].Exrta_Field_Type == 1)
                                {
                                    ExtraField1 = txtExtraField1.Text;
                                }
                            }

                            string ExtraField2 = "";
                            if (dsWF.tblwalkflow[0].Exrta_Field2_Naming.Trim() != "")
                            {
                                if (dsWF.tblwalkflow[0].Exrta_Field2_Type > 1)
                                {
                                    ExtraField2 = cboExtraField2.SelectedItem.Text;
                                }
                                else if (dsWF.tblwalkflow[0].Exrta_Field2_Type == 1)
                                {
                                    ExtraField2 = txtExtraField2.Text;
                                }
                            }

                            int ExtraField_ID = 0;
                            int ExtraField2_ID = 0;
                            string AditionalComment = "";

                            List<StepPostData> InitStepPosts = new List<StepPostData>();

                            int intEL2 = 0;
                            if (Convert.ToInt32(ViewState["el2"]) == 0)
                            {
                                intEL2 = Convert.ToInt32(cboEL2.SelectedItem.Value);
                            }
                            else
                            {
                                intEL2 = Convert.ToInt32(ViewState["el2"]);
                            }

                            Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
                            ActionValidated Act = objCus.CustomTaskStartValidations(ref User_ID, ref Workflow_ID, ref Current_Step_ID, ref ExtraField1, ref ExtraField2, ref AditionalComment, ref InitStepPosts, intEL2, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

                            if (Act.Validated == true)
                            {
                                if (Workflow_ID != dsWF.tblwalkflow[0].Walkflow_ID)
                                {
                                    dsWF = objWF.Read(Workflow_ID);
                                    if (dsWF.tblwalkflow.Rows.Count == 0)
                                    {
                                        CreateTask = false;
                                    }
                                }
                                if (CreateTask)
                                {
                                    N_Ter.Customizable.Master_Tables objMasterTables = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);

                                    if (dsWF.tblwalkflow[0].Exrta_Field_Type == 3 && dsWF.tblwalkflow[0].Extra_Field_Master_Table_ID > 0)
                                    {
                                        DS_Master_Tables dsM = objMasterTables.GetMasterTable(dsWF.tblwalkflow[0].Extra_Field_Master_Table_ID);
                                        List<DS_Master_Tables.tblDataRow> drM = dsM.tblData.Where(x => x.Data.Trim() == ExtraField1.Trim()).ToList();
                                        if (drM.Count > 0)
                                        {
                                            ExtraField_ID = drM[0].Data_ID;
                                        }
                                    }

                                    if (dsWF.tblwalkflow[0].Exrta_Field2_Type == 3 && dsWF.tblwalkflow[0].Extra_Field2_Master_Table_ID > 0)
                                    {
                                        DS_Master_Tables dsM = objMasterTables.GetMasterTable(dsWF.tblwalkflow[0].Extra_Field2_Master_Table_ID);
                                        List<DS_Master_Tables.tblDataRow> drM = dsM.tblData.Where(x => x.Data.Trim() == ExtraField2.Trim()).ToList();
                                        if (drM.Count > 0)
                                        {
                                            ExtraField2_ID = drM[0].Data_ID;
                                        }
                                    }

                                    int Queue_ID = 0;
                                    if (dsWF.tblwalkflow[0].Guest_Show_Queue_Task_Start)
                                    {
                                        Queue_ID = Convert.ToInt32(cboQueue.SelectedItem.Value);
                                    }

                                    int MaxID = objTasks.Insert(Workflow_ID, User_ID, User_ID, Current_Step_ID, DueDate, intEL2, Schedule_ID, ExtraField1, ExtraField2, ExtraField_ID, ExtraField2_ID, objSes.FullName, User_ID, Queue_ID, objCus, objMasterTables);

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
                                                objTasks.UpdateTaskHistory(MaxID, PostData.Next_Step_ID, objSes.UserID, objSes.FullName, true, objCus, objMasterTables);
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
                                    DS_Workflow dsNewStep = objWF.ReadStep(dsNewTask.tbltasks[0].Current_Step_ID);
                                    Current_Step_ID = dsNewTask.tbltasks[0].Current_Step_ID;

                                    N_Ter.Common.Common_Task_Actions objCommAct = new N_Ter.Common.Common_Task_Actions();
                                    while (dsNewTask.tbltasks[0].Current_Step_ID != -1 && dsNewTask.tbltasks[0].Current_Step_ID != -2 && dsNewStep.tblworkflow_steps[0].Is_Auto_Submit)
                                    {
                                        if (objCommAct.AutoSubmit(dsNewTask, objTasks, objWF, objCus, objMasterTables, objSes.UserID, objSes.FullName))
                                        {
                                            dsNewTask = objTasks.Read(MaxID, false, false, false);
                                            dsNewStep = objWF.ReadStep(dsNewTask.tbltasks[0].Current_Step_ID);
                                            Current_Step_ID = dsNewTask.tbltasks[0].Current_Step_ID;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    if (MaxID > 0)
                                    {
                                        Response.Redirect("guest_task_post.aspx?tid=" + objURL.Encrypt(MaxID.ToString()) + "&stp=" + objURL.Encrypt(Current_Step_ID.ToString()));
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
                                    cs.RegisterStartupScript(this.GetType(), "AKeyM2_5", "ShowError('Custom Validation Failed');", true);
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
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKeyM2_5", "ShowError('Error Reading the User Details');", true);
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKeyM2_5", "ShowError('Error Reading the Workflow');", true);
            }
        }
    }
}