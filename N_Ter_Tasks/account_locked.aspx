<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base.Master" AutoEventWireup="true" CodeBehind="account_locked.aspx.cs" Inherits="N_Ter_Tasks.account_locked" %>

<%@ Register Src="~/controls_customizable/intro_section_1.ascx" TagPrefix="uc1" TagName="intro1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter Tasks > Account Locked
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
    <link href="assets/stylesheets/pages.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contBody" runat="Server">
    <div id="page-signup-bg">
        <div class="overlay"></div>
        <img src="assets/images/signin-bg-1.jpg" alt="" />
    </div>
    <div class="signup-container">
        <div class="signup-header">
            <uc1:intro1 runat="server" ID="intro1" />
        </div>
        <div class="signup-form">
            <div id="divLogin" runat="server">
                <div class="signup-text">
                    <span>Your Account is Locked!</span>
                </div>
                <div class="form-actions">
                    <p>Please contact your Systems Administrator to Unlock your Account</p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
</asp:Content>