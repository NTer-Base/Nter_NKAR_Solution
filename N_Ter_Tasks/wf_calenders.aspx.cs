using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class wf_calenders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                hndLinkID.Value = "0";
                if (Request.QueryString["fid"] == null)
                {
                    Response.Redirect("error.aspx?");
                }
                else
                {
                    SessionObject objSes = (SessionObject)Session["dt"];
                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    int WFID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["fid"])));
                    ViewState["fid"] = WFID;

                    Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                    DS_Workflow dsWF = objWF.Read(WFID, false, false);
                    lblWorkflow.Text = dsWF.tblwalkflow[0].Workflow_Name;

                    LoadCalenders(objSes);
                    LoadFieldsforConditions(objWF.ReadAllEvalOKFields(WFID));

                    if (cboCalender.Items.Count > 0)
                    {
                        LoadFields(WFID, objSes);
                        if (cboStartDate.Items.Count > 0 && cboStartTime.Items.Count > 0 && cboDuration.Items.Count > 0)
                        {
                            divCanAddCals.Visible = true;
                            divNoCalenders.Visible = false;
                            divNoFields.Visible = false;

                            LoadSteps(WFID, objSes);
                            RefreshGrid(objSes);
                        }
                        else
                        {
                            divCanAddCals.Visible = false;
                            divNoCalenders.Visible = false;
                            divNoFields.Visible = true;
                        }
                    }
                    else
                    {
                        divCanAddCals.Visible = false;
                        divNoCalenders.Visible = true;
                        divNoFields.Visible = false;
                    }
                }
            }

            cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndLinkID.ClientID + "').value = '0'; ClearControls(true);");
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidateWFCelender('" + cboCalender.ClientID + "', '" + cboStartDate.ClientID + "', '" + cboStartTime.ClientID + "', '" + cboDuration.ClientID + "');";

            cboStepField.Attributes.Add("onChange", "SelectOperators('" + cboStepField.ClientID + "', '" + cboOperator.ClientID + "');");
            chkCondition.Attributes.Add("onClick", "CheckConditions('" + chkCondition.ClientID + "', '" + cboStepField.ClientID + "', '" + cboOperator.ClientID + "', '" + txtCriteria.ClientID + "', 'divCondition', 'cboCondtTemp', 'txtCondtDateTemp', '', true);");
        }

        private void LoadCalenders(SessionObject objSes)
        {
            Calendars objCal = ObjectCreator.GetCalendars(objSes.Connection, objSes.DB_Type);
            DS_Calenders ds = objCal.ReadAll();
            cboCalender.DataSource = ds.tblcalenders;
            cboCalender.DataValueField = "Calender_ID";
            cboCalender.DataTextField = "Calender_Name";
            cboCalender.DataBind();
        }

        private void LoadFieldsforConditions(DS_Workflow dsWF)
        {
            cboStepField.DataSource = dsWF.tblworkflow_step_fields;
            cboStepField.DataValueField = "Default_Text";
            cboStepField.DataTextField = "Field_Name";
            cboStepField.DataBind();
        }

        private void LoadSteps(int Walkflow_ID, SessionObject objSes)
        {
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.ReadAllStep(Walkflow_ID);
            cboBounceBack.DataSource = ds.tblworkflow_steps;
            cboBounceBack.DataValueField = "Workflow_Step_ID";
            cboBounceBack.DataTextField = "Step_Status";
            cboBounceBack.DataBind();
            cboBounceBack.Items.Insert(0, new ListItem("[Completed Status]", "-1"));

            cboStep.DataSource = ds.tblworkflow_steps;
            cboStep.DataValueField = "Workflow_Step_ID";
            cboStep.DataTextField = "Step_Status";
            cboStep.DataBind();
            cboStep.Items.Insert(0, new ListItem("[Completed Status]", "-1"));
        }

        private void LoadFields(int Walkflow_ID, SessionObject objSes)
        {
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.ReadAllFields(Walkflow_ID);
            cboStartDate.DataSource = ds.tblworkflow_step_fields.Where(x => x.Field_Type == 6);
            cboStartDate.DataValueField = "Workflow_Step_Field_ID";
            cboStartDate.DataTextField = "Field_Name_Extended";
            cboStartDate.DataBind();

            cboStartTime.DataSource = ds.tblworkflow_step_fields.Where(x => x.Field_Type == 7);
            cboStartTime.DataValueField = "Workflow_Step_Field_ID";
            cboStartTime.DataTextField = "Field_Name_Extended";
            cboStartTime.DataBind();

            cboDuration.DataSource = ds.tblworkflow_step_fields.Where(x => x.Field_Type == 12);
            cboDuration.DataValueField = "Workflow_Step_Field_ID";
            cboDuration.DataTextField = "Field_Name_Extended";
            cboDuration.DataBind();

            cboSubject.DataSource = ds.tblworkflow_step_fields.Where(x => x.Field_Type == 2 || x.Field_Type == 3);
            cboSubject.DataValueField = "Workflow_Step_Field_ID";
            cboSubject.DataTextField = "Field_Name_Extended";
            cboSubject.DataBind();
            cboSubject.Items.Insert(0, new ListItem("[Use Calendar Defailt]", "0"));

            cboDescription.DataSource = ds.tblworkflow_step_fields.Where(x => x.Field_Type == 2 || x.Field_Type == 3);
            cboDescription.DataValueField = "Workflow_Step_Field_ID";
            cboDescription.DataTextField = "Field_Name_Extended";
            cboDescription.DataBind();
            cboDescription.Items.Insert(0, new ListItem("[Use Calendar Defailt]", "0"));
        }

        private void RefreshGrid(SessionObject objSes)
        {
            Workflow_Calendars objCal = ObjectCreator.GetWorkflow_Calendars(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objCal.ReadAll(Convert.ToInt32(ViewState["fid"]));
            gvMain.SelectedIndex = -1;
            gvMain.DataSource = ds.tblworkflow_calenders;
            gvMain.DataBind();
            if (ds.tblworkflow_calenders.Rows.Count > 0)
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
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndLinkID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndLinkID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow_Calendars objCal = ObjectCreator.GetWorkflow_Calendars(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objCal.Delete(Convert.ToInt32(hndLinkID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Calendar Successfully Deleted');", true);
                RefreshGrid(objSes);
                hndLinkID.Value = "0";
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow_Calendars objCal = ObjectCreator.GetWorkflow_Calendars(objSes.Connection, objSes.DB_Type);

            int Step_Field = 0;
            int Operator = 0;
            string Criteria = "";
            if (chkCondition.Checked)
            {
                Step_Field = Convert.ToInt32(cboStepField.SelectedItem.Value.Split('_')[0]);
                Operator = Convert.ToInt32(cboOperator.SelectedItem.Value);
                Criteria = txtCriteria.Text;
            }

            if (hndLinkID.Value == "0")
            {
                if (objCal.Insert(Convert.ToInt32(cboCalender.SelectedItem.Value), Convert.ToInt32(ViewState["fid"]), Convert.ToInt32(cboStartDate.SelectedItem.Value), Convert.ToInt32(cboStartTime.SelectedItem.Value), Convert.ToInt32(cboDuration.SelectedItem.Value), Convert.ToInt32(cboSubject.SelectedItem.Value), Convert.ToInt32(cboDescription.SelectedItem.Value), Convert.ToInt32(cboStep.SelectedItem.Value), Convert.ToInt32(cboBounceBack.SelectedItem.Value), chkCondition.Checked, Step_Field, Operator, Criteria) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Calendar could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Calendar Successfully Added');", true);
                    RefreshGrid(objSes);
                    hndLinkID.Value = "0";
                }
            }
            else
            {
                if (objCal.Update(Convert.ToInt32((hndLinkID.Value)), Convert.ToInt32(cboCalender.SelectedItem.Value), Convert.ToInt32(cboStartDate.SelectedItem.Value), Convert.ToInt32(cboStartTime.SelectedItem.Value), Convert.ToInt32(cboDuration.SelectedItem.Value), Convert.ToInt32(cboSubject.SelectedItem.Value), Convert.ToInt32(cboDescription.SelectedItem.Value), Convert.ToInt32(cboStep.SelectedItem.Value), Convert.ToInt32(cboBounceBack.SelectedItem.Value), chkCondition.Checked, Step_Field, Operator, Criteria) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Calendar could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Calendar Successfully Updated');", true);
                    RefreshGrid(objSes);
                    hndLinkID.Value = "0";
                }
            }
        }
    }
}