<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid_wf.master" AutoEventWireup="true" CodeBehind="wf_api_calls.aspx.cs" Inherits="N_Ter_Tasks.wf_api_calls" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Workflow API Calls
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Workflow API Calls -
    <asp:Label ID="lblWorkflowName" runat="server" Text="Label"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <asp:HiddenField ID="hndAPI_Call_ID" runat="server" />
    <asp:HiddenField ID="hndWF_ID" runat="server" />
    <div class="panel panel-info">
        <div class="panel-body">
            <div class="padding-xs-vr">
                <button id="cmdNew" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New API Call</button>
                <asp:ModalPopupExtender ID="cmdNew_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlData" TargetControlID="cmdNew" CancelControlID="cmdCloseAPI" BackgroundCssClass="at_modelpopup_background">
                </asp:ModalPopupExtender>
            </div>
            <div class="table-responsive table-primary no-margin-b">
                <asp:GridView ID="gvMain" runat="server" CssClass="table table-striped table-hover grid_table grid_wf_categories" AutoGenerateColumns="False" OnRowDataBound="gvMain_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="API_Call_ID" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <button id='cmdEdit' type='submit' runat="server" class="btn btn-primary btn-xs" title="Edit"><i class="fa fa-edit button_icon"></i></button>
                                <asp:ModalPopupExtender ID="cmdEdit_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlData" TargetControlID="cmdEdit" CancelControlID="cmdCloseAPI" BackgroundCssClass="at_modelpopup_background">
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
                        <asp:BoundField DataField="API_Call_Name" HeaderText="API Call Name" />
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
                        <button id='cmdHelp' type='submit' runat="server" class="btn btn-success" title="Help"><i class="fa fa-question"></i></button>
                        <asp:ModalPopupExtender ID="cmdHelp_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlHelp" TargetControlID="cmdHelp" CancelControlID="cmdCloseHelp" BackgroundCssClass="at_modelpopup_background_2">
                        </asp:ModalPopupExtender>
                        <asp:Button ID="cmdCloseAPI" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add/Edit API Call</h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>API Call Name</label>
                            <asp:TextBox ID="txtAPICallName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>URL (Template)</label>
                            <asp:TextBox ID="txtURL" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>API Method</label>
                            <asp:DropDownList ID="cboAPIMethod" runat="server" CssClass="form-control">
                                <asp:ListItem Value="GET">GET</asp:ListItem>
                                <asp:ListItem Value="POST">POST</asp:ListItem>
                                <asp:ListItem Value="PUT">PUT</asp:ListItem>
                                <asp:ListItem Value="PATCH">PATCH</asp:ListItem>
                                <asp:ListItem Value="DELETE">DELETE</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Content Type</label>
                            <asp:TextBox ID="txtContentType" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Request Body</label>
                            <asp:TextBox ID="txtRequestBody" runat="server" CssClass="form-control" TextMode="MultiLine" Height="200"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Perform API Call</label>
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
    <asp:Panel ID="pnlHelp" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_help_indent" class="at_modelpopup_indent">
            <div id="at_model_help_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Template Help</h4>
                </div>
                <div id="at_model_help_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <b>Loggedn in User Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_code] : </span>User Code
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_name] : </span>Full Name
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_designation] : </span>Designation
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_phone] : </span>Phone
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_email] : </span>Email
                            </div>
                            <div class="col-md-12">
                                <span class="text-danger">[s_signature] : </span>Singature Image (Only for Template Documents)
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Task Owner Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[o_code] : </span>User Code
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[o_name] : </span>Full Name
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[o_designation] : </span>Designation
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[o_phone] : </span>Phone
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[o_email] : </span>Email
                            </div>
                            <div class="col-md-12">
                                <span class="text-danger">[o_signature] : </span>Singature Image (Only for Template Documents)
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Other User Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_name:Group Name] : </span>Full Name of the Last Accessed user in the User Group
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_name:Step Number] : </span>Full Name of the Last Accessed user of the Workflow Step
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_designation:Group Name] : </span>Designation of the Last Accessed user in the User Group
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_designation:Step Number] : </span>Designation of the Last Accessed user of the Workflow Step
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_email:Group Name] : </span>Email of the Last Accessed user in the User Group
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_email:Step Number] : </span>Email of the Last Accessed user of the Workflow Step
                            </div>
                            <div class="col-md-12 mt10">
                                <b>
                                    <asp:Literal ID="ltrEL2" runat="server"></asp:Literal>
                                    Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[c_code] : </span>
                                <asp:Literal ID="ltrEL2_1" runat="server"></asp:Literal>
                                Code
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[c_name] : </span>Diaplay Name
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[c_address] : </span>Address
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[c_phone] : </span>Phone
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[c_email] : </span>Email
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[c_main_contact] : </span>Main Contact Name
                            </div>
                            <div class="col-md-12 mt10">
                                <b>
                                    <asp:Literal ID="ltrEL1" runat="server"></asp:Literal>
                                    Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[cc_code] : </span>
                                <asp:Literal ID="ltrEL1_1" runat="server"></asp:Literal>
                                Code
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[cc_name] : </span>Diaplay Name
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[cc_phone] : </span>Phone
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[cc_email] : </span>Email
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[cc_main_contact] : </span>Main Contact Name
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Other</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[scd_name] : </span>Schedule Description 1 (if available)
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[scd_name_2] : </span>Schedule Description 2 (if available)
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[today] : </span>Today's Date
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[time] : </span>Now Time (HH:MM)
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[last_day] : </span>Task Deadline
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[app_link] : </span>N-Ter Application Link
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[step_owners_emails] : </span>All E-mail Addresses of the users in the Step User Group (Only for E-mail Templates)
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Task Related Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[task_no] : </span>Task Number
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[task_date] : </span>Task Created Date
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[ef1] : </span>Special Field 1 Data
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[ef2] : </span>Special Field 2 Data
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[tp_ext_link] : </span>Guest Task Posting Link
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[task_link] : </span>Task Link
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[tf:Field Name] : </span>Task Step Fields
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[api:API Name] : </span>API Call Results
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[af:Field Name] : </span>Task Step Addon Fields
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[date:Step Number] : </span>Last Posted Date of the Workflow Step
                            </div>
                            <div class="col-md-12">
                                <span class="text-danger">⟦afr:Addon Name:Section to Repeat⟧ : </span>Task Step Addon - Repeat Section
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Task Fields/Addon Fields Bracket Hierarchy</b>
                            </div>
                            <div class="col-md-12">
                                <span class="text-danger">〈 〉  ⟪ ⟫  [ ]</span>
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Sample Condition</b>
                            </div>
                            <div class="col-md-12">
                                <span class="text-danger">[tf:Field Name{1|First Value|{2|Second Value|{3|Third Value|{4|Fourth Value|Fifth Value}}}}]</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCloseHelp" runat="server" Text="Close" CssClass="btn btn-primary" />
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
            AdjustPopupSize(80, 700, 'at_model_help');
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

            clearTextBox(['<%=txtAPICallName.ClientID%>', '<%=txtContentType.ClientID%>', '<%=txtRequestBody.ClientID%>', '<%=txtURL.ClientID%>']);
            clearCheckListBox(['<%=chkAllowedWorkflowStep.ClientID%>'])
            clearDropDown(['<%=cboAPIMethod.ClientID%>', '<%=cboExecuteTime.ClientID%>', '<%=cboStepField.ClientID%>', '<%=cboOperator.ClientID%>']);
            clearCheckBox(['<%=chkCondition.ClientID%>']);

            CheckConditions('<%=chkCondition.ClientID%>', '<%=cboStepField.ClientID%>', '<%=cboOperator.ClientID%>', '<%=txtCriteria.ClientID%>', 'divCondition', 'cboCondtTemp', 'txtCondtDateTemp', '', reload_codt_drps);
            return false;
        }

        function ResetControls() {
            if ($("#<%=hndAPI_Call_ID.ClientID%>").val().trim() == '0') {
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
                url: "api/tasks/GetWFAPICall",
                data: { API_Call_ID: $("#<%=hndAPI_Call_ID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassed(result) {
            ClearControls(false);

            var resultObject = result;
            $('#<%=txtAPICallName.ClientID%>').val(resultObject[0].API_Call_Name);
            $('#<%=txtURL.ClientID%>').val(resultObject[0].API_URL);
            $('#<%=cboAPIMethod.ClientID%>').val(resultObject[0].API_Method);
            $('#<%=txtContentType.ClientID%>').val(resultObject[0].Contant_Type);
            $('#<%=txtRequestBody.ClientID%>').val(resultObject[0].Request_Body);
            $('#<%=cboExecuteTime.ClientID%>').val(resultObject[0].At_Begining);
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

            var resultObjectSub = resultObject[0].WF_Steps_For_API;
            $('#<%=chkAllowedWorkflowStep.ClientID%> input').each(function () {
                for (var i = 0; i < resultObjectSub.length; i++) {
                    if (resultObjectSub[i].Workflow_Step_ID.trim() == $(this).val().trim()) {
                        onCheckBox(this);
                    }
                }
            });
        }
    </script>
</asp:Content>
