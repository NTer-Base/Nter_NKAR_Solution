<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_guest.master" AutoEventWireup="true" CodeBehind="guest_task_post.aspx.cs" Inherits="N_Ter_Tasks.guest_task_post" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter > Guest Task Posting
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
    <link href="assets/stylesheets/widgets.min.css" rel="stylesheet" />
    <link href="assets/stylesheets/pages.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contBody" runat="Server">
    <div id="divNoError" runat="server" class="row no-margin-hr">
        <div id="altHelp" runat="server" class="col-md-12">
            <div class="alert alert-success">
                <asp:Literal ID="ltrGuestHelp" runat="server"></asp:Literal>
            </div>
        </div>
        <div id="divPost" runat="server" class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="panel-title-nter">Guest Task Posting</div>
                    <div class="border-t panel-info" style="height: 10px"></div>
                    <asp:Panel ID="pnlStepData" runat="server">
                    </asp:Panel>
                </div>
                <div id="divSubmit" runat="server" class="modal-footer">
                    <asp:Button ID="cmdSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="cmdSubmit_Click" />
                </div>
            </div>
        </div>
        <div id="divOther" runat="server" class="col-md-5">
            <div id="divDocuments" runat="server" class="panel panel-info">
                <div class="widget-comments panel-body long_word_wrap">
                    <div class="panel-title-nter">Task Documents</div>
                    <div class="border-t panel-info" style="height: 10px"></div>
                    <div id="divNoDocs" runat="server" class="alert alert-info mt10">
                        No Doauments Available
                    </div>
                    <asp:Literal ID="ltrAttachments" runat="server"></asp:Literal>
                </div>
            </div>
            <div id="divHistory" runat="server" class="panel panel-info">
                <div class="panel-body">
                    <div class="panel-title-nter">Task History</div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <div class="timeline">
                        <asp:Literal ID="ltrHistory" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divError" runat="server">
        <div class="col-md-12">
            <div class="alert alert-danger alert-dark no-margin-b">
                <strong>This Action has Expored</strong> Please contact the System's Administrator"
            </div>
        </div>
    </div>
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
<asp:Content ID="Content4" ContentPlaceHolderID="contScripts" runat="Server">
    <script type="text/javascript">
        init.push(function () {
            AdjustPopups();
            $('.history_parent').tooltip();
        });

        $(function () {
            $(window).resize(function () {
                AdjustPopups();
            });
        });

        function AdjustPopups() {
            <%=HelpPanelResizeScript%>
        }

        <%=HelpScript%>

        <%=Required_Fields%>

        <%=Custom_Scripts%>
    </script>
</asp:Content>