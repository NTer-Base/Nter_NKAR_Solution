using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;


namespace N_Ter_Tasks
{
    public partial class n_ter_base_loggedin_grid_task_list_t3 : System.Web.UI.MasterPage
    {
        public string TableScript;
        public string TabCountScript;
        public string SelectedQueueScript;
        public string SelectedCatScript;
        public string SelectedWFScript;
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
                int WF_Category = 0;
                int Walkflow_ID = 0;
                int Step_ID = 0;

                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                if (Request.QueryString["qid"] != null && Convert.ToString(Request.QueryString["qid"]) != "")
                {
                    Queue_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["qid"])));
                }
                if (Request.QueryString["wcid"] != null && Convert.ToString(Request.QueryString["wcid"]) != "")
                {
                    WF_Category = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["wcid"])));
                }

                if (Request.QueryString["wid"] != null && Convert.ToString(Request.QueryString["wid"]) != "")
                {
                    Walkflow_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["wid"])));
                }

                if (Request.QueryString["sid"] != null && Convert.ToString(Request.QueryString["sid"]) != "")
                {
                    Step_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["sid"])));
                }
                PopulateDropdowns(Queue_ID, WF_Category, Walkflow_ID, Step_ID, objSes);
            }

            ReturnPage = Request.FilePath.Substring(1);
        }

        private void PopulateDropdowns(int Queue_ID, int Req_WFCategory_ID, int Req_Workflow_ID, int Req_Step_ID, SessionObject objSes)
        {
            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();

            DS_Users dsAltUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).ReadAssignedUsers(objSes.UserID);

            Tasks objTasks = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            DS_Tasks dsTasks = objTasks.ReadAll(objSes.UserID, isActive, ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type));

            if (dsAltUsers.tblusers.Count > 0)
            {
                DS_Tasks dsTasksTemp = new DS_Tasks();
                foreach (DS_Users.tblusersRow rowUser in dsAltUsers.tblusers)
                {
                    dsTasksTemp.tbltasks.Clear();
                    dsTasksTemp = objTasks.ReadAll(rowUser.User_ID, isActive, ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type));
                    dsTasks.tbltasks.Merge(dsTasksTemp.tbltasks);
                }
            }

            TabCountScript = "0";
            SelectedQueueScript = "0";
            SelectedCatScript = "0";
            SelectedWFScript = "0";

            if (dsTasks.tbltasks.Rows.Count == 0)
            {
                divTasks.Visible = false;
                divCategories.Visible = false;
            }
            else
            {
                if (Req_WFCategory_ID > 0 && dsTasks.tbltasks.Where(x => x.Workflow_Category_ID == Req_WFCategory_ID).Count() == 0)
                {
                    Req_WFCategory_ID = 0;
                }
                if (Req_Workflow_ID > 0 && dsTasks.tbltasks.Where(x => x.Walkflow_ID == Req_Workflow_ID).Count() == 0)
                {
                    Req_Workflow_ID = 0;
                }
                if (Req_Step_ID > 0 && dsTasks.tbltasks.Where(x => x.Current_Step_ID == Req_Step_ID).Count() == 0)
                {
                    Req_Step_ID = 0;
                }
                divNoTasks.Visible = false;
                List<DS_Tasks.tbltasksRow> drTasksInQueue;
                List<DS_Tasks.tbltasksRow> drTasksInCat;
                List<DS_Tasks.tbltasksRow> drTasksInWF;

                DS_Task_Queues dsQues = ObjectCreator.GetTask_Queues(objSes.Connection, objSes.DB_Type).ReadAll();
                DS_Task_Queues.tbltask_queuesRow drTempQueue = dsQues.tbltask_queues.Newtbltask_queuesRow();
                drTempQueue.Queue_ID = 0;
                drTempQueue.Queue_Name = "[All My Tasks]";
                dsQues.tbltask_queues.Rows.InsertAt(drTempQueue, 0);

                ltrOtherCategories.Text = "";
                ltrOtherQueues.Text = "";
                ltrOtherWorkflows.Text = "";

                Workflow_Categories objWF_Cat = ObjectCreator.GetWorkflow_Categories(objSes.Connection, objSes.DB_Type);
                DS_Workflow dsCats = objWF_Cat.ReadAllForUser(objSes.UserID);
                if (dsAltUsers.tblusers.Count > 0)
                {
                    DS_Workflow dsTemp = new DS_Workflow();
                    foreach (DS_Users.tblusersRow rowUser in dsAltUsers.tblusers)
                    {
                        dsTemp.tblworkflow_categories.Clear();
                        dsTemp = objWF_Cat.ReadAllForUser(rowUser.User_ID);
                        dsCats.tblworkflow_categories.Merge(dsTemp.tblworkflow_categories);
                    }
                }

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

                            bool FirstWFCat = true;

                            foreach (DS_Workflow.tblworkflow_categoriesRow row in dsCats.tblworkflow_categories)
                            {
                                drTasksInCat = drTasksInQueue.Where(x => x.Workflow_Category_ID == row.Workflow_Category_ID).ToList();
                                if (drTasksInCat.Count > 0)
                                {
                                    if (FirstWFCat == true)
                                    {
                                        if (Req_WFCategory_ID == 0)
                                        {
                                            Req_WFCategory_ID = row.Workflow_Category_ID;
                                        }
                                        FirstWFCat = false;
                                    }

                                    if (row.Workflow_Category_ID == Req_WFCategory_ID)
                                    {
                                        SelectedCatScript = row.Workflow_Category_ID.ToString();

                                        ltrSelectedCategory.Text = row.Workflow_Category_Name + "&nbsp;&nbsp;&nbsp;&nbsp;<span id='cat_cnt_" + row.Workflow_Category_ID + "' class=\"label label-success\">" + drTasksInCat.Count + "</span>";

                                        Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                                        DS_Workflow dsWF = objWF.ReadAllForCat(Req_WFCategory_ID);

                                        bool FirstWF = true;

                                        foreach (DS_Workflow.tblwalkflowRow rowWF in dsWF.tblwalkflow)
                                        {
                                            drTasksInWF = drTasksInCat.Where(x => x.Walkflow_ID == rowWF.Walkflow_ID).ToList();
                                            if (drTasksInWF.Count > 0)
                                            {
                                                if (FirstWF == true)
                                                {
                                                    if (Req_Workflow_ID == 0)
                                                    {
                                                        Req_Workflow_ID = rowWF.Walkflow_ID;
                                                    }
                                                    FirstWF = false;
                                                }

                                                if (rowWF.Walkflow_ID == Req_Workflow_ID)
                                                {
                                                    SelectedWFScript = rowWF.Walkflow_ID.ToString();

                                                    ltrSelectedWF.Text = rowWF.Workflow_Name + "&nbsp;&nbsp;&nbsp;&nbsp;<span id='wf_cnt_" + rowWF.Walkflow_ID + "' class=\"label label-success\">" + drTasksInWF.Count + "</span>";
                                                    DS_Workflow dsWFSteps = objWF.ReadAllStep(Req_Workflow_ID);

                                                    TaskListArgs objArgs = new TaskListArgs();
                                                    objArgs.SelectedTasks = drTasksInWF;
                                                    objArgs.SelectedWF = rowWF;
                                                    objArgs.Queue_ID = Queue_ID;
                                                    objArgs.Active_WF_Category = Req_WFCategory_ID;
                                                    objArgs.Active_WF = Req_Workflow_ID;
                                                    objArgs.Active_WF_Step = Req_Step_ID;
                                                    objArgs.AltUsers = dsAltUsers;
                                                    FillTasks.Invoke(this, objArgs);
                                                }
                                                else
                                                {
                                                    ltrOtherWorkflows.Text = ltrOtherWorkflows.Text + "<li><a href=\"" + ReturnPage + "?qid=" + objURL.Encrypt(Convert.ToString(drQue.Queue_ID)) + "&wcid=" + objURL.Encrypt(Convert.ToString(row.Workflow_Category_ID)) + "&wid=" + objURL.Encrypt(Convert.ToString(rowWF.Walkflow_ID)) + "\">" + rowWF.Workflow_Name + "&nbsp;&nbsp;&nbsp;&nbsp;<span id='wf_cnt_" + rowWF.Walkflow_ID + "' class=\"label label-success pull-right\">" + drTasksInWF.Count + "</span></a></li>";
                                                }
                                            }
                                        }

                                        if (ltrOtherWorkflows.Text.Trim() == "")
                                        {
                                            ltrOtherWorkflows.Text = "<li style='padding: 0 10px'>No Other Workflows</li>";
                                        }
                                    }
                                    else
                                    {
                                        ltrOtherCategories.Text = ltrOtherCategories.Text + "<li><a href=\"" + ReturnPage + "?qid=" + objURL.Encrypt(Convert.ToString(drQue.Queue_ID)) + "&wcid=" + objURL.Encrypt(Convert.ToString(row.Workflow_Category_ID)) + "\">" + row.Workflow_Category_Name + "&nbsp;&nbsp;&nbsp;&nbsp;<span id='cat_cnt_" + row.Workflow_Category_ID + "' class=\"label label-success pull-right\">" + drTasksInCat.Count + "</span></a></li>";
                                    }
                                }
                            }

                            if (ltrOtherCategories.Text.Trim() == "")
                            {
                                ltrOtherCategories.Text = "<li style='padding: 0 10px'>No Other Categories</li>";
                            }
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