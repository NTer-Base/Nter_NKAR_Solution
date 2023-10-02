<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid_wf.master" AutoEventWireup="true" CodeBehind="folder_read_rules.aspx.cs" Inherits="N_Ter_Tasks.folder_read_rules" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Folder Reading Rules
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Folder Reading Rules -
    <asp:Literal ID="ltrWorkflowName" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <asp:HiddenField ID="hndRuleID" runat="server" />
    <div class="panel panel-info" style="margin-top: -18px">
        <div class="panel-body">
            <div class="padding-xs-vr">
                <button id="cmdNew" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Folder Read Rule</button>
                <asp:ModalPopupExtender ID="cmdNew_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlData" TargetControlID="cmdNew" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                </asp:ModalPopupExtender>
            </div>
            <div class="table-responsive table-primary no-margin-b">
                <asp:GridView ID="gvMain" runat="server" CssClass="table table-striped table-hover grid_table grid_wf_categories non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvMain_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Rule_ID" />
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
                        <asp:BoundField DataField="Display_Name" HeaderText="Display_Name" />
                        <asp:BoundField DataField="Folder_Name" HeaderText="Folder to Read" />
                        <asp:BoundField DataField="Criteria_Text" HeaderText="Criteria" />
                    </Columns>
                </asp:GridView>
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
                    <h4 class="panel-title">Add/Edit Folder Read Rule</h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Folder to Read</label>
                            <asp:DropDownList ID="cboFolder" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <label>Conditions to Evaluate</label>
                                <table class="table">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="cboRule_Type_1" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="1">File Name Contains</asp:ListItem>
                                                    <asp:ListItem Value="2">File Name Starts With</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCheck_Word_1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="cboRule_Type_2" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">[No Validation]</asp:ListItem>
                                                    <asp:ListItem Value="1">File Name Contains</asp:ListItem>
                                                    <asp:ListItem Value="2">File Name Starts With</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCheck_Word_2" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="cboRule_Type_3" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">[No Validation]</asp:ListItem>
                                                    <asp:ListItem Value="1">File Name Contains</asp:ListItem>
                                                    <asp:ListItem Value="2">File Name Starts With</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCheck_Word_3" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="form-group pb5">
                            <div class="input-group">
                                <span class="input-group-addon">If</span>
                                <asp:DropDownList ID="cboConditionType" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="1">All Conditions Evaluate to True</asp:ListItem>
                                    <asp:ListItem Value="2">One Conditions Evaluate to True</asp:ListItem>
                                    <asp:ListItem Value="3">None of the Conditions Evaluate to True</asp:ListItem>
                                </asp:DropDownList>
                                <span class="input-group-addon">Then</span>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>Create a Task for -
                                <asp:Literal ID="ltrEl2_1" runat="server"></asp:Literal></label>
                            <asp:DropDownList ID="cboEl2" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label># of Days to Complete the Task</label>
                            <asp:TextBox ID="txtDaysToFinish" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
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
            $('.grid_wf_categories').dataTable({
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
            clearTextBox(['<%=txtCheck_Word_1.ClientID%>', '<%=txtCheck_Word_2.ClientID%>', '<%=txtCheck_Word_3.ClientID%>', '<%=txtDaysToFinish.ClientID%>']);
            clearDropDown(['<%=cboFolder.ClientID%>', '<%=cboRule_Type_1.ClientID%>', '<%=cboRule_Type_2.ClientID%>', '<%=cboRule_Type_3.ClientID%>', '<%=cboEl2.ClientID%>', '<%=cboConditionType.ClientID%>']);
            return false;
        }

        function ResetControls() {
            if ($("#<%=hndRuleID.ClientID%>").val().trim() == '0') {
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
                url: "api/tasks/GetFolderReadRule",
                data: { Rule_ID: $("#<%=hndRuleID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassed(result) {
            ClearControls();

            var resultObject = result;
            $('#<%=cboConditionType.ClientID%>').val(resultObject[0].Condition_Type);
            $('#<%=cboEl2.ClientID%>').val(resultObject[0].Entity_L2_ID);
            $('#<%=cboRule_Type_1.ClientID%>').val(resultObject[0].Rule_Type_1);
            $('#<%=txtCheck_Word_1.ClientID%>').val(resultObject[0].Check_Word_1);
            $('#<%=cboRule_Type_2.ClientID%>').val(resultObject[0].Rule_Type_2);
            $('#<%=txtCheck_Word_2.ClientID%>').val(resultObject[0].Check_Word_2);
            $('#<%=cboRule_Type_3.ClientID%>').val(resultObject[0].Rule_Type_3);
            $('#<%=txtCheck_Word_3.ClientID%>').val(resultObject[0].Check_Word_3);
            if ($("#<%=cboFolder.ClientID%> option[value='" + resultObject[0].Folder_Name + "']").length > 0) {
                $('#<%=cboFolder.ClientID%>').val(resultObject[0].Folder_Name);
            }
            $('#<%=txtDaysToFinish.ClientID%>').val(resultObject[0].Days_To_Deadline);
        }
    </script>
</asp:Content>