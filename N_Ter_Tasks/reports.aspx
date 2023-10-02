<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin.master" AutoEventWireup="true" CodeBehind="reports.aspx.cs" Inherits="N_Ter_Tasks.reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" Runat="Server">
    N-Test-Tasks > Report Preview
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" Runat="Server">
    Report Preview - <asp:Literal ID="ltrReportName" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" Runat="Server">
    <div id="divContent" runat="server"></div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" Runat="Server">
</asp:Content>