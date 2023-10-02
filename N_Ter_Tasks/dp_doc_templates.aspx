<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid_dp.master" AutoEventWireup="true" CodeBehind="dp_doc_templates.aspx.cs" Inherits="N_Ter_Tasks.dp_doc_templates" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Document Project Templates
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Document Project Templates -
    <asp:Label ID="lblDocProjectName" runat="server" Text="Label"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="row" style="margin-top: -18px">
        <div class="col-lg-12">
            <div class="tab-base">
                <ul class="nav nav-tabs nav-tabs-simple">
                    <asp:Literal ID="ltrTabs" runat="server"></asp:Literal>
                </ul>
                <div class="panel panel-info">
                    <div class="panel-body no-padding">
                        <div class="tab-content grid-with-paging">
                            <asp:Panel ID="lft_tab_1" ClientIDMode="Static" runat="server" CssClass="">
                                <asp:HiddenField ID="hndEmail_ID" runat="server" />
                                <div class="padding-xs-vr">
                                    <asp:HiddenField ID="hndDP_ID" runat="server" />
                                    <button id="cmdNewEmail" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Email Template</button>
                                    <asp:ModalPopupExtender ID="cmdNewEMail_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDataEmail" TargetControlID="cmdNewEmail" CancelControlID="cmdNoEmail" BackgroundCssClass="at_modelpopup_background">
                                    </asp:ModalPopupExtender>
                                </div>
                                <div class="table-responsive table-primary no-margin-b">
                                    <asp:GridView ID="gvEmail" runat="server" CssClass="table table-striped table-hover grid_table grid_wf_categories" AutoGenerateColumns="False" OnRowDataBound="gvEmail_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Email_ID" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <button id='cmdEditEmail' type='submit' runat="server" class="btn btn-primary btn-xs" title="Edit"><i class="fa fa-edit button_icon"></i></button>
                                                    <asp:ModalPopupExtender ID="cmdEditEmail_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDataEmail" TargetControlID="cmdEditEmail" CancelControlID="cmdNoEmail" BackgroundCssClass="at_modelpopup_background">
                                                    </asp:ModalPopupExtender>
                                                </ItemTemplate>
                                                <ItemStyle Width="22px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <button id='cmdDeleteEmail' type='submit' runat="server" class="btn btn-danger btn-xs" title="Delete"><i class="fa fa-trash-o button_icon"></i></button>
                                                    <asp:ModalPopupExtender ID="cmdDeleteEmail_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDeleteEmail" TargetControlID="cmdDeleteEmail" CancelControlID="cmdCancelEmail" BackgroundCssClass="at_modelpopup_background">
                                                    </asp:ModalPopupExtender>
                                                </ItemTemplate>
                                                <ItemStyle Width="22px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Email_Name" HeaderText="Email Name" />
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
    <asp:Panel ID="pnlDataEmail" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_email_indent" class="at_modelpopup_indent">
            <div id="at_model_email_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <button id='cmdHelp1' type='submit' runat="server" class="btn btn-success" title="Help"><i class="fa fa-question"></i></button>
                        <asp:ModalPopupExtender ID="cmdHelp1_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlHelp" TargetControlID="cmdHelp1" CancelControlID="cmdCloseHelp" BackgroundCssClass="at_modelpopup_background_2">
                        </asp:ModalPopupExtender>
                        <asp:Button ID="cmdNoEmail" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add/Edit E-Mail Template</h4>
                </div>
                <div id="at_model_email_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>E-Mail Template Name</label>
                            <asp:TextBox ID="txtEmailName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>From Name (Leave blank to use System Defaults)</label>
                            <asp:TextBox ID="txtFromName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>From E-Mail Address (Leave blank to use System Defaults)</label>
                            <asp:TextBox ID="txtFromAddress" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>To E-Mail Address</label>
                            <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>CC Addresses</label>
                            <asp:TextBox ID="txtCCAddresses" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>E-Mail Title</label>
                            <asp:TextBox ID="txtEmailTitle" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>E-Mail Body</label>
                            <asp:TextBox ID="txtEmailBody" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="10"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSaveEmail" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSaveEmail_Click" />
                    <asp:Button ID="cmdResetEmail" runat="server" Text="Reset" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDeleteEmail" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_model_email_indent" class="at_modelpopup_indent">
            <div id="at_del_model_email_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete</h4>
                </div>
                <div id="at_del_model_email_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDeleteEmail" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDeleteEmail_Click" />
                    <asp:Button ID="cmdCancelEmail" runat="server" Text="No" CssClass="btn btn-default" />
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
                                <b>Loggedn in User Details (Guest User)</b>
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
                            <div class="col-md-12 mt10">
                                <b>
                                    <asp:Literal ID="ltrEL2" runat="server"></asp:Literal>
                                    Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[c_code] : </span><asp:Literal ID="ltrEL2_1" runat="server"></asp:Literal> Code
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
                                <span class="text-danger">[today] : </span>Today's Date
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[time] : </span>Now Time (HH:MM)
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[app_link] : </span>N-Ter Application Link
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Task Related Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[doc_no] : </span>Document Number
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[doc_date] : </span>Document Uploaded Date
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[tg:Tag Name] : </span>Document Tags
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Document Tag Bracket Hierarchy</b>
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
            AdjustPopupSize(167, 800, 'at_model_email');
            AdjustPopupSize(80, 400, 'at_del_model_email');
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

        function ClearControlsEmail() {
            clearTextBox(['<%=txtEmailName.ClientID%>', '<%=txtFromName.ClientID%>', '<%=txtFromAddress.ClientID%>', '<%=txtEmailBody.ClientID%>', '<%=txtEmailTitle.ClientID%>', '<%=txtEmailAddress.ClientID%>', '<%=txtCCAddresses.ClientID%>']);
            return false;
        }

        function ResetControlsEmail() {
            if ($("#<%=hndEmail_ID.ClientID%>").val().trim() == '0') {
                ClearControlsEmail();
            }
            else {
                LoadValuesEmail();
            }
            return false;
        }

        function LoadValuesEmail() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetDPEmailTemplate",
                data: { Email_ID: $("#<%=hndEmail_ID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesEmailPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesEmailPassed(result) {
            ClearControlsEmail();

            var resultObject = result;
            $('#<%=txtEmailName.ClientID%>').val(resultObject[0].Email_Name);
            $('#<%=txtFromName.ClientID%>').val(resultObject[0].From_Name);
            $('#<%=txtFromAddress.ClientID%>').val(resultObject[0].From_Address);
            $('#<%=txtEmailAddress.ClientID%>').val(resultObject[0].Email_Address);
            $('#<%=txtCCAddresses.ClientID%>').val(resultObject[0].CC_Addresses);
            $('#<%=txtEmailBody.ClientID%>').val(resultObject[0].Email_Body);
            $('#<%=txtEmailTitle.ClientID%>').val(resultObject[0].Email_Title);
        }
    </script>
</asp:Content>