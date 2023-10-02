using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class wf_categories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                hndWF_CatID.Value = "0";
                RefreshGrid();
            }

            cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndWF_CatID.ClientID + "').value = '0'; ClearControls();");
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidateWF_Cat('" + txtCat_Name.ClientID + "');";
        }

        private void RefreshGrid()
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow_Categories objCategory = ObjectCreator.GetWorkflow_Categories(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objCategory.ReadAll();
            gvMain.SelectedIndex = -1;
            gvMain.DataSource = ds.tblworkflow_categories;
            gvMain.DataBind();
            if (ds.tblworkflow_categories.Rows.Count > 0)
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
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndWF_CatID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndWF_CatID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow_Categories category = ObjectCreator.GetWorkflow_Categories(objSes.Connection, objSes.DB_Type);
            DeleteReason del = category.Delete(Convert.ToInt32(hndWF_CatID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Category Successfully Deleted');", true);
                RefreshGrid();
                hndWF_CatID.Value = "0";
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow_Categories objWorkflowCategory = ObjectCreator.GetWorkflow_Categories(objSes.Connection, objSes.DB_Type);
            if (hndWF_CatID.Value == "0")
            {
                if (objWorkflowCategory.Insert(txtCat_Name.Text) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Category could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Category Details Successfully Added');", true);
                    RefreshGrid();
                    hndWF_CatID.Value = "0";
                }
            }
            else
            {
                if (objWorkflowCategory.Update(Convert.ToInt32((hndWF_CatID.Value)), txtCat_Name.Text) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Category could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Category Details Successfully Updated');", true);
                    RefreshGrid();
                    hndWF_CatID.Value = "0";
                }
            }
        }
    }
}