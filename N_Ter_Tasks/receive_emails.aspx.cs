using N_Ter.Base;
using N_Ter.PeriodicalAutomations;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class receive_emails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                SessionObject objSes = (SessionObject)Session["dt"];
                gvRules.Columns[2].HeaderText = objSes.EL2;

                RefreshGrids(objSes);
            }
        }

        private void RefreshGrids(SessionObject objSes)
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

            Workflow_EMail_Rules objRule = ObjectCreator.GetWorkflow_EMail_Rules(objSes.Connection, objSes.DB_Type);
            DS_WF_EMail_Rules ds2 = objRule.ReadAllInfo();
            gvRules.SelectedIndex = -1;
            gvRules.DataSource = ds2.tblworkflow_email_rules;
            gvRules.DataBind();
            if (ds2.tblworkflow_email_rules.Rows.Count > 0)
            {
                gvRules.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            if (ds.tblmail_boxes.Count == 0)
            {
                ltrStatus.Text = "There are no Mail Boxes to Receive E-mails";
                divSubmit.Visible = false;
            }
            else if (ds2.tblworkflow_email_rules.Rows.Count == 0)
            {
                ltrStatus.Text = "There are no Rules to Create Tasks after Receiving E-mails";
                divSubmit.Visible = false;
            }
            else
            {
                ltrStatus.Text = "All Unseen E-mails in all below Mail Boxes will be downloaded and all rules associalted in each mail box will be executed.";
            }

            divError.Visible = false;
        }

        protected void cmdReceiveHide_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            Automations objAuto = new Automations();
            System.Threading.Thread objThread1 = new System.Threading.Thread(delegate () { objAuto.ReadEMails(objSes); });
            objThread1.Priority = System.Threading.ThreadPriority.BelowNormal;
            objThread1.Start();

            //string AllErrors = objAuto.ReadEMails(objSes);
            //if (AllErrors.Trim() != "")
            //{
            //    divError.Visible = true;
            //    ltrError.Text = AllErrors;
            //}

            RefreshGrids(objSes);

            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Force Receive Email Initiated');", true);
        }
    }
}