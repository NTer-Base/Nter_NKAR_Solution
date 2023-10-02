<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid.master" AutoEventWireup="true" CodeBehind="users.aspx.cs" Inherits="N_Ter_Tasks.users" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Users
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
    <link href="assets/stylesheets/pages.min.css" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Users
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <asp:HiddenField ID="hndUserID" runat="server" />
    <div class="panel panel-info">
        <div class="panel-body">
            <div class="padding-xs-vr" style="min-height: 50px">
                <button id="cmdNew" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New User</button>
                <asp:HiddenField ID="hndRec" runat="server" />
                <asp:ModalPopupExtender ID="hndRec_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuRec" PopupControlID="pnlData" TargetControlID="hndRec" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                </asp:ModalPopupExtender>
                <asp:HiddenField ID="hndDelete" runat="server" />
                <asp:ModalPopupExtender ID="hndDelete_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuDel" PopupControlID="pnlDelete" TargetControlID="hndDelete" CancelControlID="cmdCancel" BackgroundCssClass="at_modelpopup_background">
                </asp:ModalPopupExtender>
                <button id="cmdStaffAllocation" runat="server" class="btn btn-labeled btn-warning pull-right"><span class="btn-label icon fa fa-file-text"></span>Staff Allocation Sheet</button>
                <asp:ModalPopupExtender ID="cmdStaffAllocation_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuAlloExport" PopupControlID="pnlAlloExport" TargetControlID="cmdStaffAllocation" CancelControlID="cmsAllocationCancel" BackgroundCssClass="at_modelpopup_background">
                </asp:ModalPopupExtender>
            </div>
            <div class="table-responsive table-primary no-margin-b">
                <table id="tblUsers" class="table table-striped table-hover grid_table grid_test non_full_width_table" data-size="non_full_width_table">
                    <thead>
                        <tr>
                            <th></th>
                            <th></th>
                            <th>E-Mail</th>
                            <th>User Code</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Supervisor Name</th>
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
                    <h4 class="panel-title">Add/Edit User</h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="tab-base">
                            <ul class="nav nav-tabs nav-tabs-simple">
                                <li id="tab1" class="active">
                                    <a data-toggle='tab' href='#tab_cont1'>Main Info</a>
                                </li>
                                <li id="tab2">
                                    <a data-toggle='tab' href='#tab_cont2'>User Groups</a>
                                </li>
                                <li id="tab3">
                                    <a data-toggle='tab' href='#tab_cont3'>
                                        <asp:Literal ID="ltrEL2s" runat="server"></asp:Literal></a>
                                </li>
                                <li id="tab4">
                                    <a data-toggle='tab' href='#tab_cont4'>Reporting Structure</a>
                                </li>
                            </ul>
                            <div class="panel panel-info">
                                <div class="panel-body no-padding">
                                    <div class="tab-content grid-with-paging">
                                        <div id="tab_cont1" class="tab-pane fade active in">
                                            <div class="form-group">
                                                <label>E-Mail</label>
                                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Password</label>
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                    <span class="input-group-btn">
                                                        <button class="btn btn-success" type="button" onclick="return showPWPolicy()"><i class='fa fa-question'></i></button>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label>Retype Password</label>
                                                <asp:TextBox ID="txtRePassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>User Code</label>
                                                <asp:TextBox ID="txtUserCode" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>First Name</label>
                                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Last Name</label>
                                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Phone</label>
                                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Designation</label>
                                                <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Supervisor</label>
                                                <asp:DropDownList ID="cboSupervisor" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label>Assignment of Work</label>
                                                <asp:DropDownList ID="cboTempUser" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label>Special Comments</label>
                                                <asp:TextBox ID="txtSpecialComments" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:CheckBox runat="server" ID="chkOverrideRestriction" CssClass="checkboxlist" /><span style="padding-left: 10px">Override Restrictions</span>
                                            </div>
                                            <div class="form-group">
                                                <asp:CheckBox runat="server" ID="chkActive" CssClass="checkboxlist" /><span style="padding-left: 10px">This is an Active User</span>
                                            </div>
                                        </div>
                                        <div id="tab_cont2" class="tab-pane fade">
                                            <div class="form-group-margin text-right">
                                                <button class="btn btn-labeled btn-primary" onclick="return selectAllUserGroups();">Select All</button>
                                                <button class="btn btn-labeled btn-primary" onclick="return deSelectAllUserGroups();">Unselect All</button>
                                            </div>
                                            <asp:HiddenField ID="hndGroups" ClientIDMode="Static" runat="server" />
                                            <div class="table-responsive table-primary no-margin-b">
                                                <asp:GridView ID="gvUserGroups" runat="server" CssClass="table table-striped table-hover grid_table grid_l2 full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvUserGroups_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="User_Group_ID" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxlist usergrp" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="22px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="User_Group_Name" HeaderText="User Group Name" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div id="tab_cont3" class="tab-pane fade">
                                            <div class="form-group-margin text-right">
                                                <button class="btn btn-labeled btn-primary" onclick="return selectAllEL2();">Select All</button>
                                                <button class="btn btn-labeled btn-primary" onclick="return deSelectAllEL2();">Unselect All</button>
                                            </div>
                                            <asp:HiddenField ID="hndEl2" ClientIDMode="Static" runat="server" />
                                            <div class="table-responsive table-primary no-margin-b">
                                                <asp:GridView ID="gvEntity_L2" runat="server" CssClass="table table-striped table-hover grid_table grid_l2 full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvEntity_L2_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="Entity_L2_ID" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxlist el2" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="22px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Display_Name" HeaderText="Entity Name" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div id="tab_cont4" class="tab-pane fade">
                                            <div class="timeline" style="padding-bottom: 0px; margin-bottom: 0px">
                                                <div id="strParents"></div>
                                                <div class="tl-entry left">
                                                    <img id="curImg" src="assets/images/user.png" alt="" class="rounded tl-icon user_report_image" style="width: 40px; height: 40px; margin-top: -2px;">
                                                    <div id="curName" class="panel tl-body" style="min-height: 50px">
                                                        Nilesh Piyaweera
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
                    <asp:Button ID="cmdDiscard" runat="server" Text="Discard" CssClass="btn btn-danger pull-left" OnClick="cmdDiscard_Click" />
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
                    <h4 class="panel-title">Delete - User</h4>
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
    <asp:Panel ID="pnlAlloExport" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_export_model_indent" class="at_modelpopup_indent">
            <div id="at_export_model_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Staff Allocation Sheet</h4>
                </div>
                <div id="at_export_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Export Type</label>
                            <asp:DropDownList ID="cboExportType" runat="server" CssClass="form-control">
                                <asp:ListItem Value="1">All Staff</asp:ListItem>
                                <asp:ListItem Value="2">Only Active Staff</asp:ListItem>
                                <asp:ListItem Value="3">Only Locked Staff</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox runat="server" ID="chkSendEmail" CssClass="checkboxlist" /><span style="padding-left: 10px">Send as an Email</span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdAllocationExport" runat="server" Text="Export" CssClass="btn btn-primary" OnClientClick="setTimeout(hideExport, 2000);" OnClick="cmdAllocationExport_Click" />
                    <asp:Button ID="cmsAllocationCancel" runat="server" Text="Cancel" CssClass="btn btn-default" />
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
            AdjustPopupSize(80, 500, 'at_model_help');
            AdjustPopupSize(80, 500, 'at_export_model');
        }

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
                    url: "api/tasks/GetUsers",
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
                    { data: "Delete_Button" },
                    { data: "Username" },
                    { data: "User_Code" },
                    { data: "First_Name" },
                    { data: "Last_Name" },
                    { data: "Supervisor_Name" }
                ],
                columnDefs: [
                    { orderable: false, width: "22px", targets: 0 },
                    { orderable: false, width: "22px", targets: 1 }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });

            $('.grid_l2').dataTable({
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
            clearTextBox(['<%=txtUsername.ClientID%>', '<%=txtUserCode.ClientID%>', '<%=txtPassword.ClientID%>', '<%=txtRePassword.ClientID%>', '<%=txtFirstName.ClientID%>', '<%=txtLastName.ClientID%>', '<%=txtPhone.ClientID%>', '<%=txtDesignation.ClientID%>', '<%=txtSpecialComments.ClientID%>']);
            clearCheckBox(['<%=chkActive.ClientID%>', '<%=chkOverrideRestriction.ClientID%>']);
            clearDropDown(['<%=cboSupervisor.ClientID%>', '<%=cboTempUser.ClientID%>']);
            deSelectAllEL2();
            deSelectAllUserGroups();
            showDataTab1();
            $('#curImg').attr('src', 'assets/images/user.png');
            $('#curName').html('');
            $('#strParents').html('');

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
            if ($("#<%=hndUserID.ClientID%>").val().trim() == '0') {
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
                url: "api/tasks/GetUser",
                data: { User_ID: $("#<%=hndUserID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassed(result) {
            ClearControls();

            var resultObject = result;
            $('#<%=txtUsername.ClientID%>').val(resultObject[0].Username);
            $('#<%=txtUserCode.ClientID%>').val(resultObject[0].User_Code);
            $('#<%=txtFirstName.ClientID%>').val(resultObject[0].First_Name);
            $('#<%=txtLastName.ClientID%>').val(resultObject[0].Last_Name);
            $('#<%=txtPhone.ClientID%>').val(resultObject[0].Phone);
            $('#<%=txtDesignation.ClientID%>').val(resultObject[0].Designation);
            $('#<%=cboSupervisor.ClientID%>').val(resultObject[0].Supervisor_ID);
            $('#<%=cboTempUser.ClientID%>').val(resultObject[0].Temp_User);
            $('#<%=txtSpecialComments.ClientID%>').val(resultObject[0].Special_Comments);
            $('#curImg').attr('src', resultObject[0].Image_Path);
            $('#curName').html(resultObject[0].First_Name + ' ' + resultObject[0].Last_Name);
            if (resultObject[0].isDeleted == '0') {
                onCheckBox('#<%=chkActive.ClientID%>');
            }
            if (resultObject[0].Override_Restrictions == '1') {
                onCheckBox('#<%=chkOverrideRestriction.ClientID%>');
            }

            var resultObjectSub = resultObject[0].Groups_For_User;
            $('#hndGroups').val('');
            $(".usergrp").each(function () {
                for (var i = 0; i < resultObjectSub.length; i++) {
                    if ($(this).attr('data-id').trim() == resultObjectSub[i].User_Group_ID.trim()) {
                        $(this).children().children('input').each(function () {
                            onCheckBox(this);
                        });
                        $('#hndGroups').val($('#hndGroups').val() + resultObjectSub[i].User_Group_ID.trim() + "|");
                    }
                }
            });

            var resultObjectSub2 = resultObject[0].EL2s_For_User;
            $('#hndEl2').val('');
            $(".el2").each(function () {
                for (var i = 0; i < resultObjectSub2.length; i++) {
                    if ($(this).attr('data-id').trim() == resultObjectSub2[i].Entity_L2_ID.trim()) {
                        $(this).children().children('input').each(function () {
                            onCheckBox(this);
                        });
                        $('#hndEl2').val($('#hndEl2').val() + resultObjectSub2[i].Entity_L2_ID.trim() + "|");
                    }
                }
            });

            LoadStructure();
        }

        $('#<%=txtFirstName.ClientID%>').keyup(function () {
            AdjustReportName();
        });

        $('#<%=txtLastName.ClientID%>').keyup(function () {
            AdjustReportName();
        });

        $('#<%=cboSupervisor.ClientID%>').change(function () {
            LoadStructure();
        });

        function AdjustReportName() {
            $('#curName').html($('#<%=txtFirstName.ClientID%>').val() + ' ' + $('#<%=txtLastName.ClientID%>').val());
        }

        function LoadStructure() {
            $('#strParents').html('');
            $.ajax({
                type: "GET",
                url: "api/tasks/GetUserHierarchy",
                data: { User_ID: $("#<%=cboSupervisor.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadStructurePassed,
                failure: LoadValuesFailed
            });
        }

        function LoadStructurePassed(result) {
            if (JSON.stringify(result).trim() != "") {
                var resultObject = result;
                var HierarchyText = "";
                for (var i = 0; i < resultObject.length; i++) {
                    HierarchyText = HierarchyText + "<div class='tl-entry left'>" +
                        "<img src='" + resultObject[i].Image_Path + "' alt='' class='rounded tl-icon user_report_image' style='width: 40px; height: 40px; margin-top: -2px;'>" +
                        "<div class='panel tl-body' style='min-height: 50px'>" +
                        resultObject[i].Full_Name +
                        "</div>" +
                        "</div>"
                }
                $('#strParents').html(HierarchyText);
            }
        }

        function selectAllUserGroups() {
            selectCheckBoxRange('usergrp');
            $('#hndGroups').val('');
            $(".usergrp").each(function () {
                $('#hndGroups').val($('#hndGroups').val() + $(this).attr('data-id').trim() + "|");
            });
            return false;
        }

        function deSelectAllUserGroups() {
            $('#hndGroups').val('');
            clearCheckBoxRange('usergrp');
            return false;
        }

        function selectAllEL2() {
            selectCheckBoxRange('el2');
            $('#hndEl2').val('');
            $(".el2").each(function () {
                $('#hndEl2').val($('#hndEl2').val() + $(this).attr('data-id').trim() + "|");
            });
            return false;
        }

        function deSelectAllEL2() {
            $('#hndEl2').val('');
            clearCheckBoxRange('el2');
            return false;
        }

        function editRec(recId) {
            $("#<%=hndUserID.ClientID%>").val(recId);
            $find("mpuRec").show();
            LoadValues();
            return false;
        }

        function deleteRec(recId) {
            $("#<%=hndUserID.ClientID%>").val(recId);
            $find("mpuDel").show();
            return false;
        }

        function showPWPolicy() {
            $find("mpuHelp").show();
            return false;
        }

        function hideExport() {
            $find("mpuAlloExport").hide();
        }
    </script>
</asp:Content>
