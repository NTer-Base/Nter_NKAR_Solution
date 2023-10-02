<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="N_Ter_Tasks.login" %>

<%@ Register Src="~/controls_customizable/intro_section_1.ascx" TagPrefix="ucSign1" TagName="intro1" %>
<%@ Register Src="~/controls_customizable/intro_section_2.ascx" TagPrefix="ucSign2" TagName="intro2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter Tasks > Login
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
    <link href="assets/stylesheets/pages.min.css" rel="stylesheet" type="text/css" />
    <link href="assets_customizable/stylesheets/customizable.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contBody" runat="Server">
    <div id="page-signin-bg">
        <div class="overlay"></div>
        <img src="assets/images/signin-bg-1.jpg" alt="" />
    </div>
    <div class="signin-container">
        <div class="signin-info">
            <ucSign1:intro1 runat="server" ID="intro1" />
            <ucSign2:intro2 runat="server" ID="intro2" />
        </div>
        <div class="signin-form">
            <div id="signin-form_id">
                <div class="signin-text">
                    <span>Sign In to your account</span>
                </div>
                <div class="form-group w-icon">
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control input-lg" placeholder="Username"></asp:TextBox>
                    <span class="fa fa-user signin-form-icon"></span>
                </div>
                <div class="form-group w-icon">
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control input-lg" placeholder="Password" TextMode="Password"></asp:TextBox>
                    <span class="fa fa-lock signin-form-icon"></span>
                </div>
                <div class="form-group">
                    <table>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkRememberMe" runat="server" CssClass="switcher-primary" Checked="true" /></td>
                            <td class="padding-sm-hr">Remember Me</td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="signin-with text-right" style="margin-top: 24px">
                <asp:Button ID="cmdSubmit" runat="server" Text="Sign In to N-Ter" CssClass="btn btn-primary" OnClick="cmdSubmit_Click" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contScripts" runat="Server">
    <script type="text/javascript">
        $(function () {
            $('#<%=chkRememberMe.ClientID%>').switcher({
                theme: 'square',
                on_state_content: '<span class="fa fa-check"></span>',
                off_state_content: '<span class="fa fa-times"></span>'
            });
        });
    </script>
</asp:Content>