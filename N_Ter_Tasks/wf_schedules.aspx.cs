using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class wf_schedules : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                if (Request.QueryString["fid"] == null)
                {
                    Response.Redirect("error.aspx?");
                }
                else
                {
                    SessionObject objSes = (SessionObject)Session["dt"];
                    ltrEL2.Text = objSes.EL2;

                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    int WFID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["fid"])));
                    ViewState["fid"] = WFID;

                    Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                    DS_Workflow ds = objWF.Read(WFID);
                    ltrWorkflowName.Text = ds.tblwalkflow[0].Workflow_Name;

                    LoadRecFrecs();
                    LoadSchedules(objSes);
                    LoadEL2s(WFID, objSes);

                    RefreshGrid(WFID, objSes);
                }
            }

            cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndWP_SH_ID.ClientID + "').value = '0'; ClearControls();");
            cboSchedule.Attributes.Add("onChange", "CheckAutoStart();");
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidateWFSH('" + cboSchedule.ClientID + "', '" + cboRecType.ClientID + "', '" + cboRecFrec.ClientID + "');";
        }

        private void LoadRecFrecs()
        {
            cboRecFrec.Items.Clear();
            cboRecFrec.Items.Add(new ListItem("[Not Selected]", "0"));
            for (int i = 1; i <= 10; i++)
            {
                cboRecFrec.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        private void LoadSchedules(SessionObject objSes)
        {
            Workflow_Schedules objWF = ObjectCreator.GetWorkflow_Schedules(objSes.Connection, objSes.DB_Type);
            DS_Workflow_Scedules ds = objWF.ReadAll();
            cboSchedule.DataValueField = ds.tblworkflow_schedules.Schedule_IDColumn.ColumnName;
            cboSchedule.DataTextField = ds.tblworkflow_schedules.Schedule_NameColumn.ColumnName;
            cboSchedule.DataSource = ds.tblworkflow_schedules;
            cboSchedule.DataBind();
            cboSchedule.Items.Insert(0, new ListItem("[Not Selected]", "0"));
        }

        private void LoadEL2s(int Workflow_ID, SessionObject objSes)
        {
            Entity_Level_2 objEL2 = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type);
            DS_Entity_Level_2 ds = objEL2.ReadForWorkflow(Workflow_ID);
            cboEL2.DataSource = ds.tblentity_level_2;
            cboEL2.DataTextField = "Display_Name";
            cboEL2.DataValueField = "Entity_L2_ID";
            cboEL2.DataBind();
            cboEL2.Items.Insert(0, new ListItem("[All Allocated " + objSes.EL2P + "]", "0"));
        }

        private void RefreshGrid(int Workflow_ID, SessionObject objSes)
        {
            Workflow_Schedule_Automation objWFSH = ObjectCreator.GetWorkflow_Schedule_Automations(objSes.Connection, objSes.DB_Type);
            DS_WF_Schedules ds = objWFSH.ReadAll(Workflow_ID);
            gvMain.SelectedIndex = -1;
            gvMain.DataSource = ds.tblworkflow_schedule_automations;
            gvMain.DataBind();
            if (ds.tblworkflow_schedule_automations.Rows.Count > 0)
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
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndWP_SH_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndWP_SH_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            int Workflow_ID = Convert.ToInt32(ViewState["fid"]);
            Workflow_Schedule_Automation objWFSH = ObjectCreator.GetWorkflow_Schedule_Automations(objSes.Connection, objSes.DB_Type);
            if (hndWP_SH_ID.Value == "0")
            {
                if (objWFSH.Insert(Workflow_ID, Convert.ToInt32(cboSchedule.SelectedItem.Value), Convert.ToInt32(cboRecType.SelectedItem.Value), Convert.ToInt32(cboRecFrec.SelectedItem.Value), Convert.ToInt32(cboCopyEnt1.SelectedItem.Value), Convert.ToInt32(cboCopyEnt2.SelectedItem.Value), Convert.ToInt32(cboEL2.SelectedItem.Value)) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Schedule Automation could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Schedule Automation Successfully Added');", true);
                    hndWP_SH_ID.Value = "0";
                }
            }
            else
            {
                if (objWFSH.Update(Convert.ToInt32(hndWP_SH_ID.Value), Convert.ToInt32(cboSchedule.SelectedItem.Value), Convert.ToInt32(cboRecType.SelectedItem.Value), Convert.ToInt32(cboRecFrec.SelectedItem.Value), Convert.ToInt32(cboCopyEnt1.SelectedItem.Value), Convert.ToInt32(cboCopyEnt2.SelectedItem.Value), Convert.ToInt32(cboEL2.SelectedItem.Value)) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Schedule Automation could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Schedule Automation Successfully Updated');", true);
                    hndWP_SH_ID.Value = "0";
                }
            }

            RefreshGrid(Workflow_ID, objSes);
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow_Schedule_Automation objWFSH = ObjectCreator.GetWorkflow_Schedule_Automations(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objWFSH.Delete(Convert.ToInt32(hndWP_SH_ID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Schedule Automation Successfully Deleted');", true);
                hndWP_SH_ID.Value = "0";
            }
            int Workflow_ID = Convert.ToInt32(ViewState["fid"]);
            RefreshGrid(Workflow_ID, objSes);
        }
    }
}