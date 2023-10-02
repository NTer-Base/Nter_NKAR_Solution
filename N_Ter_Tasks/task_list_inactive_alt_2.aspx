<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid_task_list_t2.master" AutoEventWireup="true" CodeBehind="task_list_inactive_alt_2.aspx.cs" Inherits="N_Ter_Tasks.task_list_inactive_alt_2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" Runat="Server">
    N-Ter Tasks > My Tasks List - Inactive
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" Runat="Server">
    My Tasks List - Inactive
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" Runat="Server">
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
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" Runat="Server">
</asp:Content>