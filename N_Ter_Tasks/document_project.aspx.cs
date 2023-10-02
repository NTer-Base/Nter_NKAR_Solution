using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter.Base;

namespace N_Ter_Tasks
{
    public partial class document_project : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                SessionObject objSes = (SessionObject)Session["dt"];

                if (Request.QueryString["rid"] == null)
                {
                    Response.Redirect("error.aspx?");
                }
                else
                {
                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    int Document_Project_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["rid"])));
                    ViewState["rid"] = Document_Project_ID;

                    LoadMasterTableNames(objSes);
                    RefreshGrid(objSes, Document_Project_ID);
                }
            }

            cmdNewTag.Attributes.Add("onClick", "javascript:document.getElementById('" + hndTagID.ClientID + "').value = '0'; ClearControls(); ShowSelectionText();");
            cmdResetTag.Attributes.Add("onClick", "return ResetControls();");
            cmdSaveTag.Attributes.Add("onClick", "return ValidateDocProjectTag('" + txtTagName.ClientID + "');");
            cboTagType.Attributes.Add("onChange", "ShowSelectionText()");
        }

        private void LoadMasterTableNames(SessionObject objSes)
        {
            N_Ter.Customizable.Custom_Lists objCus = new N_Ter.Customizable.Custom_Lists();
            DS_Master_Tables ds = objCus.LoadMasterTableNames(objSes.EL2P);
            cboMasterTable.DataSource = ds.tblTables;
            cboMasterTable.DataTextField = "Table_Name";
            cboMasterTable.DataValueField = "Table_ID";
            cboMasterTable.DataBind();
            cboMasterTable.Items.Insert(0, new ListItem("[Not Selected]", "0"));
        }

        private void RefreshGrid(SessionObject objSes, int Document_Project_ID)
        {
            Document_Projects objDP = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
            DS_Doc_Project ds = objDP.ReadWithLabels(Document_Project_ID);
            foreach (DS_Doc_Project.tbldocument_project_indexesRow row in ds.tbldocument_project_indexes)
            {
                switch (row.Tag_Type)
                {
                    case 0:
                        row.Tag_TypeSP = "Label Only";
                        break;
                    case 1:
                        row.Tag_TypeSP = "Yes/No (Dropdown)";
                        break;
                    case 2:
                        row.Tag_TypeSP = "Text";
                        break;
                    case 3:
                        row.Tag_TypeSP = "Memo";
                        break;
                    case 4:
                        row.Tag_TypeSP = "Number";
                        break;
                    case 5:
                        row.Tag_TypeSP = "Selection";
                        break;
                    case 6:
                        row.Tag_TypeSP = "Date";
                        break;
                    case 7:
                        row.Tag_TypeSP = "Time";
                        break;
                    case 8:
                        row.Tag_TypeSP = "Currency";
                        break;
                    case 9:
                        row.Tag_TypeSP = "Percentage";
                        break;
                    case 10:
                        row.Tag_TypeSP = "Master Table";
                        break;
                    case 12:
                        row.Tag_TypeSP = "Time Span";
                        break;
                    case 13:
                        row.Tag_TypeSP = "Yes/No (Switch)";
                        break;
                }
            }
            gvTags.SelectedIndex = -1;
            gvTags.DataSource = ds.tbldocument_project_indexes;
            gvTags.DataBind();
            if (ds.tbldocument_project_indexes.Rows.Count > 0)
            {
                gvTags.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void cmdDeleteTag_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Document_Projects objDP = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objDP.DeleteTag(Convert.ToInt32(hndTagID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Document Project Tag Successfully Deleted');", true);

                int Document_Project_ID = Convert.ToInt32(ViewState["rid"]);
                RefreshGrid(objSes, Document_Project_ID);
                hndTagID.Value = "0";
            }
        }

        protected void gvTags_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                if (e.Row.Cells[1].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdEdit = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[1].Controls[1]);
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndTagID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndTagID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
                e.Row.Attributes.Add("data-id", e.Row.Cells[0].Text);
            }
        }

        protected void cmdSaveTag_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Document_Project_ID = Convert.ToInt32(ViewState["rid"]);
            Document_Projects objDP = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
            if (hndTagID.Value == "0")
            {
                if (objDP.InsertTag(txtTagName.Text, Convert.ToInt32(cboTagType.SelectedItem.Value), txtSelectionText.Text, txtDefaultText.Text, chkFieldRequired.Checked, Convert.ToInt32(string.IsNullOrEmpty(txtTagMaxLength.Text.Trim(' ')) ? "-1" : txtTagMaxLength.Text), Document_Project_ID, Convert.ToInt32(cboFieldSize.SelectedItem.Value), Convert.ToInt32(cboMasterTable.SelectedItem.Value), chkTypable.Checked, txtHelpText.Text) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Document Project Tag could not be Saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Document Project Tag Successfully Saved');", true);

                    RefreshGrid(objSes, Document_Project_ID);
                    hndTagID.Value = "0";
                }
            }
            else
            {
                if (objDP.UpdateTag(Convert.ToInt32(hndTagID.Value), txtTagName.Text, Convert.ToInt32(cboTagType.SelectedItem.Value), txtSelectionText.Text, txtDefaultText.Text, chkFieldRequired.Checked, Convert.ToInt32(string.IsNullOrEmpty(txtTagMaxLength.Text.Trim(' ')) ? "-1" : txtTagMaxLength.Text), Document_Project_ID, Convert.ToInt32(cboFieldSize.SelectedItem.Value), Convert.ToInt32(cboMasterTable.SelectedItem.Value), chkTypable.Checked, txtHelpText.Text) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Document Project Tag could not be Saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Document Project Tag Successfully Saved');", true);

                    RefreshGrid(objSes, Document_Project_ID);
                    hndTagID.Value = "0";
                }
            }
        }
    }
}