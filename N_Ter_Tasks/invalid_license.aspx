<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base.Master" AutoEventWireup="true" CodeBehind="invalid_license.aspx.cs" Inherits="N_Ter_Tasks.invalid_license" %>

<%@ Register Src="~/controls_customizable/intro_section_1.ascx" TagPrefix="uc1" TagName="intro1" %>
<%@ Register Src="~/controls_customizable/intro_section_2.ascx" TagPrefix="uc2" TagName="intro2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter Tasks > Invalid License
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
                    <span>Authentication Code</span>
                </div>
                <div class="alert alert-danger">
                    <strong>Error !</strong> Your License File is invalid. Please obtain a New License File by sending the Authentication code to the System&#39;s Administrator of N-Ter
                </div>
                <div class="form-group w-icon">
                    <asp:TextBox ID="txtAuthCode" runat="server" CssClass="form-control input-lg" placeholder="Authentication Code"></asp:TextBox>
                    <span class="fa fa-user signin-form-icon"></span>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contScripts" runat="Server">
</asp:Content>