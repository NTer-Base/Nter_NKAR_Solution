using System;
using System.Web.UI;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter.Base;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class settings : System.Web.UI.Page
    {
        public string Base_URL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                SessionObject objSes = (SessionObject)Session["dt"];
                LoadUserGroups(objSes);
                LoadSettings(objSes);
                LoadAltStartPages();
                LoadPages(objSes);
                ShowGustTaskCreationURL(objSes);
                ltrEL2.Text = objSes.EL2;
            }
            cmdTestEmail.Attributes.Add("onClick", "return ValidateEmail('" + txtTestEmail.ClientID + "')");
            chkAllowDualUsers.Attributes.Add("onclick", "CheckUserDual();");
            chkAllowDualUG.Attributes.Add("onclick", "CheckUGDual();");
        }

        private void ShowGustTaskCreationURL(SessionObject objSes)
        {
            DS_Settings dsSett = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type).Read();
            if (dsSett.tblsettings[0].User_Group_Guest > 0)
            {
                DS_Users dsUsr = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type).ReadUsers(dsSett.tblsettings[0].User_Group_Guest);
                if (dsUsr.tblusers.Rows.Count > 0)
                {
                    DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).ReadAllParentOnly(dsUsr.tblusers[0].User_ID);
                    if (dsWF.tblwalkflow.Rows.Count > 0)
                    {
                        N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                        Base_URL = Request.Url.GetLeftPart(UriPartial.Authority) + (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath);
                        string Main_URL = Base_URL + "/guest_task_creation.aspx?w=";
                        string HelpText = "Following Workflows are Configured for Guest Task Creation<br /><br />\r\n<table class='table table-striped table-hover grid_table last_col_right'>";
                        foreach (DS_Workflow.tblwalkflowRow row in dsWF.tblwalkflow)
                        {
                            HelpText += "<tr>\r\n" +
                                            "<td>" + row.Workflow_Name + "</td>\r\n" +
                                            "<td>" + Main_URL + objURL.Encrypt(row.Walkflow_ID.ToString()) + "</td>\r\n" +
                                            "<td><button type='submit' class='btn btn-primary btn-xs' title='" + objSes.EL2 + " Specific URLs' onclick='return OpenEL2_URLs(" + row.Walkflow_ID + ");'><i class='fa fa-link button_icon'></i></button></td>\r\n" +
                                        "</tr>\r\n";
                        }
                        HelpText += "</table>";
                        divHelpContent.InnerHtml = HelpText;
                    }
                    else
                    {
                        divHelpContent.InnerHtml = "There are no Workflows available to Shere";
                    }
                }
                else
                {
                    divHelpContent.InnerHtml = "There are no Guest Users Available";
                }
            }
        }

        private void LoadUserGroups(SessionObject objSes)
        {
            User_Groups objUserGroups = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type);
            DS_Users ds = objUserGroups.ReadAll();
            cboSuperAdmin.DataSource = ds.tbluser_groups;
            cboSuperAdmin.DataValueField = "User_Group_ID";
            cboSuperAdmin.DataTextField = "User_Group_Name";
            cboSuperAdmin.DataBind();

            cboSupervisorAdmin.DataSource = ds.tbluser_groups;
            cboSupervisorAdmin.DataValueField = "User_Group_ID";
            cboSupervisorAdmin.DataTextField = "User_Group_Name";
            cboSupervisorAdmin.DataBind();
            cboSupervisorAdmin.Items.Add(new ListItem("[N/A]", "-1"));

            cboGuest.DataSource = ds.tbluser_groups;
            cboGuest.DataValueField = "User_Group_ID";
            cboGuest.DataTextField = "User_Group_Name";
            cboGuest.DataBind();
            cboGuest.Items.Add(new ListItem("[N/A]", "-1"));

            cboUGApprove.DataSource = ds.tbluser_groups;
            cboUGApprove.DataValueField = "User_Group_ID";
            cboUGApprove.DataTextField = "User_Group_Name";
            cboUGApprove.DataBind();
            cboUGApprove.Items.Insert(0, new ListItem("[N/A]", "0"));

            cboUGDataEntry.DataSource = ds.tbluser_groups;
            cboUGDataEntry.DataValueField = "User_Group_ID";
            cboUGDataEntry.DataTextField = "User_Group_Name";
            cboUGDataEntry.DataBind();
            cboUGDataEntry.Items.Insert(0, new ListItem("[N/A]", "0"));

            cboUserDataEntry.DataSource = ds.tbluser_groups;
            cboUserDataEntry.DataValueField = "User_Group_ID";
            cboUserDataEntry.DataTextField = "User_Group_Name";
            cboUserDataEntry.DataBind();
            cboUserDataEntry.Items.Insert(0, new ListItem("[N/A]", "0"));

            cboUserApprove.DataSource = ds.tbluser_groups;
            cboUserApprove.DataValueField = "User_Group_ID";
            cboUserApprove.DataTextField = "User_Group_Name";
            cboUserApprove.DataBind();
            cboUserApprove.Items.Insert(0, new ListItem("[N/A]", "0"));
        }

        private void LoadAltStartPages()
        {
            DS_Extra_Sections dsEtr = new N_Ter.Customizable.Custom_Lists().LoadExtraSections();
            DS_Extra_Sections.tblSectionsRow dr = dsEtr.tblSections.NewtblSectionsRow();
            dr.Section_ID = 0;
            dr.Section_Name = "[None]";
            dsEtr.tblSections.Rows.InsertAt(dr, 0);
            cboAltStart.DataSource = dsEtr.tblSections.Where(x => x.IsisTask_SpecificNull() || x.isTask_Specific == false);
            cboAltStart.DataValueField = "Section_ID";
            cboAltStart.DataTextField = "Section_Name";
            cboAltStart.DataBind();
        }

        private void LoadPages(SessionObject objSes)
        {
            Settings objSettings = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type);
            DS_Pages ds = objSettings.ReadPages();
            foreach (DS_Pages.tblpagesRow row in ds.tblpages)
            {
                switch (row.Page_Index)
                {
                    case 1:
                        row.Page_Desc_Menu = "Dashboard : " + row.Page_Desc_Menu;
                        break;
                    case 2:
                        row.Page_Desc_Menu = "Tasks : " + row.Page_Desc_Menu;
                        break;
                    case 3:
                        row.Page_Desc_Menu = "Document : " + row.Page_Desc_Menu;
                        break;
                    case 4:
                        row.Page_Desc_Menu = "Calendars : " + row.Page_Desc_Menu;
                        break;
                    case 5:
                        row.Page_Desc_Menu = "Reports : " + row.Page_Desc_Menu;
                        break;
                    case 6:
                        row.Page_Desc_Menu = "Manage : " + row.Page_Desc_Menu;
                        break;
                    case 7:
                        row.Page_Desc_Menu = "Admin : " + row.Page_Desc_Menu;
                        break;
                }
            }
            gvPages.DataSource = ds.tblpages.Where(x => x.Page_Index > 0 || x.Sort_Order == 1).OrderBy(y => y.Page_Index).ThenBy(z => z.Sort_Order);
            gvPages.DataBind();
        }

        private void LoadSettings(SessionObject objSes)
        {
            Settings objSettings = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type);
            DS_Settings ds = objSettings.Read();
            DS_Settings.tblsettingsRow dr = ds.tblsettings[0];
            txtCompanyName.Text = dr.Company_Name;
            txtAddressLine1.Text = dr.Address_Line_1;
            txtAddressLine2.Text = dr.Address_Line_2;
            txtCity.Text = dr.City;
            txtState.Text = dr.State;
            txtPhone.Text = dr.Phone;
            txtFax.Text = dr.Fax;
            txtEmail.Text = dr.Email;
            txtNotes.Text = dr.Notes;
            txtCompanyWeb.Text = dr.Company_Web;
            txtFacebook.Text = dr.Facebook_Link;
            txtTwitter.Text = dr.Twitter_Link;
            txtLinkedin.Text = dr.Linkedin_Link;
            txtEL1.Text = dr.Entity_Level_1_Name;
            txtEL1P.Text = dr.Entity_Level_1_Name_Plural;
            txtEL2.Text = dr.Entity_Level_2_Name;
            txtEL2P.Text = dr.Entity_Level_2_Name_Plural;
            txtCurrency.Text = dr.Currency_Symbol;
            cboSuperAdmin.SelectedValue = Convert.ToString(dr.User_Group_Super_Admin);
            cboSupervisorAdmin.SelectedValue = Convert.ToString(dr.User_Group_Supervisor);
            cboGuest.SelectedValue = Convert.ToString(dr.User_Group_Guest);
            txtEmailHost.Text = dr.Send_Email_Host;
            txtSenderName.Text = dr.Default_Email_Sender;
            txtUsername.Text = dr.Send_Email_Username;
            txtPassword.Text = "";
            txtPortNumber.Text = dr.Send_Email_Port;
            chkSSL.Checked = dr.Send_Email_isSSL;
            chkExchange.Checked = dr.Send_Email_isExchange;
            txtHeader.Text = dr.Guest_Post_Header;
            txtFooter.Text = dr.Guest_Post_Footer;
            chkEnableAlerts.Checked = dr.Enable_Alerts;
            chkShowHelp.Checked = dr.Show_Help;
            chkHourlyMsg.Checked = dr.Enable_Hourly_Messages;
            chkEnableReading.Checked = dr.Enable_Reading_Documents;
            chkDataExtractBGProcess.Checked = dr.Data_Extract_As_BG_Process;
            chkMessageAlerts.Checked = dr.Enable_Message_Alerts;
            cboAltStart.SelectedValue = Convert.ToString(dr.Alternative_Start_Page);
            txtVersion.Text = dr.App_Version.ToString();
            cboTaskList.SelectedValue = Convert.ToString(dr.Default_Task_List);
            cboTaskScreen.SelectedValue = Convert.ToString(dr.Task_Screen_Type);
            cboRefreshFreq.SelectedValue = Convert.ToString(dr.Screen_Refresh_Freq);
            cboAlertDurtion.SelectedValue = Convert.ToString(dr.Alert_Duration);
            cboWebServiceFreq.SelectedValue = Convert.ToString(dr.Web_Service_Freq);
            cboDefaultSort.SelectedValue = Convert.ToString(dr.Task_List_Sort_Field);
            cboTaskListView.SelectedValue = (dr.Task_List_View ? "0" : "1");
            cboTaskTLListView.SelectedValue = (dr.TL_Task_List_View ? "0" : "1"); ;
            cboSortDirection.SelectedValue = (dr.Task_List_Sort_Dir ? "1" : "2");
            cboHistorySort.SelectedValue = (dr.Task_History_Sort_Dir ? "1" : "2");
            cboDocsSort.SelectedValue = (dr.Task_Docs_Sort_Dir ? "1" : "2");
            cboUnit.SelectedValue = Convert.ToString(dr.Unit_Duration);
            cboPWExpiry.SelectedValue = Convert.ToString(dr.PW_Expires_After);
            cboPWMinAlpha.SelectedValue = Convert.ToString(dr.PW_Min_Alpha);
            cboPWMinChar.SelectedValue = Convert.ToString(dr.PW_Min_Char);
            cboPWMinNum.SelectedValue = Convert.ToString(dr.PW_Min_Numbers);
            cboPWMinSpecial.SelectedValue = Convert.ToString(dr.PW_Min_Special);
            chkFirstLogin.Checked = dr.PW_Must_Change_Init_Login;
            cboInactiveTime.SelectedValue = Convert.ToString(dr.Max_Inactive_Time);
            cboFailedLogins.SelectedValue = Convert.ToString(dr.Max_Failed_Logins_Allowed);
            cboUsernameOption.SelectedValue = Convert.ToString(dr.Username_Option);
            chkAllowDualUG.Checked = dr.Dual_Validation_User_Groups;
            chkAllowDualUsers.Checked = dr.Dual_Validation_Users;
            CheckDualValidations();
            cboUGApprove.SelectedValue = Convert.ToString(dr.User_Group_Approve);
            cboUGDataEntry.SelectedValue = Convert.ToString(dr.User_Group_Data_Entry);
            cboUserApprove.SelectedValue = Convert.ToString(dr.Users_Approve);
            cboUserDataEntry.SelectedValue = Convert.ToString(dr.Users_Data_Entry);
        }

        private void CheckDualValidations()
        {
            if (chkAllowDualUG.Checked)
            {
                divUGDual.Attributes["class"] = "form-group";
            }
            else
            {
                divUGDual.Attributes["class"] = "form-group hide";
            }

            if (chkAllowDualUsers.Checked)
            {
                divUserDual.Attributes["class"] = "form-group";
            }
            else
            {
                divUserDual.Attributes["class"] = "form-group hide";
            }
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            LoadSettings(objSes);
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            if (txtCompanyName.Text.Trim() == "")
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Company Name is Required');", true);
            }
            else
            {
                SessionObject objSes = (SessionObject)Session["dt"];
                Settings objSettings = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type);

                bool ListSortDirection = true;
                if (cboSortDirection.SelectedItem.Value == "2")
                {
                    ListSortDirection = false;
                }
                bool HistorySortDirection = true;
                if (cboHistorySort.SelectedItem.Value == "2")
                {
                    HistorySortDirection = false;
                }
                bool DocSortDirection = true;
                if (cboDocsSort.SelectedItem.Value == "2")
                {
                    DocSortDirection = false;
                }

                bool Task_List_View = true;
                if (cboTaskListView.SelectedItem.Value == "1")
                {
                    Task_List_View = false;
                }

                bool TL_Task_List_View = true;
                if (cboTaskTLListView.SelectedItem.Value == "1")
                {
                    TL_Task_List_View = false;
                }

                if (objSettings.Update(1, txtCompanyName.Text, txtAddressLine1.Text, txtAddressLine2.Text, txtCity.Text, txtState.Text,
                    txtPhone.Text, txtFax.Text, txtEmail.Text, txtEL1.Text, txtEL1P.Text, txtEL2.Text, txtEL2P.Text, txtCurrency.Text,
                    Convert.ToInt32(cboSuperAdmin.SelectedItem.Value), Convert.ToInt32(cboSupervisorAdmin.SelectedItem.Value),
                    Convert.ToInt32(cboGuest.SelectedItem.Value), txtEmailHost.Text, txtUsername.Text, txtPassword.Text, txtPortNumber.Text,
                    chkSSL.Checked, chkExchange.Checked, Convert.ToInt32(cboTaskList.SelectedItem.Value), txtHeader.Text, txtFooter.Text,
                    chkEnableAlerts.Checked, Convert.ToInt32(cboRefreshFreq.SelectedItem.Value), Convert.ToInt32(cboDefaultSort.SelectedItem.Value),
                    ListSortDirection, HistorySortDirection, DocSortDirection, chkHourlyMsg.Checked, txtCompanyWeb.Text, txtFacebook.Text,
                    txtTwitter.Text, txtLinkedin.Text, chkEnableReading.Checked, Convert.ToInt32(cboAltStart.SelectedValue),
                    Convert.ToInt32(cboWebServiceFreq.SelectedValue), Task_List_View, chkShowHelp.Checked, Convert.ToInt32(cboUnit.SelectedItem.Value),
                    txtNotes.Text, Convert.ToInt32(cboPWMinChar.SelectedValue), Convert.ToInt32(cboPWMinAlpha.SelectedValue),
                    Convert.ToInt32(cboPWMinNum.SelectedValue), Convert.ToInt32(cboPWMinSpecial.SelectedValue), Convert.ToInt32(cboPWExpiry.SelectedValue),
                    chkFirstLogin.Checked, Convert.ToInt32(cboFailedLogins.SelectedValue), Convert.ToInt32(cboInactiveTime.SelectedValue),
                    Convert.ToInt32(cboUsernameOption.SelectedValue), chkAllowDualUG.Checked, Convert.ToInt32(cboUGDataEntry.SelectedValue),
                    Convert.ToInt32(cboUGApprove.SelectedValue), chkAllowDualUsers.Checked, Convert.ToInt32(cboUserDataEntry.SelectedValue),
                    Convert.ToInt32(cboUserApprove.SelectedValue), chkDataExtractBGProcess.Checked, Convert.ToInt32(cboAlertDurtion.SelectedItem.Value),
                    TL_Task_List_View, chkMessageAlerts.Checked, Convert.ToInt32(cboTaskScreen.SelectedValue), txtSenderName.Text) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Settings could not be Saved');", true);
                }
                else
                {
                    if (objSettings.UpdateAlternativePages(GetAltPages()) == false)
                    {
                        LoadPages(objSes);
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Alternative Pages could not be Saved');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Settings Successfully Saved');", true);
                        CheckDualValidations();
                        AdjustSession(objSes);
                        n_ter_base_loggedin_grid pg = (n_ter_base_loggedin_grid)Master;
                        pg.BuildAlerts(objSes);
                    }
                }
            }
        }

        private DS_Pages GetAltPages()
        {
            DS_Pages ds = new DS_Pages();
            DS_Pages.tblpagesRow dr;
            foreach (GridViewRow row in gvPages.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    if (row.Cells[2].HasControls())
                    {
                        DropDownList cboPages = (DropDownList)row.Cells[2].Controls[1];
                        dr = ds.tblpages.NewtblpagesRow();
                        dr.Page_ID = Convert.ToInt32(row.Cells[0].Text);
                        dr.Alternative_Page = Convert.ToInt32(cboPages.SelectedItem.Value);
                        ds.tblpages.Rows.Add(dr);
                    }
                }
            }
            return ds;
        }

        private void AdjustSession(SessionObject objSes)
        {
            Settings objSettings = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type);
            DS_Settings dsSettings = objSettings.Read();

            objSes.Enable_Alerts = dsSettings.tblsettings[0].Enable_Alerts;
            objSes.EL1 = dsSettings.tblsettings[0].Entity_Level_1_Name;
            objSes.EL1P = dsSettings.tblsettings[0].Entity_Level_1_Name_Plural;
            objSes.EL2 = dsSettings.tblsettings[0].Entity_Level_2_Name;
            objSes.EL2P = dsSettings.tblsettings[0].Entity_Level_2_Name_Plural;
            objSes.Currency_Sbl = dsSettings.tblsettings[0].Currency_Symbol;
            objSes.Guest_Group = dsSettings.tblsettings[0].User_Group_Guest;
            objSes.ListSort = dsSettings.tblsettings[0].Task_List_Sort_Field;
            objSes.ListSortDir = dsSettings.tblsettings[0].Task_List_Sort_Dir;
            objSes.TaskHtryDir = dsSettings.tblsettings[0].Task_History_Sort_Dir;
            objSes.TaskDocDir = dsSettings.tblsettings[0].Task_Docs_Sort_Dir;
            objSes.EnableHrMsg = dsSettings.tblsettings[0].Enable_Hourly_Messages;
            objSes.EnableReading = dsSettings.tblsettings[0].Enable_Reading_Documents;
            objSes.Task_List = dsSettings.tblsettings[0].Default_Task_List;
            objSes.Task_List_View = dsSettings.tblsettings[0].Task_List_View;
            objSes.TL_Task_List_View = dsSettings.tblsettings[0].TL_Task_List_View;
            objSes.Show_Help = dsSettings.tblsettings[0].Show_Help;
            objSes.GrowlDur = dsSettings.tblsettings[0].Alert_Duration;
            objSes.Task_Screen_Type = dsSettings.tblsettings[0].Task_Screen_Type;

            DS_Users dsUsr = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).Read(objSes.UserID);
            N_Ter.Common.Common_Actions objComAct = new N_Ter.Common.Common_Actions();
            if (dsUsr.tblusers.Count > 0)
            {
                objComAct.AdjustUserSessionParams(dsUsr.tblusers[0].Default_Task_List, dsUsr.tblusers[0].Task_List_View, dsUsr.tblusers[0].TL_Task_List_View, dsUsr.tblusers[0].Show_Help, dsUsr.tblusers[0].Task_Screen_Type, ref objSes);
            }

            n_ter_base_loggedin_grid pg = (n_ter_base_loggedin_grid)this.Master;
            pg.ShowHelp = objSes.Show_Help;

            DS_Extra_Sections dsEtr = new N_Ter.Customizable.Custom_Lists().LoadExtraSections();
            List<DS_Extra_Sections.tblSectionsRow> drEtr = dsEtr.tblSections.Where(x => x.Section_ID == dsSettings.tblsettings[0].Alternative_Start_Page).ToList();
            if (drEtr.Count > 0)
            {
                objSes.AltStart = drEtr[0].Page_Name;
            }
            else
            {
                objSes.AltStart = "";
            }
            objSes.RefFreq = objComAct.GetRefreshFrequencySeconds(dsSettings.tblsettings[0].Screen_Refresh_Freq);
            objSes.UnitMins = objComAct.GetUnitMinutes(dsSettings.tblsettings[0].Unit_Duration);

            Session["dt"] = objSes;
        }

        protected void cmdTestEmail_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Settings objSettings = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type);
            DS_Settings dsSettings = objSettings.Read();

            N_Ter.Emails.Email_Sender objEMail = new N_Ter.Emails.Email_Sender();

            string PhysicalRootFolder = objSes.PhysicalRoot + "\\nter_app_uploads\\temp_email_attachments";

            ActionDone Act = objEMail.SendTestEmail(dsSettings.tblsettings[0], txtTestEmail.Text, ref PhysicalRootFolder);
            if (Act.Done == true)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Email Successfully Sent');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + Act.Reason + "');", true);

                Response.AddHeader("Content-Disposition", "attachment; filename=Email_Error.txt");
                Response.ContentType = "application/octet-stream";
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();

                Response.WriteFile(PhysicalRootFolder);
                Response.End();
            }
        }

        protected void gvPages_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[3].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[2].HasControls())
                {
                    DropDownList cboPages = (DropDownList)e.Row.Cells[2].Controls[1];
                    DS_Extra_Sections dsEtr = new N_Ter.Customizable.Custom_Lists().LoadExtraSections();
                    DS_Extra_Sections.tblSectionsRow dr = dsEtr.tblSections.NewtblSectionsRow();
                    dr.Section_ID = 0;
                    dr.Section_Name = "[None]";
                    dsEtr.tblSections.Rows.InsertAt(dr, 0);
                    cboPages.DataSource = dsEtr.tblSections.Where(x => x.IsisTask_SpecificNull() || x.isTask_Specific == false);
                    cboPages.DataValueField = "Section_ID";
                    cboPages.DataTextField = "Section_Name";
                    cboPages.DataBind();

                    cboPages.SelectedValue = e.Row.Cells[3].Text;
                }
            }
        }
    }
}