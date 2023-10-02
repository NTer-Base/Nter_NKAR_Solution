<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base.Master" AutoEventWireup="true" CodeBehind="subscription.aspx.cs" Inherits="N_Ter_Tasks.subscription" %>

<%@ Register Src="~/controls_customizable/intro_section_1.ascx" TagPrefix="uc1" TagName="intro1" %>
<%@ Register Src="~/controls_customizable/intro_section_2.ascx" TagPrefix="uc2" TagName="intro2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter Tasks > Subscriptions
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
    <link href="assets/stylesheets/pages.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contBody" runat="Server">
    <div id="page-signin-bg">
        <div class="overlay"></div>
        <img src="assets/images/signin-bg-1.jpg" alt="" />
    </div>
    <div class="signin-container">
        <div class="signin-info">
            <uc1:intro1 runat="server" ID="intro1" />
            <uc2:intro2 runat="server" ID="intro2" />
        </div>
        <div class="signin-form">
            <div id="signin-form_id">
                <div class="signin-text">
                    <span>Subscription Code</span>
                </div>
                <div id="divExpired" runat="server">
                    <div class="alert alert-danger">
                        Your Subscription had Expired on the
                        <asp:Literal ID="ltrExpDate" runat="server"></asp:Literal>. Please Contact the System&#39;s Administrator to obtain a New Subscription Code.
                    </div>
                </div>
                <div id="divNotExpired" runat="server">
                    <div class="alert alert-success">
                        <strong>Information !</strong> You have a valid Subscription to use this application until
                        <asp:Label ID="ltrExpDate2" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="form-group w-icon">
                    <asp:TextBox ID="txtSubscriptionCode" runat="server" CssClass="form-control input-lg" placeholder="Subscription Code"></asp:TextBox>
                    <span class="fa fa-user signin-form-icon"></span>
                </div>
                <div id="divButtons" runat="server" class="signin-with text-right" style="margin-top: 17px">
                    <asp:Button ID="cmdSubmit" runat="server" Text="Register Subscription Code" CssClass="btn btn-primary" OnClick="cmdSubmit_Click" />
                    <asp:Button ID="cmdLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="cmdLogin_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contScripts" runat="Server">
</asp:Content>