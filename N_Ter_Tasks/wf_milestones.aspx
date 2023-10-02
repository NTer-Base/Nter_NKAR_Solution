<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid_wf.master" AutoEventWireup="true" CodeBehind="wf_milestones.aspx.cs" Inherits="N_Ter_Tasks.wf_milestones" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Workflow Milestones
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
    <link href="assets/stylesheets/pages.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Workflow Milestones -
    <asp:Literal ID="ltrWorkflowName" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div style="margin: -18px -20px;" class="border-t">
        <div class="mail-nav">
            <div class="compose-btn text-right">
                <div class="pull-left" style="margin-top: 4px">
                    Milestones
                </div>
                <button id="cmdNewMS" runat="server" class="btn btn-labeled btn-primary" onserverclick="cmdNewMS_ServerClick"><i class="fa fa-plus"></i></button>
            </div>
            <asp:HiddenField ID="hndWFMSID" runat="server" />
            <asp:Button ID="cmdLoadMS" runat="server" Text="Button" Style="display: none" OnClick="cmdLoadMS_Click" />
            <div class="navigation">
                <ul id="ul_milestones" class="sections">
                    <li class="mail-select-folder active"><a href="#">Select a Milestone...</a></li>
                    <asp:Literal ID="ltrMSs" runat="server"></asp:Literal>
                </ul>
            </div>
        </div>
        <asp:Panel ID="pnlMS" runat="server" CssClass="mail-container">
            <div class="mail-container-header show">
                <asp:Literal ID="ltrMSMode" runat="server"></asp:Literal>
                <div class="pull-right">
                    <button id="cmdDeleteMS" runat="server" class="btn btn-labeled btn-danger"><span class="btn-label icon fa fa-times"></span>Delete Step</button>
                    <asp:ModalPopupExtender ID="cmdDeleteMS_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlMSDelete" TargetControlID="cmdDeleteMS" CancelControlID="cmdMSDeleteNo" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                </div>
            </div>
            <div class="new-mail-form form-horizontal">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-sm-3 control-label pt14">Milestone Name</label>
                        <div class="col-sm-9" style="padding-top: 7px">
                            <asp:TextBox ID="txtMilestoneName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label pt14">Milestone Weight</label>
                        <div class="col-sm-9" style="padding-top: 7px">
                            <asp:TextBox ID="txtMilestoneWeight" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label pt14">Sort Order</label>
                        <div class="col-sm-9" style="padding-top: 7px">
                            <asp:DropDownList ID="cboSortOrder" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="mail-container-header show text-right border-t">
                <asp:Button ID="cmdSaveMS" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSaveMS_Click" />
                <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmdCancel_Click" />
            </div>
        </asp:Panel>
    </div>
    <asp:Panel ID="pnlMSDelete" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_ms_indent" class="at_modelpopup_indent">
            <div id="at_del_ms_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete - Milestone</h4>
                </div>
                <div id="at_del_ms_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdMSDeleteOK" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdMSDeleteOK_Click" />
                    <asp:Button ID="cmdMSDeleteNo" runat="server" Text="No" CssClass="btn btn-default" />
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
            ArrangeNavigation();
            ArrangeMilestoneSort();

        });

        function ArrangePopups() {
            AdjustPopupSize(80, 400, 'at_del_step');
        }

        function ArrangeNavigation() {
            $('.mail-nav .navigation li.active a').click(function () {
                $('.mail-nav .navigation').toggleClass('open');
                return false;
            });
        }

        function ArrangeMilestoneSort() {
            $("#ul_milestones").sortable({
                axis: "y",
                handle: "i",
                stop: function (event, ui) {
                    ui.item.children("i").triggerHandler("focusout");
                    var intIndex = 1;
                    var strIDs = "";
                    $(".ms_no").each(function () {
                        $(this).html('Milestone ' + intIndex);
                        strIDs = strIDs + $(this).attr('data-id') + '|';
                        intIndex++;
                    });

                    $.ajax({
                        type: "GET",
                        url: "api/tasks/UpdateWFMilestoneSort",
                        data: { Workflow_ID: '<%=Convert.ToString(ViewState["fid"])%>', Sort_List: strIDs },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: UpdateMSSortPassed,
                        failure: LoadValuesFailed
                    });
                }
            });
        }

        function UpdateMSSortPassed(result) { }

        function LoadMS() {
            document.getElementById('<%=cmdLoadMS.ClientID%>').click();
        }
    </script>
</asp:Content>