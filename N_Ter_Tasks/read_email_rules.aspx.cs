using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter.Base;

namespace N_Ter_Tasks
{
    public partial class read_email_rules : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                SessionObject objSes = (SessionObject)Session["dt"];
                hndRuleID.Value = "0";
                if (Request.QueryString["fid"] == null)
                {
                    Response.Redirect("error.aspx?");
                }
                else
                {
                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    int WFID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["fid"])));
                    ViewState["fid"] = WFID;

                    Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                    DS_Workflow ds = objWF.Read(WFID, false, false);
                    ltrWorkflowName.Text = ds.tblwalkflow[0].Workflow_Name;

                    LoadMailBoxes(objSes);
                    LoadEL2(WFID, objSes);
                    RefreshGrid(objSes);
                }

                ltrEl2_1.Text = objSes.EL2;
            }

            cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndRuleID.ClientID + "').value = '0'; ClearControls();");
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidateRule('" + txtDaysToFinish.ClientID + "');";
        }

        private void LoadMailBoxes(SessionObject objSes)
        {
            Mail_Boxes objMailBoxes = ObjectCreator.GetMail_Boxes(objSes.Connection, objSes.DB_Type);
            DS_Mail_Boxes ds = objMailBoxes.ReadAll();
            cboMailBox.DataSource = ds.tblmail_boxes;
            cboMailBox.DataValueField = "Mail_Box_ID";
            cboMailBox.DataTextField = "Mail_Box_Name";
            cboMailBox.DataBind();
        }

        private void LoadEL2(int Workflow_ID, SessionObject objSes)
        {
            Entity_Level_2 objEL2 = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type);
            DS_Entity_Level_2 ds = objEL2.ReadForWorkflow(Workflow_ID);
            cboEl2.DataSource = ds.tblentity_level_2;
            cboEl2.DataValueField = "Entity_L2_ID";
            cboEl2.DataTextField = "Display_Name";
            cboEl2.DataBind();
        }

        private void RefreshGrid(SessionObject objSes)
        {
            gvMain.Columns[3].HeaderText = objSes.EL2;

            Workflow_EMail_Rules objRule = ObjectCreator.GetWorkflow_EMail_Rules(objSes.Connection, objSes.DB_Type);
            DS_WF_EMail_Rules ds = objRule.ReadAll(Convert.ToInt32(ViewState["fid"]));
            gvMain.SelectedIndex = -1;
            gvMain.DataSource = ds.tblworkflow_email_rules;
            gvMain.DataBind();
            if (ds.tblworkflow_email_rules.Rows.Count > 0)
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
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndRuleID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndRuleID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow_EMail_Rules objRule = ObjectCreator.GetWorkflow_EMail_Rules(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objRule.Delete(Convert.ToInt32(hndRuleID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Email Rule Successfully Deleted');", true);
                RefreshGrid(objSes);
                hndRuleID.Value = "0";
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow_EMail_Rules objRule = ObjectCreator.GetWorkflow_EMail_Rules(objSes.Connection, objSes.DB_Type);
            string Match_With_2 = txtCheck_Word_2.Text;
            string Match_With_3 = txtCheck_Word_3.Text;

            if (cboRule_Type_2.SelectedIndex == 0)
            {
                Match_With_2 = "";
            }
            if (cboRule_Type_3.SelectedIndex == 0)
            {
                Match_With_3 = "";
            }

            int DaysToFinish = 0;
            if (int.TryParse(txtDaysToFinish.Text, out DaysToFinish) == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Invalid No of Days To Finish');", true);
            }
            else
            {
                if (hndRuleID.Value == "0")
                {
                    if (objRule.Insert(Convert.ToInt32(ViewState["fid"]), Convert.ToInt32(cboMailBox.SelectedItem.Value), Convert.ToInt32(cboConditionType.SelectedItem.Value), Convert.ToInt32(cboRule_Type_1.SelectedItem.Value), txtCheck_Word_1.Text, Convert.ToInt32(cboRule_Type_2.SelectedItem.Value), Match_With_2, Convert.ToInt32(cboRule_Type_3.SelectedItem.Value), Match_With_3, Convert.ToInt32(cboEl2.SelectedItem.Value), DaysToFinish) == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Email Rule could not be saved');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Email Rule Details Successfully Added');", true);
                        RefreshGrid(objSes);
                        hndRuleID.Value = "0";
                    }
                }
                else
                {
                    if (objRule.Update(Convert.ToInt32((hndRuleID.Value)), Convert.ToInt32(cboMailBox.SelectedItem.Value), Convert.ToInt32(cboConditionType.SelectedItem.Value), Convert.ToInt32(cboRule_Type_1.SelectedItem.Value), txtCheck_Word_1.Text, Convert.ToInt32(cboRule_Type_2.SelectedItem.Value), Match_With_2, Convert.ToInt32(cboRule_Type_3.SelectedItem.Value), Match_With_3, Convert.ToInt32(cboEl2.SelectedItem.Value), DaysToFinish) == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Email Rule could not be saved');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Email Rule Details Successfully Updated');", true);
                        RefreshGrid(objSes);
                        hndRuleID.Value = "0";
                    }
                }
            }
        }
    }
}