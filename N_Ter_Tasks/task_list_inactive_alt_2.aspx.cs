using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class task_list_inactive_alt_2 : System.Web.UI.Page
    {
        private string TableScript;

        protected void Page_Init(object sender, System.EventArgs e)
        {
            n_ter_base_loggedin_grid_task_list_t2 objT2 = (n_ter_base_loggedin_grid_task_list_t2)Master;
            objT2.LoadActive = false;
            objT2.FillTasks += ObjT2_FillTasks;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void ObjT2_FillTasks(object sender, EventArgs e)
        {
            TaskListArgs objArgs = (TaskListArgs)e;

            SessionObject objSes = (SessionObject)Session["dt"];
            DS_Workflow dsCats = ObjectCreator.GetWorkflow_Categories(objSes.Connection, objSes.DB_Type).ReadAll();
            List<DS_Tasks.tbltasksRow> drTasksInWF;

            int TabIndex = 1;
            string TabCSS = "";

            ltrTabs.Text = "";
            TableScript = "";

            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow dsWF = objWF.ReadAllForCat(objArgs.Active_WF_Category);

            foreach (DS_Workflow.tblwalkflowRow rowWF in dsWF.tblwalkflow)
            {
                drTasksInWF = objArgs.SelectedTasks.Where(x => x.Walkflow_ID == rowWF.Walkflow_ID).ToList();
                if (drTasksInWF.Count > 0)
                {
                    if ((objArgs.Active_WF == 0 && TabIndex == 1) || (objArgs.Active_WF == rowWF.Walkflow_ID))
                    {
                        TabCSS = " class='active'";
                    }
                    else
                    {
                        TabCSS = "";
                    }
                    ltrTabs.Text = ltrTabs.Text + "<li" + TabCSS + ">" + "\r\n" +
                                       "<a data-toggle='tab' href='#lft_tab_" + TabIndex + "'>" + rowWF.Workflow_Name + " <span id='wf_cnt_" + rowWF.Walkflow_ID + "' class='label label-info'>" + Convert.ToString(drTasksInWF.Count) + "</span></a>" + "\r\n" +
                                   "</li>" + "\r\n";
                    DrawTabs(drTasksInWF, TabIndex, objArgs.Queue_ID, rowWF.Walkflow_ID, objArgs.Active_WF, rowWF.Exrta_Field_Naming, rowWF.Exrta_Field2_Naming, objSes);
                    TabIndex = TabIndex + 1;
                }
            }

            n_ter_base_loggedin_grid_task_list_t2 objT2 = (n_ter_base_loggedin_grid_task_list_t2)Master;
            objT2.Tables = TableScript;
            objT2.TabCountScript = (TabIndex - 1).ToString();
        }

        private void DrawTabs(List<DS_Tasks.tbltasksRow> drTasks, int TabIndex, int Queue_ID, int Walkflow_ID, int Req_Workflow_ID, string Extra1Name, string Extra2Name, SessionObject objSes)
        {
            if (drTasks.Count > 0)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divTabMain = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divTabMain.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                if ((Req_Workflow_ID == 0 && TabIndex == 1) || (Req_Workflow_ID == Walkflow_ID))
                {
                    divTabMain.Attributes.Add("class", "tab-pane fade active in");
                }
                else
                {
                    divTabMain.Attributes.Add("class", "tab-pane fade");
                }
                divTabMain.ID = "lft_tab_" + TabIndex;
                System.Web.UI.HtmlControls.HtmlGenericControl divTabInner = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divTabInner.Attributes.Add("class", "table-responsive table-primary no-margin-b");
                divTabInner.ID = "tabInner_" + TabIndex;
                if (objSes.Task_List_View)
                {
                    divTabInner.InnerHtml = "<table id=\"task_table_" + TabIndex + "\" class=\"table table-striped table-hover grid_table check_arrange grid_test color-grid full_width_table\" data-size=\"full_width_table\">" + "\r\n" +
                                                "<thead>" + "\r\n" +
                                                    "<tr>" + "\r\n" +
                                                        "<th></th>" + "\r\n" +
                                                        "<th>Task Number</th>" + "\r\n" +
                                                        "<th>Created On</th>" + "\r\n" +
                                                        "<th>" + objSes.EL2 + " Name</th>" + "\r\n" +
                                                        "<th>" + Extra1Name + "</th>" + "\r\n" +
                                                        "<th>" + Extra2Name + "</th>" + "\r\n" +
                                                        "<th>Current Step</th>" + "\r\n" +
                                                        "<th>Task Owner</th>" + "\r\n" +
                                                        "<th>Due On</th>" + "\r\n" +
                                                        "<th>Posted On</th>" + "\r\n" +
                                                        "<th>Posted By</th>" + "\r\n" +
                                                        "<th>" + objSes.EL1 + "</th>" + "\r\n" +
                                                        "<th></th>" + "\r\n" +
                                                    "</tr>" + "\r\n" +
                                                "</thead>" + "\r\n" +
                                            "</table>";
                }
                else
                {
                    divTabInner.InnerHtml = "<table id=\"task_table_" + TabIndex + "\" class=\"table cards check_arrange grid_test full_width_table\" data-size=\"full_width_table\">" + "\r\n" +
                                            "<thead>" + "\r\n" +
                                                "<tr>" + "\r\n" +
                                                    "<th></th>" + "\r\n" +
                                                    "<th>Created On</th>" + "\r\n" +
                                                    "<th>" + objSes.EL2 + " Name</th>" + "\r\n" +
                                                    "<th>" + Extra1Name + "</th>" + "\r\n" +
                                                    "<th>" + Extra2Name + "</th>" + "\r\n" +
                                                    "<th>Task Owner</th>" + "\r\n" +
                                                    "<th>Due On</th>" + "\r\n" +
                                                    "<th>Posted On</th>" + "\r\n" +
                                                    "<th>Posted By</th>" + "\r\n" +
                                                    "<th>" + objSes.EL1 + "</th>" + "\r\n" +
                                                    "<th>Current Step</th>" + "\r\n" +
                                                "</tr>" + "\r\n" +
                                            "</thead>" + "\r\n" +
                                        "</table>";
                }

                divTabMain.Controls.Add(divTabInner);
                divTabPages.Controls.Add(divTabMain);

                if (objSes.Task_List_View)
                {
                    TableScript = TableScript + "$(\"#task_table_" + TabIndex + "\").dataTable({" + "\r\n" +
                                                "                pageLength: 50," + "\r\n" +
                                                "                order: [[" + (objSes.ListSort == 1 ? "2" : "9") + ", \"" + (objSes.ListSortDir ? "asc" : "desc") + "\"]]," + "\r\n" +
                                                "                responsive: true," + "\r\n" +
                                                "                autoWidth: true," + "\r\n" +
                                                "                processing: true," + "\r\n" +
                                                "                serverSide: true," + "\r\n" +
                                                "                ajax:" + "\r\n" +
                                                "                {" + "\r\n" +
                                                "                    url: \"api/tasks/GetTasksCat2\"," + "\r\n" +
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
                                                "                        data.que_id = \"" + Queue_ID + "\";" + "\r\n" +
                                                "                        data.wf_id = \"" + Walkflow_ID + "\";" + "\r\n" +
                                                "                        data.typ_id = \"0\";" + "\r\n" +
                                                "                    }" + "\r\n" +
                                                "                }," + "\r\n" +
                                                "                columns: [" + "\r\n" +
                                                "                    { data: \"Edit_Button\" }," + "\r\n" +
                                                "                    { data: \"Task_Number\" }," + "\r\n" +
                                                "                    { data: \"Task_Date\" }," + "\r\n" +
                                                "                    { data: \"Display_Name\" }," + "\r\n" +
                                                "                    { data: \"Extra_Field_Value\" }," + "\r\n" +
                                                "                    { data: \"Extra_Field_Value2\" }," + "\r\n" +
                                                "                    { data: \"Current_Step\" }," + "\r\n" +
                                                "                    { data: \"Created_By\" }," + "\r\n" +
                                                "                    { data: \"ETB_Value\" }," + "\r\n" +
                                                "                    { data: \"Posted_Date\" }," + "\r\n" +
                                                "                    { data: \"Edited_User\" }," + "\r\n" +
                                                "                    { data: \"Entity_L1_Name\" }," + "\r\n" +
                                                "                    { data: \"Check_Box\" }" + "\r\n" +
                                                "                ]," + "\r\n" +
                                                "                columnDefs: [" + "\r\n" +
                                                "                    { orderable: false, width:\"22px\", targets: 0 }," + "\r\n" +
                                                "                    { orderable: false, width:\"22px\", targets: 12 }," + "\r\n" +
                                                "                    { orderable: true, className: 'progress_cell', targets: 6 }" + "\r\n" +
                                                "                ]," + "\r\n" +
                                                "                \"fnDrawCallback\": function(oSettings, json) {\r\n" +
                                                "                     AdjustGridResp();\r\n" +
                                                "                }\r\n" +
                                                "            });" + "\r\n\r\n" +
                                                "        setInterval(function () {" + "\r\n" +
                                                "            $('#task_table_" + TabIndex + "').DataTable().ajax.reload(null, false);" + "\r\n" +
                                                "        }, " + objSes.RefFreq + ");" + "\r\n\r\n";
                }
                else
                {
                    TableScript = TableScript + "$(\"#task_table_" + TabIndex + "\").dataTable({" + "\r\n" +
                                            "                lengthMenu: [[12, 24, 48, 96], [12, 24, 48, 96]], " + "\r\n" +
                                            "                pageLength: 48," + "\r\n" +
                                            "                order: [[" + (objSes.ListSort == 1 ? "1" : "7") + ", \"" + (objSes.ListSortDir ? "asc" : "desc") + "\"]]," + "\r\n" +
                                            "                responsive: true," + "\r\n" +
                                            "                autoWidth: true," + "\r\n" +
                                            "                processing: true," + "\r\n" +
                                            "                serverSide: true," + "\r\n" +
                                            "                ajax:" + "\r\n" +
                                            "                {" + "\r\n" +
                                            "                    url: \"api/tasks/GetTasksCat2Card\"," + "\r\n" +
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
                                            "                        data.que_id = \"" + Queue_ID + "\";" + "\r\n" +
                                            "                        data.wf_id = \"" + Walkflow_ID + "\";" + "\r\n" +
                                            "                        data.typ_id = \"0\";" + "\r\n" +
                                            "                    }" + "\r\n" +
                                            "                }," + "\r\n" +
                                            "                columns: [" + "\r\n" +
                                            "                    { data: \"Edit_Button\" }," + "\r\n" +
                                            "                    { data: \"Task_Date\" }," + "\r\n" +
                                            "                    { data: \"Display_Name\" }," + "\r\n" +
                                            "                    { data: \"Extra_Field_Value\" }," + "\r\n" +
                                            "                    { data: \"Extra_Field_Value2\" }," + "\r\n" +
                                            "                    { data: \"Created_By\" }," + "\r\n" +
                                            "                    { data: \"ETB_Value\" }," + "\r\n" +
                                            "                    { data: \"Posted_Date\" }," + "\r\n" +
                                            "                    { data: \"Edited_User\" }," + "\r\n" +
                                            "                    { data: \"Entity_L1_Name\" }," + "\r\n" +
                                            "                    { data: \"Current_Step\" }" + "\r\n" +
                                            "                ]," + "\r\n" +
                                            "                columnDefs: [" + "\r\n" +
                                            "                    { orderable: false, width:\"22px\", targets: 0 }" + "\r\n" +
                                            "                ]," + "\r\n" +
                                            "                \"fnDrawCallback\": function(oSettings, json) {\r\n" +
                                            "                     $('.cards > tbody').each(function (index) {\r\n" +
                                            "                        $(this).addClass('row');\r\n" +
                                            "                        $(this).children().addClass('col-md-3 col-sm-6')\r\n" +
                                            "                     });\r\n" +
                                            "                     AdjustGridResp();\r\n" +
                                            "                }\r\n" +
                                            "            });" + "\r\n\r\n" +
                                            "        setInterval(function () {" + "\r\n" +
                                            "            $('#task_table_" + TabIndex + "').DataTable().ajax.reload(null, false);" + "\r\n" +
                                            "        }, " + objSes.RefFreq + ");" + "\r\n\r\n";
                }
            }
        }
    }
}