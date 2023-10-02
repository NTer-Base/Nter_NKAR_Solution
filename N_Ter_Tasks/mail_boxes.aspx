<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid.master" AutoEventWireup="true" CodeBehind="mail_boxes.aspx.cs" Inherits="N_Ter_Tasks.mail_boxes" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Incoming Mailboxes
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Incoming Mailboxes
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <asp:HiddenField ID="hndMailBoxID" runat="server" />
    <div class="panel panel-info">
        <div class="panel-body">
            <div class="padding-xs-vr">
                <button id="cmdNew" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Mail Box</button>
                <asp:ModalPopupExtender ID="cmdNew_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlData" TargetControlID="cmdNew" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                </asp:ModalPopupExtender>
                <button id="cmdReceiveEMails" runat="server" class="btn btn-labeled btn-warning pull-right" onserverclick="cmdReceiveEMails_ServerClick"><span class="btn-label icon fa fa-envelope-o"></span>Force Receive E-mails</button>
            </div>
            <div class="table-responsive table-primary no-margin-b">
                <asp:GridView ID="gvMain" runat="server" CssClass="table table-striped table-hover grid_table grid_wf_categories non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvMain_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Mail_Box_ID" />
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
                        <asp:BoundField DataField="Mail_Box_Name" HeaderText="Mailbox Name" />
                        <asp:BoundField DataField="Mail_Box_Username" HeaderText="Email Address" />
                        <asp:BoundField DataField="Mail_Box_Type_SP" HeaderText="Mailbox Type" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <button id='cmdTest' type='submit' runat="server" class="btn btn-success btn-xs" title="Test Emmail Address"><i class="fa fa-envelope-o"></i></button>
                            </ItemTemplate>
                            <ItemStyle Width="22px" />
                        </asp:TemplateField>
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
                        <asp:Button ID="cmdNo" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add/Edit Mailbox</h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Mailbox Type</label>
                            <asp:DropDownList ID="cboMailBoxType" runat="server" CssClass="form-control">
                                <asp:ListItem Value="1">POP3</asp:ListItem>
                                <asp:ListItem Value="2">IMAP</asp:ListItem>
                                <asp:ListItem Value="3">Exchange</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Mailbox Name</label>
                            <asp:TextBox ID="txtMailboxName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>E-Mail Address</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Incoming Mail Server</label>
                            <asp:TextBox ID="txtMailServer" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div id="divPort" class="form-group">
                            <label>Incomiing Mail Server Port</label>
                            <asp:TextBox ID="txtPort" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                        <div id="divSSL" class="form-group">
                            <asp:CheckBox ID="chkSSL" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">SSL Enabled</span>
                        </div>
                        <div class="alert alert-danger">
                            <div class="h4">Unidentified E-mails</div>
                            <div class="form-group">
                                <label>Send an E-mails to</label>
                                <asp:TextBox ID="txtSendTo" runat="server" CssClass="form-control"></asp:TextBox>
                                <label>(Leave Blank if not applicable)</label>
                            </div>
                            <hr style="border-color: #e4b9c0" />
                            <div class="form-group">
                                <label>Create a Task for</label>
                                <asp:DropDownList ID="cboEl2" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div id="divWF" class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Use the Workflow</label>
                                        <asp:DropDownList ID="cboWorkflows" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>No of Days to Complete</label>
                                        <asp:TextBox ID="txtNoOfDays" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
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
                    { 'orderable': false, targets: 1 },
                    { 'orderable': false, targets: 5 }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });

            $(".dataTables_filter").children().children('input').attr('placeholder', 'Search...');
        }

        function ClearControls() {
            clearDropDown(['<%=cboMailBoxType.ClientID%>', '<%=cboEl2.ClientID%>', '<%=cboWorkflows.ClientID%>']);
            clearTextBox(['<%=txtMailboxName.ClientID%>', '<%=txtEmail.ClientID%>', '<%=txtMailServer.ClientID%>', '<%=txtPassword.ClientID%>', '<%=txtPort.ClientID%>', '<%=txtSendTo.ClientID%>', '<%=txtNoOfDays.ClientID%>']);
            clearCheckBox(['<%=chkSSL.ClientID%>']);
            CheckExchange();
            CheckEL2();
            return false;
        }

        function ResetControls() {
            if ($("#<%=hndMailBoxID.ClientID%>").val().trim() == '0') {
                ClearControls();
            }
            else {
                LoadValues();
            }
            return false;
        }

        function LoadValues() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetMailbox",
                data: { Mail_Box_ID: $("#<%=hndMailBoxID.ClientID%>").val() },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassed(result) {
            ClearControls();

            var resultObject = result;
            $('#<%=cboMailBoxType.ClientID%>').val(resultObject[0].Mail_Box_Type);
            $('#<%=txtMailboxName.ClientID%>').val(resultObject[0].Mail_Box_Name);
            $('#<%=txtMailServer.ClientID%>').val(resultObject[0].Mail_Box_Server);
            $('#<%=txtEmail.ClientID%>').val(resultObject[0].Mail_Box_Username);
            $('#<%=txtPort.ClientID%>').val(resultObject[0].Mail_Box_Port);
            $('#<%=txtSendTo.ClientID%>').val(resultObject[0].Send_If_Not_Identified);
            $('#<%=cboEl2.ClientID%>').val(resultObject[0].EL2_If_Not_Identified);
            $('#<%=cboWorkflows.ClientID%>').val(resultObject[0].WF_If_Not_Identified);
            $('#<%=txtNoOfDays.ClientID%>').val(resultObject[0].Days_If_Not_Identified);
            if (resultObject[0].Mail_Box_IsSSL == '1') {
                onCheckBox('#<%=chkSSL.ClientID%>');
            }
            CheckExchange();
            CheckEL2();
        }

        function CheckEL2() {
            if ($('#<%=cboEl2.ClientID%>').val() == '0') {
                $('#divWF').addClass('hide');
                $('#<%=cboWorkflows.ClientID%>').val('0');
                $('#<%=txtNoOfDays.ClientID%>').val('0');
            }
            else {
                $('#divWF').removeClass('hide');
            }
        }

        function TestEmail(mbid) {
            onPleaseWait();
            $.ajax({
                type: "GET",
                url: "api/tasks/CheckMailBox",
                data: { Mail_Box_ID: mbid },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: CheckMailBoxPassed,
                failure: CheckMailBoxFailed
            });
            return false;
        }

        function CheckMailBoxPassed(result) {
            offPleaseWait();
            if (result == "Done") {
                ShowSuccess('E-mail Address Validation Successfull');
            }
            else {
                ShowError('E-mail Address Validation Failed');
            }
        }

        function CheckMailBoxFailed(result) {
            offPleaseWait();
            ShowError('E-mail Address Validation Failed');
        }

        function CheckExchange() {
            if ($('#<%=cboMailBoxType.ClientID%>').val() == '3') {
                $('#divPort').addClass('hide');
                $('#divSSL').addClass('hide');
                offCheckBox('#<%=chkSSL.ClientID%>');
                $('#<%=txtPort.ClientID%>').val('0');
            }
            else {
                $('#divPort').removeClass('hide');
                $('#divSSL').removeClass('hide');
            }
        }
    </script>
</asp:Content>