using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;

namespace N_Ter_Tasks
{
    public partial class welcome : System.Web.UI.Page
    {
        public string RefreshFreq;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            ltrName.Text = objSes.FullName.Split(' ')[0];

            DS_Users dsAltUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).ReadAssignedUsers(objSes.UserID);
            DS_Workflow dsCat = ObjectCreator.GetWorkflow_Categories(objSes.Connection, objSes.DB_Type).ReadAll();
            DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).ReadAllWithSteps();

            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
            N_Ter.Common.Common_Actions objAct = new N_Ter.Common.Common_Actions();

            LoadTasks(objSes, dsAltUsers, dsCat, dsWF, objURL, objAct);
            LoadInactiveTasks(objSes, dsAltUsers, dsCat, dsWF, objURL, objAct);
            LoadMessages(objSes, objURL, objAct);
            LoadNotices(objSes, objAct);

            RefreshFreq = objSes.RefFreq.ToString();
        }

        private void LoadTasks(SessionObject objSes, DS_Users dsAltUsers, DS_Workflow dsCat, DS_Workflow dsWF, N_Ter.Security.URL_Manager objURL, N_Ter.Common.Common_Actions objAct)
        {
            Tasks objTasks = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            DS_Tasks dsTasksTemp = new DS_Tasks();

            DS_Tasks dsTasks = objTasks.ReadAll(objSes.UserID, true, ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type));
            foreach (DS_Users.tblusersRow rowUser in dsAltUsers.tblusers)
            {
                dsTasksTemp.tbltasks.Clear();
                dsTasksTemp = objTasks.ReadAll(rowUser.User_ID, true, ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type));
                dsTasks.tbltasks.Merge(dsTasksTemp.tbltasks);
            }

            ltrTasks.InnerHtml = "";
            if (dsTasks.tbltasks.Rows.Count > 0)
            {
                divNoTasks.Attributes["class"] = "alert alert-success mb hide";
                ltrTasks.InnerHtml = objAct.GetTaskSummaryHTML(dsTasks, dsCat, dsWF, objSes.UserID, "task_list", objURL);
            }
            else
            {
                divNoTasks.Attributes["class"] = "alert alert-success mb";
            }
        }

        private void LoadInactiveTasks(SessionObject objSes, DS_Users dsAltUsers, DS_Workflow dsCat, DS_Workflow dsWF, N_Ter.Security.URL_Manager objURL, N_Ter.Common.Common_Actions objAct)
        {
            Tasks objTasks = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            DS_Tasks dsTasksTemp = new DS_Tasks();

            DS_Tasks dsTasksInct = objTasks.ReadAll(objSes.UserID, false, ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type));
            foreach (DS_Users.tblusersRow rowUser in dsAltUsers.tblusers)
            {
                dsTasksTemp.tbltasks.Clear();
                dsTasksTemp = objTasks.ReadAll(rowUser.User_ID, false, ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type));
                dsTasksInct.tbltasks.Merge(dsTasksTemp.tbltasks);
            }

            ltrInactiveTasks.InnerHtml = "";
            if (dsTasksInct.tbltasks.Rows.Count > 0)
            {
                divNoInactiveTasks.Attributes["class"] = "alert alert-success mb hide";
                ltrInactiveTasks.InnerHtml = objAct.GetTaskSummaryHTML(dsTasksInct, dsCat, dsWF, objSes.UserID, "task_list_inactive", objURL);
            }
            else
            {
                divNoInactiveTasks.Attributes["class"] = "alert alert-success mb";
            }
        }

        private void LoadMessages(SessionObject objSes, N_Ter.Security.URL_Manager objURL, N_Ter.Common.Common_Actions objAct)
        {
            DS_Messages dsMsg = ObjectCreator.GetMessages(objSes.Connection, objSes.DB_Type).ReadPendingMessages(objSes.UserID);
            ltrMessages.InnerHtml = "";
            if (dsMsg.tblmessages.Rows.Count > 0)
            {
                divNoMessages.Attributes["class"] = "alert alert-success mb hide";
                ltrMessages.InnerHtml = objAct.GetMessageSummaryHTML(dsMsg, objURL);
            }
            else
            {
                divNoMessages.Attributes["class"] = "alert alert-success mb";
            }
        }

        private void LoadNotices(SessionObject objSes, N_Ter.Common.Common_Actions objAct)
        {
            DS_Notice_Board dsNtc = ObjectCreator.GetNoticeBoard(objSes.Connection, objSes.DB_Type).ReadPendingNotes(objSes.UserID);
            ltrNotices.InnerHtml = "";
            if (dsNtc.tblnotice_board.Rows.Count > 0)
            {
                divNoWall.Attributes["class"] = "alert alert-success mb hide";
                ltrNotices.InnerHtml = objAct.GetNoticeSummaryHTML(dsNtc);
            }
            else
            {
                divNoWall.Attributes["class"] = "alert alert-success mb";
            }
        }

        protected void cmdAllTasks_Click(object sender, EventArgs e)
        {
            Response.Redirect("task_list.aspx?");
        }

        protected void cmdAllMessages_Click(object sender, EventArgs e)
        {
            Response.Redirect("messages.aspx?");
        }

        protected void cmdWall_Click(object sender, EventArgs e)
        {
            Response.Redirect("notice_board.aspx?");
        }

        protected void cmdAllInactiveTasks_Click(object sender, EventArgs e)
        {
            Response.Redirect("task_list_inactive.aspx?");
        }
    }
}