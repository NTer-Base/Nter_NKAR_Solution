<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid.master" AutoEventWireup="true" CodeBehind="dp_search.aspx.cs" Inherits="N_Ter_Tasks.dp_search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="server">
    N-Ter-Tasks > Search in Document Repo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="server">
    Search in Document Repo :
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="server">
    <div class="row">
        <div class="col-md-3">
            <div class="form-group">
                <label>
                    <asp:Literal ID="ltrEL2" runat="server"></asp:Literal>
                    Name</label>
                <asp:DropDownList ID="cboEL2Name" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboEL2Name_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <h5 class="text-semibold">Repository</h5>
            <asp:HiddenField ID="hndDoc_Proj_ID" runat="server" />
            <div class="list-group">
                <asp:Literal ID="ltrDocProjects" runat="server"></asp:Literal>
            </div>
        </div>
        <div class="col-md-9">
            <asp:Panel ID="pnlDP" runat="server">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel panel-info">
                            <div class="panel-body">
                                <div class="panel-title-nter">Document Filter Options</div>
                                <div class="border-t panel-info" style="height: 20px"></div>
                                <div class="row padding-xs-vr">
                                    <div class="col-md-3">
                                        Document Content
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="cboDocOperator" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">[Any]</asp:ListItem>
                                            <asp:ListItem Value="1">Starts with</asp:ListItem>
                                            <asp:ListItem Value="2">Ends with</asp:ListItem>
                                            <asp:ListItem Value="3">Contains</asp:ListItem>
                                            <asp:ListItem Value="4">Not Contain</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtDocCriteria" runat="server" TextMode="MultiLine" Height="75" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div id="divDocFilters" runat="server"></div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="cmdFilter" runat="server" Text="Submit" class="btn btn-primary" OnClick="cmdFilter_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel panel-info">
                            <div class="panel-body">
                                <div class="panel-title-nter">Filtered Documents</div>
                                <div class="border-t panel-info" style="height: 20px"></div>
                                <asp:HiddenField ID="hndSelectedDocs" ClientIDMode="Static" runat="server" />
                                <div id="divDocGrid" runat="server" class="table-responsive table-primary no-margin-b">
                                    <%--<asp:GridView ID="gvDocs" runat="server" CssClass="table table-striped table-hover grid_table grid_docs non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvDocs_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Document_ID" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxlist chkSelected" />
                                                </ItemTemplate>
                                                <ItemStyle Width="22px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Document_No" HeaderText="Doc Number" />
                                            <asp:BoundField DataField="Creator_Name" HeaderText="Uploaded By" />
                                            <asp:BoundField DataField="Created_Date" HeaderText="Date Uploaded">
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Matching_Percentage_SP" HeaderText="">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="30px" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <button id='cmdView' type='submit' runat="server" class="btn btn-primary btn-xs" title="View Document"><span class="btn-label button_icon fa fa-eye"></span></button>
                                                </ItemTemplate>
                                                <ItemStyle Width="22px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <button id='cmdIfo' type='submit' runat="server" class="btn btn-primary btn-xs" title="View Tags"><span class="btn-label button_icon fa fa-info"></span></button>
                                                </ItemTemplate>
                                                <ItemStyle Width="22px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>--%>
                                </div>
                                <div id="divNoDocs" runat="server" class="alert alert-danger alert-dark">
                                    <strong>No Documents</strong> attached to this task to show.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="server">
</asp:Content>
