<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin.master" AutoEventWireup="true" CodeBehind="direct_db.aspx.cs" Inherits="N_Ter_Tasks.direct_db" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Direct DB Access
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Direct DB Access
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <label>SQL Statement</label>
                <asp:TextBox ID="txtStatement" runat="server" CssClass="form-control" TextMode="MultiLine" Height="200"></asp:TextBox>
            </div>
            <div class="form-group text-right">
                <asp:Button ID="cmdExecute" runat="server" Text="Execute" CssClass="btn btn-primary" OnClick="cmdExecute_Click" />
                <asp:Button ID="cmdReset" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="cmdReset_Click" />
            </div>
            <div id="divQuery" runat="server" class="form-group">
                <label>Results</label>
                <div class="table-primary no-margin-b" style="overflow-y: scroll">
                    <asp:GridView ID="gvMain" runat="server" CssClass="table table-striped table-hover grid_table grid_wf_categories non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="true"></asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
</asp:Content>