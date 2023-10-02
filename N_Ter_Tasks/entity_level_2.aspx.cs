using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter.Base;
using System.Text.RegularExpressions;

namespace N_Ter_Tasks
{
    public partial class entity_level_2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                SessionObject objSes = (SessionObject)Session["dt"];

                ltrEntityL1_Name.Text = objSes.EL1;
                ltrEntityL2_Code.Text = objSes.EL2 + " Code";
                ltrEntityL2_Name.Text = objSes.EL2 + " Name";

                ltrEL1.Text = objSes.EL1;
                ltrEL2.Text = objSes.EL2;
                ltrEL2_2.Text = objSes.EL2;
                ltrEL2_3.Text = objSes.EL2;
                ltrEL2_4.Text = objSes.EL2P;
                ltrEL2_5.Text = objSes.EL2P;
                ltrEL2_6.Text = objSes.EL2;
                ltrEL2_7.Text = objSes.EL2;
                ltrEL2_8.Text = objSes.EL2P;
                ltrEL2_9.Text = objSes.EL2;
                ltrEL2_10.Text = objSes.EL2P;

                hndEL2_ID.Value = "-1";
                LoadEntity_Level_1s(objSes);
                LoadTemplates(objSes);
                RefreshGrid(objSes);
            }

            txtDisplayName.Attributes.Add("onChange", "changeDN();");
            cboEntity_Level_2.Attributes.Add("onChange", "changeParents();");
            cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndEL2_ID.ClientID + "').value = '-1'; ClearControls();");
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidateEL2('" + txtDisplayName.ClientID + "', '" + txtLegalName.ClientID + "', '" + txtE_Mail.ClientID + "', '" + txtFolder.ClientID + "', '" + txtEntityCode.ClientID + "', '" + cboEntity_Level_1.ClientID + "', '" + hndEL2_ID.ClientID + "', '" + cboEntity_Level_2.ClientID + "', true);";
        }

        private void LoadTemplates(SessionObject objSes)
        {
            string TemplatePath = objSes.PhysicalRoot + "\\nter_app_uploads\\company_letter_head";
            System.IO.DirectoryInfo TemplatesList = new System.IO.DirectoryInfo(TemplatePath);
            cboLetterHead.Items.Clear();
            string FileName = "";
            foreach (System.IO.FileInfo Template in TemplatesList.GetFiles())
            {
                FileName = Template.Name.Substring(0, Template.Name.IndexOf('.'));
                cboLetterHead.Items.Add(new ListItem(FileName, FileName));
            }
        }

        private void LoadEntity_Level_1s(SessionObject objSes)
        {
            Entity_Level_1 objRef = ObjectCreator.GetEntity_Level_1(objSes.Connection, objSes.DB_Type);
            DS_Entity_Level_1 ds = objRef.ReadAll();
            cboEntity_Level_1.DataSource = ds.tblentity_level_1;
            cboEntity_Level_1.DataTextField = ds.tblentity_level_1.Display_NameColumn.ColumnName;
            cboEntity_Level_1.DataValueField = ds.tblentity_level_1.Entity_L1_IDColumn.ColumnName;
            cboEntity_Level_1.DataBind();
            cboEntity_Level_1.Items.Insert(0, new ListItem("[Not Selected]", "0"));
        }

        private void LoadEntity_Level_2s(SessionObject objSes)
        {
            Entity_Level_2 objRef = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type);
            DS_Entity_Level_2 ds = objRef.ReadAll();
            cboEntity_Level_2.DataSource = ds.tblentity_level_2;
            cboEntity_Level_2.DataTextField = ds.tblentity_level_2.Display_NameColumn.ColumnName;
            cboEntity_Level_2.DataValueField = ds.tblentity_level_2.Entity_L2_IDColumn.ColumnName;
            cboEntity_Level_2.DataBind();
            cboEntity_Level_2.Items.Insert(0, new ListItem("[Main " + objSes.EL2 + "]", "0"));
        }

        private void LoadUsers(SessionObject objSes)
        {
            Users objUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
            DS_Users ds = objUsers.ReadAllActiveWithGroups();

            gvUsers.SelectedIndex = -1;
            gvUsers.DataSource = ds.tblusers;
            gvUsers.DataBind();
            if (ds.tblusers.Rows.Count > 0)
            {
                gvUsers.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void LoadWorkflows(SessionObject objSes)
        {
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.ReadAll();

            gvWorkflows.SelectedIndex = -1;
            gvWorkflows.DataSource = ds.tblwalkflow;
            gvWorkflows.DataBind();
            if (ds.tblwalkflow.Rows.Count > 0)
            {
                gvWorkflows.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void RefreshGrid(SessionObject objSes)
        {
            LoadUsers(objSes);
            LoadWorkflows(objSes);
            LoadEntity_Level_2s(objSes);
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Entity_Level_2 objEL2 = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objEL2.Delete(Convert.ToInt32(hndEL2_ID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
                RefreshGrid(objSes);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('" + objSes.EL2 + " Successfully Deleted');", true);
                RefreshGrid(objSes);
                hndEL2_ID.Value = "-1";
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Entity_Level_2 objEL2 = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type);
            if (hndEL2_ID.Value == "-1")
            {
                if (objEL2.Insert(CleanFolderName(txtFolder.Text), txtEntityCode.Text, txtDisplayName.Text, txtLegalName.Text, txtDescription.Text, txtPHStreet.Text, txtPHTown.Text,
                    txtPHState.Text, txtMainContact.Text, txtPhone.Text, txtE_Mail.Text, txtWebSite.Text, Convert.ToInt32(cboEntity_Level_1.SelectedItem.Value),
                    Convert.ToInt32(cboEntity_Level_2.SelectedItem.Value), cboLetterHead.SelectedItem.Text, GetAvailableUsers(), GetAvailableWorkflows()) == -1)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + objSes.EL2 + " could not be saved');", true);
                    RefreshGrid(objSes);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('" + objSes.EL2 + " Details Successfully Added');", true);
                    RefreshGrid(objSes);
                    hndEL2_ID.Value = "-1";
                }
            }
            else
            {
                if (objEL2.Update(Convert.ToInt32(hndEL2_ID.Value), CleanFolderName(txtFolder.Text), txtEntityCode.Text, txtDisplayName.Text, txtLegalName.Text,
                    txtDescription.Text, txtPHStreet.Text, txtPHTown.Text, txtPHState.Text, txtMainContact.Text, txtPhone.Text, txtE_Mail.Text,
                    txtWebSite.Text, Convert.ToInt32(cboEntity_Level_1.SelectedItem.Value), Convert.ToInt32(cboEntity_Level_2.SelectedItem.Value),
                    cboLetterHead.SelectedItem.Value, GetAvailableUsers(), GetAvailableWorkflows()) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + objSes.EL2 + " could not be saved');", true);
                    RefreshGrid(objSes);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('" + objSes.EL2 + " Details Successfully Updated');", true);
                    RefreshGrid(objSes);
                    hndEL2_ID.Value = "-1";
                }
            }
        }

        private string CleanFolderName(string FolderName)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            FolderName = rgx.Replace(FolderName, "_");
            return FolderName;
        }

        private DS_Entity_Level_2.tbluser_entity_l2DataTable GetAvailableUsers()
        {
            DS_Entity_Level_2 ds = new DS_Entity_Level_2();
            DS_Entity_Level_2.tbluser_entity_l2Row dr;
            foreach (string user in hndUsers.Value.Split('|'))
            {
                if (user.Trim() != "")
                {
                    dr = ds.tbluser_entity_l2.Newtbluser_entity_l2Row();
                    dr.Entity_L2_ID = 0;
                    dr.User_ID = Convert.ToInt32(user);
                    ds.tbluser_entity_l2.Rows.Add(dr);
                }
            }
            return ds.tbluser_entity_l2;
        }

        private DS_Entity_Level_2.tblwalkflow_entity_l2DataTable GetAvailableWorkflows()
        {
            DS_Entity_Level_2 ds = new DS_Entity_Level_2();
            DS_Entity_Level_2.tblwalkflow_entity_l2Row dr;

            int No_Of_Units;

            foreach (GridViewRow row in gvWorkflows.Rows)
            {
                if (row.Cells[1].HasControls())
                {
                    CheckBox chkSelect = (CheckBox)row.Cells[1].Controls[1];
                    foreach (string wf in hndWorkflows.Value.Split('|'))
                    {
                        if (chkSelect.Attributes["data-id"].Trim() == wf.Trim())
                        {
                            No_Of_Units = 0;
                            TextBox txtNOU = (TextBox)row.Cells[3].Controls[1];
                            int.TryParse(txtNOU.Text, out No_Of_Units);

                            dr = ds.tblwalkflow_entity_l2.Newtblwalkflow_entity_l2Row();
                            dr.Entity_L2_ID = 0;
                            dr.Walkflow_ID = Convert.ToInt32(wf);
                            dr.No_Of_Units = No_Of_Units;
                            ds.tblwalkflow_entity_l2.Rows.Add(dr);
                        }
                    }
                }
            }
            return ds.tblwalkflow_entity_l2;
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].HasControls())
                {
                    System.Web.UI.WebControls.CheckBox chkSelect = ((System.Web.UI.WebControls.CheckBox)e.Row.Cells[1].Controls[1]);
                    chkSelect.Attributes.Add("data-id", e.Row.Cells[0].Text);
                    chkSelect.Attributes.Add("data-hnd", "hndUsers");
                }
            }
        }

        protected void gvWorkflows_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].HasControls())
                {
                    System.Web.UI.WebControls.CheckBox chkSelect = ((System.Web.UI.WebControls.CheckBox)e.Row.Cells[1].Controls[1]);
                    chkSelect.Attributes.Add("data-id", e.Row.Cells[0].Text);
                    chkSelect.Attributes.Add("data-hnd", "hndWorkflows");
                }
            }
        }

        protected void cmdShowInactive_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("entity_level_2_inactive.aspx?");
        }

        protected void cmdDeactivateEL2_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Entity_Level_2 objEL2 = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type);
            ActionDone Deactivate = objEL2.Deactivate(Convert.ToInt32(hndEL2_ID.Value));
            if (Deactivate.Done == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + Deactivate.Reason + "');", true);
                RefreshGrid(objSes);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('" + objSes.EL2 + " Successfully Deactivated');", true);
                RefreshGrid(objSes);
                hndEL2_ID.Value = "-1";
            }
        }
    }
}