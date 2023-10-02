using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter.Base;

namespace N_Ter_Tasks
{
    public partial class task_queues : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            if (IsPostBack == false)
            {
                hndQueueID.Value = "0";
                RefreshGrid(objSes);
            }

            LoadColors(objSes);

            cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndQueueID.ClientID + "').value = '0'; ClearControls();");
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidateQueue('" + txtQueueName.ClientID + "');";
        }

        private void LoadColors(SessionObject objSes)
        {
            string Selected_ID = "0";
            if (IsPostBack)
            {
                Selected_ID = cboColor.SelectedValue;
            }
            DS_CSS_Colors ds = ObjectCreator.GetCSS_Colors(objSes.Connection, objSes.DB_Type).ReadAll();
            cboColor.Items.Clear();
            foreach (DS_CSS_Colors.tblcolorsRow row in ds.tblcolors)
            {
                ListItem itm = new ListItem(row.Color_Name, row.Color_ID.ToString());
                itm.Attributes.Add("style", "background-color: " + row.Color_Name);
                cboColor.Items.Add(itm);
            }
            cboColor.SelectedValue = Selected_ID;
        }

        private void RefreshGrid(SessionObject objSes)
        {
            Task_Queues objQue = ObjectCreator.GetTask_Queues(objSes.Connection, objSes.DB_Type);
            DS_Task_Queues ds = objQue.ReadAll();
            gvMain.SelectedIndex = -1;
            gvMain.DataSource = ds.tbltask_queues;
            gvMain.DataBind();
            if (ds.tbltask_queues.Rows.Count > 0)
            {
                gvMain.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[5].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdEdit = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[1].Controls[1]);
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndQueueID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndQueueID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
                if (e.Row.Cells[4].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl divColor = ((System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.Cells[4].Controls[1]);
                    divColor.Attributes.Add("style", "background: " + e.Row.Cells[5].Text.Trim());
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Task_Queues objQue = ObjectCreator.GetTask_Queues(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objQue.Delete(Convert.ToInt32(hndQueueID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Task Queue Successfully Deleted');", true);
                RefreshGrid(objSes);
                hndQueueID.Value = "0";
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Task_Queues objQue = ObjectCreator.GetTask_Queues(objSes.Connection, objSes.DB_Type);
            if (hndQueueID.Value == "0")
            {
                if (objQue.Insert(txtQueueName.Text, Convert.ToInt32(cboColor.SelectedItem.Value)) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Task Queue could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Task Queue Details Successfully Added');", true);
                    RefreshGrid(objSes);
                    hndQueueID.Value = "0";
                }
            }
            else
            {
                if (objQue.Update(Convert.ToInt32((hndQueueID.Value)), txtQueueName.Text, Convert.ToInt32(cboColor.SelectedItem.Value)) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Task Queue could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Task Queue Details Successfully Updated');", true);
                    RefreshGrid(objSes);
                    hndQueueID.Value = "0";
                }
            }
        }
    }
}