using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter.Base;

namespace N_Ter_Tasks
{
    public partial class document_projects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                SessionObject objSes = (SessionObject)Session["dt"];
                hndDocProjectID.Value = "0";
                LoadUserGroup(objSes);
                RefreshGrid(objSes);
            }
            cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndDocProjectID.ClientID + "').value = '0'; ClearControls();");
            cmdReset.OnClientClick = "return ClearControls();";
            cmdSave.OnClientClick = "return ValidateDocProject('" + txtProjectName.ClientID + "');";
        }

        private void RefreshGrid(SessionObject objSes)
        {
            Document_Projects objDP = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
            DS_Doc_Project ds = objDP.ReadAll();
            gvMain.SelectedIndex = -1;
            gvMain.DataSource = ds.tbldocument_project;
            gvMain.DataBind();
            if (ds.tbldocument_project.Rows.Count > 0)
            {
                gvMain.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void LoadUserGroup(SessionObject objSes)
        {
            User_Groups objUG = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type);
            DS_Users ds = objUG.ReadAll();
            cboUserGroup.DataSource = ds.tbluser_groups;
            cboUserGroup.DataValueField = "User_Group_ID";
            cboUserGroup.DataTextField = "User_Group_Name";
            cboUserGroup.DataBind();
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Document_Projects objDP = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objDP.Delete(Convert.ToInt32(hndDocProjectID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Document Project Successfully Deleted');", true);
                hndDocProjectID.Value = "0";
            }
            RefreshGrid(objSes);
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Document_Projects objDP = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
            int RetentionAmount;
            if (!int.TryParse(txtRetentionAmount.Text, out RetentionAmount))
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Invalid Retention Amount');", true);
            }
            else
            {
                if (hndDocProjectID.Value == "0")
                {
                    int Max_ID = objDP.Insert(txtProjectName.Text, Convert.ToInt32(cboUserGroup.SelectedItem.Value), txtGuestHelp.Text, 
                        Convert.ToInt32(cboRetentionType.SelectedItem.Value), RetentionAmount);
                    if (Max_ID > 0)
                    {
                        N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                        Response.Redirect("document_projects.aspx?dpid=" + objURL.Encrypt(Convert.ToString(Max_ID)));
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Document project could not be saved');", true);
                    }
                }
                else
                {
                    if (objDP.Update(Convert.ToInt32(hndDocProjectID.Value), txtProjectName.Text, Convert.ToInt32(cboUserGroup.SelectedItem.Value), 
                        txtGuestHelp.Text, Convert.ToInt32(cboRetentionType.SelectedItem.Value), RetentionAmount))
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Document project Successfully saved');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Document project could not be saved');", true);
                    }
                }
            }
            RefreshGrid(objSes);
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                N_Ter.Common.Common_Actions objComAct = new N_Ter.Common.Common_Actions();

                SessionObject objSes = (SessionObject)Session["dt"];
                Document_Projects objDP = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);

                if (e.Row.Cells[1].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdEdit = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[1].Controls[1]);
                    cmdEdit.Attributes.Add("onclick", "$('#" + hndDocProjectID.ClientID + "').val('" + e.Row.Cells[0].Text + "'); LoadValues(); return false;");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    DS_Doc_Project dsAct = objDP.ReadActionCounts(Convert.ToInt32(e.Row.Cells[0].Text));
                    Literal ltrActions = ((Literal)e.Row.Cells[2].Controls[1]);
                    ltrActions.Text = objComAct.GetDocRepoActionMenu(objURL.Encrypt(e.Row.Cells[0].Text), " btn-xs", " btn-success", "", dsAct.tblaction_counts[0]);
                }
                if (e.Row.Cells[3].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[3].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndDocProjectID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
            }
        }
    }
}