using N_Ter.Base;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class my_tasks_overview : System.Web.UI.Page
    {
        public string TableScript;
        public string GridClass;
        public string TaskNumber;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            if (objSes.Task_List_View)
            {
                GridClass = " table-striped table-hover grid_table";
                TaskNumber = "<th class=\"task-number\">Task Number</th>";
            }
            else
            {
                GridClass = " cards";
                TaskNumber = "";
            }

            if (IsPostBack == false)
            {
                ltrEntityL1Name.Text = objSes.EL1;
                ltrEntityL1Name2.Text = objSes.EL1;
                ltrEntityL1Name3.Text = objSes.EL1;
                ltrEntityL1Name4.Text = objSes.EL1;
                ltrEntityL1Name5.Text = objSes.EL1;

                ltrEntityL2Name.Text = objSes.EL2 + " Name";
                ltrEntityL2Name2.Text = objSes.EL2 + " Name";
                ltrEntityL2Name3.Text = objSes.EL2 + " Name";
                ltrEntityL2Name4.Text = objSes.EL2 + " Name";
                ltrEntityL2Name5.Text = objSes.EL2 + " Name";

                txtFromDate.Text = string.Format("{0:" + Constants.DateFormat + "}", DateTime.Today.AddMonths(-1));
                hndFrom.Value = string.Format("{0:" + Constants.DateFormat + "}", DateTime.Today.AddMonths(-1));
                txtToDate.Text = string.Format("{0:" + Constants.DateFormat + "}", DateTime.Today);
                hndTo.Value = string.Format("{0:" + Constants.DateFormat + "}", DateTime.Today);

                RefreshGrids(1, objSes);
            }
        }

        private void RefreshGrids(int ActiveTab, SessionObject objSes)
        {
            string TabCSS1 = "";
            if (ActiveTab == 1)
            {
                TabCSS1 = " class='active'";
                lft_tab_1.CssClass = "tab-pane fade active in";
                lft_tab_2.CssClass = "tab-pane fade";
                lft_tab_3.CssClass = "tab-pane fade";
                lft_tab_4.CssClass = "tab-pane fade";
                lft_tab_5.CssClass = "tab-pane fade";
            }
            string TabCSS2 = "";
            if (ActiveTab == 2)
            {
                TabCSS2 = " class='active'";
                lft_tab_1.CssClass = "tab-pane fade";
                lft_tab_2.CssClass = "tab-pane fade active in";
                lft_tab_3.CssClass = "tab-pane fade";
                lft_tab_4.CssClass = "tab-pane fade";
                lft_tab_5.CssClass = "tab-pane fade";
            }
            string TabCSS3 = "";
            if (ActiveTab == 3)
            {
                TabCSS3 = " class='active'";
                lft_tab_1.CssClass = "tab-pane fade";
                lft_tab_2.CssClass = "tab-pane fade";
                lft_tab_3.CssClass = "tab-pane fade active in";
                lft_tab_4.CssClass = "tab-pane fade";
                lft_tab_5.CssClass = "tab-pane fade";
            }
            string TabCSS4 = "";
            if (ActiveTab == 4)
            {
                TabCSS4 = " class='active'";
                lft_tab_1.CssClass = "tab-pane fade";
                lft_tab_2.CssClass = "tab-pane fade";
                lft_tab_3.CssClass = "tab-pane fade";
                lft_tab_4.CssClass = "tab-pane fade active in";
                lft_tab_5.CssClass = "tab-pane fade";
            }
            string TabCSS5 = "";
            if (ActiveTab == 5)
            {
                TabCSS5 = " class='active'";
                lft_tab_1.CssClass = "tab-pane fade";
                lft_tab_2.CssClass = "tab-pane fade";
                lft_tab_3.CssClass = "tab-pane fade";
                lft_tab_4.CssClass = "tab-pane fade";
                lft_tab_5.CssClass = "tab-pane fade active in";
            }

            Tasks objTasks = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            TableScript = "";
            int RecNumber = RefreshOnProgress(objTasks, objSes);

            ltrTabs.Text = "<li" + TabCSS1 + ">" + "\r\n" +
                               "<a data-toggle='tab' href='#lft_tab_1'>In Progress <span id='opCount' class='badge badge-info'>" + Convert.ToString(RecNumber) + "</span></a>" + "\r\n" +
                           "</li>" + "\r\n";

            RecNumber = RefreshInactive(objTasks, objSes);

            ltrTabs.Text = ltrTabs.Text + "<li" + TabCSS2 + ">" + "\r\n" +
                                              "<a data-toggle='tab' href='#lft_tab_2'>Inactive <span id='intvCount' class='badge badge-info'>" + Convert.ToString(RecNumber) + "</span></a>" + "\r\n" +
                                          "</li>" + "\r\n";

            RecNumber = RefreshUnreachable(objTasks, objSes);

            ltrTabs.Text = ltrTabs.Text + "<li" + TabCSS3 + ">" + "\r\n" +
                                              "<a data-toggle='tab' href='#lft_tab_3'>Unreachable <span id='unrchCount' class='badge badge-info'>" + Convert.ToString(RecNumber) + "</span></a>" + "\r\n" +
                                          "</li>" + "\r\n";

            RecNumber = RefreshClosed(objTasks, objSes);
            ltrTabs.Text = ltrTabs.Text + " <li" + TabCSS4 + ">" + "\r\n" +
                                              "<a data-toggle='tab' href='#lft_tab_4'>Completed <span id='cpltCount' class='badge badge-info'>" + Convert.ToString(RecNumber) + "</span></a>" + "\r\n" +
                                          "</li>" + "\r\n";

            RecNumber = RefreshLocked(objTasks, objSes);
            ltrTabs.Text = ltrTabs.Text + "<li" + TabCSS5 + ">" + "\r\n" +
                                              "<a data-toggle='tab' href='#lft_tab_5'>Locked Tasks <span id='lcdCount' class='badge badge-info'>" + Convert.ToString(RecNumber) + "</span></a>" + "\r\n" +
                                          "</li>" + "\r\n";

            if (objSes.isAdmin != 1 && objSes.isAdmin != 2)
            {
                pnlUnlock.Visible = false;
                hndID_ModalPopupExtender.Enabled = false;
            }
        }

        private int RefreshLocked(Tasks objTasks, SessionObject objSes)
        {
            DS_Tasks ds = objTasks.ReadTaskToRelease(objSes.UserID, objSes.isAdmin, true, ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type));
            if (objSes.Task_List_View)
            {
                TableScript = TableScript + "$(\"#tblLocked\").dataTable({" + "\r\n" +
                                        "                pageLength: 50," + "\r\n" +
                                        "                order: [[" + (objSes.ListSort == 1 ? "2" : "10") + ", \"" + (objSes.ListSortDir ? "asc" : "desc") + "\"]]," + "\r\n" +
                                        "                responsive: true," + "\r\n" +
                                        "                autoWidth: true," + "\r\n" +
                                        "                processing: true," + "\r\n" +
                                        "                serverSide: true," + "\r\n" +
                                        "                ajax:" + "\r\n" +
                                        "                {" + "\r\n" +
                                        "                    url: \"api/tasks/GetTasksInvoivements\"," + "\r\n" +
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
                                        "                        data.cat_id = \"1\";" + "\r\n" +
                                        "                    }" + "\r\n" +
                                        "                }," + "\r\n" +
                                        "                columns: [" + "\r\n" +
                                        "                    { data: \"Edit_Button\" }," + "\r\n" +
                                        "                    { data: \"Task_Number\" }," + "\r\n" +
                                        "                    { data: \"Task_Date\" }," + "\r\n" +
                                        "                    { data: \"Workflow_Name\" }," + "\r\n" +
                                        "                    { data: \"Display_Name\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value2\" }," + "\r\n" +
                                        "                    { data: \"Current_Step\" }," + "\r\n" +
                                        "                    { data: \"Created_By\" }," + "\r\n" +
                                        "                    { data: \"ETB_Value\" }," + "\r\n" +
                                        "                    { data: \"Posted_Date\" }," + "\r\n" +
                                        "                    { data: \"Edited_User\" }," + "\r\n" +
                                        "                    { data: \"Entity_L1_Name\" }," + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                columnDefs: [" + "\r\n" +
                                        "                    { orderable: false, width:\"22px\", targets: 0 }," + "\r\n" +
                                        "                    { orderable: true, className: 'progress_cell', targets: 7 }" + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                \"fnDrawCallback\": function(oSettings, json) {\r\n" +
                                        "                     AdjustGridResp();\r\n" +
                                        "                }\r\n" +
                                        "            });" + "\r\n\r\n" +
                                        "        setInterval(function () {" + "\r\n" +
                                        "            $('#tblLocked').DataTable().ajax.reload(null, false);" + "\r\n" +
                                        "        }, " + objSes.RefFreq + ");" + "\r\n\r\n";
            }
            else
            {
                TableScript = TableScript + "$(\"#tblLocked\").dataTable({" + "\r\n" +
                                        "                lengthMenu: [[12, 24, 48, 96], [12, 24, 48, 96]], " + "\r\n" +
                                        "                pageLength: 48," + "\r\n" +
                                        "                order: [[" + (objSes.ListSort == 1 ? "1" : "8") + ", \"" + (objSes.ListSortDir ? "asc" : "desc") + "\"]]," + "\r\n" +
                                        "                responsive: true," + "\r\n" +
                                        "                autoWidth: true," + "\r\n" +
                                        "                processing: true," + "\r\n" +
                                        "                serverSide: true," + "\r\n" +
                                        "                ajax:" + "\r\n" +
                                        "                {" + "\r\n" +
                                        "                    url: \"api/tasks/GetTasksInvoivementsCard\"," + "\r\n" +
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
                                        "                        data.cat_id = \"1\";" + "\r\n" +
                                        "                    }" + "\r\n" +
                                        "                }," + "\r\n" +
                                        "                columns: [" + "\r\n" +
                                        "                    { data: \"Edit_Button\" }," + "\r\n" +
                                        "                    { data: \"Task_Date\" }," + "\r\n" +
                                        "                    { data: \"Workflow_Name\" }," + "\r\n" +
                                        "                    { data: \"Display_Name\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value2\" }," + "\r\n" +
                                        "                    { data: \"Created_By\" }," + "\r\n" +
                                        "                    { data: \"ETB_Value\" }," + "\r\n" +
                                        "                    { data: \"Posted_Date\" }," + "\r\n" +
                                        "                    { data: \"Edited_User\" }," + "\r\n" +
                                        "                    { data: \"Entity_L1_Name\" }," + "\r\n" +
                                        "                    { data: \"Current_Step\" }," + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                columnDefs: [" + "\r\n" +
                                        "                    { orderable: false, width:\"22px\", targets: 0 }" + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                \"fnDrawCallback\": function(oSettings, json) {\r\n" +
                                        "                     $('#tblLocked > tbody').each(function (index) {\r\n" +
                                        "                        $(this).addClass('row');\r\n" +
                                        "                        $(this).children().addClass('col-md-3 col-sm-6')\r\n" +
                                        "                     });\r\n" +
                                        "                     AdjustGridResp();\r\n" +
                                        "                }\r\n" +
                                        "            });" + "\r\n\r\n" +
                                        "        setInterval(function () {" + "\r\n" +
                                        "            $('#tblLocked').DataTable().ajax.reload(null, false);" + "\r\n" +
                                        "        }, " + objSes.RefFreq + ");" + "\r\n\r\n";
            }
            return ds.tbltasks.Rows.Count;
        }

        private int RefreshOnProgress(Tasks objTasks, SessionObject objSes)
        {
            DS_Tasks ds = objTasks.ReadMyTasks(objSes.UserID, false, true, objSes.isAdmin, ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type));
            if (objSes.Task_List_View)
            {
                TableScript = TableScript + "$(\"#tblOnProgress\").dataTable({" + "\r\n" +
                                        "                pageLength: 50," + "\r\n" +
                                        "                order: [[" + (objSes.ListSort == 1 ? "2" : "10") + ", \"" + (objSes.ListSortDir ? "asc" : "desc") + "\"]]," + "\r\n" +
                                        "                responsive: true," + "\r\n" +
                                        "                autoWidth: true," + "\r\n" +
                                        "                processing: true," + "\r\n" +
                                        "                serverSide: true," + "\r\n" +
                                        "                ajax:" + "\r\n" +
                                        "                {" + "\r\n" +
                                        "                    url: \"api/tasks/GetTasksInvoivements\"," + "\r\n" +
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
                                        "                        data.cat_id = \"2\";" + "\r\n" +
                                        "                    }" + "\r\n" +
                                        "                }," + "\r\n" +
                                        "                columns: [" + "\r\n" +
                                        "                    { data: \"Edit_Button\" }," + "\r\n" +
                                        "                    { data: \"Task_Number\" }," + "\r\n" +
                                        "                    { data: \"Task_Date\" }," + "\r\n" +
                                        "                    { data: \"Workflow_Name\" }," + "\r\n" +
                                        "                    { data: \"Display_Name\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value2\" }," + "\r\n" +
                                        "                    { data: \"Current_Step\" }," + "\r\n" +
                                        "                    { data: \"Created_By\" }," + "\r\n" +
                                        "                    { data: \"ETB_Value\" }," + "\r\n" +
                                        "                    { data: \"Posted_Date\" }," + "\r\n" +
                                        "                    { data: \"Edited_User\" }," + "\r\n" +
                                        "                    { data: \"Entity_L1_Name\" }," + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                columnDefs: [" + "\r\n" +
                                        "                    { orderable: false, width:\"22px\", targets: 0 }," + "\r\n" +
                                        "                    { orderable: true, className: 'progress_cell', targets: 7 }" + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                \"fnDrawCallback\": function(oSettings, json) {\r\n" +
                                        "                     AdjustGridResp();\r\n" +
                                        "                }\r\n" +
                                        "            });" + "\r\n\r\n" +
                                        "        setInterval(function () {" + "\r\n" +
                                        "            $('#tblOnProgress').DataTable().ajax.reload(null, false);" + "\r\n" +
                                        "        }, " + objSes.RefFreq + ");" + "\r\n\r\n";
            }
            else
            {
                TableScript = TableScript + "$(\"#tblOnProgress\").dataTable({" + "\r\n" +
                                        "                lengthMenu: [[12, 24, 48, 96], [12, 24, 48, 96]], " + "\r\n" +
                                        "                pageLength: 48," + "\r\n" +
                                        "                order: [[" + (objSes.ListSort == 1 ? "1" : "8") + ", \"" + (objSes.ListSortDir ? "asc" : "desc") + "\"]]," + "\r\n" +
                                        "                responsive: true," + "\r\n" +
                                        "                autoWidth: true," + "\r\n" +
                                        "                processing: true," + "\r\n" +
                                        "                serverSide: true," + "\r\n" +
                                        "                ajax:" + "\r\n" +
                                        "                {" + "\r\n" +
                                        "                    url: \"api/tasks/GetTasksInvoivementsCard\"," + "\r\n" +
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
                                        "                        data.cat_id = \"2\";" + "\r\n" +
                                        "                    }" + "\r\n" +
                                        "                }," + "\r\n" +
                                        "                columns: [" + "\r\n" +
                                        "                    { data: \"Edit_Button\" }," + "\r\n" +
                                        "                    { data: \"Task_Date\" }," + "\r\n" +
                                        "                    { data: \"Workflow_Name\" }," + "\r\n" +
                                        "                    { data: \"Display_Name\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value2\" }," + "\r\n" +
                                        "                    { data: \"Created_By\" }," + "\r\n" +
                                        "                    { data: \"ETB_Value\" }," + "\r\n" +
                                        "                    { data: \"Posted_Date\" }," + "\r\n" +
                                        "                    { data: \"Edited_User\" }," + "\r\n" +
                                        "                    { data: \"Entity_L1_Name\" }," + "\r\n" +
                                        "                    { data: \"Current_Step\" }," + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                columnDefs: [" + "\r\n" +
                                        "                    { orderable: false, width:\"22px\", targets: 0 }" + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                \"fnDrawCallback\": function(oSettings, json) {\r\n" +
                                        "                     $('#tblOnProgress > tbody').each(function (index) {\r\n" +
                                        "                        $(this).addClass('row');\r\n" +
                                        "                        $(this).children().addClass('col-md-3 col-sm-6')\r\n" +
                                        "                     });\r\n" +
                                        "                     AdjustGridResp();\r\n" +
                                        "                }\r\n" +
                                        "            });" + "\r\n\r\n" +
                                        "        setInterval(function () {" + "\r\n" +
                                        "            $('#tblOnProgress').DataTable().ajax.reload(null, false);" + "\r\n" +
                                        "        }, " + objSes.RefFreq + ");" + "\r\n\r\n";
            }
            return ds.tbltasks.Rows.Count;
        }

        private int RefreshInactive(Tasks objTasks, SessionObject objSes)
        {
            DS_Tasks ds = objTasks.ReadMyTasks(objSes.UserID, false, false, objSes.isAdmin, ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type));
            if (objSes.Task_List_View)
            {
                TableScript = TableScript + "$(\"#tblInactive\").dataTable({" + "\r\n" +
                                        "                pageLength: 50," + "\r\n" +
                                        "                order: [[" + (objSes.ListSort == 1 ? "2" : "10") + ", \"" + (objSes.ListSortDir ? "asc" : "desc") + "\"]]," + "\r\n" +
                                        "                responsive: true," + "\r\n" +
                                        "                autoWidth: true," + "\r\n" +
                                        "                processing: true," + "\r\n" +
                                        "                serverSide: true," + "\r\n" +
                                        "                ajax:" + "\r\n" +
                                        "                {" + "\r\n" +
                                        "                    url: \"api/tasks/GetTasksInvoivements\"," + "\r\n" +
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
                                        "                        data.cat_id = \"3\";" + "\r\n" +
                                        "                    }" + "\r\n" +
                                        "                }," + "\r\n" +
                                        "                columns: [" + "\r\n" +
                                        "                    { data: \"Edit_Button\" }," + "\r\n" +
                                        "                    { data: \"Task_Number\" }," + "\r\n" +
                                        "                    { data: \"Task_Date\" }," + "\r\n" +
                                        "                    { data: \"Workflow_Name\" }," + "\r\n" +
                                        "                    { data: \"Display_Name\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value2\" }," + "\r\n" +
                                        "                    { data: \"Current_Step\" }," + "\r\n" +
                                        "                    { data: \"Created_By\" }," + "\r\n" +
                                        "                    { data: \"ETB_Value\" }," + "\r\n" +
                                        "                    { data: \"Posted_Date\" }," + "\r\n" +
                                        "                    { data: \"Edited_User\" }," + "\r\n" +
                                        "                    { data: \"Entity_L1_Name\" }," + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                columnDefs: [" + "\r\n" +
                                        "                    { orderable: false, width:\"22px\", targets: 0 }," + "\r\n" +
                                        "                    { orderable: true, className: 'progress_cell', targets: 7 }" + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                \"fnDrawCallback\": function(oSettings, json) {\r\n" +
                                        "                     AdjustGridResp();\r\n" +
                                        "                }\r\n" +
                                        "            });" + "\r\n\r\n" +
                                        "        setInterval(function () {" + "\r\n" +
                                        "            $('#tblInactive').DataTable().ajax.reload(null, false);" + "\r\n" +
                                        "        }, " + objSes.RefFreq + ");" + "\r\n\r\n";
            }
            else
            {
                TableScript = TableScript + "$(\"#tblInactive\").dataTable({" + "\r\n" +
                                        "                lengthMenu: [[12, 24, 48, 96], [12, 24, 48, 96]], " + "\r\n" +
                                        "                pageLength: 48," + "\r\n" +
                                        "                order: [[" + (objSes.ListSort == 1 ? "1" : "8") + ", \"" + (objSes.ListSortDir ? "asc" : "desc") + "\"]]," + "\r\n" +
                                        "                responsive: true," + "\r\n" +
                                        "                autoWidth: true," + "\r\n" +
                                        "                processing: true," + "\r\n" +
                                        "                serverSide: true," + "\r\n" +
                                        "                ajax:" + "\r\n" +
                                        "                {" + "\r\n" +
                                        "                    url: \"api/tasks/GetTasksInvoivementsCard\"," + "\r\n" +
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
                                        "                        data.cat_id = \"3\";" + "\r\n" +
                                        "                    }" + "\r\n" +
                                        "                }," + "\r\n" +
                                        "                columns: [" + "\r\n" +
                                        "                    { data: \"Edit_Button\" }," + "\r\n" +
                                        "                    { data: \"Task_Date\" }," + "\r\n" +
                                        "                    { data: \"Workflow_Name\" }," + "\r\n" +
                                        "                    { data: \"Display_Name\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value2\" }," + "\r\n" +
                                        "                    { data: \"Created_By\" }," + "\r\n" +
                                        "                    { data: \"ETB_Value\" }," + "\r\n" +
                                        "                    { data: \"Posted_Date\" }," + "\r\n" +
                                        "                    { data: \"Edited_User\" }," + "\r\n" +
                                        "                    { data: \"Entity_L1_Name\" }," + "\r\n" +
                                        "                    { data: \"Current_Step\" }," + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                columnDefs: [" + "\r\n" +
                                        "                    { orderable: false, width:\"22px\", targets: 0 }" + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                \"fnDrawCallback\": function(oSettings, json) {\r\n" +
                                        "                     $('#tblInactive > tbody').each(function (index) {\r\n" +
                                        "                        $(this).addClass('row');\r\n" +
                                        "                        $(this).children().addClass('col-md-3 col-sm-6')\r\n" +
                                        "                     });\r\n" +
                                        "                     AdjustGridResp();\r\n" +
                                        "                }\r\n" +
                                        "            });" + "\r\n\r\n" +
                                        "        setInterval(function () {" + "\r\n" +
                                        "            $('#tblInactive').DataTable().ajax.reload(null, false);" + "\r\n" +
                                        "        }, " + objSes.RefFreq + ");" + "\r\n\r\n";
            }
            return ds.tbltasks.Rows.Count;
        }

        private int RefreshUnreachable(Tasks objTasks, SessionObject objSes)
        {
            DS_Tasks dsTasks = objTasks.ReadMyTasks(objSes.UserID, false, true, objSes.isAdmin, ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type));
            DS_Users dsUEC = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type).ReadUserGroupEntityUserCount();

            DS_Tasks dsTasksCopy = new DS_Tasks();
            DS_Tasks.tbltasksRow drTaskCopy;

            int Group_ID = 0;
            int Entity_L2_ID = 0;

            foreach (DS_Tasks.tbltasksRow sRow in dsTasks.tbltasks.OrderBy(x => x.User_Group_Involved).ThenBy(y => y.Entity_L2_ID).ToList())
            {
                if (Group_ID != sRow.User_Group_Involved || Entity_L2_ID != sRow.Entity_L2_ID)
                {
                    Group_ID = sRow.User_Group_Involved;
                    Entity_L2_ID = sRow.Entity_L2_ID;

                    if (dsUEC.tbluser_group_entity_l2.Where(x => x.Entity_L2_ID == Entity_L2_ID && x.User_Group_ID == Group_ID).Count() == 0)
                    {
                        foreach (DS_Tasks.tbltasksRow row in dsTasks.tbltasks.Where(x => x.User_Group_Involved == Group_ID && x.Entity_L2_ID == Entity_L2_ID).ToList())
                        {
                            drTaskCopy = dsTasksCopy.tbltasks.NewtbltasksRow();
                            drTaskCopy.ItemArray = row.ItemArray;
                            dsTasksCopy.tbltasks.Rows.Add(drTaskCopy);
                        }
                    }
                }
            }
            if (objSes.Task_List_View)
            {
                TableScript = TableScript + "$(\"#tblUnreachable\").dataTable({" + "\r\n" +
                                        "                pageLength: 50," + "\r\n" +
                                        "                order: [[" + (objSes.ListSort == 1 ? "2" : "10") + ", \"" + (objSes.ListSortDir ? "asc" : "desc") + "\"]]," + "\r\n" +
                                        "                responsive: true," + "\r\n" +
                                        "                autoWidth: true," + "\r\n" +
                                        "                processing: true," + "\r\n" +
                                        "                serverSide: true," + "\r\n" +
                                        "                ajax:" + "\r\n" +
                                        "                {" + "\r\n" +
                                        "                    url: \"api/tasks/GetTasksInvoivements\"," + "\r\n" +
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
                                        "                        data.cat_id = \"4\";" + "\r\n" +
                                        "                    }" + "\r\n" +
                                        "                }," + "\r\n" +
                                        "                columns: [" + "\r\n" +
                                        "                    { data: \"Edit_Button\" }," + "\r\n" +
                                        "                    { data: \"Task_Number\" }," + "\r\n" +
                                        "                    { data: \"Task_Date\" }," + "\r\n" +
                                        "                    { data: \"Workflow_Name\" }," + "\r\n" +
                                        "                    { data: \"Display_Name\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value2\" }," + "\r\n" +
                                        "                    { data: \"Current_Step\" }," + "\r\n" +
                                        "                    { data: \"Created_By\" }," + "\r\n" +
                                        "                    { data: \"ETB_Value\" }," + "\r\n" +
                                        "                    { data: \"Posted_Date\" }," + "\r\n" +
                                        "                    { data: \"Edited_User\" }," + "\r\n" +
                                        "                    { data: \"Entity_L1_Name\" }," + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                columnDefs: [" + "\r\n" +
                                        "                    { orderable: false, width:\"22px\", targets: 0 }," + "\r\n" +
                                        "                    { orderable: true, className: 'progress_cell', targets: 7 }" + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                \"fnDrawCallback\": function(oSettings, json) {\r\n" +
                                        "                     AdjustGridResp();\r\n" +
                                        "                }\r\n" +
                                        "            });" + "\r\n\r\n" +
                                        "        setInterval(function () {" + "\r\n" +
                                        "            $('#tblUnreachable').DataTable().ajax.reload(null, false);" + "\r\n" +
                                        "        }, " + objSes.RefFreq + ");" + "\r\n\r\n";
            }
            else
            {
                TableScript = TableScript + "$(\"#tblUnreachable\").dataTable({" + "\r\n" +
                                        "                lengthMenu: [[12, 24, 48, 96], [12, 24, 48, 96]], " + "\r\n" +
                                        "                pageLength: 48," + "\r\n" +
                                        "                order: [[" + (objSes.ListSort == 1 ? "1" : "8") + ", \"" + (objSes.ListSortDir ? "asc" : "desc") + "\"]]," + "\r\n" +
                                        "                responsive: true," + "\r\n" +
                                        "                autoWidth: true," + "\r\n" +
                                        "                processing: true," + "\r\n" +
                                        "                serverSide: true," + "\r\n" +
                                        "                ajax:" + "\r\n" +
                                        "                {" + "\r\n" +
                                        "                    url: \"api/tasks/GetTasksInvoivementsCard\"," + "\r\n" +
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
                                        "                        data.cat_id = \"4\";" + "\r\n" +
                                        "                    }" + "\r\n" +
                                        "                }," + "\r\n" +
                                        "                columns: [" + "\r\n" +
                                        "                    { data: \"Edit_Button\" }," + "\r\n" +
                                        "                    { data: \"Task_Date\" }," + "\r\n" +
                                        "                    { data: \"Workflow_Name\" }," + "\r\n" +
                                        "                    { data: \"Display_Name\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value2\" }," + "\r\n" +
                                        "                    { data: \"Created_By\" }," + "\r\n" +
                                        "                    { data: \"ETB_Value\" }," + "\r\n" +
                                        "                    { data: \"Posted_Date\" }," + "\r\n" +
                                        "                    { data: \"Edited_User\" }," + "\r\n" +
                                        "                    { data: \"Entity_L1_Name\" }," + "\r\n" +
                                        "                    { data: \"Current_Step\" }," + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                columnDefs: [" + "\r\n" +
                                        "                    { orderable: false, width:\"22px\", targets: 0 }" + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                \"fnDrawCallback\": function(oSettings, json) {\r\n" +
                                        "                     $('#tblUnreachable > tbody').each(function (index) {\r\n" +
                                        "                        $(this).addClass('row');\r\n" +
                                        "                        $(this).children().addClass('col-md-3 col-sm-6')\r\n" +
                                        "                     });\r\n" +
                                        "                     AdjustGridResp();\r\n" +
                                        "                }\r\n" +
                                        "            });" + "\r\n\r\n" +
                                        "        setInterval(function () {" + "\r\n" +
                                        "            $('#tblUnreachable').DataTable().ajax.reload(null, false);" + "\r\n" +
                                        "        }, " + objSes.RefFreq + ");" + "\r\n\r\n";
            }
            return dsTasksCopy.tbltasks.Rows.Count;
        }

        private int RefreshClosed(Tasks objTasks, SessionObject objSes)
        {
            DateTime dtFrom = DateTime.Today.Date;
            DateTime dtTo = DateTime.Today.Date;

            DS_Tasks ds = new DS_Tasks();

            Common_Actions objCom = new Common_Actions();

            if (objCom.ValidateDate(hndFrom.Value, ref dtFrom))
            {
                if (objCom.ValidateDate(hndTo.Value, ref dtTo))
                {
                    ds = objTasks.ReadMyTasks(objSes.UserID, true, dtFrom, dtTo, objSes.isAdmin, ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type));
                }
            }
            if (objSes.Task_List_View)
            {
                TableScript = TableScript + "$(\"#tblClosed\").dataTable({" + "\r\n" +
                                        "                pageLength: 50," + "\r\n" +
                                        "                order: [[" + (objSes.ListSort == 1 ? "2" : "9") + ", \"" + (objSes.ListSortDir ? "asc" : "desc") + "\"]]," + "\r\n" +
                                        "                responsive: true," + "\r\n" +
                                        "                autoWidth: true," + "\r\n" +
                                        "                processing: true," + "\r\n" +
                                        "                serverSide: true," + "\r\n" +
                                        "                ajax:" + "\r\n" +
                                        "                {" + "\r\n" +
                                        "                    url: \"api/tasks/GetTasksInvoivements\"," + "\r\n" +
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
                                        "                        data.cat_id = \"5\";" + "\r\n" +
                                        "                        data.fr_dt = $(\"#" + hndFrom.ClientID + "\").val();" + "\r\n" +
                                        "                        data.to_dt = $(\"#" + hndTo.ClientID + "\").val();" + "\r\n" +
                                        "                    }" + "\r\n" +
                                        "                }," + "\r\n" +
                                        "                columns: [" + "\r\n" +
                                        "                    { data: \"Edit_Button\" }," + "\r\n" +
                                        "                    { data: \"Task_Number\" }," + "\r\n" +
                                        "                    { data: \"Task_Date\" }," + "\r\n" +
                                        "                    { data: \"Workflow_Name\" }," + "\r\n" +
                                        "                    { data: \"Display_Name\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value2\" }," + "\r\n" +
                                        "                    { data: \"Created_By\" }," + "\r\n" +
                                        "                    { data: \"ETB_Value\" }," + "\r\n" +
                                        "                    { data: \"Posted_Date\" }," + "\r\n" +
                                        "                    { data: \"Edited_User\" }," + "\r\n" +
                                        "                    { data: \"Entity_L1_Name\" }," + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                columnDefs: [" + "\r\n" +
                                        "                    { orderable: false, width:\"22px\", targets: 0 }" + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                \"fnDrawCallback\": function(oSettings, json) {\r\n" +
                                        "                     AdjustGridResp();\r\n" +
                                        "                }\r\n" +
                                        "            });" + "\r\n\r\n" +
                                        "        setInterval(function () {" + "\r\n" +
                                        "            $('#tblClosed').DataTable().ajax.reload(null, false);" + "\r\n" +
                                        "        }, " + objSes.RefFreq + ");" + "\r\n\r\n";
            }
            else
            {
                TableScript = TableScript + "$(\"#tblClosed\").dataTable({" + "\r\n" +
                                        "                lengthMenu: [[12, 24, 48, 96], [12, 24, 48, 96]], " + "\r\n" +
                                        "                pageLength: 48," + "\r\n" +
                                        "                order: [[" + (objSes.ListSort == 1 ? "1" : "8") + ", \"" + (objSes.ListSortDir ? "asc" : "desc") + "\"]]," + "\r\n" +
                                        "                responsive: true," + "\r\n" +
                                        "                autoWidth: true," + "\r\n" +
                                        "                processing: true," + "\r\n" +
                                        "                serverSide: true," + "\r\n" +
                                        "                ajax:" + "\r\n" +
                                        "                {" + "\r\n" +
                                        "                    url: \"api/tasks/GetTasksInvoivementsCard\"," + "\r\n" +
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
                                        "                        data.cat_id = \"5\";" + "\r\n" +
                                        "                        data.fr_dt = $(\"#" + hndFrom.ClientID + "\").val();" + "\r\n" +
                                        "                        data.to_dt = $(\"#" + hndTo.ClientID + "\").val();" + "\r\n" +
                                        "                    }" + "\r\n" +
                                        "                }," + "\r\n" +
                                        "                columns: [" + "\r\n" +
                                        "                    { data: \"Edit_Button\" }," + "\r\n" +
                                        "                    { data: \"Task_Date\" }," + "\r\n" +
                                        "                    { data: \"Workflow_Name\" }," + "\r\n" +
                                        "                    { data: \"Display_Name\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value\" }," + "\r\n" +
                                        "                    { data: \"Extra_Field_Value2\" }," + "\r\n" +
                                        "                    { data: \"Created_By\" }," + "\r\n" +
                                        "                    { data: \"ETB_Value\" }," + "\r\n" +
                                        "                    { data: \"Posted_Date\" }," + "\r\n" +
                                        "                    { data: \"Edited_User\" }," + "\r\n" +
                                        "                    { data: \"Entity_L1_Name\" }," + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                columnDefs: [" + "\r\n" +
                                        "                    { orderable: false, width:\"22px\", targets: 0 }" + "\r\n" +
                                        "                ]," + "\r\n" +
                                        "                \"fnDrawCallback\": function(oSettings, json) {\r\n" +
                                        "                     $('#tblClosed > tbody').each(function (index) {\r\n" +
                                        "                        $(this).addClass('row');\r\n" +
                                        "                        $(this).children().addClass('col-md-3 col-sm-6')\r\n" +
                                        "                     });\r\n" +
                                        "                     AdjustGridResp();\r\n" +
                                        "                }\r\n" +
                                        "            });" + "\r\n\r\n" +
                                        "        setInterval(function () {" + "\r\n" +
                                        "            $('#tblClosed').DataTable().ajax.reload(null, false);" + "\r\n" +
                                        "        }, " + objSes.RefFreq + ");" + "\r\n\r\n";
            }
            return ds.tbltasks.Rows.Count;
        }

        protected void cmdConfirm_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int TaskID = Convert.ToInt32(hndID.Value);
            Tasks objTasks = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            DS_Tasks ds = objTasks.Read(TaskID, false, false, false);
            DS_Tasks.tbltask_historyRow[] drLastTaskHistory = (DS_Tasks.tbltask_historyRow[])ds.tbltask_history.Select("", "Task_Update_ID DESC");

            if (!objTasks.Update_Task_Lock(TaskID, drLastTaskHistory[0].Task_Update_ID, 0))
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Task Lock cannot be released');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Task successfully unlocked');", true);
                RefreshGrids(5, objSes);
                ShowTab5();
            }
        }

        private void ShowTab4()
        {
            lft_tab_4.Attributes.Remove("class");
            lft_tab_4.Attributes.Add("class", "tab-pane fade active in");
            lft_tab_1.Attributes.Remove("class");
            lft_tab_1.Attributes.Add("class", "tab-pane fade");
            lft_tab_2.Attributes.Remove("class");
            lft_tab_2.Attributes.Add("class", "tab-pane fade");
            lft_tab_3.Attributes.Remove("class");
            lft_tab_3.Attributes.Add("class", "tab-pane fade");
            lft_tab_5.Attributes.Remove("class");
            lft_tab_5.Attributes.Add("class", "tab-pane fade");
        }

        private void ShowTab5()
        {
            lft_tab_5.Attributes.Remove("class");
            lft_tab_5.Attributes.Add("class", "tab-pane fade active in");
            lft_tab_1.Attributes.Remove("class");
            lft_tab_1.Attributes.Add("class", "tab-pane fade");
            lft_tab_2.Attributes.Remove("class");
            lft_tab_2.Attributes.Add("class", "tab-pane fade");
            lft_tab_3.Attributes.Remove("class");
            lft_tab_3.Attributes.Add("class", "tab-pane fade");
            lft_tab_4.Attributes.Remove("class");
            lft_tab_4.Attributes.Add("class", "tab-pane fade");
        }

        protected void cmdShowCompleted_Click(object sender, EventArgs e)
        {
            DateTime dtFrom = DateTime.Today.Date;
            DateTime dtTo = DateTime.Today.Date;

            Common_Actions objCom = new Common_Actions();
            
            if (objCom.ValidateDate(txtFromDate.Text, ref dtFrom))
            {
                hndFrom.Value = string.Format("{0:" + Constants.DateFormat + "}", dtFrom);
            }
            if (objCom.ValidateDate(txtToDate.Text, ref dtTo))
            {
                hndTo.Value = string.Format("{0:" + Constants.DateFormat + "}", dtTo);
            }
            SessionObject objSes = (SessionObject)Session["dt"];
            RefreshGrids(4, objSes);
            ShowTab4();
        }
    }
}