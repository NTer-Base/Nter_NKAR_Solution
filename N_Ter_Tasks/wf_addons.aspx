<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid_wf.master" AutoEventWireup="true" CodeBehind="wf_addons.aspx.cs" Inherits="N_Ter_Tasks.wf_addons" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Workflow Addons
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
    <link href="assets/stylesheets/pages.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Workflow Addons -
    <asp:Literal ID="ltrWorkflowName" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div style="margin: -18px -20px;" class="border-t">
        <div class="mail-nav">
            <div class="compose-btn text-right">
                <div class="pull-left" style="margin-top: 4px">
                    Addons
                </div>
                <button id="cmdNewAddon" runat="server" class="btn btn-labeled btn-primary" title="New Step" onserverclick="cmdNewAddon_ServerClick"><i class="fa fa-plus"></i></button>
            </div>
            <asp:HiddenField ID="hndWFAddonID" runat="server" />
            <asp:Button ID="cmdLoadAddon" runat="server" Text="Button" Style="display: none" OnClick="cmdLoadAddon_Click" />
            <div class="navigation">
                <ul id="ul_steps" class="sections">
                    <li class="mail-select-folder active"><a href="#">Select an Addon...</a></li>
                    <asp:Literal ID="ltrAddons" runat="server"></asp:Literal>
                </ul>
            </div>
        </div>
        <asp:Panel ID="pnlAddon" runat="server" CssClass="mail-container">
            <div class="mail-container-header show">
                <asp:Literal ID="ltrAddonMode" runat="server"></asp:Literal>
                <div class="pull-right">
                    <button id="cmdDuplicateAddon" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-files-o"></span>Create Duplicate Addon</button>
                    <asp:ModalPopupExtender ID="cmdDuplicate_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDuplicateAddon" TargetControlID="cmdDuplicateAddon" CancelControlID="cmdCancelDuplicate" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                    <button id="cmdDeleteAddon" runat="server" class="btn btn-labeled btn-danger"><span class="btn-label icon fa fa-times"></span>Delete Addon</button>
                    <asp:ModalPopupExtender ID="cmdDeleteAddon_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDeleteAddon" TargetControlID="cmdDeleteAddon" CancelControlID="cmdCalcelDelAddon" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                </div>
            </div>
            <div class="new-mail-form form-horizontal">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <span class="panel-title">Main Addon Details</span>
                    </div>
                    <div class="panel-body" style="padding-bottom: 0px!important">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-sm-3 control-label pt14">Addon Name</label>
                                <div class="col-sm-9" style="padding-top: 7px">
                                    <asp:TextBox ID="txtAddonName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label pt14">Dialog Size</label>
                                <div class="col-sm-9" style="padding-top: 7px">
                                    <asp:DropDownList ID="cboScreenSize" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1">Large</asp:ListItem>
                                        <asp:ListItem Value="2">Medium</asp:ListItem>
                                        <asp:ListItem Value="3">Small</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="cmdSaveAddon" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSaveAddon_Click" />
                        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmdCancel_Click" />
                    </div>
                </div>
                <asp:Panel ID="pnlAddonFields" runat="server" CssClass="panel panel-info">
                    <asp:HiddenField ID="hndAddon_Field_ID" runat="server" />
                    <div class="panel-heading">
                        <div class="at_modelpopup_add">
                            <button id="cmdNewAddonField" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Addon Field</button>
                            <asp:ModalPopupExtender ID="cmdNewAddonField_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlAddonField" TargetControlID="cmdNewAddonField" CancelControlID="cmdCancelAddonField" BackgroundCssClass="at_modelpopup_background">
                            </asp:ModalPopupExtender>
                        </div>
                        <span class="panel-title">Addon Fields</span>
                    </div>
                    <div class="panel-body" style="padding-bottom: 0px!important">
                        <div class="table-responsive">
                            <asp:GridView ID="gvStepAddons" runat="server" CssClass="table table-striped table-hover grid_table non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvStepAddon_RowDataBound" RowStyle-CssClass="group field_addon_row">
                                <Columns>
                                    <asp:BoundField DataField="Workflow_Addon_Field_ID" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <button id='cmdEditAddonField' type='submit' runat="server" class="btn btn-primary btn-xs" title="Edit"><i class="fa fa-edit button_icon"></i></button>
                                            <asp:ModalPopupExtender ID="cmdEditAddonField_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlAddonField" TargetControlID="cmdEditAddonField" CancelControlID="cmdCancelAddonField" BackgroundCssClass="at_modelpopup_background">
                                            </asp:ModalPopupExtender>
                                        </ItemTemplate>
                                        <ItemStyle Width="22px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <button id='cmdDeleteAddonField' type='submit' runat="server" class="btn btn-danger btn-xs" title="Delete"><i class="fa fa-trash-o button_icon"></i></button>
                                            <asp:ModalPopupExtender ID="cmdDeleteStepAddon_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDeleteAddonField" TargetControlID="cmdDeleteAddonField" CancelControlID="cmdCancelDeleteAddonField" BackgroundCssClass="at_modelpopup_background">
                                            </asp:ModalPopupExtender>
                                        </ItemTemplate>
                                        <ItemStyle Width="22px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Field_Name" HeaderText="Field Name"></asp:BoundField>
                                    <asp:BoundField DataField="Field_SizeSP" HeaderText="Field Size">
                                        <ItemStyle Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Field_TypeSP" HeaderText="Field Type">
                                        <ItemStyle Width="120px" />
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
                </asp:Panel>
            </div>
        </asp:Panel>
    </div>
    <asp:Panel ID="pnlDeleteAddon" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_addon_indent" class="at_modelpopup_indent">
            <div id="at_del_addon_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete - Addon</h4>
                </div>
                <div id="at_del_addon_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDeleteAddonConf" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDeleteAddonConf_Click" />
                    <asp:Button ID="cmdCalcelDelAddon" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDeleteAddonField" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_addon_fld_indent" class="at_modelpopup_indent">
            <div id="at_del_addon_fld_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete - Addon Field</h4>
                </div>
                <div id="at_del_addon_fld_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmeDeleteAddonFieldConf" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmeDeleteAddonFieldConf_Click" />
                    <asp:Button ID="cmdCancelDeleteAddonField" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlAddonField" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_addon_indent" class="at_modelpopup_indent">
            <div id="at_model_addon_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCancelAddonField" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add/Edit Workflow Step Addon Field</h4>
                </div>
                <div id="at_model_addon_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Field Name</label>
                            <asp:TextBox ID="txtFieldName_Addon" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Field Type</label>
                            <asp:DropDownList ID="cboFieldType_Addon" runat="server" CssClass="form-control">
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
                                <asp:ListItem Value="11">File Upload</asp:ListItem>
                                <asp:ListItem Value="10">Master Table</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div id="accessLevel" class="form-group">
                            <label>Access Level</label>
                            <asp:DropDownList ID="cboAccessLevel_Addon" runat="server" CssClass="form-control">
                                <asp:ListItem Value="3">Level 3</asp:ListItem>
                                <asp:ListItem Value="2">Level 2</asp:ListItem>
                                <asp:ListItem Value="1">Level 1</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div id="defaultDocCat" class="form-group">
                            <label>Default Upload Category</label>
                            <asp:DropDownList ID="cboDocCat_Addon" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div id="divTypable" class="form-group">
                            <asp:CheckBox Text="" runat="server" ID="chkTypable_Addon" CssClass="checkboxlist" /><span style="padding-left: 10px">Enable Search</span>
                        </div>
                        <div id="default_text_addon" class="form-group">
                            <label>Default Text</label>
                            <asp:TextBox ID="txtDefaultText_Addon" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div id="default_text_memo_addon" class="form-group">
                            <label>Default Text</label>
                            <asp:TextBox ID="txtDefaultTextMemo_Addon" runat="server" CssClass="form-control" TextMode="MultiLine" Height="75"></asp:TextBox>
                        </div>
                        <div id="max_length_addon" class="form-group">
                            <label>Maximum Lenght</label>
                            <asp:TextBox runat="server" CssClass="form-control" TextMode="Number" ID="txtFieldMaxLength_Addon" Min="0" />
                        </div>
                        <div id="selection_text_addon" class="form-group">
                            <label>Selection Text</label>
                            <asp:TextBox ID="txtSelectionText_Addon" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div id="masterTable_addon" class="form-group">
                            <label>Master Table</label>
                            <asp:DropDownList ID="cboMasterTable_Addon" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div id="masterTableParamType_addon" class="form-group">
                            <label>Master Table Parameter</label>
                            <asp:DropDownList ID="cboMasterTableParamType_Addon" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">[No Parameter]</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div id="masterTableParam2Type_addon" class="form-group">
                            <label>Master Table Parameter</label>
                            <asp:DropDownList ID="cboMasterTableParam2Type_Addon" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">[No Parameter]</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Field Side</label>
                            <asp:DropDownList ID="cboFieldSize_Addon" runat="server" CssClass="form-control">
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
                        <div id="validateField_addon" class="form-group">
                            <label>Validate with</label>
                            <asp:DropDownList ID="cboValidateField_Addon" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div id="copyData_addon" class="form-group">
                            <label>Copy Data From</label>
                            <asp:DropDownList ID="cboCopyDataFrom_Addon" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div id="divDisableControl_addon" class="form-group">
                            <asp:CheckBox Text="" runat="server" ID="chkDisableControl_Addon" CssClass="checkboxlist" /><span style="padding-left: 10px">Make Control Read Only</span>
                        </div>
                        <div id="helpContent_addon" class="form-group">
                            <label>Help Text</label>
                            <asp:TextBox ID="txtFieldHelpText_Addon" runat="server" CssClass="form-control" TextMode="MultiLine" Style="height: 100px"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Custom Data</label>
                            <asp:TextBox ID="txtCustomData_Addon" runat="server" CssClass="form-control" TextMode="MultiLine" Style="height: 100px"></asp:TextBox>
                        </div>
                        <div id="requiredField_addon" class="form-group">
                            <asp:CheckBox Text="" runat="server" ID="chkFieldRequired_Addon" CssClass="checkboxlist" /><span style="padding-left: 10px">Required Field</span>
                        </div>
                        <div id="copyEF1_addon" class="form-group">
                            <asp:CheckBox Text="" runat="server" ID="chkCopyEF1_Addon" CssClass="checkboxlist" /><span style="padding-left: 10px">Copy to Special Field 1 (<asp:Literal ID="ltrEx1Name2" runat="server"></asp:Literal>)</span>
                        </div>
                        <div id="copyEF2_addon" class="form-group">
                            <asp:CheckBox Text="" runat="server" ID="chkCopyEF2_Addon" CssClass="checkboxlist" /><span style="padding-left: 10px">Copy to Special Field 2 (<asp:Literal ID="ltrEx2Name2" runat="server"></asp:Literal>)</span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSaveAddonField" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSaveAddonField_Click" />
                    <asp:Button ID="cmdResetAddonField" runat="server" Text="Reset" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDuplicateAddon" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_dup_indent" class="at_modelpopup_indent">
            <div id="at_model_dup_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCancelDuplicate" runat="server" Text="&times;" CssClass="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Duplicate Addon</h4>
                </div>
                <div id="at_model_dup_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Addon to Duplicate</label>
                            <asp:TextBox ID="txtCurrentAddon" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>New Step Name</label>
                            <asp:TextBox ID="txtNewAddon" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCreateDuplicate" runat="server" Text="Create Duplicate Addon" CssClass="btn btn-primary" OnClick="cmdCreateDuplicate_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script type="text/javascript">
        $(function () {
            $(window).resize(function () {
                ArrangePoups();
            });
        });

        init.push(function () {
            ArrangePoups();
            ArrangeNavigation();
            ArrangeAddonSort();
        })

        function ArrangePoups() {
            AdjustPopupSize(80, 400, 'at_del_addon');
            AdjustPopupSize(80, 400, 'at_del_addon_fld');
            AdjustPopupSize(167, 600, 'at_model_addon');
            AdjustPopupSize(167, 800, 'at_model_dup');
        }

        function ArrangeNavigation() {
            $('.mail-nav .navigation li.active a').click(function () {
                $('.mail-nav .navigation').toggleClass('open');
                return false;
            });
        }

        function ArrangeAddonSort() {
            $("#<%=gvStepAddons.ClientID%> tbody").attr('id', 'table_addons');
            $("#table_addons").sortable({
                axis: "y",
                handle: "i",
                stop: function (event, ui) {
                    ui.item.children("i").triggerHandler("focusout");
                    var strIDs = "";
                    $(".field_addon_row").each(function () {
                        strIDs = strIDs + $(this).attr('data-id') + '|';
                    });

                    $.ajax({
                        type: "GET",
                        url: "api/tasks/UpdateWFStepAddonSort",
                        data: { Addon_ID: $("#<%=hndWFAddonID.ClientID%>").val(), Sort_List: strIDs },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: UpdateStepSortPassed,
                        failure: LoadValuesFailed
                    });
                }
            });
        }

        function UpdateStepSortPassed(result) { }

        function LoadAddon() {
            document.getElementById('<%=cmdLoadAddon.ClientID%>').click();
        }

        function ClearControlsAddon() {
            clearTextBox(['<%=txtFieldName_Addon.ClientID%>', '<%=txtSelectionText_Addon.ClientID%>', '<%=txtDefaultText_Addon.ClientID%>', '<%=txtDefaultTextMemo_Addon.ClientID%>', '<%=txtFieldMaxLength_Addon.ClientID%>', '<%=txtFieldHelpText_Addon.ClientID%>', '<%=txtCustomData_Addon.ClientID%>']);
            clearDropDown(['<%=cboFieldType_Addon.ClientID%>', '<%=cboValidateField_Addon.ClientID%>', '<%=cboCopyDataFrom_Addon.ClientID%>', '<%=cboFieldSize_Addon.ClientID%>', '<%=cboMasterTable_Addon.ClientID%>', '<%=cboMasterTableParamType_Addon.ClientID%>', '<%=cboMasterTableParam2Type_Addon.ClientID%>', '<%=cboAccessLevel_Addon.ClientID%>', '<%=cboDocCat_Addon.ClientID%>']);
            clearCheckBox(['<%=chkFieldRequired_Addon.ClientID%>', '<%=chkCopyEF1_Addon.ClientID%>', '<%=chkCopyEF2_Addon.ClientID%>', '<%=chkDisableControl_Addon.ClientID%>', '<%=chkTypable_Addon.ClientID%>']);
            ShowHideSectionsAddon(false);
            return false;
        }

        function ResetControlsAddon() {
            if ($("#<%=hndAddon_Field_ID.ClientID%>").val().trim() == '0') {
                ClearControlsAddon();
            }
            else {
                LoadValuesAddonField();
            }
            return false;
        }

        function LoadValuesAddonField() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetWorklFlowAddonField",
                data: { Workflow_Addon_Field_ID: $("#<%=hndAddon_Field_ID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassedAddon,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassedAddon(result) {
            ClearControlsAddon();

            var resultObject = result;
            $('#<%=txtFieldName_Addon.ClientID%>').val(resultObject[0].Field_Name);
            $('#<%=cboFieldType_Addon.ClientID%>').val(resultObject[0].Field_Type);
            $('#<%=cboFieldSize_Addon.ClientID%>').val(resultObject[0].Field_Size);
            $('#<%=txtSelectionText_Addon.ClientID%>').val(resultObject[0].Selection_Texts);
            if (resultObject[0].Field_Type == '3') {
                $('#<%=txtDefaultTextMemo_Addon.ClientID%>').val(resultObject[0].Default_Texts);
            }
            else {
                $('#<%=txtDefaultText_Addon.ClientID%>').val(resultObject[0].Default_Texts);
            }
            $('#<%=cboValidateField_Addon.ClientID%>').val(resultObject[0].Validate_With_Field_ID);
            $('#<%=txtFieldHelpText_Addon.ClientID%>').val(resultObject[0].Help_Text);
            $('#<%=txtCustomData_Addon.ClientID%>').val(resultObject[0].Custom_Data);
            if (resultObject[0].IsRequired == true) {
                onCheckBox('#<%=chkFieldRequired_Addon.ClientID%>');
            }
            if (resultObject[0].Copy_To_EF1 == true) {
                onCheckBox('#<%=chkCopyEF1_Addon.ClientID%>');
            }
            if (resultObject[0].Copy_To_EF2 == true) {
                onCheckBox('#<%=chkCopyEF2_Addon.ClientID%>');
            }
            if (resultObject[0].isTypable == true) {
                onCheckBox('#<%=chkTypable_Addon.ClientID%>');
            }
            if (parseInt(resultObject[0].Max_Length) > 0) {
                $('#<%=txtFieldMaxLength_Addon.ClientID%>').val(resultObject[0].Max_Length);
            }
            $('#<%=cboCopyDataFrom_Addon.ClientID%>').val(resultObject[0].Copy_Data_From);
            $('#<%=cboMasterTable_Addon.ClientID%>').val(resultObject[0].Master_Table_ID);
            $('#<%=cboMasterTableParamType_Addon.ClientID%>').val(resultObject[0].Master_Table_Param_Type);
            $('#<%=cboMasterTableParam2Type_Addon.ClientID%>').val(resultObject[0].Master_Table_Param_2_Type);
            $('#<%=cboAccessLevel_Addon.ClientID%>').val(resultObject[0].Access_Level);
            $('#<%=cboDocCat_Addon.ClientID%>').val(resultObject[0].Default_Doc_Category);
            ShowHideSectionsAddon(resultObject[0].Disable_Control);
        }

        function CheckCopyDisableAddon(isDisabled) {
            if ($('#<%=cboCopyDataFrom_Addon.ClientID%>').val() == '0') {
                $('#divDisableControl_addon').addClass('hide');
                offCheckBox('#<%=chkDisableControl_Addon.ClientID%>');
            }
            else {
                $('#divDisableControl_addon').removeClass('hide');
                if (isDisabled == true) {
                    onCheckBox('#<%=chkDisableControl_Addon.ClientID%>');
                }
                else {
                    offCheckBox('#<%=chkDisableControl_Addon.ClientID%>');
                }
            }
        }

        function ShowHideSectionsAddon(isControlDisabled) {
            if ($('#<%=cboFieldType_Addon.ClientID%>').val() == "0") {
                $('#helpContent_addon').addClass('hide');
                $('#<%=txtFieldHelpText_Addon.ClientID%>').val('');
                $('#copyEF1_addon').addClass('hide');
                offCheckBox('#<%=chkCopyEF1_Addon.ClientID%>');
                $('#copyEF2_addon').addClass('hide');
                offCheckBox('#<%=chkCopyEF2_Addon.ClientID%>');
            }
            else {
                $('#helpContent_addon').removeClass('hide');
                $('#copyEF1_addon').removeClass('hide');
                $('#copyEF2_addon').removeClass('hide');
            }

            if ($('#<%=cboFieldType_Addon.ClientID%>').val() == "0" || $('#<%=cboFieldType_Addon.ClientID%>').val() == "11") {
                $('#validateField_addon').addClass('hide');
                $('#<%=cboValidateField_Addon.ClientID%>').val('0');
                $('#copyData_addon').addClass('hide');
                $('#<%=cboCopyDataFrom_Addon.ClientID%>').val('0');
            }
            else {
                $('#validateField_addon').removeClass('hide');
                $('#copyData_addon').removeClass('hide');
            }

            if ($('#<%=cboFieldType_Addon.ClientID%>').val() == "0" || $('#<%=cboFieldType_Addon.ClientID%>').val() == "13") {
                $('#requiredField_addon').addClass('hide');
                offCheckBox('#<%=chkFieldRequired_Addon.ClientID%>');
            }
            else {
                $('#requiredField_addon').removeClass('hide');
            }

            if ($('#<%=cboFieldType_Addon.ClientID%>').val() == "10") {
                $('#masterTable_addon').removeClass('hide');
                $('#masterTableParamType_addon').removeClass('hide');
                $('#masterTableParam2Type_addon').removeClass('hide');
            }
            else {
                $('#masterTable_addon').addClass('hide');
                $('#masterTableParamType_addon').addClass('hide');
                $('#masterTableParam2Type_addon').addClass('hide');
            }

            if ($('#<%=cboFieldType_Addon.ClientID%>').val() == "5") {
                $('#selection_text_addon').removeClass('hide');
            }
            else {
                $('#selection_text_addon').addClass('hide');
            }

            if ($('#<%=cboFieldType_Addon.ClientID%>').val() == "3") {
                $('#default_text_memo_addon').removeClass('hide');
                $('#default_text_addon').addClass('hide');
            }
            else if ($('#<%=cboFieldType_Addon.ClientID%>').val() == "2") {
                $('#default_text_memo_addon').addClass('hide');
                $('#default_text_addon').removeClass('hide');
            }
            else {
                $('#default_text_memo_addon').addClass('hide');
                $('#default_text_addon').addClass('hide');
            }

            if ($('#<%=cboFieldType_Addon.ClientID%>').val() == "2" || $('#<%=cboFieldType_Addon.ClientID%>').val() == "3") {
                $('#max_length_addon').removeClass('hide');
            }
            else {
                $('#max_length_addon').addClass('hide');
            }

            if ($('#<%=cboFieldType_Addon.ClientID%>').val() == "5" || $('#<%=cboFieldType_Addon.ClientID%>').val() == "7" || $('#<%=cboFieldType_Addon.ClientID%>').val() == "12" || $('#<%=cboFieldType_Addon.ClientID%>').val() == "10") {
                $('#divTypable').removeClass('hide');
            }
            else {
                $('#divTypable').addClass('hide');
                offCheckBox('#<%=chkTypable_Addon.ClientID%>');
            }

            if ($('#<%=cboFieldType_Addon.ClientID%>').val() == "11") {
                $('#accessLevel').removeClass('hide');
                $('#defaultDocCat').removeClass('hide');
            }
            else {
                $('#accessLevel').addClass('hide');
                $('#<%=cboAccessLevel_Addon.ClientID%>').val('3');
                $('#defaultDocCat').addClass('hide');
                $('#<%=cboDocCat_Addon.ClientID%>').val('General Documents');
            }
            CheckCopyDisableAddon(isControlDisabled);
        }
    </script>
</asp:Content>