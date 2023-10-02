<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid_task_list_t1.master" AutoEventWireup="true" CodeBehind="task_list_timeline.aspx.cs" Inherits="N_Ter_Tasks.task_list_timeline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter Tasks > My Task Overview - Timeline
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    My Task Overview - Timeline
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="panel panel-info">
        <div class="panel-body p10">
            <div id="divGrid" runat="server" class="table-responsive table-primary no-margin-b"></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script type="text/javascript">
        function ArrangeTooltip() {
            $(".prg_row").tooltip();
        }
    </script>
</asp:Content>