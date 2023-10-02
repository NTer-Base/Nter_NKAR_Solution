using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class task_analysis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            divTimeline.Visible = false;
            if (ViewState["t"] == null)
            {
                if (Request.QueryString["t"] != null)
                {
                    int Task_ID = Convert.ToInt32(new N_Ter.Security.URL_Manager().Decrypt(Convert.ToString(Request.QueryString["t"])));
                    ViewState["t"] = Task_ID;
                    LoadTaskTimeline();
                }
            }
            else
            {
                LoadTaskTimeline();
            }

            cmdShow.Attributes.Add("onClick", "return ValidateTaskNo();");
        }

        private void LoadTaskTimeline()
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["t"]);
            DS_Tasks dsTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading).Read(Task_ID, false, false, false);
            DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(dsTask.tbltasks[0].Walkflow_ID, false, false);

            if (IsPostBack == false)
            {
                txtTaskNo.Text = dsTask.tbltasks[0].Task_Number;
            }


            ltrMainDetails.Text = "<tr><td style='width: 20%'><b>Task No</b></td><td style='width: 30%'>" + dsTask.tbltasks[0].Task_Number + "</td><td  style='width: 20%'><b>Workflow</b></td><td>" + dsTask.tbltasks[0].Workflow_Name + "</td></tr>";
            ltrMainDetails.Text = ltrMainDetails.Text + "<tr><td style='width: 20%'><b>Current Step</b></td><td>" + (dsTask.tbltasks[0].Current_Step_ID == -1 ? "Completed" : (dsTask.tbltasks[0].Current_Step_ID == -2 ? "Cancelled" : dsTask.tbltasks[0].Step_Status)) + "</td><td style='width: 20%'><b>" + objSes.EL2 + "</b></td><td style='width: 30%'>" + dsTask.tbltasks[0].Display_Name + "</td></tr>";
            if (dsWF.tblwalkflow[0].Exrta_Field_Naming.Trim() != "" || dsWF.tblwalkflow[0].Exrta_Field2_Naming.Trim() != "")
            {
                ltrMainDetails.Text = ltrMainDetails.Text + "<tr>";
                if (dsWF.tblwalkflow[0].Exrta_Field_Naming.Trim() != "")
                {
                    ltrMainDetails.Text = ltrMainDetails.Text + "<td style='width: 20%'><b>" + dsWF.tblwalkflow[0].Exrta_Field_Naming + "</b></td><td>" + dsTask.tbltasks[0].Extra_Field_Value + "</td>";
                }
                if (dsWF.tblwalkflow[0].Exrta_Field2_Naming.Trim() != "")
                {
                    ltrMainDetails.Text = ltrMainDetails.Text + "<td style='width: 20%'><b>" + dsWF.tblwalkflow[0].Exrta_Field2_Naming + "</b></td><td>" + dsTask.tbltasks[0].Extra_Field_Value2 + "</td>";
                }
                ltrMainDetails.Text = ltrMainDetails.Text + "</tr>";
            }

            DS_Users dsUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).ReadAll();
            List<DS_Users.tblusersRow> drUser;

            DS_Tasks.tbltimelineRow drTime = dsTask.tbltimeline.NewtbltimelineRow();
            drTime.Event_Date = dsTask.tbltasks[0].Task_Date;
            drTime.Step_Name = "";
            drTime.Event_Desctiption = "Task Creation";
            drUser = dsUsers.tblusers.Where(x => x.User_ID == dsTask.tbltasks[0].Creator_ID).ToList();
            if (drUser.Count > 0)
            {
                drTime.Full_Name = drUser[0].First_Name + " " + drUser[0].Last_Name;
            }
            else
            {
                drTime.Full_Name = "";
            }
            dsTask.tbltimeline.Rows.Add(drTime);

            List<DS_Tasks.tbltask_history_durationsRow> drDurs;
            DS_Tasks.tbltask_history_durationsRow rowT;

            foreach (DS_Tasks.tbltask_historyRow rowD in dsTask.tbltask_history.OrderBy(x => x.Posted_Date))
            {
                drTime = dsTask.tbltimeline.NewtbltimelineRow();
                drTime.Event_Date = rowD.Posted_Date;
                drTime.Step_Name = rowD.Step_Status;
                drTime.Event_Desctiption = "Step Initiated";
                drUser = dsUsers.tblusers.Where(x => x.User_ID == rowD.Posted_By).ToList();
                if (drUser.Count > 0)
                {
                    drTime.Full_Name = drUser[0].First_Name + " " + drUser[0].Last_Name;
                }
                else
                {
                    drTime.Full_Name = "";
                }
                dsTask.tbltimeline.Rows.Add(drTime);

                drDurs = dsTask.tbltask_history_durations.Where(y => y.Task_Update_ID == rowD.Task_Update_ID).OrderBy(z => z.Start_Date_Time).ToList();
                for (int i = 0; i < drDurs.Count; i++)
                {
                    rowT = drDurs[i];

                    drTime = dsTask.tbltimeline.NewtbltimelineRow();
                    drTime.Event_Date = rowT.Start_Date_Time;
                    drTime.Step_Name = "";
                    if (i > 0 && drDurs[i - 1].User_ID == rowT.User_ID)
                    {
                        drTime.Event_Desctiption = "Step Resumed";
                    }
                    else
                    {
                        drTime.Event_Desctiption = "Step Claimed";
                    }

                    drUser = dsUsers.tblusers.Where(x => x.User_ID == rowT.User_ID).ToList();
                    if (drUser.Count > 0)
                    {
                        drTime.Full_Name = drUser[0].First_Name + " " + drUser[0].Last_Name;
                    }
                    else
                    {
                        drTime.Full_Name = "";
                    }
                    dsTask.tbltimeline.Rows.Add(drTime);

                    if (!rowT.IsEnd_Date_TimeNull())
                    {
                        drTime = dsTask.tbltimeline.NewtbltimelineRow();
                        drTime.Event_Date = rowT.End_Date_Time;
                        drTime.Step_Name = "";
                        if (i + 1 < drDurs.Count && drDurs[i + 1].User_ID == rowT.User_ID)
                        {
                            drTime.Event_Desctiption = "Step Paused";
                        }
                        else
                        {
                            drTime.Event_Desctiption = "Step Unclaimed";
                        }
                        if (drUser.Count > 0)
                        {
                            drTime.Full_Name = drUser[0].First_Name + " " + drUser[0].Last_Name;
                        }
                        else
                        {
                            drTime.Full_Name = "";
                        }
                        dsTask.tbltimeline.Rows.Add(drTime);
                    }
                }
            }

            foreach (DS_Tasks.tbltask_docsRow rowDoc in dsTask.tbltask_docs)
            {
                drTime = dsTask.tbltimeline.NewtbltimelineRow();
                drTime.Event_Date = rowDoc.Uploaded_Date;
                drTime.Step_Name = "";
                drTime.Event_Desctiption = "Document Uploaded - " + rowDoc.Doc_Number;
                drUser = dsUsers.tblusers.Where(x => x.User_ID == rowDoc.Uploaded_By).ToList();
                if (drUser.Count > 0)
                {
                    drTime.Full_Name = drUser[0].First_Name + " " + drUser[0].Last_Name;
                }
                else
                {
                    drTime.Full_Name = "";
                }
                dsTask.tbltimeline.Rows.Add(drTime);
            }

            foreach (DS_Tasks.tbltask_commentsRow rowCom in dsTask.tbltask_comments)
            {
                drTime = dsTask.tbltimeline.NewtbltimelineRow();
                drTime.Event_Date = rowCom.Comment_Date;
                drTime.Step_Name = "";
                if (rowCom.Comment.Trim() == "Task is Flagged")
                {
                    drTime.Event_Desctiption = "Task Flagged";
                }
                else if (rowCom.Comment.Trim() == "Task Flag Removed")
                {
                    drTime.Event_Desctiption = "Task Flag Removed";
                }
                else
                {
                    drTime.Event_Desctiption = "Comment Added";
                }
                drUser = dsUsers.tblusers.Where(x => x.User_ID == rowCom.Commented_By).ToList();
                if (drUser.Count > 0)
                {
                    drTime.Full_Name = drUser[0].First_Name + " " + drUser[0].Last_Name;
                }
                else
                {
                    drTime.Full_Name = "";
                }
                dsTask.tbltimeline.Rows.Add(drTime);
            }

            divTimeline.Visible = true;

            gvMain.SelectedIndex = -1;
            gvMain.DataSource = dsTask.tbltimeline.OrderBy(x => x.Event_Date);
            gvMain.DataBind();
            if (dsTask.tbltimeline.Rows.Count > 0)
            {
                gvMain.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void cmdShow_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            DS_Tasks dsTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading).Read(txtTaskNo.Text);
            if (dsTask.tbltasks.Rows.Count > 0)
            {
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                Response.Redirect("task_analysis.aspx?t=" + objURL.Encrypt(Convert.ToString(dsTask.tbltasks[0].Task_ID)));
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Invalid Task Number');", true);
            }
        }
    }
}