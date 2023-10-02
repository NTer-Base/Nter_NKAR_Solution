using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter.Base;

namespace N_Ter_Tasks
{
    public partial class entity_level_1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                SessionObject objSes = (SessionObject)Session["dt"];

                ltrEL1.Text = objSes.EL1P;
                ltrEL1_2.Text = objSes.EL1P;
                ltrEL1_3.Text = objSes.EL1;
                ltrEL1_4.Text = objSes.EL1;
                ltrEL1_5.Text = objSes.EL1;
                ltrEL1_6.Text = objSes.EL1;

                gvMain.Columns[3].HeaderText = objSes.EL1 + " Code";
                gvMain.Columns[4].HeaderText = objSes.EL1 + " Name";

                hndEL1_ID.Value = "0";
                RefreshGrid(objSes);
            }

            cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndEL1_ID.ClientID + "').value = '0'; ClearControls();");
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidateEL1('" + txtDisplayName.ClientID + "', '" + txtEntityName.ClientID + "', '" + txtEMail.ClientID + "');";
        }

        private void RefreshGrid(SessionObject objSes)
        {
            Entity_Level_1 objRef = ObjectCreator.GetEntity_Level_1(objSes.Connection, objSes.DB_Type);
            DS_Entity_Level_1 ds = objRef.ReadAll();
            gvMain.SelectedIndex = -1;
            gvMain.DataSource = ds.tblentity_level_1;
            gvMain.DataBind();
            if (ds.tblentity_level_1.Rows.Count > 0)
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
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndEL1_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndEL1_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Entity_Level_1 objRef = ObjectCreator.GetEntity_Level_1(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objRef.Delete(Convert.ToInt32(hndEL1_ID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('" + objSes.EL1 + " Successfully Deleted');", true);
                RefreshGrid(objSes);
                hndEL1_ID.Value = "0";
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            string Entity_Level_1_Name = objSes.EL1;
            Entity_Level_1 objRef = ObjectCreator.GetEntity_Level_1(objSes.Connection, objSes.DB_Type);
            if (hndEL1_ID.Value == "0")
            {
                if (objRef.Insert(txtEntityName.Text, txtEntityCode.Text, txtDisplayName.Text, txtPhone.Text, txtEMail.Text, txtMainContact.Text) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + Entity_Level_1_Name + " could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('" + Entity_Level_1_Name + " Details Successfully Added');", true);
                    RefreshGrid(objSes);
                    hndEL1_ID.Value = "0";
                }
            }
            else
            {
                if (objRef.Update(Convert.ToInt32(hndEL1_ID.Value), txtEntityName.Text, txtEntityCode.Text, txtDisplayName.Text, txtPhone.Text, txtEMail.Text, txtMainContact.Text) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + Entity_Level_1_Name + " could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('" + Entity_Level_1_Name + " Details Successfully Updated');", true);
                    RefreshGrid(objSes);
                    hndEL1_ID.Value = "0";
                }
            }
        }
    }
}