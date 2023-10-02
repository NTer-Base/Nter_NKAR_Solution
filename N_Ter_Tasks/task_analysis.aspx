<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid.master" AutoEventWireup="true" CodeBehind="task_analysis.aspx.cs" Inherits="N_Ter_Tasks.task_analysis" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Task Timeline Analysis
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Task Timeline Analysis
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="panel panel-info">
        <div class="panel-body">
            <div class="panel-title-nter">Task Filter</div>
            <div class="border-t panel-info" style="height: 20px"></div>
            <div class="input-group">
                <asp:TextBox ID="txtTaskNo" runat="server" CssClass="form-control" placeholder="Task Number"></asp:TextBox>
                <span class="input-group-btn">
                    <asp:Button ID="cmdShow" runat="server" Text="Show Timeline" CssClass="btn btn-primary" OnClick="cmdShow_Click" />
                </span>
            </div>
        </div>
    </div>
    <div id="divTimeline" runat="server" class="panel panel-info">
        <div class="panel-body">
            <div class="panel-title-nter">Task Timeline</div>
            <div class="border-t panel-info" style="height: 20px"></div>
            <div class="table-responsive table-primary no-margin-b">
                <table class="table">
                    <asp:Literal ID="ltrMainDetails" runat="server"></asp:Literal>
                </table>
            </div>
            <div class="table-responsive table-primary no-margin-b">
                <asp:GridView ID="gvMain" runat="server" CssClass="table table-striped table-hover grid_table non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Step_Name" HeaderText="Workflow Step" />
                        <asp:BoundField DataField="Event_Desctiption" HeaderText="Event Descriptiom" />
                        <asp:BoundField DataField="Event_Date" HeaderText="Date" />
                        <asp:BoundField DataField="Full_Name" HeaderText="User" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script type="text/javascript">
        function ValidateTaskNo() {
            if ($("#<%=txtTaskNo.ClientID%>").val().trim() == "") {
                ShowError('Task Number is Required');
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</asp:Content>