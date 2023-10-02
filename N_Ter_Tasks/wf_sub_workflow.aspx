<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid_wf.master" AutoEventWireup="true" CodeBehind="wf_sub_workflow.aspx.cs" Inherits="N_Ter_Tasks.wf_sub_workflow" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="server">
    N-Ter-Tasks > Workflow Sub Worhflow - Automations
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="server">
    Workflow Sub Worhflow - Automations -
    <asp:Label ID="lblWorkflowName" runat="server" Text="Label"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="server">
    <div class="row" style="margin-top: -18px">
        <div class="col-lg-12">
            <div class="tab-base">
                <ul class="nav nav-tabs nav-tabs-simple">
                    <li id="tab1" runat="server" class="">
                        <a data-toggle='tab' href='#divTabCont1'>Sub Workflow Creations</a>
                    </li>
                    <li id="tab2" runat="server" class="">
                        <a data-toggle='tab' href='#divTabCont2'>Parent Task Posts</a>
                    </li>
                </ul>
                <div class="panel panel-info">
                    <div class="panel-body no-padding">
                        <div class="tab-content grid-with-paging">
                            <asp:Panel ID="divTabCont1" ClientIDMode="Static" runat="server" CssClass="">
                                <asp:HiddenField ID="hndLinkID" runat="server" />
                                <asp:HiddenField ID="hndWF_ID" runat="server" />
                                <div class="padding-xs-vr">
                                    <button id="cmdNew" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Sub Workflow Automation</button>
                                    <asp:ModalPopupExtender ID="cmdNew_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlData" TargetControlID="cmdNew" CancelControlID="cmdCloseSubLink" BackgroundCssClass="at_modelpopup_background">
                                    </asp:ModalPopupExtender>
                                </div>
                                <div class="table-responsive table-primary no-margin-b">
                                    <asp:GridView ID="gvMain" runat="server" CssClass="table table-striped table-hover grid_table grid_wf_categories" AutoGenerateColumns="False" OnRowDataBound="gvMain_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Link_ID" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <button id='cmdEdit' type='submit' runat="server" class="btn btn-primary btn-xs" title="Edit"><i class="fa fa-edit button_icon"></i></button>
                                                    <asp:ModalPopupExtender ID="cmdEdit_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlData" TargetControlID="cmdEdit" CancelControlID="cmdCloseSubLink" BackgroundCssClass="at_modelpopup_background">
                                                    </asp:ModalPopupExtender>
                                                </ItemTemplate>
                                                <ItemStyle Width="22px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <button id='cmdDelete' type='submit' runat="server" class="btn btn-danger btn-xs" title="Delete"><i class="fa fa-trash-o button_icon"></i></button>
                                                    <asp:ModalPopupExtender ID="cmdDeleteDoc_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDelete" TargetControlID="cmdDelete" CancelControlID="cmdCloseDelete" BackgroundCssClass="at_modelpopup_background">
                                                    </asp:ModalPopupExtender>
                                                </ItemTemplate>
                                                <ItemStyle Width="22px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Sub_Workflow_Name" HeaderText="Sub Workflow" />
                                            <asp:BoundField DataField="Starting_Step" HeaderText="Start On" />
                                            <asp:BoundField DataField="Finishing_Step" HeaderText="Finish On" />
                                            <asp:BoundField DataField="isAutomatic_SP" HeaderText="Type" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="divTabCont2" ClientIDMode="Static" runat="server" CssClass="">
                                <asp:HiddenField ID="hndPostLinkID" runat="server" />
                                <div class="padding-xs-vr">
                                    <button id="cmdNewPost" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Parent Post</button>
                                    <asp:ModalPopupExtender ID="cmdNewPost_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlParentPost" TargetControlID="cmdNewPost" CancelControlID="cmdParentPostClose" BackgroundCssClass="at_modelpopup_background">
                                    </asp:ModalPopupExtender>
                                </div>
                                <div class="table-responsive table-primary no-margin-b">
                                    <asp:GridView ID="gvParentPost" runat="server" CssClass="table table-striped table-hover grid_table grid_wf_categories" AutoGenerateColumns="False" OnRowDataBound="gvParentPost_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Link_ID" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <button id='cmdPrEdit' type='submit' runat="server" class="btn btn-primary btn-xs" title="Edit"><i class="fa fa-edit button_icon"></i></button>
                                                    <asp:ModalPopupExtender ID="cmdPrEdit_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlParentPost" TargetControlID="cmdPrEdit" CancelControlID="cmdParentPostClose" BackgroundCssClass="at_modelpopup_background">
                                                    </asp:ModalPopupExtender>
                                                </ItemTemplate>
                                                <ItemStyle Width="22px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <button id='cmdPrDelete' type='submit' runat="server" class="btn btn-danger btn-xs" title="Delete"><i class="fa fa-trash-o button_icon"></i></button>
                                                    <asp:ModalPopupExtender ID="cmdPrDelete_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDeleteParentPost" TargetControlID="cmdPrDelete" CancelControlID="cmdCloseDelPrPost" BackgroundCssClass="at_modelpopup_background">
                                                    </asp:ModalPopupExtender>
                                                </ItemTemplate>
                                                <ItemStyle Width="22px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Parent_Workflow" HeaderText="Parent Workflow" />
                                            <asp:BoundField DataField="Parent_Workflow_Step" HeaderText="Workflow Step" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </div>
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
                        <asp:Button ID="cmdCloseSubLink" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add/Edit Sub Workflow Automation</h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Sub Workflow</label>
                            <asp:DropDownList ID="cboSubWorkflow" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Start on Step</label>
                            <asp:DropDownList ID="cboStart" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Finish on Step</label>
                            <asp:DropDownList ID="cboFinish" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox runat="server" ID="chkIsAutomatic" CssClass="checkboxlist" /><span style="padding-left: 10px">Automatically Create the Task</span>
                        </div>
                        <div id="divEx1" class="form-group">
                            <label id="ex1Name"></label>
                            <asp:DropDownList ID="cboEx1Type" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div id="divEx2" class="form-group">
                            <label id="ex2Name"></label>
                            <asp:DropDownList ID="cboEx2Type" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label># of Days to Complete the Task</label>
                            <asp:TextBox ID="txtDaysToFinish" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox runat="server" ID="chkCondition" CssClass="checkboxlist" /><span style="padding-left: 10px">Enable Conditional Execution</span>
                        </div>
                        <div id="divCondition" class="alert alert-info mb">
                            <h4>Apply Only if</h4>
                            <div class="row">
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>Field</label>
                                        <asp:DropDownList ID="cboStepField" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Operator</label>
                                        <asp:DropDownList ID="cboOperator" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="1">&lt;</asp:ListItem>
                                            <asp:ListItem Value="2">&gt;</asp:ListItem>
                                            <asp:ListItem Value="3">=</asp:ListItem>
                                            <asp:ListItem Value="6">&#33;=</asp:ListItem>
                                            <asp:ListItem Value="4">&lt;=</asp:ListItem>
                                            <asp:ListItem Value="5">&gt;=</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>Criteria</label>
                                        <asp:TextBox ID="txtCriteria" runat="server" CssClass="form-control"></asp:TextBox>
                                        <select id="cboCondtTemp" class="form-control"></select>
                                        <input type="text" id="txtCondtDateTemp" class="form-control dtapi"/>
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
                    <asp:Button ID="cmdDeleteOK" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDeleteOK_Click" />
                    <asp:Button ID="cmdCloseDelete" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlParentPost" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_pr_indent" class="at_modelpopup_indent">
            <div id="at_model_pr_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdParentPostClose" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add/Edit Parent Workflow Post</h4>
                </div>
                <div id="at_model_pr_content" class="at_modelpopup_content styled-bar">
                    <asp:HiddenField ID="hndParentWFStep" runat="server" />
                    <asp:HiddenField ID="hndPostStepData" runat="server" />
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Parent Workflow</label>
                            <asp:DropDownList ID="cboParentWorkflow" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div id="divParentStep" class="form-group">
                            <label>Parent Workflow Step</label>
                            <select id="sltParentStep" class="form-control" onchange="LoadStepFields();"></select>
                        </div>
                        <div id="divParentLinks" class="alert alert-info">
                            <h4>Copy Details To Parent Workflow</h4>
                            <div class="form-group">
                                <label>Field Name</label>
                                <select id="sltfield1" class="form-control"></select>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>Perform Parent Post</label>
                            <asp:DropDownList ID="cboExecuteTime" runat="server" CssClass="form-control">
                                <asp:ListItem Value="1">When Task Enters the Step</asp:ListItem>
                                <asp:ListItem Value="0">When Task Leaves the Step</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Include in - Workflow Steps</label>
                            <asp:CheckBoxList ID="chkAllowedWorkflowStep" runat="server" CssClass="checkboxlist"></asp:CheckBoxList>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox runat="server" ID="chkCondition_Pr" CssClass="checkboxlist" /><span style="padding-left: 10px">Enable Conditional Execution</span>
                        </div>
                        <div id="divCondition_Pr" class="alert alert-info mb">
                            <h4>Apply Only if</h4>
                            <div class="row">
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>Field</label>
                                        <asp:DropDownList ID="cboStepField_Pr" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Operator</label>
                                        <asp:DropDownList ID="cboOperator_Pr" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="1">&lt;</asp:ListItem>
                                            <asp:ListItem Value="2">&gt;</asp:ListItem>
                                            <asp:ListItem Value="3">=</asp:ListItem>
                                            <asp:ListItem Value="6">&#33;=</asp:ListItem>
                                            <asp:ListItem Value="4">&lt;=</asp:ListItem>
                                            <asp:ListItem Value="5">&gt;=</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>Criteria</label>
                                        <asp:TextBox ID="txtCriteria_Pr" runat="server" CssClass="form-control"></asp:TextBox>
                                        <select id="cboCondtTemp_Pr" class="form-control"></select>
                                        <input type="text" id="txtCondtDateTemp_Pr" class="form-control dtapi_Pr"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdParentPostSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdParentPostSave_Click" />
                    <asp:Button ID="cmdParentPostReset" runat="server" Text="Reset" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDeleteParentPost" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_model_pr_indent" class="at_modelpopup_indent">
            <div id="at_del_model_pr_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete</h4>
                </div>
                <div id="at_del_model_pr_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDeleteParentPost" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDeleteParentPost_Click" />
                    <asp:Button ID="cmdCloseDelPrPost" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script src="assets/javascripts/app_scripts/operator_pages.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $(window).resize(function () {
                ArrangePopups();
            });
        });

        init.push(function () {
            ArrangePopups();
            ArrangeGrids();
            InitialteCondControls('<%=txtCriteria.ClientID%>', 'dtapi', 'cboCondtTemp', 'txtCondtDateTemp', '<%=cboStepField.ClientID%>');
            InitialteCondControls('<%=txtCriteria_Pr.ClientID%>', 'dtapi_Pr', 'cboCondtTemp_Pr', 'txtCondtDateTemp_Pr', '<%=cboStepField_Pr.ClientID%>');
        });

        function ArrangePopups() {
            AdjustPopupSize(167, 800, 'at_model');
            AdjustPopupSize(80, 400, 'at_del_model');
            AdjustPopupSize(167, 800, 'at_model_pr');
            AdjustPopupSize(80, 400, 'at_del_model_pr');
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

        function ClearControls(reload_codt_drps) {
            $('#divCondition').addClass('hide');

            clearTextBox(['<%=txtDaysToFinish.ClientID%>']);
            clearCheckBox(['<%=chkIsAutomatic.ClientID%>', '<%=chkCondition.ClientID%>'])
            clearDropDown(['<%=cboEx1Type.ClientID%>', '<%=cboEx2Type.ClientID%>', '<%=cboFinish.ClientID%>', '<%=cboStart.ClientID%>', '<%=cboSubWorkflow.ClientID%>', '<%=cboStepField.ClientID%>', '<%=cboOperator.ClientID%>']);

            CheckEx();
            CheckConditions('<%=chkCondition.ClientID%>', '<%=cboStepField.ClientID%>', '<%=cboOperator.ClientID%>', '<%=txtCriteria.ClientID%>', 'divCondition', 'cboCondtTemp', 'txtCondtDateTemp', '', reload_codt_drps);
            return false;
        }

        function ResetControls() {
            if ($("#<%=hndLinkID.ClientID%>").val().trim() == '0') {
                ClearControls(true);
            }
            else {
                LoadValues();
            }
            return false;
        }

        function LoadValues() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetWFSub_WF",
                data: { Link_ID: $("#<%=hndLinkID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassed(result) {
            ClearControls(false);

            var resultObject = result;
            $('#<%=cboSubWorkflow.ClientID%>').val(resultObject[0].Sub_Workflow_ID);
            $('#<%=cboStart.ClientID%>').val(resultObject[0].Workflow_Step_ID);
            $('#<%=cboFinish.ClientID%>').val(resultObject[0].Finish_Step);
            $('#<%=cboEx1Type.ClientID%>').val(resultObject[0].Extra_Field1_Type);
            $('#<%=cboEx2Type.ClientID%>').val(resultObject[0].Extra_Field2_Type);
            $('#<%=txtDaysToFinish.ClientID%>').val(resultObject[0].Days_To_Deadline);
            if (resultObject[0].isAutomatic == '1') {
                onCheckBox('#<%=chkIsAutomatic.ClientID%>');
            }
            CheckEx();
            if (resultObject[0].Enable_Conditions == "1") {
                onCheckBox('#<%=chkCondition.ClientID%>');
                $("#<%=cboStepField.ClientID%> > option").each(function () {
                    if ($(this).val().split('_')[0].trim() == resultObject[0].Workflow_Step_Field_ID) {
                        $('#<%=cboStepField.ClientID%>').val($(this).val());
                    }
                });
                CheckConditions('<%=chkCondition.ClientID%>', '<%=cboStepField.ClientID%>', '<%=cboOperator.ClientID%>', '<%=txtCriteria.ClientID%>', 'divCondition', 'cboCondtTemp', 'txtCondtDateTemp', resultObject[0].Condition_Value, true);
                $('#<%=cboOperator.ClientID%>').val(resultObject[0].Operator_ID);
            }
        }

        function CheckEx() {
            if ($('#<%=chkIsAutomatic.ClientID%>').prop('checked') == false || $('#<%=cboSubWorkflow.ClientID%>').val() == '0') {
                $('#divEx1').addClass('hide');
                $('#<%=cboEx1Type.ClientID%>').val('0');

                $('#divEx2').addClass('hide');
                $('#<%=cboEx2Type.ClientID%>').val('0');
            }
            else {
                var ExName = $("#<%=cboSubWorkflow.ClientID%> option:selected").data('ex').split('|')
                if (ExName[0].trim() != "") {
                    $('#divEx1').removeClass('hide');
                    $('#ex1Name').html(ExName[0].trim());
                }
                else {
                    $('#divEx1').addClass('hide');
                    $('#<%=cboEx1Type.ClientID%>').val('0');
                }
                if (ExName[1].trim() != "") {
                    $('#divEx2').removeClass('hide');
                    $('#ex2Name').html(ExName[1].trim());
                }
                else {
                    $('#divEx2').addClass('hide');
                    $('#<%=cboEx2Type.ClientID%>').val('0');
                }
            }
        }

        function ClearControlsParent(reload_codt_drps) {
            $('#divCondition_Pr').addClass('hide');

            clearDropDown(['<%=cboParentWorkflow.ClientID%>', '<%=cboExecuteTime.ClientID%>', '<%=cboStepField_Pr.ClientID%>', '<%=cboOperator_Pr.ClientID%>']);
            clearCheckListBox(['<%=chkAllowedWorkflowStep.ClientID%>'])
            clearCheckBox(['<%=chkCondition_Pr.ClientID%>']);

            CheckConditions('<%=chkCondition_Pr.ClientID%>', '<%=cboStepField_Pr.ClientID%>', '<%=cboOperator_Pr.ClientID%>', '<%=txtCriteria_Pr.ClientID%>', 'divCondition_Pr', 'cboCondtTemp_Pr', 'txtCondtDateTemp_Pr', '', reload_codt_drps);
            LoadParentWFSteps();
            return false;
        }

        function ResetControlsParent() {
            if ($("#<%=hndPostLinkID.ClientID%>").val().trim() == '0') {
                ClearControlsParent(true);
            }
            else {
                LoadValuesParent();
            }
            return false;
        }

        function LoadValuesParent() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetParentPost",
                data: { Link_ID: $("#<%=hndPostLinkID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesParentPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesParentPassed(result) {
            ClearControlsParent(false);
            var resultObject = result;
            $('#<%=cboParentWorkflow.ClientID%>').val(resultObject[0].Parent_Workflow_ID);
            $('#<%=cboExecuteTime.ClientID%>').val(resultObject[0].At_Begining);
            if (resultObject[0].Enable_Conditions == "1") {
                onCheckBox('#<%=chkCondition_Pr.ClientID%>');
                $("#<%=cboStepField_Pr.ClientID%> > option").each(function () {
                    if ($(this).val().split('_')[0].trim() == resultObject[0].Workflow_Step_Field_ID) {
                        $('#<%=cboStepField_Pr.ClientID%>').val($(this).val());
                    }
                });
                CheckConditions('<%=chkCondition_Pr.ClientID%>', '<%=cboStepField_Pr.ClientID%>', '<%=cboOperator_Pr.ClientID%>', '<%=txtCriteria_Pr.ClientID%>', 'divCondition_Pr', 'cboCondtTemp_Pr', 'txtCondtDateTemp_Pr', resultObject[0].Condition_Value, true);
                $('#<%=cboOperator_Pr.ClientID%>').val(resultObject[0].Operator_ID);
            }

            var resultObjectSub = resultObject[0].WF_Steps_For_Post;
            $('#<%=chkAllowedWorkflowStep.ClientID%> input').each(function () {
                for (var i = 0; i < resultObjectSub.length; i++) {
                    if (resultObjectSub[i].Workflow_Step_ID.trim() == $(this).val().trim()) {
                        onCheckBox(this);
                    }
                }
            });

            LoadParentWFSteps();
            $('#sltParentStep').val(resultObject[0].Parent_Workflow_Step_ID);

            LoadStepFields();
            var resultObjectSub2 = resultObject[0].WF_Links_For_Post;
            console.log(resultObjectSub2);
            for (var i = 0; i < resultObjectSub2.length; i++) {
                $('#post_link_' + resultObjectSub2[i].Parent_Workflow_Step_Field_ID).val(resultObjectSub2[i].Parent_Workflow_Step_Field_ID + '|' + resultObjectSub2[i].Workflow_Step_Field_ID);
            }
        }

        function LoadParentWFSteps() {
            onPleaseWait();
            $('#sltParentStep').empty();
            $('#divParentLinks').html('');
            $('#divParentLinks').addClass('hide');

            if ($('#<%=cboParentWorkflow.ClientID%>').val() == 0) {
                $('#divParentStep').addClass('hide');
            }
            else {
                $('#divParentStep').removeClass('hide');
                $('#sltParentStep').append(
                    $('<option>', {
                        value: '0',
                        text: '[Not Selected]'
                    }, '<option/>'));
                $.ajax({
                    type: "GET",
                    async: false,
                    url: "api/tasks/GetWorkflowSteps",
                    data: { Workflow_ID: $('#<%=cboParentWorkflow.ClientID%>').val() },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var resultObject = data;
                        for (var i = 0; i < resultObject.length; i++) {
                            $('#sltParentStep').append(
                                $('<option>', {
                                    value: resultObject[i].Item_ID,
                                    text: resultObject[i].Item_Name.trim()
                                }, '<option/>'));
                        }
                    },
                    failure: LoadValuesFailed
                });
            }
            offPleaseWait();
        }

        function LoadStepFields() {
            onPleaseWait();
            $('#divParentLinks').html('');
            if ($('#sltParentStep').val() == 0) {
                $('#divParentLinks').addClass('hide');
            }
            else {
                $('#divParentLinks').removeClass('hide');
                var divText = '<h4>Post Data</h4>';
                $.ajax({
                    type: "GET",
                    async: false,
                    url: "api/tasks/GetParentPostFields",
                    data: { Workflow_Step_ID: $('#sltParentStep').val(), Sub_Workflow_ID: $('#<%=hndWF_ID.ClientID%>').val() },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var resultObject1 = data[0];
                        var resultObject2 = data[1];
                        for (var i = 0; i < resultObject1.length; i++) {
                            divText = divText + "<div class='form-group'>" +
                                "<label>" + resultObject1[i].Item_Name + "</label>" +
                                "<select id='post_link_" + resultObject1[i].Item_ID + "' class='form-control post-data'>" +
                                "<option value='0'>[Not Selected]</option>";
                            for (var ii = 0; ii < resultObject2.length; ii++) {
                                divText = divText + "<option value='" + resultObject1[i].Item_ID + "|" + resultObject2[ii].Item_ID + "'>" + resultObject2[ii].Item_Name + "</option>";
                            }
                            divText = divText + "</select>" +
                                "</div>";

                        }
                    },
                    failure: LoadValuesFailed
                });
                $('#divParentLinks').html(divText);
            }
            offPleaseWait();
        }
    </script>
</asp:Content>
