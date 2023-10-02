<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid_task_list_t3.master" AutoEventWireup="true" CodeBehind="task_list_inactive_alt.aspx.cs" Inherits="N_Ter_Tasks.task_list_inactive_alt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter Tasks > My Tasks List - Inactive
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    My Tasks List - Inactive
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="tab-base">
        <ul class="nav nav-tabs nav-tabs-simple">
            <asp:Literal ID="ltrTabs" runat="server"></asp:Literal>
        </ul>
        <div class="panel panel-info">
            <div class="panel-body no-padding">
                <div id="divTabPages" runat="server" class="tab-content grid-with-paging">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
</asp:Content>