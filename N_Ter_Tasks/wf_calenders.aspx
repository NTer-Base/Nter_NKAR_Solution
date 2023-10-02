<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid_wf.master" AutoEventWireup="true" CodeBehind="wf_calenders.aspx.cs" Inherits="N_Ter_Tasks.wf_calenders" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Calendars for Workflows
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Calendars for Workflows -
    <asp:Label ID="lblWorkflow" runat="server" Text="Label"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <asp:HiddenField ID="hndLinkID" runat="server" />
    <div id="divNoCalenders" runat="server" class="row" style="margin-top: -18px">
        <div class="col-md-12">
            <div class="alert alert-success">
                <strong>Information !</strong> There are no Calendars available.
            </div>
        </div>
    </div>
    <div id="divNoFields" runat="server" class="row" style="margin-top: -18px">
        <div class="col-md-12">
            <div class="alert alert-success">
                <strong>Information !</strong> Selected Workflow doesn't have enough maching fields to add Calendars.
            </div>
        </div>
    </div>
    <div id="divCanAddCals" runat="server" class="row" style="margin-top: -18px">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="padding-xs-vr">
                        <button id="cmdNew" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Calendar Link</button>
                        <asp:ModalPopupExtender ID="cmdNew_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlData" TargetControlID="cmdNew" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                        </asp:ModalPopupExtender>
                    </div>
                    <div class="table-responsive table-primary no-margin-b">
                        <asp:GridView ID="gvMain" runat="server" CssClass="table table-striped table-hover grid_table grid_wf_categories non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvMain_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="Calender_Link_ID" />
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
                                <asp:BoundField DataField="Calender_Name" HeaderText="Calendar" />
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
                    <h4 class="panel-title">Add/Edit Calendar Link</h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Calendar</label>
                            <asp:DropDownList ID="cboCalender" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Start Date</label>
                            <asp:DropDownList ID="cboStartDate" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Start Time</label>
                            <asp:DropDownList ID="cboStartTime" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Duration</label>
                            <asp:DropDownList ID="cboDuration" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Subject</label>
                            <asp:DropDownList ID="cboSubject" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Description</label>
                            <asp:DropDownList ID="cboDescription" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Add to Calendar when Task Reaches</label>
                            <asp:DropDownList ID="cboStep" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Move to (if Calendar Event Validation Fails)</label>
                            <asp:DropDownList ID="cboBounceBack" runat="server" CssClass="form-control"></asp:DropDownList>
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

            clearDropDown(['<%=cboCalender.ClientID%>', '<%=cboStartDate.ClientID%>', '<%=cboStartTime.ClientID%>', '<%=cboDuration.ClientID%>', '<%=cboSubject.ClientID%>', '<%=cboDescription.ClientID%>', '<%=cboBounceBack.ClientID%>', '<%=cboStep.ClientID%>', '<%=cboStepField.ClientID%>', '<%=cboOperator.ClientID%>']);
            clearCheckBox(['<%=chkCondition.ClientID%>']);

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
                url: "api/tasks/GetWorkflowCalender",
                data: { Calender_Link_ID: $("#<%=hndLinkID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassed(result) {
            ClearControls(false);

            var resultObject = result;
            $('#<%=cboCalender.ClientID%>').val(resultObject[0].Calender_ID);
            $('#<%=cboStartDate.ClientID%>').val(resultObject[0].Start_Date_Field_ID);
            $('#<%=cboStartTime.ClientID%>').val(resultObject[0].Start_Time_Field_ID);
            $('#<%=cboDuration.ClientID%>').val(resultObject[0].Duration_Field_ID);
            $('#<%=cboSubject.ClientID%>').val(resultObject[0].Subject_Field_ID);
            $('#<%=cboDescription.ClientID%>').val(resultObject[0].Description_Field_ID);
            $('#<%=cboStep.ClientID%>').val(resultObject[0].Workflow_Step_ID);
            $('#<%=cboBounceBack.ClientID%>').val(resultObject[0].Bounce_Back_Step_ID);
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
    </script>
</asp:Content>
