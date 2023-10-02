using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter.Base;
using N_Ter.Common;

namespace N_Ter_Tasks
{
    public partial class data_export : System.Web.UI.Page
    {
        DS_Data_Extract dsExtract = new DS_Data_Extract();
        public string DateBetweenScript = "";
        public string Extraction_Script1 = "";
        public string Extraction_Script2 = "";
        public string DeleteFile_Script = "";
        public string DeleteFile_Script2 = "";

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
                    hndTemplateID.Value = "0";
                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    int WFID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["fid"])));
                    ViewState["fid"] = WFID;
                }
            }

            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow ds = objWF.Read(Convert.ToInt32(ViewState["fid"]));

            AddHeaderContols(ds, objSes);

            DS_Data_Extract.tblControlsRow drExtract;
            foreach (DS_Workflow.tblworkflow_step_fieldsRow row in ds.tblworkflow_step_fields.OrderBy(x => x.Sort_Order))
            {
                if (row.Field_Type != 0 && row.Field_Type != 11)
                {
                    drExtract = dsExtract.tblControls.NewtblControlsRow();
                    drExtract.Workflow_Step_Field_ID = row.Workflow_Step_Field_ID;
                    drExtract.Workflow_Step_ID = row.Workflow_Step_ID;
                    drExtract.Field_Name = row.Field_Name;
                    CheckBox chk = new CheckBox();
                    chk.ID = "chk_" + row.Workflow_Step_Field_ID;
                    chk.CssClass = "checkboxlist";
                    drExtract.Select_Control = chk;
                    if (row.Field_Type == 1 || row.Field_Type == 5 || row.Field_Type == 7 || row.Field_Type == 10 || row.Field_Type == 12)
                    {
                        drExtract.Operator_Control = GetOperator(row.Workflow_Step_Field_ID.ToString(), 1);
                    }
                    else if (row.Field_Type == 2 || row.Field_Type == 3)
                    {
                        drExtract.Operator_Control = GetOperator(row.Workflow_Step_Field_ID.ToString(), 2);
                    }
                    else if (row.Field_Type == 6)
                    {
                        drExtract.Operator_Control = GetOperator(row.Workflow_Step_Field_ID.ToString(), 4);
                        drExtract.Criteria_Control_2 = GetCriteria2(row.Workflow_Step_Field_ID.ToString());
                    }
                    else
                    {
                        drExtract.Operator_Control = GetOperator(row.Workflow_Step_Field_ID.ToString(), 3);
                    }
                    drExtract.Criteria_Control = GetFieldCriteria(row, objSes);
                    drExtract.Criteria_Control_Type = row.Field_Type;
                    dsExtract.tblControls.Rows.Add(drExtract);
                }
            }
            foreach (DS_Workflow.tblworkflow_stepsRow row in ds.tblworkflow_steps)
            {
                drExtract = dsExtract.tblControls.NewtblControlsRow();
                drExtract.Workflow_Step_Field_ID = -(((row.Workflow_Step_ID + 1) * 10) + 1);
                drExtract.Workflow_Step_ID = row.Workflow_Step_ID;
                drExtract.Field_Name = "Step Active Time";
                CheckBox chk = new CheckBox();
                chk.ID = "chk_m1_" + row.Workflow_Step_ID;
                chk.CssClass = "checkboxlist";
                drExtract.Select_Control = chk;
                drExtract.Operator_Control = GetOperator("m1_1_" + row.Workflow_Step_ID, 3);
                TextBox tx = (TextBox)GetHeaderCriteria(null, "m1_1_" + row.Workflow_Step_ID, 1, objSes);
                tx.Attributes.Add("placeholder", "HH:MM");
                tx.CssClass = tx.CssClass + " tmMask";
                drExtract.Criteria_Control = tx;
                drExtract.Criteria_Control_Type = -1;
                dsExtract.tblControls.Rows.Add(drExtract);

                drExtract = dsExtract.tblControls.NewtblControlsRow();
                drExtract.Workflow_Step_Field_ID = -(((row.Workflow_Step_ID + 1) * 10) + 2);
                drExtract.Workflow_Step_ID = row.Workflow_Step_ID;
                drExtract.Field_Name = "Step Inactive Time";
                CheckBox chk2 = new CheckBox();
                chk2.ID = "chk_m2_" + row.Workflow_Step_ID;
                chk2.CssClass = "checkboxlist";
                drExtract.Select_Control = chk2;
                drExtract.Operator_Control = GetOperator("m2_2_" + row.Workflow_Step_ID, 3);
                TextBox tx2 = (TextBox)GetHeaderCriteria(null, "m2_2_" + row.Workflow_Step_ID, 1, objSes);
                tx2.Attributes.Add("placeholder", "HH:MM");
                tx2.CssClass = tx2.CssClass + " tmMask";
                drExtract.Criteria_Control = tx2;
                drExtract.Criteria_Control_Type = -1;
                dsExtract.tblControls.Rows.Add(drExtract);

                drExtract = dsExtract.tblControls.NewtblControlsRow();
                drExtract.Workflow_Step_Field_ID = -(((row.Workflow_Step_ID + 1) * 10) + 4);
                drExtract.Workflow_Step_ID = row.Workflow_Step_ID;
                drExtract.Field_Name = "Step Time";
                CheckBox chk4 = new CheckBox();
                chk4.ID = "chk_m4_" + row.Workflow_Step_ID;
                chk4.CssClass = "checkboxlist";
                drExtract.Select_Control = chk4;
                drExtract.Operator_Control = GetOperator("m4_4_" + row.Workflow_Step_ID, 3);
                TextBox tx4 = (TextBox)GetHeaderCriteria(null, "m4_4_" + row.Workflow_Step_ID, 1, objSes);
                tx4.Attributes.Add("placeholder", "HH:MM");
                tx4.CssClass = tx4.CssClass + " tmMask";
                drExtract.Criteria_Control = tx4;
                drExtract.Criteria_Control_Type = -1;
                dsExtract.tblControls.Rows.Add(drExtract);

                drExtract = dsExtract.tblControls.NewtblControlsRow();
                drExtract.Workflow_Step_Field_ID = -(((row.Workflow_Step_ID + 1) * 10) + 3);
                drExtract.Workflow_Step_ID = row.Workflow_Step_ID;
                drExtract.Field_Name = "Step Posted By";
                CheckBox chk3 = new CheckBox();
                chk3.ID = "chk_m3_" + row.Workflow_Step_ID;
                chk3.CssClass = "checkboxlist";
                drExtract.Select_Control = chk3;
                drExtract.Operator_Control = GetOperator("m3_3_" + row.Workflow_Step_ID, 1);
                DropDownList drp = (DropDownList)GetHeaderCriteria(null, "m3_3_" + row.Workflow_Step_ID, 4, objSes);
                drExtract.Criteria_Control = drp;
                drExtract.Criteria_Control_Type = 5;
                dsExtract.tblControls.Rows.Add(drExtract);


                drExtract = dsExtract.tblControls.NewtblControlsRow();
                drExtract.Workflow_Step_Field_ID = -(((row.Workflow_Step_ID + 1) * 10) + 5);
                drExtract.Workflow_Step_ID = row.Workflow_Step_ID;
                drExtract.Field_Name = "Step Posted On";
                CheckBox chk5 = new CheckBox();
                chk5.ID = "chk_m5_" + row.Workflow_Step_ID;
                chk5.CssClass = "checkboxlist";
                drExtract.Select_Control = chk5;
                drExtract.Operator_Control = GetOperator("m5_5_" + row.Workflow_Step_ID, 4);
                drExtract.Criteria_Control = GetHeaderCriteria(null, "m5_5_" + row.Workflow_Step_ID, 2, objSes);
                drExtract.Criteria_Control_2 = GetCriteria2("m5_5_" + row.Workflow_Step_ID);
                drExtract.Criteria_Control_Type = 6;
                dsExtract.tblControls.Rows.Add(drExtract);
            }

            RefreshGrid(ds.tblworkflow_steps, objSes);
            cmdSaveConfirm.Attributes.Add("onClick", "return ValidateTemplateName('" + txtName.ClientID + "')");
            cmdSaveAsConfirm.Attributes.Add("onClick", "return ValidateTemplateName('" + txtNameSaveAs.ClientID + "')");

            LoadExistingExtracts(objSes, Convert.ToInt32(ViewState["fid"]));
        }

        private void LoadExistingExtracts(SessionObject objSes, int Workflow_ID)
        {
            DS_Settings dsSett = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type).Read();
            if (dsSett.tblsettings[0].Data_Extract_As_BG_Process)
            {
                Extraction_Script1 = "ShowExtractions();";
                Extraction_Script2 = "function ShowExtractions(){" +
                                        "$(\"#tblExtracts\").dataTable({" + "\r\n" +
                                        "                pageLength: 10," + "\r\n" +
                                        "                order: [[1, \"desc\"]]," + "\r\n" +
                                        "                responsive: true," + "\r\n" +
                                        "                autoWidth: true," + "\r\n" +
                                        "                processing: true," + "\r\n" +
                                        "                serverSide: true," + "\r\n" +
                                        "                ajax:" + "\r\n" +
                                        "                {" + "\r\n" +
                                        "                    url: \"api/tasks/GetDataExtracts\"," + "\r\n" +
                                        "                    contentType: \"application/json\"," + "\r\n" +
                                        "                    type: \"GET\"," + "\r\n" +
                                        "                    dataType: \"JSON\"," + "\r\n" +
                                        "                    data: function (data) {" + "\r\n" +
                                        "                        for (var i = 0, len = data.columns.length; i < len; i++) {" + "\r\n" +
                                        "                            delete data.columns[i].search;" + "\r\n" +
                                        "                            delete data.columns[i].searchable;" + "\r\n" +
                                        "                            delete data.columns[i].orderable;" + "\r\n" +
                                        "                            delete data.columns[i].name;" + "\r\n" +
                                        "                        }" + "\r\n" +
                                        "                        delete data.search.regex;" + "\r\n" +
                                        "                        data.w_id = \"" + Workflow_ID + "\";" + "\r\n" +
                                        "                    }" + "\r\n" +
                                        "                }," + "\r\n" +
                                        "                columns: [" + "\r\n" +
                                        "                    { data: \"Del_Button\" }," + "\r\n" +
                                        "                    { data: \"Extract_Date\" }," + "\r\n" +
                                        "                    { data: \"File_Path\" }," + "\r\n" +
                                        "                    { data: \"Template_Name\" }," + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                columnDefs: [" + "\r\n" +
                                        "                    { orderable: false, width:\"22px\", targets: 0 }" + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                \"fnDrawCallback\": function(oSettings, json) {\r\n" +
                                        "                     AdjustGridResp();\r\n" +
                                        "                }\r\n" +
                                        "            });" + "\r\n\r\n" +
                                        "        setInterval(function () {" + "\r\n" +
                                        "            $('#tblExtracts').DataTable().ajax.reload(null, false);" + "\r\n" +
                                        "        }, " + objSes.RefFreq + ");" + "\r\n\r\n" +
                                    "}";
                DeleteFile_Script = "function DeleteFile(f_id){\r\n" +
                                        "$('#" + hndDeleteFile.ClientID + "').val(f_id);\r\n" +
                                        "$find('mpuDeleteFile').show();\r\n" +
                                        "return false;\r\n" +
                                    "}";
                DeleteFile_Script2 = "AdjustPopupSize(80, 400, 'at_del_model');";
            }
            else
            {
                Extraction_Script1 = "";
                Extraction_Script2 = "";
                DeleteFile_Script = "";
                DeleteFile_Script2 = "";
                divExtracts.Visible = false;
                hndDeleteFile.Visible = false;
                hndDeleteFile_ModalPopupExtender.Enabled = false;
                pnlDeleteFile.Visible = false;
            }
        }

        private void AddHeaderContols(DS_Workflow dsWF, SessionObject objSes)
        {
            //-1:Created On 
            //-2:Task Number
            //-3:EL2
            //-4:Ex1
            //-5:Ex2
            //-6:Current_Step
            //-7:Task Owner
            //-8:Due Date
            //-9:Last Post Date
            //-10:Last Posted By

            DS_Workflow.tblwalkflowRow drWF = dsWF.tblwalkflow[0];

            DS_Data_Extract.tblControlsRow drExtract;
            for (int i = 1; i <= 17; i++)
            {
                drExtract = dsExtract.tblControls.NewtblControlsRow();
                drExtract.Workflow_Step_Field_ID = -i;
                drExtract.Workflow_Step_ID = 0;
                CheckBox chk = new CheckBox();
                chk.ID = "chk_m" + i;
                chk.CssClass = "checkboxlist";
                drExtract.Select_Control = chk;

                switch (i)
                {
                    case 1:
                        drExtract.Field_Name = "Task Queue";
                        drExtract.Operator_Control = GetOperator("m1", 1);
                        drExtract.Criteria_Control = GetHeaderCriteria(dsWF, "m1", 6, objSes);
                        drExtract.Criteria_Control_Type = 5;
                        dsExtract.tblControls.Rows.Add(drExtract);
                        break;
                    case 2:
                        drExtract.Field_Name = "Task Created On";
                        drExtract.Operator_Control = GetOperator("m2", 4);
                        drExtract.Criteria_Control = GetHeaderCriteria(dsWF, "m2", 2, objSes);
                        drExtract.Criteria_Control_2 = GetCriteria2("m2");
                        drExtract.Criteria_Control_Type = 6;
                        dsExtract.tblControls.Rows.Add(drExtract);
                        break;
                    case 3:
                        drExtract.Field_Name = "Task Number";
                        drExtract.Operator_Control = GetOperator("m3", 2);
                        drExtract.Criteria_Control = GetHeaderCriteria(dsWF, "m3", 1, objSes);
                        drExtract.Criteria_Control_Type = 2;
                        dsExtract.tblControls.Rows.Add(drExtract);
                        break;
                    case 4:
                        drExtract.Field_Name = objSes.EL2 + " Name";
                        drExtract.Operator_Control = GetOperator("m4", 1);
                        drExtract.Criteria_Control = GetHeaderCriteria(dsWF, "m4", 3, objSes);
                        drExtract.Criteria_Control_Type = 10;
                        dsExtract.tblControls.Rows.Add(drExtract);
                        break;
                    case 5:
                        if (drWF.Exrta_Field_Naming.Trim() != "")
                        {
                            drExtract.Field_Name = drWF.Exrta_Field_Naming;
                            drExtract.Operator_Control = GetOperator("m5", 2);
                            drExtract.Criteria_Control = GetHeaderCriteria(dsWF, "m5", 1, objSes);
                            drExtract.Criteria_Control_Type = 2;
                            dsExtract.tblControls.Rows.Add(drExtract);
                        }
                        break;
                    case 6:
                        if (drWF.Exrta_Field2_Naming.Trim() != "")
                        {
                            drExtract.Field_Name = drWF.Exrta_Field2_Naming;
                            drExtract.Operator_Control = GetOperator("m6", 2);
                            drExtract.Criteria_Control = GetHeaderCriteria(dsWF, "m6", 1, objSes);
                            drExtract.Criteria_Control_Type = 2;
                            dsExtract.tblControls.Rows.Add(drExtract);
                        }
                        break;
                    case 7:
                        drExtract.Field_Name = "Current Step";
                        drExtract.Operator_Control = GetOperator("m7", 1);
                        drExtract.Criteria_Control = GetHeaderCriteria(dsWF, "m7", 5, objSes);
                        drExtract.Criteria_Control_Type = 5;
                        dsExtract.tblControls.Rows.Add(drExtract);
                        break;
                    case 8:
                        drExtract.Field_Name = "Task Owner";
                        drExtract.Operator_Control = GetOperator("m8", 1);
                        drExtract.Criteria_Control = GetHeaderCriteria(dsWF, "m8", 4, objSes);
                        drExtract.Criteria_Control_Type = 5;
                        dsExtract.tblControls.Rows.Add(drExtract);
                        break;
                    case 9:
                        drExtract.Field_Name = "Due Date";
                        drExtract.Operator_Control = GetOperator("m9", 4);
                        drExtract.Criteria_Control = GetHeaderCriteria(dsWF, "m9", 2, objSes);
                        drExtract.Criteria_Control_2 = GetCriteria2("m9");
                        drExtract.Criteria_Control_Type = 6;
                        dsExtract.tblControls.Rows.Add(drExtract);
                        break;
                    case 10:
                        drExtract.Field_Name = "Last Posted Date";
                        drExtract.Operator_Control = GetOperator("m10", 4);
                        drExtract.Criteria_Control = GetHeaderCriteria(dsWF, "m10", 2, objSes);
                        drExtract.Criteria_Control_2 = GetCriteria2("m10");
                        drExtract.Criteria_Control_Type = 6;
                        dsExtract.tblControls.Rows.Add(drExtract);
                        break;
                    case 11:
                        drExtract.Field_Name = "Last Posted By";
                        drExtract.Operator_Control = GetOperator("m11", 1);
                        drExtract.Criteria_Control = GetHeaderCriteria(dsWF, "m11", 4, objSes);
                        drExtract.Criteria_Control_Type = 5;
                        dsExtract.tblControls.Rows.Add(drExtract);
                        break;
                    case 12:
                        drExtract.Field_Name = "Locked By";
                        drExtract.Operator_Control = GetOperator("m12", 1);
                        drExtract.Criteria_Control = GetHeaderCriteria(dsWF, "m12", 4, objSes);
                        drExtract.Criteria_Control_Type = 5;
                        dsExtract.tblControls.Rows.Add(drExtract);
                        break;
                    case 13:
                        drExtract.Field_Name = "Task Active Time";
                        drExtract.Operator_Control = GetOperator("m13", 3);
                        TextBox tx = (TextBox)GetHeaderCriteria(dsWF, "m13", 1, objSes);
                        tx.Attributes.Add("placeholder", "HH:MM");
                        tx.CssClass = tx.CssClass + " tmMask";
                        drExtract.Criteria_Control = tx;
                        drExtract.Criteria_Control_Type = -1;
                        dsExtract.tblControls.Rows.Add(drExtract);
                        break;
                    case 14:
                        drExtract.Field_Name = "Task Inactive Time";
                        drExtract.Operator_Control = GetOperator("m14", 3);
                        TextBox tx2 = (TextBox)GetHeaderCriteria(dsWF, "m14", 1, objSes);
                        tx2.Attributes.Add("placeholder", "HH:MM");
                        tx2.CssClass = tx2.CssClass + " tmMask";
                        drExtract.Criteria_Control = tx2;
                        drExtract.Criteria_Control_Type = -1;
                        dsExtract.tblControls.Rows.Add(drExtract);
                        break;
                    case 15:
                        drExtract.Field_Name = "Task Time";
                        drExtract.Operator_Control = GetOperator("m15", 3);
                        TextBox tx3 = (TextBox)GetHeaderCriteria(dsWF, "m15", 1, objSes);
                        tx3.Attributes.Add("placeholder", "HH:MM");
                        tx3.CssClass = tx3.CssClass + " tmMask";
                        drExtract.Criteria_Control = tx3;
                        drExtract.Criteria_Control_Type = -1;
                        dsExtract.tblControls.Rows.Add(drExtract);
                        break;
                    case 16:
                        drExtract.Field_Name = "Active";
                        drExtract.Operator_Control = GetOperator("m16", 1);
                        drExtract.Criteria_Control = GetHeaderCriteria(dsWF, "m16", 7, objSes);
                        drExtract.Criteria_Control_Type = 1;
                        dsExtract.tblControls.Rows.Add(drExtract);
                        break;
                    case 17:
                        drExtract.Field_Name = "Flagged";
                        drExtract.Operator_Control = GetOperator("m17", 1);
                        drExtract.Criteria_Control = GetHeaderCriteria(dsWF, "m17", 7, objSes);
                        drExtract.Criteria_Control_Type = 1;
                        dsExtract.tblControls.Rows.Add(drExtract);
                        break;
                }
            }
        }

        private DropDownList GetOperator(string Step_Field_ID, int FieldType)
        {
            DropDownList op = new DropDownList();
            op.ClientIDMode = ClientIDMode.Static;
            op.ID = "op_" + Step_Field_ID;
            op.CssClass = "form-control";
            switch (FieldType)
            {
                case 1:
                    op.Items.Add(new ListItem("[Any]", "0"));
                    op.Items.Add(new ListItem("Equals", "1"));
                    op.Items.Add(new ListItem("Not Equals", "2"));
                    break;
                case 2:
                    op.Items.Add(new ListItem("[Any]", "0"));
                    op.Items.Add(new ListItem("Contains", "3"));
                    op.Items.Add(new ListItem("Starts with", "4"));
                    op.Items.Add(new ListItem("Ends with", "5"));
                    op.Items.Add(new ListItem("Equals", "1"));
                    op.Items.Add(new ListItem("Not Equals", "2"));
                    break;
                case 3:
                    op.Items.Add(new ListItem("[Any]", "0"));
                    op.Items.Add(new ListItem("Equals", "1"));
                    op.Items.Add(new ListItem("Not Equal", "2"));
                    op.Items.Add(new ListItem("Greater Than", "6"));
                    op.Items.Add(new ListItem("Less Than", "7"));
                    break;
                case 4:
                    op.Items.Add(new ListItem("[Any]", "0"));
                    op.Items.Add(new ListItem("Equals", "1"));
                    op.Items.Add(new ListItem("Not Equal", "2"));
                    op.Items.Add(new ListItem("Greater Than", "6"));
                    op.Items.Add(new ListItem("Less Than", "7"));
                    op.Items.Add(new ListItem("Between", "8"));
                    op.Items.Add(new ListItem("Within Yesterday", "9"));
                    op.Items.Add(new ListItem("Within Last Day", "10"));
                    op.Items.Add(new ListItem("Within Last Week", "11"));
                    op.Items.Add(new ListItem("Within Last Month", "12"));
                    break;
            }
            return op;
        }

        private Control GetFieldCriteria(DS_Workflow.tblworkflow_step_fieldsRow row, SessionObject objSes)
        {
            switch (row.Field_Type)
            {
                case 2:
                    TextBox tx = new TextBox();
                    tx.ID = "cr_" + row.Workflow_Step_Field_ID;
                    tx.CssClass = "form-control";
                    return tx;
                case 3:
                    TextBox Mem = new TextBox();
                    Mem.ID = "cr_" + row.Workflow_Step_Field_ID;
                    Mem.CssClass = "form-control";
                    Mem.Height = 50;
                    Mem.TextMode = TextBoxMode.MultiLine;
                    return Mem;
                case 4:
                    TextBox Num = new TextBox();
                    Num.ID = "cr_" + row.Workflow_Step_Field_ID;
                    Num.CssClass = "form-control";
                    Num.TextMode = TextBoxMode.Number;
                    return Num;
                case 8:
                    TextBox Curr = new TextBox();
                    Curr.ID = "cr_" + row.Workflow_Step_Field_ID;
                    Curr.CssClass = "form-control";
                    Curr.TextMode = TextBoxMode.Number;
                    return Curr;
                case 9:
                    TextBox Per = new TextBox();
                    Per.ID = "cr_" + row.Workflow_Step_Field_ID;
                    Per.CssClass = "form-control";
                    Per.TextMode = TextBoxMode.Number;
                    return Per;
                case 6:
                    TextBox date = new TextBox();
                    date.ID = "cr_" + row.Workflow_Step_Field_ID;
                    date.CssClass = "form-control dtPicker";
                    return date;
                case 1:
                    DropDownList YesNo = new DropDownList();
                    YesNo.ID = "cr_" + row.Workflow_Step_Field_ID;
                    YesNo.CssClass = "form-control";
                    YesNo.Items.Add("[Any]");
                    YesNo.Items.Add("Yes");
                    YesNo.Items.Add("No");
                    return YesNo;
                case 13:
                    DropDownList YesNo2 = new DropDownList();
                    YesNo2.ID = "cr_" + row.Workflow_Step_Field_ID;
                    YesNo2.CssClass = "form-control";
                    YesNo2.Items.Add("[Any]");
                    YesNo2.Items.Add("Yes");
                    YesNo2.Items.Add("No");
                    return YesNo2;
                case 5:
                    DropDownList sele = new DropDownList();
                    sele.ID = "cr_" + row.Workflow_Step_Field_ID;
                    sele.CssClass = "form-control";
                    sele.Items.Add("[Any]");
                    int intIndex = 1;
                    foreach (string item in row.Selection_Texts.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        sele.Items.Add(item);
                        intIndex++;
                    }
                    return sele;
                case 7:
                    DropDownList time = new DropDownList();
                    time.ID = "cr_" + row.Workflow_Step_Field_ID;
                    time.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    time.CssClass = "form-control";
                    time.Items.Add("[Any]");
                    for (int i = 0; i < 24; i++)
                    {
                        time.Items.Add(Make2Digits(i.ToString()) + ":00");
                        time.Items.Add(Make2Digits(i.ToString()) + ":15");
                        time.Items.Add(Make2Digits(i.ToString()) + ":30");
                        time.Items.Add(Make2Digits(i.ToString()) + ":45");
                    }
                    return time;
                case 10:
                    DS_Master_Tables dsM = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type).GetMasterTable(row.Master_Table_ID);
                    DropDownList mas = new DropDownList();
                    mas.ID = "cr_" + row.Workflow_Step_Field_ID;
                    mas.CssClass = "form-control";
                    mas.Items.Add("[Any]");
                    int intIndex2 = 1;
                    foreach (DS_Master_Tables.tblDataRow item in dsM.tblData)
                    {
                        mas.Items.Add(item.Data);
                        intIndex2++;
                    }
                    return mas;
                case 12:
                    DropDownList timeSpan = new DropDownList();
                    timeSpan.ID = "cr_" + row.Workflow_Step_Field_ID;
                    timeSpan.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    timeSpan.CssClass = "form-control";
                    timeSpan.Items.Add("[Any]");
                    for (int i = 0; i < 24; i++)
                    {
                        timeSpan.Items.Add(Make2Digits(i.ToString()) + ":00");
                        timeSpan.Items.Add(Make2Digits(i.ToString()) + ":15");
                        timeSpan.Items.Add(Make2Digits(i.ToString()) + ":30");
                        timeSpan.Items.Add(Make2Digits(i.ToString()) + ":45");
                    }
                    return timeSpan;
                default:
                    return new DropDownList();
            }
        }

        private Control GetCriteria2(string Step_Field_ID)
        {
            TextBox date = new TextBox();
            date.ID = "cr_2_" + Step_Field_ID;
            date.CssClass = "form-control dtPicker";
            return date;
        }

        private Control GetHeaderCriteria(DS_Workflow dsWF, string Step_Field_ID, int HeaderType, SessionObject objSes)
        {
            switch (HeaderType)
            {
                case 1:
                    TextBox tx = new TextBox();
                    tx.ID = "cr_" + Step_Field_ID;
                    tx.CssClass = "form-control";
                    return tx;
                case 2:
                    TextBox date = new TextBox();
                    date.ID = "cr_" + Step_Field_ID;
                    date.CssClass = "form-control dtPicker";
                    return date;
                case 3:
                    DS_Entity_Level_2 dsE = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type).ReadForUser(objSes.UserID);
                    DropDownList el2 = new DropDownList();
                    el2.ID = "cr_" + Step_Field_ID;
                    el2.CssClass = "form-control";
                    el2.Items.Add("[Any]");
                    foreach (DS_Entity_Level_2.tblentity_level_2Row item in dsE.tblentity_level_2)
                    {
                        el2.Items.Add(new ListItem(item.Display_Name, item.Entity_L2_ID.ToString()));
                    }
                    return el2;
                case 4:
                    DS_Users dsU = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).ReadAllActive();
                    DropDownList user = new DropDownList();
                    user.ID = "cr_" + Step_Field_ID;
                    user.CssClass = "form-control";
                    user.Items.Add("[Any]");
                    foreach (DS_Users.tblusersRow item in dsU.tblusers)
                    {
                        user.Items.Add(new ListItem(item.Full_Name, item.User_ID.ToString()));
                    }
                    return user;
                case 5:
                    DropDownList steps = new DropDownList();
                    steps.ID = "cr_" + Step_Field_ID;
                    steps.CssClass = "form-control";
                    steps.Items.Add("[Any]");
                    foreach (DS_Workflow.tblworkflow_stepsRow item in dsWF.tblworkflow_steps)
                    {
                        steps.Items.Add(new ListItem(item.Step_Status, item.Workflow_Step_ID.ToString()));
                    }
                    steps.Items.Add(new ListItem("[Completed]", "-1"));
                    return steps;
                case 6:
                    DropDownList queue = new DropDownList();
                    queue.ID = "cr_" + Step_Field_ID;
                    queue.CssClass = "form-control";
                    queue.Items.Add("[Any]");
                    DS_Task_Queues dsQ = ObjectCreator.GetTask_Queues(objSes.Connection, objSes.DB_Type).ReadAll();
                    foreach (DS_Task_Queues.tbltask_queuesRow item in dsQ.tbltask_queues)
                    {
                        queue.Items.Add(new ListItem(item.Queue_Name, item.Queue_ID.ToString()));
                    }
                    return queue;
                case 7:
                    DropDownList drpBool = new DropDownList();
                    drpBool.ID = "cr_" + Step_Field_ID;
                    drpBool.CssClass = "form-control";
                    drpBool.Items.Add("[Any]");
                    drpBool.Items.Add("Yes");
                    drpBool.Items.Add("No");
                    return drpBool;
                default:
                    return new DropDownList();
            }
        }

        private string Make2Digits(string inVal)
        {
            if (inVal.Length < 2)
            {
                while (!(inVal.Length == 2))
                {
                    inVal = "0" + inVal;
                }
            }
            return inVal;
        }

        private void RefreshGrid(DS_Workflow.tblworkflow_stepsDataTable tbl, SessionObject objSes)
        {
            DateBetweenScript = "";

            divContent.Controls.Clear();

            ltrSteps.Text = "";

            ltrSteps.Text = ltrSteps.Text + "<li class=\"active-item group w_steps\">" + "\r\n" +
                                                        "<a href=\"#\" onclick=\"return showStep(0, 'Header', this);\">" + "\r\n" +
                                                            "<span class=\"text-bold stp_no\">Header</span><br />" + "\r\n" +
                                                            "<span class=\"text-sm\">Main Task Info</span>" + "\r\n" +
                                                        "</a>" + "\r\n" +
                                                    "</li>" + "\r\n";
            HtmlGenericControl divControl = new HtmlGenericControl("div");
            divControl.ID = "step_0";
            divControl.ClientIDMode = ClientIDMode.Static;
            divControl.Attributes.Add("class", "stepControls");
            foreach (DS_Data_Extract.tblControlsRow drExtract in dsExtract.tblControls.Where(x => x.Workflow_Step_ID == 0))
            {
                divControl.Controls.Add(GetControl(drExtract));
            }
            divContent.Controls.Add(divControl);

            int intIndex = 1;
            foreach (DS_Workflow.tblworkflow_stepsRow row in tbl)
            {
                ltrSteps.Text = ltrSteps.Text + "<li class='group w_steps'>" + "\r\n" +
                                                        "<a href=\"#\" onclick=\"return showStep(" + intIndex + ", '" + row.Step_Status.Replace("'", "\\'") + "', this);\">" + "\r\n" +
                                                            "<span class=\"text-bold stp_no\">Step " + row.Sort_order + "</span><br />" + "\r\n" +
                                                            "<span class=\"text-sm\">" + row.Step_Status + "</span>" + "\r\n" +
                                                        "</a>" + "\r\n" +
                                                    "</li>" + "\r\n";
                HtmlGenericControl divInnerControl = new HtmlGenericControl("div");
                divInnerControl.ID = "step_" + intIndex;
                divInnerControl.ClientIDMode = ClientIDMode.Static;
                divInnerControl.Attributes.Add("class", "stepControls hide");
                foreach (DS_Data_Extract.tblControlsRow drExtract in dsExtract.tblControls.Where(x => x.Workflow_Step_ID == row.Workflow_Step_ID))
                {
                    divInnerControl.Controls.Add(GetControl(drExtract));
                }
                divContent.Controls.Add(divInnerControl);

                intIndex++;
            }

            pnlSteps.Attributes.Remove("style");
            pnlSteps.Attributes.Add("style", "min-height: " + ((tbl.Rows.Count + 1) * 65) + "px");

            if (hndTemplateID.Value == "0")
            {
                cmdSaveExisting.Visible = false;
                cmdSaveAs.Visible = false;
                cmdSaveNew.Visible = true;
            }
            else
            {
                DS_Data_Extract dsT = ObjectCreator.GetData_Extract_Templates(objSes.Connection, objSes.DB_Type).Read(Convert.ToInt32(hndTemplateID.Value));
                if (dsT.tbldata_extract_templates[0].Created_By == objSes.UserID)
                {
                    cmdSaveExisting.Visible = true;
                    hndSave_ModalPopupExtender.Enabled = true;
                    pnlNewTemplate.Visible = true;
                }
                else
                {
                    cmdSaveExisting.Visible = false;
                    hndSave_ModalPopupExtender.Enabled = false;
                    pnlNewTemplate.Visible = false;
                }
                cmdSaveAs.Visible = true;
                cmdSaveNew.Visible = false;
            }
            LoadTemplates(objSes);
        }

        private void LoadTemplates(SessionObject objSes)
        {
            Data_Extract_Templates objTemp = ObjectCreator.GetData_Extract_Templates(objSes.Connection, objSes.DB_Type);
            DS_Data_Extract ds = objTemp.ReadAll(Convert.ToInt32(ViewState["fid"]), objSes.UserID);
            gvTemplates.DataSource = ds.tbldata_extract_templates;
            gvTemplates.DataBind();

            if (ds.tbldata_extract_templates.Rows.Count > 0)
            {
                gvTemplates.HeaderRow.TableSection = TableRowSection.TableHeader;
                cmdLoad.Visible = true;
            }
            else
            {
                cmdLoad.Visible = false;
            }
        }

        protected void gvTemplates_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].HasControls())
                {
                    HtmlButton cmdEdit = (HtmlButton)e.Row.Cells[1].Controls[1];
                    cmdEdit.Attributes.Add("onclick", "return LoadTemplate(" + e.Row.Cells[0].Text + ")");
                }
            }
        }

        private HtmlGenericControl GetControl(DS_Data_Extract.tblControlsRow row)
        {
            HtmlGenericControl divMainRow = new HtmlGenericControl("div");
            divMainRow.Attributes.Add("class", "row border-b p8");

            HtmlGenericControl divCol1 = new HtmlGenericControl("div");
            divCol1.Attributes.Add("class", "col-md-1 col-sm-2");
            divCol1.Controls.Add((Control)row.Select_Control);
            divMainRow.Controls.Add(divCol1);

            HtmlGenericControl divCol2 = new HtmlGenericControl("div");
            divCol2.Attributes.Add("class", "col-md-5 col-sm-10");
            divCol2.InnerHtml = row.Field_Name;
            divMainRow.Controls.Add(divCol2);

            HtmlGenericControl divCol3 = new HtmlGenericControl("div");
            divCol3.Attributes.Add("class", "col-md-2 col-sm-4");
            divCol3.Controls.Add((Control)row.Operator_Control);
            divMainRow.Controls.Add(divCol3);

            if (row.Criteria_Control_Type != 6)
            {
                HtmlGenericControl divCol4 = new HtmlGenericControl("div");
                divCol4.Attributes.Add("class", "col-md-4 col-sm-8");
                divCol4.Controls.Add((Control)row.Criteria_Control);
                divMainRow.Controls.Add(divCol4);
            }
            else
            {
                DropDownList drp = (DropDownList)row.Operator_Control;
                drp.Attributes.Add("onChange", "DateBetween();");
                DateBetweenScript = DateBetweenScript + "if ($('#" + drp.ID + "').val() == '8') {\r\n" +
                                                            "$('#divDateTo" + drp.ID + "').removeClass('hide');\r\n" +
                                                            "$('#divDateFrom" + drp.ID + "').removeClass('hide');\r\n" +
                                                            "$('#divDateFrom" + drp.ID + "').removeClass('col-md-4');\r\n" +
                                                            "$('#divDateFrom" + drp.ID + "').removeClass('col-sm-8');\r\n" +
                                                            "$('#divDateFrom" + drp.ID + "').addClass('col-md-2');\r\n" +
                                                            "$('#divDateFrom" + drp.ID + "').addClass('col-sm-4');\r\n" +
                                                        "}\r\n" +
                                                        "else if ($('#" + drp.ID + "').val() == '9' || $('#" + drp.ID + "').val() == '10' || $('#" + drp.ID + "').val() == '11' || $('#" + drp.ID + "').val() == '12') {\r\n" +
                                                            "$('#divDateTo" + drp.ID + "').addClass('hide');\r\n" +
                                                            "$('#divDateFrom" + drp.ID + "').addClass('hide');\r\n" +
                                                        "}\r\n" +
                                                        "else {\r\n" +
                                                            "$('#divDateTo" + drp.ID + "').addClass('hide');\r\n" +
                                                            "$('#divDateFrom" + drp.ID + "').removeClass('hide');\r\n" +
                                                            "$('#divDateFrom" + drp.ID + "').removeClass('col-md-2');\r\n" +
                                                            "$('#divDateFrom" + drp.ID + "').removeClass('col-sm-4');\r\n" +
                                                            "$('#divDateFrom" + drp.ID + "').addClass('col-md-4');\r\n" +
                                                            "$('#divDateFrom" + drp.ID + "').addClass('col-sm-8');\r\n" +
                                                        "}\r\n";
                HtmlGenericControl divCol4 = new HtmlGenericControl("div");
                divCol4.ClientIDMode = ClientIDMode.Static;
                divCol4.ID = "divDateFrom" + drp.ID;
                divCol4.Attributes.Add("class", "col-md-2 col-sm-4");
                divCol4.Controls.Add((Control)row.Criteria_Control);
                divMainRow.Controls.Add(divCol4);

                HtmlGenericControl divCol5 = new HtmlGenericControl("div");
                divCol5.ClientIDMode = ClientIDMode.Static;
                divCol5.ID = "divDateTo" + drp.ID;
                divCol5.Attributes.Add("class", "col-md-2 col-sm-4");
                divCol5.Controls.Add((Control)row.Criteria_Control_2);
                divMainRow.Controls.Add(divCol5);
            }

            return divMainRow;
        }

        protected void cmdSaveConfirm_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Data_Extract_Templates objTemp = ObjectCreator.GetData_Extract_Templates(objSes.Connection, objSes.DB_Type);

            if (hndTemplateID.Value == "0")
            {
                int Template_ID = objTemp.Insert(txtName.Text, Convert.ToInt32(ViewState["fid"]), chkIsPrivate.Checked, objSes.UserID, GetCriteriaData());
                if (Template_ID > 0)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Template Successfully Added');", true);
                    hndTemplateID.Value = Template_ID.ToString();
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Template could not be saved');", true);
                }
            }
            else
            {
                if (objTemp.Update(Convert.ToInt32(hndTemplateID.Value), txtName.Text, chkIsPrivate.Checked, GetCriteriaData()))
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Template Successfully Updated');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Template could not be Updated');", true);
                }
            }

            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow dsWF = objWF.Read(Convert.ToInt32(ViewState["fid"]));
            RefreshGrid(dsWF.tblworkflow_steps, objSes);
        }

        protected void cmdSaveAsConfirm_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Data_Extract_Templates objTemp = ObjectCreator.GetData_Extract_Templates(objSes.Connection, objSes.DB_Type);

            int Template_ID = objTemp.Insert(txtNameSaveAs.Text, Convert.ToInt32(ViewState["fid"]), chkisPrivateSaveAs.Checked, objSes.UserID, GetCriteriaData());
            if (Template_ID > 0)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Template Successfully Added');", true);
                txtName.Text = txtNameSaveAs.Text;
                chkIsPrivate.Checked = chkisPrivateSaveAs.Checked;
                txtNameSaveAs.Text = "";
                chkisPrivateSaveAs.Checked = false;
                hndTemplateID.Value = Template_ID.ToString();
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Template could not be saved');", true);
            }

            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow dsWF = objWF.Read(Convert.ToInt32(ViewState["fid"]));
            RefreshGrid(dsWF.tblworkflow_steps, objSes);
        }

        private DS_Data_Extract.tbldata_extract_templates_subDataTable GetCriteriaData()
        {
            DS_Data_Extract ds = new DS_Data_Extract();
            DS_Data_Extract.tbldata_extract_templates_subRow dr;
            int ControlType;
            foreach (DS_Data_Extract.tblControlsRow row in dsExtract.tblControls)
            {
                dr = ds.tbldata_extract_templates_sub.Newtbldata_extract_templates_subRow();
                CheckBox chk = (CheckBox)row.Select_Control;
                dr.isSelected = chk.Checked;
                dr.Workflow_Step_Field_ID = row.Workflow_Step_Field_ID;
                DropDownList drp = (DropDownList)row.Operator_Control;
                dr.Operator_ID = Convert.ToInt32(drp.SelectedItem.Value);
                ControlType = row.Criteria_Control_Type;
                if (ControlType == 2 || ControlType == 3 || ControlType == 4 || ControlType == 8 || ControlType == 9 || ControlType == 6 || ControlType == -1)
                {
                    TextBox txt = (TextBox)row.Criteria_Control;
                    dr.Criteria_Text = txt.Text;
                    if (ControlType == 6)
                    {
                        TextBox txt2 = (TextBox)row.Criteria_Control_2;
                        dr.Criteria_Text_2 = txt2.Text;
                    }
                    else
                    {
                        dr.Criteria_Text_2 = "";
                    }
                }
                else
                {
                    DropDownList txt = (DropDownList)row.Criteria_Control;
                    dr.Criteria_Text = txt.SelectedItem.Text;
                    dr.Criteria_ID = txt.SelectedItem.Value;
                    dr.Criteria_Text_2 = "";
                }
                ds.tbldata_extract_templates_sub.Rows.Add(dr);
            }
            return ds.tbldata_extract_templates_sub;
        }

        private DS_Data_Extract.tbldata_extract_templates_subDataTable GetSelectedCriteriaData()
        {
            DS_Data_Extract ds = new DS_Data_Extract();
            DS_Data_Extract.tbldata_extract_templates_subRow dr;
            int ControlType;
            foreach (DS_Data_Extract.tblControlsRow row in dsExtract.tblControls)
            {
                CheckBox chk = (CheckBox)row.Select_Control;
                if (chk.Checked == true)
                {
                    dr = ds.tbldata_extract_templates_sub.Newtbldata_extract_templates_subRow();
                    dr.isSelected = chk.Checked;
                    dr.Workflow_Step_Field_ID = row.Workflow_Step_Field_ID;
                    dr.Workflow_Step_ID = row.Workflow_Step_ID;
                    DropDownList drp = (DropDownList)row.Operator_Control;
                    dr.Operator_ID = Convert.ToInt32(drp.SelectedItem.Value);
                    ControlType = row.Criteria_Control_Type;
                    if (ControlType == 2 || ControlType == 3 || ControlType == 4 || ControlType == 8 || ControlType == 9 || ControlType == 6 || ControlType == -1)
                    {
                        TextBox txt = (TextBox)row.Criteria_Control;
                        dr.Criteria_Text = txt.Text;
                        if (ControlType == 6)
                        {
                            TextBox txt2 = (TextBox)row.Criteria_Control_2;
                            dr.Criteria_Text_2 = txt2.Text;
                        }
                        else
                        {
                            dr.Criteria_Text_2 = "";
                        }
                    }
                    else
                    {
                        DropDownList txt = (DropDownList)row.Criteria_Control;
                        dr.Criteria_Text = txt.SelectedItem.Text;
                        dr.Criteria_ID = txt.SelectedItem.Value;
                        dr.Criteria_Text_2 = "";
                    }
                    ds.tbldata_extract_templates_sub.Rows.Add(dr);
                }
            }
            return ds.tbldata_extract_templates_sub;
        }

        protected void cmdLoadTemplate_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            Data_Extract_Templates objTemp = ObjectCreator.GetData_Extract_Templates(objSes.Connection, objSes.DB_Type);
            DS_Data_Extract dsT = objTemp.Read(Convert.ToInt32(hndTemplateID.Value));
            txtName.Text = dsT.tbldata_extract_templates[0].Template_Name;
            chkIsPrivate.Checked = dsT.tbldata_extract_templates[0].isPrivate;
            txtNameSaveAs.Text = "";
            chkisPrivateSaveAs.Checked = false;

            List<DS_Data_Extract.tbldata_extract_templates_subRow> drField;
            int ControlType;
            foreach (DS_Data_Extract.tblControlsRow row in dsExtract.tblControls)
            {
                drField = dsT.tbldata_extract_templates_sub.Where(x => x.Workflow_Step_Field_ID == row.Workflow_Step_Field_ID).ToList();
                if (drField.Count > 0)
                {
                    CheckBox chk = (CheckBox)row.Select_Control;
                    chk.Checked = drField[0].isSelected;
                    DropDownList drp = (DropDownList)row.Operator_Control;
                    drp.SelectedValue = Convert.ToString(drField[0].Operator_ID);
                    ControlType = row.Criteria_Control_Type;
                    if (ControlType == 2 || ControlType == 3 || ControlType == 4 || ControlType == 8 || ControlType == 9 || ControlType == 6 || ControlType == -1)
                    {
                        TextBox txt = (TextBox)row.Criteria_Control;
                        if (ControlType == -1)
                        {
                            txt.Attributes.Add("placeholder", "HH:MM");
                        }
                        else if (ControlType == 6)
                        {
                            TextBox txt2 = (TextBox)row.Criteria_Control_2;
                            txt2.Text = drField[0].Criteria_Text_2;
                        }
                        txt.Text = drField[0].Criteria_Text;
                    }
                    else
                    {
                        DropDownList txt = (DropDownList)row.Criteria_Control;
                        for (int i = 0; i < txt.Items.Count; i++)
                        {
                            if (txt.Items[i].Text.Trim() == drField[0].Criteria_Text.Trim())
                            {
                                txt.SelectedIndex = i;
                            }
                        }
                    }
                }
            }

            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow dsWF = objWF.Read(Convert.ToInt32(ViewState["fid"]));
            RefreshGrid(dsWF.tblworkflow_steps, objSes);
        }

        protected void cmdExtract_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            DS_Data_Extract.tbldata_extract_templates_subDataTable tblCriteria = GetSelectedCriteriaData();
            if (tblCriteria.Rows.Count <= 40)
            {
                DS_Settings dsSett = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type).Read();
                if (dsSett.tblsettings[0].Data_Extract_As_BG_Process)
                {
                    System.Threading.Thread objThread1 = new System.Threading.Thread(delegate () { ExtractBGProcess(objSes, tblCriteria); });
                    objThread1.Priority = System.Threading.ThreadPriority.BelowNormal;
                    objThread1.Start();

                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Data Extraction Initiated');", true);
                }
                else
                {
                    DirectExtract(objSes, tblCriteria);
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('You have Selected more than Maximum Number of Elements Allowed');", true);
            }
        }

        private void DirectExtract(SessionObject objSes, DS_Data_Extract.tbldata_extract_templates_subDataTable tblCriteria)
        {
            Data_Extract_Templates objTemp = ObjectCreator.GetData_Extract_Templates(objSes.Connection, objSes.DB_Type);
            bool isAdmin = false;
            if (objSes.isAdmin == 1)
            {
                isAdmin = true;
            }
            System.Data.DataSet ds = objTemp.ExtractData(Convert.ToInt32(ViewState["fid"]), tblCriteria, objSes.UserID, objSes.EL2, isAdmin);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string FileName = "Data_Extraxt_" + N_Ter.Common.Common_Actions.ShortTimeStampForFileName() + Convert.ToString(objSes.UserID) + ".xlsx";
                    string PhysicalPath = objSes.PhysicalRoot + "\\nter_app_uploads\\temp_email_attachments\\" + FileName;

                    System.IO.FileInfo FilePath = new System.IO.FileInfo(PhysicalPath);

                    OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    OfficeOpenXml.ExcelPackage objExcel = new OfficeOpenXml.ExcelPackage(FilePath);
                    OfficeOpenXml.ExcelWorkbook objWorkBook = objExcel.Workbook;
                    OfficeOpenXml.ExcelWorksheet objSheet = objWorkBook.Worksheets.Add("Data Extract");

                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        objSheet.Cells[1, i + 1].Value = ds.Tables[0].Columns[i].ColumnName;
                        for (int ii = 0; ii < ds.Tables[0].Rows.Count; ii++)
                        {
                            objSheet.Cells[ii + 2, i + 1].Value = ds.Tables[0].Rows[ii][i];
                            if (ds.Tables[0].Columns[i].DataType.Name == "DateTime")
                            {
                                objSheet.Cells[ii + 2, i + 1].Style.Numberformat.Format = Constants.DateFormatJava + " hh:mm";
                            }
                        }
                    }

                    objExcel.Save();

                    Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
                    Response.ContentType = "application/octet-stream";
                    HttpContext.Current.ApplicationInstance.CompleteRequest();

                    Response.WriteFile(PhysicalPath);
                    Response.End();
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Empty Data Set');", true);
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Empty Data Set');", true);
            }
        }

        private void ExtractBGProcess(SessionObject objSes, DS_Data_Extract.tbldata_extract_templates_subDataTable tblCriteria)
        {
            Data_Extract_Templates objTemp = ObjectCreator.GetData_Extract_Templates(objSes.Connection, objSes.DB_Type);
            bool isAdmin = false;
            if (objSes.isAdmin == 1)
            {
                isAdmin = true;
            }
            System.Data.DataSet ds = objTemp.ExtractData(Convert.ToInt32(ViewState["fid"]), tblCriteria, objSes.UserID, objSes.EL2, isAdmin);

            int Template_ID = 0;
            if (hndTemplateID.Value.Trim() != "" && Convert.ToInt32(hndTemplateID.Value) > 0)
            {
                Template_ID = Convert.ToInt32(hndTemplateID.Value);
            }

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string PhysicalPath = "nter_app_uploads\\profile_docs\\profile_" + objSes.UserID;
                    if (System.IO.Directory.Exists(objSes.PhysicalRoot + "\\" + PhysicalPath) == false)
                    {
                        System.IO.Directory.CreateDirectory(objSes.PhysicalRoot + "\\" + PhysicalPath);
                    }

                    string FileName = "Data_Extraxt_" + N_Ter.Common.Common_Actions.ShortTimeStampForFileName() + Convert.ToString(objSes.UserID) + ".xlsx";
                    PhysicalPath = PhysicalPath + "\\" + FileName;

                    System.IO.FileInfo FilePath = new System.IO.FileInfo(objSes.PhysicalRoot + "\\" + PhysicalPath);

                    OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    OfficeOpenXml.ExcelPackage objExcel = new OfficeOpenXml.ExcelPackage(FilePath);
                    OfficeOpenXml.ExcelWorkbook objWorkBook = objExcel.Workbook;
                    OfficeOpenXml.ExcelWorksheet objSheet = objWorkBook.Worksheets.Add("Data Extract");

                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        objSheet.Cells[1, i + 1].Value = ds.Tables[0].Columns[i].ColumnName;
                        for (int ii = 0; ii < ds.Tables[0].Rows.Count; ii++)
                        {
                            objSheet.Cells[ii + 2, i + 1].Value = ds.Tables[0].Rows[ii][i];
                            if (ds.Tables[0].Columns[i].DataType.Name == "DateTime")
                            {
                                objSheet.Cells[ii + 2, i + 1].Style.Numberformat.Format = Constants.DateFormatJava + " hh:mm";
                            }
                        }
                    }

                    objExcel.Save();

                    objTemp.InsertExtract(Template_ID, Convert.ToInt32(ViewState["fid"]), objSes.UserID, PhysicalPath, DateTime.Now, true);
                }
                else
                {
                    objTemp.InsertExtract(Template_ID, Convert.ToInt32(ViewState["fid"]), objSes.UserID, "Extraction Failed : Empty Data Set", DateTime.Now, false);
                }
            }
            else
            {
                objTemp.InsertExtract(Template_ID, Convert.ToInt32(ViewState["fid"]), objSes.UserID, "Extraction Failed : Empty Data Set", DateTime.Now, false);
            }
        }

        protected void cmdDeleteFile_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Data_Extract_Templates objTemp = ObjectCreator.GetData_Extract_Templates(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objTemp.DeleteExtract(Convert.ToInt32(hndDeleteFile.Value), objSes.PhysicalRoot);
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('File Successfully Deleted');", true);
                hndDeleteFile.Value = "0";
            }
        }
    }
}