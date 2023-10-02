using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter.Base;

namespace N_Ter_Tasks
{
    public partial class dp_doc_templates : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                if (Request.QueryString["rid"] != null)
                {
                    SessionObject objSes = (SessionObject)Session["dt"];
                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    int DP_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["rid"])));
                    DS_Doc_Project ds = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type).Read(DP_ID);
                    if (ds.tbldocument_project.Rows.Count > 0)
                    {
                        lblDocProjectName.Text = ds.tbldocument_project[0].Doc_Project_Name;
                    }
                    hndEmail_ID.Value = "0";
                    hndDP_ID.Value = Convert.ToString(DP_ID);
                    RefreshGrid(DP_ID, 1, objSes);

                    ltrEL2.Text = objSes.EL2;
                    ltrEL2_1.Text = objSes.EL2;

                    ltrEL1.Text = objSes.EL1;
                    ltrEL1_1.Text = objSes.EL1;
                }
                else
                {
                    Response.Redirect("error.aspx?");
                }
            }

            cmdNewEmail.Attributes.Add("onClick", "javascript:document.getElementById('" + hndEmail_ID.ClientID + "').value = '0'; ClearControlsEmail();");
            cmdResetEmail.OnClientClick = "return ResetControlsEmail();";
            cmdSaveEmail.OnClientClick = "return ValidateDPEmailTemplate('" + txtEmailName.ClientID + "','" + txtEmailBody.ClientID + "', '" + txtEmailAddress.ClientID + "');";
        }

        private void RefreshGrid(int Document_Project_ID, int ActiveTab, SessionObject objSes)
        {
            string TabCSS1 = "";
            if (ActiveTab == 1)
            {
                TabCSS1 = " class='active'";
                lft_tab_1.CssClass = "tab-pane fade active in";
            }

            ltrTabs.Text = "<li" + TabCSS1 + ">" + "\r\n" +
                               "<a data-toggle='tab' href='#lft_tab_1'>E-Mails</a>" + "\r\n" +
                           "</li>" + "\r\n";

            DP_Email_Templates objETemp = ObjectCreator.GetDP_Email_Templates(objSes.Connection, objSes.DB_Type);
            DS_DP_Email_Templates ds2 = objETemp.ReadAll(Document_Project_ID);
            gvEmail.SelectedIndex = -1;
            gvEmail.DataSource = ds2.tbldoc_proj_email_templates;
            gvEmail.DataBind();
            if (ds2.tbldoc_proj_email_templates.Rows.Count > 0)
            {
                gvEmail.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
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
            DP_Email_Templates objETemp = ObjectCreator.GetDP_Email_Templates(objSes.Connection, objSes.DB_Type);
            if (hndEmail_ID.Value == "0")
            {
                if (objETemp.Insert(txtEmailName.Text, txtEmailAddress.Text, txtCCAddresses.Text, txtEmailBody.Text, txtEmailTitle.Text, Convert.ToInt32(hndDP_ID.Value), txtFromName.Text, txtFromAddress.Text) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('E-Mail Template could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('E-Mail Template Details Successfully Added');", true);
                    RefreshGrid(Convert.ToInt32(hndDP_ID.Value), 1, objSes);
                    hndEmail_ID.Value = "0";
                }
            }
            else
            {
                if (objETemp.Update(Convert.ToInt32((hndEmail_ID.Value)), txtEmailName.Text, txtEmailAddress.Text, txtCCAddresses.Text, txtEmailBody.Text, txtEmailTitle.Text, Convert.ToInt32(hndDP_ID.Value), txtFromName.Text, txtFromAddress.Text) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('E-Mail Template could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('E-Mail Template Details Successfully Updated');", true);
                    RefreshGrid(Convert.ToInt32(hndDP_ID.Value), 1, objSes);
                    hndEmail_ID.Value = "0";
                }
            }
        }

        protected void cmdDeleteEmail_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            DP_Email_Templates objETemp = ObjectCreator.GetDP_Email_Templates(objSes.Connection, objSes.DB_Type);
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
                RefreshGrid(Convert.ToInt32(hndDP_ID.Value), 1, objSes);
                hndEmail_ID.Value = "0";
            }
        }
    }
}