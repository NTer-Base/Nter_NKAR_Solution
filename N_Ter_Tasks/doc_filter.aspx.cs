using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter.Base;
using N_Ter.Common;

namespace N_Ter_Tasks
{
    public partial class doc_filter : System.Web.UI.Page
    {
        private DP_Controls_Main objDPCtrlList = new DP_Controls_Main();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            ((BoundField)gvDocs.Columns[4]).DataFormatString = "{0:" + Constants.DateFormat + " HH:mm}";
            if (IsPostBack == false)
            {
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                if (string.IsNullOrEmpty(Convert.ToString(Request.QueryString["tid"])) == false)
                {
                    Custom_Base objCus = ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type);
                    int Task_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["tid"])));
                    Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

                    DS_Users dsAltUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).ReadAssignedUsers(objSes.UserID);
                    if (objTask.Can_Access_Task(Task_ID, objSes.UserID, dsAltUsers.tblusers, objCus) == false)
                    {
                        Response.Redirect("no_access.aspx?");
                    }
                    else
                    {
                        ltrEL2.Text = objSes.EL2;
                        ViewState["tid"] = Task_ID;
                        int Section = 0;
                        if (Request.QueryString["s"] != null)
                        {
                            Section = Convert.ToInt32(Request.QueryString["s"]);
                            cboFolder.SelectedIndex = Section;
                        }
                    }
                }
                else
                {
                    Response.Redirect("error.aspx?");
                }
            }

            LoadTaskDetails(objSes);
            cmdStart.Attributes.Add("onClick", "return validateProcess();");
        }

        private void LoadTaskDetails(SessionObject objSes)
        {
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            DS_Tasks ds = objTask.Read(Task_ID, false, false, false);

            if (ds.tbltasks.Rows.Count > 0)
            {
                if (ds.tbltasks[0].Current_Step_ID != -1 && ds.tbltasks[0].Current_Step_ID != -2)
                {
                    Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                    DS_Workflow dsWF = objWF.Read(ds.tbltasks[0].Walkflow_ID, false, false);

                    lblTaskNo.Text = ds.tbltasks[0].Task_Number;
                    ltrTaskNumber.Text = " : " + ds.tbltasks[0].Task_Number;
                    lblWorkflow.Text = ds.tbltasks[0].Workflow_Name;
                    if (ds.tbltasks[0].Creator_On_Behalf_ID == 0 || ds.tbltasks[0].Creator_ID == ds.tbltasks[0].Creator_On_Behalf_ID)
                    {
                        lblTaskCreator.Text = ds.tbltasks[0].Created_By;
                    }
                    else
                    {
                        DS_Users dsOnBehalf = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).Read(ds.tbltasks[0].Creator_On_Behalf_ID);
                        lblTaskCreator.Text = dsOnBehalf.tblusers[0].First_Name + " " + dsOnBehalf.tblusers[0].Last_Name + " (on behalf of " + ds.tbltasks[0].Created_By + ")";
                    }
                    lblEL2Name.Text = ds.tbltasks[0].Display_Name;
                    lblCurrentStatus.Text = ds.tbltasks[0].Step_Status;
                    lblTaskDate.Text = string.Format("{0:" + Constants.DateFormat + " HH:mm}", ds.tbltasks[0].Task_Date);
                    lblDueDate.Text = ds.tbltasks[0].IsETB_ValueNull() ? "" : string.Format("{0:" + Constants.DateFormat + " HH:mm}", ds.tbltasks[0].ETB_Value);

                    LoadExtraFeilds(ds, objSes);
                    LoadTagDetails(dsWF.tblwalkflow[0].Document_Project_ID, objSes);
                    if (IsPostBack == false)
                    {
                        ProcessFilter(objSes);
                    }
                }
                else
                {
                    Response.Redirect("error.aspx?");
                }
            }
            else
            {
                Response.Redirect("error.aspx?");
            }
        }

        private void LoadExtraFeilds(DS_Tasks dsTask, SessionObject objSes)
        {
            Tasks objTK = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            string[] arr = objTK.ReadExtraField(Convert.ToInt32(ViewState["tid"]), true);

            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.ReadStep(dsTask.tbltasks[0].Current_Step_ID);

            if (!string.IsNullOrEmpty(arr[0].Trim(' ')))
            {
                lblExtraFieldName.Text = arr[0];
                lblExtraFieldValue.Text = arr[1];
                divExtraField.Visible = true;
            }
            else
            {
                lblExtraFieldName.Text = string.Empty;
                lblExtraFieldValue.Text = string.Empty;
                divExtraField.Visible = false;
            }
            if (!string.IsNullOrEmpty(arr[2].Trim(' ')))
            {
                lblExtraField2Name.Text = arr[2];
                lblExtraField2Value.Text = arr[3];
                divExtraField2.Visible = true;
            }
            else
            {
                lblExtraField2Name.Text = string.Empty;
                lblExtraField2Value.Text = string.Empty;
                divExtraField2.Visible = false;
            }
        }

        private void LoadTagDetails(int Document_Project_ID, SessionObject objSes)
        {
            Document_Projects objDP = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
            DS_Doc_Project ds = objDP.Read(Document_Project_ID);

            Common_Doc_Actions objComDoc = new Common_Doc_Actions();

            ControlTypes objCtrl = ControlTypes.LabelOnly;

            foreach (DS_Doc_Project.tbldocument_project_indexesRow row in ds.tbldocument_project_indexes)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divControl.Attributes.Add("class", "row padding-xs-vr");
                divControl.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                divControl.ID = "divDocControl" + row.Document_Project_Index_ID;

                System.Web.UI.HtmlControls.HtmlGenericControl divLabel = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divLabel.Attributes.Add("class", "col-md-3");
                divLabel.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                divLabel.ID = "divLabel" + row.Document_Project_Index_ID;
                Label lblTitle = new Label();
                lblTitle.Text = row.Tag_Name;
                divLabel.Controls.Add(lblTitle);
                divControl.Controls.Add(divLabel);

                System.Web.UI.HtmlControls.HtmlGenericControl divCondition = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divCondition.Attributes.Add("class", "col-md-3");
                divCondition.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                divCondition.ID = "divCondition" + row.Document_Project_Index_ID;

                System.Web.UI.HtmlControls.HtmlGenericControl divValue = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divValue.Attributes.Add("class", "col-md-6");
                divValue.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                divValue.ID = "divValue" + row.Document_Project_Index_ID;

                DP_Controls drUI = new DP_Controls();
                drUI.Document_Project_Index_ID = row.Document_Project_Index_ID;

                objCtrl = (ControlTypes)row.Tag_Type;
                drUI.Tag_Type = objCtrl;

                objComDoc.PrepareFilterControl(row, objCtrl, ref divValue, ref divCondition, ref drUI);

                objDPCtrlList.Controls.Add(drUI);

                divControl.Controls.Add(divCondition);
                divControl.Controls.Add(divValue);

                divDocFilters.Controls.Add(divControl);
            }
        }

        protected void cmdBackToTask_ServerClick(object sender, EventArgs e)
        {
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
            Response.Redirect("task.aspx?tid=" + objURL.Encrypt(Convert.ToString(Task_ID)));
        }

        protected void cmdFilter_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            ProcessFilter(objSes);
        }

        private void ProcessFilter(SessionObject objSes)
        {
            List<Document_Filters> lstFilters = new List<Document_Filters>();

            int intFilters = 0;
            foreach (DP_Controls row in objDPCtrlList.Controls)
            {
                Document_Filters objFilter = new Document_Filters();
                objFilter.Doc_Index = row.Document_Project_Index_ID;
                objFilter.Tag_Type = row.Tag_Type;

                if (row.UI_Type == UI_Types.DropDowns || row.UI_Type == UI_Types.Checkbox)
                {
                    DropDownList drp = (DropDownList)row.UI_Control;
                    objFilter.Filter = drp.SelectedItem.Text;
                }
                else if (row.UI_Type == UI_Types.TextBoxes)
                {
                    TextBox txt = (TextBox)row.UI_Control;
                    objFilter.Filter = txt.Text;
                }

                DropDownList drpCon = (DropDownList)row.UI_Condition;
                if (drpCon.SelectedIndex == 0)
                {
                    objFilter.Filter_Type = Document_Filter_Types.None;
                }
                else
                {
                    if (row.Tag_Type == ControlTypes.Yes_No_Dropdown || row.Tag_Type == ControlTypes.Yes_No_Switch || row.Tag_Type == ControlTypes.Selection)
                    {
                        if (drpCon.SelectedIndex == 1)
                        {
                            objFilter.Filter_Type = Document_Filter_Types.Equals;
                        }
                        else
                        {
                            objFilter.Filter_Type = Document_Filter_Types.Not_Equal;
                        }
                        intFilters++;
                    }
                    else if (row.Tag_Type == ControlTypes.Text || row.Tag_Type == ControlTypes.Memo)
                    {
                        if (drpCon.SelectedIndex == 1)
                        {
                            objFilter.Filter_Type = Document_Filter_Types.Starts_With;
                            intFilters++;
                        }
                        else if (drpCon.SelectedIndex == 2)
                        {
                            objFilter.Filter_Type = Document_Filter_Types.Ends_With;
                            intFilters++;
                        }
                        else if (drpCon.SelectedIndex == 3)
                        {
                            objFilter.Filter_Type = Document_Filter_Types.Contains;
                            intFilters = intFilters + objFilter.Filter.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
                        }
                        else
                        {
                            objFilter.Filter_Type = Document_Filter_Types.Not_Contain;
                            intFilters = intFilters + objFilter.Filter.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
                        }
                    }
                    else if (row.Tag_Type == ControlTypes.Number)
                    {
                        if (drpCon.SelectedIndex == 1)
                        {
                            objFilter.Filter_Type = Document_Filter_Types.Equals;
                        }
                        else if (drpCon.SelectedIndex == 2)
                        {
                            objFilter.Filter_Type = Document_Filter_Types.Not_Equal;
                        }
                        else if (drpCon.SelectedIndex == 3)
                        {
                            objFilter.Filter_Type = Document_Filter_Types.Greater_Than;
                        }
                        else
                        {
                            objFilter.Filter_Type = Document_Filter_Types.Less_Than;
                        }
                        intFilters++;
                    }
                    else if (row.Tag_Type == ControlTypes.Date || row.Tag_Type == ControlTypes.Time)
                    {
                        if (drpCon.SelectedIndex == 1)
                        {
                            objFilter.Filter_Type = Document_Filter_Types.Equals;
                        }
                        else if (drpCon.SelectedIndex == 2)
                        {
                            objFilter.Filter_Type = Document_Filter_Types.Greater_Than;
                        }
                        else
                        {
                            objFilter.Filter_Type = Document_Filter_Types.Less_Than;
                        }
                        intFilters++;
                    }
                }
                lstFilters.Add(objFilter);
            }

            Document_Filters objDocFilter = new Document_Filters();
            objDocFilter.Doc_Index = -1;
            objDocFilter.Tag_Type = ControlTypes.Text;
            objDocFilter.Filter = txtDocCriteria.Text;

            if (cboDocOperator.SelectedIndex == 1)
            {
                objDocFilter.Filter_Type = Document_Filter_Types.Starts_With;
                intFilters++;
            }
            else if (cboDocOperator.SelectedIndex == 2)
            {
                objDocFilter.Filter_Type = Document_Filter_Types.Ends_With;
                intFilters++;
            }
            else if (cboDocOperator.SelectedIndex == 3)
            {
                objDocFilter.Filter_Type = Document_Filter_Types.Contains;
                intFilters = intFilters + objDocFilter.Filter.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
            }
            else
            {
                objDocFilter.Filter_Type = Document_Filter_Types.Not_Contain;
                intFilters = intFilters + objDocFilter.Filter.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
            }
            lstFilters.Add(objDocFilter);


            int Task_ID = Convert.ToInt32(ViewState["tid"]);

            Documents objDocs = ObjectCreator.GetDocuments(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            DS_Documents dsDocs;
            if (cboFolder.SelectedIndex == 0)
            {
                dsDocs = objDocs.ReadTaskDocuments(Task_ID);
            }
            else if (cboFolder.SelectedIndex == 1)
            {
                dsDocs = objDocs.ReadTaskDocuments(Task_ID, false);
            }
            else
            {
                dsDocs = objDocs.ReadTaskDocuments(Task_ID, true);
            }

            DS_Documents.tbldocument_tagsRow drTag;
            foreach (DS_Documents.tbldocumentsRow row in dsDocs.tbldocuments)
            {
                drTag = dsDocs.tbldocument_tags.Newtbldocument_tagsRow();
                drTag.Document_ID = row.Document_ID;
                drTag.Document_Project_Index_ID = -1;
                drTag.Tag_Type = 3;
                drTag.Tag_Value = row.Doc_Content;
                dsDocs.tbldocument_tags.Rows.Add(drTag);
            }

            DS_Documents.tbldocument_tagsRow[] drResults;
            decimal FilterValue = 100;
            if (intFilters > 0)
            {
                FilterValue = (decimal)100 / (decimal)intFilters;
            }

            Common_Actions objCom = new Common_Actions();
            foreach (Document_Filters flter in lstFilters)
            {
                if (flter.Filter_Type != Document_Filter_Types.None && flter.Filter.Trim() != "[Any]")
                {
                    if (flter.Tag_Type == ControlTypes.Yes_No_Dropdown || flter.Tag_Type == ControlTypes.Yes_No_Switch || flter.Tag_Type == ControlTypes.Selection)
                    {
                        if (flter.Filter_Type == Document_Filter_Types.Equals)
                        {
                            drResults = dsDocs.tbldocument_tags.Where(x => x.Document_Project_Index_ID == flter.Doc_Index && x.Tag_Value == flter.Filter).ToArray();
                        }
                        else
                        {
                            drResults = dsDocs.tbldocument_tags.Where(x => x.Document_Project_Index_ID == flter.Doc_Index && x.Tag_Value != flter.Filter).ToArray();
                        }
                        ReduceScores(ref dsDocs, drResults, FilterValue);
                    }
                    else if (flter.Tag_Type == ControlTypes.Number)
                    {
                        decimal dcmCompareWith;
                        if (decimal.TryParse(flter.Filter.Trim(), out dcmCompareWith))
                        {
                            if (flter.Filter_Type == Document_Filter_Types.Equals)
                            {
                                drResults = dsDocs.tbldocument_tags.Where(x => x.Document_Project_Index_ID == flter.Doc_Index && Convert.ToDecimal(x.Tag_Value) == dcmCompareWith).ToArray();
                            }
                            else if (flter.Filter_Type == Document_Filter_Types.Greater_Than)
                            {
                                drResults = dsDocs.tbldocument_tags.Where(x => x.Document_Project_Index_ID == flter.Doc_Index && Convert.ToDecimal(x.Tag_Value) > dcmCompareWith).ToArray();
                            }
                            else if (flter.Filter_Type == Document_Filter_Types.Less_Than)
                            {
                                drResults = dsDocs.tbldocument_tags.Where(x => x.Document_Project_Index_ID == flter.Doc_Index && Convert.ToDecimal(x.Tag_Value) < dcmCompareWith).ToArray();
                            }
                            else
                            {
                                drResults = dsDocs.tbldocument_tags.Where(x => x.Document_Project_Index_ID == flter.Doc_Index && Convert.ToDecimal(x.Tag_Value) != dcmCompareWith).ToArray();
                            }
                            ReduceScores(ref dsDocs, drResults, FilterValue);
                        }
                    }
                    else if (flter.Tag_Type == ControlTypes.Date)
                    {
                        DateTime dtCompareWith = new DateTime();
                        if (objCom.ValidateDate(flter.Filter.Trim(), ref dtCompareWith))
                        {
                            drResults = dsDocs.tbldocument_tags.Where(x => x.Document_Project_Index_ID == flter.Doc_Index && CompareStringDate(x.Tag_Value, dtCompareWith, flter.Filter_Type)).ToArray();
                            ReduceScores(ref dsDocs, drResults, FilterValue);
                        }
                    }
                    else if (flter.Tag_Type == ControlTypes.Time)
                    {
                        if (flter.Filter_Type == Document_Filter_Types.Equals)
                        {
                            drResults = dsDocs.tbldocument_tags.Where(x => x.Document_Project_Index_ID == flter.Doc_Index && Convert.ToDecimal(x.Tag_Value.Replace(":", ".")) == Convert.ToDecimal(flter.Filter.Replace(":", "."))).ToArray();
                        }
                        else if (flter.Filter_Type == Document_Filter_Types.Greater_Than)
                        {
                            drResults = dsDocs.tbldocument_tags.Where(x => x.Document_Project_Index_ID == flter.Doc_Index && Convert.ToDecimal(x.Tag_Value.Replace(":", ".")) > Convert.ToDecimal(flter.Filter.Replace(":", "."))).ToArray();
                        }
                        else
                        {
                            drResults = dsDocs.tbldocument_tags.Where(x => x.Document_Project_Index_ID == flter.Doc_Index && Convert.ToDecimal(x.Tag_Value.Replace(":", ".")) < Convert.ToDecimal(flter.Filter.Replace(":", "."))).ToArray();
                        }
                        ReduceScores(ref dsDocs, drResults, FilterValue);
                    }
                    else
                    {
                        if (flter.Filter_Type == Document_Filter_Types.Starts_With)
                        {
                            drResults = dsDocs.tbldocument_tags.Where(x => x.Document_Project_Index_ID == flter.Doc_Index && x.Tag_Value.Trim().StartsWith(flter.Filter.Trim(), StringComparison.OrdinalIgnoreCase)).ToArray();
                            ReduceScores(ref dsDocs, drResults, FilterValue);
                        }
                        else if (flter.Filter_Type == Document_Filter_Types.Ends_With)
                        {
                            drResults = dsDocs.tbldocument_tags.Where(x => x.Document_Project_Index_ID == flter.Doc_Index && x.Tag_Value.Trim().EndsWith(flter.Filter.Trim(), StringComparison.OrdinalIgnoreCase)).ToArray();
                            ReduceScores(ref dsDocs, drResults, FilterValue);
                        }
                        else if (flter.Filter_Type == Document_Filter_Types.Contains)
                        {
                            string[] words = flter.Filter.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string word in words)
                            {
                                drResults = dsDocs.tbldocument_tags.Where(x => x.Document_Project_Index_ID == flter.Doc_Index && x.Tag_Value.Trim().ToLower().Contains(word.Trim().ToLower())).ToArray();
                                ReduceScores(ref dsDocs, drResults, FilterValue);
                            }
                        }
                        else
                        {
                            string[] words = flter.Filter.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string word in words)
                            {
                                drResults = dsDocs.tbldocument_tags.Where(x => x.Document_Project_Index_ID == flter.Doc_Index && x.Tag_Value.Trim().ToLower().Contains(word.Trim().ToLower()) == false).ToArray();
                                ReduceScores(ref dsDocs, drResults, FilterValue);
                            }
                        }
                    }
                }
            }

            foreach (DS_Documents.tbldocumentsRow row in dsDocs.tbldocuments)
            {
                if (row.Matching_Percentage < 0.01M)
                {
                    row.Matching_Percentage_SP = "0%";
                }
                else
                {
                    row.Matching_Percentage_SP = Math.Round(row.Matching_Percentage, 2) + "%";
                }
            }

            gvDocs.DataSource = dsDocs.tbldocuments;
            gvDocs.DataBind();

            if (dsDocs.tbldocuments.Rows.Count > 0)
            {
                divNoDocs.Visible = false;
                divDocGrid.Visible = true;
                divResultsFooter.Visible = true;
                hndRemoveSelected_ModalPopupExtender.Enabled = true;
                hndRemoveUnselected_ModalPopupExtender.Enabled = true;

                gvDocs.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                divNoDocs.Visible = true;
                divDocGrid.Visible = false;
                divResultsFooter.Visible = false;
                hndRemoveSelected_ModalPopupExtender.Enabled = false;
                hndRemoveUnselected_ModalPopupExtender.Enabled = false;
            }
        }

        private bool CompareStringDate(string DateString1, DateTime Date2, Document_Filter_Types CompareType)
        {
            bool ret = false;
            Common_Actions objCom = new Common_Actions();
            DateTime dtMainValue = new DateTime();
            
            if (objCom.ValidateDate(DateString1.Trim(), ref dtMainValue))
            {
                if (CompareType == Document_Filter_Types.Equals)
                {
                    if (dtMainValue == Date2)
                    {
                        ret = true;
                    }
                }
                else if (CompareType == Document_Filter_Types.Greater_Than)
                {
                    if (dtMainValue > Date2)
                    {
                        ret = true;
                    }
                }
                else
                {
                    if (dtMainValue < Date2)
                    {
                        ret = true;
                    }
                }
            }
            return ret;
        }

        private void ReduceScores(ref DS_Documents ds, DS_Documents.tbldocument_tagsRow[] Results, decimal FilterValue)
        {
            foreach (DS_Documents.tbldocumentsRow row in ds.tbldocuments)
            {
                if (Results.Where(x => x.Document_ID == row.Document_ID).Count() <= 0)
                {
                    row.Matching_Percentage = row.Matching_Percentage - FilterValue;
                }
            }
        }

        protected void gvDocs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
            }

            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].HasControls())
                {
                    System.Web.UI.WebControls.CheckBox chkSelected = (System.Web.UI.WebControls.CheckBox)e.Row.Cells[1].Controls[1];
                    if (e.Row.Cells[5].Text.Trim() == "100%")
                    {
                        chkSelected.Checked = true;
                    }
                    chkSelected.Attributes.Add("data-id", e.Row.Cells[0].Text);
                    chkSelected.Attributes.Add("data-hnd", "hndSelectedDocs");

                }
                if (e.Row.Cells[6].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdView = (System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[6].Controls[1];
                    cmdView.Attributes.Add("onclick", "window.open('document_preview_dp.aspx?fid=" + objURL.Encrypt(e.Row.Cells[0].Text) + "', '_blank'); return false;");
                }
                if (e.Row.Cells[7].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdInfo = (System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[7].Controls[1];
                    cmdInfo.Attributes.Add("onclick", "return showAttachInfo(" + e.Row.Cells[0].Text + ");");
                }
            }
        }

        protected void cmdDetachSelectedYes_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            string[] Selected_IDs = hndSelectedDocs.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Tasks objTasks = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            bool AllRemoved = true;

            foreach (string doc in Selected_IDs)
            {
                if (objTasks.DeleteLink(Convert.ToInt32(doc), Task_ID).Deleted == false)
                {
                    AllRemoved = false;
                }
            }


            if (AllRemoved == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Document could not be Detached');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Documents Successfully Detached');", true);
                hndSelectedDocs.Value = "";
            }
            ProcessFilter(objSes);
        }

        protected void cmdDetachUnselectedYes_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            string[] Selected_IDs = hndSelectedDocs.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Tasks objTasks = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            bool AllRemoved = true;

            foreach (string doc in Selected_IDs)
            {
                if (objTasks.DeleteLink(Convert.ToInt32(doc), Task_ID).Deleted == false)
                {
                    AllRemoved = false;
                }
            }


            if (AllRemoved == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Document could not be Detached');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Documents Successfully Detached');", true);
                hndSelectedDocs.Value = "";
            }
            ProcessFilter(objSes);
        }

        protected void cmdStart_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            string[] Selected_IDs = hndSelectedDocs.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Tasks objTasks = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            bool AllRemoved = true;

            foreach (string doc in Selected_IDs)
            {
                if (cboProcessType.SelectedIndex == 1)
                {
                    if (objTasks.UpdateLinkFolder(Convert.ToInt32(doc), Task_ID, false) == false)
                    {
                        AllRemoved = false;
                    }
                }
                else if (cboProcessType.SelectedIndex == 2)
                {
                    if (objTasks.UpdateLinkFolder(Convert.ToInt32(doc), Task_ID, true) == false)
                    {
                        AllRemoved = false;
                    }
                }
            }


            if (AllRemoved == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Document could not be Moved');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Documents Successfully Moved');", true);
                hndSelectedDocs.Value = "";
            }
            ProcessFilter(objSes);
        }

        protected void cboFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            ProcessFilter(objSes);
        }
    }
}