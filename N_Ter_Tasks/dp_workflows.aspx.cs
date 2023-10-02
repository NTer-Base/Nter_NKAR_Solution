using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class dp_workflows : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                if (Request.QueryString["rid"] == null)
                {
                    Response.Redirect("error.aspx?");
                }
                else
                {
                    SessionObject objSes = (SessionObject)Session["dt"];

                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    int Repo_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["rid"])));
                    ViewState["rid"] = Repo_ID;

                    Document_Projects objDP = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
                    DS_Doc_Project ds = objDP.Read(Repo_ID);
                    ltrRepoName.Text = ds.tbldocument_project[0].Doc_Project_Name;

                    LoadWorkflows(Repo_ID, objSes);
                    LoadTags(ds.tbldocument_project_indexes);

                    RefreshGrid(Repo_ID, objSes);
                }
            }

            cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndDP_WF_ID.ClientID + "').value = '0'; ClearControls();");
            cboWorkflow.Attributes.Add("onChange", "LoadWFConnectionPane();");
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidateDPWF('" + cboWorkflow.ClientID + "');";
        }

        private void LoadWorkflows(int Doc_Project_ID, SessionObject objSes)
        {
            DS_Workflow ds = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).ReadAll();
            cboWorkflow.DataSource = ds.tblwalkflow;
            cboWorkflow.DataTextField = "Workflow_Name";
            cboWorkflow.DataValueField = "Walkflow_ID";
            cboWorkflow.DataBind();
            cboWorkflow.Items.Insert(0, new ListItem("[Not Selected]", "0"));
        }

        private void LoadTags(DS_Doc_Project.tbldocument_project_indexesDataTable tbl)
        {
            cboTags1.DataSource = tbl;
            cboTags1.DataTextField = "Tag_Name";
            cboTags1.DataValueField = "Document_Project_Index_ID";
            cboTags1.DataBind();
            cboTags1.Items.Insert(0, new ListItem("[Document Number]", "-1"));
            cboTags1.Items.Insert(0, new ListItem("[N/A]", "0"));

            cboTags2.DataSource = tbl;
            cboTags2.DataTextField = "Tag_Name";
            cboTags2.DataValueField = "Document_Project_Index_ID";
            cboTags2.DataBind();
            cboTags2.Items.Insert(0, new ListItem("[Document Number]", "-1"));
            cboTags2.Items.Insert(0, new ListItem("[N/A]", "0"));
        }

        private void RefreshGrid(int Doc_Project_ID, SessionObject objSes)
        {
            Document_Projects_Workflows objDPWF = ObjectCreator.GetDocument_Project_Workflow(objSes.Connection, objSes.DB_Type);
            DS_DP_Workflow ds = objDPWF.ReadAll(Doc_Project_ID);
            gvMain.SelectedIndex = -1;
            gvMain.DataSource = ds.tbldocument_project_workflows;
            gvMain.DataBind();
            if (ds.tbldocument_project_workflows.Rows.Count > 0)
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
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndDP_WF_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndDP_WF_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            int Repo_ID = Convert.ToInt32(ViewState["rid"]);
            Document_Projects_Workflows objDPWF = ObjectCreator.GetDocument_Project_Workflow(objSes.Connection, objSes.DB_Type);
            if (hndDP_WF_ID.Value == "0")
            {
                if (objDPWF.Insert(Repo_ID, Convert.ToInt32(cboWorkflow.SelectedItem.Value), Convert.ToInt32(cboTags1.SelectedItem.Value), Convert.ToInt32(cboTags2.SelectedItem.Value)) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Document Repo Workflow Link could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Document Repo Workflow Link Successfully Added');", true);
                    hndDP_WF_ID.Value = "0";
                }
            }
            else
            {
                if (objDPWF.Update(Convert.ToInt32(hndDP_WF_ID.Value), Convert.ToInt32(cboWorkflow.SelectedItem.Value), Convert.ToInt32(cboTags1.SelectedItem.Value), Convert.ToInt32(cboTags2.SelectedItem.Value)) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Document Repo Workflow Link could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Document Repo Workflow Link Successfully Updated');", true);
                    hndDP_WF_ID.Value = "0";
                }
            }

            RefreshGrid(Repo_ID, objSes);
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Document_Projects_Workflows objDPWF = ObjectCreator.GetDocument_Project_Workflow(objSes.Connection, objSes.DB_Type);

            DeleteReason del = objDPWF.Delete(Convert.ToInt32(hndDP_WF_ID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Document Repo Workflow Link Successfully Deleted');", true);
                hndDP_WF_ID.Value = "0";
            }
            int Repo_ID = Convert.ToInt32(ViewState["rid"]);
            RefreshGrid(Repo_ID, objSes);
        }
    }
}