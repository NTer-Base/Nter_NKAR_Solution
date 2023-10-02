<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin.master" AutoEventWireup="true" CodeBehind="dashboard_tasks.aspx.cs" Inherits="N_Ter_Tasks.dashboard_tasks" %>

<%@ Register Src="~/controls_customizable/cuz_dashboard.ascx" TagPrefix="uc1" TagName="cuz_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" Runat="Server">
    N-Ter Tasks > Dashboard - Tasks
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" Runat="Server">
    Dashboard - Tasks
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" Runat="Server">
    <uc1:cuz_dashboard runat="server" ID="cuz_dashboard" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" Runat="Server">
</asp:Content>