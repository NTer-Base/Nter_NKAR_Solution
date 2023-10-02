using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class wf_repos : System.Web.UI.Page
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
                    int WFID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["fid"])));
                    ViewState["fid"] = WFID;

                    Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                    DS_Workflow ds = objWF.Read(WFID);
                    ltrWorkflowName.Text = ds.tblwalkflow[0].Workflow_Name;

                    LoadDocTypes(ds.tblwalkflow[0].Workflow_Doc_Types);
                    LoadSteps(ds.tblworkflow_steps);
                    LoadDocRepos(objSes);
                    LoadFieldsforConditions(objWF.ReadAllEvalOKFields(WFID));

                    RefreshGrid(WFID, objSes);
                }
            }

            cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndWP_DP_ID.ClientID + "').value = '0'; ClearControls(true);");
            cboDocRepo.Attributes.Add("onChange", "LoadDPConnectionPane(false);");
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidateWFDP('" + cboDocRepo.ClientID + "', '" + hndRepoDetails.ClientID + "');";

            cboStepField.Attributes.Add("onChange", "SelectOperators('" + cboStepField.ClientID + "', '" + cboOperator.ClientID + "');");
            chkCondition.Attributes.Add("onClick", "CheckConditions('" + chkCondition.ClientID + "', '" + cboStepField.ClientID + "', '" + cboOperator.ClientID + "', '" + txtCriteria.ClientID + "', 'divCondition', 'cboCondtTemp', 'txtCondtDateTemp', '', true);");
        }

        private void LoadDocTypes(string DocTypes)
        {
            cboDocType.Items.Clear();
            cboDocType.Items.Add(new ListItem("[All Documents]", "0"));
            string[] strDocs = DocTypes.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string strDoc in strDocs)
            {
                if (strDoc.Trim() != "")
                {
                    cboDocType.Items.Add(new ListItem(strDoc, strDoc));
                }
            }
            cboDocType.Items.Add(new ListItem("Other Documents", "Other Documents"));
        }

        private void LoadSteps(DS_Workflow.tblworkflow_stepsDataTable tblSteps)
        {
            cboSteps.DataSource = tblSteps;
            cboSteps.DataTextField = "Step_Status";
            cboSteps.DataValueField = "Workflow_Step_ID";
            cboSteps.DataBind();
            cboSteps.Items.Insert(0, new ListItem("[Completed Status]", "-1"));
        }

        private void LoadDocRepos(SessionObject objSes)
        {
            Document_Projects objDoc = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
            DS_Doc_Project ds = objDoc.ReadAll();
            cboDocRepo.DataSource = ds.tbldocument_project;
            cboDocRepo.DataTextField = "Doc_Project_Name";
            cboDocRepo.DataValueField = "Document_Project_ID";
            cboDocRepo.DataBind();
            cboDocRepo.Items.Insert(0, new ListItem("[Not Selected]", "0"));
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
            Workflow_Document_Project objWFDP = ObjectCreator.GetWorkflow_Document_Projects(objSes.Connection, objSes.DB_Type);
            DS_WF_Doc_Projects ds = objWFDP.ReadAll(Workflow_ID);
            gvMain.SelectedIndex = -1;
            gvMain.DataSource = ds.tblworkflow_document_projects;
            gvMain.DataBind();
            if (ds.tblworkflow_document_projects.Rows.Count > 0)
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
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndWP_DP_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndWP_DP_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
            }
        }

        private DS_WF_Doc_Projects.tblworkflow_document_projects_subDataTable GetTagFields()
        {
            DS_WF_Doc_Projects ds = new DS_WF_Doc_Projects();
            string[] TagFields = hndRepoDetails.Value.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string TagField in TagFields)
            {
                if (TagField.Trim() != "")
                {
                    string[] TagElements = TagField.Split('|');
                    if (TagElements.Length == 2)
                    {
                        DS_WF_Doc_Projects.tblworkflow_document_projects_subRow dr = ds.tblworkflow_document_projects_sub.Newtblworkflow_document_projects_subRow();
                        dr.WF_DP_ID = 0;
                        dr.Workflow_Step_Field_ID = Convert.ToInt32(TagElements[1]);
                        dr.Document_Project_Index_ID = Convert.ToInt32(TagElements[0]);
                        ds.tblworkflow_document_projects_sub.Rows.Add(dr);
                    }
                }
            }
            return ds.tblworkflow_document_projects_sub;
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            int Workflow_ID = Convert.ToInt32(ViewState["fid"]);
            Workflow_Document_Project objWHDP = ObjectCreator.GetWorkflow_Document_Projects(objSes.Connection, objSes.DB_Type);

            int Step_Field = 0;
            int Operator = 0;
            string Criteria = "";
            if (chkCondition.Checked)
            {
                Step_Field = Convert.ToInt32(cboStepField.SelectedItem.Value.Split('_')[0]);
                Operator = Convert.ToInt32(cboOperator.SelectedItem.Value);
                Criteria = txtCriteria.Text;
            }

            if (hndWP_DP_ID.Value == "0")
            {
                if (objWHDP.Insert(Workflow_ID, cboDocType.SelectedItem.Value, Convert.ToInt32(cboDocRepo.SelectedItem.Value), Convert.ToInt32(cboAccessLevel.SelectedItem.Value), Convert.ToInt32(cboSteps.SelectedItem.Value), GetTagFields(), chkCondition.Checked, Step_Field, Operator, Criteria) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Document Repo could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Document Repo Successfully Added');", true);
                    hndWP_DP_ID.Value = "0";
                }
            }
            else
            {
                if (objWHDP.Update(Convert.ToInt32(hndWP_DP_ID.Value), cboDocType.SelectedItem.Value, Convert.ToInt32(cboDocRepo.SelectedItem.Value), Convert.ToInt32(cboAccessLevel.SelectedItem.Value), Convert.ToInt32(cboSteps.SelectedItem.Value), GetTagFields(), chkCondition.Checked, Step_Field, Operator, Criteria) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Document Repo could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Document Repo Successfully Updated');", true);
                    hndWP_DP_ID.Value = "0";
                }
            }

            RefreshGrid(Workflow_ID, objSes);
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow_Document_Project objWHDP = ObjectCreator.GetWorkflow_Document_Projects(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objWHDP.Delete(Convert.ToInt32(hndWP_DP_ID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Document Repo Successfully Deleted');", true);
                hndWP_DP_ID.Value = "0";
            }
            int Workflow_ID = Convert.ToInt32(ViewState["fid"]);
            RefreshGrid(Workflow_ID, objSes);
        }
    }
}