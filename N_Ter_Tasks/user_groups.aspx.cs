using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class user_groups : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                SessionObject objSes = (SessionObject)Session["dt"];
                DualAuthTypes AuthType = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type).GetAuthenticatoinType(objSes.UserID, DualAuthArea.UserGroups);
                if (AuthType != DualAuthTypes.Approve)
                {
                    cmdDiscard.Visible = false;
                }
                else
                {
                    cmdNew.Visible = false;
                    cmdNew_ModalPopupExtender.Enabled = false;
                }
                ViewState["au"] = (int)AuthType;
                LoadPages(objSes);
                LoadReports();
                LoadSections();
                hndUserGroupID.Value = "0";
                RefreshGrid(objSes);
            }

            cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndUserGroupID.ClientID + "').value = '0'; ClearControls();");
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidateUserGroup('" + txtUserGroupName.ClientID + "');";

            tab2.ClientIDMode = ClientIDMode.Static;
            tab3.ClientIDMode = ClientIDMode.Static;
            tab_cont2.ClientIDMode = ClientIDMode.Static;
            tab_cont3.ClientIDMode = ClientIDMode.Static;
        }

        private void LoadPages(SessionObject objSes)
        {
            Settings objSettings = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type);
            DS_Pages ds = objSettings.ReadPages();
            chkAllowedPages.DataSource = ds.tblpages;
            chkAllowedPages.DataTextField = ds.tblpages.Page_DescriptionColumn.ColumnName;
            chkAllowedPages.DataValueField = ds.tblpages.Page_IDColumn.ColumnName;
            chkAllowedPages.DataBind();
        }

        private void LoadReports()
        {
            DS_Reports ds = new N_Ter.Customizable.Custom_Lists().LoadReports();
            if (ds.tblReports.Rows.Count > 0)
            {
                chkAllowedReports.DataSource = ds.tblReports;
                chkAllowedReports.DataTextField = ds.tblReports.Report_NameColumn.ColumnName;
                chkAllowedReports.DataValueField = ds.tblReports.Report_IDColumn.ColumnName;
                chkAllowedReports.DataBind();
            }
            else
            {
                tab2.Visible = false;
                tab_cont2.Visible = false;
            }
        }

        private void LoadSections()
        {
            DS_Extra_Sections ds = new N_Ter.Customizable.Custom_Lists().LoadExtraSections();
            if (ds.tblSections.Rows.Count > 0)
            {
                chkAllowedSections.DataSource = ds.tblSections;
                chkAllowedSections.DataTextField = ds.tblSections.Section_NameColumn.ColumnName;
                chkAllowedSections.DataValueField = ds.tblSections.Section_IDColumn.ColumnName;
                chkAllowedSections.DataBind();
            }
            else
            {
                tab3.Visible = false;
                tab_cont3.Visible = false;
            }
        }

        private void RefreshGrid(SessionObject objSes)
        {
            User_Groups objUG = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type);
            DualAuthTypes AuthType = (DualAuthTypes)ViewState["au"];
            DS_Users ds = null;
            switch (AuthType)
            {
                case DualAuthTypes.Regular:
                    ds = objUG.ReadAll();
                    break;
                case DualAuthTypes.DataEntry:
                    ds = objUG.ReadAllDataEntry();
                    break;
                case DualAuthTypes.Approve:
                    ds = objUG.ReadAllApprove();
                    break;
            }
            gvMain.SelectedIndex = -1;
            gvMain.DataSource = ds.tbluser_groups;
            gvMain.DataBind();
            if (ds.tbluser_groups.Rows.Count > 0)
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
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndUserGroupID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdUsers = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdUsers.Attributes.Add("onclick", "document.getElementById('" + hndUserGroupID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; $('#" + lblGroupName.ClientID + "').text('" + e.Row.Cells[4].Text + "'); LoadUsers();");
                }
                if (e.Row.Cells[3].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[3].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndUserGroupID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            DualAuthTypes AuthType = (DualAuthTypes)ViewState["au"];
            if (AuthType == DualAuthTypes.Regular)
            {
                User_Groups objUG = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type);
                DeleteReason del = objUG.Delete(Convert.ToInt32(hndUserGroupID.Value));
                if (del.Deleted == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('User Group Successfully Deleted');", true);
                    hndUserGroupID.Value = "0";
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('You are not allowed to Delete User Groups');", true);
            }
            RefreshGrid(objSes);
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            User_Groups objUG = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type);
            bool Result = false;
            DualAuthTypes AuthType = (DualAuthTypes)ViewState["au"];
            if (hndUserGroupID.Value == "0")
            {
                switch (AuthType)
                {
                    case DualAuthTypes.Regular:
                        Result = objUG.Insert(txtUserGroupName.Text, Convert.ToInt32(cboAccessLevel.SelectedItem.Value), chkDeleteDocuments.Checked, chkDeleteAddons.Checked, chkDeleteComments.Checked, chkEditDocRepo.Checked, chkTaskScript.Checked, GetAvailablePages(), GetAvailableReports(), GetAvailableSections());
                        break;
                    case DualAuthTypes.DataEntry:
                        Result = objUG.InsertTransit(0, txtUserGroupName.Text, Convert.ToInt32(cboAccessLevel.SelectedItem.Value), chkDeleteDocuments.Checked, chkDeleteAddons.Checked, chkDeleteComments.Checked, chkEditDocRepo.Checked, chkTaskScript.Checked, GetAvailablePages(), GetAvailableReports(), GetAvailableSections());
                        break;
                }
                if (Result == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('User Group could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('User Group Details Successfully Added');", true);
                    RefreshGrid(objSes);
                    hndUserGroupID.Value = "0";
                }
            }
            else
            {
                switch (AuthType)
                {
                    case DualAuthTypes.Regular:
                        Result = objUG.Update(Convert.ToInt32((hndUserGroupID.Value)), txtUserGroupName.Text, Convert.ToInt32(cboAccessLevel.SelectedItem.Value), chkDeleteDocuments.Checked, chkDeleteAddons.Checked, chkDeleteComments.Checked, chkEditDocRepo.Checked, chkTaskScript.Checked, GetAvailablePages(), GetAvailableReports(), GetAvailableSections());
                        break;
                    case DualAuthTypes.DataEntry:
                        Result = objUG.InsertTransit(Convert.ToInt32((hndUserGroupID.Value)), txtUserGroupName.Text, Convert.ToInt32(cboAccessLevel.SelectedItem.Value), chkDeleteDocuments.Checked, chkDeleteAddons.Checked, chkDeleteComments.Checked, chkEditDocRepo.Checked, chkTaskScript.Checked, GetAvailablePages(), GetAvailableReports(), GetAvailableSections());
                        break;
                    case DualAuthTypes.Approve:
                        Result = objUG.TransferFromTransit(Convert.ToInt32(hndUserGroupID.Value));
                        break;
                }
                if (Result == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('User Group could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('User Group Details Successfully Updated');", true);
                    RefreshGrid(objSes);
                    hndUserGroupID.Value = "0";
                }
            }
        }

        protected void cmdDiscard_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            DualAuthTypes AuthType = (DualAuthTypes)ViewState["au"];
            if (AuthType == DualAuthTypes.Approve)
            {
                User_Groups objUG = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type);
                DeleteReason del = objUG.DiscardTransit(Convert.ToInt32(hndUserGroupID.Value));
                if (del.Deleted == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('User Group Update Successfully Deleted');", true);
                    hndUserGroupID.Value = "0";
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('You are not allowed to Delete User Group Amendments');", true);
            }
            RefreshGrid(objSes);
        }

        private DS_Users.tbluser_group_pagesDataTable GetAvailablePages()
        {
            DS_Users ds = new DS_Users();
            DS_Users.tbluser_group_pagesRow dr;
            foreach (ListItem itm in chkAllowedPages.Items)
            {
                if (itm.Selected == true)
                {
                    dr = ds.tbluser_group_pages.Newtbluser_group_pagesRow();
                    dr.User_Group_ID = 0;
                    dr.Page_ID = Convert.ToInt32(itm.Value);
                    ds.tbluser_group_pages.Rows.Add(dr);
                }
            }
            return ds.tbluser_group_pages;
        }

        private DS_Users.tbluser_group_reportsDataTable GetAvailableReports()
        {
            DS_Users ds = new DS_Users();
            DS_Users.tbluser_group_reportsRow dr;
            foreach (ListItem itm in chkAllowedReports.Items)
            {
                if (itm.Selected == true)
                {
                    dr = ds.tbluser_group_reports.Newtbluser_group_reportsRow();
                    dr.User_Group_ID = 0;
                    dr.Report_ID = Convert.ToInt32(itm.Value);
                    ds.tbluser_group_reports.Rows.Add(dr);
                }
            }
            return ds.tbluser_group_reports;
        }

        private DS_Users.tbluser_group_extra_pagesDataTable GetAvailableSections()
        {
            DS_Users ds = new DS_Users();
            DS_Users.tbluser_group_extra_pagesRow dr;
            foreach (ListItem itm in chkAllowedSections.Items)
            {
                if (itm.Selected == true)
                {
                    dr = ds.tbluser_group_extra_pages.Newtbluser_group_extra_pagesRow();
                    dr.User_Group_ID = 0;
                    dr.Section_ID = Convert.ToInt32(itm.Value);
                    ds.tbluser_group_extra_pages.Rows.Add(dr);
                }
            }
            return ds.tbluser_group_extra_pages;
        }

        protected void cmdGroupRights_ServerClick(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            DS_Users dsUserGroups = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type).ReadAll();
            DS_Pages dsPages = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type).ReadPages();
            N_Ter.Customizable.Custom_Lists objCus = new N_Ter.Customizable.Custom_Lists();
            DS_Extra_Sections dsExtra = objCus.LoadExtraSections();
            DS_Reports dsRpt = objCus.LoadReports();

            string FileName = "User_Groups_List_" + N_Ter.Common.Common_Actions.ShortTimeStampForFileName() + Convert.ToString(objSes.UserID) + ".xlsx";
            string PhysicalPath = objSes.PhysicalRoot + "\\nter_app_uploads\\temp_email_attachments\\" + FileName;

            System.IO.FileInfo FilePath = new System.IO.FileInfo(PhysicalPath);

            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            OfficeOpenXml.ExcelPackage objExcel = new OfficeOpenXml.ExcelPackage(FilePath);
            OfficeOpenXml.ExcelWorkbook objWorkBook = objExcel.Workbook;
            OfficeOpenXml.ExcelWorksheet objSheet2 = objWorkBook.Worksheets.Add("User Group Rights");

            int Col_No = 2;
            int Row_No = 2;

            objSheet2.Row(2).Height = 125;
            objSheet2.Column(1).Width = 50;

            objSheet2.Cells[1, Col_No, 1, Col_No + dsUserGroups.tbluser_groups.Rows.Count - 1].Merge = true;
            objSheet2.Cells[1, Col_No].Value = "User Groups";
            objSheet2.Cells[1, Col_No].Style.Font.Bold = true;

            foreach (DS_Users.tbluser_groupsRow row in dsUserGroups.tbluser_groups)
            {
                objSheet2.Cells[Row_No, Col_No].Value = row.User_Group_Name;
                objSheet2.Cells[Row_No, Col_No].Style.Font.Bold = true;
                objSheet2.Cells[Row_No, Col_No].Style.TextRotation = 90;
                objSheet2.Column(Col_No).Width = 3;
                Col_No++;
            }

            Row_No++;

            objSheet2.Cells[Row_No, 2, Row_No, dsUserGroups.tbluser_groups.Rows.Count + 1].Merge = true;
            objSheet2.Cells[Row_No, 1].Value = "Main Pages";
            objSheet2.Cells[Row_No, 1].Style.Font.Bold = true;
            objSheet2.Cells[Row_No, 1].Style.Font.UnderLine = true;
            objSheet2.Row(Row_No).Height = 30;
            objSheet2.Cells[Row_No, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
            Row_No++;

            foreach (DS_Pages.tblpagesRow drPage in dsPages.tblpages)
            {
                Col_No = 1;

                objSheet2.Cells[Row_No, Col_No].Value = drPage.Page_Description;
                objSheet2.Cells[Row_No, Col_No].Style.Font.Bold = true;

                Col_No++;
                foreach (DS_Users.tbluser_groupsRow drGroup in dsUserGroups.tbluser_groups)
                {
                    if (dsUserGroups.tbluser_group_pages.Where(x => x.Page_ID == drPage.Page_ID && x.User_Group_ID == drGroup.User_Group_ID).Count() > 0)
                    {
                        objSheet2.Cells[Row_No, Col_No].Value = "Y";
                        objSheet2.Cells[Row_No, Col_No].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        objSheet2.Cells[Row_No, Col_No].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.LightGray;
                    }
                    Col_No++;
                }
                Row_No++;
            }

            objSheet2.Cells[Row_No, 2, Row_No, dsUserGroups.tbluser_groups.Rows.Count + 1].Merge = true;
            objSheet2.Cells[Row_No, 1].Value = "Custom Pages";
            objSheet2.Cells[Row_No, 1].Style.Font.Bold = true;
            objSheet2.Cells[Row_No, 1].Style.Font.UnderLine = true;
            objSheet2.Row(Row_No).Height = 30;
            objSheet2.Cells[Row_No, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
            Row_No++;

            foreach (DS_Extra_Sections.tblSectionsRow drSection in dsExtra.tblSections)
            {
                Col_No = 1;

                objSheet2.Cells[Row_No, Col_No].Value = drSection.Section_Name;
                objSheet2.Cells[Row_No, Col_No].Style.Font.Bold = true;

                Col_No++;
                foreach (DS_Users.tbluser_groupsRow drGroup in dsUserGroups.tbluser_groups)
                {
                    if (dsUserGroups.tbluser_group_extra_pages.Where(x => x.Section_ID == drSection.Section_ID && x.User_Group_ID == drGroup.User_Group_ID).Count() > 0)
                    {
                        objSheet2.Cells[Row_No, Col_No].Value = "Y";
                        objSheet2.Cells[Row_No, Col_No].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        objSheet2.Cells[Row_No, Col_No].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.LightGray;
                    }
                    Col_No++;
                }
                Row_No++;
            }

            objSheet2.Cells[Row_No, 2, Row_No, dsUserGroups.tbluser_groups.Rows.Count + 1].Merge = true;
            objSheet2.Cells[Row_No, 1].Value = "Reports";
            objSheet2.Cells[Row_No, 1].Style.Font.Bold = true;
            objSheet2.Cells[Row_No, 1].Style.Font.UnderLine = true;
            objSheet2.Row(Row_No).Height = 30;
            objSheet2.Cells[Row_No, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
            Row_No++;

            foreach (DS_Reports.tblReportsRow drReport in dsRpt.tblReports)
            {
                Col_No = 1;

                objSheet2.Cells[Row_No, Col_No].Value = drReport.Report_Name;
                objSheet2.Cells[Row_No, Col_No].Style.Font.Bold = true;

                Col_No++;
                foreach (DS_Users.tbluser_groupsRow drGroup in dsUserGroups.tbluser_groups)
                {
                    if (dsUserGroups.tbluser_group_reports.Where(x => x.Report_ID == drReport.Report_ID && x.User_Group_ID == drGroup.User_Group_ID).Count() > 0)
                    {
                        objSheet2.Cells[Row_No, Col_No].Value = "Y";
                        objSheet2.Cells[Row_No, Col_No].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        objSheet2.Cells[Row_No, Col_No].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.LightGray;
                    }
                    Col_No++;
                }
                Row_No++;
            }

            objSheet2.Cells[Row_No, 2, Row_No, dsUserGroups.tbluser_groups.Rows.Count + 1].Merge = true;
            objSheet2.Cells[Row_No, 1].Value = "Special Rights";
            objSheet2.Cells[Row_No, 1].Style.Font.Bold = true;
            objSheet2.Cells[Row_No, 1].Style.Font.UnderLine = true;
            objSheet2.Row(Row_No).Height = 30;
            objSheet2.Cells[Row_No, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
            Row_No++;

            Col_No = 1;

            objSheet2.Cells[Row_No, Col_No].Value = "Delete Documents";
            objSheet2.Cells[Row_No, Col_No].Style.Font.Bold = true;

            Col_No++;
            foreach (DS_Users.tbluser_groupsRow drGroup in dsUserGroups.tbluser_groups)
            {
                if (drGroup.Delete_Docs)
                {
                    objSheet2.Cells[Row_No, Col_No].Value = "Y";
                    objSheet2.Cells[Row_No, Col_No].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    objSheet2.Cells[Row_No, Col_No].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.LightGray;
                }
                Col_No++;
            }
            Row_No++;

            Col_No = 1;

            objSheet2.Cells[Row_No, Col_No].Value = "Delete Addons";
            objSheet2.Cells[Row_No, Col_No].Style.Font.Bold = true;

            Col_No++;
            foreach (DS_Users.tbluser_groupsRow drGroup in dsUserGroups.tbluser_groups)
            {
                if (drGroup.Delete_Addons)
                {
                    objSheet2.Cells[Row_No, Col_No].Value = "Y";
                    objSheet2.Cells[Row_No, Col_No].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    objSheet2.Cells[Row_No, Col_No].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.LightGray;
                }
                Col_No++;
            }
            Row_No++;

            Col_No = 1;

            objSheet2.Cells[Row_No, Col_No].Value = "Delete Comments";
            objSheet2.Cells[Row_No, Col_No].Style.Font.Bold = true;

            Col_No++;
            foreach (DS_Users.tbluser_groupsRow drGroup in dsUserGroups.tbluser_groups)
            {
                if (drGroup.Delete_Comments)
                {
                    objSheet2.Cells[Row_No, Col_No].Value = "Y";
                    objSheet2.Cells[Row_No, Col_No].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    objSheet2.Cells[Row_No, Col_No].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.LightGray;
                }
                Col_No++;
            }

            OfficeOpenXml.ExcelWorksheet objSheet3 = objWorkBook.Worksheets.Add("User Group Allocation");

            Col_No = 2;
            Row_No = 2;

            objSheet3.Row(2).Height = 125;
            objSheet3.Column(1).Width = 50;

            objSheet3.Cells[1, Col_No, 1, Col_No + dsUserGroups.tbluser_groups.Rows.Count - 1].Merge = true;
            objSheet3.Cells[1, Col_No].Value = "User Groups";
            objSheet3.Cells[1, Col_No].Style.Font.Bold = true;

            foreach (DS_Users.tbluser_groupsRow row in dsUserGroups.tbluser_groups)
            {
                objSheet3.Cells[Row_No, Col_No].Value = row.User_Group_Name;
                objSheet3.Cells[Row_No, Col_No].Style.Font.Bold = true;
                objSheet3.Cells[Row_No, Col_No].Style.TextRotation = 90;
                objSheet3.Column(Col_No).Width = 3;
                Col_No++;
            }

            Row_No++;

            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);

            DS_Workflow dsWF = objWF.ReadAll();

            foreach (DS_Workflow.tblwalkflowRow drWF in dsWF.tblwalkflow.OrderBy(x => x.Workflow_Name))
            {
                objSheet3.Cells[Row_No, 2, Row_No, dsUserGroups.tbluser_groups.Rows.Count + 1].Merge = true;
                objSheet3.Cells[Row_No, 1].Value = drWF.Workflow_Name;
                objSheet3.Cells[Row_No, 1].Style.Font.Bold = true;
                objSheet3.Cells[Row_No, 1].Style.Font.UnderLine = true;
                objSheet3.Row(Row_No).Height = 30;
                objSheet3.Cells[Row_No, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                Row_No++;

                DS_Workflow dsSelectedWF = objWF.Read(drWF.Walkflow_ID);

                foreach (DS_Workflow.tblworkflow_stepsRow drStep in dsSelectedWF.tblworkflow_steps.OrderBy(x => x.Sort_order))
                {
                    Col_No = 1;

                    objSheet3.Cells[Row_No, Col_No].Value = drStep.Step_Status;
                    objSheet3.Cells[Row_No, Col_No].Style.Font.Bold = true;

                    Col_No++;
                    foreach (DS_Users.tbluser_groupsRow drGroup in dsUserGroups.tbluser_groups)
                    {
                        if (drGroup.User_Group_ID == drStep.User_Group_Involved)
                        {
                            objSheet3.Cells[Row_No, Col_No].Value = "Y";
                            objSheet3.Cells[Row_No, Col_No].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            objSheet3.Cells[Row_No, Col_No].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.LightGray;
                        }
                        Col_No++;
                    }
                    Row_No++;
                }
            }

            objExcel.Save();

            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
            Response.ContentType = "application/octet-stream";
            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();

            Response.WriteFile(PhysicalPath);
            Response.End();
        }
    }
}