using N_Ter.Base;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class wf_doc_templates : Page
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
                    LoadTemplates(objSes);
                    LoadSteps(Workflow_ID, objSes);
                    LoadAttachmentsForEMails(Workflow_ID, objSes);
                    LoadDocTypes(Workflow_ID, objSes);
                    LoadFieldsforConditions(objWF.ReadAllEvalOKFields(Workflow_ID));
                    hndDocumentID.Value = "0";
                    hndEmail_ID.Value = "0";
                    hndWF_ID.Value = Convert.ToString(Workflow_ID);
                    RefreshGrid(Workflow_ID, 1, objSes);
                    ltrEL2.Text = objSes.EL2;
                    ltrEL2_1.Text = objSes.EL2;
                    ltrEL2_2.Text = objSes.EL2;
                    ltrEL2_3.Text = objSes.EL2;

                    ltrEL1.Text = objSes.EL1;
                    ltrEL1_1.Text = objSes.EL1;
                    ltrEL1_2.Text = objSes.EL1;
                    ltrEL1_3.Text = objSes.EL1;
                }
                else
                {
                    Response.Redirect("error.aspx?");
                }
            }
            txtMeetingDate.Attributes.Add("placeholder", Constants.DateFormatJava + " hh:mm");

            cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndDocumentID.ClientID + "').value = '0'; ClearControls(true);");
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidateWFDocTemplate('" + txtDocumentName.ClientID + "');";

            cmdNewEmail.Attributes.Add("onClick", "javascript:document.getElementById('" + hndEmail_ID.ClientID + "').value = '0'; ClearControlsEmail(true);");
            cmdResetEmail.OnClientClick = "return ResetControlsEmail();";
            cmdSaveEmail.OnClientClick = "return ValidateWFEmailTemplate('" + txtEmailName.ClientID + "','" + txtEmailBody.ClientID + "', '" + txtEmailAddress.ClientID + "','" + chkAllowedWorkflowStepEmail.ClientID + "');";
            chkSendMeetingRequest.Attributes.Add("onclick", "CheckMeetingRequest();");

            cmdNewFile.Attributes.Add("onClick", "javascript:document.getElementById('" + hndDocumentID.ClientID + "').value = '0'; ClearControlsFile(true);");
            cmdResetFile.OnClientClick = "return ResetControlsFile();";
            cmdSaveFile.OnClientClick = "return ValidateWFFileTemplate('" + txtFileName.ClientID + "');";

            cboStepField.Attributes.Add("onChange", "SelectOperators('" + cboStepField.ClientID + "', '" + cboOperator.ClientID + "');");
            chkCondition.Attributes.Add("onClick", "CheckConditions('" + chkCondition.ClientID + "', '" + cboStepField.ClientID + "', '" + cboOperator.ClientID + "', '" + txtCriteria.ClientID + "', 'divCondition', 'cboCondtTemp', 'txtCondtDateTemp', '', true);");
            cboStepField_Email.Attributes.Add("onChange", "SelectOperators('" + cboStepField_Email.ClientID + "', '" + cboOperator_Email.ClientID + "');");
            chkCondition_Email.Attributes.Add("onClick", "CheckConditions('" + chkCondition_Email.ClientID + "', '" + cboStepField_Email.ClientID + "', '" + cboOperator_Email.ClientID + "', '" + txtCriteria_Email.ClientID + "', 'divCondition_Email', 'cboCondtTemp_Email', 'txtCondtDateTemp_Email', '', true);");
            cboStepField_File.Attributes.Add("onChange", "SelectOperators('" + cboStepField_File.ClientID + "', '" + cboOperator_File.ClientID + "');");
            chkCondition_File.Attributes.Add("onClick", "CheckConditions('" + chkCondition_File.ClientID + "', '" + cboStepField_File.ClientID + "', '" + cboOperator_File.ClientID + "', '" + txtCriteria_File.ClientID + "', 'divCondition_File', 'cboCondtTemp_File', 'txtCondtDateTemp_File', '', true);");
        }

        private void LoadTemplates(SessionObject objSes)
        {
            string TemplatePath = objSes.PhysicalRoot + "\\nter_app_uploads\\company_letter_head";
            System.IO.DirectoryInfo TemplatesList = new System.IO.DirectoryInfo(TemplatePath);
            cboTemplates.Items.Clear();
            string FileName = "";
            foreach (System.IO.FileInfo Template in TemplatesList.GetFiles())
            {
                if (Template.Extension.ToLower() == ".docx")
                {
                    FileName = Template.Name.Substring(0, Template.Name.IndexOf('.'));
                    cboTemplates.Items.Add(new ListItem(FileName, FileName));
                }
            }
        }

        private void LoadSteps(int Workflow_ID, SessionObject objSes)
        {
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.ReadAllStep(Workflow_ID);
            chkAllowedWorkflowStep.DataSource = ds.tblworkflow_steps;
            chkAllowedWorkflowStep.DataTextField = ds.tblworkflow_steps.Step_StatusColumn.ColumnName;
            chkAllowedWorkflowStep.DataValueField = ds.tblworkflow_steps.Workflow_Step_IDColumn.ColumnName;
            chkAllowedWorkflowStep.DataBind();

            chkAllowedWorkflowStepEmail.DataSource = ds.tblworkflow_steps;
            chkAllowedWorkflowStepEmail.DataTextField = ds.tblworkflow_steps.Step_StatusColumn.ColumnName;
            chkAllowedWorkflowStepEmail.DataValueField = ds.tblworkflow_steps.Workflow_Step_IDColumn.ColumnName;
            chkAllowedWorkflowStepEmail.DataBind();

            chkAllowedWorkflowStep_File.DataSource = ds.tblworkflow_steps;
            chkAllowedWorkflowStep_File.DataTextField = ds.tblworkflow_steps.Step_StatusColumn.ColumnName;
            chkAllowedWorkflowStep_File.DataValueField = ds.tblworkflow_steps.Workflow_Step_IDColumn.ColumnName;
            chkAllowedWorkflowStep_File.DataBind();
        }

        private void LoadAttachmentsForEMails(int Workflow_ID, SessionObject objSes)
        {
            DS_WF_Doc_Templates ds = ObjectCreator.GetWF_Document_Templates(objSes.Connection, objSes.DB_Type).ReadAll(Workflow_ID);
            cboDocumentToAttach.DataSource = ds.tblworkflow_document_templates;
            cboDocumentToAttach.DataTextField = "Document_Name";
            cboDocumentToAttach.DataValueField = "Document_ID";
            cboDocumentToAttach.DataBind();
            cboDocumentToAttach.Items.Insert(0, new ListItem("[Not Applicable]", "0"));

            DS_Data_Extract dsEx = ObjectCreator.GetData_Extract_Templates(objSes.Connection, objSes.DB_Type).ReadAll(objSes.UserID);
            cboExtractToAttach.DataSource = dsEx.tbldata_extract_templates;
            cboExtractToAttach.DataTextField = "Template_Name";
            cboExtractToAttach.DataValueField = "Template_ID";
            cboExtractToAttach.DataBind();
            cboExtractToAttach.Items.Insert(0, new ListItem("[Not Applicable]", "0"));

            cboTaskDocsToAttach.Items.Clear();
            DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(Workflow_ID);
            int intIndex = 1;
            foreach (string DocType in dsWF.tblwalkflow[0].Workflow_Doc_Types.Split('|'))
            {
                if (DocType.Trim() != "")
                {
                    cboTaskDocsToAttach.Items.Add(new ListItem(DocType, intIndex.ToString()));
                    intIndex++;
                }
            }
            cboTaskDocsToAttach.Items.Insert(0, new ListItem("[Not Applicable]", "0"));
        }

        private void LoadDocTypes(int Workflow_ID, SessionObject objSes)
        {
            cboDocType.Items.Clear();
            cboDocType.Items.Add(new ListItem("[System Generated Letters]", "System Generated Letters"));
            DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(Workflow_ID);
            string[] strDocs = dsWF.tblwalkflow[0].Workflow_Doc_Types.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string strDoc in strDocs)
            {
                if (strDoc.Trim() != "")
                {
                    cboDocType.Items.Add(new ListItem(strDoc, strDoc));
                    cboDocType_File.Items.Add(new ListItem(strDoc, strDoc));
                }
            }
            cboDocType.Items.Add(new ListItem("Other Documents", "Other Documents"));
            cboDocType_File.Items.Add(new ListItem("Other Documents", "Other Documents"));
        }

        private void LoadFieldsforConditions(DS_Workflow dsWF)
        {
            cboStepField.DataSource = dsWF.tblworkflow_step_fields;
            cboStepField.DataValueField = "Default_Text";
            cboStepField.DataTextField = "Field_Name";
            cboStepField.DataBind();

            cboStepField_File.DataSource = dsWF.tblworkflow_step_fields;
            cboStepField_File.DataValueField = "Default_Text";
            cboStepField_File.DataTextField = "Field_Name";
            cboStepField_File.DataBind();

            cboStepField_Email.DataSource = dsWF.tblworkflow_step_fields;
            cboStepField_Email.DataValueField = "Default_Text";
            cboStepField_Email.DataTextField = "Field_Name";
            cboStepField_Email.DataBind();
        }

        private void RefreshGrid(int Workflow_ID, int ActiveTab, SessionObject objSes)
        {
            string TabCSS1 = "";
            if (ActiveTab == 1)
            {
                TabCSS1 = " class='active'";
                lft_tab_1.CssClass = "tab-pane fade active in";
                lft_tab_2.CssClass = "tab-pane fade";
                lft_tab_3.CssClass = "tab-pane fade";
            }
            string TabCSS2 = "";
            if (ActiveTab == 2)
            {
                TabCSS2 = " class='active'";
                lft_tab_1.CssClass = "tab-pane fade";
                lft_tab_2.CssClass = "tab-pane fade active in";
                lft_tab_3.CssClass = "tab-pane fade";
            }
            string TabCSS3 = "";
            if (ActiveTab == 3)
            {
                TabCSS3 = " class='active'";
                lft_tab_1.CssClass = "tab-pane fade";
                lft_tab_2.CssClass = "tab-pane fade";
                lft_tab_3.CssClass = "tab-pane fade active in";
            }

            ltrTabs.Text = "<li" + TabCSS1 + ">" + "\r\n" +
                               "<a data-toggle='tab' href='#lft_tab_1'>Documents</a>" + "\r\n" +
                           "</li>" + "\r\n";
            ltrTabs.Text = ltrTabs.Text + " <li" + TabCSS3 + ">" + "\r\n" +
                                              "<a data-toggle='tab' href='#lft_tab_3'>Flat Files</a>" + "\r\n" +
                                          "</li>" + "\r\n";
            ltrTabs.Text = ltrTabs.Text + " <li" + TabCSS2 + ">" + "\r\n" +
                                              "<a data-toggle='tab' href='#lft_tab_2'>Emails</a>" + "\r\n" +
                                          "</li>" + "\r\n";

            WF_Document_Templates objMTemp = ObjectCreator.GetWF_Document_Templates(objSes.Connection, objSes.DB_Type);
            DS_WF_Doc_Templates ds = objMTemp.ReadAll(Workflow_ID, false);
            gvMain.SelectedIndex = -1;
            gvMain.DataSource = ds.tblworkflow_document_templates;
            gvMain.DataBind();
            if (ds.tblworkflow_document_templates.Rows.Count > 0)
            {
                gvMain.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            DS_WF_Doc_Templates ds3 = objMTemp.ReadAll(Workflow_ID, true);
            gvFlatFiles.SelectedIndex = -1;
            gvFlatFiles.DataSource = ds3.tblworkflow_document_templates;
            gvFlatFiles.DataBind();
            if (ds3.tblworkflow_document_templates.Rows.Count > 0)
            {
                gvFlatFiles.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            WF_Email_Templates objETemp = ObjectCreator.GetWF_Email_Templates(objSes.Connection, objSes.DB_Type);
            DS_WF_Email_Templates ds2 = objETemp.ReadAll(Workflow_ID);
            gvEmail.SelectedIndex = -1;
            gvEmail.DataSource = ds2.tblworkflow_email_templates;
            gvEmail.DataBind();
            if (ds2.tblworkflow_email_templates.Rows.Count > 0)
            {
                gvEmail.HeaderRow.TableSection = TableRowSection.TableHeader;
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
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndDocumentID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndDocumentID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            WF_Document_Templates objETemp = ObjectCreator.GetWF_Document_Templates(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objETemp.Delete(Convert.ToInt32(hndDocumentID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Document Template Successfully Deleted');", true);
                RefreshGrid(Convert.ToInt32(hndWF_ID.Value), 1, objSes);
                LoadAttachmentsForEMails(Convert.ToInt32(hndWF_ID.Value), objSes);
                hndDocumentID.Value = "0";
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            WF_Document_Templates objETemp = ObjectCreator.GetWF_Document_Templates(objSes.Connection, objSes.DB_Type);
            string DocTitle = "";
            string DocBody = txtDocumentBody.Text;
            if (chkUseDefault.Checked == false)
            {
                DocTitle = cboTemplates.Text;
                DocBody = "";
            }
            bool At_Begining = true;
            if (cboCreationTime.SelectedItem.Value == "0")
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

            if (hndDocumentID.Value == "0")
            {
                if (objETemp.Insert(txtDocumentName.Text, DocBody, DocTitle, Convert.ToInt32(hndWF_ID.Value), At_Begining, chkUseDefault.Checked, chkSingleDoc.Checked, cboDocType.SelectedItem.Value, Convert.ToInt32(cboAccessLevel.SelectedItem.Value), GetDocWorkflowSteps(), false, chkCondition.Checked, Step_Field, Operator, Criteria) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Document Template could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Document Template Details Successfully Added');", true);
                    RefreshGrid(Convert.ToInt32(hndWF_ID.Value), 1, objSes);
                    LoadAttachmentsForEMails(Convert.ToInt32(hndWF_ID.Value), objSes);
                    hndDocumentID.Value = "0";
                }
            }
            else
            {
                if (objETemp.Update(Convert.ToInt32((hndDocumentID.Value)), txtDocumentName.Text, DocBody, DocTitle, Convert.ToInt32(hndWF_ID.Value), At_Begining, chkUseDefault.Checked, chkSingleDoc.Checked, cboDocType.SelectedItem.Value, Convert.ToInt32(cboAccessLevel.SelectedItem.Value), GetDocWorkflowSteps(), chkCondition.Checked, Step_Field, Operator, Criteria) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Document Template could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Document Template Details Successfully Updated');", true);
                    RefreshGrid(Convert.ToInt32(hndWF_ID.Value), 1, objSes);
                    LoadAttachmentsForEMails(Convert.ToInt32(hndWF_ID.Value), objSes);
                    hndDocumentID.Value = "0";
                }
            }
        }

        private DS_WF_Doc_Templates.tblworkflow_document_templates_subDataTable GetDocWorkflowSteps()
        {
            DS_WF_Doc_Templates ds = new DS_WF_Doc_Templates();
            DS_WF_Doc_Templates.tblworkflow_document_templates_subRow dr;
            foreach (ListItem itm in chkAllowedWorkflowStep.Items)
            {
                if (itm.Selected == true)
                {
                    dr = ds.tblworkflow_document_templates_sub.Newtblworkflow_document_templates_subRow();
                    dr.Document_ID = 0;
                    dr.Workflow_Step_ID = Convert.ToInt32(itm.Value);
                    ds.tblworkflow_document_templates_sub.Rows.Add(dr);
                }
            }
            return ds.tblworkflow_document_templates_sub;
        }

        protected void gvEmail_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndEmail_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValuesEmail();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndEmail_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
            }
        }

        protected void cmdSaveEmail_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            WF_Email_Templates objETemp = ObjectCreator.GetWF_Email_Templates(objSes.Connection, objSes.DB_Type);
            string MeetingTime = "";
            string MeetingDuration = "";
            if (chkSendMeetingRequest.Checked == true)
            {
                MeetingTime = txtMeetingDate.Text;
                MeetingDuration = txtMeetingDuration.Text;
            }
            bool At_Begining = true;
            if (cboSentTime.SelectedItem.Value == "0")
            {
                At_Begining = false;
            }

            int Step_Field = 0;
            int Operator = 0;
            string Criteria = "";
            if (chkCondition_Email.Checked)
            {
                Step_Field = Convert.ToInt32(cboStepField_Email.SelectedItem.Value.Split('_')[0]);
                Operator = Convert.ToInt32(cboOperator_Email.SelectedItem.Value);
                Criteria = txtCriteria_Email.Text;
            }

            if (hndEmail_ID.Value == "0")
            {
                if (objETemp.Insert(txtEmailName.Text, txtEmailAddress.Text, txtCCAddresses.Text, txtEmailBody.Text, txtEmailTitle.Text, chkSendMeetingRequest.Checked, MeetingTime, MeetingDuration, Convert.ToInt32(hndWF_ID.Value), Convert.ToInt32(cboDocumentToAttach.SelectedItem.Value), Convert.ToInt32(cboExtractToAttach.SelectedItem.Value), cboTaskDocsToAttach.SelectedItem.Text, At_Begining, GetEmailWorkflowSteps(), chkCondition_Email.Checked, Step_Field, Operator, Criteria, txtFromName.Text, txtFromAddress.Text) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('E-Mail Template could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('E-Mail Template Details Successfully Added');", true);
                    RefreshGrid(Convert.ToInt32(hndWF_ID.Value), 2, objSes);
                    hndEmail_ID.Value = "0";
                }
            }
            else
            {
                if (objETemp.Update(Convert.ToInt32((hndEmail_ID.Value)), txtEmailName.Text, txtEmailAddress.Text, txtCCAddresses.Text, txtEmailBody.Text, txtEmailTitle.Text, chkSendMeetingRequest.Checked, MeetingTime, MeetingDuration, Convert.ToInt32(hndWF_ID.Value), Convert.ToInt32(cboDocumentToAttach.SelectedItem.Value), Convert.ToInt32(cboExtractToAttach.SelectedItem.Value), cboTaskDocsToAttach.SelectedItem.Text, At_Begining, GetEmailWorkflowSteps(), chkCondition_Email.Checked, Step_Field, Operator, Criteria, txtFromName.Text, txtFromAddress.Text) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('E-Mail Template could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('E-Mail Template Details Successfully Updated');", true);
                    RefreshGrid(Convert.ToInt32(hndWF_ID.Value), 2, objSes);
                    hndEmail_ID.Value = "0";
                }
            }
        }

        protected void cmdDeleteEmail_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            WF_Email_Templates objETemp = ObjectCreator.GetWF_Email_Templates(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objETemp.Delete(Convert.ToInt32(hndEmail_ID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('E-Mail Template Successfully Deleted');", true);
                RefreshGrid(Convert.ToInt32(hndWF_ID.Value), 2, objSes);
                hndEmail_ID.Value = "0";
            }
        }

        private DS_WF_Email_Templates.tblworkflow_email_templates_subDataTable GetEmailWorkflowSteps()
        {
            DS_WF_Email_Templates ds = new DS_WF_Email_Templates();
            DS_WF_Email_Templates.tblworkflow_email_templates_subRow dr;
            foreach (ListItem itm in chkAllowedWorkflowStepEmail.Items)
            {
                if (itm.Selected == true)
                {
                    dr = ds.tblworkflow_email_templates_sub.Newtblworkflow_email_templates_subRow();
                    dr.Email_ID = 0;
                    dr.Workflow_Step_ID = Convert.ToInt32(itm.Value);
                    ds.tblworkflow_email_templates_sub.Rows.Add(dr);
                }
            }
            return ds.tblworkflow_email_templates_sub;
        }

        protected void gvFlatFiles_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndDocumentID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValuesFile();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndDocumentID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
            }
        }

        protected void cmdSaveFile_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            WF_Document_Templates objETemp = ObjectCreator.GetWF_Document_Templates(objSes.Connection, objSes.DB_Type);
            bool At_Begining = true;
            if (cboCreationTime_File.SelectedItem.Value == "0")
            {
                At_Begining = false;
            }

            int Step_Field = 0;
            int Operator = 0;
            string Criteria = "";
            if (chkCondition_File.Checked)
            {
                Step_Field = Convert.ToInt32(cboStepField_File.SelectedItem.Value.Split('_')[0]);
                Operator = Convert.ToInt32(cboOperator_File.SelectedItem.Value);
                Criteria = txtCriteria_File.Text;
            }

            if (hndDocumentID.Value == "0")
            {
                if (objETemp.Insert(txtFileName.Text, txtFileContent.Text, txtExtension.Text, Convert.ToInt32(hndWF_ID.Value), At_Begining, false, chkOneFile_File.Checked, cboDocType_File.SelectedItem.Value, Convert.ToInt32(cboAccessLevel_File.SelectedItem.Value), GetFileWorkflowSteps(), true, chkCondition_File.Checked, Step_Field, Operator, Criteria) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Flat File Template could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Flat File Template Details Successfully Added');", true);
                    RefreshGrid(Convert.ToInt32(hndWF_ID.Value), 3, objSes);
                    LoadAttachmentsForEMails(Convert.ToInt32(hndWF_ID.Value), objSes);
                    hndDocumentID.Value = "0";
                }
            }
            else
            {
                if (objETemp.Update(Convert.ToInt32((hndDocumentID.Value)), txtFileName.Text, txtFileContent.Text, txtExtension.Text, Convert.ToInt32(hndWF_ID.Value), At_Begining, false, chkOneFile_File.Checked, cboDocType_File.SelectedItem.Value, Convert.ToInt32(cboAccessLevel_File.SelectedItem.Value), GetFileWorkflowSteps(), chkCondition_File.Checked, Step_Field, Operator, Criteria) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Flat File Template could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Flat File Template Details Successfully Updated');", true);
                    RefreshGrid(Convert.ToInt32(hndWF_ID.Value), 3, objSes);
                    LoadAttachmentsForEMails(Convert.ToInt32(hndWF_ID.Value), objSes);
                    hndDocumentID.Value = "0";
                }
            }
        }

        private DS_WF_Doc_Templates.tblworkflow_document_templates_subDataTable GetFileWorkflowSteps()
        {
            DS_WF_Doc_Templates ds = new DS_WF_Doc_Templates();
            DS_WF_Doc_Templates.tblworkflow_document_templates_subRow dr;
            foreach (ListItem itm in chkAllowedWorkflowStep_File.Items)
            {
                if (itm.Selected == true)
                {
                    dr = ds.tblworkflow_document_templates_sub.Newtblworkflow_document_templates_subRow();
                    dr.Document_ID = 0;
                    dr.Workflow_Step_ID = Convert.ToInt32(itm.Value);
                    ds.tblworkflow_document_templates_sub.Rows.Add(dr);
                }
            }
            return ds.tblworkflow_document_templates_sub;
        }
    }
}