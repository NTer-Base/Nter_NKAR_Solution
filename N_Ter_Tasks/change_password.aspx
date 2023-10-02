<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base.Master" AutoEventWireup="true" CodeBehind="change_password.aspx.cs" Inherits="N_Ter_Tasks.change_password" %>

<%@ Register Src="~/controls_customizable/intro_section_1.ascx" TagPrefix="uc1" TagName="intro1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter Tasks > Change Password
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
    <link href="assets/stylesheets/pages.min.css" rel="stylesheet" type="text/css" />
    <link href="assets_customizable/stylesheets/customizable.css" rel="stylesheet" />
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
            <div id="divRenew" runat="server">
                <div class="signup-text">
                    <span>Your Password has Expired!</span>
                </div>
                <div class="form-group w-icon">
                    <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="form-control input-lg" placeholder="Current Password" TextMode="Password"></asp:TextBox>
                    <span class="fa fa-lock signup-form-icon"></span>
                </div>
                <div class="form-group w-icon">
                    <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control input-lg" placeholder="New Password" TextMode="Password"></asp:TextBox>
                    <span class="fa fa-lock signup-form-icon"></span>
                </div>
                <div class="form-group w-icon">
                    <asp:TextBox ID="txtRetypePassword" runat="server" CssClass="form-control input-lg" placeholder="Retype Password" TextMode="Password"></asp:TextBox>
                    <span class="fa fa-lock signup-form-icon"></span>
                </div>
                <div class="form-actions">
                    <asp:Button ID="cmdSubmit" runat="server" Text="Change Password" CssClass="signup-btn bg-primary" OnClick="cmdSubmit_Click" />
                </div>
            </div>
            <div id="divLogin" runat="server">
                <div class="signup-text">
                    <span>Your Password has been Updated!</span>
                </div>
                <div class="form-actions">
                    <asp:Button ID="cmdLogin" runat="server" Text="Login to N-Ter" CssClass="signup-btn bg-primary" OnClick="cmdLogin_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
</asp:Content>