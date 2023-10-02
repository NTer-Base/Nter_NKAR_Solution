<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin.master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="N_Ter_Tasks.search" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Search Results
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Search Results for
    <asp:Literal ID="ltrSearch" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <div class="tab-base">
                <ul class="nav nav-tabs nav-tabs-simple">
                    <li class="active">
                        <a data-toggle="tab" href="#lft_tab_1">Tasks</a>
                    </li>
                    <li>
                        <a data-toggle="tab" href="#lft_tab_2">Document Projects</a>
                    </li>
                </ul>
                <div class="panel panel-info">
                    <div class="panel-body no-padding">
                        <div class="tab-content grid-with-paging">
                            <div id="lft_tab_1" class="tab-pane fade active in">
                                <div class="table-responsive table-primary no-margin-b">
                                    <asp:GridView ID="gvTasksSearch" runat="server" ClientIDMode="Static" CssClass="table table-striped table-hover grid_table" AutoGenerateColumns="False" OnRowDataBound="gvMain_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Task_ID" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <button id='cmdEdit' type='submit' runat="server" class="btn btn-primary btn-xs" title="Edit"><i class="fa fa-eye button_icon"></i></button>
                                                </ItemTemplate>
                                                <ItemStyle Width="22px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Task_Date" HeaderText="Task Date/Time" SortExpression="Task_Date">
                                                <ItemStyle Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Task_Number" HeaderText="Task Number" SortExpression="Task_Number">
                                                <ItemStyle Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Workflow_Name" HeaderText="Workflow Name" SortExpression="Workflow_Name" />
                                            <asp:BoundField DataField="Display_Name" HeaderText="EL2 Name" SortExpression="Display_Name" />
                                            <asp:BoundField DataField="Created_By" HeaderText="Task Owner" SortExpression="Created_By" />
                                            <asp:BoundField DataField="Posted_Date" HeaderText="Posted Date/Time" SortExpression="Posted_Date">
                                                <ItemStyle Width="80px" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div id="lft_tab_2" class="tab-pane fade">
                                <div class="table-responsive table-primary no-margin-b">
                                    <asp:GridView ID="gvDocumentsSearch" ClientIDMode="Static" runat="server" CssClass="table table-striped table-hover grid_table" AutoGenerateColumns="False" OnRowDataBound="gvDocumentsSearch_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Document_ID" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <button id='cmdView' type='submit' runat="server" class="btn btn-primary btn-xs" title="View"><i class="fa fa-eye button_icon"></i></button>
                                                </ItemTemplate>
                                                <ItemStyle Width="22px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <button id='cmdView' type='submit' runat="server" class="btn btn-success btn-xs" title="View"><i class="fa fa-info-circle button_icon"></i></button>
                                                </ItemTemplate>
                                                <ItemStyle Width="22px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Doc_Project_Name" HeaderText="Project Name"></asp:BoundField>
                                            <asp:BoundField DataField="Document_No" HeaderText="Document No"></asp:BoundField>
                                            <asp:BoundField DataField="Created_Date" HeaderText="Date"></asp:BoundField>
                                            <asp:BoundField DataField="Display_Name" HeaderText="Entity Name"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="cmdDocInfo" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="cmdDocInfo_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDocInfo" BehaviorID="mpuDocInfo" TargetControlID="cmdDocInfo" CancelControlID="cmdCancelDocInfo" BackgroundCssClass="at_modelpopup_background_2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlDocInfo" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <asp:HiddenField ID="hndDocument_ID" runat="server" />
        <div id="at_model_docinfo_indent" class="at_modelpopup_indent">
            <div id="at_model_docinfo_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Document Info</h4>
                </div>
                <div id="at_model_docinfo_content" class="at_modelpopup_content styled-bar">
                    <div id="divDocInfo" class="panel-body">
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCancelDocInfo" runat="server" Text="Close" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script type="text/javascript">
        $(function () {
            $(window).resize(function () {
                AdjustPopups();
                AdjustPopups();
            });
        });

        init.push(function () {
            ArrangeGrids();
        });

        function AdjustPopups() {
            AdjustPopupSize(80, 800, 'at_model_docinfo');
        }

        function ArrangeGrids() {
            $('#gvTasksSearch').dataTable({
                "pageLength": 50,
                "order": [[2, 'asc']],
                "responsive": true,
                "autoWidth": true,
                "columnDefs": [
                    { 'orderable': false, targets: 0 },
                    { type: 'de_datetime', targets: 1 },
                    { type: 'de_datetime', targets: 6 }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });

            $('#gvDocumentsSearch').dataTable({
                "pageLength": 50,
                "order": [[2, 'asc']],
                "responsive": true,
                "autoWidth": true,
                "columnDefs": [
                    { 'orderable': false, targets: 0 },
                    { 'orderable': false, targets: 1 },
                    { type: 'de_datetime', targets: 4 }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });

            $(".dataTables_filter").children().children('input').attr('placeholder', 'Search...');
        }

        function showAttachInfo(Doc_ID) {
            $find("mpuDocInfo").show();
            $.ajax({
                type: "GET",
                url: "api/tasks/GetDocInfo",
                data: { Document_ID: Doc_ID },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadDocInfoPassed,
                failure: LoadValuesFailed
            });
            return false;
        }

        function LoadDocInfoPassed(result) {
            $("#divDocInfo").html(result);
        }
    </script>
</asp:Content>