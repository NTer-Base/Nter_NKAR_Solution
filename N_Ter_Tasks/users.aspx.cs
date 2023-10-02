using N_Ter.Base;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                SessionObject objSes = (SessionObject)Session["dt"];
                DualAuthTypes AuthType = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type).GetAuthenticatoinType(objSes.UserID, DualAuthArea.Users);
                if (AuthType != DualAuthTypes.Approve)
                {
                    cmdDiscard.Visible = false;
                }
                else
                {
                    cmdNew.Visible = false;
                }
                ViewState["au"] = (int)AuthType;
                ltrEL2s.Text = objSes.EL2P;
                gvEntity_L2.Columns[2].HeaderText = objSes.EL2 + " Name";
                LoadSupervisors(objSes);
                LoadTempUsers(objSes);
                LoadPasswordPolicy(objSes);
                hndUserID.Value = "0";
                RefreshGrid(objSes);
            }

            cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndUserID.ClientID + "').value = '0'; ClearControls(); $find('mpuRec').show(); return false;");
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidateUser('" + txtUsername.ClientID + "', '" + txtPassword.ClientID + "', '" + txtRePassword.ClientID + "', '" + txtFirstName.ClientID + "', '" + txtUserCode.ClientID + "', '" + hndUserID.ClientID + "');";
        }

        private void LoadPasswordPolicy(SessionObject objSes)
        {
            N_Ter.Common.Common_Task_Actions objCommAct = new N_Ter.Common.Common_Task_Actions();
            divPasswordPolicy.InnerHtml = objCommAct.ReadPasswordPolicy(ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type));
        }

        private void LoadUserGroups(SessionObject objSes)
        {
            User_Groups objUserG = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type);
            DS_Users ds = objUserG.ReadAll();

            gvUserGroups.SelectedIndex = -1;
            gvUserGroups.DataSource = ds.tbluser_groups;
            gvUserGroups.DataBind();
            if (ds.tbluser_groups.Rows.Count > 0)
            {
                gvUserGroups.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void LoadSupervisors(SessionObject objSes)
        {
            Users objUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
            DS_Users ds = objUsers.ReadAll();
            DS_Users.tblusersRow dr = ds.tblusers.NewtblusersRow();
            dr.User_ID = 0;
            dr.Username = "TopUser";
            dr.Full_Name = "[Not Applicable]";
            ds.tblusers.Rows.InsertAt(dr, 0);
            cboSupervisor.DataSource = ds.tblusers;
            cboSupervisor.DataTextField = "Full_Name";
            cboSupervisor.DataValueField = "User_ID";
            cboSupervisor.DataBind();
        }

        private void LoadTempUsers(SessionObject objSes)
        {
            Users objUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
            DS_Users ds = objUsers.ReadAll();
            cboTempUser.DataSource = ds.tblusers;
            cboTempUser.DataTextField = "Full_Name";
            cboTempUser.DataValueField = "User_ID";
            cboTempUser.DataBind();
            cboTempUser.Items.Insert(0, new ListItem("[Not Assigned]", "0"));
        }

        private void LoadEL2s(SessionObject objSes)
        {
            Entity_Level_2 objEL2 = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type);
            DS_Entity_Level_2 ds = objEL2.ReadAll();
            foreach (DS_Entity_Level_2.tblentity_level_2Row row in ds.tblentity_level_2)
            {
                if (row.Entity_L1_Name != "-")
                {
                    row.Display_Name = row.Entity_L1_Name + " - " + row.Display_Name;
                }
            }

            gvEntity_L2.SelectedIndex = -1;
            gvEntity_L2.DataSource = ds.tblentity_level_2;
            gvEntity_L2.DataBind();
            if (ds.tblentity_level_2.Rows.Count > 0)
            {
                gvEntity_L2.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void RefreshGrid(SessionObject objSes)
        {
            LoadUserGroups(objSes);
            LoadEL2s(objSes);
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Users objUser = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
            bool isDeleted = false;
            if (chkActive.Checked == false)
            {
                isDeleted = true;
            }
            bool Result = false;
            DualAuthTypes AuthType = (DualAuthTypes)ViewState["au"];
            if (hndUserID.Value == "0")
            {
                switch (AuthType)
                {
                    case DualAuthTypes.Regular:
                        Result = objUser.Create(txtUsername.Text, txtPassword.Text, txtUserCode.Text, txtFirstName.Text, txtLastName.Text, txtPhone.Text, Convert.ToInt32(cboSupervisor.SelectedItem.Value), txtDesignation.Text, isDeleted, Convert.ToInt32(cboTempUser.SelectedItem.Value), GetAvailableUserGroups(objSes), GetAvailableUserEL2s(objSes), chkOverrideRestriction.Checked, txtSpecialComments.Text);
                        break;
                    case DualAuthTypes.DataEntry:
                        Result = objUser.CreateTransit(0, txtUsername.Text, txtPassword.Text, txtUserCode.Text, txtFirstName.Text, txtLastName.Text, txtPhone.Text, Convert.ToInt32(cboSupervisor.SelectedItem.Value), txtDesignation.Text, isDeleted, Convert.ToInt32(cboTempUser.SelectedItem.Value), GetAvailableUserGroups(objSes), GetAvailableUserEL2s(objSes), chkOverrideRestriction.Checked, txtSpecialComments.Text);
                        break;
                }
                if (Result == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('User could not be Added');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('User Details Successfully Added');", true);
                    RefreshGrid(objSes);
                    LoadSupervisors(objSes);
                    hndUserID.Value = "0";
                }
            }
            else
            {
                switch (AuthType)
                {
                    case DualAuthTypes.Regular:
                        if (txtPassword.Text.Trim() == "")
                        {
                            Result = objUser.Update(Convert.ToInt32(hndUserID.Value), txtUsername.Text, txtUserCode.Text, txtFirstName.Text, txtLastName.Text, txtPhone.Text, Convert.ToInt32(cboSupervisor.SelectedItem.Value), txtDesignation.Text, isDeleted, Convert.ToInt32(cboTempUser.SelectedItem.Value), GetAvailableUserGroups(objSes), GetAvailableUserEL2s(objSes), chkOverrideRestriction.Checked, txtSpecialComments.Text);
                        }
                        else
                        {
                            Result = objUser.Update(Convert.ToInt32(hndUserID.Value), txtUsername.Text, txtPassword.Text, txtUserCode.Text, txtFirstName.Text, txtLastName.Text, txtPhone.Text, Convert.ToInt32(cboSupervisor.SelectedItem.Value), txtDesignation.Text, isDeleted, Convert.ToInt32(cboTempUser.SelectedItem.Value), GetAvailableUserGroups(objSes), GetAvailableUserEL2s(objSes), chkOverrideRestriction.Checked, txtSpecialComments.Text);
                        }
                        if (Result == true)
                        {
                            objUser.UpdateLoginUserTime(Convert.ToInt32(hndUserID.Value));
                            objUser.ResetLoginFailedAttempts(Convert.ToInt32(hndUserID.Value));
                        }
                        break;
                    case DualAuthTypes.DataEntry:
                        Result = objUser.CreateTransit(Convert.ToInt32(hndUserID.Value), txtUsername.Text, txtPassword.Text, txtUserCode.Text, txtFirstName.Text, txtLastName.Text, txtPhone.Text, Convert.ToInt32(cboSupervisor.SelectedItem.Value), txtDesignation.Text, isDeleted, Convert.ToInt32(cboTempUser.SelectedItem.Value), GetAvailableUserGroups(objSes), GetAvailableUserEL2s(objSes), chkOverrideRestriction.Checked, txtSpecialComments.Text);
                        break;
                    case DualAuthTypes.Approve:
                        Result = objUser.TransferFromTransit(Convert.ToInt32(hndUserID.Value));
                        break;
                }

                if (Result == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('User could not be Updated');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('User Details Successfully Updated');", true);
                    LoadSupervisors(objSes);
                    RefreshGrid(objSes);
                    hndUserID.Value = "0";
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            DualAuthTypes AuthType = (DualAuthTypes)ViewState["au"];
            if (AuthType == DualAuthTypes.Regular)
            {
                Users objUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
                DeleteReason del = objUsers.Delete(Convert.ToInt32(hndUserID.Value));
                if (del.Deleted == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('User Group Successfully Deleted');", true);

                    LoadSupervisors(objSes);
                    hndUserID.Value = "0";
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('You are not allowed to Delete Users');", true);
            }
            RefreshGrid(objSes);
        }

        protected void cmdDiscard_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            DualAuthTypes AuthType = (DualAuthTypes)ViewState["au"];
            if (AuthType == DualAuthTypes.Approve)
            {
                Users objUsr = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
                DeleteReason del = objUsr.DiscardTransit(Convert.ToInt32(hndUserID.Value));
                if (del.Deleted == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('User Update Successfully Deleted');", true);
                    hndUserID.Value = "0";
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('You are not allowed to Delete User Amendments');", true);
            }
            RefreshGrid(objSes);
        }

        private DS_Users.tbluser_group_usersDataTable GetAvailableUserGroups(SessionObject objSes)
        {
            DS_Users ds = new DS_Users();
            DS_Users.tbluser_group_usersRow dr;
            foreach (string userGrp in hndGroups.Value.Split('|'))
            {
                if (userGrp.Trim() != "")
                {
                    dr = ds.tbluser_group_users.Newtbluser_group_usersRow();
                    dr.User_ID = 0;
                    dr.User_Group_ID = Convert.ToInt32(userGrp);
                    ds.tbluser_group_users.Rows.Add(dr);
                }
            }
            LoadUserGroups(objSes);
            return ds.tbluser_group_users;
        }

        private DS_Users.tbluser_entity_l2DataTable GetAvailableUserEL2s(SessionObject objSes)
        {
            DS_Users ds = new DS_Users();
            DS_Users.tbluser_entity_l2Row dr;
            foreach (string el2 in hndEl2.Value.Split('|'))
            {
                if (el2.Trim() != "")
                {
                    dr = ds.tbluser_entity_l2.Newtbluser_entity_l2Row();
                    dr.User_ID = 0;
                    dr.Entity_L2_ID = Convert.ToInt32(el2);
                    ds.tbluser_entity_l2.Rows.Add(dr);
                }
            }
            LoadEL2s(objSes);
            return ds.tbluser_entity_l2;
        }

        protected void gvEntity_L2_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    chkSelect.Attributes.Add("data-hnd", "hndEl2");
                }
            }
        }

        protected void gvUserGroups_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    chkSelect.Attributes.Add("data-hnd", "hndGroups");
                }
            }
        }

        protected void cmdAllocationExport_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            string FileName = "User_List_" + N_Ter.Common.Common_Actions.ShortTimeStampForFileName() + Convert.ToString(objSes.UserID) + ".xlsx";
            string PhysicalPath = objSes.PhysicalRoot + "\\nter_app_uploads\\temp_email_attachments\\" + FileName;

            if (chkSendEmail.Checked)
            {
                System.Threading.Thread objThread1 = new System.Threading.Thread(delegate () { EmailAllocationReport(objSes, PhysicalPath, Convert.ToInt32(cboExportType.SelectedValue)); });
                objThread1.Priority = System.Threading.ThreadPriority.BelowNormal;
                objThread1.Start();

                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Report Creation Initiated');", true);
            }
            else
            {
                if (GetAllocationReport(objSes, PhysicalPath, Convert.ToInt32(cboExportType.SelectedValue)))
                {
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
                    Response.ContentType = "application/octet-stream";
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();

                    Response.WriteFile(PhysicalPath);
                    Response.End();
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Empty Data Set');", true);
                }
            }
        }

        private bool GetAllocationReport(SessionObject objSes, string PhysicalPath, int ExportOption)
        {
            bool ret = true;

            Users objUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
            DS_Users dsUsers = objUsers.ReadAll();
            List<DS_Users.tblusersRow> drUsers;
            if (ExportOption == 2)
            {
                drUsers = dsUsers.tblusers.Where(x => x.isDeleted == false).ToList();
            }
            else if (ExportOption == 3)
            {
                drUsers = dsUsers.tblusers.Where(x => x.isDeleted).ToList();
            }
            else
            {
                drUsers = dsUsers.tblusers.ToList();
            }
            if (drUsers.Count > 0)
            {
                DS_Users dsUserGroups = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type).ReadAll();
                DS_Entity_Level_2 dsEL2 = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type).ReadAll();

                DS_Users dsUserData = objUsers.ReadAllUserData();
                DS_Settings dsSettings = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type).Read();
                Common_Actions objComAct = new Common_Actions();

                System.IO.FileInfo FilePath = new System.IO.FileInfo(PhysicalPath);

                OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                OfficeOpenXml.ExcelPackage objExcel = new OfficeOpenXml.ExcelPackage(FilePath);
                OfficeOpenXml.ExcelWorkbook objWorkBook = objExcel.Workbook;
                OfficeOpenXml.ExcelWorksheet objSheet = objWorkBook.Worksheets.Add("Users List");
                int Col_No = 3;
                int Row_No = 2;

                objSheet.Row(2).Height = 125;
                objSheet.Column(1).Width = 40;
                objSheet.Column(2).Width = 40;

                objSheet.Cells[Row_No, 1].Value = "Username";
                objSheet.Cells[Row_No, 1].Style.Font.Bold = true;
                objSheet.Cells[Row_No, 1].Style.Font.UnderLine = true;

                objSheet.Cells[Row_No, 2].Value = "Name";
                objSheet.Cells[Row_No, 2].Style.Font.Bold = true;
                objSheet.Cells[Row_No, 2].Style.Font.UnderLine = true;

                objSheet.Cells[1, Col_No, 1, Col_No + dsUserGroups.tbluser_groups.Rows.Count - 1].Merge = true;
                objSheet.Cells[1, Col_No].Value = "User Groups";
                objSheet.Cells[1, Col_No].Style.Font.Bold = true;

                foreach (DS_Users.tbluser_groupsRow row in dsUserGroups.tbluser_groups)
                {
                    objSheet.Cells[Row_No, Col_No].Value = row.User_Group_Name;
                    objSheet.Cells[Row_No, Col_No].Style.Font.Bold = true;
                    objSheet.Cells[Row_No, Col_No].Style.TextRotation = 90;
                    objSheet.Column(Col_No).Width = 3;
                    Col_No++;
                }

                objSheet.Column(Col_No).Width = 3;
                objSheet.Cells[1, Col_No, 2 + dsUsers.tblusers.Rows.Count, Col_No].Merge = true;
                Col_No++;

                objSheet.Cells[1, Col_No, 1, Col_No + dsEL2.tblentity_level_2.Rows.Count - 1].Merge = true;
                objSheet.Cells[1, Col_No].Value = objSes.EL2P;
                objSheet.Cells[1, Col_No].Style.Font.Bold = true;

                foreach (DS_Entity_Level_2.tblentity_level_2Row row in dsEL2.tblentity_level_2)
                {
                    objSheet.Cells[Row_No, Col_No].Value = row.Display_Name;
                    objSheet.Cells[Row_No, Col_No].Style.Font.Bold = true;
                    objSheet.Cells[Row_No, Col_No].Style.TextRotation = 90;
                    objSheet.Column(Col_No).Width = 3;
                    Col_No++;
                }

                Col_No++;
                objSheet.Cells[Row_No, Col_No].Value = "Last Login Date/Time";
                objSheet.Cells[Row_No, Col_No].Style.Font.Bold = true;
                objSheet.Cells[Row_No, Col_No].Style.TextRotation = 90;
                objSheet.Column(Col_No).Width = 20;

                Col_No++;
                objSheet.Cells[Row_No, Col_No].Value = "Last PW Change Date";
                objSheet.Cells[Row_No, Col_No].Style.Font.Bold = true;
                objSheet.Cells[Row_No, Col_No].Style.TextRotation = 90;
                objSheet.Column(Col_No).Width = 15;

                Col_No++;
                objSheet.Cells[Row_No, Col_No].Value = "Active/Inactive";
                objSheet.Cells[Row_No, Col_No].Style.Font.Bold = true;
                objSheet.Cells[Row_No, Col_No].Style.TextRotation = 90;
                objSheet.Column(Col_No).Width = 15;

                Col_No++;
                objSheet.Cells[Row_No, Col_No].Value = "PW Expiry Date";
                objSheet.Cells[Row_No, Col_No].Style.Font.Bold = true;
                objSheet.Cells[Row_No, Col_No].Style.TextRotation = 90;
                objSheet.Column(Col_No).Width = 15;

                Row_No++;

                foreach (DS_Users.tblusersRow drUser in drUsers)
                {
                    Col_No = 1;

                    objSheet.Cells[Row_No, Col_No].Value = drUser.Username;
                    objSheet.Cells[Row_No, Col_No].Style.Font.Bold = true;
                    Col_No++;

                    objSheet.Cells[Row_No, Col_No].Value = drUser.First_Name + " " + drUser.Last_Name;
                    objSheet.Cells[Row_No, Col_No].Style.Font.Bold = true;
                    Col_No++;
                    foreach (DS_Users.tbluser_groupsRow drGroup in dsUserGroups.tbluser_groups)
                    {
                        if (dsUserData.tbluser_group_users.Where(x => x.User_ID == drUser.User_ID && x.User_Group_ID == drGroup.User_Group_ID).Count() > 0)
                        {
                            objSheet.Cells[Row_No, Col_No].Value = "Y";
                            objSheet.Cells[Row_No, Col_No].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            objSheet.Cells[Row_No, Col_No].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.LightGray;
                        }
                        Col_No++;
                    }

                    Col_No++;
                    foreach (DS_Entity_Level_2.tblentity_level_2Row drEL2 in dsEL2.tblentity_level_2)
                    {
                        if (dsUserData.tbluser_entity_l2.Where(x => x.User_ID == drUser.User_ID && x.Entity_L2_ID == drEL2.Entity_L2_ID).Count() > 0)
                        {
                            objSheet.Cells[Row_No, Col_No].Value = "Y";
                            objSheet.Cells[Row_No, Col_No].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            objSheet.Cells[Row_No, Col_No].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.LightGray;
                        }
                        Col_No++;
                    }

                    //write the correct values for following 4 items
                    Col_No++;
                    objSheet.Cells[Row_No, Col_No].Value = drUser.IsLoginTimeNull() ? "Never" : string.Format("{0:" + Constants.DateFormat + " HH:mm}", drUser.LoginTime);
                    objSheet.Cells[Row_No, Col_No].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                    Col_No++;
                    objSheet.Cells[Row_No, Col_No].Value = drUser.IsPW_Changed_DateNull() ? "Never" : string.Format("{0:" + Constants.DateFormat + "}", drUser.PW_Changed_Date);
                    objSheet.Cells[Row_No, Col_No].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                    Col_No++;
                    objSheet.Cells[Row_No, Col_No].Value = drUser.isDeleted ? "Locked" : "Active";
                    objSheet.Cells[Row_No, Col_No].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                    Col_No++;
                    objSheet.Cells[Row_No, Col_No].Value = dsSettings.tblsettings[0].PW_Expires_After == 0 ? "N/A" : string.Format("{0:" + Constants.DateFormat + "}", objComAct.AddPeriodType(drUser.PW_Changed_Date, dsSettings.tblsettings[0].PW_Expires_After));
                    objSheet.Cells[Row_No, Col_No].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                    Row_No++;
                }

                objExcel.Save();
            }
            else
            {
                ret = false;
            }
            return ret;
        }

        private void EmailAllocationReport(SessionObject objSes, string PhysicalPath, int ExportOption)
        {
            Users objUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
            DS_Users dsUser = objUsers.Read(objSes.UserID);
            if (dsUser.tblusers.Count > 0)
            {
                if (GetAllocationReport(objSes, PhysicalPath, ExportOption))
                {
                    N_Ter.Emails.Email_Sender objEmail = new N_Ter.Emails.Email_Sender();
                    string[] strTo = new string[0];

                    objEmail.SendEmail("", "", new string[] { dsUser.tblusers[0].Username }, strTo, "Users List", "Requested users list is attached.", "", objSes.PhysicalRoot, ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type).Read().tblsettings[0], "", null, new string[] { PhysicalPath });
                }
            }
        }
    }
}