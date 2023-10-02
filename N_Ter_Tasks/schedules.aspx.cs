using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter.Base;

namespace N_Ter_Tasks
{
    public partial class schedules : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            if (IsPostBack == false)
            {
                hndWorkflowScheduleID.Value = "0";
                RefreshGrid(objSes);
            }
            if (objSes.isAdmin == 0)
            {
                divNew.Visible = false;
                cmdNew_ModalPopupExtender.Enabled = false;
            }
            else
            {
                divNew.Visible = true;
                cmdNew_ModalPopupExtender.Enabled = true;
                cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndWorkflowScheduleID.ClientID + "').value = '0'; ClearControls();");
            }
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidateWorkflowSchedules('" + txtScheduleName.ClientID + "', '" + txtEntityName.ClientID + "', '" + txtEntityDateName.ClientID + "');";
        }

        private void RefreshGrid(SessionObject objSes)
        {
            Workflow_Schedules objWS = ObjectCreator.GetWorkflow_Schedules(objSes.Connection, objSes.DB_Type);
            DS_Workflow_Scedules ds = objWS.ReadAll();
            gvMain.SelectedIndex = -1;
            gvMain.DataSource = ds.tblworkflow_schedules;
            gvMain.DataBind();
            if (ds.tblworkflow_schedules.Rows.Count > 0)
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
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                if (e.Row.Cells[1].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdEdit = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[1].Controls[1]);
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndWorkflowScheduleID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdUpdate = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdUpdate.Attributes.Add("onclick", "window.open('schedule.aspx?wsid=" + objURL.Encrypt(e.Row.Cells[0].Text) + "', '_self'); return false;");
                }
                if (e.Row.Cells[3].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[3].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndWorkflowScheduleID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow_Schedules objWS = ObjectCreator.GetWorkflow_Schedules(objSes.Connection, objSes.DB_Type);
            if (hndWorkflowScheduleID.Value == "0")
            {
                if (objWS.Insert(txtScheduleName.Text, txtEntityName.Text, txtEntity2Name.Text, txtEntityDateName.Text) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Schedule could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Schedule Details Successfully Added');", true);
                    RefreshGrid(objSes);
                    hndWorkflowScheduleID.Value = "0";
                }
            }
            else
            {
                if (objWS.Update(Convert.ToInt32((hndWorkflowScheduleID.Value)), txtScheduleName.Text, txtEntityName.Text, txtEntity2Name.Text, txtEntityDateName.Text) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Schedule could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Schedule Details Successfully Updated');", true);
                    RefreshGrid(objSes);
                    hndWorkflowScheduleID.Value = "0";
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow_Schedules objWS = ObjectCreator.GetWorkflow_Schedules(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objWS.Delete(Convert.ToInt32(hndWorkflowScheduleID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Schedule Successfully Deleted');", true);
                RefreshGrid(objSes);
                hndWorkflowScheduleID.Value = "0";
            }
        }
    }
}