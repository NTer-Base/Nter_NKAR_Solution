<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid_dp.master" AutoEventWireup="true" CodeBehind="dp_workflows.aspx.cs" Inherits="N_Ter_Tasks.dp_workflows" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" Runat="Server">
    N-Ter-Tasks > Document Repo - Workflow Automations
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" Runat="Server">
    Document Repo - Workflow Automations -
    <asp:Literal ID="ltrRepoName" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" Runat="Server">
    <asp:HiddenField ID="hndDP_WF_ID" runat="server" />
    <div class="row" style="margin-top: -18px">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="padding-xs-vr">
                        <button id="cmdNew" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Workflow Action</button>
                        <asp:ModalPopupExtender ID="cmdNew_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlData" TargetControlID="cmdNew" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                        </asp:ModalPopupExtender>
                    </div>
                    <div class="table-responsive table-primary no-margin-b">
                        <asp:GridView ID="gvMain" runat="server" CssClass="table table-striped table-hover grid_table grid_dp non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvMain_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="DP_WF_ID" />
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
                                <asp:BoundField DataField="Workflow_Name" HeaderText="Workflow Name" />
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
                    <h4 class="panel-title">Add/Edit Document Repo Workflow Automation</h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Workflow</label>
                            <asp:DropDownList ID="cboWorkflow" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div id="divEF1" class="form-group">
                            <label>Special Field 1</label>
                            <asp:DropDownList ID="cboTags1" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div id="divEF2" class="form-group">
                            <label>Special Field 2</label>
                            <asp:DropDownList ID="cboTags2" runat="server" CssClass="form-control"></asp:DropDownList>
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
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" Runat="Server">
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
            clearDropDown(['<%=cboWorkflow.ClientID%>', '<%=cboTags1.ClientID%>', '<%=cboTags2.ClientID%>']);
            LoadWFConnectionPane();
            return false;
        }

        function ResetControls() {
            if ($("#<%=hndDP_WF_ID.ClientID%>").val().trim() == '0') {
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
                url: "api/tasks/GetDP_Workflow",
                data: { DP_WF_ID: $("#<%=hndDP_WF_ID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassed(result) {
            ClearControls();

            var resultObject = result;
            $('#<%=cboWorkflow.ClientID%>').val(resultObject[0].Workflow_ID);
            $('#<%=cboTags1.ClientID%>').val(resultObject[0].Extra_Field_1_Index_ID);
            $('#<%=cboTags2.ClientID%>').val(resultObject[0].Extra_Field_2_Index_ID);
            LoadWFConnectionPane();
        }

        function LoadWFConnectionPane() {
            if ($('#<%=cboWorkflow.ClientID%>').val() == '0') {
                $('#divEF1').addClass('hide');
                $('#<%=cboTags1.ClientID%>').val('0');
                $('#divEF2').addClass('hide');
                $('#<%=cboTags2.ClientID%>').val('0');
            }
            else {
                $.ajax({
                    type: "GET",
                    url: "api/tasks/getWorkflowExtraFields",
                    data: { Walkflow_ID: $('#<%=cboWorkflow.ClientID%>').val() },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus, jQxhr) {
                        var resultObject = data;
                        if (resultObject[0].Exrta_Field_Naming.trim() == '') {
                            $('#divEF1').addClass('hide');
                            $('#<%=cboTags1.ClientID%>').val('0');
                        }
                        else {
                            $('#divEF1').removeClass('hide');
                        }
                        if (resultObject[0].Exrta_Field2_Naming.trim() == '') {
                            $('#divEF2').addClass('hide');
                            $('#<%=cboTags2.ClientID%>').val('0');
                        }
                        else {
                            $('#divEF2').removeClass('hide');
                        }
                    },
                    failure: LoadValuesFailed
                });
            }
        }
    </script>
</asp:Content>