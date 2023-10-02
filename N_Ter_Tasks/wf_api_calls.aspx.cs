using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class wf_api_calls : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                if (Request.QueryString["fid"] != null)
                {
                    SessionObject objSes = (SessionObject)Session["dt"];
                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    int Workflow_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["fid"])));
                    Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                    DS_Workflow ds = objWF.Read(Workflow_ID, false, false);
                    if (ds.tblwalkflow.Rows.Count > 0)
                    {
                        lblWorkflowName.Text = ds.tblwalkflow[0].Workflow_Name;
                    }
                    LoadSteps(Workflow_ID, objSes);
                    LoadFieldsforConditions(objWF.ReadAllEvalOKFields(Workflow_ID));
                    hndAPI_Call_ID.Value = "0";
                    hndWF_ID.Value = Convert.ToString(Workflow_ID);
                    RefreshGrid(Workflow_ID, objSes);
                    ltrEL2.Text = objSes.EL2;
                    ltrEL2_1.Text = objSes.EL2;

                    ltrEL1.Text = objSes.EL1;
                    ltrEL1_1.Text = objSes.EL1;
                }
                else
                {
                    Response.Redirect("error.aspx?");
                }
            }
            cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndAPI_Call_ID.ClientID + "').value = '0'; ClearControls(true);");
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidateAPICAll('" + txtAPICallName.ClientID + "','" + chkAllowedWorkflowStep.ClientID + "');";

            cboStepField.Attributes.Add("onChange", "SelectOperators('" + cboStepField.ClientID + "', '" + cboOperator.ClientID + "');");
            chkCondition.Attributes.Add("onClick", "CheckConditions('" + chkCondition.ClientID + "', '" + cboStepField.ClientID + "', '" + cboOperator.ClientID + "', '" + txtCriteria.ClientID + "', 'divCondition', 'cboCondtTemp', 'txtCondtDateTemp', '', true);");
        }

        private void LoadSteps(int Workflow_ID, SessionObject objSes)
        {
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.ReadAllStep(Workflow_ID);
            chkAllowedWorkflowStep.DataSource = ds.tblworkflow_steps;
            chkAllowedWorkflowStep.DataTextField = ds.tblworkflow_steps.Step_StatusColumn.ColumnName;
            chkAllowedWorkflowStep.DataValueField = ds.tblworkflow_steps.Workflow_Step_IDColumn.ColumnName;
            chkAllowedWorkflowStep.DataBind();
        }

        private void LoadFieldsforConditions(DS_Workflow dsWF)
        {
            cboStepField.DataSource = dsWF.tblworkflow_step_fields;
            cboStepField.DataValueField = "Default_Text";
            cboStepField.DataTextField = "Field_Name";
            cboStepField.DataBind();
        }

        private void RefreshGrid(int Workflow_ID, SessionObject objSes)
        {
            WF_API_Calls objMTemp = ObjectCreator.GetWF_API_Calls(objSes.Connection, objSes.DB_Type);
            DS_WF_API_Call ds = objMTemp.ReadAll(Workflow_ID);
            gvMain.SelectedIndex = -1;
            gvMain.DataSource = ds.tblworkflow_api_calls;
            gvMain.DataBind();
            if (ds.tblworkflow_api_calls.Rows.Count > 0)
            {
                gvMain.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            WF_API_Calls objETemp = ObjectCreator.GetWF_API_Calls(objSes.Connection, objSes.DB_Type);
            bool At_Begining = true;
            if (cboExecuteTime.SelectedItem.Value == "0")
            {
                At_Begining = false;
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

            if (hndAPI_Call_ID.Value == "0")
            {
                if (objETemp.Insert(txtAPICallName.Text, txtURL.Text, cboAPIMethod.SelectedItem.Value, txtContentType.Text, txtRequestBody.Text, Convert.ToInt32(hndWF_ID.Value), At_Begining, GetDocWorkflowSteps(), chkCondition.Checked, Step_Field, Operator, Criteria) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Document Template could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Document Template Details Successfully Added');", true);
                    RefreshGrid(Convert.ToInt32(hndWF_ID.Value), objSes);
                    hndAPI_Call_ID.Value = "0";
                }
            }
            else
            {
                if (objETemp.Update(Convert.ToInt32((hndAPI_Call_ID.Value)), txtAPICallName.Text, txtURL.Text, cboAPIMethod.SelectedItem.Value, txtContentType.Text, txtRequestBody.Text, Convert.ToInt32(hndWF_ID.Value), At_Begining, GetDocWorkflowSteps(), chkCondition.Checked, Step_Field, Operator, Criteria) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Document Template could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Document Template Details Successfully Updated');", true);
                    RefreshGrid(Convert.ToInt32(hndWF_ID.Value), objSes);
                    hndAPI_Call_ID.Value = "0";
                }
            }
        }

        protected void cmdDeleteOK_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            WF_API_Calls objETemp = ObjectCreator.GetWF_API_Calls(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objETemp.Delete(Convert.ToInt32(hndAPI_Call_ID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Document Template Successfully Deleted');", true);
                RefreshGrid(Convert.ToInt32(hndWF_ID.Value), objSes);
                hndAPI_Call_ID.Value = "0";
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
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndAPI_Call_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndAPI_Call_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
            }
        }

        private DS_WF_API_Call.tblworkflow_api_calls_subDataTable GetDocWorkflowSteps()
        {
            DS_WF_API_Call ds = new DS_WF_API_Call();
            DS_WF_API_Call.tblworkflow_api_calls_subRow dr;
            foreach (ListItem itm in chkAllowedWorkflowStep.Items)
            {
                if (itm.Selected == true)
                {
                    dr = ds.tblworkflow_api_calls_sub.Newtblworkflow_api_calls_subRow();
                    dr.API_Call_ID = 0;
                    dr.Workflow_Step_ID = Convert.ToInt32(itm.Value);
                    ds.tblworkflow_api_calls_sub.Rows.Add(dr);
                }
            }
            return ds.tblworkflow_api_calls_sub;
        }
    }
}