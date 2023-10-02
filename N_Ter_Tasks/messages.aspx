<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid.master" AutoEventWireup="true" CodeBehind="messages.aspx.cs" Inherits="N_Ter_Tasks.messages" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Messages
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Messages
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <asp:HiddenField ID="hndMID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hndTID" runat="server" ClientIDMode="Static" />
    <div class="row">
        <div class="col-md-3 animated fadeIn">
            <div class="panel widget-messages">
                <div class="panel-heading">
                    <span class="panel-title">Users</span>
                </div>
                <div class="panel-body no-padding-b no-padding-t">
                    <asp:Literal ID="ltrUsersList" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
        <div class="col-md-9 animated fadeIn">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <button id="cmdNewMessage" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>Message</button>
                    <asp:ModalPopupExtender ID="cmdNewMessage_ModalPopupExtender" runat="server" BehaviorID="mpuMessage" BackgroundCssClass="at_modelpopup_background" CancelControlID="cmdCloseChat" Enabled="True" PopupControlID="pnlChat" TargetControlID="cmdNewMessage">
                    </asp:ModalPopupExtender>
                </div>
                <div class="panel-body">
                    <div class="alert alert-success active" runat="server" id="divNoRecords">
                        There are no Messages available from the selected Member.
                    </div>
                    <asp:Literal ID="ltrThreads" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlChat" runat="server" CssClass="at_modelpopup_container" Style="display: none" DefaultButton="cmdDefaultEnter">
        <div id="at_model_indent" class="at_modelpopup_indent">
            <div id="at_model_inner_indent" class="panel panel-widget chat-widget at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseChat" runat="server" class="btn btn-default" Text="&times;" />
                    </div>
                    <span class="panel-icon">
                        <i class="panel-title-icon fa fa-comments-o"></i>
                    </span>
                    <span class="panel-title">Message View</span>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div id="oldMessage" class="row">
                            <h4 class="col-sm-2 control-label text-right-sm">Title:</h4>
                            <div class="col-sm-8 text-primary">
                                <h4>
                                    <asp:Label ID="lblTitle" runat="server" ClientIDMode="Static" Text=""></asp:Label></h4>
                            </div>
                            <div class="col-md-2 text-right">
                                <button id="cmdOpenTask" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-eye"></span>Open Task</button>
                            </div>
                        </div>
                        <div id="newMessage" class="row form-horizontal">
                            <div class="form-group no-margin-b">
                                <label class="col-md-2 control-label text-left">Title</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtTitle" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel no-padding widget-chat">
                        <div id="MessageThread" class="panel-body well">
                        </div>
                    </div>
                </div>
                <div class="panel-footer">
                    <div class="input-group">
                        <asp:TextBox ID="txtMessage" runat="server" class="form-control" ClientIDMode="Static" placeholder="Enter your message here..."></asp:TextBox>
                        <span class="input-group-btn">
                            <asp:Button ID="cmdSendMessage" runat="server" class="btn btn-default btn-gradient" Text="Send Message" OnClick="cmdSendMessage_Click" />
                            <asp:Button ID="cmdDefaultEnter" runat="server" CssClass="hidden" OnClientClick="return false;" />
                        </span>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script>
        $(function () {
            $(window).resize(function () {
                ArrangePopups();
            });
        });

        init.push(function () {
            ArrangeMessageLoad();
            ArrangePopups();
            ArrangeGrids();
            <%=Start_Script%>
        });

        function ArrangePopups() {
            AdjustPopupSize(167, 1200, 'at_model');
            AdjustMessagePopup();
        }

        function ArrangeGrids() {
            $('#message_table').dataTable({
                "pageLength": 50,
                "order": [[2, 'asc']],
                "responsive": true,
                "autoWidth": true,
                "language": {
                    "paginate": {
                        "previous": '<i class="fa fa-angle-left"></i>',
                        "next": '<i class="fa fa-angle-right"></i>'
                    }
                },
                "columnDefs": [
					   { 'orderable': false, targets: 1 },
                       { type: 'de_date', targets: 2 }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });

            $(".dataTables_filter").children().children('input').attr('placeholder', 'Search...');
        }

        function ArrangeMessageLoad() {
            $(".clickable-row").click(function () {
                $find('mpuMessage').show();
                LoadMessage($(this).data("href"));
            });
        }

        function LoadMessage(thread_id) {
            $("#hndTID").val(thread_id);
            $("#txtMessage").val("");
            if (thread_id == 0) {
                $("#newMessage").removeClass("hide");
                $("#oldMessage").addClass("hide");
                $("#txtTitle").val('');
                $("#MessageThread").html('');
                AdjustMessagePopup();
            }
            else {
                $("#newMessage").addClass("hide");
                $("#oldMessage").removeClass("hide");
                $("#lblTitle").html('');
                $("#MessageThread").html('');

                $.ajax({
                    type: "GET",
                    url: "api/tasks/GetMessageThread",
                    data: { Thread_ID: thread_id },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: LoadValauesPassed,
                    failure: LoadValuesFailed
                });
            }
        }

        function LoadValauesPassed(result) {
            var resultObject = result;
            $("#lblTitle").html(resultObject[0].Title);
            if (resultObject[0].Task_ID == 0) {
                $("#<%=cmdOpenTask.ClientID%>").addClass('hide');
            }
            else {
                $("#<%=cmdOpenTask.ClientID%>").removeClass('hide');
                $("#<%=cmdOpenTask.ClientID%>").click(function () {
                    openPage('task_info.aspx?tid=' + resultObject[0].Task_ID);
                    return false;
                });
            }
            var resultObjectSub = resultObject[0].Messages;
            var mediaString = "";
            for (var i = 0; i < resultObjectSub.length; i++) {
                if ($("#hndMID").val() == resultObjectSub[i].User_ID) {
                    mediaString += "<div class=\"message\">" +
									"<img src=\"" + resultObjectSub[i].Image_Path + "\" alt=\"\" class=\"message-avatar\">" +
									"<div class=\"message-body\">" +
										"<div class=\"message-heading\">" +
											"<a href=\"#\" title=\"\">" + resultObjectSub[i].Full_Name + "</a> says:" +
													"<span class=\"pull-right\">" + resultObjectSub[i].Posted_Date_Time + "" +
										"</div>" +
											"<div class=\"message-text\">" + resultObjectSub[i].Message + "</div>" +
										"</div>" +
									 "</div>";
                }
                else {
                    mediaString += "<div class=\"message right\">" +
									"<img src=\"" + resultObjectSub[i].Image_Path + "\" alt=\"\" class=\"message-avatar\">" +
									"<div class=\"message-body\">" +
										"<div class=\"message-heading\">" +
											"<a href=\"#\" title=\"\">" + resultObjectSub[i].Full_Name + "</a> says:" +
													"<span class=\"pull-right\">" + resultObjectSub[i].Posted_Date_Time + "" +
										"</div>" +
											"<div class=\"message-text\">" + resultObjectSub[i].Message + "</div>" +
										"</div>" +
									 "</div>";
                }
            }
            $("#MessageThread").html(mediaString);
            AdjustMessagePopup();
        }

        function AdjustMessagePopup() {
            var h = $(window).height();
            if ($("#hndTID").val() == '0') {
                $("#MessageThread").css('min-height', h - 211);
            }
            else {
                $("#MessageThread").css('min-height', h - 196);
            }
        }
    </script>
</asp:Content>