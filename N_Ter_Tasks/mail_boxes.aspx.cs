using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter.Base;

namespace N_Ter_Tasks
{
    public partial class mail_boxes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                SessionObject objSes = (SessionObject)Session["dt"];

                hndMailBoxID.Value = "0";
                Load_EL2(objSes);
                Load_Workflows(objSes);
                RefreshGrid(objSes);
            }

            cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndMailBoxID.ClientID + "').value = '0'; ClearControls();");
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidateMailbox('" + txtMailboxName.ClientID + "', '" + txtEmail.ClientID + "', '" + txtPort.ClientID + "', '" + txtSendTo.ClientID + "', '" + cboEl2.ClientID + "', '" + cboWorkflows.ClientID + "', '" + txtNoOfDays.ClientID + "');";
            cboMailBoxType.Attributes.Add("onChange", "CheckExchange();");
            cboEl2.Attributes.Add("onChange", "CheckEL2();");
        }

        private void Load_EL2(SessionObject objSes)
        {
            DS_Entity_Level_2 ds = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type).ReadAll();
            cboEl2.DataSource = ds.tblentity_level_2;
            cboEl2.DataTextField = "Display_Name";
            cboEl2.DataValueField = "Entity_L2_ID";
            cboEl2.DataBind();
            cboEl2.Items.Insert(0, new ListItem("[Not Applicable]", "0"));
        }

        private void Load_Workflows(SessionObject objSes)
        {
            DS_Workflow ds = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).ReadAll();
            cboWorkflows.DataSource = ds.tblwalkflow;
            cboWorkflows.DataTextField = "Workflow_Name";
            cboWorkflows.DataValueField = "Walkflow_ID";
            cboWorkflows.DataBind();
            cboWorkflows.Items.Insert(0, new ListItem("[Not Applicable]", "0"));
        }

        private void RefreshGrid(SessionObject objSes)
        {
            Mail_Boxes objMailbox = ObjectCreator.GetMail_Boxes(objSes.Connection, objSes.DB_Type);
            DS_Mail_Boxes ds = objMailbox.ReadAll();

            foreach (DS_Mail_Boxes.tblmail_boxesRow row in ds.tblmail_boxes)
            {
                switch (row.Mail_Box_Type)
                {
                    case 1:
                        row.Mail_Box_Type_SP = "POP3";
                        break;
                    case 2:
                        row.Mail_Box_Type_SP = "IMAP";
                        break;
                    case 3:
                        row.Mail_Box_Type_SP = "Exchange";
                        break;
                }
            }

            gvMain.SelectedIndex = -1;
            gvMain.DataSource = ds.tblmail_boxes;
            gvMain.DataBind();
            if (ds.tblmail_boxes.Rows.Count > 0)
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
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndMailBoxID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndMailBoxID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
                if (e.Row.Cells[6].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdTest = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[6].Controls[1]);
                    cmdTest.Attributes.Add("onclick", "return TestEmail(" + e.Row.Cells[0].Text + ");");
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Mail_Boxes objMailbox = ObjectCreator.GetMail_Boxes(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objMailbox.Delete(Convert.ToInt32(hndMailBoxID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
                RefreshGrid(objSes);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Mailbox Successfully Deleted');", true);
                RefreshGrid(objSes);
                hndMailBoxID.Value = "0";
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Mail_Boxes objMailbox = ObjectCreator.GetMail_Boxes(objSes.Connection, objSes.DB_Type);
            int PortNumber = 0;
            if (int.TryParse(txtPort.Text, out PortNumber) == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Invalid Port Number');", true);
            }
            else
            {
                if (hndMailBoxID.Value == "0")
                {
                    if (objMailbox.Insert(txtMailboxName.Text, Convert.ToInt32(cboMailBoxType.SelectedItem.Value), txtMailServer.Text, txtEmail.Text, txtPassword.Text, PortNumber, chkSSL.Checked, txtSendTo.Text, Convert.ToInt32(cboEl2.SelectedItem.Value), Convert.ToInt32(cboWorkflows.SelectedItem.Value), Convert.ToInt32(txtNoOfDays.Text)) == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Mailbox could not be saved');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Mailbox Details Successfully Added');", true);
                        hndMailBoxID.Value = "0";
                    }
                }
                else
                {
                    if (objMailbox.Update(Convert.ToInt32(hndMailBoxID.Value), txtMailboxName.Text, Convert.ToInt32(cboMailBoxType.SelectedItem.Value), txtMailServer.Text, txtEmail.Text, txtPassword.Text, PortNumber, chkSSL.Checked, txtSendTo.Text, Convert.ToInt32(cboEl2.SelectedItem.Value), Convert.ToInt32(cboWorkflows.SelectedItem.Value), Convert.ToInt32(txtNoOfDays.Text)) == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Mailbox could not be saved');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Mailbox Details Successfully Updated');", true);
                        hndMailBoxID.Value = "0";
                    }
                }
            }

            RefreshGrid(objSes);
        }

        protected void cmdReceiveEMails_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("receive_emails.aspx?");
        }
    }
}