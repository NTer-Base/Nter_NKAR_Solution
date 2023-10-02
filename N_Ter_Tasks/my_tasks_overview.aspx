<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid.master" AutoEventWireup="true" CodeBehind="my_tasks_overview.aspx.cs" Inherits="N_Ter_Tasks.my_tasks_overview" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > My Task Overview
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    My Task Overview
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <div class="tab-base">
                <ul class="nav nav-tabs nav-tabs-simple">
                    <asp:Literal ID="ltrTabs" runat="server"></asp:Literal>
                </ul>
                <div class="panel panel-info">
                    <div class="panel-body no-padding">
                        <div class="tab-content grid-with-paging">
                            <asp:Panel ID="lft_tab_1" ClientIDMode="Static" runat="server" CssClass="">
                                <div class="table-responsive table-primary no-margin-b">
                                    <table id="tblOnProgress" class="table<%=GridClass%> grid_test full_width_table" data-size="full_width_table">
                                        <thead>
                                            <tr>
                                                <th></th><%=TaskNumber%>
                                                <th>Created On</th>
                                                <th>Workflow Name</th>
                                                <th>
                                                    <asp:Literal ID="ltrEntityL2Name" runat="server"></asp:Literal></th>
                                                <th></th>
                                                <th></th>
                                                <th>Current Step</th>
                                                <th>Task Owner</th>
                                                <th>Due On</th>
                                                <th>Posted On</th>
                                                <th>Posted By</th>
                                                <th>
                                                    <asp:Literal ID="ltrEntityL1Name" runat="server"></asp:Literal></th>
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="lft_tab_2" ClientIDMode="Static" runat="server" CssClass="">
                                <div class="table-responsive table-primary no-margin-b">
                                    <table id="tblInactive" class="table<%=GridClass%> grid_test full_width_table" data-size="full_width_table">
                                        <thead>
                                            <tr>
                                                <th></th><%=TaskNumber%>
                                                <th>Created On</th>
                                                <th>Workflow Name</th>
                                                <th>
                                                    <asp:Literal ID="ltrEntityL2Name2" runat="server"></asp:Literal></th>
                                                <th></th>
                                                <th></th>
                                                <th>Current Step</th>
                                                <th>Task Owner</th>
                                                <th>Due On</th>
                                                <th>Posted On</th>
                                                <th>Posted By</th>
                                                <th>
                                                    <asp:Literal ID="ltrEntityL1Name2" runat="server"></asp:Literal></th>
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="lft_tab_3" ClientIDMode="Static" runat="server" CssClass="">
                                <div class="table-responsive table-primary no-margin-b">
                                    <table id="tblUnreachable" class="table<%=GridClass%> grid_test full_width_table" data-size="full_width_table">
                                        <thead>
                                            <tr>
                                                <th></th><%=TaskNumber%>
                                                <th>Created On</th>
                                                <th>Workflow Name</th>
                                                <th>
                                                    <asp:Literal ID="ltrEntityL2Name3" runat="server"></asp:Literal></th>
                                                <th></th>
                                                <th></th>
                                                <th>Current Step</th>
                                                <th>Task Owner</th>
                                                <th>Due On</th>
                                                <th>Posted On</th>
                                                <th>Posted By</th>
                                                <th>
                                                    <asp:Literal ID="ltrEntityL1Name3" runat="server"></asp:Literal></th>
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="lft_tab_4" ClientIDMode="Static" runat="server" CssClass="">
                                <div class="row" style="margin-left: -22px !important; margin-right: -22px !important">
                                    <asp:HiddenField ID="hndFrom" runat="server" />
                                    <asp:HiddenField ID="hndTo" runat="server" />
                                    <div class="col-sm-6">
                                        <div class="row pb5">
                                            <label class="col-sm-3 control-label text-bold" for="demo-hor-inputemail">From Date</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control dtPicker"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="row pb5">
                                            <label class="col-sm-3 control-label text-bold" for="demo-hor-inputemail">To Date</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control dtPicker"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="row pb5">
                                            <div class="col-sm-12 text-right">
                                                <asp:Button ID="cmdShowCompleted" runat="server" Text="Refresh" CssClass="btn btn-primary" OnClick="cmdShowCompleted_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="table-responsive table-primary no-margin-b">
                                    <table id="tblClosed" class="table<%=GridClass%> grid_test full_width_table" data-size="full_width_table">
                                        <thead>
                                            <tr>
                                                <th></th><%=TaskNumber%>
                                                <th>Created On</th>
                                                <th>Workflow Name</th>
                                                <th>
                                                    <asp:Literal ID="ltrEntityL2Name4" runat="server"></asp:Literal></th>
                                                <th></th>
                                                <th></th>
                                                <th>Task Owner</th>
                                                <th>Due On</th>
                                                <th>Posted On</th>
                                                <th>Posted By</th>
                                                <th>
                                                    <asp:Literal ID="ltrEntityL1Name4" runat="server"></asp:Literal></th>
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="lft_tab_5" ClientIDMode="Static" runat="server" CssClass="">
                                <div class="table-responsive table-primary no-margin-b">
                                    <table id="tblLocked" class="table<%=GridClass%> grid_test full_width_table" data-size="full_width_table">
                                        <thead>
                                            <tr>
                                                <th></th><%=TaskNumber%>
                                                <th>Created On</th>
                                                <th>Workflow Name</th>
                                                <th>
                                                    <asp:Literal ID="ltrEntityL2Name5" runat="server"></asp:Literal></th>
                                                <th></th>
                                                <th></th>
                                                <th>Current Step</th>
                                                <th>Task Owner</th>
                                                <th>Due On</th>
                                                <th>Posted On</th>
                                                <th>Locked By</th>
                                                <th>
                                                    <asp:Literal ID="ltrEntityL1Name5" runat="server"></asp:Literal></th>
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hndID" runat="server" />
    <asp:ModalPopupExtender ID="hndID_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuUnlock" PopupControlID="pnlUnlock" TargetControlID="hndID" CancelControlID="cmdCancel" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlUnlock" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_indent" class="at_modelpopup_indent">
            <div id="at_model_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Unlock Confirmation</h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            Please <b>Confirm</b> to <b>Unlock</b> Task <b>
                                <asp:Label ID="lblTaskNumber" runat="server" Text=""></asp:Label></b>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdConfirm" runat="server" CssClass="btn btn-primary" Text="Confirm" OnClick="cmdConfirm_Click" />
                    <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="btn btn-default" />
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
            setInterval(function () {
                LoadCounts();
            }, 10000);
        });

        function ArrangePopups() {
            AdjustPopupSize(80, 400, 'at_model');
        }

        function LoadCounts() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetInvTaskCounts",
                data: { FromDate: $('#<%=hndFrom.ClientID%>').val(), ToDate: $('#<%=hndTo.ClientID%>').val() },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var response = result;
                    if (JSON.stringify(response) != "") {
                        var resultObject = result;
                        $('#opCount').html(resultObject[0].Item_Name);
                        $('#intvCount').html(resultObject[1].Item_Name);
                        $('#unrchCount').html(resultObject[2].Item_Name);
                        $('#cpltCount').html(resultObject[3].Item_Name);
                        $('#lcdCount').html(resultObject[4].Item_Name);
                    }
                },
                failure: function (data) { }
            });
        }

        function ArrangeGrids() {
            $.fn.dataTable.ext.errMode = 'none';

            <%=TableScript%>

            $('.nav-tabs-simple').tabdrop();

            $(".dataTables_filter").children().children('input').attr('placeholder', 'Search...');
        }

        function showUnlock(recId) {
            $("#<%=hndID.ClientID%>").val(recId);
            $find("mpuUnlock").show();
            return false;
        }
    </script>
</asp:Content>