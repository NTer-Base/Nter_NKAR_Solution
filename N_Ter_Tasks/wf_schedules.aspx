<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid_wf.master" AutoEventWireup="true" CodeBehind="wf_schedules.aspx.cs" Inherits="N_Ter_Tasks.wf_schedules" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Workflow Schedules - Automations
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Workflow Schedules - Automations -
    <asp:Literal ID="ltrWorkflowName" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <asp:HiddenField ID="hndWP_SH_ID" runat="server" />
    <div class="row" style="margin-top: -18px">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="padding-xs-vr">
                        <button id="cmdNew" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Schedule Automation</button>
                        <asp:ModalPopupExtender ID="cmdNew_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlData" TargetControlID="cmdNew" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                        </asp:ModalPopupExtender>
                    </div>
                    <div class="table-responsive table-primary no-margin-b">
                        <asp:GridView ID="gvMain" runat="server" CssClass="table table-striped table-hover grid_table grid_dp non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvMain_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="WF_SH_ID" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <button id='cmdEdit' type='submit' runat="server" class="btn btn-primary btn-xs" title="Edit"><i class="fa fa-edit button_icon"></i></button>
                                        <asp:ModalPopupExtender ID="cmdEdit_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlData" TargetControlID="cmdEdit" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                                        </asp:ModalPopupExtender>
                                    </ItemTemplate>
                                    <ItemStyle Width="22px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <button id='cmdDelete' type='submit' runat="server" class="btn btn-danger btn-xs" title="Delete"><i class="fa fa-trash-o button_icon"></i></button>
                                        <asp:ModalPopupExtender ID="cmdDelete_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDelete" TargetControlID="cmdDelete" CancelControlID="cmdCancel" BackgroundCssClass="at_modelpopup_background">
                                        </asp:ModalPopupExtender>
                                    </ItemTemplate>
                                    <ItemStyle Width="22px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Schedule_Name" HeaderText="Schedule Name" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlData" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_indent" class="at_modelpopup_indent">
            <div id="at_model_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdNo" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add/Edit Schedule Automation</h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Schedule Name</label>
                            <asp:DropDownList ID="cboSchedule" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Automatically Start a Task</label>
                            <div class="row">
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cboRecFrec" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="cboRecType" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">[Not Selected]</asp:ListItem>
                                        <asp:ListItem Value="1">Days</asp:ListItem>
                                        <asp:ListItem Value="2">Weeks</asp:ListItem>
                                        <asp:ListItem Value="3">Months</asp:ListItem>
                                        <asp:ListItem Value="4">Years</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3 mt8">Before Deadline</div>
                            </div>
                        </div>
                        <div id="divSchEnt" class="form-group">
                            <label>Copy <span id="schEntName"></span>&nbsp;to</label>
                            <asp:DropDownList ID="cboCopyEnt1" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">[N/A]</asp:ListItem>
                                <asp:ListItem Value="1">Task Special Field 1</asp:ListItem>
                                <asp:ListItem Value="2">Task Special Field 2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div id="divSchEnt2" class="form-group">
                            <label>Copy <span id="schEntName2"></span>&nbsp;to</label>
                            <asp:DropDownList ID="cboCopyEnt2" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">[N/A]</asp:ListItem>
                                <asp:ListItem Value="1">Task Special Field 1</asp:ListItem>
                                <asp:ListItem Value="2">Task Special Field 2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>
                                <asp:Literal ID="ltrEL2" runat="server"></asp:Literal>
                                Name</label>
                            <asp:DropDownList ID="cboEL2" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSave_Click" />
                    <asp:Button ID="cmdReset" runat="server" Text="Reset" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDelete" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_model_indent" class="at_modelpopup_indent">
            <div id="at_del_model_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete</h4>
                </div>
                <div id="at_del_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDelete" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDelete_Click" />
                    <asp:Button ID="cmdCancel" runat="server" Text="No" CssClass="btn btn-default" />
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
            AdjustPopupSize(167, 800, 'at_model');
            AdjustPopupSize(80, 400, 'at_del_model');
        }

        function ArrangeGrids() {
            $('.grid_dp').dataTable({
                "pageLength": 50,
                "order": [[2, 'asc']],
                "responsive": true,
                "autoWidth": true,
                "columnDefs": [
                    { 'orderable': false, targets: 0 },
                    { 'orderable': false, targets: 1 }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });

            $(".dataTables_filter").children().children('input').attr('placeholder', 'Search...');
        }

        function ClearControls() {
            clearDropDown(['<%=cboSchedule.ClientID%>', '<%=cboRecType.ClientID%>', '<%=cboRecFrec.ClientID%>', '<%=cboCopyEnt1.ClientID%>', '<%=cboCopyEnt2.ClientID%>', '<%=cboEL2.ClientID%>']);
            CheckAutoStart();
            return false;
        }

        function ResetControls() {
            if ($("#<%=hndWP_SH_ID.ClientID%>").val().trim() == '0') {
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
                url: "api/tasks/GetWFSchedule",
                data: { WF_SH_ID: $("#<%=hndWP_SH_ID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassed(result) {
            ClearControls();

            var resultObject = result;
            $('#<%=cboSchedule.ClientID%>').val(resultObject[0].Schedule_ID);
            $('#<%=cboRecType.ClientID%>').val(resultObject[0].Recurence_Type);
            $('#<%=cboRecFrec.ClientID%>').val(resultObject[0].Recurence_Frequency);
            $('#<%=cboCopyEnt1.ClientID%>').val(resultObject[0].Copy_Sch_Entity_1);
            $('#<%=cboCopyEnt2.ClientID%>').val(resultObject[0].Copy_Sch_Entity_2);
            $('#<%=cboEL2.ClientID%>').val(resultObject[0].Entity_L2_ID);
            CheckAutoStart();
        }

        function CheckAutoStart() {
            $('#divSchEnt').addClass('hide');
            $('#divSchEnt2').addClass('hide');
            if ($("#<%=cboSchedule.ClientID%>").val() != "0") {
                CheckScheduleFields();
            }
            else {
                $("#<%=cboCopyEnt1.ClientID%>").val('0');
                $("#<%=cboCopyEnt2.ClientID%>").val('0');
            }
        }

        function CheckScheduleFields() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetWorkflowSchedule",
                data: { Schedule_ID: $("#<%=cboSchedule.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus, jQxhr) {
                    var resultObject = data;
                    if (resultObject[0].Schedule_Entity_Name.trim() != '') {
                        $('#schEntName').html(resultObject[0].Schedule_Entity_Name.trim());
                        $('#divSchEnt').removeClass('hide');
                    }
                    else {
                        $('#divSchEnt').addClass('hide');
                        $("#<%=cboCopyEnt1.ClientID%>").val('0');

                    }
                    if (resultObject[0].Schedule_Entity2_Name.trim() != '') {
                        $('#schEntName2').html(resultObject[0].Schedule_Entity2_Name.trim());
                        $('#divSchEnt2').removeClass('hide');
                    }
                    else {
                        $('#divSchEnt2').addClass('hide');
                        $("#<%=cboCopyEnt2.ClientID%>").val('0');
                    }
                },
                failure: LoadValuesFailed
            });
        }
    </script>
</asp:Content>