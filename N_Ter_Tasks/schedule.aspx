<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid.master" AutoEventWireup="true" CodeBehind="schedule.aspx.cs" Inherits="N_Ter_Tasks.schedule" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Workflow Schedules
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Workflow Schedule
    <asp:Label ID="lblScheduleName" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="panel panel-info">
        <div class="panel-body">
            <div class="padding-xs-vr">
                <button id="cmdNewRecord" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Record</button>
                <asp:ModalPopupExtender ID="cmdNewRecord_ModalPopupExtender" runat="server" BehaviorID="mpuRec" Enabled="True" TargetControlID="cmdNewRecord" PopupControlID="pnlScheduleData" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                </asp:ModalPopupExtender>
                <asp:HiddenField ID="hndDelete" runat="server" />
                <asp:ModalPopupExtender ID="hndDelete_ModalPopupExtender" runat="server" BehaviorID="mpuDel" Enabled="True" PopupControlID="pnlDeleteSchedule" TargetControlID="hndDelete" CancelControlID="cmdCalcelDelSchedule" BackgroundCssClass="at_modelpopup_background">
                </asp:ModalPopupExtender>
            </div>
            <asp:HiddenField ID="hndScheduleLineID" runat="server" />
            <div class="table-responsive table-primary no-margin-b">
                <table id="tblSchedule" class="table table-striped table-hover grid_table grid_test non_full_width_table" data-size="non_full_width_table">
                    <thead>
                        <tr>
                            <th></th>
                            <th></th>
                            <%=Schedule_Field_Names%>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlScheduleData" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_indent" class="at_modelpopup_indent">
            <div id="at_model_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdNo" runat="server" Text="&times;" CssClass="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add/Edit Workflow Schedule Data</h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <asp:Label ID="lblEntityName" runat="server" Text=""></asp:Label>
                            <asp:TextBox ID="txtEntityName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div id="divEntity2" runat="server" class="form-group">
                            <asp:Label ID="lblEntity2Name" runat="server" Text=""></asp:Label>
                            <asp:TextBox ID="txtEntity2Name" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblEntityDate" runat="server"></asp:Label>
                            <asp:TextBox ID="txtEntityDate" runat="server" CssClass="form-control dtPicker"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblEntityTime" runat="server" Text=""></asp:Label>
                            <asp:DropDownList ID="cboEntityTime" runat="server" CssClass="form-control">
                                <asp:ListItem>00:30</asp:ListItem>
                                <asp:ListItem>01:00</asp:ListItem>
                                <asp:ListItem>01:30</asp:ListItem>
                                <asp:ListItem>02:00</asp:ListItem>
                                <asp:ListItem>02:30</asp:ListItem>
                                <asp:ListItem>03:00</asp:ListItem>
                                <asp:ListItem>03:30</asp:ListItem>
                                <asp:ListItem>04:00</asp:ListItem>
                                <asp:ListItem>04:30</asp:ListItem>
                                <asp:ListItem>05:00</asp:ListItem>
                                <asp:ListItem>05:30</asp:ListItem>
                                <asp:ListItem>06:00</asp:ListItem>
                                <asp:ListItem>06:30</asp:ListItem>
                                <asp:ListItem>07:00</asp:ListItem>
                                <asp:ListItem>07:30</asp:ListItem>
                                <asp:ListItem>08:00</asp:ListItem>
                                <asp:ListItem>08:30</asp:ListItem>
                                <asp:ListItem>09:00</asp:ListItem>
                                <asp:ListItem>09:30</asp:ListItem>
                                <asp:ListItem>10:00</asp:ListItem>
                                <asp:ListItem>10:30</asp:ListItem>
                                <asp:ListItem>11:00</asp:ListItem>
                                <asp:ListItem>11:30</asp:ListItem>
                                <asp:ListItem>12:00</asp:ListItem>
                                <asp:ListItem>12:30</asp:ListItem>
                                <asp:ListItem>13:00</asp:ListItem>
                                <asp:ListItem>13:30</asp:ListItem>
                                <asp:ListItem>14:00</asp:ListItem>
                                <asp:ListItem>14:30</asp:ListItem>
                                <asp:ListItem>15:00</asp:ListItem>
                                <asp:ListItem>15:30</asp:ListItem>
                                <asp:ListItem>16:00</asp:ListItem>
                                <asp:ListItem>16:30</asp:ListItem>
                                <asp:ListItem>17:00</asp:ListItem>
                                <asp:ListItem>17:30</asp:ListItem>
                                <asp:ListItem>18:00</asp:ListItem>
                                <asp:ListItem>18:30</asp:ListItem>
                                <asp:ListItem>19:00</asp:ListItem>
                                <asp:ListItem>19:30</asp:ListItem>
                                <asp:ListItem>20:00</asp:ListItem>
                                <asp:ListItem>20:30</asp:ListItem>
                                <asp:ListItem>21:00</asp:ListItem>
                                <asp:ListItem>21:30</asp:ListItem>
                                <asp:ListItem>22:00</asp:ListItem>
                                <asp:ListItem>22:30</asp:ListItem>
                                <asp:ListItem>23:00</asp:ListItem>
                                <asp:ListItem>23:30</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSaveSchedule" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSaveSchedule_Click" />
                    <asp:Button ID="cmdResetSchedule" runat="server" Text="Reset" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDeleteSchedule" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_model_indent" class="at_modelpopup_indent">
            <div id="at_del_model_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete - Schedule</h4>
                </div>
                <div id="at_del_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDeleteSchedule" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDeleteSchedule_Click" />
                    <asp:Button ID="cmdCalcelDelSchedule" runat="server" Text="No" CssClass="btn btn-default" />
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
            ArrangePopups();
            ArrangeGrids();
        });

        function ArrangePopups() {
            AdjustPopupSize(167, 600, 'at_model');
            AdjustPopupSize(80, 400, 'at_del_model');
        }

        function ArrangeGrids() {
            $('#tblSchedule').dataTable({
                pageLength: 50,
                order: [[2, 'asc']],
                responsive: true,
                autoWidth: true,
                processing: true,
                serverSide: true,
                ajax:
                {
                    url: "api/tasks/GetWorkflowScheduleData",
                    data: function (data) {
                        for (var i = 0, len = data.columns.length; i < len; i++) {
                            delete data.columns[i].search;
                            delete data.columns[i].searchable;
                            delete data.columns[i].orderable;
                            delete data.columns[i].name;
                        }
                        delete data.search.regex;
                        data.shd_id = "<%=Schedule_ID%>";
                        data.ent_d2 = "<%=HasEnt2Data%>";
                    },
                    contentType: "application/json",
                    type: "GET",
                    dataType: "JSON"
                },
                columns: [
                    { data: "Edit_Button" },
                    { data: "Delete_Button" }<%=Schedule_Fields%>
                ],
                columnDefs: [
                    { orderable: false, width: "22px", targets: 0 },
                    { orderable: false, width: "22px", targets: 1 },
                    { type: 'de_datetime', targets: <%=LastFieldIndex%> }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });
            setInterval(function () {
                $('#tblSchedule').DataTable().ajax.reload(null, false);
            }, <%=Refresh_Frequency%>);

            $(".dataTables_filter").children().children('input').attr('placeholder', 'Search...');
        }

        function ClearControls() {
            clearTextBox(['<%=txtEntityName.ClientID%>', '<%=txtEntity2Name.ClientID%>', '<%=txtEntityDate.ClientID%>']);
            clearDropDown(['<%=cboEntityTime.ClientID%>']);
            clearDateTextBox(['<%=txtEntityDate.ClientID%>']);
            $('#<%=txtEntityDate.ClientID%>').keyup();
            return false;
        }

        function ResetControls() {
            if ($("#<%=hndScheduleLineID.ClientID%>").val().trim() == '0') {
                ClearControls();
            }
            else {
                LoadValues();
            }
            return false;
        }

        function LoadValues() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetWorkflowScheduleDataItem",
                data: { Schedule_Line_ID: $("#<%=hndScheduleLineID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassed(result) {
            ClearControls();
            var resultObject = result;
            $('#<%=txtEntityName.ClientID%>').val(resultObject[0].Entity_Data);
            $('#<%=txtEntity2Name.ClientID%>').val(resultObject[0].Entity_Data2);
            $('#<%=txtEntityDate.ClientID%>').val(resultObject[0].Entity_Data_Date);
            $('#<%=cboEntityTime.ClientID%>').val(resultObject[0].Entity_Data_Time);
            $('#<%=txtEntityDate.ClientID%>').keyup();
        }

        function editRec(recId) {
            $("#<%=hndScheduleLineID.ClientID%>").val(recId);
            $find("mpuRec").show();
            LoadValues();
            return false;
        }

        function deleteRec(recId) {
            $("#<%=hndScheduleLineID.ClientID%>").val(recId);
            $find("mpuDel").show();
            return false;
        }
    </script>
</asp:Content>