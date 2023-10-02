using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace N_Ter_Tasks
{
    public partial class task_list_inactive : System.Web.UI.Page
    {
        private string TableScript;

        protected void Page_Init(object sender, System.EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            if (Request.QueryString["sid"] != null)
            {
                Response.Redirect("task_list_inactive_alt.aspx?" + Request.QueryString.ToString());
            }
            else if (Request.QueryString["wid"] != null)
            {
                if (objSes.Task_List == 3)
                {
                    Response.Redirect("task_list_inactive_alt.aspx?" + Request.QueryString.ToString());
                }
                else
                {
                    Response.Redirect("task_list_inactive_alt_2.aspx?" + Request.QueryString.ToString());
                }
            }
            else
            {
                if (objSes.Task_List == 3)
                {
                    Response.Redirect("task_list_inactive_alt.aspx?" + Request.QueryString.ToString());
                }
                else if (objSes.Task_List == 2)
                {
                    Response.Redirect("task_list_inactive_alt_2.aspx?" + Request.QueryString.ToString());
                }
                else
                {
                    n_ter_base_loggedin_grid_task_list_t1 objT1 = (n_ter_base_loggedin_grid_task_list_t1)Master;
                    objT1.LoadActive = false;
                    objT1.FillTasks += ObjT1_FillTasks;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void ObjT1_FillTasks(object sender, EventArgs e)
        {
            TaskListArgs objArgs = (TaskListArgs)e;

            SessionObject objSes = (SessionObject)Session["dt"];
            DS_Workflow dsCats = ObjectCreator.GetWorkflow_Categories(objSes.Connection, objSes.DB_Type).ReadAll();
            List<DS_Tasks.tbltasksRow> drTasksInCat;
            int TabIndex = 0;
            string TabCSS = "";

            ltrTabs.Text = "";
            TableScript = "";

            foreach (DS_Workflow.tblworkflow_categoriesRow row in dsCats.tblworkflow_categories)
            {
                drTasksInCat = objArgs.SelectedTasks.Where(x => x.Workflow_Category_ID == row.Workflow_Category_ID).ToList();
                if (drTasksInCat.Count > 0)
                {
                    DrawTabs(drTasksInCat, objArgs.Queue_ID, row.Workflow_Category_ID, objArgs.Active_WF_Category, TabIndex, objArgs.AltUsers, objSes);
                    if (row.Workflow_Category_ID == objArgs.Active_WF_Category || (TabIndex == 0 && objArgs.Active_WF_Category == 0))
                    {
                        TabCSS = " class='active'";
                    }
                    else
                    {
                        TabCSS = "";
                    }
                    ltrTabs.Text = ltrTabs.Text + "<li" + TabCSS + ">" + "\r\n" +
                                       "<a data-toggle='tab' href='#lft_tab_" + row.Workflow_Category_ID + "'>" + row.Workflow_Category_Name + " <span id='cat_cnt_" + row.Workflow_Category_ID + "' class='label label-info'>" + Convert.ToString(drTasksInCat.Count) + "</span></a>" + "\r\n" +
                                   "</li>" + "\r\n";
                    TabIndex = TabIndex + 1;
                }
            }

            n_ter_base_loggedin_grid_task_list_t1 objT1 = (n_ter_base_loggedin_grid_task_list_t1)Master;
            objT1.Tables = TableScript;
            objT1.TabCountScript = TabIndex.ToString();
        }

        private void DrawTabs(List<DS_Tasks.tbltasksRow> drTasks, int Queue_ID, int Workflow_Category_ID, int Active_WF_Category, int TabIndex, DS_Users dsAltUsers, SessionObject objSes)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divTabMain = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            divTabMain.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            if (Workflow_Category_ID == Active_WF_Category || (TabIndex == 0 && Active_WF_Category == 0))
            {
                divTabMain.Attributes.Add("class", "tab-pane fade active in");
            }
            else
            {
                divTabMain.Attributes.Add("class", "tab-pane fade");
            }
            divTabMain.ID = "lft_tab_" + Workflow_Category_ID;
            System.Web.UI.HtmlControls.HtmlGenericControl divTabInner = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            divTabInner.Attributes.Add("class", "table-responsive table-primary no-margin-b");
            divTabInner.ID = "tabInner_" + Workflow_Category_ID;
            if (objSes.Task_List_View)
            {
                divTabInner.InnerHtml = "<table id=\"task_table_" + Workflow_Category_ID + "\" class=\"table table-striped table-hover grid_table check_arrange grid_test color-grid full_width_table\" data-size=\"full_width_table\">" + "\r\n" +
                                            "<thead>" + "\r\n" +
                                                "<tr>" + "\r\n" +
                                                    "<th></th>" + "\r\n" +
                                                    "<th>Task Number</th>" + "\r\n" +
                                                    "<th>Created On</th>" + "\r\n" +
                                                    "<th>Workflow Name</th>" + "\r\n" +
                                                    "<th>" + objSes.EL2 + " Name</th>" + "\r\n" +
                                                    "<th></th>" + "\r\n" +
                                                    "<th></th>" + "\r\n" +
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
                divTabInner.InnerHtml = "<table id=\"task_table_" + Workflow_Category_ID + "\" class=\"table cards check_arrange grid_test full_width_table\" data-size=\"full_width_table\">" + "\r\n" +
                                        "<thead>" + "\r\n" +
                                            "<tr>" + "\r\n" +
                                                "<th></th>" + "\r\n" +
                                                "<th>Created On</th>" + "\r\n" +
                                                "<th>Workflow Name</th>" + "\r\n" +
                                                "<th>" + objSes.EL2 + " Name</th>" + "\r\n" +
                                                "<th></th>" + "\r\n" +
                                                "<th></th>" + "\r\n" +
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
                TableScript = TableScript + "$(\"#task_table_" + Workflow_Category_ID + "\").dataTable({" + "\r\n" +
                                            "                pageLength: 50," + "\r\n" +
                                            "                order: [[" + (objSes.ListSort == 1 ? "2" : "10") + ", \"" + (objSes.ListSortDir ? "asc" : "desc") + "\"]]," + "\r\n" +
                                            "                responsive: true," + "\r\n" +
                                            "                autoWidth: true," + "\r\n" +
                                            "                processing: true," + "\r\n" +
                                            "                serverSide: true," + "\r\n" +
                                            "                ajax:" + "\r\n" +
                                            "                {" + "\r\n" +
                                            "                    url: \"api/tasks/GetTasksCat\"," + "\r\n" +
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
                                            "                        data.cat_id = \"" + Workflow_Category_ID + "\";" + "\r\n" +
                                            "                        data.typ_id = \"0\";" + "\r\n" +
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
                                            "                    { data: \"Check_Box\" }" + "\r\n" +
                                            "                ]," + "\r\n" +
                                            "                columnDefs: [" + "\r\n" +
                                            "                    { orderable: false, width:\"22px\", targets: 0 }," + "\r\n" +
                                            "                    { orderable: false, width:\"22px\", targets: 13 }," + "\r\n" +
                                            "                    { orderable: true, className: 'progress_cell', targets: 7 }" + "\r\n" +
                                            "                ]," + "\r\n" +
                                            "                \"fnDrawCallback\": function(oSettings, json) {\r\n" +
                                            "                     AdjustGridResp();\r\n" +
                                            "                }\r\n" +
                                            "            });" + "\r\n\r\n" +
                                            "        setInterval(function () {" + "\r\n" +
                                            "            $('#task_table_" + Workflow_Category_ID + "').DataTable().ajax.reload(null, false);" + "\r\n" +
                                            "        }, " + objSes.RefFreq + ");" + "\r\n\r\n";
            }
            else
            {
                TableScript = TableScript + "$(\"#task_table_" + Workflow_Category_ID + "\").dataTable({" + "\r\n" +
                                        "                lengthMenu: [[12, 24, 48, 96], [12, 24, 48, 96]], " + "\r\n" +
                                        "                pageLength: 48," + "\r\n" +
                                        "                order: [[" + (objSes.ListSort == 1 ? "1" : "8") + ", \"" + (objSes.ListSortDir ? "asc" : "desc") + "\"]]," + "\r\n" +
                                        "                responsive: true," + "\r\n" +
                                        "                autoWidth: true," + "\r\n" +
                                        "                processing: true," + "\r\n" +
                                        "                serverSide: true," + "\r\n" +
                                        "                ajax:" + "\r\n" +
                                        "                {" + "\r\n" +
                                        "                    url: \"api/tasks/GetTasksCatCard\"," + "\r\n" +
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
                                        "                        data.cat_id = \"" + Workflow_Category_ID + "\";" + "\r\n" +
                                        "                        data.typ_id = \"0\";" + "\r\n" +
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
                                        "            $('#task_table_" + Workflow_Category_ID + "').DataTable().ajax.reload(null, false);" + "\r\n" +
                                        "        }, " + objSes.RefFreq + ");" + "\r\n\r\n";
            }
        }
    }
}