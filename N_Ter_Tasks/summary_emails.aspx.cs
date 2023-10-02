using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class summary_emails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            if (IsPostBack == false)
            {
                if (Request.QueryString["fid"] == null)
                {
                    Response.Redirect("error.aspx?");
                }
                else
                {
                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    int WFID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["fid"])));
                    ViewState["fid"] = WFID;

                    Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                    DS_Workflow ds = objWF.Read(WFID);
                    ltrWorkflowName.Text = ds.tblwalkflow[0].Workflow_Name;

                    DS_Users dsUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).ReadAll();
                    DS_Users dsUserGroups = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type).ReadAll();
                    DS_Entity_Level_2 dsEL2 = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type).ReadForWorkflow(WFID);

                    LoadUserGroups(dsUserGroups);
                    LoadUsers(dsUsers);
                    LoadEL2s(dsEL2, objSes);
                    LoadWFSteps(WFID, objSes);
                    LoadFieldsforConditions(objWF.ReadAllEvalOKFields(WFID));

                    RefreshGrid(WFID, objSes, dsUsers, dsUserGroups);

                    ltrEl2.Text = objSes.EL2;
                    foreach (ListItem item in cboEventType.Items)
                    {
                        item.Text = item.Text.Replace("[el2]", objSes.EL2);
                    }
                }
            }

            cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndSummary_ID.ClientID + "').value = '0'; ClearControls(true);");
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidateSummaryEmail('" + cboReceiverType.ClientID + "', '" + cboEventType.ClientID + "', '" + txtNoOfDays.ClientID + "', '" + cboUserGroup.ClientID + "', '" + cboUser.ClientID + "', '" + txtReceiverSupport.ClientID + "');";
            cboEventType.Attributes.Add("onChange", "CheckControls();");
            cboReceiverType.Attributes.Add("onChange", "CheckControls();");

            cboStepField.Attributes.Add("onChange", "SelectOperators('" + cboStepField.ClientID + "', '" + cboOperator.ClientID + "');");
            chkCondition.Attributes.Add("onClick", "CheckConditions('" + chkCondition.ClientID + "', '" + cboStepField.ClientID + "', '" + cboOperator.ClientID + "', '" + txtCriteria.ClientID + "', 'divCondition', 'cboCondtTemp', 'txtCondtDateTemp', '', true);");
        }

        private void LoadUserGroups(DS_Users dsUserGroups)
        {
            cboUserGroup.DataSource = dsUserGroups.tbluser_groups;
            cboUserGroup.DataTextField = "User_Group_Name";
            cboUserGroup.DataValueField = "User_Group_ID";
            cboUserGroup.DataBind();
            cboUserGroup.Items.Insert(0, new ListItem("[Not Selected]", "0"));
        }

        private void LoadUsers(DS_Users dsUsers)
        {
            cboUser.DataSource = dsUsers.tblusers;
            cboUser.DataTextField = "Full_Name";
            cboUser.DataValueField = "User_ID";
            cboUser.DataBind();
            cboUser.Items.Insert(0, new ListItem("[Not Selected]", "0"));
        }

        private void LoadEL2s(DS_Entity_Level_2 dsEL2, SessionObject objSes)
        {
            cboEl2.DataSource = dsEL2.tblentity_level_2;
            cboEl2.DataTextField = "Display_Name";
            cboEl2.DataValueField = "Entity_L2_ID";
            cboEl2.DataBind();
            cboEl2.Items.Insert(0, new ListItem("[All " + objSes.EL2P + "]", "0"));
        }

        private void LoadWFSteps(int Workflow_ID, SessionObject objSes)
        {
            DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).ReadAllStep(Workflow_ID);
            cboWorkflowStep.DataSource = dsWF.tblworkflow_steps;
            cboWorkflowStep.DataTextField = "Step_Status";
            cboWorkflowStep.DataValueField = "Workflow_Step_ID";
            cboWorkflowStep.DataBind();
            cboWorkflowStep.Items.Insert(0, new ListItem("[All Steps]", "0"));
        }

        private void LoadFieldsforConditions(DS_Workflow dsWF)
        {
            cboStepField.DataSource = dsWF.tblworkflow_step_fields;
            cboStepField.DataValueField = "Default_Text";
            cboStepField.DataTextField = "Field_Name";
            cboStepField.DataBind();
        }

        private void RefreshGrid(int Workflow_ID, SessionObject objSes, DS_Users dsUsers, DS_Users dsUserGroups)
        {
            Summary_Emails objSE = ObjectCreator.GetSummaryEmils(objSes.Connection, objSes.DB_Type);
            DS_Summary_Emails ds = objSE.ReadAll(Workflow_ID);
            SummaryEmailReceivers Recs;
            SummaryEmailEvents Evts;

            List<DS_Users.tblusersRow> drUser;
            List<DS_Users.tbluser_groupsRow> drUserGroup;

            foreach (DS_Summary_Emails.tblsummary_emailsRow row in ds.tblsummary_emails)
            {
                if (row.Entity_L2_ID == 0)
                {
                    row.Display_Name = "[All " + objSes.EL2P + "]";
                }

                Recs = (SummaryEmailReceivers)row.Receiver_Type;
                Evts = (SummaryEmailEvents)row.Event_Type;
                switch (Recs)
                {
                    case SummaryEmailReceivers.All_Step_Owners:
                        row.Receiver_Type_Desc = "All Step Owners";
                        break;
                    case SummaryEmailReceivers.All_Involved:
                        row.Receiver_Type_Desc = "All who are involved in the Task";
                        break;
                    case SummaryEmailReceivers.Task_Creator:
                        row.Receiver_Type_Desc = "Task Creator";
                        break;
                    case SummaryEmailReceivers.Task_Creator_Supervisor:
                        row.Receiver_Type_Desc = "Task Creator's Supervisor";
                        break;
                    case SummaryEmailReceivers.Group_Members:
                        row.Receiver_Type_Desc = "All Members of a User Group";
                        drUserGroup = dsUserGroups.tbluser_groups.Where(x => x.User_Group_ID == row.Receiver_ID).ToList();
                        if (drUserGroup.Count > 0)
                        {
                            row.Receiver_Type_Desc = row.Receiver_Type_Desc + " - " + drUserGroup[0].User_Group_Name;
                        }
                        break;
                    case SummaryEmailReceivers.All_Involved_Group_Members:
                        row.Receiver_Type_Desc = "All Members of a User Group who are Involved in the Task";
                        drUserGroup = dsUserGroups.tbluser_groups.Where(x => x.User_Group_ID == row.Receiver_ID).ToList();
                        if (drUserGroup.Count > 0)
                        {
                            row.Receiver_Type_Desc = row.Receiver_Type_Desc + " - " + drUserGroup[0].User_Group_Name;
                        }
                        break;
                    case SummaryEmailReceivers.In_Hiararchy_Group_Members:
                        row.Receiver_Type_Desc = "All Members of a User Group who are in the Hiararchy of the Task Creator";
                        drUserGroup = dsUserGroups.tbluser_groups.Where(x => x.User_Group_ID == row.Receiver_ID).ToList();
                        if (drUserGroup.Count > 0)
                        {
                            row.Receiver_Type_Desc = row.Receiver_Type_Desc + " - " + drUserGroup[0].User_Group_Name;
                        }
                        break;
                    case SummaryEmailReceivers.Particular_User:
                        row.Receiver_Type_Desc = "Particular User";
                        drUser = dsUsers.tblusers.Where(x => x.User_ID == row.Receiver_ID).ToList();
                        if (drUser.Count > 0)
                        {
                            row.Receiver_Type_Desc = row.Receiver_Type_Desc + " - " + drUser[0].First_Name + " " + drUser[0].Last_Name;
                        }
                        break;
                    case SummaryEmailReceivers.Non_Users:
                        row.Receiver_Type_Desc = "Non-Users";
                        row.Receiver_Type_Desc = row.Receiver_Support;
                        break;
                }
                switch (Evts)
                {
                    case SummaryEmailEvents.Reaches_Step:
                        row.Event_Type_Desc = "When Task Reaches a Step";
                        if (row.Workflow_Step_ID == 0)
                        {
                            row.Step_Status = "[All Steps]";
                        }
                        break;
                    case SummaryEmailEvents.Days_Stays_On_Step:
                        row.Event_Type_Desc = "When Task is held on a Step for " + row.Event_Suuport_ID + " Day(s)";
                        if (row.Workflow_Step_ID == 0)
                        {
                            row.Step_Status = "[All Steps]";
                        }
                        break;
                    case SummaryEmailEvents.Comment_Added:
                        row.Event_Type_Desc = "When a Comment is Added to the Task";
                        if (row.Workflow_Step_ID == 0)
                        {
                            row.Step_Status = "[All Steps]";
                        }
                        break;
                    case SummaryEmailEvents.Document_Added:
                        row.Event_Type_Desc = "When a Document is Added to the Task";
                        if (row.Workflow_Step_ID == 0)
                        {
                            row.Step_Status = "[All Steps]";
                        }
                        break;
                    case SummaryEmailEvents.Addon_Added:
                        row.Event_Type_Desc = "When an Addon is Added to the Task";
                        row.Step_Status = "[N/A]";
                        break;
                    case SummaryEmailEvents.Due_Date_Adjusted:
                        row.Event_Type_Desc = "When the Task Due Date is Adjusted";
                        row.Step_Status = "[N/A]";
                        break;
                    case SummaryEmailEvents.EL2_Changed:
                        row.Event_Type_Desc = "When the " + objSes.EL2 + " of the Task is Changed";
                        row.Step_Status = "[N/A]";
                        break;
                }
            }

            gvMain.SelectedIndex = -1;
            gvMain.DataSource = ds.tblsummary_emails;
            gvMain.DataBind();
            if (ds.tblsummary_emails.Rows.Count > 0)
            {
                gvMain.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndSummary_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndSummary_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            int Workflow_ID = Convert.ToInt32(ViewState["fid"]);
            Summary_Emails objSE = ObjectCreator.GetSummaryEmils(objSes.Connection, objSes.DB_Type);

            int Receiver_ID = 0;
            string Receiver_Support = "";
            SummaryEmailReceivers Recs = (SummaryEmailReceivers)Convert.ToInt32(cboReceiverType.SelectedValue);
            switch (Recs)
            {
                case SummaryEmailReceivers.Group_Members:
                    Receiver_ID = Convert.ToInt32(cboUserGroup.SelectedValue);
                    break;
                case SummaryEmailReceivers.All_Involved_Group_Members:
                    Receiver_ID = Convert.ToInt32(cboUserGroup.SelectedValue);
                    break;
                case SummaryEmailReceivers.In_Hiararchy_Group_Members:
                    Receiver_ID = Convert.ToInt32(cboUserGroup.SelectedValue);
                    break;
                case SummaryEmailReceivers.Particular_User:
                    Receiver_ID = Convert.ToInt32(cboUser.SelectedValue);
                    break;
                case SummaryEmailReceivers.Non_Users:
                    Receiver_Support = txtReceiverSupport.Text;
                    break;
            }
            int Event_Support_ID = 0;
            bool Event_Support_OK = true;
            SummaryEmailEvents Evts = (SummaryEmailEvents)Convert.ToInt32(cboEventType.SelectedValue);
            switch (Evts)
            {
                case SummaryEmailEvents.Days_Stays_On_Step:
                    if (int.TryParse(txtNoOfDays.Text, out Event_Support_ID) == false)
                    {
                        Event_Support_OK = false;
                    }
                    break;
            }

            if (Event_Support_OK == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Invalid Number of Days');", true);
            }
            else
            {
                bool isInstant = false;
                if (cboEventType.SelectedItem.Value != "2" && cboIsInstant.SelectedItem.Value == "1")
                {
                    isInstant = true;
                }

                int Step_Field = 0;
                int Operator = 0;
                string Criteria = "";
                if (chkCondition.Checked)
                {
                    Step_Field = Convert.ToInt32(cboStepField.SelectedItem.Value.Split('_')[0]);
                    Operator = Convert.ToInt32(cboOperator.SelectedItem.Value);
                    Criteria = txtCriteria.Text;
                }

                if (hndSummary_ID.Value == "0")
                {
                    if (objSE.Insert(Workflow_ID, Convert.ToInt32(cboReceiverType.SelectedValue), Receiver_ID, Convert.ToInt32(cboEventType.SelectedValue), Event_Support_ID, Convert.ToInt32(cboEl2.SelectedValue), Convert.ToInt32(cboWorkflowStep.SelectedValue), Receiver_Support, isInstant, chkCondition.Checked, Step_Field, Operator, Criteria) == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Summary Email Job could not be saved');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Summary Email Job Successfully Added');", true);
                        hndSummary_ID.Value = "0";
                    }
                }
                else
                {
                    if (objSE.Update(Convert.ToInt32(hndSummary_ID.Value), Convert.ToInt32(cboReceiverType.SelectedValue), Receiver_ID, Convert.ToInt32(cboEventType.SelectedValue), Event_Support_ID, Convert.ToInt32(cboEl2.SelectedValue), Convert.ToInt32(cboWorkflowStep.SelectedValue), Receiver_Support, isInstant, chkCondition.Checked, Step_Field, Operator, Criteria) == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Summary Email Job could not be saved');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Summary Email Job Successfully Updated');", true);
                        hndSummary_ID.Value = "0";
                    }
                }
            }

            DS_Users dsUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).ReadAll();
            DS_Users dsUserGroups = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type).ReadAll();

            RefreshGrid(Workflow_ID, objSes, dsUsers, dsUserGroups);
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Summary_Emails objSE = ObjectCreator.GetSummaryEmils(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objSE.Delete(Convert.ToInt32(hndSummary_ID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Summary Email Job Successfully Deleted');", true);
                hndSummary_ID.Value = "0";
            }
            int Workflow_ID = Convert.ToInt32(ViewState["fid"]);
            DS_Users dsUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).ReadAll();
            DS_Users dsUserGroups = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type).ReadAll();

            RefreshGrid(Workflow_ID, objSes, dsUsers, dsUserGroups);
        }
    }
}