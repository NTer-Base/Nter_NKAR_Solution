<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid.master" AutoEventWireup="true" CodeBehind="settings.aspx.cs" Inherits="N_Ter_Tasks.settings" ValidateRequest="false" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Settings
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Settings
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="panel panel-info">
        <div class="form-horizontal">
            <div class="panel-body">
                <div class="tab-base">
                    <ul class="nav nav-tabs nav-tabs-simple">
                        <li id="tab1" class="active">
                            <a data-toggle='tab' href='#tab_cont1'>Main Info</a>
                        </li>
                        <li id="tab2">
                            <a data-toggle='tab' href='#tab_cont2'>Special User Groups</a>
                        </li>
                        <li id="tab7">
                            <a data-toggle='tab' href='#tab_cont7'>Logins</a>
                        </li>
                        <li id="tab3">
                            <a data-toggle='tab' href='#tab_cont3'>Structure</a>
                        </li>
                        <li id="tab6">
                            <a data-toggle='tab' href='#tab_cont6'>Pages</a>
                        </li>
                        <li id="tab8">
                            <a data-toggle='tab' href='#tab_cont8'>Guests</a>
                        </li>
                        <li id="tab4">
                            <a data-toggle='tab' href='#tab_cont4'>Email</a>
                        </li>
                        <li id="tab5">
                            <a data-toggle='tab' href='#tab_cont5'>Other</a>
                        </li>
                    </ul>
                    <div class="panel panel-info no-margin-b">
                        <div class="panel-body no-padding">
                            <div class="tab-content grid-with-paging">
                                <div id="tab_cont1" class="tab-pane fade active in">
                                    <div class="alert alert-success">
                                        Information stated here holds the ownership of the License issued by N-Ter Software (Pvt) Ltd. 
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Company Name</label>
                                        <div class="col-sm-10" style="padding-top: 7px">
                                            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Address Line 1</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtAddressLine1" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Address Line 2</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtAddressLine2" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">City</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">State</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtState" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Phone</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Fax</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtFax" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">E-Mail</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Notes</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="MultiLine" Height="150"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Company Web</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtCompanyWeb" runat="server" CssClass="form-control" placeholder="http://www.companyweb.com"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Facebook Page</label>
                                        <div class="col-sm-10">
                                            <div class="input-group">
                                                <span class="input-group-addon">https://www.facebook.com/</span>
                                                <asp:TextBox ID="txtFacebook" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Twitter Page</label>
                                        <div class="col-sm-10">
                                            <div class="input-group">
                                                <span class="input-group-addon">https://www.twitter.com/</span>
                                                <asp:TextBox ID="txtTwitter" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Linkedin Page</label>
                                        <div class="col-sm-10">
                                            <div class="input-group">
                                                <span class="input-group-addon">https://www.linkedin.com/</span>
                                                <asp:TextBox ID="txtLinkedin" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="tab_cont2" class="tab-pane fade">
                                    <div class="alert alert-success">
                                        This identifies the special privileges given to User Groups other than the privileges given provided by the User Group
                                        <ul>
                                            <li>Administrator – Shows Admin Navigation on the Main Menu, shows All Tasks in My Involvements page and enable Task Unlock feature</li>
                                            <li>Supervisor – Enable Task Unlock feature</li>
                                            <li>Guest – Enable Task Submission and Document Upload without logging in to the system if the workflow allows custom URLs</li>
                                            <li>Enable Approvels for Data Entry in User Groups - Select 2 User Groups for Data Entry and Approve</li>
                                            <li>Enable Approvels for Data Entry in Users - Select 2 User Groups for Data Entry and Approve</li>
                                        </ul>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Administrator</label>
                                        <div class="col-sm-10">
                                            <asp:DropDownList ID="cboSuperAdmin" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Supervisor</label>
                                        <div class="col-sm-10">
                                            <asp:DropDownList ID="cboSupervisorAdmin" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Guest</label>
                                        <div class="col-sm-10">
                                            <asp:DropDownList ID="cboGuest" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group pt5 mb5">
                                        <label class="col-sm-2 control-label"></label>
                                        <div class="col-sm-10">
                                            <asp:CheckBox ID="chkAllowDualUG" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Allow Data Entry Approvals on User Groups</span>
                                        </div>
                                    </div>
                                    <div id="divUGDual" runat="server" class="form-group">
                                        <label class="col-sm-2 control-label">Data Entry</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboUGDataEntry" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <label class="col-sm-2 control-label">Approve</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboUGApprove" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group pt5 mb5">
                                        <label class="col-sm-2 control-label"></label>
                                        <div class="col-sm-10">
                                            <asp:CheckBox ID="chkAllowDualUsers" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Allow Data Entry Approvals on Users</span>
                                        </div>
                                    </div>
                                    <div id="divUserDual" runat="server" class="form-group">
                                        <label class="col-sm-2 control-label">Data Entry</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboUserDataEntry" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <label class="col-sm-2 control-label">Approve</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboUserApprove" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div id="tab_cont7" class="tab-pane fade">
                                    <div class="alert alert-success">
                                        This enables a strong Password/User Locking Policy across the application except the instances where Password related options are disabled when Active Directory or Azure AD is used for User Authentication.
                                    </div>
                                    <h5 class="text-bold">Password Policy</h5>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Default Username Field</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboUsernameOption" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1">E-mail</asp:ListItem>
                                                <asp:ListItem Value="2">User Code</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-sm-3 control-label">Password Expires Automatically After</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboPWExpiry" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">[N/A]</asp:ListItem>
                                                <asp:ListItem Value="1">10 Days</asp:ListItem>
                                                <asp:ListItem Value="2">20 Days</asp:ListItem>
                                                <asp:ListItem Value="3">1 Month</asp:ListItem>
                                                <asp:ListItem Value="4">2 Month</asp:ListItem>
                                                <asp:ListItem Value="5">3 Month</asp:ListItem>
                                                <asp:ListItem Value="6">4 Month</asp:ListItem>
                                                <asp:ListItem Value="7">6 Month</asp:ListItem>
                                                <asp:ListItem Value="8">1 Year</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Minimum Number of Characters</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboPWMinChar" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">[N/A]</asp:ListItem>
                                                <asp:ListItem Value="1">1 Character</asp:ListItem>
                                                <asp:ListItem Value="2">2 Characters</asp:ListItem>
                                                <asp:ListItem Value="3">3 Characters</asp:ListItem>
                                                <asp:ListItem Value="4">4 Characters</asp:ListItem>
                                                <asp:ListItem Value="5">5 Characters</asp:ListItem>
                                                <asp:ListItem Value="6">6 Characters</asp:ListItem>
                                                <asp:ListItem Value="7">7 Characters</asp:ListItem>
                                                <asp:ListItem Value="8">8 Characters</asp:ListItem>
                                                <asp:ListItem Value="9">9 Characters</asp:ListItem>
                                                <asp:ListItem Value="10">10 Characters</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-sm-3 control-label">Minimum Number of Capital Letters</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboPWMinAlpha" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">[N/A]</asp:ListItem>
                                                <asp:ListItem Value="1">1 Character</asp:ListItem>
                                                <asp:ListItem Value="2">2 Characters</asp:ListItem>
                                                <asp:ListItem Value="3">3 Characters</asp:ListItem>
                                                <asp:ListItem Value="4">4 Characters</asp:ListItem>
                                                <asp:ListItem Value="5">5 Characters</asp:ListItem>
                                                <asp:ListItem Value="6">6 Characters</asp:ListItem>
                                                <asp:ListItem Value="7">7 Characters</asp:ListItem>
                                                <asp:ListItem Value="8">8 Characters</asp:ListItem>
                                                <asp:ListItem Value="9">9 Characters</asp:ListItem>
                                                <asp:ListItem Value="10">10 Characters</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Minimum Number of Numeric Characters</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboPWMinNum" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">[N/A]</asp:ListItem>
                                                <asp:ListItem Value="1">1 Character</asp:ListItem>
                                                <asp:ListItem Value="2">2 Characters</asp:ListItem>
                                                <asp:ListItem Value="3">3 Characters</asp:ListItem>
                                                <asp:ListItem Value="4">4 Characters</asp:ListItem>
                                                <asp:ListItem Value="5">5 Characters</asp:ListItem>
                                                <asp:ListItem Value="6">6 Characters</asp:ListItem>
                                                <asp:ListItem Value="7">7 Characters</asp:ListItem>
                                                <asp:ListItem Value="8">8 Characters</asp:ListItem>
                                                <asp:ListItem Value="9">9 Characters</asp:ListItem>
                                                <asp:ListItem Value="10">10 Characters</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-sm-3 control-label">Minimum Number of Special Characters</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboPWMinSpecial" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">[N/A]</asp:ListItem>
                                                <asp:ListItem Value="1">1 Character</asp:ListItem>
                                                <asp:ListItem Value="2">2 Characters</asp:ListItem>
                                                <asp:ListItem Value="3">3 Characters</asp:ListItem>
                                                <asp:ListItem Value="4">4 Characters</asp:ListItem>
                                                <asp:ListItem Value="5">5 Characters</asp:ListItem>
                                                <asp:ListItem Value="6">6 Characters</asp:ListItem>
                                                <asp:ListItem Value="7">7 Characters</asp:ListItem>
                                                <asp:ListItem Value="8">8 Characters</asp:ListItem>
                                                <asp:ListItem Value="9">9 Characters</asp:ListItem>
                                                <asp:ListItem Value="10">10 Characters</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <h5 class="text-bold pt5 mt15">User Account Locking Policy</h5>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Maximum User Inactive Period</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboInactiveTime" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">[N/A]</asp:ListItem>
                                                <asp:ListItem Value="1">10 Days</asp:ListItem>
                                                <asp:ListItem Value="2">20 Days</asp:ListItem>
                                                <asp:ListItem Value="3">1 Month</asp:ListItem>
                                                <asp:ListItem Value="4">2 Month</asp:ListItem>
                                                <asp:ListItem Value="5">3 Month</asp:ListItem>
                                                <asp:ListItem Value="6">4 Month</asp:ListItem>
                                                <asp:ListItem Value="7">6 Month</asp:ListItem>
                                                <asp:ListItem Value="8">1 Year</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-sm-3 control-label">Maximum Failed Login Attempts Allowed</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboFailedLogins" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">[N/A]</asp:ListItem>
                                                <asp:ListItem Value="1">1 Attempt</asp:ListItem>
                                                <asp:ListItem Value="2">2 Attempts</asp:ListItem>
                                                <asp:ListItem Value="3">3 Attempts</asp:ListItem>
                                                <asp:ListItem Value="4">4 Attempts</asp:ListItem>
                                                <asp:ListItem Value="5">5 Attempts</asp:ListItem>
                                                <asp:ListItem Value="6">6 Attempts</asp:ListItem>
                                                <asp:ListItem Value="7">7 Attempts</asp:ListItem>
                                                <asp:ListItem Value="8">8 Attempts</asp:ListItem>
                                                <asp:ListItem Value="9">9 Attempts</asp:ListItem>
                                                <asp:ListItem Value="10">10 Attempts</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label"></label>
                                        <div class="col-sm-9">
                                            <asp:CheckBox ID="chkFirstLogin" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Users must change the password on the First Login</span>
                                        </div>
                                    </div>
                                </div>
                                <div id="tab_cont3" class="tab-pane fade">
                                    <div class="alert alert-success">
                                        This identifies the complete entity structure in the application. (S: Singular, P: Plural)
                                        <br />
                                        Followng are examles,
                                        <ul>
                                            <li>Company/Department</li>
                                            <li>Company/Client</li>
                                            <li>Company/Branches</li>
                                            <li>Main Company/Subsidiaries</li>
                                        </ul>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Entity Level 1</label>
                                        <div class="col-sm-5">
                                            <div class="input-group">
                                                <span class="input-group-addon">S</span>
                                                <asp:TextBox ID="txtEL1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-5">
                                            <div class="input-group">
                                                <span class="input-group-addon">P</span>
                                                <asp:TextBox ID="txtEL1P" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Entity Level 2</label>
                                        <div class="col-sm-5">
                                            <div class="input-group">
                                                <span class="input-group-addon">S</span>
                                                <asp:TextBox ID="txtEL2" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-5">
                                            <div class="input-group">
                                                <span class="input-group-addon">P</span>
                                                <asp:TextBox ID="txtEL2P" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="tab_cont6" class="tab-pane fade">
                                    <div class="alert alert-success">
                                        Following options are identified with these settings.
                                        <ul>
                                            <li>Alternative Page for Task Start from Custom Developed Pages</li>
                                            <li>Alternative Pages that can be assigned to each Standard Page from Custom Developed Pages</li>
                                        </ul>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Alternative Start Task Page</label>
                                        <div class="col-sm-10">
                                            <asp:DropDownList ID="cboAltStart" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Alternative Pages</label>
                                        <div class="col-sm-10">
                                            <div class="table-responsive table-primary no-margin-b">
                                                <asp:GridView ID="gvPages" runat="server" CssClass="table table-striped table-hover grid_table grid_wf_categories non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvPages_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="Page_ID" />
                                                        <asp:BoundField DataField="Page_Desc_Menu" HeaderText="Page" />
                                                        <asp:TemplateField HeaderText="Alternative Page">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="cboPages" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="200px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Alternative_Page" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="tab_cont8" class="tab-pane fade">
                                    <div class="alert alert-success">
                                        Following options are identified with these settings.
                                        <ul>
                                            <li>Appearance of Header and Footer for Guest Task Post Page and Guest Document Upload Page</li>
                                            <li>Retrieve Guest Task Creation URLs</li>
                                        </ul>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 mb10 text-right no-padding-hr">
                                            <button id="cmdShare" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-link"></span>Share URLs</button>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Guest Posts Header HTML</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtHeader" runat="server" CssClass="form-control" TextMode="MultiLine" Height="150"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Guest Post Footer HTML</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtFooter" runat="server" CssClass="form-control" TextMode="MultiLine" Height="150"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div id="tab_cont4" class="tab-pane fade">
                                    <div class="alert alert-success">
                                        All Emails sent out from the application uses these settings.
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Default Sender Name</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtSenderName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Email Host</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtEmailHost" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Username</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Password</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Port Number</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtPortNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label"></label>
                                        <div class="col-sm-10">
                                            <asp:CheckBox ID="chkSSL" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">SSL Enabled</span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label"></label>
                                        <div class="col-sm-10">
                                            <asp:CheckBox ID="chkExchange" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Uses Exchange Server</span>
                                        </div>
                                    </div>
                                    <div class="panel panel-info no-margin-b">
                                        <div class="panel-body">
                                            <h5 class="mt mb20 text-semibold">Send Test E-mail (Please Save Settings before sending Test Emails)</h5>
                                            <div class="border-t panel-info" style="height: 20px"></div>
                                            <div class="form-group no-margin-b">
                                                <label class="col-sm-2 control-label">Send a Test Email to</label>
                                                <div class="col-sm-10">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtTestEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <asp:Button ID="cmdTestEmail" runat="server" Text="Send" CssClass="btn btn-primary pull-right" OnClick="cmdTestEmail_Click" />
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="tab_cont5" class="tab-pane fade">
                                    <div class="alert alert-success">
                                        Following options are identified with these settings.
                                        <ul>
                                            <li>Currency Symbol used accoss the application</li>
                                            <li>Task Time Measurement Unit</li>
                                            <li>Autimatic Refresh Rate of Task Lists and Dashboards</li>
                                            <li>Appearance of Task Lists/Task Screens</li>
                                            <li>Enable/Disable Task Post Alerts, Hourly login Messages and Help</li>
                                            <li>Set Visible Duration for System Alerts</li>
                                            <li>Enable/Disable of Automatic Reading of Uploaded Files</li>
                                            <li>Enable/Disable of Executing Data Exports as a Background Process</li>
                                            <li>Enable/Disable of Receiving Emails Alerts on Receiving Messages</li>
                                        </ul>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Currency Symbol</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtCurrency" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-2 control-label">Unit Time</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboUnit" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">[N/A]</asp:ListItem>
                                                <asp:ListItem Value="1">15 Minutes</asp:ListItem>
                                                <asp:ListItem Value="2">30 Minutes</asp:ListItem>
                                                <asp:ListItem Value="3">60 Minutes</asp:ListItem>
                                                <asp:ListItem Value="4">90 Minutes</asp:ListItem>
                                                <asp:ListItem Value="5">2 Hours</asp:ListItem>
                                                <asp:ListItem Value="6">3 Hours</asp:ListItem>
                                                <asp:ListItem Value="7">4 Hours</asp:ListItem>
                                                <asp:ListItem Value="8">5 Hours</asp:ListItem>
                                                <asp:ListItem Value="9">6 Hours</asp:ListItem>
                                                <asp:ListItem Value="10">7 Hours</asp:ListItem>
                                                <asp:ListItem Value="11">8 Hours</asp:ListItem>
                                                <asp:ListItem Value="12">9 Hours</asp:ListItem>
                                                <asp:ListItem Value="13">10 Hours</asp:ListItem>
                                                <asp:ListItem Value="14">12 Hours</asp:ListItem>
                                                <asp:ListItem Value="15">15 Hours</asp:ListItem>
                                                <asp:ListItem Value="16">20 Hours</asp:ListItem>
                                                <asp:ListItem Value="17">30 Hours</asp:ListItem>
                                                <asp:ListItem Value="18">50 Hours</asp:ListItem>
                                                <asp:ListItem Value="19">100 Hours</asp:ListItem>
                                                <asp:ListItem Value="20">200 Hours</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Default Tasks List</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboTaskList" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1">Grouped on Task Queue</asp:ListItem>
                                                <asp:ListItem Value="2">Grouped on Task Queue and Workflow Category</asp:ListItem>
                                                <asp:ListItem Value="3">Grouped on Task Queue, Workflow Category and Workflow</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-sm-2 control-label">Default Tasks Screen</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboTaskScreen" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1">Show All Panels</asp:ListItem>
                                                <asp:ListItem Value="2">Dock Documents and History Panels to Right</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Tasks List View</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboTaskListView" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">Grid View</asp:ListItem>
                                                <asp:ListItem Value="1">Card View</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-sm-2 control-label">Timeline Tasks List View</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboTaskTLListView" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">Grid View</asp:ListItem>
                                                <asp:ListItem Value="1">Card View</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Tasks List Refresh Frequency</label>
                                        <div class="col-sm-10">
                                            <asp:DropDownList ID="cboRefreshFreq" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1">10 Seconds</asp:ListItem>
                                                <asp:ListItem Value="2">30 Seconds</asp:ListItem>
                                                <asp:ListItem Value="3">1 Minute</asp:ListItem>
                                                <asp:ListItem Value="4">5 Minutes</asp:ListItem>
                                                <asp:ListItem Value="5">10 Minutes</asp:ListItem>
                                                <asp:ListItem Value="6">15 Minutes</asp:ListItem>
                                                <asp:ListItem Value="7">30 Minutes</asp:ListItem>
                                                <asp:ListItem Value="8">45 Minutes</asp:ListItem>
                                                <asp:ListItem Value="9">1 Hour</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Automatic Tasks Creation Frequency (only if you are using N-Ter Windows Service)</label>
                                        <div class="col-sm-10">
                                            <asp:DropDownList ID="cboWebServiceFreq" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1">10 Seconds</asp:ListItem>
                                                <asp:ListItem Value="2">30 Seconds</asp:ListItem>
                                                <asp:ListItem Value="3">1 Minute</asp:ListItem>
                                                <asp:ListItem Value="4">5 Minutes</asp:ListItem>
                                                <asp:ListItem Value="5">10 Minutes</asp:ListItem>
                                                <asp:ListItem Value="6">15 Minutes</asp:ListItem>
                                                <asp:ListItem Value="7">30 Minutes</asp:ListItem>
                                                <asp:ListItem Value="8">45 Minutes</asp:ListItem>
                                                <asp:ListItem Value="9">1 Hour</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Alerts Duration</label>
                                        <div class="col-sm-10">
                                            <asp:DropDownList ID="cboAlertDurtion" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1000">1 Second</asp:ListItem>
                                                <asp:ListItem Value="2000">2 Seconds</asp:ListItem>
                                                <asp:ListItem Value="3000">3 Seconds</asp:ListItem>
                                                <asp:ListItem Value="4000">4 Seconds</asp:ListItem>
                                                <asp:ListItem Value="5000">5 Seconds</asp:ListItem>
                                                <asp:ListItem Value="6000">6 Seconds</asp:ListItem>
                                                <asp:ListItem Value="7000">7 Seconds</asp:ListItem>
                                                <asp:ListItem Value="8000">8 Seconds</asp:ListItem>
                                                <asp:ListItem Value="9000">9 Seconds</asp:ListItem>
                                                <asp:ListItem Value="10000">10 Seconds</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Tasks List Default Sort</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboDefaultSort" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1">Task Created Date/Time</asp:ListItem>
                                                <asp:ListItem Value="2">Task Posted Date/Time</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-sm-2 control-label">Sort Direction</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboSortDirection" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1">Ascending Order</asp:ListItem>
                                                <asp:ListItem Value="2">Descending Order</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Tasks History Sort Direction</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboHistorySort" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1">Ascending Order of Date</asp:ListItem>
                                                <asp:ListItem Value="2">Descending Order of Date</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-sm-2 control-label">Tasks Docs Sort Direction</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboDocsSort" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1">Ascending Order of Date</asp:ListItem>
                                                <asp:ListItem Value="2">Descending Order of Date</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label"></label>
                                        <div class="col-sm-10">
                                            <asp:CheckBox ID="chkShowHelp" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Show Page Help</span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label"></label>
                                        <div class="col-sm-10">
                                            <asp:CheckBox ID="chkEnableAlerts" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Enable Task Alerts</span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label"></label>
                                        <div class="col-sm-10">
                                            <asp:CheckBox ID="chkHourlyMsg" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Enable Hourly Login Messages</span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label"></label>
                                        <div class="col-sm-10">
                                            <asp:CheckBox ID="chkEnableReading" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Enable Reading of Upload Content</span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label"></label>
                                        <div class="col-sm-10">
                                            <asp:CheckBox ID="chkDataExtractBGProcess" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Execute Data Export as a Background Process</span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label"></label>
                                        <div class="col-sm-10">
                                            <asp:CheckBox ID="chkMessageAlerts" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Send Emails Alerts on Receiving Messages</span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Software Version</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtVersion" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel-footer">
            <asp:Button ID="cmdReset" runat="server" Text="Reset" CssClass="btn btn-default" OnClick="cmdReset_Click" />
            <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="btn btn-primary pull-right" OnClick="cmdSave_Click" />
        </div>
    </div>
    <asp:ModalPopupExtender ID="cmdShare_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlHelp" BehaviorID="mpuHelp" TargetControlID="cmdShare" CancelControlID="cmdCancelHelp" BackgroundCssClass="at_modelpopup_background_2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlHelp" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_help_indent" class="at_modelpopup_indent">
            <div id="at_model_help_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 id="help_title" class="panel-title">Guest Task Creation URLs</h4>
                </div>
                <div id="at_model_help_content" class="at_modelpopup_content styled-bar">
                    <div id="divHelpContent" runat="server" class="panel-body"></div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCancelHelp" runat="server" Text="Close" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndEL2_URLs" runat="server" />
    <asp:ModalPopupExtender ID="hndEL2_URLs_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlEL2_URLs" BehaviorID="mpuEL2_URLs" TargetControlID="hndEL2_URLs" CancelControlID="cmdEL2_URLsClose" BackgroundCssClass="at_modelpopup_background_3">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlEL2_URLs" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_help2_indent" class="at_modelpopup_indent">
            <div id="at_model_help2_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Guest Task Creation URLs (<asp:Literal ID="ltrEL2" runat="server"></asp:Literal> Specific)</h4>
                </div>
                <div id="at_model_help2_content" class="at_modelpopup_content styled-bar">
                    <div id="divHelpContent2" class="panel-body"></div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdEL2_URLsClose" runat="server" Text="Close" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script type="text/javascript">
        init.push(function () {
            AdjustPopups();
            $('.nav-tabs-simple').tabdrop();
        });

        $(function () {
            $(window).resize(function () {
                AdjustPopups();
            });
        });

        function AdjustPopups() {
            AdjustPopupSize(80, 800, 'at_model_help');
            AdjustPopupSize(80, 800, 'at_model_help2');
        }

        function CheckUGDual() {
            if ($("#<%=chkAllowDualUG.ClientID%>").prop("checked") == false) {
                $("#<%=cboUGDataEntry.ClientID%>").val('0');
                $("#<%=cboUGApprove.ClientID%>").val('0');
                $("#<%=divUGDual.ClientID%>").addClass('hide');
            }
            else {
                $("#<%=divUGDual.ClientID%>").removeClass('hide');
            }
        }

        function CheckUserDual() {
            if ($("#<%=chkAllowDualUsers.ClientID%>").prop("checked") == false) {
                $("#<%=cboUserDataEntry.ClientID%>").val('0');
                $("#<%=cboUserApprove.ClientID%>").val('0');
                $("#<%=divUserDual.ClientID%>").addClass('hide');
            }
            else {
                $("#<%=divUserDual.ClientID%>").removeClass('hide');
            }
        }

        function OpenEL2_URLs(wf_id) {
            $find("mpuEL2_URLs").show();
            $('#divHelpContent2').html('');
            $.ajax({
                type: "GET",
                url: "api/tasks/GetWorklFlowEL2URLs",
                data: { Workflow_ID: wf_id, URL: '<%=Base_URL%>' },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassedAddon,
                failure: LoadValuesFailed
            });
            return false;
        }

        function LoadValauesPassedAddon(result) {
            var resultObject = result;
            $('#divHelpContent2').html(resultObject);
        }
    </script>
</asp:Content>