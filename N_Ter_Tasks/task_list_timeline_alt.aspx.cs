using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;

namespace N_Ter_Tasks
{
    public partial class task_list_timeline_alt : System.Web.UI.Page
    {
        private string TableScript;

        protected void Page_Init(object sender, System.EventArgs e)
        {
            n_ter_base_loggedin_grid_task_list_t3 objT3 = (n_ter_base_loggedin_grid_task_list_t3)Master;
            objT3.LoadActive = true;
            objT3.FillTasks += ObjT3_FillTasks;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void ObjT3_FillTasks(object sender, EventArgs e)
        {
            TaskListArgs objArgs = (TaskListArgs)e;

            SessionObject objSes = (SessionObject)Session["dt"];
            DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(objArgs.Active_WF);

            if (dsWF.tblwalkflow.Rows.Count > 0)
            {
                DrawGrid(objArgs.Queue_ID, objArgs.Active_WF_Category, objArgs.Active_WF, dsWF.tblwalkflow[0].Exrta_Field_Naming, dsWF.tblwalkflow[0].Exrta_Field2_Naming, objSes);
            }
            else
            {
                DrawGrid(objArgs.Queue_ID, objArgs.Active_WF_Category, 0, "", "", objSes);
            }

            n_ter_base_loggedin_grid_task_list_t3 objT3 = (n_ter_base_loggedin_grid_task_list_t3)Master;
            objT3.Tables = TableScript;
            objT3.TabCountScript = "0";
        }

        private void DrawGrid(int Queue_ID, int Category_ID, int Workflow_ID, string Extra1Name, string Extra2Name, SessionObject objSes)
        {
            if (objSes.TL_Task_List_View)
            {
                divGrid.InnerHtml = "<table id=\"tblOnProgress\" class=\"table table-striped table-hover grid_table check_arrange grid_test color-grid full_width_table\" data-size=\"full_width_table\">" + "\r\n" +
                                                "<thead>" + "\r\n" +
                                                    "<tr>" + "\r\n" +
                                                        "<th></th>" + "\r\n" +
                                                        "<th>Task Number</th>" + "\r\n" +
                                                        "<th>" + objSes.EL2 + " Name</th>" + "\r\n" +
                                                        "<th>" + Extra1Name + "</th>" + "\r\n" +
                                                        "<th>" + Extra2Name + "</th>" + "\r\n" +
                                                        "<th>Current Step</th>" + "\r\n" +
                                                        "<th>Task Owner</th>" + "\r\n" +
                                                        "<th>Timeline</th>" + "\r\n" +
                                                        "<th></th>" + "\r\n" +
                                                    "</tr>" + "\r\n" +
                                                "</thead>" + "\r\n" +
                                            "</table>";
                TableScript = "$(\"#tblOnProgress\").dataTable({" + "\r\n" +
                              "                pageLength: 50," + "\r\n" +
                              "                order: [[7, \"" + (objSes.ListSortDir ? "asc" : "desc") + "\"]]," + "\r\n" +
                              "                responsive: true," + "\r\n" +
                              "                autoWidth: true," + "\r\n" +
                              "                processing: true," + "\r\n" +
                              "                serverSide: true," + "\r\n" +
                              "                ajax:" + "\r\n" +
                              "                {" + "\r\n" +
                              "                    url: \"api/tasks/GetTasksInvoivementsTL2\"," + "\r\n" +
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
                              "                        data.q_id = \"" + Queue_ID + "\";" + "\r\n" +
                              "                        data.cat_id = \"" + Category_ID + "\";" + "\r\n" +
                              "                        data.wf_id = \"" + Workflow_ID + "\";" + "\r\n" +
                              "                    }" + "\r\n" +
                              "                }," + "\r\n" +
                              "                columns: [" + "\r\n" +
                              "                    { data: \"Edit_Button\" }," + "\r\n" +
                              "                    { data: \"Task_Number\" }," + "\r\n" +
                              "                    { data: \"Display_Name\" }," + "\r\n" +
                              "                    { data: \"Extra_Field_Value\" }," + "\r\n" +
                              "                    { data: \"Extra_Field_Value2\" }," + "\r\n" +
                              "                    { data: \"Current_Step\" }," + "\r\n" +
                              "                    { data: \"Created_By\" }," + "\r\n" +
                              "                    { data: \"Step_Status\" }," + "\r\n" +
                              "                    { data: \"Check_Box\" }," + "\r\n" +
                              "                ]," + "\r\n" +
                              "                columnDefs: [" + "\r\n" +
                              "                    { orderable: false, width:\"22px\", targets: 0 }," + "\r\n" +
                              "                    { orderable: false, width:\"22px\", targets: 8 }," + "\r\n" +
                              "                    { orderable: true, className: 'progress_cell', targets: 5 }," + "\r\n" +
                              "                    { orderable: true, width:\"200px\", targets: 7 }," + "\r\n" +
                              "                ]," + "\r\n" +
                              "                \"fnDrawCallback\": function(oSettings, json) {\r\n" +
                              "                     AdjustGridResp();\r\n" +
                              "                     ArrangeTooltip();" +
                              "                }\r\n" +
                              "            });" + "\r\n\r\n" +
                              "        setInterval(function () {" + "\r\n" +
                              "            $('#tblOnProgress').DataTable().ajax.reload(null, false);" + "\r\n" +
                              "        }, " + objSes.RefFreq + ");" + "\r\n\r\n";
            }
            else
            {
                divGrid.InnerHtml = "<table id=\"tblOnProgress\" class=\"table cards check_arrange grid_test full_width_table\" data-size=\"full_width_table\">" + "\r\n" +
                                        "<thead>" + "\r\n" +
                                            "<tr>" + "\r\n" +
                                                "<th></th>" + "\r\n" +
                                                "<th>" + objSes.EL2 + " Name</th>" + "\r\n" +
                                                "<th></th>" + "\r\n" +
                                                "<th></th>" + "\r\n" +
                                                "<th>Task Owner</th>" + "\r\n" +
                                                "<th>Posted By</th>" + "\r\n" +
                                                "<th>" + objSes.EL1 + " Name</th>" + "\r\n" +
                                                "<th>Current Step</th>" + "\r\n" +
                                                "<th>Timeline</th>" + "\r\n" +
                                            "</tr>" + "\r\n" +
                                        "</thead>" + "\r\n" +
                                    "</table>";
                TableScript = "$(\"#tblOnProgress\").dataTable({" + "\r\n" +
                                "                lengthMenu: [[12, 24, 48, 96], [12, 24, 48, 96]], " + "\r\n" +
                                "                pageLength: 48," + "\r\n" +
                                "                order: [[8, \"" + (objSes.ListSortDir ? "asc" : "desc") + "\"]]," + "\r\n" +
                                "                responsive: true," + "\r\n" +
                                "                autoWidth: true," + "\r\n" +
                                "                processing: true," + "\r\n" +
                                "                serverSide: true," + "\r\n" +
                                "                ajax:" + "\r\n" +
                                "                {" + "\r\n" +
                                "                    url: \"api/tasks/GetTasksInvoivementsTL2Card\"," + "\r\n" +
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
                                "                        data.q_id = \"" + Queue_ID + "\";" + "\r\n" +
                                "                        data.cat_id = \"" + Category_ID + "\";" + "\r\n" +
                                "                        data.wf_id = \"" + Workflow_ID + "\";" + "\r\n" +
                                "                    }" + "\r\n" +
                                "                }," + "\r\n" +
                                "                columns: [" + "\r\n" +
                                "                    { data: \"Edit_Button\" }," + "\r\n" +
                                "                    { data: \"Display_Name\" }," + "\r\n" +
                                "                    { data: \"Extra_Field_Value\" }," + "\r\n" +
                                "                    { data: \"Extra_Field_Value2\" }," + "\r\n" +
                                "                    { data: \"Created_By\" }," + "\r\n" +
                                "                    { data: \"Edited_User\" }," + "\r\n" +
                                "                    { data: \"Entity_L1_Name\" }," + "\r\n" +
                                "                    { data: \"Current_Step\" }," + "\r\n" +
                                "                    { data: \"Step_Status\" }" + "\r\n" +
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
                                "                     ArrangeTooltip();" +
                                "                }\r\n" +
                                "            });" + "\r\n\r\n" +
                                "        setInterval(function () {" + "\r\n" +
                                "            $('#tblOnProgress').DataTable().ajax.reload(null, false);" + "\r\n" +
                                "        }, " + objSes.RefFreq + ");" + "\r\n\r\n";
            }
        }
    }
}