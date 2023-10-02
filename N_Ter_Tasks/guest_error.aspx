<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_guest.master" AutoEventWireup="true" CodeBehind="guest_error.aspx.cs" Inherits="N_Ter_Tasks.guest_error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter > Error
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contBody" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="h3 mt mb20">Unauthorized Access</div>
                    You are <b>Not Authorized</b> to <b>Access</b> this page. Please contact the System’s Administrator
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
</asp:Content>