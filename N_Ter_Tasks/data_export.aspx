<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid_wf.master" AutoEventWireup="true" CodeBehind="data_export.aspx.cs" Inherits="N_Ter_Tasks.data_export" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Data Export
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
    <link href="assets/stylesheets/pages.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Data Export
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div style="margin: -18px -20px;" class="border-t">
        <asp:HiddenField ID="hndTemplateID" runat="server" />
        <div class="mail-nav">
            <div class="navigation">
                <ul id="ul_steps" class="sections">
                    <li class="mail-select-folder"><a href="#">Select a Step...</a></li>
                    <asp:Literal ID="ltrSteps" runat="server"></asp:Literal>
                </ul>
            </div>
        </div>
        <asp:Panel ID="pnlSteps" runat="server" CssClass="mail-container">
            <div class="mail-container-header show">
                <div class="pull-right">
                    <button id='cmdLoad' type='submit' runat="server" class="btn btn-default" title="Load Template" onclick="return LoadTepmlate();"><i class="fa fa-folder-open"></i></button>
                    <button id='cmdSaveNew' type='submit' runat="server" class="btn btn-default" title="Save Template" onclick="return NewTemplate();"><i class="fa fa-file"></i></button>
                    <button id='cmdSaveExisting' type='submit' runat="server" class="btn btn-default" title="Save Template" onclick="return NewTemplate();"><i class="fa fa-file"></i></button>
                    <button id='cmdSaveAs' type='submit' runat="server" class="btn btn-default" title="Save a Template Copy" onclick="return SaveAsTemplate();"><i class="fa fa-paste"></i></button>
                    <button id='cmdExtract' type='submit' runat="server" class="btn btn-primary" title="Extract Data" onclick="return ValidateMaxElements();"><i class="fa fa-download"></i></button>
                    <asp:Button ID="cmdExtractHnd" runat="server" Text="" CssClass="hide" OnClick="cmdExtract_Click" />
                </div>
                <span id="stepName">Header</span>
            </div>
            <div id="divContent" runat="server" class="new-mail-form form-horizontal border-t"></div>
            <div id="divExtracts" runat="server" class="pr20 pl20">
                <div class="panel panel-info">
                    <div class="panel-body">
                        <div class="panel-title-nter">Your Extractions</div>
                        <div class="border-t panel-info" style="height: 10px"></div>
                        <div class="table-responsive table-primary no-margin-b">
                            <table id="tblExtracts" class="table table-striped table-hover grid_table grid_test non_full_width_table" data-size="non_full_width_table">
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th>Date</th>
                                        <th>Description</th>
                                        <th>Template</th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="hndSave" runat="server" />
    <asp:ModalPopupExtender ID="hndSave_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuSave" PopupControlID="pnlNewTemplate" TargetControlID="hndSave" CancelControlID="cmdSaveCencel" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlNewTemplate" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_new_indent" class="at_modelpopup_indent">
            <div id="at_new_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Template Name</h4>
                </div>
                <div id="at_new_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Name</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox runat="server" ID="chkIsPrivate" CssClass="checkboxlist" /><span style="padding-left: 10px">This is a Private Template</span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSaveConfirm" runat="server" Text="OK" CssClass="btn btn-primary" OnClick="cmdSaveConfirm_Click" />
                    <asp:Button ID="cmdSaveCencel" runat="server" Text="Cancel" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndSaveAs" runat="server" />
    <asp:ModalPopupExtender ID="hndSaveAs_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuSaveAs" PopupControlID="pnlSaveAs" TargetControlID="hndSaveAs" CancelControlID="cmdSaveAsCencel" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlSaveAs" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_saveas_indent" class="at_modelpopup_indent">
            <div id="at_saveas_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Template Name</h4>
                </div>
                <div id="at_saveas_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Name</label>
                            <asp:TextBox ID="txtNameSaveAs" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox runat="server" ID="chkisPrivateSaveAs" CssClass="checkboxlist" /><span style="padding-left: 10px">This is a Private Template</span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSaveAsConfirm" runat="server" Text="OK" CssClass="btn btn-primary" OnClick="cmdSaveAsConfirm_Click" />
                    <asp:Button ID="cmdSaveAsCencel" runat="server" Text="Cancel" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="cmdLoadTemplate" runat="server" Text="" OnClick="cmdLoadTemplate_Click" Style="display: none" />
    <asp:HiddenField ID="hndSelectTemplate" runat="server" />
    <asp:ModalPopupExtender ID="hndSelectTemplate_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuSelectTemplate" PopupControlID="pnlSelectTemplate" TargetControlID="hndSelectTemplate" CancelControlID="cmdCloseSelect" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlSelectTemplate" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_select_indent" class="at_modelpopup_indent">
            <div id="at_model_select_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseSelect" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Select Existing Template</h4>
                </div>
                <div id="at_model_select_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body grid-with-paging">
                        <div class="table-responsive table-primary no-margin-b">
                            <asp:GridView ID="gvTemplates" runat="server" CssClass="table table-striped table-hover grid_table gvTemplates" AutoGenerateColumns="False" OnRowDataBound="gvTemplates_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="Template_ID" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <button id='cmdSelect' type='submit' runat="server" class="btn btn-primary btn-xs" title="Edit"><i class="fa fa-folder-open button_icon"></i></button>
                                        </ItemTemplate>
                                        <ItemStyle Width="22px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Template_Name" HeaderText="Template Name"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndDeleteFile" runat="server" />
    <asp:ModalPopupExtender ID="hndDeleteFile_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDeleteFile" BehaviorID="mpuDeleteFile" TargetControlID="hndDeleteFile" CancelControlID="cmdCancelDelFile" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlDeleteFile" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_model_indent" class="at_modelpopup_indent">
            <div id="at_del_model_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete - File</h4>
                </div>
                <div id="at_del_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDeleteFile" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDeleteFile_Click" />
                    <asp:Button ID="cmdCancelDelFile" runat="server" Text="No" CssClass="btn btn-default" />
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
            ArrangeGrids();
            DateBetween();
            CheckForCount();
            <%=Extraction_Script1%>
        });

        function ArrangePoups() {
            AdjustPopupSize(80, 600, 'at_new');
            AdjustPopupSize(80, 600, 'at_saveas');
            AdjustPopupSize(80, 1200, 'at_model_select');
            <%=DeleteFile_Script2%>
        }

        function ArrangeGrids() {
            $('.gvTemplates').dataTable({
                "pageLength": 50,
                "order": [[1, 'asc']],
                "responsive": true,
                "autoWidth": true,
                "columnDefs": [
                    { 'orderable': false, targets: 0 }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });

            $(".dataTables_filter").children().children('input').attr('placeholder', 'Search...');
        }

        function showStep(stepIndex, stepName, control) {
            $('#stepName').html(stepName);
            $('.w_steps').each(function (index) {
                $(this).removeClass('active-item');
            });
            $(control).parent().addClass('active-item');

            $('.stepControls').each(function (index) {
                $(this).addClass('hide');
            });

            $('#step_' + stepIndex).removeClass('hide');
            return false;
        }

        function NewTemplate() {
            $find("mpuSave").show();
            return false;
        }

        function SaveAsTemplate() {
            $find("mpuSaveAs").show();
            return false;
        }

        function LoadTepmlate() {
            $find("mpuSelectTemplate").show();
            return false;
        }

        function LoadTemplate(template_id) {
            $('#<%=hndTemplateID.ClientID%>').val(template_id);
            $('#<%=cmdLoadTemplate.ClientID%>').click();
            return false;
        }

        function DateBetween() {
            <%=DateBetweenScript%>
        }

        function CheckForCount() {
            $(".checkboxlist").change(function () {
                var CheckedCount = ItemCheckedCount();
                if (CheckedCount > 40) {
                    ShowError('You have reached the Maximum Number of Elements Allowed');
                    var CtrlID = $(this).children().children(':checkbox').prop('id');
                    offCheckBox('#' + CtrlID);
                }
            });
        }

        function ItemCheckedCount() {
            var CheckedCount = 0;
            $('.checkboxlist').each(function (index) {
                if ($(this).children().children(':checkbox').prop('checked') == true) {
                    CheckedCount++;
                }
            });
            return CheckedCount;
        }

        function ValidateMaxElements() {
            var CheckedCount = ItemCheckedCount();
            if (CheckedCount == 0) {
                ShowError('Please select at least 1 Element');
                return false;
            }
            else if (CheckedCount > 40) {
                ShowError('You have Selected more than Maximum Number of Elements Allowed');
                return false;
            }
            else {
                $('#<%=cmdExtractHnd.ClientID%>').click();
                return false;
            }
        }

        <%=Extraction_Script2%>

        <%=DeleteFile_Script%>
    </script>
</asp:Content>