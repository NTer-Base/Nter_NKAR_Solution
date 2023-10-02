using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace N_Ter_Tasks
{
    public partial class n_ter_base_loggedin_grid_task_list_t1 : System.Web.UI.MasterPage
    {
        public string TableScript;
        public string TabCountScript;
        public string SelectedQueueScript;
        public string RefreshFreq;
        public bool isActive;
        public event EventHandler FillTasks;
        private string ReturnPage;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            RefreshFreq = objSes.RefFreq.ToString();

            if (IsPostBack == false)
            {
                int Queue_ID = 0;
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                if (Request.QueryString["qid"] != null && Convert.ToString(Request.QueryString["qid"]) != "")
                {
                    Queue_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["qid"])));
                }
                PopulateDropdowns(Queue_ID, 0, objSes);
            }

            ReturnPage = Request.FilePath.Substring(1);
        }

        private void PopulateDropdowns(int Queue_ID, int Active_WF_Category, SessionObject objSes)
        {
            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();

            DS_Users dsAltUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).ReadAssignedUsers(objSes.UserID);

            Tasks objTasks = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            DS_Tasks dsTasksTemp = new DS_Tasks();

            DS_Tasks dsTasks = objTasks.ReadAll(objSes.UserID, isActive, ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type));
            foreach (DS_Users.tblusersRow rowUser in dsAltUsers.tblusers)
            {
                dsTasksTemp.tbltasks.Clear();
                dsTasksTemp = objTasks.ReadAll(rowUser.User_ID, isActive, ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type));
                dsTasks.tbltasks.Merge(dsTasksTemp.tbltasks);
            }

            TabCountScript = "0";
            SelectedQueueScript = "0";

            if (dsTasks.tbltasks.Rows.Count == 0)
            {
                divTasks.Visible = false;
                divCategories.Visible = false;
            }
            else
            {
                if (Active_WF_Category > 0 && dsTasks.tbltasks.Where(x => x.Workflow_Category_ID == Active_WF_Category).Count() == 0)
                {
                    Active_WF_Category = 0;
                }
                divNoTasks.Visible = false;
                List<DS_Tasks.tbltasksRow> drTasksInQueue;

                DS_Task_Queues dsQues = ObjectCreator.GetTask_Queues(objSes.Connection, objSes.DB_Type).ReadAll();
                DS_Task_Queues.tbltask_queuesRow drTempQueue = dsQues.tbltask_queues.Newtbltask_queuesRow();
                drTempQueue.Queue_ID = 0;
                drTempQueue.Queue_Name = "[All My Tasks]";
                dsQues.tbltask_queues.Rows.InsertAt(drTempQueue, 0);

                ltrOtherQueues.Text = "";

                foreach (DS_Task_Queues.tbltask_queuesRow drQue in dsQues.tbltask_queues)
                {
                    if (drQue.Queue_ID == 0)
                    {
                        drTasksInQueue = dsTasks.tbltasks.ToList();
                    }
                    else
                    {
                        drTasksInQueue = dsTasks.tbltasks.Where(x => x.Queue_ID == drQue.Queue_ID).ToList();
                    }
                    if (drTasksInQueue.Count > 0)
                    {
                        if (Queue_ID == drQue.Queue_ID)
                        {
                            SelectedQueueScript = drQue.Queue_ID.ToString();

                            ltrSelectedQueue.Text = drQue.Queue_Name + "&nbsp;&nbsp;&nbsp;&nbsp;<span id='q_cnt_" + drQue.Queue_ID + "' class=\"label label-success\">" + drTasksInQueue.Count + "</span>";

                            TaskListArgs objArgs = new TaskListArgs();
                            objArgs.SelectedTasks = drTasksInQueue;
                            objArgs.Queue_ID = Queue_ID;
                            objArgs.Active_WF_Category = Active_WF_Category;
                            objArgs.AltUsers = dsAltUsers;
                            FillTasks.Invoke(this, objArgs);
                        }
                        else
                        {
                            ltrOtherQueues.Text = ltrOtherQueues.Text + "<li><a href=\"" + ReturnPage + "?qid=" + objURL.Encrypt(Convert.ToString(drQue.Queue_ID)) + "\">" + drQue.Queue_Name + "&nbsp;&nbsp;&nbsp;&nbsp;<span id='q_cnt_" + drQue.Queue_ID + "' class=\"label label-success pull-right\">" + drTasksInQueue.Count + "</span></a></li>";
                        }
                    }
                }

                if (ltrOtherQueues.Text.Trim() == "")
                {
                    ltrOtherQueues.Text = "<li style='padding: 0 10px'>No Other Queues</li>";
                }
            }
        }

        public bool LoadActive
        {
            set { isActive = value; }
        }

        public string Tables
        {
            set { TableScript = value; }
        }

        public string TabCount
        {
            set { TabCountScript = value; }
        }
    }
}