<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid_wf.master" AutoEventWireup="true" CodeBehind="summary_emails.aspx.cs" Inherits="N_Ter_Tasks.summary_emails" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter Tasks > Summary Emails
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Summary Emails -
    <asp:Literal ID="ltrWorkflowName" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <asp:HiddenField ID="hndSummary_ID" runat="server" />
    <div id="divHaveRepos" runat="server" class="row" style="margin-top: -18px">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="padding-xs-vr">
                        <button id="cmdNew" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Summery Email Job</button>
                        <asp:ModalPopupExtender ID="cmdNew_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlData" TargetControlID="cmdNew" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                        </asp:ModalPopupExtender>
                    </div>
                    <div class="table-responsive table-primary no-margin-b">
                        <asp:GridView ID="gvMain" runat="server" CssClass="table table-striped table-hover grid_table grid_dp non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvMain_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="Summary_ID" />
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
                                <asp:BoundField DataField="Event_Type_Desc" HeaderText="Event" />
                                <asp:BoundField DataField="Display_Name" HeaderText="Apply To - Client" />
                                <asp:BoundField DataField="Step_Status" HeaderText="Apply To - Workflow Step" />
                                <asp:BoundField DataField="Receiver_Type_Desc" HeaderText="Email Receiver(s)" />
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
                    <h4 class="panel-title">Add/Edit Summary Email Job</h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="alert alert-info">
                            <h4>Event Details</h4>
                            <div class="form-group">
                                <label>Add to Email Summary When</label>
                                <asp:DropDownList ID="cboEventType" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">[Not Selected]</asp:ListItem>
                                    <asp:ListItem Value="1">The Task Reaches a Step</asp:ListItem>
                                    <asp:ListItem Value="2">The Task is held on a Step for a Number of Days</asp:ListItem>
                                    <asp:ListItem Value="3">A Comment is Added to the Task</asp:ListItem>
                                    <asp:ListItem Value="4">A Document is Added to the Task</asp:ListItem>
                                    <asp:ListItem Value="5">An Addon is Added to the Task</asp:ListItem>
                                    <asp:ListItem Value="6">The Task Due Date is Adjusted</asp:ListItem>
                                    <asp:ListItem Value="7">The [el2] of the Task is Changed</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div id="divNOD" class="form-group">
                                <label>Number of Days</label>
                                <asp:TextBox ID="txtNoOfDays" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    Apply to -
                                    <asp:Literal ID="ltrEl2" runat="server"></asp:Literal></label>
                                <asp:DropDownList ID="cboEl2" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div id="divWfStep" class="form-group">
                                <label>Apply to - Workflow Step</label>
                                <asp:DropDownList ID="cboWorkflowStep" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="alert alert-info">
                            <h4>Receiver Details</h4>
                            <div class="form-group">
                                <label>Add the Item to the Summary of</label>
                                <asp:DropDownList ID="cboReceiverType" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">[Not Selected]</asp:ListItem>
                                    <asp:ListItem Value="1">All Step Owners</asp:ListItem>
                                    <asp:ListItem Value="2">All involved in the Task</asp:ListItem>
                                    <asp:ListItem Value="3">Task Creator</asp:ListItem>
                                    <asp:ListItem Value="4">Task Creator's Supervisor</asp:ListItem>
                                    <asp:ListItem Value="5">All Members of a User Group</asp:ListItem>
                                    <asp:ListItem Value="6">All Members of a User Group and Involved in the Task</asp:ListItem>
                                    <asp:ListItem Value="7">All Members of a User Group and in the Hiararchy of the Task Creator</asp:ListItem>
                                    <asp:ListItem Value="8">Particular User</asp:ListItem>
                                    <asp:ListItem Value="9">Non-Users</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div id="divUsrGrp" class="form-group">
                                <label>User Group</label>
                                <asp:DropDownList ID="cboUserGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div id="divUsr" class="form-group">
                                <label>User</label>
                                <asp:DropDownList ID="cboUser" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div id="divNonUsr" class="form-group">
                                <label>Non-User Emails - Seperated by Commas(,)</label>
                                <asp:TextBox ID="txtReceiverSupport" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="alert alert-info">
                            <h4>Send Instantly/Daily Summary</h4>
                            <div class="form-group">
                                <label>Send the Message</label>
                                <asp:DropDownList ID="cboIsInstant" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">In the Daily Summary</asp:ListItem>
                                    <asp:ListItem Value="1">In an Instant Email</asp:ListItem>
                                </asp:DropDownList>
                            </div>
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
                    <asp:Button ID="cmdDelete" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDelete_Click" />
                    <asp:Button ID="cmdCancel" runat="server" Text="No" CssClass="btn btn-default" />
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

        function ClearControls(reload_codt_drps) {
            $('#divCondition').addClass('hide');

            clearDropDown(['<%=cboEl2.ClientID%>', '<%=cboEventType.ClientID%>', '<%=cboReceiverType.ClientID%>', '<%=cboUser.ClientID%>', '<%=cboUserGroup.ClientID%>', '<%=cboWorkflowStep.ClientID%>', '<%=cboIsInstant.ClientID%>', '<%=cboStepField.ClientID%>', '<%=cboOperator.ClientID%>']);
            clearTextBox(['<%=txtNoOfDays.ClientID%>', '<%=txtReceiverSupport.ClientID%>']);
            clearCheckBox(['<%=chkCondition.ClientID%>']);

            CheckControls();
            CheckConditions('<%=chkCondition.ClientID%>', '<%=cboStepField.ClientID%>', '<%=cboOperator.ClientID%>', '<%=txtCriteria.ClientID%>', 'divCondition', 'cboCondtTemp', 'txtCondtDateTemp', '', reload_codt_drps);
            return false;
        }

        function ResetControls() {
            if ($("#<%=hndSummary_ID.ClientID%>").val().trim() == '0') {
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
                url: "api/tasks/GetSummaryEmail",
                data: { Summary_ID: $("#<%=hndSummary_ID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassed(result) {
            ClearControls(false);

            var resultObject = result;
            $('#<%=cboEventType.ClientID%>').val(resultObject[0].Event_Type);
            $('#<%=cboReceiverType.ClientID%>').val(resultObject[0].Receiver_Type);
            $('#<%=cboEl2.ClientID%>').val(resultObject[0].Entity_L2_ID);
            $('#<%=cboIsInstant.ClientID%>').val(resultObject[0].Is_Instant);
            if ($('#<%=cboEventType.ClientID%>').val() == '1' || $('#<%=cboEventType.ClientID%>').val() == '2' || $('#<%=cboEventType.ClientID%>').val() == '3' || $('#<%=cboEventType.ClientID%>').val() == '4') {
                $('#<%=cboWorkflowStep.ClientID%>').val(resultObject[0].Workflow_Step_ID);
            }
            if ($('#<%=cboEventType.ClientID%>').val() == '2') {
                $('#<%=txtNoOfDays.ClientID%>').val(resultObject[0].Event_Suuport_ID);
            }
            if ($('#<%=cboReceiverType.ClientID%>').val() == '5' || $('#<%=cboReceiverType.ClientID%>').val() == '6' || $('#<%=cboReceiverType.ClientID%>').val() == '7') {
                $('#<%=cboUserGroup.ClientID%>').val(resultObject[0].Receiver_ID);
            }
            if ($('#<%=cboReceiverType.ClientID%>').val() == '8') {
                $('#<%=cboUser.ClientID%>').val(resultObject[0].Receiver_ID);
            }
            if ($('#<%=cboReceiverType.ClientID%>').val() == '9') {
                $('#<%=txtReceiverSupport.ClientID%>').val(resultObject[0].Receiver_Support);
            }
            CheckControls();
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

        function CheckControls() {
            $('#divNOD').addClass('hide');
            $('#divWfStep').addClass('hide');
            $('#divUsrGrp').addClass('hide');
            $('#divUsr').addClass('hide');
            $('#divNonUsr').addClass('hide');
            if ($('#<%=cboEventType.ClientID%>').val() == '1' || $('#<%=cboEventType.ClientID%>').val() == '2' || $('#<%=cboEventType.ClientID%>').val() == '3' || $('#<%=cboEventType.ClientID%>').val() == '4') {
                $('#divWfStep').removeClass('hide');
            }
            else {
                $('#<%=cboWorkflowStep.ClientID%>').val('0');
            }
            if ($('#<%=cboEventType.ClientID%>').val() == '2') {
                $('#divNOD').removeClass('hide');
                $('#<%=cboIsInstant.ClientID%>').val('0');
                $('#<%=cboIsInstant.ClientID%>').prop('disabled', true);
            }
            else {
                $('#<%=txtNoOfDays.ClientID%>').val('0');
                $('#<%=cboIsInstant.ClientID%>').prop('disabled', false);
            }
            if ($('#<%=cboReceiverType.ClientID%>').val() == '5' || $('#<%=cboReceiverType.ClientID%>').val() == '6' || $('#<%=cboReceiverType.ClientID%>').val() == '7') {
                $('#divUsrGrp').removeClass('hide');
            }
            else {
                $('#<%=cboUserGroup.ClientID%>').val('0');
            }
            if ($('#<%=cboReceiverType.ClientID%>').val() == '8') {
                $('#divUsr').removeClass('hide');
            }
            else {
                $('#<%=cboUser.ClientID%>').val('0');
            }
            if ($('#<%=cboReceiverType.ClientID%>').val() == '9') {
                $('#divNonUsr').removeClass('hide');
            }
            else {
                $('#<%=txtReceiverSupport.ClientID%>').val('');
            }
        }
    </script>
</asp:Content>
