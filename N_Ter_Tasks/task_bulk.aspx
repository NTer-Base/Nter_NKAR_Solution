<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin.master" AutoEventWireup="true" CodeBehind="task_bulk.aspx.cs" Inherits="N_Ter_Tasks.task_bulk" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter > Multiple Tasks Posting
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Multiple Tasks Posting
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div id="divStepFormulaError" runat="server" class="alert alert-danger">
        <strong>Warning ! </strong>This step contains <strong>Field Formulas</strong> which cannot be accomplished with Multiple Task Posting.
    </div>
    <div id="divFieldValidationsError" runat="server" class="alert alert-danger">
        <strong>Warning ! </strong>There are <strong>Field Validations</strong> in this step that cannot be performed with Multiple Task Posting.
    </div>
    <div id="divSubWorkflowError" runat="server" class="alert alert-danger">
        <strong>Warning ! </strong>This step contains starting of sub workflows which cannot be accomplished with Multiple Task Posting.
    </div>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="panel-title-nter">Tasks to Post</div>
                    <div class="border-t panel-info" style="height: 20px"></div>

                    <div class="row mt15">
                        <div class="col-sm-12">
                            Following tasks are successfully locked for you and ready to submit.
                            <br />
                            <br />
                        </div>
                        <div class="col-md-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Workflow Name</div>
                                <div class="col-sm-8">
                                    <asp:Literal ID="ltrWorkflowName" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Current Step</div>
                                <div class="col-sm-8">
                                    <asp:Literal ID="ltrCurrentStep" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="row pt3">
                                <div class="col-sm-2 text-bold text-right-md">Progress</div>
                                <div class="col-sm-10 text-info">
                                    <div class="progress mt5 mb">
                                        <div id="divProgress" runat="server" class="progress-bar" style="width: 1%"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12" style="margin-top: 10px">
                            <asp:Literal ID="ltrTaskDetails" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="panel_menu">
                        <button id="cmdCancel" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-trash-o"></span>Discard Tasks</button>
                        <asp:ModalPopupExtender ID="cmdCancel_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlConfirmCancel" TargetControlID="cmdCancel" CancelControlID="cmdCancelTaskCancel" BackgroundCssClass="at_modelpopup_background">
                        </asp:ModalPopupExtender>
                        <button id="cmdRelease" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-unlock"></span>Unclaim Tasks</button>
                        <asp:ModalPopupExtender ID="cmdRelease_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlConfirmRelease" TargetControlID="cmdRelease" CancelControlID="cmdCancelRelease" BackgroundCssClass="at_modelpopup_background">
                        </asp:ModalPopupExtender>
                    </div>
                    <div class="panel-title-nter">Task Posting</div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <asp:Panel ID="pnlStepData" runat="server">
                    </asp:Panel>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdReject" runat="server" Text="Reject" class="btn btn-default pull-left" />
                    <asp:ModalPopupExtender ID="cmdReject_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlRejectConfirm" BehaviorID="mpuReject" TargetControlID="cmdReject" CancelControlID="cmdRejectCancel" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                    <asp:Button ID="cmdSubmit" runat="server" Text="Submit" class="btn btn-primary" OnClick="cmdSubmit_Click" />
                    <div id="divAnyStep" runat="server" class="input-group" style="margin-top: 10px">
                        <asp:DropDownList ID="cboSteps" runat="server" CssClass="form-control"></asp:DropDownList>
                        <span class="input-group-btn">
                            <asp:Button ID="cmdSubmitSpecial" runat="server" Text="Submit - Selected Step" CssClass="btn btn-primary" OnClick="cmdSubmitSpecial_Click" />
                        </span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlRejectConfirm" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_reg_indent" class="at_modelpopup_indent">
            <div id="at_model_reg_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Reject Confirm</h4>
                </div>
                <div id="at_model_reg_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            Are you sure, you want to <b>reject</b> this?
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdRejectConfirm" runat="server" CssClass="btn btn-primary" Text="Yes" OnClick="cmdRejectConfirm_Click" />
                    <asp:Button ID="cmdRejectCancel" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlConfirmRelease" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_rel_indent" class="at_modelpopup_indent">
            <div id="at_model_rel_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Release Confirm</h4>
                </div>
                <div id="at_model_rel_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            Are you sure, you want to <b>release</b> this task?
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdReleaseConfirm" runat="server" CssClass="btn btn-primary" Text="Yes" OnClick="cmdReleaseConfirm_Click" />
                    <asp:Button ID="cmdCancelRelease" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlConfirmCancel" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_cans_indent" class="at_modelpopup_indent">
            <div id="at_model_cans_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Cancel Confirm</h4>
                </div>
                <div id="at_model_cans_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            Are you sure, you want to <b>cancel</b> this task?
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCancelTaskConform" runat="server" CssClass="btn btn-primary" Text="Yes" OnClick="cmdCancelTaskConform_Click" />
                    <asp:Button ID="cmdCancelTaskCancel" runat="server" Text="No" CssClass="btn btn-default" />
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
                    <h4 id="help_title" class="panel-title">Help Instructions</h4>
                </div>
                <div id="at_model_help_content" class="at_modelpopup_content styled-bar">
                    <div id="divFieldHelp" class="panel-body"></div>
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
            AdjustPopups();
        });
        $(function () {
            $(window).resize(function () {
                AdjustPopups();
            });
        });

        function AdjustPopups() {
            AdjustPopupSize(80, 400, 'at_model_reg');
            AdjustPopupSize(80, 400, 'at_model_cans');
            AdjustPopupSize(80, 400, 'at_model_rel');
            <%=HelpPanelResizeScript%>
        }

        <%=HelpScript%>

        <%=Required_Fields%>

        <%=Custom_Scripts%>
    </script>
</asp:Content>