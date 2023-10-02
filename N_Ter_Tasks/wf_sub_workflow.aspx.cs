using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class wf_sub_workflow : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);

            if (IsPostBack == false)
            {
                if (Request.QueryString["fid"] != null)
                {
                    
                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    int Workflow_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["fid"])));
                    DS_Workflow ds = objWF.Read(Workflow_ID, false, false);
                    if (ds.tblwalkflow.Rows.Count > 0)
                    {
                        lblWorkflowName.Text = ds.tblwalkflow[0].Workflow_Name;
                    }
                    hndLinkID.Value = "0";
                    hndWF_ID.Value = Convert.ToString(Workflow_ID);

                    LoadSteps(objWF, Workflow_ID);                    
                    LoadExData(objWF, Workflow_ID);
                    LoadSubWorkflows(objWF);
                    LoadFieldsforConditions(objWF.ReadAllEvalOKFields(Workflow_ID));
                    RefreshGrid(Workflow_ID, objSes);

                    LoadAllWorkflows(objWF);
                    RefreshPrPostGrid(Workflow_ID, objSes);
                }
                else
                {
                    Response.Redirect("error.aspx?");
                }
                LoadTab(1);
            }

            cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndLinkID.ClientID + "').value = '0'; ClearControls(true);");
            cboSubWorkflow.Attributes.Add("onChange", "CheckEx();");
            chkIsAutomatic.Attributes.Add("onChange", "CheckEx();");
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidateWFSubWF('" + cboSubWorkflow.ClientID + "','" + cboStart.ClientID + "', '" + txtDaysToFinish.ClientID + "');";

            cmdNewPost.Attributes.Add("onClick", "javascript:document.getElementById('" + hndPostLinkID.ClientID + "').value = '0'; ClearControlsParent(true);");
            cboParentWorkflow.Attributes.Add("onChange", "LoadParentWFSteps();");
            cmdParentPostReset.OnClientClick = "return ResetControlsParent();";
            cmdParentPostSave.OnClientClick = "return ValidateParentPost('" + cboParentWorkflow.ClientID + "','" + chkAllowedWorkflowStep.ClientID + "', '" + hndParentWFStep.ClientID + "', '" + hndPostStepData.ClientID + "');";

            cboStepField.Attributes.Add("onChange", "SelectOperators('" + cboStepField.ClientID + "', '" + cboOperator.ClientID + "');");
            chkCondition.Attributes.Add("onClick", "CheckConditions('" + chkCondition.ClientID + "', '" + cboStepField.ClientID + "', '" + cboOperator.ClientID + "', '" + txtCriteria.ClientID + "', 'divCondition', 'cboCondtTemp', 'txtCondtDateTemp', '', true);");

            cboStepField_Pr.Attributes.Add("onChange", "SelectOperators('" + cboStepField_Pr.ClientID + "', '" + cboOperator_Pr.ClientID + "');");
            chkCondition_Pr.Attributes.Add("onClick", "CheckConditions('" + chkCondition_Pr.ClientID + "', '" + cboStepField_Pr.ClientID + "', '" + cboOperator_Pr.ClientID + "', '" + txtCriteria_Pr.ClientID + "', 'divCondition_Pr', 'cboCondtTemp_Pr', 'txtCondtDateTemp_Pr', '', true);");
        }

        private void LoadSteps(Workflow objWF, int Workflow_ID)
        {
            DS_Workflow ds = objWF.ReadAllStep(Workflow_ID);

            cboStart.DataSource = ds.tblworkflow_steps;
            cboStart.DataTextField = ds.tblworkflow_steps.Step_StatusColumn.ColumnName;
            cboStart.DataValueField = ds.tblworkflow_steps.Workflow_Step_IDColumn.ColumnName;
            cboStart.DataBind();
            cboStart.Items.Insert(0, new ListItem("[Not Selected]", "0"));

            cboFinish.DataSource = ds.tblworkflow_steps;
            cboFinish.DataTextField = ds.tblworkflow_steps.Step_StatusColumn.ColumnName;
            cboFinish.DataValueField = ds.tblworkflow_steps.Workflow_Step_IDColumn.ColumnName;
            cboFinish.DataBind();
            cboFinish.Items.Insert(0, new ListItem("[Not Selected]", "0"));

            chkAllowedWorkflowStep.DataSource = ds.tblworkflow_steps;
            chkAllowedWorkflowStep.DataTextField = ds.tblworkflow_steps.Step_StatusColumn.ColumnName;
            chkAllowedWorkflowStep.DataValueField = ds.tblworkflow_steps.Workflow_Step_IDColumn.ColumnName;
            chkAllowedWorkflowStep.DataBind();
        }

        private void LoadSubWorkflows(Workflow objWF)
        {
            cboSubWorkflow.Items.Clear();
            cboSubWorkflow.Items.Add(new ListItem("[Not Selected]", "0"));

            DS_Workflow ds = objWF.ReadAllSubWorkFlows();
            foreach (DS_Workflow.tblwalkflowRow row in ds.tblwalkflow)
            {
                ListItem lst = new ListItem(row.Workflow_Name, row.Walkflow_ID.ToString());
                lst.Attributes.Add("data-ex", row.Exrta_Field_Naming.Trim() + "|" + row.Exrta_Field2_Naming.Trim());
                cboSubWorkflow.Items.Add(lst);
            }
        }

        private void LoadExData(Workflow objWF, int Workflow_ID)
        {
            DS_Workflow ds = objWF.Read(Workflow_ID);
            cboEx1Type.Items.Add(new ListItem("[N/A]", "0"));
            cboEx2Type.Items.Add(new ListItem("[N/A]", "0"));

            cboEx1Type.Items.Add(new ListItem("[Task Number]", "1"));
            cboEx2Type.Items.Add(new ListItem("[Task Number]", "1"));

            if (ds.tblwalkflow[0].Exrta_Field_Naming.Trim() != "")
            {
                cboEx1Type.Items.Add(new ListItem(ds.tblwalkflow[0].Exrta_Field_Naming.Trim(), "2"));
                cboEx2Type.Items.Add(new ListItem(ds.tblwalkflow[0].Exrta_Field_Naming.Trim(), "2"));
            }

            if (ds.tblwalkflow[0].Exrta_Field2_Naming.Trim() != "")
            {
                cboEx1Type.Items.Add(new ListItem(ds.tblwalkflow[0].Exrta_Field2_Naming.Trim(), "3"));
                cboEx2Type.Items.Add(new ListItem(ds.tblwalkflow[0].Exrta_Field2_Naming.Trim(), "3"));
            }
        }

        private void LoadFieldsforConditions(DS_Workflow dsWF)
        {
            cboStepField.DataSource = dsWF.tblworkflow_step_fields;
            cboStepField.DataValueField = "Default_Text";
            cboStepField.DataTextField = "Field_Name";
            cboStepField.DataBind();

            cboStepField_Pr.DataSource = dsWF.tblworkflow_step_fields;
            cboStepField_Pr.DataValueField = "Default_Text";
            cboStepField_Pr.DataTextField = "Field_Name";
            cboStepField_Pr.DataBind();
        }

        private void RefreshGrid(int Workflow_ID, SessionObject objSes)
        {
            WF_Sub_Workflows objMTemp = ObjectCreator.GetWF_Sub_Workflows(objSes.Connection, objSes.DB_Type);
            DS_WF_Sub_Workflow ds = objMTemp.ReadAll(Workflow_ID);
            foreach (DS_WF_Sub_Workflow.tblworkflow_sub_workflowRow row in ds.tblworkflow_sub_workflow)
            {
                if (row.isAutomatic)
                {
                    row.isAutomatic_SP = "Automatic";
                }
                else
                {
                    row.isAutomatic_SP = "Manual";
                }
            }
            gvMain.SelectedIndex = -1;
            gvMain.DataSource = ds.tblworkflow_sub_workflow;
            gvMain.DataBind();
            if (ds.tblworkflow_sub_workflow.Rows.Count > 0)
            {
                gvMain.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            WF_Sub_Workflows objMTemp = ObjectCreator.GetWF_Sub_Workflows(objSes.Connection, objSes.DB_Type);
            int NoOfDays = 0;
            if (int.TryParse(txtDaysToFinish.Text, out NoOfDays) == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Invalid No of Days to Complete the Task');", true);
            }
            else
            {
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
                    if (objMTemp.Insert(Convert.ToInt32(cboStart.SelectedItem.Value), Convert.ToInt32(cboSubWorkflow.SelectedItem.Value), Convert.ToInt32(cboFinish.SelectedItem.Value), chkIsAutomatic.Checked, Convert.ToInt32(cboEx1Type.SelectedItem.Value), Convert.ToInt32(cboEx2Type.SelectedItem.Value), NoOfDays, chkCondition.Checked, Step_Field, Operator, Criteria) == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Sub Workflow Automation could not be saved');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Sub Workflow Automation Details Successfully Added');", true);
                        hndLinkID.Value = "0";
                    }
                }
                else
                {
                    if (objMTemp.Update(Convert.ToInt32(hndLinkID.Value), Convert.ToInt32(cboStart.SelectedItem.Value), Convert.ToInt32(cboSubWorkflow.SelectedItem.Value), Convert.ToInt32(cboFinish.SelectedItem.Value), chkIsAutomatic.Checked, Convert.ToInt32(cboEx1Type.SelectedItem.Value), Convert.ToInt32(cboEx2Type.SelectedItem.Value), NoOfDays, chkCondition.Checked, Step_Field, Operator, Criteria) == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Sub Workflow Automation could not be saved');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Sub Workflow Automation Details Successfully Updated');", true);
                        hndLinkID.Value = "0";
                    }
                }
            }
            RefreshGrid(Convert.ToInt32(hndWF_ID.Value), objSes);
            RefreshPrPostGrid(Convert.ToInt32(hndWF_ID.Value), objSes);
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            LoadSubWorkflows(objWF);
            LoadTab(1);
        }

        protected void cmdDeleteOK_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            WF_Sub_Workflows objMTemp = ObjectCreator.GetWF_Sub_Workflows(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objMTemp.Delete(Convert.ToInt32(hndLinkID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Sub Workflow Automation Successfully Deleted');", true);
                hndLinkID.Value = "0";
            }
            RefreshGrid(Convert.ToInt32(hndWF_ID.Value), objSes);
            RefreshPrPostGrid(Convert.ToInt32(hndWF_ID.Value), objSes);
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            LoadSubWorkflows(objWF);
            LoadTab(1);
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

        private void LoadAllWorkflows(Workflow objWF)
        {
            DS_Workflow ds = objWF.ReadAll();
            cboParentWorkflow.DataSource = ds.tblwalkflow;
            cboParentWorkflow.DataTextField = ds.tblwalkflow.Workflow_NameColumn.ColumnName;
            cboParentWorkflow.DataValueField = ds.tblwalkflow.Walkflow_IDColumn.ColumnName;
            cboParentWorkflow.DataBind();
            cboParentWorkflow.Items.Insert(0, new ListItem("[Not Selected]", "0"));
        }

        private void RefreshPrPostGrid(int Workflow_ID, SessionObject objSes)
        {
            WF_Parent_Workflows objMTemp = ObjectCreator.GetWF_Parent_Workflows(objSes.Connection, objSes.DB_Type);
            DS_WF_Parent_Workflow ds = objMTemp.ReadAll(Workflow_ID);

            gvParentPost.SelectedIndex = -1;
            gvParentPost.DataSource = ds.tblworkflow_parent_posts;
            gvParentPost.DataBind();
            if (ds.tblworkflow_parent_posts.Rows.Count > 0)
            { 
                gvParentPost.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvParentPost_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndPostLinkID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValuesParent();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndPostLinkID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
            }
        }

        protected void cmdParentPostSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            WF_Parent_Workflows objMTemp = ObjectCreator.GetWF_Parent_Workflows(objSes.Connection, objSes.DB_Type);
            bool At_Begining = true;
            if (cboExecuteTime.SelectedItem.Value == "0")
            {
                At_Begining = false;
            }

            int Step_Field = 0;
            int Operator = 0;
            string Criteria = "";
            if (chkCondition_Pr.Checked)
            {
                Step_Field = Convert.ToInt32(cboStepField_Pr.SelectedItem.Value.Split('_')[0]);
                Operator = Convert.ToInt32(cboOperator_Pr.SelectedItem.Value);
                Criteria = txtCriteria_Pr.Text;
            }

            if (hndPostLinkID.Value == "0")
            {
                if (objMTemp.Insert(Convert.ToInt32(hndWF_ID.Value), Convert.ToInt32(cboParentWorkflow.SelectedItem.Value), Convert.ToInt32(hndParentWFStep.Value), At_Begining, GetParentExSteps(), GetParentLinks(), chkCondition_Pr.Checked, Step_Field, Operator, Criteria) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Parent Workflow Post Action could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Parent Workflow Post Action Successfully Added');", true);
                    hndPostLinkID.Value = "0";
                }
            }
            else
            {
                if (objMTemp.Update(Convert.ToInt32(hndPostLinkID.Value), Convert.ToInt32(hndWF_ID.Value), Convert.ToInt32(cboParentWorkflow.SelectedItem.Value), Convert.ToInt32(hndParentWFStep.Value), At_Begining, GetParentExSteps(), GetParentLinks(), chkCondition_Pr.Checked, Step_Field, Operator, Criteria) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Parent Workflow Post Action could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Parent Workflow Post Action Successfully Updated');", true);
                    hndPostLinkID.Value = "0";
                }
            }
            RefreshGrid(Convert.ToInt32(hndWF_ID.Value), objSes);
            RefreshPrPostGrid(Convert.ToInt32(hndWF_ID.Value), objSes);
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            LoadSubWorkflows(objWF);
            LoadTab(2);
        }

        protected void cmdDeleteParentPost_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            WF_Parent_Workflows objMTemp = ObjectCreator.GetWF_Parent_Workflows(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objMTemp.Delete(Convert.ToInt32(hndPostLinkID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Parent Workflow Post Action Successfully Deleted');", true);
                hndPostLinkID.Value = "0";
            }
            RefreshGrid(Convert.ToInt32(hndWF_ID.Value), objSes);
            RefreshPrPostGrid(Convert.ToInt32(hndWF_ID.Value), objSes);
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            LoadSubWorkflows(objWF);
            LoadTab(2);
        }

        private DS_WF_Parent_Workflow.tblworkflow_parent_posts_subDataTable GetParentExSteps()
        {
            DS_WF_Parent_Workflow ds = new DS_WF_Parent_Workflow();
            DS_WF_Parent_Workflow.tblworkflow_parent_posts_subRow dr;
            foreach (ListItem itm in chkAllowedWorkflowStep.Items)
            {
                if (itm.Selected == true)
                {
                    dr = ds.tblworkflow_parent_posts_sub.Newtblworkflow_parent_posts_subRow();
                    dr.Link_ID = 0;
                    dr.Workflow_Step_ID = Convert.ToInt32(itm.Value);
                    ds.tblworkflow_parent_posts_sub.Rows.Add(dr);
                }
            }
            return ds.tblworkflow_parent_posts_sub;
        }

        private DS_WF_Parent_Workflow.tblworkflow_parent_posts_linksDataTable GetParentLinks()
        {
            string[] LinkSplits = hndPostStepData.Value.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
            DS_WF_Parent_Workflow ds = new DS_WF_Parent_Workflow();
            DS_WF_Parent_Workflow.tblworkflow_parent_posts_linksRow dr;
            foreach (string strLink in LinkSplits)
            {
                if (strLink.Trim() != "")
                {
                    string[] strParts = strLink.Trim().Split('|');
                    if (strParts.Length == 2)
                    {
                        dr = ds.tblworkflow_parent_posts_links.Newtblworkflow_parent_posts_linksRow();
                        dr.Link_ID = 0;
                        dr.Workflow_Step_Field_ID = Convert.ToInt32(strParts[1]);
                        dr.Parent_Workflow_Step_Field_ID = Convert.ToInt32(strParts[0]);
                        ds.tblworkflow_parent_posts_links.Rows.Add(dr);
                    }
                }
            }
            return ds.tblworkflow_parent_posts_links;
        }

        private void LoadTab(int Tab_ID)
        {
            switch (Tab_ID)
            {
                case  1:
                    tab1.Attributes["class"] = "active";
                    tab2.Attributes["class"] = "";
                    divTabCont1.Attributes["class"] = "tab-pane fade active in";
                    divTabCont2.Attributes["class"] = "tab-pane fade";
                    break;
                case 2:
                    tab1.Attributes["class"] = "";
                    tab2.Attributes["class"] = "active";
                    divTabCont1.Attributes["class"] = "tab-pane fade";
                    divTabCont2.Attributes["class"] = "tab-pane fade active in";
                    break;
            }
        }
    }
}