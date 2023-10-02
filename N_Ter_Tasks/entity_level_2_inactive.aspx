<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid.master" AutoEventWireup="true" CodeBehind="entity_level_2_inactive.aspx.cs" Inherits="N_Ter_Tasks.entity_level_2_inactive" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Test-Tasks > Inactive
    <asp:Literal ID="ltrEL2" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
    <link href="assets/stylesheets/pages.min.css" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Inactive
    <asp:Literal ID="ltrEL2_2" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <asp:HiddenField ID="hndEL2_ID" runat="server" />
    <div class="panel panel-info">
        <div class="panel-body">
            <div class="padding-xs-vr text-right">
                <button id="cmdShowActive" runat="server" class="btn btn-labeled btn-success" onserverclick="cmdShowActive_ServerClick">
                    <span class="btn-label icon fa fa-unlock"></span>Show Active
                <asp:Literal ID="ltrEL2_3" runat="server"></asp:Literal></button>
                <asp:HiddenField ID="hndActivate" runat="server" />
                <asp:ModalPopupExtender ID="hndActivate_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuActivate" PopupControlID="pnlActivate" TargetControlID="hndActivate" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                </asp:ModalPopupExtender>
            </div>
            <div class="table-responsive table-primary no-margin-b">
                <table id="tblEntityL2" class="table table-striped table-hover grid_table grid_test full_width_table" data-cat="4" data-size="full_width_table">
                    <thead>
                        <tr>
                            <th></th>
                            <th>
                                <asp:Literal ID="ltrEntityL2_Code" runat="server"></asp:Literal></th>
                            <th>
                                <asp:Literal ID="ltrEntityL2_Name" runat="server"></asp:Literal></th>
                            <th>
                                <asp:Literal ID="ltrEntityL1_Name" runat="server"></asp:Literal></th>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlActivate" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_act_model_indent" class="at_modelpopup_indent">
            <div id="at_act_model_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Activate Company</h4>
                </div>
                <div id="at_act_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdActivateEL2" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdActivateEL2_Click" />
                    <asp:Button ID="cmdNo" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script type="text/javascript">
        $(function () {
            $(window).resize(function () {
                AdjustPopupSize(80, 400, 'at_act_model');
            });
        });

        init.push(function () {
            AdjustPopupSize(80, 400, 'at_act_model');

            $('#tblEntityL2').dataTable({
                pageLength: 50,
                order: [[1, 'asc']],
                responsive: true,
                autoWidth: true,
                processing: true,
                serverSide: true,
                ajax:
                {
                    url: "api/tasks/GetInactiveEntityL2s",
                    data: function (data) {
                        for (var i = 0, len = data.columns.length; i < len; i++) {
                            delete data.columns[i].search;
                            delete data.columns[i].searchable;
                            delete data.columns[i].orderable;
                            delete data.columns[i].name;
                        }
                        delete data.search.regex;
                    },
                    contentType: "application/json",
                    type: "GET",
                    dataType: "JSON"
                },
                columns: [
                    { data: "Deactivate_Button" },
                    { data: "Entity_Code" },
                    { data: "Display_Name" },
                    { data: "Entity_L1_Name" }
                ],
                columnDefs: [
                    { orderable: false, width: "22px", targets: 0 }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });
        });

        function activateEl2(recId) {
            $("#<%=hndEL2_ID.ClientID%>").val(recId);
            $find("mpuActivate").show();
            return false;
        }
    </script>
</asp:Content>