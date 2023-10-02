<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid.master" AutoEventWireup="true" CodeBehind="user_groups.aspx.cs" Inherits="N_Ter_Tasks.user_groups" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > User Groups
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    User Groups
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <asp:HiddenField ID="hndUserGroupID" runat="server" />
    <div class="panel panel-info">
        <div class="panel-body">
            <div class="padding-xs-vr" style="min-height: 50px">
                <button id="cmdNew" runat="server" class="btn btn-primary btn-labeled"><span class="btn-label icon fa fa-plus"></span>New User Group</button>
                <asp:ModalPopupExtender ID="cmdNew_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlData" TargetControlID="cmdNew" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                </asp:ModalPopupExtender>
                <button id="cmdGroupRights" runat="server" class="btn btn-labeled btn-warning pull-right" onserverclick="cmdGroupRights_ServerClick">
                    <span class="btn-label icon fa fa-file-text"></span>User Group Rights Sheet</button>
            </div>
            <div class="table-responsive table-primary no-margin-b">
                <asp:GridView ID="gvMain" runat="server" CssClass="table table-striped table-hover grid_table grid_user_groups non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvMain_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="User_Group_ID" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <button id='cmdEdit' type='submit' runat="server" class="btn btn-primary btn-xs" title="Edit"><span class="fa fa-edit button_icon"></span></button>
                                <asp:ModalPopupExtender ID="cmdEdit_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlData" TargetControlID="cmdEdit" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                                </asp:ModalPopupExtender>
                            </ItemTemplate>
                            <ItemStyle Width="22px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <button id='cmdUserList' type='submit' runat="server" class="btn btn-info btn-xs" title="Users List"><span class="fa fa-users button_icon"></span></button>
                                <asp:ModalPopupExtender ID="cmdUserList_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlUsers" TargetControlID="cmdUserList" CancelControlID="cmdUserListClose" BackgroundCssClass="at_modelpopup_background">
                                </asp:ModalPopupExtender>
                            </ItemTemplate>
                            <ItemStyle Width="22px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <button id='cmdDelete' type='submit' runat="server" class="btn btn-danger btn-xs" title="Delete"><i class="fa fa-trash-o button_icon"></i></button>
                                <asp:ModalPopupExtender ID="cmdDelete_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDelete" TargetControlID="cmdDelete" CancelControlID="cmdCancel" BackgroundCssClass="at_modelpopup_background">
                                </asp:ModalPopupExtender>
                            </ItemTemplate>
                            <ItemStyle Width="22px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="User_Group_Name" HeaderText="User Group Name" />
                    </Columns>
                </asp:GridView>
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
                    <h4 class="panel-title">Add/Edit User Groups</h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>User Group Name</label>
                            <asp:TextBox ID="txtUserGroupName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Access Level</label>
                            <asp:DropDownList ID="cboAccessLevel" runat="server" CssClass="form-control">
                                <asp:ListItem Value="3">Level 3</asp:ListItem>
                                <asp:ListItem Value="2">Level 2</asp:ListItem>
                                <asp:ListItem Value="1">Level 1</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="tab-base">
                            <ul class="nav nav-tabs nav-tabs-simple">
                                <li id="tab1" class="active">
                                    <a data-toggle='tab' href='#tab_cont1'>Allowed Pages</a>
                                </li>
                                <li id="tab2" runat="server">
                                    <a data-toggle='tab' href='#tab_cont2'>Allowed Reports</a>
                                </li>
                                <li id="tab3" runat="server">
                                    <a data-toggle='tab' href='#tab_cont3'>Allowed Other Sections</a>
                                </li>
                                <li id="tab4">
                                    <a data-toggle='tab' href='#tab_cont4'>Special Rights</a>
                                </li>
                            </ul>
                            <div class="panel panel-info no-margin-b">
                                <div class="panel-body no-padding">
                                    <div class="tab-content grid-with-paging">
                                        <div id="tab_cont1" class="tab-pane fade active in">
                                            <asp:CheckBoxList ID="chkAllowedPages" runat="server" CssClass="checkboxlist"></asp:CheckBoxList>
                                        </div>
                                        <div id="tab_cont2" runat="server" class="tab-pane fade">
                                            <asp:CheckBoxList ID="chkAllowedReports" runat="server" CssClass="checkboxlist"></asp:CheckBoxList>
                                        </div>
                                        <div id="tab_cont3" runat="server" class="tab-pane fade">
                                            <asp:CheckBoxList ID="chkAllowedSections" runat="server" CssClass="checkboxlist"></asp:CheckBoxList>
                                        </div>
                                        <div id="tab_cont4" class="tab-pane fade">
                                            <div>
                                                <asp:CheckBox ID="chkDeleteDocuments" runat="server" CssClass="checkboxlist" Text="Delete Task Documents" />
                                            </div>
                                            <div>
                                                <asp:CheckBox ID="chkDeleteAddons" runat="server" CssClass="checkboxlist" Text="Delete Task Addons" />
                                            </div>
                                            <div>
                                                <asp:CheckBox ID="chkDeleteComments" runat="server" CssClass="checkboxlist" Text="Delete Task Comments" />
                                            </div>
                                            <div>
                                                <asp:CheckBox ID="chkEditDocRepo" runat="server" CssClass="checkboxlist" Text="Edit Documents in Document Repos" />
                                            </div>
                                            <div>
                                                <asp:CheckBox ID="chkTaskScript" runat="server" CssClass="checkboxlist" Text="Enable Task Script Extraction" />
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
    <asp:Panel ID="pnlUsers" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_user_indent" class="at_modelpopup_indent">
            <div id="at_model_user_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Users List -
                        <asp:Label ID="lblGroupName" runat="server" Text="Label"></asp:Label></h4>
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
                    <asp:Button ID="cmdDelete" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDelete_Click" />
                    <asp:Button ID="cmdCancel" runat="server" Text="No" CssClass="btn btn-default" />
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
            AdjustPopupSize(80, 600, 'at_model_user');
        }

        function ArrangeGrids() {
            $('.grid_user_groups').dataTable({
                "pageLength": 50,
                "order": [[3, 'asc']],
                "responsive": true,
                "autoWidth": true,
                "columnDefs": [
                    { 'orderable': false, targets: 0 },
                    { 'orderable': false, targets: 1 },
                    { 'orderable': false, targets: 2 }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });

            $(".dataTables_filter").children().children('input').attr('placeholder', 'Search...');
        }

        function ClearControls() {
            clearTextBox(['<%=txtUserGroupName.ClientID%>']);
            clearDropDown(['<%=cboAccessLevel.ClientID%>']);
            clearCheckListBox(['<%=chkAllowedPages.ClientID%>', '<%=chkAllowedReports.ClientID%>', '<%=chkAllowedSections.ClientID%>']);
            clearCheckBox(['<%=chkDeleteDocuments.ClientID%>', '<%=chkDeleteAddons.ClientID%>', '<%=chkDeleteComments.ClientID%>', '<%=chkEditDocRepo.ClientID%>', '<%=chkTaskScript.ClientID%>']);
            $('#tab1').addClass('active');
            $('#tab2').removeClass('active');
            $('#tab3').removeClass('active');
            $('#tab4').removeClass('active');
            $('#tab_cont1').addClass('active in');
            $('#tab_cont2').removeClass('active in');
            $('#tab_cont3').removeClass('active in');
            $('#tab_cont4').removeClass('active in');
            return false;
        }

        function ResetControls() {
            if ($("#<%=hndUserGroupID.ClientID%>").val().trim() == '0') {
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
                url: "api/tasks/GetUserGroup",
                data: { User_Group_ID: $("#<%=hndUserGroupID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassed(result) {
            ClearControls();

            var resultObject = result;
            $('#<%=txtUserGroupName.ClientID%>').val(resultObject[0].User_Group_Name);
            $('#<%=cboAccessLevel.ClientID%>').val(resultObject[0].Access_Level);

            if (resultObject[0].Delete_Docs == '1') {
                onCheckBox('#<%=chkDeleteDocuments.ClientID%>');
            }
            if (resultObject[0].Delete_Addons == '1') {
                onCheckBox('#<%=chkDeleteAddons.ClientID%>');
            }
            if (resultObject[0].Delete_Comments == '1') {
                onCheckBox('#<%=chkDeleteComments.ClientID%>');
            }
            if (resultObject[0].Edit_Doc_Repo_Documents == '1') {
                onCheckBox('#<%=chkEditDocRepo.ClientID%>');
            }
            if (resultObject[0].Extract_Task_Scripts == '1') {
                onCheckBox('#<%=chkTaskScript.ClientID%>');
            }

            var resultObjectSub = resultObject[0].Pages_For_Group;
            $('#<%=chkAllowedPages.ClientID%> input').each(function () {
                for (var i = 0; i < resultObjectSub.length; i++) {
                    if (resultObjectSub[i].Page_ID.trim() == $(this).val().trim()) {
                        onCheckBox(this);
                    }
                }
            });

            var resultObjectSub2 = resultObject[0].Reports_For_Group;
            $('#<%=chkAllowedReports.ClientID%> input').each(function () {
                for (var i = 0; i < resultObjectSub2.length; i++) {
                    if (resultObjectSub2[i].Report_ID.trim() == $(this).val().trim()) {
                        onCheckBox(this);
                    }
                }
            });

            var resultObjectSub3 = resultObject[0].Secitons_For_Group;
            $('#<%=chkAllowedSections.ClientID%> input').each(function () {
                for (var i = 0; i < resultObjectSub3.length; i++) {
                    if (resultObjectSub3[i].Section_ID.trim() == $(this).val().trim()) {
                        onCheckBox(this);
                    }
                }
            });
        }

        function LoadUsers() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetUserGroupUsers",
                data: { User_Group_ID: $("#<%=hndUserGroupID.ClientID%>")[0].value },
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
    </script>
</asp:Content>