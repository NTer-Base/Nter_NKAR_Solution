<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin.master" AutoEventWireup="true" CodeBehind="no_access.aspx.cs" Inherits="N_Ter_Tasks.no_access" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Access Issue Log
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Access Issue Log
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="h3 mt mb20">Unauthorized Access</div>
                    You do not have enough rights to access the requested page. Please contact the System Administrator.
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
</asp:Content>