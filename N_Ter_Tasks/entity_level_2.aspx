<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid.master" AutoEventWireup="true" CodeBehind="entity_level_2.aspx.cs" Inherits="N_Ter_Tasks.entity_level_2" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Test-Tasks >
    <asp:Literal ID="ltrEL2_4" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
    <link href="assets/stylesheets/pages.min.css" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    <asp:Literal ID="ltrEL2_5" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <asp:HiddenField ID="hndEL2_ID" runat="server" />
    <div class="panel panel-info">
        <div class="panel-body">
            <div class="padding-xs-vr">
                <button id="cmdNew" runat="server" class="btn btn-labeled btn-primary">
                    <span class="btn-label icon fa fa-plus"></span>New
                    <asp:Literal ID="ltrEL2" runat="server"></asp:Literal></button>
                <asp:ModalPopupExtender ID="cmdNew_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuRec" PopupControlID="pnlData" TargetControlID="cmdNew" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                </asp:ModalPopupExtender>
                <asp:HiddenField ID="hndUserList" runat="server" />
                <asp:ModalPopupExtender ID="hndUserList_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuUsers" PopupControlID="pnlUsers" TargetControlID="hndUserList" CancelControlID="cmdUserListClose" BackgroundCssClass="at_modelpopup_background">
                </asp:ModalPopupExtender>
                <asp:HiddenField ID="hndDelete" runat="server" />
                <asp:ModalPopupExtender ID="hndDelete_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuDel" PopupControlID="pnlDelete" TargetControlID="hndDelete" CancelControlID="cmdCancel" BackgroundCssClass="at_modelpopup_background">
                </asp:ModalPopupExtender>
                <asp:HiddenField ID="hndDeactivate" runat="server" />
                <asp:ModalPopupExtender ID="hndDeactivate_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuDeactivate" PopupControlID="pnlDeactivate" TargetControlID="hndDeactivate" CancelControlID="cmdCancelDeactivate" BackgroundCssClass="at_modelpopup_background">
                </asp:ModalPopupExtender>
                <button id="cmdShowInactive" runat="server" class="btn btn-labeled btn-warning pull-right" onserverclick="cmdShowInactive_ServerClick">
                    <span class="btn-label icon fa fa-unlock-alt"></span>Show Inactive
                <asp:Literal ID="ltrEL2_8" runat="server"></asp:Literal></button>
            </div>
            <div class="table-responsive table-primary no-margin-b">
                <table id="tblEntityL2" class="table table-striped table-hover grid_table grid_test non_full_width_table" data-size="non_full_width_table">
                    <thead>
                        <tr>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th>
                                <asp:Literal ID="ltrEntityL2_Code" runat="server"></asp:Literal></th>
                            <th>
                                <asp:Literal ID="ltrEntityL2_Name" runat="server"></asp:Literal></th>
                            <th>
                                <asp:Literal ID="ltrEntityL1_Name" runat="server"></asp:Literal></th>
                        </tr>
                    </thead>
                </table>
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
                    <h4 class="panel-title">Add/Edit
                        <asp:Literal ID="ltrEL2_2" runat="server"></asp:Literal></h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="tab-base">
                            <ul class="nav nav-tabs nav-tabs-simple">
                                <li id="tab1" class="active">
                                    <a data-toggle='tab' href='#tab_cont1'>Main Info</a>
                                </li>
                                <li id="tab3">
                                    <a data-toggle='tab' href='#tab_cont3'>Workflows</a>
                                </li>
                                <li id="tab2">
                                    <a data-toggle='tab' href='#tab_cont2'>Users</a>
                                </li>
                                <li id="tab4">
                                    <a data-toggle='tab' href='#tab_cont4'>Structure</a>
                                </li>
                            </ul>
                            <div class="panel panel-info">
                                <div class="panel-body no-padding">
                                    <div class="tab-content grid-with-paging">
                                        <div id="tab_cont1" class="tab-pane fade active in">
                                            <div class="form-group">
                                                <label>Folder Name</label>
                                                <asp:TextBox ID="txtFolder" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>
                                                    <asp:Literal ID="ltrEL2_3" runat="server"></asp:Literal>
                                                    Code</label>
                                                <asp:TextBox ID="txtEntityCode" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Diaplay Name</label>
                                                <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Legal Name</label>
                                                <asp:TextBox ID="txtLegalName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Description</label>
                                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Height="80"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Street or PO Box</label>
                                                <asp:TextBox ID="txtPHStreet" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Town / City</label>
                                                <asp:TextBox ID="txtPHTown" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>State / Region</label>
                                                <asp:TextBox ID="txtPHState" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Main Contact Person</label>
                                                <asp:TextBox ID="txtMainContact" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Phone</label>
                                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>E-mail</label>
                                                <asp:TextBox ID="txtE_Mail" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Website</label>
                                                <asp:TextBox ID="txtWebSite" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>
                                                    Parent
                                                    <asp:Literal ID="ltrEL2_6" runat="server"></asp:Literal>
                                                    Name</label>
                                                <asp:DropDownList ID="cboEntity_Level_2" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label>
                                                    <asp:Literal ID="ltrEL1" runat="server"></asp:Literal></label>
                                                <asp:DropDownList ID="cboEntity_Level_1" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label>Default Letterhead</label>
                                                <asp:DropDownList ID="cboLetterHead" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div id="tab_cont3" class="tab-pane fade">
                                            <div class="form-group-margin text-right">
                                                <button class="btn btn-labeled btn-primary" onclick="return selectAllWF();">Select All</button>
                                                <button class="btn btn-labeled btn-primary" onclick="return deSelectAllWF();">Unselect All</button>
                                            </div>
                                            <asp:HiddenField ID="hndWorkflows" ClientIDMode="Static" runat="server" />
                                            <div class="table-responsive table-primary no-margin-b">
                                                <asp:GridView ID="gvWorkflows" runat="server" CssClass="table table-striped table-hover grid_table grid_wf" AutoGenerateColumns="False" OnRowDataBound="gvWorkflows_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="Walkflow_ID" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxlist wfs" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="22px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Workflow_Name" HeaderText="Workflow Name" />
                                                        <asp:TemplateField HeaderText="No of Units">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtUnits" runat="server" CssClass="form-control nou" Width="100"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="100px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div id="tab_cont2" class="tab-pane fade">
                                            <div class="col-md-12 hp0">
                                                <div class="alert alert-success mb10">
                                                    <strong>Required User Groups :</strong>
                                                    <div id="divUserGroups" runat="server"></div>
                                                </div>
                                            </div>
                                            <div class="form-group-margin text-right">
                                                <button class="btn btn-labeled btn-primary" onclick="return selectAllUsers();">Select All</button>
                                                <button class="btn btn-labeled btn-primary" onclick="return deSelectAllUsers();">Unselect All</button>
                                            </div>
                                            <asp:HiddenField ID="hndUsers" ClientIDMode="Static" runat="server" />
                                            <div class="table-responsive table-primary no-margin-b">
                                                <asp:GridView ID="gvUsers" runat="server" CssClass="table table-striped table-hover grid_table grid_users" AutoGenerateColumns="False" OnRowDataBound="gvUsers_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="User_ID" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxlist users" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="22px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Full_Name" HeaderText="Name" />
                                                        <asp:BoundField DataField="Groups" HeaderText="Member Of" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div id="tab_cont4" class="tab-pane fade" style="padding: 20px 50px 20px 0">
                                            <div class="timeline" style="padding-bottom: 0px; margin-bottom: 0px">
                                                <div id="strParents"></div>
                                                <div class="tl-entry left">
                                                    <div class="tl-icon bg-dark-gray"><i class="fa fa-check"></i></div>
                                                    <div class="panel tl-body">
                                                        <h4 class="text-info">Current Level</h4>
                                                        <div id="currCompany"></div>
                                                    </div>
                                                </div>
                                                <div class="tl-entry left">
                                                    <div class="tl-icon bg-success"><i class="fa fa-check"></i></div>
                                                    <div class="panel tl-body">
                                                        <h4 class="text-info">Subodinate
                                                            <asp:Literal ID="ltrEL2_7" runat="server"></asp:Literal></h4>
                                                        <div id="subCompanies"></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
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
    <asp:Panel ID="pnlUsers" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_user_indent" class="at_modelpopup_indent">
            <div id="at_model_user_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Users List - <span id="spnEl2_Name"></span></h4>
                </div>
                <div id="at_model_user_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <table id="users_list" class="table table-striped table-hover grid_table non_full_width_table" data-size="non_full_width_table" style="margin-bottom: 0px !important">
                            <tr>
                                <th><b>Name</b></th>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdUserListClose" runat="server" Text="OK" CssClass="btn btn-primary" />
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
                    <asp:Button ID="cmdDeleteEL2" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDelete_Click" />
                    <asp:Button ID="cmdCancel" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDeactivate" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_deac_model_indent" class="at_modelpopup_indent">
            <div id="at_deac_model_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Deactivate</h4>
                </div>
                <div id="at_deac_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDeactivateEL2" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDeactivateEL2_Click" />
                    <asp:Button ID="cmdCancelDeactivate" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndSimilarName" runat="server" />
    <asp:ModalPopupExtender ID="hndSimilarName_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuSimilarName" PopupControlID="pnlSimilarName" TargetControlID="hndSimilarName" CancelControlID="cmdCancelSimilarName" BackgroundCssClass="at_modelpopup_background_2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlSimilarName" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_name_model_indent" class="at_modelpopup_indent">
            <div id="at_name_model_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCancelSimilarName" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Similar Names</h4>
                </div>
                <div id="at_name_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="row pb5">
                            <div class="col-md-12">
                                There are other
                                <asp:Literal ID="ltrEL2_10" runat="server"></asp:Literal>
                                with Similar Names<br />
                                Please Open an <b>Existing
                                    <asp:Literal ID="ltrEL2_9" runat="server"></asp:Literal></b> or Proceed with <b>Current Update</b>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <select id="cboSimilarEL2" class="form-control"></select>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdOpenExisting" runat="server" Text="Open the Selected Company" CssClass="btn btn-primary" OnClientClick="return LoadSimilarEL2();" />
                    <asp:Button ID="cmdProceed" runat="server" Text="Proceed with Update" CssClass="btn btn-default" OnClick="cmdSave_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script type="text/javascript">
        $(function () {
            $(window).resize(function () {
                ArrangePopoups();
            });
        });

        init.push(function () {
            ArrangePopoups();
            ArrangeGrids();
        });

        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            var target = $(e.target).attr("href");
            if (target == "#tab_cont2") {
                LoadReqiredUserGroups();
            }
        });

        function LoadReqiredUserGroups() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetWFUserGroups",
                data: { Workflows_List: $("#<%=hndWorkflows.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadReqiredUserGroupsPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadReqiredUserGroupsPassed(result) {
            var resultObject = result;
            $('#<%=divUserGroups.ClientID%>').html(resultObject[0].Required_User_Groups);
        }

        function ArrangePopoups() {
            AdjustPopupSize(167, 600, 'at_name_model');
            AdjustPopupSize(167, 800, 'at_model');
            AdjustPopupSize(167, 800, 'at_model_user');
            AdjustPopupSize(80, 400, 'at_del_model');
            AdjustPopupSize(80, 400, 'at_deac_model');
        }

        function ArrangeGrids() {
            $('#tblEntityL2').dataTable({
                pageLength: 50,
                order: [[4, 'asc']],
                responsive: true,
                autoWidth: true,
                processing: true,
                serverSide: true,
                ajax:
                {
                    url: "api/tasks/GetEntityL2s",
                    data: function (data) {
                        for (var i = 0, len = data.columns.length; i < len; i++) {
                            delete data.columns[i].search;
                            delete data.columns[i].searchable;
                            delete data.columns[i].orderable;
                            delete data.columns[i].name;
                        }
                        delete data.search.regex;
                    },
                    contentType: "application/json",
                    type: "GET",
                    dataType: "JSON"
                },
                columns: [
                    { data: "Edit_Button" },
                    { data: "User_List_Button" },
                    { data: "Delete_Button" },
                    { data: "Deactivate_Button" },
                    { data: "Entity_Code" },
                    { data: "Display_Name" },
                    { data: "Entity_L1_Name" }
                ],
                columnDefs: [
                    { orderable: false, width: "22px", targets: 0 },
                    { orderable: false, width: "22px", targets: 1 },
                    { orderable: false, width: "22px", targets: 2 },
                    { orderable: false, width: "22px", targets: 3 }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });

            $('.grid_users').dataTable({
                "paging": false,
                "order": [[1, 'asc']],
                "responsive": true,
                "autoWidth": true,
                "columnDefs": [
                    { 'orderable': false, targets: 0 }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });

            $('.grid_wf').dataTable({
                "paging": false,
                "order": [[1, 'asc']],
                "responsive": true,
                "autoWidth": true,
                "columnDefs": [
                    { 'orderable': false, targets: 0 }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });

            $('.nav-tabs-simple').tabdrop();

            $(".dataTables_filter").children().children('input').attr('placeholder', 'Search...');
        }

        function ClearControls() {
            clearTextBox(['<%=txtFolder.ClientID%>', '<%=txtEntityCode.ClientID%>', '<%=txtDescription.ClientID%>', '<%=txtDisplayName.ClientID%>', '<%=txtE_Mail.ClientID%>', '<%=txtMainContact.ClientID%>', '<%=txtLegalName.ClientID%>', '<%=txtPhone.ClientID%>', '<%=txtPHState.ClientID%>', '<%=txtPHStreet.ClientID%>', '<%=txtPHTown.ClientID%>', '<%=txtWebSite.ClientID%>']);
            clearDropDown(['<%=cboEntity_Level_1.ClientID%>', '<%=cboEntity_Level_2.ClientID%>', '<%=cboLetterHead.ClientID%>']);
            $('#strParents').html('');
            $('#currCompany').html('');
            $('#subCompanies').html('');
            $('#<%=divUserGroups.ClientID%>').html('');
            deSelectAllUsers();
            deSelectAllWF();
            $(".nou").each(function () {
                $(this).val('0');
            });
            showDataTab1();
            return false;
        }

        function showDataTab1() {
            $('#tab1').addClass('active');
            $('#tab2').removeClass('active');
            $('#tab3').removeClass('active');
            $('#tab4').removeClass('active');

            $('#tab_cont1').addClass('active in');
            $('#tab_cont2').removeClass('active in');
            $('#tab_cont3').removeClass('active in');
            $('#tab_cont4').removeClass('active in');
        }

        function ResetControls() {
            if ($("#<%=hndEL2_ID.ClientID%>").val().trim() == '-1') {
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
                url: "api/tasks/GetEL2",
                data: { Entity_L2_ID: $("#<%=hndEL2_ID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassed(result) {
            ClearControls();

            var resultObject = result;
            $('#<%=txtFolder.ClientID%>').val(resultObject[0].Folder_Name);
            $('#<%=txtEntityCode.ClientID%>').val(resultObject[0].Entity_Code);
            $('#<%=txtDisplayName.ClientID%>').val(resultObject[0].Display_Name);
            $('#<%=txtLegalName.ClientID%>').val(resultObject[0].Legal_Name);
            $('#<%=txtDescription.ClientID%>').val(resultObject[0].Description);
            $('#<%=txtPHStreet.ClientID%>').val(resultObject[0].Address);
            $('#<%=txtPHTown.ClientID%>').val(resultObject[0].Town_City);
            $('#<%=txtPHState.ClientID%>').val(resultObject[0].State_Region);
            $('#<%=txtMainContact.ClientID%>').val(resultObject[0].Main_Contact_Person);
            $('#<%=txtPhone.ClientID%>').val(resultObject[0].Telephone);
            $('#<%=txtE_Mail.ClientID%>').val(resultObject[0].E_Mail);
            $('#<%=txtWebSite.ClientID%>').val(resultObject[0].Website);
            $('#<%=cboLetterHead.ClientID%>').val(resultObject[0].Default_Letter_Head);
            $('#<%=cboEntity_Level_1.ClientID%>').val(resultObject[0].Entity_L1_ID);
            $('#<%=cboEntity_Level_2.ClientID%>').val(resultObject[0].Parent_L2_ID);

            if (resultObject[0].isActive == true) {
                $('#<%=cmdSave.ClientID%>').prop('value', 'Save');
            }
            else {
                $('#<%=cmdSave.ClientID%>').prop('value', 'Save & Activate');
            }

            $('#currCompany').html(resultObject[0].Display_Name);

            var parents = '';
            var resultObjectSub3 = resultObject[0].EL2_Parents;
            for (var i = 0; i < resultObjectSub3.length; i++) {
                parents = parents + "<div class=\"tl-entry left\">" +
                    "<div class=\"tl-icon bg-success\"><i class=\"fa fa-check\"></i></div>" +
                    "<div class=\"panel tl-body\">" +
                    "<h4 class=\"text-info\">Parent Level " + (i + 1) + "</h4>" +
                    resultObjectSub3[i].Display_Name +
                    "</div>" +
                    "</div>";
            }
            $('#strParents').html(parents);

            var children = '';
            var resultObjectSub4 = resultObject[0].EL2_Children;
            for (var i = 0; i < resultObjectSub4.length; i++) {
                children = children + resultObjectSub4[i].Display_Name + '<br />';
            }
            $('#subCompanies').html(children);

            var resultObjectSub = resultObject[0].EL2_Users;
            $('#hndUsers').val('');
            $(".users").each(function () {
                for (var i = 0; i < resultObjectSub.length; i++) {
                    if ($(this).attr('data-id').trim() == resultObjectSub[i].User_ID.trim()) {
                        $(this).children().children('input').each(function () {
                            onCheckBox(this);
                        });
                        $('#hndUsers').val($('#hndUsers').val() + resultObjectSub[i].User_ID.trim() + "|");
                    }
                }
            });

            var resultObjectSub2 = resultObject[0].EL2_Workflows;
            $('#hndWorkflows').val('');
            $(".wfs").each(function () {
                for (var i = 0; i < resultObjectSub2.length; i++) {
                    if ($(this).attr('data-id').trim() == resultObjectSub2[i].Walkflow_ID.trim()) {
                        $(this).children().children('input').each(function () {
                            onCheckBox(this);
                        });
                        $(this).parent().parent().children().children('.nou').val(resultObjectSub2[i].No_Of_Units.trim());
                        $('#hndWorkflows').val($('#hndWorkflows').val() + resultObjectSub2[i].Walkflow_ID.trim() + "|");
                    }
                }
            });
        }

        function changeDN() {
            $('#currCompany').html($('#<%=txtDisplayName.ClientID%>').val());
        }

        function changeParents() {
            $('#strParents').html('');
            if ($("#<%=hndEL2_ID.ClientID%>").val().trim() != $('#<%=cboEntity_Level_2.ClientID%>').val().trim() && $('#<%=cboEntity_Level_2.ClientID%>').val().trim() != '0') {
                $.ajax({
                    type: "GET",
                    url: "api/tasks/GetEL2Parents",
                    data: { Entity_L2_ID: $("#<%=cboEntity_Level_2.ClientID%>").val().trim() },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: LoadParentsPassed,
                    failure: LoadValuesFailed
                });
            }
        }

        function LoadParentsPassed(result) {
            var resultObject = result;
            var parents = '';
            for (var i = 0; i < resultObject.length; i++) {
                parents = parents + "<div class=\"tl-entry left\">" +
                    "<div class=\"tl-icon bg-success\"><i class=\"fa fa-check\"></i></div>" +
                    "<div class=\"panel tl-body\">" +
                    "<h4 class=\"text-info\">Parent Level " + (i + 1) + "</h4>" +
                    resultObject[i].Display_Name +
                    "</div>" +
                    "</div>";
            }
            $('#strParents').html(parents);
        }

        function selectAllUsers() {
            selectCheckBoxRange('users');
            $('#hndUsers').val('');
            $(".users").each(function () {
                $('#hndUsers').val($('#hndUsers').val() + $(this).attr('data-id').trim() + "|");
            });
            return false;
        }

        function deSelectAllUsers() {
            $('#hndUsers').val('');
            clearCheckBoxRange('users');
            return false;
        }

        function selectAllWF() {
            selectCheckBoxRange('wfs');
            $('#hndWorkflows').val('');
            $(".wfs").each(function () {
                $('#hndWorkflows').val($('#hndWorkflows').val() + $(this).attr('data-id').trim() + "|");
            });
            return false;
        }

        function deSelectAllWF() {
            $('#hndWorkflows').val('');
            clearCheckBoxRange('wfs');
            return false;
        }

        function LoadUsers() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetEL2Users",
                data: { Entity_L2_ID: $("#<%=hndEL2_ID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadUsersPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadUsersPassed(result) {
            resetTableUsers();

            var resultObject = result;
            for (var i = 0; i < resultObject.length; i++) {
                addNewUserRow(resultObject[i].Full_Name);
            }
        }

        function addNewUserRow(userName) {
            $('#users_list tr').last().after(addRowUser(userName));
            return false;
        }

        function addRowUser(userName) {
            return "<tr>" +
                "<td>" + userName + "</td>" +
                "</tr>";
        }

        function resetTableUsers() {
            while ($('#users_list tr').length > 1) {
                $('#users_list tr:last-child').remove();
            }
        }

        function LoadSimilarEL2() {
            $find('mpuSimilarName').hide();
            $('#<%=hndEL2_ID.ClientID%>').val($('#cboSimilarEL2').val());
            LoadValues();
            return false;
        }

        function editRec(recId) {
            $("#<%=hndEL2_ID.ClientID%>").val(recId);
            $find("mpuRec").show();
            LoadValues();
            return false;
        }

        function deleteRec(recId) {
            $("#<%=hndEL2_ID.ClientID%>").val(recId);
            $find("mpuDel").show();
            return false;
        }

        function usersList(recId, entity_el2) {
            $("#<%=hndEL2_ID.ClientID%>").val(recId);
            $find("mpuUsers").show();
            $('#spnEl2_Name').html(entity_el2);
            LoadUsers();
            return false;
        }

        function deactivateEl2(recId) {
            $("#<%=hndEL2_ID.ClientID%>").val(recId);
            $find("mpuDeactivate").show();
            return false;
        }
    </script>
</asp:Content>