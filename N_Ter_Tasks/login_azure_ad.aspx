<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base.Master" AutoEventWireup="true" CodeBehind="login_azure_ad.aspx.cs" Inherits="N_Ter_Tasks.login_azure_ad" %>

<%@ Register Src="~/controls_customizable/intro_section_1.ascx" TagPrefix="ucLogin1" TagName="intro1" %>
<%@ Register Src="~/controls_customizable/intro_section_2.ascx" TagPrefix="ucLogin2" TagName="intro2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" Runat="Server">
    N-Ter Tasks > Login
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" Runat="Server">
    <link href="assets/stylesheets/pages.min.css" rel="stylesheet" type="text/css" />
	<link href="assets_customizable/stylesheets/customizable.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contBody" Runat="Server">
    <div id="page-signin-bg">
		<div class="overlay"></div>
		<img src="assets/images/signin-bg-1.jpg" alt="" />
	</div>
	<div class="signin-container">
		<div class="signin-info">
			<ucLogin1:intro1 runat="server" ID="intro1" />
            <ucLogin2:intro2 runat="server" ID="intro2" />
		</div>
		<div class="signin-form">
			<div id="signin-form_id" style="height:180px">
				<div class="signin-text">
					<span>Sign in with your Azure Account</span>
				</div>
                <div class="text-center">
                    Your <b>Organization Credentials</b> are not <b>Registered</b> with us, Please <b>contact</b> the <b>System Administrator</b>
                </div>
			</div>
			<div class="signin-with text-right">
				<asp:Button ID="cmdSubmit" runat="server" Text="Retry Login" CssClass="btn btn-primary" OnClick="cmdSubmit_Click" />
			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contScripts" Runat="Server">
</asp:Content>