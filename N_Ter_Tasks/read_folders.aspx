<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid.master" AutoEventWireup="true" CodeBehind="read_folders.aspx.cs" Inherits="N_Ter_Tasks.read_folders" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Force Read Folders
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Force Read Folders
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="panel panel-info">
        <div class="panel-body" style="padding-bottom: 0px">
            <div id="divError" runat="server" class="alert alert-danger">
                <strong>Error !</strong>
                <asp:Literal ID="ltrError" runat="server"></asp:Literal>
            </div>
            <div class="alert alert-warning">
                <strong>Warninig !</strong>
                <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
            </div>
            <div id="divSubmit" runat="server" style="padding-bottom: 20px">
                <button id="cmdReceive" class="btn btn-labeled btn-primary" onclick="return ReadFolders();"><span class="btn-label icon fa fa-folder-open-o"></span>Read All Folders</button>
                <asp:Button ID="cmdReceiveHide" runat="server" Text="" CssClass="hide" OnClick="cmdReceiveHide_Click" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-body">
            <div class="panel-title-nter">Folder Read Rules</div>
            <div class="border-t panel-info" style="height: 20px"></div>
            <div class="table-responsive table-primary no-margin-b">
                <asp:GridView ID="gvRules" runat="server" CssClass="table table-striped table-hover grid_table grid_mail_boxes non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Folder_Name" HeaderText="Folder Name" />
                        <asp:BoundField DataField="Workflow_Name" HeaderText="Workflow Name" />
                        <asp:BoundField DataField="Display_Name" HeaderText="Display_Name" />
                        <asp:BoundField DataField="Criteria_Text" HeaderText="Criteria" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script type="text/javascript">
        init.push(function () {
            ArrangeGrids();
        });

        function ReadFolders() {
            $('#<%=cmdReceiveHide.ClientID%>').click();
            return false;
        }

        function ArrangeGrids() {
            $('.grid_mail_boxes').dataTable({
                "pageLength": 50,
                "order": [[0, 'asc']],
                "responsive": true,
                "autoWidth": true
            });

            $(".dataTables_filter").children().children('input').attr('placeholder', 'Search...');
        }
    </script>
</asp:Content>