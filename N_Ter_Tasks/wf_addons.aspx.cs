using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class wf_addons : System.Web.UI.Page
    {
        protected void Page_Init(object sender, System.EventArgs e)
        {
            n_ter_base_loggedin_grid_wf pg = (n_ter_base_loggedin_grid_wf)this.Master;
            pg.PageClass = "main-menu-animated page-mail";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (IsPostBack == false)
            {
                if (Request.QueryString["fid"] == null)
                {
                    Response.Redirect("error.aspx?");
                }
                else
                {
                    SessionObject objSes = (SessionObject)Session["dt"];

                    LoadMasterTableNames(objSes);

                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    int WFID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["fid"])));
                    ViewState["fid"] = WFID;

                    Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                    DS_Workflow ds = objWF.Read(WFID, false, false);

                    LoadUploadDocTypes(ds);
                    ltrWorkflowName.Text = ds.tblwalkflow[0].Workflow_Name;

                    if (ds.tblwalkflow[0].Exrta_Field_Naming.Trim() != "")
                    {
                        ltrEx1Name2.Text = ds.tblwalkflow[0].Exrta_Field_Naming.Trim();
                        if (ds.tblwalkflow[0].Exrta_Field_Type == 3 && ds.tblwalkflow[0].Extra_Field_Master_Table_ID > 0)
                        {
                            cboMasterTableParamType_Addon.Items.Add(new ListItem("Special Field 1 (" + ds.tblwalkflow[0].Exrta_Field_Naming + ")", "1"));
                            cboMasterTableParam2Type_Addon.Items.Add(new ListItem("Special Field 1 (" + ds.tblwalkflow[0].Exrta_Field_Naming + ")", "1"));
                        }
                    }
                    else
                    {
                        ltrEx1Name2.Text = "Not Named Yet";
                    }
                    if (ds.tblwalkflow[0].Exrta_Field2_Naming.Trim() != "")
                    {
                        ltrEx2Name2.Text = ds.tblwalkflow[0].Exrta_Field2_Naming.Trim();
                        if (ds.tblwalkflow[0].Exrta_Field2_Type == 3 && ds.tblwalkflow[0].Extra_Field2_Master_Table_ID > 0)
                        {
                            cboMasterTableParamType_Addon.Items.Add(new ListItem("Special Field 2 (" + ds.tblwalkflow[0].Exrta_Field2_Naming + ")", "2"));
                            cboMasterTableParam2Type_Addon.Items.Add(new ListItem("Special Field 2 (" + ds.tblwalkflow[0].Exrta_Field2_Naming + ")", "2"));
                        }
                    }
                    else
                    {
                        ltrEx2Name2.Text = "Not Named Yet";
                    }
                    cboMasterTableParamType_Addon.Items.Add(new ListItem(objSes.EL2, "3"));
                    cboMasterTableParam2Type_Addon.Items.Add(new ListItem(objSes.EL2, "3"));

                    NewAddon(objSes);
                }
            }
            cmdCreateDuplicate.Attributes.Add("onClick", "return ValidateDuplicateAddon('" + txtNewAddon.ClientID + "')");

            cmdNewAddonField.Attributes.Add("onClick", "javascript:document.getElementById('" + hndAddon_Field_ID.ClientID + "').value = '0'; ClearControlsAddon();");
            cmdResetAddonField.Attributes.Add("onClick", "return ResetControlsAddon();");
            cmdSaveAddonField.Attributes.Add("onClick", "return ValidateAddonField('" + txtFieldName_Addon.ClientID + "', '" + cboFieldType_Addon.ClientID + "', '" + cboMasterTable_Addon.ClientID + "', '" + txtDefaultText_Addon.ClientID + "', '" + txtDefaultTextMemo_Addon.ClientID + "');");
            cmdSaveAddon.Attributes.Add("onClick", "return ValidateAddon('" + txtAddonName.ClientID + "');");
            cboFieldType_Addon.Attributes.Add("onChange", "ShowHideSectionsAddon(false);");
            cboCopyDataFrom_Addon.Attributes.Add("onChange", "CheckCopyDisableAddon(false);");
        }

        private void LoadAddonCombos(SessionObject objSes)
        {
            LoadValidateFields(objSes);
        }

        private void LoadMasterTableNames(SessionObject objSes)
        {
            N_Ter.Customizable.Custom_Lists objCus = new N_Ter.Customizable.Custom_Lists();
            DS_Master_Tables ds = objCus.LoadMasterTableNames(objSes.EL2P);

            cboMasterTable_Addon.DataSource = ds.tblTables;
            cboMasterTable_Addon.DataTextField = "Table_Name";
            cboMasterTable_Addon.DataValueField = "Table_ID";
            cboMasterTable_Addon.DataBind();
            cboMasterTable_Addon.Items.Insert(0, new ListItem("[Not Selected]", "0"));
        }

        private void RefreshGrid(DS_Workflow.tblworkflow_addonsDataTable tbl)
        {
            ltrAddons.Text = "";

            foreach (DS_Workflow.tblworkflow_addonsRow row in tbl)
            {
                if (Convert.ToInt32(hndWFAddonID.Value) == row.Addon_ID)
                {
                    ltrAddons.Text = ltrAddons.Text + "<li class=\"active-item group\">" + "\r\n" +
                                                        "<a href=\"#\" onclick=\"document.getElementById('" + hndWFAddonID.ClientID + "').value = '" + row.Addon_ID + "'; LoadAddon(); return false;\">" + "\r\n" +
                                                            "<span data-id=\"" + row.Addon_ID + "\" class=\"text-bold stp_no\">" + row.Addon_Name + "</span><br />" + "\r\n" +
                                                        "</a>" + "\r\n" +
                                                    "</li>" + "\r\n";
                }
                else
                {
                    ltrAddons.Text = ltrAddons.Text + "<li class='group'>" + "\r\n" +
                                                        "<a href=\"#\" onclick=\"document.getElementById('" + hndWFAddonID.ClientID + "').value = '" + row.Addon_ID + "'; LoadAddon(); return false;\">" + "\r\n" +
                                                            "<span data-id=\"" + row.Addon_ID + "\" class=\"text-bold stp_no\">" + row.Addon_Name + "</span><br />" + "\r\n" +
                                                        "</a>" + "\r\n" +
                                                    "</li>" + "\r\n";
                }
            }

            pnlAddon.Attributes.Remove("style");
            pnlAddon.Attributes.Add("style", "min-height: " + ((tbl.Rows.Count + 1) * 65) + "px");
        }

        protected void cmdNewAddon_ServerClick(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            NewAddon(objSes);
        }

        private void NewAddon(SessionObject objSes)
        {
            hndWFAddonID.Value = "0";
            ltrAddonMode.Text = "New Addon";
            cmdSaveAddon.Text = "Create";
            pnlAddon.Visible = true;
            pnlAddonFields.Visible = false;
            LoadAddonCombos(objSes);
            txtAddonName.Text = "";
            cboScreenSize.SelectedValue = "1";
            Workflow_Addons objWFA = ObjectCreator.GetWorkflow_Addons(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWFA.ReadAll(Convert.ToInt32(ViewState["fid"]));
            RefreshGrid(ds.tblworkflow_addons);

            cmdDeleteAddon.Visible = false;
            cmdDeleteAddon_ModalPopupExtender.Enabled = false;
            pnlDeleteAddon.Visible = false;

            cmdDuplicateAddon.Visible = false;
            cmdDuplicate_ModalPopupExtender.Enabled = false;
            txtNewAddon.Text = "";
        }

        protected void cmdLoadAddon_Click(object sender, EventArgs e)
        {
            pnlAddon.Visible = true;
            SessionObject objSes = (SessionObject)Session["dt"];
            LoadAddon(objSes);
        }

        private void LoadAddon(SessionObject objSes)
        {
            pnlAddonFields.Visible = true;
            ltrAddonMode.Text = "Change Addon";
            cmdSaveAddon.Text = "Save";
            Workflow_Addons objWFA = ObjectCreator.GetWorkflow_Addons(objSes.Connection, objSes.DB_Type);
            DS_Workflow dsAllAddons = objWFA.ReadAll(Convert.ToInt32(ViewState["fid"]));
            RefreshGrid(dsAllAddons.tblworkflow_addons);

            DS_Workflow dsAddon = objWFA.Read(Convert.ToInt32(hndWFAddonID.Value));
            txtAddonName.Text = dsAddon.tblworkflow_addons[0].Addon_Name;
            cboScreenSize.SelectedValue = Convert.ToString(dsAddon.tblworkflow_addons[0].Screen_Size);
            RefreshAddons(dsAddon.tblworkflow_addon_fields);

            cmdDeleteAddon.Visible = true;
            cmdDeleteAddon_ModalPopupExtender.Enabled = true;
            pnlDeleteAddon.Visible = true;
            LoadAddonCombos(objSes);
            LoadCopyFromFields(objSes);

            cmdDuplicateAddon.Visible = true;
            cmdDuplicate_ModalPopupExtender.Enabled = true;
            txtCurrentAddon.Text = dsAddon.tblworkflow_addons[0].Addon_Name;
            txtNewAddon.Text = "";
        }

        private void LoadCopyFromFields(SessionObject objSes)
        {
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.ReadOtherStepFields(Convert.ToInt32(ViewState["fid"]), 0);

            cboCopyDataFrom_Addon.DataSource = ds.tblworkflow_step_fields;
            cboCopyDataFrom_Addon.DataTextField = "Field_Name";
            cboCopyDataFrom_Addon.DataValueField = "Workflow_Step_Field_ID";
            cboCopyDataFrom_Addon.DataBind();
            cboCopyDataFrom_Addon.Items.Insert(0, new ListItem("[N/A]", "0"));
        }

        private void LoadValidateFields(SessionObject objSes)
        {
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.ReadAllFields(Convert.ToInt32(ViewState["fid"]));
            DS_Workflow.tblworkflow_step_fieldsRow dr = ds.tblworkflow_step_fields.Newtblworkflow_step_fieldsRow();
            dr.Workflow_Step_Field_ID = 0;
            dr.Field_Name_Extended = "[N/A]";
            ds.tblworkflow_step_fields.Rows.InsertAt(dr, 0);

            cboValidateField_Addon.DataSource = ds.tblworkflow_step_fields;
            cboValidateField_Addon.DataValueField = "Workflow_Step_Field_ID";
            cboValidateField_Addon.DataTextField = "Field_Name_Extended";
            cboValidateField_Addon.DataBind();
        }

        private void LoadUploadDocTypes(DS_Workflow dsWF)
        {
            cboDocCat_Addon.Items.Clear();
            cboDocCat_Addon.Items.Add(new ListItem("[General Documents]", "General Documents"));
            foreach (string strCat in dsWF.tblwalkflow[0].Workflow_Doc_Types.Split('|'))
            {
                if (strCat.Trim() != "")
                {
                    cboDocCat_Addon.Items.Add(new ListItem(strCat, strCat));
                }
            }
        }

        private void RefreshAddons(DS_Workflow.tblworkflow_addon_fieldsDataTable tbl)
        {
            foreach (DS_Workflow.tblworkflow_addon_fieldsRow row in tbl)
            {
                switch (row.Field_Size)
                {
                    case 1:
                        row.Field_SizeSP = "Full Width";
                        break;
                    case 2:
                        row.Field_SizeSP = "1/2 Width";
                        break;
                    case 3:
                        row.Field_SizeSP = "1/3 Width";
                        break;
                    case 4:
                        row.Field_SizeSP = "2/3 Width";
                        break;
                    case 5:
                        row.Field_SizeSP = "1/4 Width";
                        break;
                    case 6:
                        row.Field_SizeSP = "3/4 Width";
                        break;
                    case 7:
                        row.Field_SizeSP = "1/6 Width";
                        break;
                    case 8:
                        row.Field_SizeSP = "5/6 Width";
                        break;
                }
                switch (row.Field_Type)
                {
                    case 0:
                        row.Field_TypeSP = "Label Only";
                        break;
                    case 1:
                        row.Field_TypeSP = "Yes/No (Dropdown)";
                        break;
                    case 2:
                        row.Field_TypeSP = "Text";
                        break;
                    case 3:
                        row.Field_TypeSP = "Memo";
                        break;
                    case 4:
                        row.Field_TypeSP = "Number";
                        break;
                    case 5:
                        row.Field_TypeSP = "Selection";
                        break;
                    case 6:
                        row.Field_TypeSP = "Date";
                        break;
                    case 7:
                        row.Field_TypeSP = "Time";
                        break;
                    case 8:
                        row.Field_TypeSP = "Currency";
                        break;
                    case 9:
                        row.Field_TypeSP = "Percentage";
                        break;
                    case 10:
                        row.Field_TypeSP = "Master Table";
                        break;
                    case 11:
                        row.Field_TypeSP = "File Upload";
                        break;
                    case 12:
                        row.Field_TypeSP = "Time Span";
                        break;
                    case 13:
                        row.Field_TypeSP = "Yes/No (Switch)";
                        break;
                }
            }
            gvStepAddons.DataSource = tbl;
            gvStepAddons.DataBind();
            if (tbl.Rows.Count > 0)
            {
                gvStepAddons.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvStepAddon_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndAddon_Field_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValuesAddonField();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndAddon_Field_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
                e.Row.Attributes.Add("data-id", e.Row.Cells[0].Text);
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            pnlAddon.Visible = false;
            hndWFAddonID.Value = "0";
            Workflow_Addons objWFA = ObjectCreator.GetWorkflow_Addons(objSes.Connection, objSes.DB_Type);
            DS_Workflow dsAddons = objWFA.ReadAll(Convert.ToInt32(ViewState["fid"]));
            RefreshGrid(dsAddons.tblworkflow_addons);
        }

        protected void cmdSaveAddon_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow_Addons objWFA = ObjectCreator.GetWorkflow_Addons(objSes.Connection, objSes.DB_Type);
            if (hndWFAddonID.Value == "0")
            {
                int MaxID = 0;
                byte[] bytes = System.Text.Encoding.Default.GetBytes(txtAddonName.Text);
                txtAddonName.Text = System.Text.Encoding.UTF8.GetString(bytes);
                MaxID = objWFA.InsertAddon(txtAddonName.Text, Convert.ToInt32(ViewState["fid"]), Convert.ToInt32(cboScreenSize.SelectedItem.Value));

                if (MaxID > 0)
                {
                    hndWFAddonID.Value = Convert.ToString(MaxID);
                    DS_Workflow dsAddons = objWFA.ReadAll(Convert.ToInt32(ViewState["fid"]));
                    RefreshGrid(dsAddons.tblworkflow_addons);
                    LoadAddonCombos(objSes);
                    LoadAddon(objSes);

                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Addon succesfully Created');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Addon cannot be Created');", true);
                }
            }
            else
            {
                if (objWFA.UpdateAddon(Convert.ToInt32(hndWFAddonID.Value), txtAddonName.Text, Convert.ToInt32(cboScreenSize.SelectedItem.Value)) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Addon cannot be Updated');", true);
                }
                else
                {
                    DS_Workflow dsAddons = objWFA.ReadAll(Convert.ToInt32(ViewState["fid"]));
                    RefreshGrid(dsAddons.tblworkflow_addons);
                    LoadAddonCombos(objSes);
                    NewAddon(objSes);

                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Step succesfully Updated');", true);
                }
            }
        }

        protected void cmdDeleteAddonConf_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow_Addons objWFA = ObjectCreator.GetWorkflow_Addons(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objWFA.DeleteAddon(Convert.ToInt32(hndWFAddonID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                DS_Workflow dsAddons = objWFA.ReadAll(Convert.ToInt32(ViewState["fid"]));
                RefreshGrid(dsAddons.tblworkflow_addons);
                LoadAddonCombos(objSes);
                NewAddon(objSes);

                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Addon Successfully Deleted');", true);
            }
        }

        protected void cmdSaveAddonField_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow_Addons objWFA = ObjectCreator.GetWorkflow_Addons(objSes.Connection, objSes.DB_Type);
            string Default_Doc_Cat = "";
            if (cboFieldType_Addon.SelectedValue == "11")
            {
                Default_Doc_Cat = cboDocCat_Addon.SelectedValue;
            }
            if (hndAddon_Field_ID.Value == "0")
            {
                byte[] bytes = System.Text.Encoding.Default.GetBytes(txtFieldName_Addon.Text);
                txtFieldName_Addon.Text = System.Text.Encoding.UTF8.GetString(bytes);
                if (objWFA.InsertAddonField(Convert.ToInt32(hndWFAddonID.Value), txtFieldName_Addon.Text, Convert.ToInt32(cboFieldType_Addon.SelectedItem.Value), Convert.ToInt32(cboFieldSize_Addon.SelectedItem.Value), txtSelectionText_Addon.Text, Convert.ToInt32(cboMasterTable_Addon.SelectedValue), Convert.ToInt32(cboMasterTableParamType_Addon.SelectedValue), Convert.ToInt32(cboMasterTableParam2Type_Addon.SelectedValue), txtDefaultTextMemo_Addon.Text, Convert.ToInt32(cboValidateField_Addon.SelectedItem.Value), chkFieldRequired_Addon.Checked, Convert.ToInt32(string.IsNullOrEmpty(txtFieldMaxLength_Addon.Text.Trim(' ')) ? "-1" : txtFieldMaxLength_Addon.Text), Convert.ToInt32(cboCopyDataFrom_Addon.SelectedItem.Value), chkDisableControl_Addon.Checked, chkCopyEF1_Addon.Checked, chkCopyEF2_Addon.Checked, txtFieldHelpText_Addon.Text, chkTypable_Addon.Checked, Convert.ToInt32(cboAccessLevel_Addon.SelectedItem.Value), txtCustomData_Addon.Text, Default_Doc_Cat) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Step Addon Field could not be Saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Step Addon Field Successfully Saved');", true);

                    DS_Workflow ds = objWFA.Read(Convert.ToInt32(hndWFAddonID.Value));
                    RefreshAddons(ds.tblworkflow_addon_fields);
                }
            }
            else
            {
                if (objWFA.UpdateAddonField(Convert.ToInt32(hndAddon_Field_ID.Value), txtFieldName_Addon.Text, Convert.ToInt32(cboFieldType_Addon.SelectedItem.Value), Convert.ToInt32(cboFieldSize_Addon.SelectedItem.Value), txtSelectionText_Addon.Text, Convert.ToInt32(cboMasterTable_Addon.SelectedValue), Convert.ToInt32(cboMasterTableParamType_Addon.SelectedValue), Convert.ToInt32(cboMasterTableParam2Type_Addon.SelectedValue), txtDefaultTextMemo_Addon.Text, Convert.ToInt32(cboValidateField_Addon.SelectedItem.Value), chkFieldRequired_Addon.Checked, Convert.ToInt32(string.IsNullOrEmpty(txtFieldMaxLength_Addon.Text.Trim(' ')) ? "-1" : txtFieldMaxLength_Addon.Text), Convert.ToInt32(cboCopyDataFrom_Addon.SelectedItem.Value), chkDisableControl_Addon.Checked, chkCopyEF1_Addon.Checked, chkCopyEF2_Addon.Checked, txtFieldHelpText_Addon.Text, chkTypable_Addon.Checked, Convert.ToInt32(cboAccessLevel_Addon.SelectedItem.Value), txtCustomData_Addon.Text, Default_Doc_Cat) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow Step Addon Field could not be Saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Step Addon Field Successfully Saved');", true);

                    DS_Workflow ds = objWFA.Read(Convert.ToInt32(hndWFAddonID.Value));
                    RefreshAddons(ds.tblworkflow_addon_fields);
                }
            }
        }

        protected void cmeDeleteAddonFieldConf_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow_Addons objWFA = ObjectCreator.GetWorkflow_Addons(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objWFA.DeleteAddonField(Convert.ToInt32(hndAddon_Field_ID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Workflow Step Addon Field Successfully Deleted');", true);

                DS_Workflow ds = objWFA.Read(Convert.ToInt32(hndWFAddonID.Value));
                RefreshAddons(ds.tblworkflow_addon_fields);
                hndAddon_Field_ID.Value = "0";
            }
        }

        protected void cmdCreateDuplicate_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow_Addons objWFA = ObjectCreator.GetWorkflow_Addons(objSes.Connection, objSes.DB_Type);
            if (objWFA.CreateDuplicateAddon(Convert.ToInt32(hndWFAddonID.Value), txtNewAddon.Text))
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Duplicate Addon Successfully Created');", true);
                txtNewAddon.Text = "";
                DS_Workflow ds = objWFA.ReadAll(Convert.ToInt32(ViewState["fid"]));
                RefreshGrid(ds.tblworkflow_addons);
                LoadAddonCombos(objSes);
                LoadAddon(objSes);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Duplicate Addon could not be Created');", true);
            }
        }
    }
}