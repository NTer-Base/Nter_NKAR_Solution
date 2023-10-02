<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid_dp.master" AutoEventWireup="true" CodeBehind="document_project.aspx.cs" Inherits="N_Ter_Tasks.document_project" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Document Repo Tags
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Document Repo Tags
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="row" style="margin-top: -18px">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <span class="panel-title">Tags</span>
                    <div class="at_modelpopup_add">
                        <button id="cmdNewTag" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Tag</button>
                        <asp:ModalPopupExtender ID="cmdNewTag_ModalPopupExtender" runat="server" Enabled="True" TargetControlID="cmdNewTag" PopupControlID="pnlTagData" CancelControlID="cmdCancelTag" BackgroundCssClass="at_modelpopup_background">
                        </asp:ModalPopupExtender>
                    </div>
                </div>
                <div class="panel-body">
                    <asp:HiddenField ID="hndTagID" runat="server" />
                    <div class="table-responsive no-margin-b">
                        <asp:GridView ID="gvTags" runat="server" CssClass="table table-striped table-hover grid_table no-margin-b non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvTags_RowDataBound" RowStyle-CssClass="group tag_row">
                            <Columns>
                                <asp:BoundField DataField="Document_Project_Index_ID" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <button id='cmdEdit' type='submit' runat="server" class="btn btn-primary btn-xs" title="Edit"><i class="fa fa-edit button_icon"></i></button>
                                        <asp:ModalPopupExtender ID="cmdEdit_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlTagData" TargetControlID="cmdEdit" CancelControlID="cmdCancelTag" BackgroundCssClass="at_modelpopup_background">
                                        </asp:ModalPopupExtender>
                                    </ItemTemplate>
                                    <ItemStyle Width="22px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <button id='cmdDelete' type='submit' runat="server" class="btn btn-danger btn-xs" title="Delete"><i class="fa fa-trash-o button_icon"></i></button>
                                        <asp:ModalPopupExtender ID="cmdDelete_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDeleteTag" TargetControlID="cmdDelete" CancelControlID="cmdCalcelDelTag" BackgroundCssClass="at_modelpopup_background">
                                        </asp:ModalPopupExtender>
                                    </ItemTemplate>
                                    <ItemStyle Width="22px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Tag_Name" HeaderText="Tag Name" />
                                <asp:BoundField DataField="Tag_TypeSP" HeaderText="Tag Type">
                                    <ItemStyle Width="140px" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <i class="fa fa-bars"></i>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlTagData" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_indent" class="at_modelpopup_indent">
            <div id="at_model_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCancelTag" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add/Edit Document Repo Tag</h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Tag Name</label>
                            <asp:TextBox ID="txtTagName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Tag Type</label>
                            <asp:DropDownList ID="cboTagType" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">Label Only</asp:ListItem>
                                <asp:ListItem Value="1">Yes/No (Dropdown)</asp:ListItem>
                                <asp:ListItem Value="13">Yes/No (Switch)</asp:ListItem>
                                <asp:ListItem Value="2">Text</asp:ListItem>
                                <asp:ListItem Value="3">Memo</asp:ListItem>
                                <asp:ListItem Value="4">Number</asp:ListItem>
                                <asp:ListItem Value="8">Currency</asp:ListItem>
                                <asp:ListItem Value="9">Percentage</asp:ListItem>
                                <asp:ListItem Value="5">Selection</asp:ListItem>
                                <asp:ListItem Value="6">Date</asp:ListItem>
                                <asp:ListItem Value="7">Time</asp:ListItem>
                                <asp:ListItem Value="12">Time Span</asp:ListItem>
                                <asp:ListItem Value="10">Master Table</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div id="divTypable" class="form-group">
                            <asp:CheckBox Text="" runat="server" ID="chkTypable" CssClass="checkboxlist" /><span style="padding-left: 10px">Enable Search</span>
                        </div>
                        <div id="default_text" class="form-group">
                            <label>Default Text</label>
                            <asp:TextBox ID="txtDefaultText" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div id="max_length" class="form-group">
                            <label>Maximum Lenght</label>
                            <asp:TextBox runat="server" CssClass="form-control" TextMode="Number" ID="txtTagMaxLength" Min="0" />
                        </div>
                        <div id="selection_text" class="form-group">
                            <label>Selection Text</label>
                            <asp:TextBox ID="txtSelectionText" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div id="masterTable" class="form-group">
                            <label>Master Table</label>
                            <asp:DropDownList ID="cboMasterTable" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Field Size</label>
                            <asp:DropDownList ID="cboFieldSize" runat="server" CssClass="form-control">
                                <asp:ListItem Value="1">Full Width</asp:ListItem>
                                <asp:ListItem Value="2">1/2 Width</asp:ListItem>
                                <asp:ListItem Value="3">1/3 Width</asp:ListItem>
                                <asp:ListItem Value="4">2/3 Width</asp:ListItem>
                                <asp:ListItem Value="5">1/4 Width</asp:ListItem>
                                <asp:ListItem Value="6">3/4 Width</asp:ListItem>
                                <asp:ListItem Value="7">1/6 Width</asp:ListItem>
                                <asp:ListItem Value="8">5/6 Width</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div id="help_text" class="form-group">
                            <label>Help Text</label>
                            <asp:TextBox ID="txtHelpText" runat="server" CssClass="form-control" TextMode="MultiLine" Style="height: 100px"></asp:TextBox>
                        </div>
                        <div id="requiredField" class="form-group">
                            <asp:CheckBox Text="" runat="server" ID="chkFieldRequired" CssClass="checkboxlist" /><span style="padding-left: 10px">Required Field</span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSaveTag" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSaveTag_Click" />
                    <asp:Button ID="cmdResetTag" runat="server" Text="Reset" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDeleteTag" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_indent" class="at_modelpopup_indent">
            <div id="at_del_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete - Tag</h4>
                </div>
                <div id="at_del_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDeleteTag" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDeleteTag_Click" />
                    <asp:Button ID="cmdCalcelDelTag" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script type="text/javascript">
        $(function () {
            $(window).resize(function () {
                AdjustPopups();
            });
        });

        init.push(function () {
            AdjustPopups();
            ArrangeTagSort();
        });

        function ArrangeTagSort() {
            $("#<%=gvTags.ClientID%> tbody").attr('id', 'table_tags');
            $("#table_tags").sortable({
                axis: "y",
                handle: "i",
                stop: function (event, ui) {
                    ui.item.children("i").triggerHandler("focusout");
                    var strIDs = "";
                    $(".tag_row").each(function () {
                        strIDs = strIDs + $(this).attr('data-id') + '|';
                    });

                    $.ajax({
                        type: "GET",
                        url: "api/tasks/UpdateDPTagSort",
                        data: { Document_Project_ID: '<%=Convert.ToString(ViewState["rid"])%>', Sort_List: strIDs },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: UpdateSortPassed,
                        failure: LoadValuesFailed
                    });
                }
            });
        }

        function UpdateSortPassed(result) { }

        function AdjustPopups() {
            AdjustPopupSize(167, 600, 'at_model');
            AdjustPopupSize(80, 400, 'at_del');
        }

        function ClearControls() {
            clearTextBox(['<%=txtTagName.ClientID%>', '<%=txtSelectionText.ClientID%>', '<%=txtDefaultText.ClientID%>', '<%=txtTagMaxLength.ClientID%>', '<%=txtHelpText.ClientID%>']);
            clearDropDown(['<%=cboTagType.ClientID%>', '<%=cboFieldSize.ClientID%>', '<%=cboMasterTable.ClientID%>']);
            clearCheckBox(['<%=chkFieldRequired.ClientID%>', '<%=chkTypable.ClientID%>']);
            return false;
        }

        function ResetControls() {
            if ($("#<%=hndTagID.ClientID%>").val().trim() == '0') {
                ClearControls();
                ShowSelectionText();
            }
            else {
                LoadValues();
            }
            return false;
        }

        function LoadValues() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetDocProjectTag",
                data: { Document_Project_Index_ID: $("#<%=hndTagID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassed(result) {
            ClearControls();

            var resultObject = result;
            $('#<%=txtTagName.ClientID%>').val(resultObject[0].Field_Name);
            $('#<%=cboTagType.ClientID%>').val(resultObject[0].Field_Type);
            $('#<%=cboFieldSize.ClientID%>').val(resultObject[0].Field_Size);
            $('#<%=cboMasterTable.ClientID%>').val(resultObject[0].Master_Table_ID);
            $('#<%=txtSelectionText.ClientID%>').val(resultObject[0].Selection_Texts);
            $('#<%=txtDefaultText.ClientID%>').val(resultObject[0].Default_Texts);
            $('#<%=txtHelpText.ClientID%>').val(resultObject[0].Help_Text);
            if (resultObject[0].IsRequired == true) {
                onCheckBox('#<%=chkFieldRequired.ClientID%>');
            }
            if (resultObject[0].isTypable == true) {
                onCheckBox('#<%=chkTypable.ClientID%>');
            }
            if (parseInt(resultObject[0].Max_Length) > 0) {
                $('#<%=txtTagMaxLength.ClientID%>').val(resultObject[0].Max_Length);
            }
            ShowSelectionText();
        }

        function ShowSelectionText() {
            if ($('#<%=cboTagType.ClientID%>').val() == "0") {
                $('#help_text').addClass('hide');
                $('#<%=txtHelpText.ClientID%>').val('');
            }
            else {
                $('#help_text').removeClass('hide');
            }

            if ($('#<%=cboTagType.ClientID%>').val() == "0" || $('#<%=cboTagType.ClientID%>').val() == "13") {
                $('#requiredField').addClass('hide');
                offCheckBox('#<%=chkFieldRequired.ClientID%>');
            }
            else {
                $('#requiredField').removeClass('hide');
            }

            if ($('#<%=cboTagType.ClientID%>').val() != "5") {
                $('#selection_text').addClass('hide');
            }
            else {
                $('#selection_text').removeClass('hide');
            }

            if ($('#<%=cboTagType.ClientID%>').val() != "2") {
                $('#default_text').addClass('hide');
            }
            else {
                $('#default_text').removeClass('hide');
            }

            if ($('#<%=cboTagType.ClientID%>').val() == "2" || $('#<%=cboTagType.ClientID%>').val() == "3") {
                $('#max_length').removeClass('hide');
            }
            else {
                $('#max_length').addClass('hide');
            }

            if ($('#<%=cboTagType.ClientID%>').val() == "10") {
                $('#masterTable').removeClass('hide');
            }
            else {
                $('#masterTable').addClass('hide');
            }

            if ($('#<%=cboTagType.ClientID%>').val() == "5" || $('#<%=cboTagType.ClientID%>').val() == "7" || $('#<%=cboTagType.ClientID%>').val() == "12" || $('#<%=cboTagType.ClientID%>').val() == "10") {
                $('#divTypable').removeClass('hide');
            }
            else {
                $('#divTypable').addClass('hide');
                offCheckBox('#<%=chkTypable.ClientID%>');
            }
        }
    </script>
</asp:Content>
