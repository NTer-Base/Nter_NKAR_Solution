<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid.master" AutoEventWireup="true" CodeBehind="workflows.aspx.cs" Inherits="N_Ter_Tasks.workflows" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Workflows
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Workflows
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <asp:HiddenField ID="hndWorkflowID" runat="server" />
    <div class="panel panel-info" style="margin-bottom: 350px">
        <div class="panel-body">
            <div class="padding-xs-vr">
                <button id="cmdNew" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Workflow</button>
                <asp:ModalPopupExtender ID="cmdNew_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlData" TargetControlID="cmdNew" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                </asp:ModalPopupExtender>
            </div>
            <div class="table-responsive table-primary no-margin-b">
                <asp:GridView ID="gvMain" runat="server" CssClass="table table-striped table-hover grid_table grid_workflows non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvMain_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Walkflow_ID" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <button id='cmdEdit' type='submit' runat="server" class="btn btn-primary btn-xs" title="Edit Workflow"><i class="fa fa-edit button_icon"></i></button>
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
                                <button id='cmdDelete' type='submit' runat="server" class="btn btn-danger btn-xs" title="Delete Workflow"><i class="fa fa-trash-o button_icon"></i></button>
                                <asp:ModalPopupExtender ID="cmdDelete_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDelete" TargetControlID="cmdDelete" CancelControlID="cmdCancel" BackgroundCssClass="at_modelpopup_background">
                                </asp:ModalPopupExtender>
                            </ItemTemplate>
                            <ItemStyle Width="22px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Workflow_Name" HeaderText="Workflow Name">
                            <ItemStyle Width="50%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Workflow_Category_Name" HeaderText="Category" />
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
                    <h4 class="panel-title">Add Workflow</h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="tab-base">
                            <ul class="nav nav-tabs nav-tabs-simple">
                                <li id="tab1" class="active">
                                    <a data-toggle='tab' href='#tab_cont1'>Main Info</a>
                                </li>
                                <li id="tab4">
                                    <a data-toggle='tab' href='#tab_cont4'>Linkage</a>
                                </li>
                                <li id="tab2">
                                    <a data-toggle='tab' href='#tab_cont2'>Special Fields</a>
                                </li>
                                <li id="tab3">
                                    <a data-toggle='tab' href='#tab_cont3'>
                                        <asp:Literal ID="ltrEL2s" runat="server"></asp:Literal></a>
                                </li>
                                <li id="tab5">
                                    <a data-toggle='tab' href='#tab_cont5'>Guests</a>
                                </li>
                            </ul>
                            <div class="panel panel-info">
                                <div class="panel-body no-padding">
                                    <div class="tab-content grid-with-paging">
                                        <div id="tab_cont1" class="tab-pane fade active in">
                                            <div class="form-group">
                                                <label>Workflow Name</label>
                                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Workflow Category</label>
                                                <asp:DropDownList ID="cboWorkflowCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label>Workflow Document Types</label>
                                                <asp:TextBox ID="txtDocTypes" runat="server" CssClass="form-control" placeholder="Eg:- Invoice|Shipment Clearance Letter"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Workflow Comment Types</label>
                                                <asp:TextBox ID="txtCommType" runat="server" CssClass="form-control" placeholder="Eg:- General|Special"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Deadline Type</label>
                                                <asp:DropDownList ID="cboDeadlineType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="1">Select at Task Creation</asp:ListItem>
                                                    <asp:ListItem Value="2">Select from a Schedule</asp:ListItem>
                                                    <asp:ListItem Value="3">Specific Number of Days</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div id="divSchedule" class="form-group">
                                                <label>Schedule Name</label>
                                                <asp:DropDownList ID="cboSchedule" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                            <div id="divNumberOfDays" class="form-group">
                                                <label id="divDurationType">Number of Days to Complete a Task</label>
                                                <asp:TextBox ID="txtNumberOfDays" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Number of Concurrent Tasks Allowed (Same User)</label>
                                                <asp:TextBox ID="countMultipleTasks" runat="server" CssClass="form-control" TextMode="Number">100</asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Retention Period</label>
                                                <div class="row">
                                                    <div class="col-md-6 pl">
                                                        <asp:TextBox ID="txtRetentionAmount" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 pr">
                                                        <asp:DropDownList ID="cboRetentionType" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="3">Months</asp:ListItem>
                                                            <asp:ListItem Value="4">Years</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:CheckBox ID="chkAllowAssignSubordinates" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Allow Assigning Tasks for Subordinates</span>
                                            </div>
                                        </div>
                                        <div id="tab_cont4" class="tab-pane fade">
                                            <div class="form-group">
                                                <label>Document Project</label>
                                                <asp:DropDownList ID="cboDocumentProject" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label>Workflow Type</label>
                                                <asp:DropDownList ID="cboWorkflowType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="1">Main Workflow</asp:ListItem>
                                                    <asp:ListItem Value="2">Sub Workflow</asp:ListItem>
                                                    <asp:ListItem Value="3">Main & Sub Workflow</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div id="divShowParent" class="form-group">
                                                <asp:CheckBox runat="server" ID="chkShowParent" CssClass="checkboxlist" /><span style="padding-left: 10px">Show Parent Task Documents/History</span>
                                            </div>
                                            <div class="form-group">
                                                <asp:CheckBox runat="server" ID="chkShowSubDocs" CssClass="checkboxlist" /><span style="padding-left: 10px">Show Sub Task Documents (Enabling this can Increase the Loading Time)</span>
                                            </div>
                                            <div class="form-group">
                                                <asp:CheckBox runat="server" ID="chkShowSubComments" CssClass="checkboxlist" /><span style="padding-left: 10px">Show Sub Task Comments (Enabling this can Increase the Loading Time)</span>
                                            </div>
                                            <div class="form-group">
                                                <label>Step Changes</label>
                                            </div>
                                            <div class="form-group">
                                                <asp:CheckBox runat="server" ID="chkChangeTLOnChanges" CssClass="checkboxlist" /><span style="padding-left: 10px">Rearrange Step Deadlines of Existing Tasks on Step Updates</span>
                                                <ul>
                                                    <li>New Step Creations</li>
                                                    <li>Existing Step Updates</li>
                                                    <li>Step Order Updates</li>
                                                    <li>New Step Condition Creations</li>
                                                    <li>Existing Step Condition Updates</li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div id="tab_cont2" class="tab-pane fade">
                                            <div class="panel panel-info">
                                                <div class="panel-heading">
                                                    Special Field 1
                                                </div>
                                                <div class="panel-body">
                                                    <div class="form-group">
                                                        <label>Name</label>
                                                        <asp:TextBox ID="txtExtraFieldName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>Type of Content</label>
                                                        <asp:DropDownList ID="cboExtraType" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="1">Text</asp:ListItem>
                                                            <asp:ListItem Value="2">Selection</asp:ListItem>
                                                            <asp:ListItem Value="3">Mater Table</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div id="divExtraSelection" class="form-group">
                                                        <label>Selection Text</label>
                                                        <asp:TextBox ID="txtExtraSelection" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div id="divExtraMasterTable" class="form-group">
                                                        <label>Master Table</label>
                                                        <asp:DropDownList ID="cboExtraMasterTable" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                    <div class="form-group no-margin-b">
                                                        <asp:CheckBox ID="chkExtra1TaskStart" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Show on Task Start</span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-info no-margin-b">
                                                <div class="panel-heading">
                                                    Special Field 2
                                                </div>
                                                <div class="panel-body">
                                                    <div class="form-group">
                                                        <label>Name</label>
                                                        <asp:TextBox ID="txtExtraField2Name" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>Type of Content</label>
                                                        <asp:DropDownList ID="cboExtra2Type" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="1">Text</asp:ListItem>
                                                            <asp:ListItem Value="2">Selection</asp:ListItem>
                                                            <asp:ListItem Value="3">Mater Table</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div id="divExtra2Selection" class="form-group">
                                                        <label>Selection Text</label>
                                                        <asp:TextBox ID="txtExtra2Selection" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div id="divExtra2MasterTable" class="form-group">
                                                        <label>Master Table</label>
                                                        <asp:DropDownList ID="cboExtra2MasterTable" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                    <div class="form-group no-margin-b">
                                                        <asp:CheckBox ID="chkExtra2TaskStart" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Show on Task Start</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tab_cont3" class="tab-pane fade">
                                            <div class="form-group-margin text-right">
                                                <button class="btn btn-labeled btn-primary" onclick="return selectAllEL2();">Select All</button>
                                                <button class="btn btn-labeled btn-primary" onclick="return deSelectAllEL2();">Unselect All</button>
                                            </div>
                                            <asp:HiddenField ID="hndEl2" ClientIDMode="Static" runat="server" />
                                            <div class="table-responsive table-primary no-margin-b">
                                                <asp:GridView ID="gvEntity_L2" runat="server" CssClass="table table-striped table-hover grid_table grid_l2" AutoGenerateColumns="False" OnRowDataBound="gvEntity_L2_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="Entity_L2_ID" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxlist el2" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="22px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Display_Name" HeaderText="Entity Name" />
                                                        <asp:TemplateField HeaderText="No of Units">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtUnits" runat="server" CssClass="form-control nou" Width="100"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="100px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div id="tab_cont5" class="tab-pane fade">
                                            <div class="alert alert-success">
                                                If <b>Task Creation</b> is allowed for <b>Guests</b> following settings will get affected. 
                                            </div>
                                            <div class="form-group">
                                                <label>Help Text - Task Start</label>
                                                <asp:TextBox ID="txtHelpTaskStart" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>
                                                    Label -
                                                    <asp:Literal ID="ltrEL2" runat="server"></asp:Literal></label>
                                                <asp:TextBox ID="txtLabelEL2" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Label - Due Date</label>
                                                <asp:TextBox ID="txtLabelDueDate" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Label - Due Time</label>
                                                <asp:TextBox ID="txtLabelDueTime" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:CheckBox ID="chkShowQueue" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Show Task Queue on Task Start</span>
                                            </div>
                                            <div class="form-group">
                                                <label>Label - Task Queue</label>
                                                <asp:TextBox ID="txtLabelTaskQueue" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Help Text - Task Post</label>
                                                <asp:TextBox ID="txtHelpTaskPost" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
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
    <asp:HiddenField ID="hndDuplicate" runat="server" />
    <asp:ModalPopupExtender ID="chndDuplicate_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuDuplicate" PopupControlID="pnlDuplicate" TargetControlID="hndDuplicate" CancelControlID="cmdCancelDuplicate" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlDuplicate" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_dup_indent" class="at_modelpopup_indent">
            <div id="at_model_dup_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCancelDuplicate" runat="server" Text="&times;" CssClass="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Duplicate Workflow</h4>
                </div>
                <div id="at_model_dup_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Workflow to Duplicate</label>
                            <asp:TextBox ID="txtCurrentWorkflow" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>New Workflow Name</label>
                            <asp:TextBox ID="txtNewWorkflowName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCreateDuplicate" runat="server" Text="Create Duplicate Workflow" CssClass="btn btn-primary" OnClick="cmdCreateDuplicate_Click" />
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
            AdjustPopupSize(167, 800, 'at_model_dup');
            AdjustPopupSize(80, 400, 'at_del_model');
        }

        function ArrangeGrids() {
            $('.grid_workflows').dataTable({
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

            $('.grid_l2').dataTable({
                "paging": false,
                "order": [[1, 'asc']],
                "responsive": true,
                "autoWidth": true,
                "columnDefs": [
                    { 'orderable': false, targets: 0 },
                    { 'orderable': false, targets: 2 }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });

            $(".dataTables_filter").children().children('input').attr('placeholder', 'Search...');
        }

        function ClearControls() {
            clearTextBox(['<%=txtName.ClientID%>', '<%=txtDocTypes.ClientID%>', '<%=txtCommType.ClientID%>', '<%=txtExtraFieldName.ClientID%>', '<%=txtExtraField2Name.ClientID%>', '<%=txtNumberOfDays.ClientID%>', '<%=txtExtraSelection.ClientID%>', '<%=txtExtra2Selection.ClientID%>', '<%=txtHelpTaskStart.ClientID%>', '<%=txtLabelEL2.ClientID%>', '<%=txtLabelDueDate.ClientID%>', '<%=txtLabelDueTime.ClientID%>', '<%=txtLabelTaskQueue.ClientID%>', '<%=txtHelpTaskPost.ClientID%>', '<%=txtRetentionAmount.ClientID%>']);
            clearDropDown(['<%=cboWorkflowType.ClientID%>', '<%=cboWorkflowCategory.ClientID%>', '<%=cboDeadlineType.ClientID%>', '<%=cboExtraType.ClientID%>', '<%=cboExtraMasterTable.ClientID%>', '<%=cboExtra2Type.ClientID%>', '<%=cboExtra2MasterTable.ClientID%>', '<%=cboRetentionType.ClientID%>']);
            clearCheckBox(['<%=chkAllowAssignSubordinates.ClientID%>', '<%=chkExtra1TaskStart.ClientID%>', '<%=chkExtra2TaskStart.ClientID%>', '<%=chkShowParent.ClientID%>', '<%=chkShowSubComments.ClientID%>', '<%=chkShowSubDocs.ClientID%>', '<%=chkShowQueue.ClientID%>', '<%=chkChangeTLOnChanges.ClientID%>']);
            SelectExtraFieldType();
            deSelectAllEL2();
            $(".nou").each(function () {
                $(this).val('0');
            });
            CheckDeadlineType();
            CheckWFType();
            showDataTab1();
            return false;
        }

        function ResetControls() {
            if ($("#<%=hndWorkflowID.ClientID%>").val().trim() == '0') {
                ClearControls();
            }
            else {
                LoadValues();
            }
            return false;
        }

        function SelectExtraFieldType() {
            $('#divExtraSelection').addClass('hide');
            $('#divExtra2Selection').addClass('hide');
            $('#divExtraMasterTable').addClass('hide');
            $('#divExtra2MasterTable').addClass('hide');

            if ($('#<%=cboExtraType.ClientID%>').val() == '2') {
                $('#divExtraSelection').removeClass('hide');
            }
            else if ($('#<%=cboExtraType.ClientID%>').val() == '3') {
                $('#divExtraMasterTable').removeClass('hide');
            }

            if ($('#<%=cboExtra2Type.ClientID%>').val() == '2') {
                $('#divExtra2Selection').removeClass('hide');
            }
            else if ($('#<%=cboExtra2Type.ClientID%>').val() == '3') {
                $('#divExtra2MasterTable').removeClass('hide');
            }
        }

        function showDataTab1() {
            $('#tab1').addClass('active');
            $('#tab2').removeClass('active');
            $('#tab3').removeClass('active');
            $('#tab4').removeClass('active');
            $('#tab5').removeClass('active');

            $('#tab_cont1').addClass('active in');
            $('#tab_cont2').removeClass('active in');
            $('#tab_cont3').removeClass('active in');
            $('#tab_cont4').removeClass('active in');
            $('#tab_cont5').removeClass('active in');
        }

        function CheckDeadlineType() {
            if ($("#<%=cboDeadlineType.ClientID%>").val() == '1') {
                $("#divSchedule").addClass('hide');
                $("#divNumberOfDays").removeClass('hide');
                $("#divDurationType").html('Default Number of Days to Complete a Task');
            }
            else if ($("#<%=cboDeadlineType.ClientID%>").val() == '2') {
                $("#divSchedule").removeClass('hide');
                $("#divNumberOfDays").addClass('hide');
            }
            else if ($("#<%=cboDeadlineType.ClientID%>").val() == '3') {
                $("#divSchedule").addClass('hide');
                $("#divNumberOfDays").removeClass('hide');
                $("#divDurationType").html('Number of Days to Complete a Task');
            }
        }

        function CheckWFType() {
            if ($("#<%=cboWorkflowType.ClientID%>").val() == '1') {
                offCheckBox('#<%=chkShowParent.ClientID%>');
                $("#divShowParent").addClass("hide");
            }
            else {
                $("#divShowParent").removeClass("hide");
            }
        }

        function LoadValues() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetWorkflow",
                data: { Walkflow_ID: $("#<%=hndWorkflowID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassed(result) {
            ClearControls();

            var resultObject = result;

            $('#<%=hndWorkflowID.ClientID%>').val(resultObject[0].Walkflow_ID);
            $('#<%=txtName.ClientID%>').val(resultObject[0].Workflow_Name);
            $('#<%=cboWorkflowCategory.ClientID%>').val(resultObject[0].Workflow_Category_ID);
            $('#<%=cboSchedule.ClientID%>').val(resultObject[0].Schedule_ID);
            $('#<%=txtDocTypes.ClientID%>').val(resultObject[0].Workflow_Doc_Types);
            $('#<%=txtCommType.ClientID%>').val(resultObject[0].Workflow_Comment_Types);
            $('#<%=txtExtraFieldName.ClientID%>').val(resultObject[0].Exrta_Field_Naming);
            $('#<%=txtExtraField2Name.ClientID%>').val(resultObject[0].Exrta_Field2_Naming);
            $('#<%=txtExtraSelection.ClientID%>').val(resultObject[0].Extra_Field_Selection);
            $('#<%=txtExtra2Selection.ClientID%>').val(resultObject[0].Extra_Field2_Selection);
            $('#<%=cboExtraType.ClientID%>').val(resultObject[0].Exrta_Field_Type);
            $('#<%=cboExtra2Type.ClientID%>').val(resultObject[0].Exrta_Field2_Type);
            $('#<%=cboExtraMasterTable.ClientID%>').val(resultObject[0].Extra_Field_Master_Table_ID);
            $('#<%=cboExtra2MasterTable.ClientID%>').val(resultObject[0].Extra_Field2_Master_Table_ID);
            SelectExtraFieldType();
            $('#<%=cboDeadlineType.ClientID%>').val(resultObject[0].Deadline_Type);
            $('#<%=txtNumberOfDays.ClientID%>').val(resultObject[0].Number_Of_Days);
            $('#<%=cboDocumentProject.ClientID%>').val(resultObject[0].Document_Project_ID);
            $('#<%=countMultipleTasks.ClientID%>').val(resultObject[0].Number_Of_Concurrent_Tasks_Allowed);
            $('#<%=cboWorkflowType.ClientID%>').val(resultObject[0].Workflow_Type);
            $('#<%=txtHelpTaskPost.ClientID%>').val(resultObject[0].Guest_Help_Task_Post);
            $('#<%=txtHelpTaskStart.ClientID%>').val(resultObject[0].Guest_Help_Task_Start);
            $('#<%=txtLabelDueDate.ClientID%>').val(resultObject[0].Guest_Due_Date_Label);
            $('#<%=txtLabelDueTime.ClientID%>').val(resultObject[0].Guest_Due_Time_Label);
            $('#<%=txtLabelEL2.ClientID%>').val(resultObject[0].Guest_Entity_L2_Label);
            $('#<%=txtLabelTaskQueue.ClientID%>').val(resultObject[0].Guest_Task_Queue_Label);
            $('#<%=cboRetentionType.ClientID%>').val(resultObject[0].Retention_Period_Type);
            $('#<%=txtRetentionAmount.ClientID%>').val(resultObject[0].Retention_Amount);
            if (parseInt(resultObject[0].Allow_Assigning_For_Subordinates) == 1) {
                onCheckBox('#<%=chkAllowAssignSubordinates.ClientID%>');
            }
            if (parseInt(resultObject[0].Extra_Field_Task_Start) == 1) {
                onCheckBox('#<%=chkExtra1TaskStart.ClientID%>');
            }
            if (parseInt(resultObject[0].Extra_Field2_Task_Start) == 1) {
                onCheckBox('#<%=chkExtra2TaskStart.ClientID%>');
            }

            if (parseInt(resultObject[0].Show_Parent_Task_History) == 1) {
                onCheckBox('#<%=chkShowParent.ClientID%>');
            }
            if (parseInt(resultObject[0].Show_SubTask_Comments) == 1) {
                onCheckBox('#<%=chkShowSubComments.ClientID%>');
            }
            if (parseInt(resultObject[0].Change_TL_On_Updates) == 1) {
                onCheckBox('#<%=chkChangeTLOnChanges.ClientID%>');
            }
            if (parseInt(resultObject[0].Show_SubTask_Documents) == 1) {
                onCheckBox('#<%=chkShowSubDocs.ClientID%>');
            }
            if (parseInt(resultObject[0].Guest_Show_Queue_Task_Start) == 1) {
                onCheckBox('#<%=chkShowQueue.ClientID%>');
            }

            CheckDeadlineType();

            CheckWFType();

            var resultObjectSub2 = resultObject[0].Workflow_EL2;
            $('#hndEl2').val('');

            $(".el2").each(function () {
                for (var i = 0; i < resultObjectSub2.length; i++) {
                    if ($(this).attr('data-id').trim() == resultObjectSub2[i].Entity_L2_ID.trim()) {
                        $(this).children().children('input').each(function () {
                            onCheckBox(this);
                        });
                        $(this).parent().parent().children().children('.nou').val(resultObjectSub2[i].No_Of_Units.trim());
                        $('#hndEl2').val($('#hndEl2').val() + resultObjectSub2[i].Entity_L2_ID.trim() + "|");
                    }
                }
            });
        }

        function selectAllEL2() {
            selectCheckBoxRange('el2');
            $('#hndEl2').val('');
            $(".el2").each(function () {
                $('#hndEl2').val($('#hndEl2').val() + $(this).attr('data-id').trim() + "|");
            });
            return false;
        }

        function deSelectAllEL2() {
            $('#hndEl2').val('');
            clearCheckBoxRange('el2');
            return false;
        }

        function duplicateWorkflow(c_wf_id, c_workflow) {
            $('#<%=hndWorkflowID.ClientID%>').val(c_wf_id);
            $('#<%=txtCurrentWorkflow.ClientID%>').val(c_workflow);
            $('#<%=txtNewWorkflowName.ClientID%>').val('');
            $find("mpuDuplicate").show();
            return false;
        }
    </script>
</asp:Content>
