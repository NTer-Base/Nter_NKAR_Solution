<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin.master" AutoEventWireup="true" CodeBehind="document_projects.aspx.cs" Inherits="N_Ter_Tasks.document_projects" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Edit Document Repos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Edit Document Repos
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <asp:HiddenField ID="hndDocProjectID" runat="server" />
    <div class="panel panel-info">
        <div class="panel-body">
            <div class="padding-xs-vr">
                <button id="cmdNew" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Document Repo</button>
                <asp:ModalPopupExtender ID="cmdNew_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlData" TargetControlID="cmdNew" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                </asp:ModalPopupExtender>
            </div>
            <div class="table-responsive table-primary no-margin-b">
                <asp:GridView ID="gvMain" runat="server" CssClass="table table-striped table-hover grid_table grid_doc non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvMain_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Document_Project_ID" />
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
                                <asp:Literal ID="ltrActions" runat="server"></asp:Literal>
                            </ItemTemplate>
                            <ItemStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <button id='cmdDelete' type='submit' runat="server" class="btn btn-danger btn-xs" title="Delete"><i class="fa fa-trash-o button_icon"></i></button>
                                <asp:ModalPopupExtender ID="cmdDelete_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDelete" TargetControlID="cmdDelete" CancelControlID="cmdCancel" BackgroundCssClass="at_modelpopup_background">
                                </asp:ModalPopupExtender>
                            </ItemTemplate>
                            <ItemStyle Width="22px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Doc_Project_Name" HeaderText="Repo Name" />
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
                        <asp:Button ID="cmdNo" runat="server" Text="&times;" CssClass="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add/Edit Document Repo</h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Repo Name</label>
                            <asp:TextBox ID="txtProjectName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>User Group</label>
                            <asp:DropDownList ID="cboUserGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Help Text - Guest Uploads</label>
                            <asp:TextBox ID="txtGuestHelp" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Retention Period</label>
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtRetentionAmount" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboRetentionType" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="3">Months</asp:ListItem>
                                        <asp:ListItem Value="4">Years</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
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
            $('.grid_doc').dataTable({
                "pageLength": 50,
                "order": [[3, 'asc']],
                "responsive": true,
                "autoWidth": true,
                "columnDefs": [
                    { 'orderable': false, targets: 0 },
                    { 'orderable': false, targets: 1 },
                    { 'orderable': false, targets: 2 }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });

            $(".dataTables_filter").children().children('input').attr('placeholder', 'Search...');
        }

        function ClearControls() {
            clearTextBox(['<%=txtProjectName.ClientID%>', '<%=txtGuestHelp.ClientID%>', '<%=txtRetentionAmount.ClientID%>']);
            clearDropDown(['<%=cboUserGroup.ClientID%>', '<%=cboRetentionType.ClientID%>']);
            return false;
        }

        function ResetControls() {
            if ($("#<%=hndDocProjectID.ClientID%>").val().trim() == '0') {
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
                url: "api/tasks/GetDocProject",
                data: { Document_Project_ID: $("#<%=hndDocProjectID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassed(result) {
            ClearControls();

            var resultObject = result;
            $('#<%=cboUserGroup.ClientID%>').val(resultObject[0].User_Group_ID);
            $('#<%=txtProjectName.ClientID%>').val(resultObject[0].Doc_Project_Name);
            $('#<%=txtGuestHelp.ClientID%>').val(resultObject[0].Guest_Help);
            $('#<%=cboRetentionType.ClientID%>').val(resultObject[0].Retention_Period_Type);
            $('#<%=txtRetentionAmount.ClientID%>').val(resultObject[0].Retention_Amount);
        }
    </script>
</asp:Content>
