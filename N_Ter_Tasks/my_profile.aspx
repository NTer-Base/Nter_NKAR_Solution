<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin.master" AutoEventWireup="true" CodeBehind="my_profile.aspx.cs" Inherits="N_Ter_Tasks.my_profile" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > My Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    My Profile
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="panel panel-info">
        <div class="form-horizontal">
            <div class="panel-body">
                <div class="form-group">
                    <label class="col-sm-2 control-label">Profile Images</label>
                    <div class="col-sm-4 col-md-3">
                        <div class="profile-image-btn">
                            <button id="cmdUploadImage" runat="server" class="btn btn-default" title="Change Profile Image"><i class="fa fa-camera"></i></button>
                            <asp:ModalPopupExtender ID="cmdImage_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlUpload" TargetControlID="cmdUploadImage" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                            </asp:ModalPopupExtender>
                        </div>
                        <asp:Image ID="imgProfileImage" runat="server" Style="width: 100%; height: auto" />
                    </div>
                    <div class="col-sm-1 mt15"></div>
                    <div class="col-sm-4 col-md-3">
                        <div class="profile-image-btn">
                            <button id="cmdUploadSignature" runat="server" class="btn btn-default" title="Change Profile Signature"><i class="fa fa-camera"></i></button>
                            <asp:ModalPopupExtender ID="cmdUploadSignature_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlUploadSignature" TargetControlID="cmdUploadSignature" CancelControlID="cmdNoS" BackgroundCssClass="at_modelpopup_background">
                            </asp:ModalPopupExtender>
                        </div>
                        <asp:Image ID="imgProfileSignature" runat="server" Style="width: 100%; height: auto" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="pull-right">
                            <button id="cmdEditPass" runat="server" class="btn btn-labeled btn-primary" style="margin-top: 5px"><span class="btn-label icon fa fa-lock"></span>Change Password</button>
                            <asp:ModalPopupExtender ID="cmdEditPass_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlchangePasswrd" TargetControlID="cmdEditPass" CancelControlID="cmdcmcel" BackgroundCssClass="at_modelpopup_background">
                            </asp:ModalPopupExtender>
                            <button id="cmdEditPin" runat="server" class="btn btn-labeled btn-primary " style="margin-top: 5px"><span class="btn-label icon fa fa-lock"></span>Change PIN</button>
                            <asp:ModalPopupExtender ID="cmdEditPin_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlChangePin" TargetControlID="cmdEditPin" CancelControlID="cmdPinOK" BackgroundCssClass="at_modelpopup_background">
                            </asp:ModalPopupExtender>
                            <button id="cmdEditToken" runat="server" class="btn btn-labeled btn-primary " style="margin-top: 5px"><span class="btn-label icon fa fa-lock"></span>Change API Key</button>
                            <asp:ModalPopupExtender ID="cmdEditToken_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlChangeToken" TargetControlID="cmdEditToken" CancelControlID="cmdTokenOK" BackgroundCssClass="at_modelpopup_background">
                            </asp:ModalPopupExtender>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">E-mail</label>
                    <div class="col-sm-10" style="padding-top: 7px">
                        <asp:Label ID="lblUsername" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">User Code</label>
                    <div class="col-sm-10" style="padding-top: 7px">
                        <asp:Label ID="lblCode" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">First Name</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Last Name</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Phone</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Work Assigned To</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="cboTempUser" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Work Assigned From</label>
                    <div class="col-sm-10" style="padding-top: 8px">
                        <asp:Literal ID="ltrAssignedFrom" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label"></label>
                    <div class="col-sm-10 h4">
                        Application Settings
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Default Task List</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="cboDefaultTaskList" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">[Use Global App Setting]</asp:ListItem>
                            <asp:ListItem Value="1">Tabs on Workflow Categories</asp:ListItem>
                            <asp:ListItem Value="2">Tabs on Workflow</asp:ListItem>
                            <asp:ListItem Value="3">Tabs on Workflow Step Status</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Task List View</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="cboTaskListView" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">[Use Global App Setting]</asp:ListItem>
                            <asp:ListItem Value="1">Grid View</asp:ListItem>
                            <asp:ListItem Value="2">Card View</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Task Screen Layout</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="cboTaskScreen" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">[Use Global App Setting]</asp:ListItem>
                            <asp:ListItem Value="1">Show All Panels</asp:ListItem>
                            <asp:ListItem Value="2">Dock Documents and History Panels to Right</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Timeline Task List View</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="cboTLTaskListView" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">[Use Global App Setting]</asp:ListItem>
                            <asp:ListItem Value="1">Grid View</asp:ListItem>
                            <asp:ListItem Value="2">Card View</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Page Help</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="cboPageHelp" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">[Use Global App Setting]</asp:ListItem>
                            <asp:ListItem Value="1">Show Help</asp:ListItem>
                            <asp:ListItem Value="2">Hide Help</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel-footer">
            <asp:Button ID="cmdReset" runat="server" Text="Reset" CssClass="btn btn-default" OnClick="cmdReset_Click" />
            <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="btn btn-primary pull-right" OnClick="cmdSave_Click" />
        </div>
    </div>
    <asp:Panel ID="pnlUpload" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_indent" class="at_modelpopup_indent">
            <div id="at_model_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdNo" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Upload Profile Image</h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>File</label>
                            <asp:FileUpload ID="fulLogo" runat="server" CssClass="form-control st_file_upload" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdYes" runat="server" CssClass="btn btn-primary pull-right" Text="Upload" OnClick="cmdYes_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlUploadSignature" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_sg_indent" class="at_modelpopup_indent">
            <div id="at_model_sg_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdNoS" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Upload Profile Signature</h4>
                </div>
                <div id="at_model_sg_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>File</label>
                            <asp:FileUpload ID="fulSignature" runat="server" CssClass="form-control st_file_upload" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdYesS" runat="server" CssClass="btn btn-primary pull-right" Text="Upload" OnClick="cmdYesS_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlchangePasswrd" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_password_indent" class="at_modelpopup_indent">
            <div id="at_model_password_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdcmcel" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Change Password </h4>
                </div>
                <div id="at_model_password_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Current Password</label>
                            <asp:TextBox ID="txtcurrntpasswrd" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>New Password</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtnewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-success" type="button" onclick="return showPWPolicy()"><i class='fa fa-question'></i></button>
                                </span>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>Retype Password</label>
                            <asp:TextBox ID="txtRePassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="Btnsubmit" runat="server" CssClass="btn btn-primary pull-right" Text="Save" OnClick="Btnsubmit_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlChangePin" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_pin_indent" class="at_modelpopup_indent">
            <div id="at_model_pin_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Change PIN</h4>
                </div>
                <div id="at_model_pin_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group text-center">
                            <label>Your New PIN</label><br />
                            <label class="h4">
                                <asp:Label ID="lblNewPin" runat="server" Text="xxxxxx"></asp:Label></label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdPinOK" runat="server" CssClass="btn btn-primary pull-right" Text="OK" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlChangeToken" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_token_indent" class="at_modelpopup_indent">
            <div id="at_model_token_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Change API Key</h4>
                </div>
                <div id="at_model_token_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group text-center">
                            <label>Your New API Key</label><br />
                            <label class="h4">
                                <asp:Label ID="lblNewToken" runat="server" Text="xxxxxx"></asp:Label></label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdTokenOK" runat="server" CssClass="btn btn-primary pull-right" Text="OK" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndHelp" runat="server" />
    <asp:ModalPopupExtender ID="hndHelp_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlHelp" BehaviorID="mpuHelp" TargetControlID="hndHelp" CancelControlID="cmdCancelHelp" BackgroundCssClass="at_modelpopup_background_2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlHelp" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_help_indent" class="at_modelpopup_indent">
            <div id="at_model_help_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 id="help_title" class="panel-title">Password Policy</h4>
                </div>
                <div id="at_model_help_content" class="at_modelpopup_content styled-bar">
                    <div id="divPasswordPolicy" runat="server" class="panel-body"></div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCancelHelp" runat="server" Text="Close" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script type="text/javascript">
        init.push(function () {
            ArrangePopups();
        });

        $(function () {
            $(window).resize(function () {
                ArrangePopups();
            });
        });

        function ArrangePopups() {
            AdjustPopupSize(80, 600, 'at_model');
            AdjustPopupSize(80, 600, 'at_model_sg');
            AdjustPopupSize(80, 600, 'at_model_password');
            AdjustPopupSize(80, 400, 'at_model_pin');
            AdjustPopupSize(80, 600, 'at_model_token');
            AdjustPopupSize(80, 500, 'at_model_help');
        }

        function LoadPIN() {
            $('#<%=lblNewPin.ClientID%>').text('xxxxx');

            $.ajax({
                type: "GET",
                url: "api/tasks/Change_PIN",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadPINPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadPINPassed(result) {
            $('#<%=lblNewPin.ClientID%>').text(result);
        }

        function LoadToken() {
            $('#<%=lblNewToken.ClientID%>').text('xxxxxxxxxxx');

            $.ajax({
                type: "GET",
                url: "api/tasks/Change_Token",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadTokenPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadTokenPassed(result) {
            $('#<%=lblNewToken.ClientID%>').text(result);
        }

        function showPWPolicy() {
            $find("mpuHelp").show();
            return false;
        }
    </script>
</asp:Content>