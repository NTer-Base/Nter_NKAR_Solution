<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid.master" AutoEventWireup="true" CodeBehind="document.aspx.cs" Inherits="N_Ter_Tasks.document" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Document
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
    <link href="assets/stylesheets/pages.min.css" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Document :
    <asp:Label ID="lblDocNumber" runat="server" Text="Label"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="panel_menu">
                        <button id="cmdViewDoc" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-eye"></span>View Document</button>
                        <div id="divMainEdit" runat="server" class="btn-group">
                            <button class="btn btn-labeled btn-primary dropdown-toggle" data-toggle="dropdown"><span class="btn-label icon fa fa-cogs"></span><i class="fa fa-caret-down"></i></button>
                            <ul class="dropdown-menu pull-right">
                                <li id="lstChgAccess" runat="server"><a href="#"><span class="btn-label fa fa-user menu_icon"></span>Change Access Level</a></li>
                                <li id="lstChgDoc" runat="server"><a href="#" onclick="ClearChangeDocControls();"><span class="btn-label fa fa-file-o menu_icon"></span>Change Document</a></li>
                            </ul>
                        </div>
                        <asp:ModalPopupExtender ID="cmdChangeAccess_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlUpdateAccessLevel" TargetControlID="lstChgAccess" CancelControlID="cmdCloseUpdateAccessLevel" BackgroundCssClass="at_modelpopup_background">
                        </asp:ModalPopupExtender>
                        <asp:ModalPopupExtender ID="cmdChangeDocument_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlUpdateDoc" TargetControlID="lstChgDoc" CancelControlID="cmdCloseUpdateDoc" BackgroundCssClass="at_modelpopup_background">
                        </asp:ModalPopupExtender>
                    </div>
                    <div class="panel-title-nter">Document Details</div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Document Number</div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblDocNumb" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">
                                    <asp:Literal ID="ltrEL2" runat="server"></asp:Literal>
                                    Name
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblEL2Name" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Document Project</div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblDocProject" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Access Level</div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblAccessLevel" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">File Name</div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblDocPath" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-7">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="panel-title-nter">Document Tags</div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <asp:Panel ID="pnlDocumentTagData" runat="server">
                    </asp:Panel>
                </div>
                <div id="divSave" runat="server" class="modal-footer">
                    <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSave_Click" />
                    <asp:Button ID="cmdReset" runat="server" Text="Reset" CssClass="btn btn-default" OnClick="cmdReset_Click" />
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="panel-title-nter">Linked Tasks</div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <div class="table-responsive table-primary no-margin-b">
                        <table id="tblLinkedTasks" class="table table-striped table-hover grid_table grid_test non_full_width_table" data-size="non_full_width_table">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>Task No</th>
                                    <th>Workflow</th>
                                    <th>Current Status</th>
                                    <th>
                                        <asp:Literal ID="ltrEL2_2" runat="server"></asp:Literal></th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-5">
            <div class="panel panel-info">
                <div class="widget-comments panel-body">
                    <div class="panel-title-nter">QR/Barcode</div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <div id="printarea" class="no-padding-vr">
                        <div class="row">
                            <div class="col-md-5 text-center">
                                <asp:Image ID="imgQR_Code" runat="server" ClientIDMode="Static" />
                                <div>
                                    <button class="btn btn-sm btn-primary" onclick="return printPageArea('Print QR', 400, 600, 'imgQR_Code');" title="Open in New Window"><i class="fa fa-external-link"></i></button>
                                </div>
                            </div>
                            <div class="col-md-7 text-center">
                                <asp:Image ID="imgBarcode" runat="server" ClientIDMode="Static" />
                                <div>
                                    <button class="btn btn-sm btn-primary" onclick="return printPageArea('Print Barcode', 400, 600, 'imgBarcode');" title="Open in New Window"><i class="fa fa-external-link"></i></button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="widget-comments panel-body">
                    <div class="panel_menu">
                        <button id="cmdDocLink" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-link"></span>New Link</button>
                        <asp:ModalPopupExtender ID="cmdDocLink_ModalPopupExtender1" runat="server" Enabled="True" PopupControlID="pnlAddDocLink" TargetControlID="cmdDocLink" CancelControlID="cmdCloseAddLink" BackgroundCssClass="at_modelpopup_background">
                        </asp:ModalPopupExtender>
                    </div>
                    <div class="panel-title-nter">Linked Documents</div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <div class="no-padding-vr">
                        <asp:Literal ID="ltrAttachments" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="panel_menu">
                        <button id="cmdAddComment" runat="server" class="btn btn-labeled btn-primary" onclick="ClearCommentControls();"><span class="btn-label icon fa fa-comments"></span>New Comment</button>
                        <asp:ModalPopupExtender ID="cmdAddComment_ModalPopupExtender1" runat="server" Enabled="True" PopupControlID="pnlAddComment" TargetControlID="cmdAddComment" CancelControlID="cmdCloseAddComment" BackgroundCssClass="at_modelpopup_background">
                        </asp:ModalPopupExtender>
                    </div>
                    <div class="panel-title-nter">Comments</div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <div class="timeline">
                        <asp:Literal ID="ltrHistory" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlUpdateDoc" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_update_doc_indent" class="at_modelpopup_indent">
            <div id="at_model_update_doc_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseUpdateDoc" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Update Document</h4>
                </div>
                <div id="at_model_update_doc_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>File</label>
                            <asp:FileUpload ID="fulDocument" runat="server" CssClass="form-control st_file_upload" />
                        </div>
                        <div class="form-group">
                            <label>File Name</label>
                            <asp:TextBox ID="txtFileName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdUpdateDoc" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdUpdateDoc_Click" />
                    <asp:Button ID="cmdResetUpdateDoc" runat="server" Text="Reset" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlUpdateAccessLevel" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_update_acc_lev_indent" class="at_modelpopup_indent">
            <div id="at_model_update_acc_lev_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseUpdateAccessLevel" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Update Access Level</h4>
                </div>
                <div id="at_model_update_acc_lev_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Access Level</label>
                            <asp:DropDownList ID="cboAccessLevel" runat="server" CssClass="form-control">
                                <asp:ListItem Value="3">Level 3</asp:ListItem>
                                <asp:ListItem Value="2">Level 2</asp:ListItem>
                                <asp:ListItem Value="1">Level 1</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdUpdateAccessLevel" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdUpdateAccessLevel_Click" />
                    <asp:Button ID="cmdCelcelUpdateAccessLevel" runat="server" Text="Reset" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndLinkDocID" runat="server" />
    <asp:Button ID="cmdSaveAddLinkhnd" runat="server" Text="Button" Style="display: none" OnClick="cmdSaveAddLinkhnd_Click" />
    <asp:Panel ID="pnlAddDocLink" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_add_doc_link_indent" class="at_modelpopup_indent">
            <div id="at_model_add_doc_link_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseAddLink" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add Linked Document</h4>
                </div>
                <div id="at_model_add_doc_link_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body grid-with-paging">
                        <div class="table-responsive table-primary no-margin-b">
                            <table id="tblLinked_Docs" class="table table-striped table-hover grid_table grid_test full_width_table" data-size="full_width_table">
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th>Document No</th>
                                        <th>Date</th>
                                        <th>
                                            <asp:Literal ID="ltrEL2_3" runat="server"></asp:Literal></th>
                                        <%=Tag_Names%>
                                        <th></th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlAddComment" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_add_comment_indent" class="at_modelpopup_indent">
            <div id="at_model_add_comment_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseAddComment" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add Comment</h4>
                </div>
                <div id="at_model_add_comment_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Comment</label>
                            <asp:TextBox ID="txtCommentMain" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSaveComment" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSaveComment_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndDeleteLink" runat="server" />
    <asp:ModalPopupExtender ID="hndDeleteLink_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuDeleteLink" PopupControlID="pnlDeleteDocLink" TargetControlID="hndDeleteLink" CancelControlID="cmdCancelDocLink" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlDeleteDocLink" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_model_doc_link_indent" class="at_modelpopup_indent">
            <div id="at_del_model_doc_link_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Remove Link</h4>
                </div>
                <div id="at_del_model_doc_link_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDeleteDocLink" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDeleteDocLink_Click" />
                    <asp:Button ID="cmdCancelDocLink" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField runat="server" ID="hndDeleteComment" ClientIDMode="Static" Value="" />
    <asp:ModalPopupExtender ID="hndDeleteComment_ModalPopupExtender" runat="server" Enabled="true" PopupControlID="pnlDeleteComment" BehaviorID="mpuDeleteComment" TargetControlID="hndDeleteComment" BackgroundCssClass="at_modelpopup_background_2" CancelControlID="cmdDeleteCommentNo">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlDeleteComment" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_del_com_indent" class="at_modelpopup_indent">
            <div id="at_model_del_com_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete - Task Comment</h4>
                </div>
                <div id="at_model_del_com_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you Sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDeleteComment" runat="server" CssClass="btn btn-primary" Text="Yes" OnClick="cmdDeleteComment_Click" />
                    <asp:Button ID="cmdDeleteCommentNo" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndDocHelp" runat="server" />
    <asp:ModalPopupExtender ID="hndDocHelp_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDocHelp" BehaviorID="mpuDocHelp" TargetControlID="hndDocHelp" CancelControlID="cmdCancelDocHelp" BackgroundCssClass="at_modelpopup_background_2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlDocHelp" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_doc_help_indent" class="at_modelpopup_indent">
            <div id="at_model_doc_help_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 id="doc_help_title" class="panel-title">Help Instructions</h4>
                </div>
                <div id="at_model_doc_help_content" class="at_modelpopup_content styled-bar">
                    <div id="divDocFieldHelp" class="panel-body"></div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCancelDocHelp" runat="server" Text="Close" CssClass="btn btn-primary" />
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

            $('.ttip').tooltip();
        });

        function ArrangePopups() {
            AdjustPopupSize(80, 800, 'at_model_update_doc');
            AdjustPopupSize(80, 600, 'at_model_update_acc_lev');
            AdjustPopupSize(80, 1200, 'at_model_add_doc_link');
            AdjustPopupSize(80, 1200, 'at_model_add_comment');
            AdjustPopupSize(80, 400, 'at_del_model_doc_link');
            AdjustPopupSize(80, 400, 'at_model_del_com');
            <%=DocHelpPanelResizeScript%>
        }

        function ClearChangeDocControls() {
            clearFleUpload(['<%=fulDocument.ClientID%>']);
            clearTextBox(['<%=txtFileName.ClientID%>']);
            return false;
        }

        function ClearCommentControls() {
            clearTextBox(['<%=txtCommentMain.ClientID%>']);
            return false;
        }

        function saveLink(doc_id) {
            $('#<%=hndLinkDocID.ClientID%>').val(doc_id);
            $('#<%=cmdSaveAddLinkhnd.ClientID%>').click();
        }

        function ArrangeGrids() {
            $('#tblLinkedTasks').dataTable({
                pageLength: 50,
                order: [[1, 'asc']],
                responsive: true,
                autoWidth: true,
                processing: true,
                serverSide: true,
                ajax:
                {
                    url: "api/tasks/GetDocLinkedTasks",
                    data: function (data) {
                        for (var i = 0, len = data.columns.length; i < len; i++) {
                            delete data.columns[i].search;
                            delete data.columns[i].searchable;
                            delete data.columns[i].orderable;
                            delete data.columns[i].name;
                        }
                        delete data.search.regex;
                        data.doc_id = "<%=Document_ID%>";
                    },
                    contentType: "application/json",
                    type: "GET",
                    dataType: "JSON"
                },
                columns: [
                    { data: "Edit_Button" },
                    { data: "Task_Number" },
                    { data: "Workflow_Name" },
                    { data: "Display_Name" },
                    { data: "Current_Step" }
                ],
                columnDefs: [
                    { orderable: false, width: "22px", targets: 0 }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });
            setInterval(function () {
                $('#tblLinkedTasks').DataTable().ajax.reload(null, false);
            }, <%=Refresh_Frequency%>);

            $('#tblLinked_Docs').dataTable({
                pageLength: 50,
                order: [[2, 'asc']],
                responsive: true,
                autoWidth: true,
                processing: true,
                serverSide: true,
                ajax:
                {
                    url: "api/tasks/GetDocumentsToLink",
                    data: function (data) {
                        for (var i = 0, len = data.columns.length; i < len; i++) {
                            delete data.columns[i].search;
                            delete data.columns[i].searchable;
                            delete data.columns[i].orderable;
                            delete data.columns[i].name;
                        }
                        delete data.search.regex;
                        data.Doc_Pr_ID = "<%=Doc_Project_ID%>";
                    },
                    contentType: "application/json",
                    type: "GET",
                    dataType: "JSON"
                },
                columns: [
                    { data: "Edit_Button" },
                    { data: "Document_No" },
                    { data: "Created_Date" },
                    { data: "Display_Name" }<%=Tag_List%>,
                    { data: "Open_Button" },
                ],
                columnDefs: [
                    { orderable: false, width: "22px", targets: 0 },
                    { orderable: false, width: "22px", targets: <%=Grid_Last_Col%> }

                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });
            setInterval(function () {
                $('#tblLinked_Docs').DataTable().ajax.reload(null, false);
            }, <%=Refresh_Frequency%>);

            $(".dataTables_filter").children().children('input').attr('placeholder', 'Search...');
        }

        <%=Delete_Scripts%>

        <%=Required_Fields%>

        <%=DocHelpScript%>
    </script>
</asp:Content>