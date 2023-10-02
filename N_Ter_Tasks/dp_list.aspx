<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid.master" AutoEventWireup="true" CodeBehind="dp_list.aspx.cs" Inherits="N_Ter_Tasks.dp_list" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > My Document Repos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    My Document Repos
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="row">
        <div class="col-md-3">
            <div class="form-group">
                <label>
                    <asp:Literal ID="ltrEL2" runat="server"></asp:Literal>
                    Name</label>
                <asp:DropDownList ID="cboEL2Name" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <h5 class="text-semibold">Repository</h5>
            <asp:HiddenField ID="hndDoc_Proj_ID" runat="server" />
            <div class="list-group">
                <asp:Literal ID="ltrDocProjects" runat="server"></asp:Literal>
            </div>
        </div>
        <div class="col-md-9">
            <asp:Panel ID="pnlStep" runat="server" CssClass="panel panel-info">
                <div class="panel-body grid-with-paging">
                    <div class="panel_menu">
                        <button id="cmdNew" runat="server" class="btn btn-labeled btn-primary" onclick="ClearUploadControls();"><span class="btn-label icon fa fa-plus"></span>Add New Document</button>
                        <asp:ModalPopupExtender ID="cmdNew_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlData" TargetControlID="cmdNew" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                        </asp:ModalPopupExtender>
                        <button id="cmdAdvFilter" runat="server" class="btn btn-labeled btn-primary" onserverclick="cmdAdvFilter_ServerClick" ><span class="btn-label icon fa fa-search"></span>Advance Filter</button>
                    </div>
                    <div class="ml10 mt10">
                        <div class="panel-title-nter">Documents</div>
                    </div>
                    <div class="border-t panel-info" style="height: 10px"></div>
                    <div class="table-responsive table-primary no-margin-b">
                        <table id="tblUsers" class="table table-striped table-hover grid_table grid_test full_width_table" data-size="full_width_table">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th></th>
                                    <th>Document No</th>
                                    <th>Date</th>
                                    <%=Tag_Names%>
                                    <th></th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <asp:Panel ID="pnlData" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_indent" class="at_modelpopup_indent">
            <div id="at_model_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdNo" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">New Document</h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>File</label>
                            <asp:FileUpload ID="fulDocument" runat="server" CssClass="form-control st_file_upload" />
                        </div>
                        <div class="form-group">
                            <label>File Name</label>
                            <asp:TextBox ID="txtFileName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Access Level</label>
                            <asp:DropDownList ID="cboAccessLevel" runat="server" CssClass="form-control">
                                <asp:ListItem Value="3">Level 3</asp:ListItem>
                                <asp:ListItem Value="2">Level 2</asp:ListItem>
                                <asp:ListItem Value="1">Level 1</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div id="divRelatedTasks" class="form-group">
                            <label>Related Task</label>
                            <select id="relatedTasks" class="form-control" onchange="SaveRelatedTaskID();"></select>
                            <asp:HiddenField ID="hndRelatedTaskID" runat="server" />
                        </div>
                        <div class="form-group">
                            <h3>Tags</h3>
                        </div>
                        <div id="divTags" runat="server"></div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSave_Click" />
                    <asp:Button ID="cmdReset" runat="server" Text="Reset" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndDocument_ID" runat="server" />
    <asp:ModalPopupExtender ID="hndDocument_ID_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuDelete" PopupControlID="pnlDelete" TargetControlID="hndDocument_ID" CancelControlID="cmdCancel" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
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
        });

        function ArrangeGrids() {
            $('#tblUsers').dataTable({
                pageLength: 50,
                order: [[2, 'asc']],
                responsive: true,
                autoWidth: true,
                processing: true,
                serverSide: true,
                ajax:
                {
                    url: "api/tasks/GetDocuments",
                    data: function (data) {
                        for (var i = 0, len = data.columns.length; i < len; i++) {
                            delete data.columns[i].search;
                            delete data.columns[i].searchable;
                            delete data.columns[i].orderable;
                            delete data.columns[i].name;
                        }
                        delete data.search.regex;
                        data.Doc_Pr_ID = $("#<%=hndDoc_Proj_ID.ClientID%>").val();
                        data.Entity_L2_ID = $("#<%=cboEL2Name.ClientID%>").val();
                    },
                    contentType: "application/json",
                    type: "GET",
                    dataType: "JSON"
                },
                columns: [
                    { data: "Edit_Button" },
                    { data: "Delete_Button" },
                    { data: "Document_No" },
                    { data: "Created_Date" }<%=Tag_List%>,
                    { data: "Open_Button" },
                ],
                columnDefs: [
                    { orderable: false, width: "22px", targets: 0 },
                    { orderable: false, width: "22px", targets: 1 },
                    { orderable: false, width: "22px", targets: <%=Grid_Last_Col%> }

                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });
            setInterval(function () {
                $('#tblUsers').DataTable().ajax.reload(null, false);
            }, <%=Refresh_Frequency%>);

            $(".dataTables_filter").children().children('input').attr('placeholder', 'Search...');
        }

        function ReloadGrid() {
            $('#tblUsers').DataTable().ajax.reload(null, false);
        }

        function ArrangePopups() {
            AdjustPopupSize(167, 800, 'at_model');
            AdjustPopupSize(80, 400, 'at_del_model');
            <%=DocHelpPanelResizeScript%>
        }

        function deleteRec(rec) {
            $('#<%=hndDocument_ID.ClientID%>').val(rec);
            $find("mpuDelete").show();
            return false;
        }

        function ClearUploadControls() {
            clearFleUpload(['<%=fulDocument.ClientID%>']);
            clearDropDown(['<%=cboAccessLevel.ClientID%>']);
            clearTextBox(['<%=txtFileName.ClientID%>']);
            LoadRelatedTasks();
            return false;
        }

        function LoadRelatedTasks() {
            ReloadGrid();
            if ($('#<%=hndDoc_Proj_ID.ClientID%>').val() == '0') {
                $('#divRelatedTasks').addClass("hide");
                $('#<%=hndRelatedTaskID.ClientID%>').val('0');
                $('#relatedTasks').empty();
            }
            else {
                $.ajax({
                    type: "GET",
                    url: "api/tasks/GetDocProjectRelatedTasks",
                    data: { Entity_L2_ID: $('#<%=cboEL2Name.ClientID%>').val(), Document_Project_ID: $('#<%=hndDoc_Proj_ID.ClientID%>').val() },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: LoadRelatedTasksPassed,
                    failure: LoadRelatedTasksFailed
                });
            }
        }

        function LoadRelatedTasksFailed(result) {
            $('#divRelatedTasks').addClass("hide");
            $('#<%=hndRelatedTaskID.ClientID%>').val('0');
            $('#relatedTasks').empty();
        }

        function LoadRelatedTasksPassed(result) {
            if (JSON.stringify(result).trim() == '') {
                $('#divRelatedTasks').addClass("hide");
                $('#<%=hndRelatedTaskID.ClientID%>').val('0');
                $('#relatedTasks').empty();
                $('#relatedTasks').append(
                    $('<option>', {
                        value: '0',
                        text: '[N/A]'
                    }, '<option/>'));
            }
            else {
                var resultObject = result;
                $('#divRelatedTasks').removeClass("hide");
                $('#relatedTasks').empty();
                $('#relatedTasks').append(
                    $('<option>', {
                        value: '0',
                        text: '[N/A]'
                    }, '<option/>'));
                for (var i = 0; i < resultObject.length; i++) {
                    $('#relatedTasks').append(
                        $('<option>', {
                            value: resultObject[i].Task_ID.trim(),
                            text: resultObject[i].Task_Number.trim()
                        }, '<option/>'));
                }
                $('#<%=hndRelatedTaskID.ClientID%>').val($('#relatedTasks').val());
            }
        }

        function SaveRelatedTaskID() {
            $('#<%=hndRelatedTaskID.ClientID%>').val($('#relatedTasks').val());
        }

        <%=required_fields%>

        <%=DocHelpScript%>
    </script>
</asp:Content>