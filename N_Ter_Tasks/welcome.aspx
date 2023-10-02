<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin.master" AutoEventWireup="true" CodeBehind="welcome.aspx.cs" Inherits="N_Ter_Tasks.welcome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter Tasks > Welcome to N-Ter
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    <span id="spnGreeting"></span>
    <asp:Literal ID="ltrName" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    Following is a <b>Summary</b> of your <b>Tasks</b>, <b>Messages</b> and <b>Notices</b>.
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-info">
                        <div class="panel-body widget-messages">
                            <asp:Button ID="cmdAllTasks" runat="server" Text="View Active Tasks" CssClass="btn btn-primary btn-sm pull-right" OnClick="cmdAllTasks_Click" />
                            <div class="panel-title-nter">Active Tasks</div>
                            <div class="border-t panel-info" style="height: 20px"></div>
                            <div id="divNoTasks" runat="server" class="alert alert-success mb">
                                <strong>Information !</strong> There are no Tasks available for you today.
                            </div>
                            <div id="ltrTasks" runat="server"></div>
                        </div>
                        <div class="panel-footer clearfix">
                            <div class="row">
                                <div class="col-md-4">
                                    <span class="bg-primary pl5 pr10 mr5"></span>- All Tasks
                                </div>
                                <div class="col-md-4">
                                    <span class="bg-danger pl5 pr10 mr5"></span>- Delayed Tasks
                                </div>
                                <div class="col-md-4">
                                    <span class="bg-warning pl5 pr10 mr5"></span>- You have Locked
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="panel panel-info">
                        <div class="panel-body widget-messages">
                            <asp:Button ID="cmdAllInactiveTasks" runat="server" Text="View Inactive Tasks" CssClass="btn btn-primary btn-sm pull-right" OnClick="cmdAllInactiveTasks_Click" />
                            <div class="panel-title-nter">Inactive Tasks</div>
                            <div class="border-t panel-info" style="height: 20px"></div>
                            <div id="divNoInactiveTasks" runat="server" class="alert alert-success mb">
                                <strong>Information !</strong> There are no Inactive Tasks available for you today.
                            </div>
                            <div id="ltrInactiveTasks" runat="server"></div>
                        </div>
                        <div class="panel-footer clearfix">
                            <div class="row">
                                <div class="col-md-4">
                                    <span class="bg-primary pl5 pr10 mr5"></span>- All Tasks
                                </div>
                                <div class="col-md-4">
                                    <span class="bg-danger pl5 pr10 mr5"></span>- Delayed Tasks
                                </div>
                                <div class="col-md-4">
                                    <span class="bg-warning pl5 pr10 mr5"></span>- You have Locked
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-info">
                        <div class="panel-body widget-messages-alt">
                            <asp:Button ID="cmdAllMessages" runat="server" Text="View Messages" CssClass="btn btn-primary btn-sm pull-right" OnClick="cmdAllMessages_Click" />
                            <div class="panel-title-nter">Messages</div>
                            <div class="border-t panel-info" style="height: 20px"></div>
                            <div id="divNoMessages" runat="server" class="alert alert-success mb">
                                <strong>Information !</strong> There are no Messages available for you today.
                            </div>
                            <div class="messages-list">
                                <div id="ltrMessages" runat="server"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="panel panel-info">
                        <div class="panel-body widget-messages-alt">
                            <asp:Button ID="cmdWall" runat="server" Text="View Wall" CssClass="btn btn-primary btn-sm pull-right" OnClick="cmdWall_Click" />
                            <div class="panel-title-nter">Notices</div>
                            <div class="border-t panel-info" style="height: 20px"></div>
                            <div id="divNoWall" runat="server" class="alert alert-success mb">
                                <strong>Information !</strong> There are no Notices available for you today.
                            </div>
                            <div id="ltrNotices" runat="server"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script type="text/javascript">
        init.push(function () {
            $('#spnGreeting').html(GetGreeting());
            setInterval(function () {
                GetMainDetails();
            }, <%=RefreshFreq%>);
        });

        function GetMainDetails() {
            $('#spnGreeting').html(GetGreeting());
            $.ajax({
                type: "GET",
                url: "api/tasks/GetMySummary",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassed(result) {
            var resultObject = result;
            if (resultObject[0].Item_Name.trim() == "") {
                $('#<%=divNoTasks.ClientID%>').removeClass('hide');
                $('#<%=ltrTasks.ClientID%>').html("");
            }
            else {
                $('#<%=divNoTasks.ClientID%>').addClass('hide');
                $('#<%=ltrTasks.ClientID%>').html(resultObject[0].Item_Name);
            }

            if (resultObject[1].Item_Name.trim() == "") {
                $('#<%=divNoInactiveTasks.ClientID%>').removeClass('hide');
                $('#<%=ltrInactiveTasks.ClientID%>').html("");
            }
            else {
                $('#<%=divNoInactiveTasks.ClientID%>').addClass('hide');
                $('#<%=ltrInactiveTasks.ClientID%>').html(resultObject[1].Item_Name);
            }

            if (resultObject[2].Item_Name.trim() == "") {
                $('#<%=divNoMessages.ClientID%>').removeClass('hide');
                $('#<%=ltrMessages.ClientID%>').html("");
            }
            else {
                $('#<%=divNoMessages.ClientID%>').addClass('hide');
                $('#<%=ltrMessages.ClientID%>').html(resultObject[2].Item_Name);
            }

            if (resultObject[3].Item_Name.trim() == "") {
                $('#<%=divNoWall.ClientID%>').removeClass('hide');
                $('#<%=ltrNotices.ClientID%>').html("");
            }
            else {
                $('#<%=divNoWall.ClientID%>').addClass('hide');
                $('#<%=ltrNotices.ClientID%>').html(resultObject[3].Item_Name);
            }
        }
    </script>
</asp:Content>