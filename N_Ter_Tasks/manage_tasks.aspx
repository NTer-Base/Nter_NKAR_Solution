<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid.master" AutoEventWireup="true" CodeBehind="manage_tasks.aspx.cs" Inherits="N_Ter_Tasks.manage_tasks" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Manage Tasks
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Manage Tasks
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="panel panel-info">
        <div class="panel-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Workflow</label>
                        <asp:DropDownList ID="cboWorkflow" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div id="divSchedule" class="col-md-8 hide">
                    <div class="form-group">
                        <label id="scheduleName"></label>
                        <select id="cboSchedule" class="form-control" onchange="SaveScheduleLine();"></select>
                        <asp:HiddenField ID="hndScheduleLineID" runat="server" />
                    </div>
                </div>
                <div id="divFrom" class="col-md-4 hide">
                    <div class="form-group">
                        <label>Started Between</label>
                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control dtPicker"></asp:TextBox>
                    </div>
                </div>
                <div id="divTo" class="col-md-4 hide">
                    <div class="form-group">
                        <label>&nbsp;</label>
                        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control dtPicker"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row pb5">
                <div class="col-sm-12 text-right">
                    <asp:Button ID="cmdShow" runat="server" Text="Show Non Completed Tasks" CssClass="btn btn-primary" OnClick="cmdShow_Click" />
                    <asp:Button ID="cmdShowCompleted" runat="server" Text="Show Completed Tasks" CssClass="btn btn-primary" OnClick="cmdShowCompleted_Click" />
                    <asp:Button ID="cmdShowDiscard" runat="server" Text="Show Discarded Tasks" CssClass="btn btn-primary" OnClick="cmdShowDiscard_Click" />
                </div>
            </div>
        </div>
    </div>
    <div id="divResults" runat="server" class="panel panel-info">
        <div class="panel-body">
            <div class="row">
                <div class="col-md-5 text-bg">
                    <asp:Literal ID="ltrTasksType" runat="server"></asp:Literal>
                </div>
                <div class="col-md-7">
                    <div class="form-group-margin text-right">
                        <button class="btn btn-labeled btn-primary" onclick="return SelectAllTasks();">Select All</button>
                        <button class="btn btn-labeled btn-primary" onclick="return DeselectAllTasks();">Unselect All</button>
                    </div>
                </div>
                <div class="col-md-12">
                    <asp:HiddenField ID="hndSelected_Tasks" runat="server" />
                    <div class="table-responsive table-primary no-margin-b">
                        <table id="tblResults" class="table table-striped table-hover grid_table check_arrange grid_test non_full_width_table" data-size="non_full_width_table">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>Task Number</th>
                                    <th>Created On</th>
                                    <th>Workflow Name</th>
                                    <th>
                                        <asp:Literal ID="ltrEL2" runat="server"></asp:Literal></th>
                                    <th>
                                        <asp:Literal ID="ltrEx1" runat="server"></asp:Literal></th>
                                    <th>
                                        <asp:Literal ID="ltrEx2" runat="server"></asp:Literal></th>
                                    <th>Current Step</th>
                                    <th>Task Owner</th>
                                    <th>Due On</th>
                                    <th>Posted On</th>
                                    <th>Posted By</th>
                                    <th>
                                        <asp:Literal ID="ltrEL1" runat="server"></asp:Literal></th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
                <div class="col-md-12 mt10">
                    <div id="divCompletedStatusChange" runat="server" class="row">
                        <div class="col-md-6 text-right padding-xs-vr">
                            Change Status of <b>Selected Tasks</b> to
                        </div>
                        <div class="col-md-6">
                            <div class="input-group">
                                <asp:DropDownList ID="cboCompletedStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                <span class="input-group-btn">
                                    <asp:Button ID="cmdCompletedChangeState" runat="server" Text="Go" CssClass="btn btn-primary" />
                                    <asp:HiddenField ID="hndCompletedChangeState" runat="server" />
                                    <asp:ModalPopupExtender ID="hndCompletedChangeState_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuCompletedChangeState" PopupControlID="pnlCompletedChangeState" TargetControlID="hndCompletedChangeState" CancelControlID="cmdCancelCompChangeState" BackgroundCssClass="at_modelpopup_background">
                                    </asp:ModalPopupExtender>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div id="divDiscardedStatusChange" runat="server" class="row">
                        <div class="col-md-6 text-right padding-xs-vr">
                            Change Status of <b>Selected Tasks</b> to
                        </div>
                        <div class="col-md-6">
                            <div class="input-group">
                                <asp:DropDownList ID="cboDiscardedStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                <span class="input-group-btn">
                                    <asp:Button ID="cmdDiscardedChangeState" runat="server" Text="Go" CssClass="btn btn-primary" />
                                    <asp:HiddenField ID="hndDiscardedChangeState" runat="server" />
                                    <asp:ModalPopupExtender ID="hndDiscardedChangeState_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuDiscardedChangeState" PopupControlID="pnlDiscardedChangeState" TargetControlID="hndDiscardedChangeState" CancelControlID="cmdCancelDiscardedChangeState" BackgroundCssClass="at_modelpopup_background">
                                    </asp:ModalPopupExtender>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div id="divStatusChange" runat="server" class="row">
                        <div class="col-md-6 text-right padding-xs-vr">
                            Change Status of <b>Selected Tasks</b> to
                        </div>
                        <div class="col-md-6">
                            <div class="input-group">
                                <asp:DropDownList ID="cboStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                <span class="input-group-btn">
                                    <asp:Button ID="cmdChangeState" runat="server" Text="Go" CssClass="btn btn-primary" />
                                    <asp:HiddenField ID="hndChangeState" runat="server" />
                                    <asp:ModalPopupExtender ID="hndChangeState_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuChangeState" PopupControlID="pnlChangeState" TargetControlID="hndChangeState" CancelControlID="cmdCancelChangeState" BackgroundCssClass="at_modelpopup_background">
                                    </asp:ModalPopupExtender>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlChangeState" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_indent" class="at_modelpopup_indent">
            <div id="at_model_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Confirm Status Change</h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdConfirmStatusChange" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdChangeState_Click" />
                    <asp:Button ID="cmdCancelChangeState" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlCompletedChangeState" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_comp_model_indent" class="at_modelpopup_indent">
            <div id="at_comp_model_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Confirm Status Change</h4>
                </div>
                <div id="at_comp_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdConfirmCompStatusChange" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdConfirmCompStatusChange_Click" />
                    <asp:Button ID="cmdCancelCompChangeState" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDiscardedChangeState" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_comp_disc_model_indent" class="at_modelpopup_indent">
            <div id="at_comp_disc_model_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Confirm Status Change</h4>
                </div>
                <div id="at_comp_disc_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdConfirmDiscardedChangeState" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdConfirmDiscardedChangeState_Click" />
                    <asp:Button ID="cmdCancelDiscardedChangeState" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script type="text/javascript">
        $(function () {
            $(window).resize(function () {
                ArrangePopups();
            });
        });

        init.push(function () {
            WorkflowChanged();
            ArrangePopups();
            ArrangeGrids();
            DeselectAllTasks();
            $('.check_arrange').on('draw.dt', function () {
                ArrangeCheckboxes();
            });
        });

        function ArrangePopups() {
            AdjustPopupSize(80, 400, 'at_model');
            AdjustPopupSize(80, 400, 'at_comp_model');
            AdjustPopupSize(80, 400, 'at_comp_disc_model');
        }

        function WorkflowChanged() {
            var SelectedWF = $('#<%=cboWorkflow.ClientID%>').val();
            if (Number(SelectedWF.split('|')[1].trim()) > 0) {
                $('#divSchedule').removeClass('hide');
                $('#divFrom').addClass('hide');
                $('#divTo').addClass('hide');
                $('#cboSchedule').empty();
                $.ajax({
                    type: "GET",
                    url: "api/tasks/GetAllScheduleData",
                    data: { Schedule_ID: SelectedWF.split('|')[1].trim() },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: CheckSchedulesPassed,
                    failure: WebMethodFailed
                });
            }
            else {
                $('#divSchedule').addClass('hide');
                $('#divFrom').removeClass('hide');
                $('#divTo').removeClass('hide');
            }
        }

        function WebMethodFailed(result) { }

        function CheckSchedulesPassed(result) {
            var sch_id = '';
            var ItemSelected = false;
            if ($('#<%=hndScheduleLineID.ClientID%>').val().trim() != "") {
                sch_id = $('#<%=hndScheduleLineID.ClientID%>').val().trim();
            }
            var resultObject = result;
            $('#scheduleName').text(resultObject[0].Schedule_Entity_Name);
            var resultObjectSub = resultObject[0].Schedule_Data;
            if (resultObjectSub.length > 0) {
                for (var i = 0; i < resultObjectSub.length; i++) {
                    if (sch_id.trim() == resultObjectSub[i].Schedule_Line_ID.trim()) {
                        ItemSelected = true;
                    }
                    $('#cboSchedule').append(
                        $('<option>', {
                            value: resultObjectSub[i].Schedule_Line_ID.trim(),
                            text: resultObjectSub[i].Entity_Data.trim()
                        }, '<option/>'))
                }
            }
            else {
                if (sch_id.trim() == '0') {
                    ItemSelected = true;
                }
                $('#cboSchedule').append(
                    $('<option>', {
                        value: '0',
                        text: '[Not Available]'
                    }, '<option/>'))
            }
            if (ItemSelected == true) {
                $('#cboSchedule').val(sch_id);
            }
            SaveScheduleLine();
        }

        function SaveScheduleLine() {
            $('#<%=hndScheduleLineID.ClientID%>').val($('#cboSchedule').val());
        }

        function ValidateChange(taskType) {
            if ($('#<%=hndSelected_Tasks.ClientID%>').val().trim() == "") {
                ShowError('No Tasks Selected');
            }
            else {
                if (taskType == 1) {
                    $find("mpuCompletedChangeState").show();
                }
                else if (taskType == 2) {
                    $find("mpuChangeState").show();
                }
                else {
                    $find("mpuDiscardedChangeState").show();
                }
            }
            return false;
        }

        function ArrangeGrids() {
            $("#tblResults").dataTable({
                pageLength: 50,
                order: [[<%=SortField%>, "<%=SortDirection%>"]],
                responsive: true,
                autoWidth: true,
                processing: true,
                serverSide: true,
                ajax:
                {
                    url: "api/tasks/GetTasksManage",
                    contentType: "application/json",
                    type: "GET",
                    dataType: "JSON",
                    data: function (data) {
                        for (var i = 0, len = data.columns.length; i < len; i++) {
                            delete data.columns[i].search;
                            delete data.columns[i].searchable;
                            delete data.columns[i].orderable;
                            delete data.columns[i].name;
                        }
                        delete data.search.regex;
                        data.filter_q = "<%=ViewState["sh"]%>";
                    }
                },
                columns: [
                    { data: "Check_Box" },
                    { data: "Task_Number" },
                    { data: "Task_Date" },
                    { data: "Workflow_Name" },
                    { data: "Display_Name" },
                    { data: "Extra_Field_Value" },
                    { data: "Extra_Field_Value2" },
                    { data: "Current_Step" },
                    { data: "Created_By" },
                    { data: "ETB_Value" },
                    { data: "Posted_Date" },
                    { data: "Edited_User" },
                    { data: "Entity_L1_Name" }
                ],
                columnDefs: [
                    { orderable: false, width: "22px", targets: 0 },
                    { orderable: false, width: "22px", targets: 12 }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });

            setInterval(function () {
                $('#task_table_1').DataTable().ajax.reload(null, false);
            }, <%=RefreshFreq%>);

            $(".dataTables_filter").children().children('input').attr('placeholder', 'Search...');
        }

        function ArrangeCheckboxes() {
            $('.checkboxlist').switcher({
                on_state_content: '<span class="fa fa-check"></span>',
                off_state_content: '<span class="fa fa-times"></span>'
            });
            UpdateExistingSelection();
        }

        function SelectAllTasks() {
            var selectedIDs = $("#<%=hndSelected_Tasks.ClientID%>").val();
            $(".tsks").each(function () {
                onCheckBox(this);
                if (selectedIDs.startsWith($(this).attr('data-id').trim() + "|") == false && selectedIDs.indexOf("|" + $(this).attr('data-id').trim() + "|") < 0) {
                    selectedIDs = selectedIDs + $(this).attr('data-id').trim() + "|";
                }
            });
            $('#<%=hndSelected_Tasks.ClientID%>').val(selectedIDs);
            return false;
        }

        function DeselectAllTasks() {
            var selectedIDs = $("#<%=hndSelected_Tasks.ClientID%>").val();
            $(".tsks").each(function () {
                offCheckBox(this);
                if (selectedIDs.startsWith($(this).attr('data-id').trim() + "|") || selectedIDs.indexOf("|" + $(this).attr('data-id').trim() + "|") > 0) {
                    selectedIDs = selectedIDs.replace($(this).attr('data-id').trim() + "|", "");
                }
            });
            $('#<%=hndSelected_Tasks.ClientID%>').val(selectedIDs);
            return false;
        }

        function UpdateExistingSelection() {
            var selectedIDs = $("#<%=hndSelected_Tasks.ClientID%>").val();
            $(".tsks").each(function () {
                if (selectedIDs.startsWith($(this).attr("data-id") + "|") || selectedIDs.indexOf("|" + $(this).attr("data-id") + "|") > 0) {
                    onCheckBox(this);
                }
            });
        }

        function UpdateSelection(control) {
            var selectedIDs = $("#<%=hndSelected_Tasks.ClientID%>").val();
            var thisID = $(control).attr("data-id");

            if (selectedIDs.startsWith(thisID + "|") || selectedIDs.indexOf("|" + thisID + "|") > 0) {
                selectedIDs = selectedIDs.replace(thisID + "|", "");
            }
            else {
                selectedIDs = selectedIDs + thisID + "|";
            }
            $("#<%=hndSelected_Tasks.ClientID%>").val(selectedIDs);
        }
    </script>
</asp:Content>