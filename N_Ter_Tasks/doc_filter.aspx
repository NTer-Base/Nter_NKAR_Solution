<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid.master" AutoEventWireup="true" CodeBehind="doc_filter.aspx.cs" Inherits="N_Ter_Tasks.doc_filter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Task Attachment Filter
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Task Attachment Filter 
    <asp:Literal ID="ltrTaskNumber" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="panel_menu">
                        <button id="cmdBackToTask" runat="server" class="btn btn-labeled btn-primary" onserverclick="cmdBackToTask_ServerClick"><span class="btn-label icon fa fa fa-backward"></span>Back to Task</button>
                    </div>
                    <div class="panel-title-nter">Task Details</div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="row pb5">
                                <label class="col-sm-3 control-label text-bold" for="demo-hor-inputemail">Task Number</label>
                                <div class="col-sm-9">
                                    <asp:Label ID="lblTaskNo" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                            <div class="row pb5">
                                <label class="col-sm-3 control-label text-bold" for="demo-hor-inputemail">Workflow Name</label>
                                <div class="col-sm-9">
                                    <asp:Label ID="lblWorkflow" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                            <div class="row pb5">
                                <label class="col-sm-3 control-label text-bold" for="demo-hor-inputemail">Task Creator</label>
                                <div class="col-sm-9">
                                    <asp:Label ID="lblTaskCreator" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                            <div id="divExtraField" runat="server" class="row pb5">
                                <div class="col-sm-3  text-bold">
                                    <asp:Label ID="lblExtraFieldName" runat="server" Text="N/A"></asp:Label>
                                </div>
                                <div class="col-sm-9">
                                    <asp:Label ID="lblExtraFieldValue" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                            <div id="divExtraField2" runat="server" class="row pb5">
                                <div class="col-sm-3  text-bold">
                                    <asp:Label ID="lblExtraField2Name" runat="server" Text="N/A"></asp:Label>
                                </div>
                                <div class="col-sm-9">
                                    <asp:Label ID="lblExtraField2Value" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="row pb5">
                                <label class="col-sm-3 control-label text-bold" for="demo-hor-inputemail">
                                    <asp:Literal ID="ltrEL2" runat="server"></asp:Literal>
                                    Name</label>
                                <div class="col-sm-9">
                                    <asp:Label ID="lblEL2Name" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                            <div class="row pb5">
                                <label class="col-sm-3 control-label text-bold" for="demo-hor-inputemail">Current Status</label>
                                <div class="col-sm-9">
                                    <asp:Label ID="lblCurrentStatus" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                            <div class="row pb5">
                                <label class="col-sm-3 control-label text-bold" for="demo-hor-inputemail">Task Date/Time</label>
                                <div class="col-sm-9">
                                    <asp:Label ID="lblTaskDate" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                            <div class="row pb5">
                                <label class="col-sm-3 control-label text-bold" for="demo-hor-inputemail">Task Due Date/Time</label>
                                <div class="col-sm-9">
                                    <asp:Label ID="lblDueDate" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
        </div>
        <div class="col-md-6">
            <div class="row padding-xs-vr">
                <div class="col-md-3 pt5">
                    Show Documents in :
                </div>
                <div class="col-md-9">
                    <asp:DropDownList ID="cboFolder" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboFolder_SelectedIndexChanged">
                        <asp:ListItem Value="0">[Any Folder]</asp:ListItem>
                        <asp:ListItem Value="1">Source Documents Folder</asp:ListItem>
                        <asp:ListItem Value="2">Output Documents Folder</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="panel-title-nter">Document Filter Options</div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <div class="row padding-xs-vr">
                        <div class="col-md-3">
                            Attached Document Content
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="cboDocOperator" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">[Any]</asp:ListItem>
                                <asp:ListItem Value="1">Starts with</asp:ListItem>
                                <asp:ListItem Value="2">Ends with</asp:ListItem>
                                <asp:ListItem Value="3">Contains</asp:ListItem>
                                <asp:ListItem Value="4">Not Contain</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtDocCriteria" runat="server" TextMode="MultiLine" Height="75" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div id="divDocFilters" runat="server"></div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdFilter" runat="server" Text="Submit" class="btn btn-primary" OnClick="cmdFilter_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="panel-title-nter">Filtered Documents</div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <asp:HiddenField ID="hndSelectedDocs" ClientIDMode="Static" runat="server" />
                    <div id="divDocGrid" runat="server" class="table-responsive table-primary no-margin-b">
                        <asp:GridView ID="gvDocs" runat="server" CssClass="table table-striped table-hover grid_table grid_docs non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvDocs_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="Document_ID" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxlist chkSelected" />
                                    </ItemTemplate>
                                    <ItemStyle Width="22px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Document_No" HeaderText="Doc Number" />
                                <asp:BoundField DataField="Creator_Name" HeaderText="Uploaded By" />
                                <asp:BoundField DataField="Created_Date" HeaderText="Date Uploaded">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Matching_Percentage_SP" HeaderText="">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle Width="30px" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <button id='cmdView' type='submit' runat="server" class="btn btn-primary btn-xs" title="View Document"><span class="btn-label button_icon fa fa-eye"></span></button>
                                    </ItemTemplate>
                                    <ItemStyle Width="22px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <button id='cmdIfo' type='submit' runat="server" class="btn btn-primary btn-xs" title="View Tags"><span class="btn-label button_icon fa fa-info"></span></button>
                                    </ItemTemplate>
                                    <ItemStyle Width="22px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div id="divNoDocs" runat="server" class="alert alert-danger alert-dark">
                        <strong>No Documents</strong> attached to this task to show.
                    </div>
                </div>
                <div id="divResultsFooter" runat="server" class="modal-footer">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="input-group">
                                <span class="input-group-addon">Submit</span>
                                <asp:DropDownList ID="cboProcessDocType" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Selected Documents</asp:ListItem>
                                    <asp:ListItem Value="1">Unselected Documents</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group filter_button_set">
                                <span class="input-group-addon">to</span>
                                <asp:DropDownList ID="cboProcessType" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Deattach</asp:ListItem>
                                    <asp:ListItem Value="1">Source Documents Folder</asp:ListItem>
                                    <asp:ListItem Value="2">Output Documents Folder</asp:ListItem>
                                </asp:DropDownList>
                                <span class="input-group-btn">
                                    <asp:Button ID="cmdStart" runat="server" Text="Start" CssClass="btn btn-primary" OnClick="cmdStart_Click" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hndRemoveSelected" runat="server" />
                    <asp:ModalPopupExtender ID="hndRemoveSelected_ModalPopupExtender" runat="server" Enabled="true" BehaviorID="mpuRemoveSelected" PopupControlID="pnlDetachSelected" TargetControlID="hndRemoveSelected" BackgroundCssClass="at_modelpopup_background_2" CancelControlID="cmdDetachSelectedNo">
                    </asp:ModalPopupExtender>
                    <asp:HiddenField ID="hndRemoveUnselected" runat="server" />
                    <asp:ModalPopupExtender ID="hndRemoveUnselected_ModalPopupExtender" runat="server" Enabled="true" BehaviorID="mpuRemoveUnselected" PopupControlID="pnlDetachUnselected" TargetControlID="hndRemoveUnselected" BackgroundCssClass="at_modelpopup_background_2" CancelControlID="cmdDetachUnselectedNo">
                    </asp:ModalPopupExtender>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlDetachSelected" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_detach_indent" class="at_modelpopup_indent">
            <div id="at_model_detach_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Detach Selected Documents</h4>
                </div>
                <div id="at_model_detach_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you Sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDetachSelectedYes" runat="server" CssClass="btn btn-primary" Text="Yes" OnClick="cmdDetachSelectedYes_Click" />
                    <asp:Button ID="cmdDetachSelectedNo" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDetachUnselected" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_detach_u_indent" class="at_modelpopup_indent">
            <div id="at_model_detach_u_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Detach Unselected Documents</h4>
                </div>
                <div id="at_model_detach_u_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you Sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDetachUnselectedYes" runat="server" CssClass="btn btn-primary" Text="Yes" OnClick="cmdDetachUnselectedYes_Click" />
                    <asp:Button ID="cmdDetachUnselectedNo" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="cmdDocInfo" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="cmdDocInfo_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDocInfo" BehaviorID="mpuDocInfo" TargetControlID="cmdDocInfo" CancelControlID="cmdCancelDocInfo" BackgroundCssClass="at_modelpopup_background_2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlDocInfo" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <asp:HiddenField ID="hndDocument_ID" runat="server" />
        <div id="at_model_docinfo_indent" class="at_modelpopup_indent">
            <div id="at_model_docinfo_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Document Info</h4>
                </div>
                <div id="at_model_docinfo_content" class="at_modelpopup_content styled-bar">
                    <div id="divDocInfo" class="panel-body">
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCancelDocInfo" runat="server" Text="Close" CssClass="btn btn-primary" />
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
            ArrangeGrids();
        });

        function AdjustPopups() {
            AdjustPopupSize(80, 400, 'at_model_detach');
            AdjustPopupSize(80, 400, 'at_model_detach_u');
            AdjustPopupSize(80, 600, 'at_model_docinfo');
        }

        function validateProcess() {
            var validated = true;
            if ($('#<%=cboProcessDocType.ClientID%>').val() == "0") {
                validated = saveChecked();
            }
            else {
                validated = saveUnchecked();
            }
            if (validated == true) {
                if ($('#<%=cboProcessType.ClientID%>').val() == "0") {
                    if ($('#<%=cboProcessDocType.ClientID%>').val() == "0") {
                        $find("mpuRemoveSelected").show();
                        validated = false;
                    }
                    else {
                        $find("mpuRemoveUnselected").show();
                        validated = false;
                    }
                }
            }
            return validated;
        }

        function ArrangeGrids() {
            $('.grid_docs').dataTable({
                "pageLength": 50,
                "order": [[1, 'asc']],
                "responsive": true,
                "autoWidth": true,
                "columnDefs": [
                    { 'orderable': false, targets: 0 },
                    { 'orderable': false, targets: 5 },
                    { 'orderable': false, targets: 6 }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });

            $(".dataTables_filter").children().children('input').attr('placeholder', 'Search...');
        }

        function saveChecked() {
            var SelectedLinks = '';
            $(".chkSelected").each(function () {
                if ($(this).children().hasClass('checked')) {
                    SelectedLinks = SelectedLinks + $(this).data("id") + "|";
                }
            });
            if (SelectedLinks.trim() == '') {
                ShowError('No Douments to Remove');
                return false;
            }
            else {
                $('#<%=hndSelectedDocs.ClientID%>').val(SelectedLinks);
                return true;
            }
        }

        function saveUnchecked() {
            var SelectedLinks = '';
            $(".chkSelected").each(function () {
                if ($(this).children().hasClass('checked') == false) {
                    SelectedLinks = SelectedLinks + $(this).data("id") + "|";
                }
            });
            if (SelectedLinks.trim() == '') {
                ShowError('No Douments to Remove');
                return false;
            }
            else {
                $('#<%=hndSelectedDocs.ClientID%>').val(SelectedLinks);
                return true;
            }
        }

        function showAttachInfo(Doc_ID) {
            $find("mpuDocInfo").show();
            $.ajax({
                type: "GET",
                url: "api/tasks/GetDocInfo",
                data: { Document_ID: Doc_ID },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadDocInfoPassed,
                failure: LoadValuesFailed
            });
            return false;
        }

        function LoadDocInfoPassed(result) {
            $("#divDocInfo").html(result);
        }
    </script>
</asp:Content>