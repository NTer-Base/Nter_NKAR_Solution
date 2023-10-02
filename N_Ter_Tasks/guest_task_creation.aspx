<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_guest.master" AutoEventWireup="true" CodeBehind="guest_task_creation.aspx.cs" Inherits="N_Ter_Tasks.guest_task_creation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter > Guest Task Creation
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contBody" runat="Server">
    <div class="row no-margin-hr">
        <div class="col-lg-12">
            <div id="altHelp" runat="server" class="alert alert-success">
                <asp:Literal ID="ltrGuestHelp" runat="server"></asp:Literal>
            </div>
            <div id="divMaxExceeded" runat="server" class="panel panel-info">
                <div class="panel-body">
                    <div class="panel-title-nter">Job Creation (<asp:Literal ID="ltrWFName2" runat="server"></asp:Literal>)</div>
                    Maximum number of Tasks that can be created is Exceeded. Please try again later.
                </div>
            </div>
            <div id="divAllowed" runat="server" class="panel panel-info">
                <div class="panel-body">
                    <div class="panel-title-nter">Job Creation (<asp:Literal ID="ltrWFName" runat="server"></asp:Literal>)</div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <div id="divEL2" runat="server" class="form-group">
                        <label>
                            <asp:Literal ID="ltrEL2" runat="server"></asp:Literal></label>
                        <asp:DropDownList ID="cboEL2" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div id="divExtraField1" runat="server" class="form-group">
                        <label>
                            <asp:Literal ID="ltrExtraField1" runat="server"></asp:Literal></label>
                        <asp:TextBox ID="txtExtraField1" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:DropDownList ID="cboExtraField1" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div id="divExtraField2" runat="server" class="form-group">
                        <label>
                            <asp:Literal ID="ltrExtraField2" runat="server"></asp:Literal></label>
                        <asp:TextBox ID="txtExtraField2" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:DropDownList ID="cboExtraField2" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div id="divSchedule" runat="server" class="form-group">
                        <label>
                            <asp:Literal ID="ltrScheduleName" runat="server"></asp:Literal></label>
                        <asp:DropDownList ID="cboScheduleName" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div id="divDueDate" runat="server" class="form-group">
                        <label><asp:Literal ID="ltrDueDate" runat="server"></asp:Literal></label>
                        <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control dtPicker"></asp:TextBox>
                    </div>
                    <div id="divDueTime" runat="server" class="form-group">
                        <label><asp:Literal ID="ltrDueTime" runat="server"></asp:Literal></label>
                        <asp:DropDownList ID="cboDueTime" runat="server" CssClass="form-control">
                            <asp:ListItem Value="00">00:00</asp:ListItem>
                            <asp:ListItem Value="01">00:30</asp:ListItem>
                            <asp:ListItem Value="10">01:00</asp:ListItem>
                            <asp:ListItem Value="11">01:30</asp:ListItem>
                            <asp:ListItem Value="20">02:00</asp:ListItem>
                            <asp:ListItem Value="21">02:30</asp:ListItem>
                            <asp:ListItem Value="30">03:00</asp:ListItem>
                            <asp:ListItem Value="31">03:30</asp:ListItem>
                            <asp:ListItem Value="40">04:00</asp:ListItem>
                            <asp:ListItem Value="41">04:30</asp:ListItem>
                            <asp:ListItem Value="50">05:00</asp:ListItem>
                            <asp:ListItem Value="51">05:30</asp:ListItem>
                            <asp:ListItem Value="60">06:00</asp:ListItem>
                            <asp:ListItem Value="61">06:30</asp:ListItem>
                            <asp:ListItem Value="70">07:00</asp:ListItem>
                            <asp:ListItem Value="71">07:30</asp:ListItem>
                            <asp:ListItem Value="80">08:00</asp:ListItem>
                            <asp:ListItem Value="81">08:30</asp:ListItem>
                            <asp:ListItem Value="90">09:00</asp:ListItem>
                            <asp:ListItem Value="91">09:30</asp:ListItem>
                            <asp:ListItem Value="100">10:00</asp:ListItem>
                            <asp:ListItem Value="101">10:30</asp:ListItem>
                            <asp:ListItem Value="110">11:00</asp:ListItem>
                            <asp:ListItem Value="111">11:30</asp:ListItem>
                            <asp:ListItem Value="120">12:00</asp:ListItem>
                            <asp:ListItem Value="121">12:30</asp:ListItem>
                            <asp:ListItem Value="130">13:00</asp:ListItem>
                            <asp:ListItem Value="131">13:30</asp:ListItem>
                            <asp:ListItem Value="140">14:00</asp:ListItem>
                            <asp:ListItem Value="141">14:30</asp:ListItem>
                            <asp:ListItem Value="150">15:00</asp:ListItem>
                            <asp:ListItem Value="151">15:30</asp:ListItem>
                            <asp:ListItem Value="160">16:00</asp:ListItem>
                            <asp:ListItem Value="161">16:30</asp:ListItem>
                            <asp:ListItem Value="170">17:00</asp:ListItem>
                            <asp:ListItem Value="171">17:30</asp:ListItem>
                            <asp:ListItem Value="180">18:00</asp:ListItem>
                            <asp:ListItem Value="181">18:30</asp:ListItem>
                            <asp:ListItem Value="190">19:00</asp:ListItem>
                            <asp:ListItem Value="191">19:30</asp:ListItem>
                            <asp:ListItem Value="200">20:00</asp:ListItem>
                            <asp:ListItem Value="201">20:30</asp:ListItem>
                            <asp:ListItem Value="210">21:00</asp:ListItem>
                            <asp:ListItem Value="211">21:30</asp:ListItem>
                            <asp:ListItem Value="220">22:00</asp:ListItem>
                            <asp:ListItem Value="221">22:30</asp:ListItem>
                            <asp:ListItem Value="230">23:00</asp:ListItem>
                            <asp:ListItem Value="231">23:30</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div id="divTaskQueue" runat="server" class="form-group">
                        <label>
                            <asp:Literal ID="ltrTaskQueue" runat="server"></asp:Literal></label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="cboQueue"></asp:DropDownList>
                    </div>
                </div>
                <div id="divSubmit" runat="server" class="modal-footer">
                    <asp:Button ID="cmdSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="cmdSubmit_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
</asp:Content>