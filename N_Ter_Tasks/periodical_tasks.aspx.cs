using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter.Base;
using N_Ter.Common;

namespace N_Ter_Tasks
{
    public partial class periodical_tasks : System.Web.UI.Page
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
                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    int Workflow_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["fid"])));
                    ViewState["fid"] = Workflow_ID;

                    DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(Workflow_ID);
                    lblWorkflow.Text = dsWF.tblwalkflow[0].Workflow_Name;

                    gvMain.Columns[4].HeaderText = objSes.EL2 + " Name";
                    ltrEL2.Text = objSes.EL2;

                    hndJobID.Value = "0";
                    LoadWorkflowDetails(Workflow_ID, objSes);
                    LoadEL2(Workflow_ID, objSes);
                    RefreshGrid(Workflow_ID, objSes);
                }
            }
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidatePeriodicTask('" + txtJobName.ClientID + "', '" + txtFrequendy.ClientID + "', '" + txtStartDate.ClientID + "', '" + txtDaysToComplete.ClientID + "');";
            cboExtraField1.Attributes.Add("onchange", "$('#" + txtExtraField1.ClientID + "').val($('#" + cboExtraField1.ClientID + "').val())");
            cboExtraField2.Attributes.Add("onchange", "$('#" + txtExtraField2.ClientID + "').val($('#" + cboExtraField2.ClientID + "').val())");
        }

        private void LoadEL2(int Workflow_ID, SessionObject objSes)
        {
            DS_Entity_Level_2 ds = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type).ReadForWorkflow(Workflow_ID);
            cboEL2.DataSource = ds.tblentity_level_2;
            cboEL2.DataValueField = "Entity_L2_ID";
            cboEL2.DataTextField = "Display_Name";
            cboEL2.DataBind();
        }

        private void LoadWorkflowDetails(int Workflow_ID, SessionObject objSes)
        {
            DS_Workflow ds = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(Workflow_ID);
            if (ds.tblwalkflow[0].Exrta_Field_Naming.Trim() == "" || ds.tblwalkflow[0].Extra_Field_Task_Start == false)
            {
                divExtraField1.Attributes["class"] = "form-group hide";
                txtExtraField1.Text = "";
            }
            else
            {
                divExtraField1.Attributes["class"] = "form-group";
                ltrExtraField1.Text = ds.tblwalkflow[0].Exrta_Field_Naming;
                if (ds.tblwalkflow[0].Exrta_Field_Type == 2 && ds.tblwalkflow[0].Extra_Field_Selection.Trim() != "")
                {
                    txtExtraField1.CssClass = "form-control hide";
                    cboExtraField1.CssClass = "form-control";
                    cboExtraField1.Items.Clear();
                    foreach (string exItem in ds.tblwalkflow[0].Extra_Field_Selection.Trim().Split('|'))
                    {
                        cboExtraField1.Items.Add(exItem);
                    }
                }
                else if (ds.tblwalkflow[0].Exrta_Field_Type == 3 && ds.tblwalkflow[0].Extra_Field_Master_Table_ID > 0)
                {
                    txtExtraField1.CssClass = "form-control hide";
                    cboExtraField1.CssClass = "form-control";
                    cboExtraField1.Items.Clear();
                    DS_Master_Tables dsMaster = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type).GetMasterTable(ds.tblwalkflow[0].Extra_Field_Master_Table_ID);
                    foreach (DS_Master_Tables.tblDataRow exItem in dsMaster.tblData)
                    {
                        cboExtraField1.Items.Add(exItem.Data);
                    }
                }
                else
                {
                    txtExtraField1.CssClass = "form-control";
                    cboExtraField1.CssClass = "form-control hide";
                }
            }
            if (ds.tblwalkflow[0].Exrta_Field2_Naming.Trim() == "" || ds.tblwalkflow[0].Extra_Field2_Task_Start == false)
            {
                divExtraField2.Attributes["class"] = "form-group hide";
                txtExtraField2.Text = "";
            }
            else
            {
                divExtraField2.Attributes["class"] = "form-group";
                ltrExtraField2.Text = ds.tblwalkflow[0].Exrta_Field2_Naming;
                if (ds.tblwalkflow[0].Exrta_Field2_Type == 2 && ds.tblwalkflow[0].Extra_Field2_Selection.Trim() != "")
                {
                    txtExtraField2.CssClass = "form-control hide";
                    cboExtraField2.CssClass = "form-control";
                    cboExtraField2.Items.Clear();
                    foreach (string exItem in ds.tblwalkflow[0].Extra_Field2_Selection.Trim().Split('|'))
                    {
                        cboExtraField2.Items.Add(exItem);
                    }
                }
                else if (ds.tblwalkflow[0].Exrta_Field2_Type == 3 && ds.tblwalkflow[0].Extra_Field2_Master_Table_ID > 0)
                {
                    txtExtraField2.CssClass = "form-control hide";
                    cboExtraField2.CssClass = "form-control";
                    cboExtraField2.Items.Clear();
                    DS_Master_Tables dsMaster = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type).GetMasterTable(ds.tblwalkflow[0].Extra_Field2_Master_Table_ID);
                    foreach (DS_Master_Tables.tblDataRow exItem in dsMaster.tblData)
                    {
                        cboExtraField2.Items.Add(exItem.Data);
                    }
                }
                else
                {
                    txtExtraField2.CssClass = "form-control";
                    cboExtraField2.CssClass = "form-control hide";
                }
            }
        }

        private void RefreshGrid(int Workflow_ID, SessionObject objSes)
        {
            Periodic_Tasks objPer = ObjectCreator.GetPeriodic_Tasks(objSes.Connection, objSes.DB_Type);
            DS_Periodic_Tasks ds = objPer.ReadAll(Workflow_ID);
            foreach (DS_Periodic_Tasks.tblperiodical_tasksRow row in ds.tblperiodical_tasks)
            {
                switch ((PeriodTypes)row.Recurrence_Type)
                {
                    case PeriodTypes.Day:
                        row.Rec_Type_SP = "Daily";
                        break;
                    case PeriodTypes.Week:
                        row.Rec_Type_SP = "Weekly";
                        break;
                    case PeriodTypes.Month:
                        row.Rec_Type_SP = "Monthly";
                        break;
                    case PeriodTypes.Year:
                        row.Rec_Type_SP = "Yearly";
                        break;
                }
            }
            gvMain.SelectedIndex = -1;
            gvMain.DataSource = ds.tblperiodical_tasks;
            gvMain.DataBind();
            if (ds.tblperiodical_tasks.Rows.Count > 0)
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
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndJobID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndJobID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Periodic_Tasks objJobs = ObjectCreator.GetPeriodic_Tasks(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objJobs.Delete(Convert.ToInt32(hndJobID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Periodical Task Successfully Deleted');", true);
                hndJobID.Value = "0";
            }
            int Workflow_ID = Convert.ToInt32(ViewState["fid"]);
            RefreshGrid(Workflow_ID, objSes);
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            int Workflow_ID = Convert.ToInt32(ViewState["fid"]);

            Periodic_Tasks objJobs = ObjectCreator.GetPeriodic_Tasks(objSes.Connection, objSes.DB_Type);
            DateTime dtStartDate = new DateTime();

            Common_Actions objCom = new Common_Actions();

            if (objCom.ValidateDate(txtStartDate.Text, ref dtStartDate) == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Invalid Start Date');", true);
            }
            else
            {
                if (hndJobID.Value == "0")
                {
                    if (objJobs.Insert(Convert.ToInt32(hndRecType.Value), txtJobName.Text, Workflow_ID, Convert.ToInt32(cboEL2.SelectedItem.Value), txtExtraField1.Text, txtExtraField2.Text, dtStartDate, Convert.ToInt32(txtFrequendy.Text), Convert.ToInt32(txtDaysToComplete.Text)) == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Periodical Task could not be saved');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Periodical Task Successfully Added');", true);
                        hndJobID.Value = "0";
                    }
                }
                else
                {
                    if (objJobs.Update(Convert.ToInt32(hndJobID.Value), txtJobName.Text, Convert.ToInt32(cboEL2.SelectedItem.Value), txtExtraField1.Text, txtExtraField2.Text, dtStartDate, Convert.ToInt32(txtFrequendy.Text), Convert.ToInt32(txtDaysToComplete.Text)) == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Periodical Task could not be saved');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Periodical Task Successfully Updated');", true);
                        hndJobID.Value = "0";
                    }
                }
            }

            RefreshGrid(Workflow_ID, objSes);
        }
    }
}