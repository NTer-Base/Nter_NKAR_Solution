<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid.master" AutoEventWireup="true" CodeBehind="task_info.aspx.cs" Inherits="N_Ter_Tasks.task_info" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter > Task Information
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
    <link href="assets/stylesheets/pages.min.css" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Task Information
    <asp:Label ID="lblTaskNumber" runat="server" Text="Label"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <asp:HiddenField ID="hndTaskID" runat="server" />
    <div id="divLocked" runat="server" class="alert alert-danger">
        <strong>Warning !</strong> This Task is currently locked by <b>
            <asp:Literal ID="ltrLockedUser" runat="server"></asp:Literal></b>.
    </div>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="panel_menu">
                        <button id="cmdOpenSub" runat="server" class="btn btn-labeled btn-primary">
                            <span class="btn-label icon fa fa fa-tasks"></span>
                            <asp:Literal ID="ltrSubTaskCount" runat="server"></asp:Literal></button>
                        <div id="divMainControls" runat="server" class="btn-group">
                            <button class="btn btn-labeled btn-primary dropdown-toggle" data-toggle="dropdown"><span class="btn-label icon fa fa-cogs"></span><i class="fa fa-caret-down"></i></button>
                            <ul class="dropdown-menu pull-right">
                                <li id="lstTaskScript" runat="server"><a href="#" onclick="return GetTaskScript();"><span class="btn-label menu_icon fa fa-download"></span>Get Task Script</a></li>
                            </ul>
                        </div>
                    </div>
                    <asp:ModalPopupExtender ID="SubTaskModalPopup" runat="server" Enabled="True" PopupControlID="pnlSubTasks" TargetControlID="cmdOpenSub" CancelControlID="cmdCloseTskList" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                    <div class="panel-title-nter">Task Details</div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <div class="row mt15">
                        <div class="col-sm-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Task Number</div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblTaskNo" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">
                                    <asp:Literal ID="ltrEL2" runat="server"></asp:Literal>
                                    Name
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblEL2Name" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Workflow Name</div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblWorkflow" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Current Status</div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblCurrentStatus" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Task Creator</div>
                                <div class="col-sm-5">
                                    <asp:Label ID="lblTaskCreator" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Task Queue</div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblQueue" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Task Date/Time</div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblTaskDate" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Task Due Date/Time</div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblDueDate" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div id="divExtraField" runat="server" class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">
                                    <asp:Literal ID="lblExtraFieldName" runat="server"></asp:Literal>
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblExtraFieldValue" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div id="divExtraField2" runat="server" class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">
                                    <asp:Literal ID="lblExtraField2Name" runat="server"></asp:Literal>
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblExtraField2Value" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Task Duration</div>
                                <div class="col-sm-8 text-info text-bold">
                                    <asp:Label ID="lblDuration" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div id="divTimeUnit" runat="server" class="col-sm-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Unit Allocation</div>
                                <div id="divTimeUnitNo" runat="server" class="col-sm-8 text-lg">
                                    <asp:Literal ID="ltrUnits" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="row pt3">
                                <div class="col-sm-2 text-bold text-right-md">Progress</div>
                                <div class="col-sm-10 text-info">
                                    <div class="progress mt5 mb">
                                        <div id="divProgress" runat="server" class="progress-bar" style="width: 1%"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-7">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="panel_menu">
                        <div class="btn-group">
                            <button class="btn btn-labeled btn-primary dropdown-toggle" data-toggle="dropdown"><span class="btn-label icon fa fa-cogs"></span><i class="fa fa-caret-down"></i></button>
                            <ul class="dropdown-menu pull-right">
                                <li><a href="#" onclick="return ViewComments();"><span class="btn-label menu_icon fa fa-eye"></span>View Comments</a></li>
                                <li><a href="#" onclick="return ClearCommentControls();"><span class="btn-label menu_icon fa fa-comment"></span>New Comments</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="panel-title-nter">Task History</div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <div class="timeline">
                        <asp:Literal ID="ltrHistory" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-5">
            <div class="panel panel-info">
                <div class="widget-comments panel-body long_word_wrap">
                    <div class="panel_menu">
                        <div class="btn-group">
                            <button class="btn btn-labeled btn-primary dropdown-toggle" data-toggle="dropdown"><span class="btn-label icon fa fa-cogs"></span><i class="fa fa-caret-down"></i></button>
                            <ul class="dropdown-menu pull-right">
                                <li><a href="#" onclick="return LoadDocumentsForType('1');"><span class="btn-label menu_icon fa fa-eye"></span>View Documents</a></li>
                                <li id="cmdDownload1"><a href="#" onclick="return GetFiles(false);"><span class="btn-label menu_icon fa fa-download"></span>Download All</a></li>
                                <li class="divider"></li>
                                <li><a href="#" onclick="return ClearUploadControls();"><span class="btn-label menu_icon fa fa-upload"></span>Upload Documents</a></li>
                                <li id="lnkDocLink" runat="server"><a href="#" onclick="return LinkDocument('0');"><span class="btn-label menu_icon fa fa-link"></span>Attach Documents</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="panel-title-nter">
                        Source Documents
                        <asp:Literal ID="ltrTaskDocCount" runat="server"></asp:Literal>
                    </div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <asp:Literal ID="ltrAttachments" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="widget-comments panel-body long_word_wrap">
                    <div class="panel_menu">
                        <div class="btn-group">
                            <button class="btn btn-labeled btn-primary dropdown-toggle" data-toggle="dropdown"><span class="btn-label icon fa fa-cogs"></span><i class="fa fa-caret-down"></i></button>
                            <ul class="dropdown-menu pull-right">
                                <li><a href="#" onclick="return LoadDocumentsForType('2');"><span class="btn-label menu_icon fa fa-eye"></span>View Documents</a></li>
                                <li id="cmdDownload2"><a href="#" onclick="return GetFiles(true);"><span class="btn-label menu_icon fa fa-download"></span>Download All</a></li>
                                <li class="divider"></li>
                                <li><a href="#" onclick="return ClearUploadControls2();"><span class="btn-label menu_icon fa fa-upload"></span>Upload Documents</a></li>
                                <li id="lnkDocLinkR" runat="server"><a href="#" onclick="return LinkDocument('1');"><span class="btn-label menu_icon fa fa-link"></span>Attach Documents</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="panel-title-nter">
                        Output Documents
                        <asp:Literal ID="ltrTaskDocCountR" runat="server"></asp:Literal>
                    </div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <asp:Literal ID="ltrAttachmentsR" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="panel_menu">
                        <div class="btn-group">
                            <button class="btn btn-labeled btn-primary dropdown-toggle" data-toggle="dropdown"><span class="btn-label icon fa fa-cogs"></span><i class="fa fa-caret-down"></i></button>
                            <ul class="dropdown-menu pull-right">
                                <li id="lstStepsList" runat="server"><a href="#" onclick="return ShowStepsList();"><span class="btn-label menu_icon fa fa-list-ul"></span>Steps List</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="panel-title-nter">Task Timeline</div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <div id="progress_chart" style="height: 212px"></div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="panel_menu">
                        <div class="btn-group">
                            <button class="btn btn-labeled btn-primary dropdown-toggle" data-toggle="dropdown"><span class="btn-label icon fa fa-cogs"></span><i class="fa fa-caret-down"></i></button>
                            <ul class="dropdown-menu pull-right">
                                <li><a href="#" onclick="return LoadMessage(0);"><span class="btn-label menu_icon fa fa-comments"></span>Start a New Chat</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="panel-title-nter">Task Chats</div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <asp:Literal ID="ltrThreads" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlSubTasks" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_tsklst_indent" class="at_modelpopup_indent">
            <div id="at_model_tsklst_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseTskList" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <div class="at_modelpopup_add_left">
                        <button id="subTaskReturn" class="btn btn-default" onclick="return stepOutSubStack();"><i class="fa fa-chevron-left"></i></button>
                    </div>
                    <asp:HiddenField ID="hndSubStack" runat="server" />
                    <asp:HiddenField ID="hndParentForSubs" runat="server" />                    
                    <h4 id="subTasksTitle" class="panel-title pl30">Sub Task List - <span id="spanParentTaskDetails"></span></h4>
                </div>
                <div id="at_model_tsklst_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body grid-with-paging">
                        <div class="table-responsive table-primary no-margin-b">
                            <table id="tblSubTasks" class="table table-striped table-hover color-grid grid_table grid_test full_width_table last_col_right" data-size="full_width_table">
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th>Task Creatiion Date/Time</th>
                                        <th>Task Number</th>
                                        <th>Workflow Name</th>
                                        <th></th>
                                        <th></th>
                                        <th>Current Step</th>
                                        <th>Posted Date/Time</th>
                                        <th>Posted By</th>
                                        <th>Sub Tasks</th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="cmdAddDocument" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="cmdAddDocument_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuUpload" PopupControlID="pnlUpload" TargetControlID="cmdAddDocument" CancelControlID="cmdCloseUpload" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlUpload" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_file_indent" class="at_modelpopup_indent">
            <div id="at_model_file_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseUpload" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Upload File -
						<asp:Label ID="lblRType" runat="server" Text="Label"></asp:Label></h4>
                </div>
                <div id="at_model_file_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div id="dropzonejs" class="dropzone-box form-group">
                            <div class="dz-default dz-message">
                                <i class="fa fa-cloud-upload"></i>
                                Drop files in here<br>
                                <span class="dz-text-small">or click to pick manually</span>
                            </div>
                            <div class="fallback">
                                <input name="file" type="file" multiple="multiple" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label>File Type</label>
                            <asp:DropDownList runat="server" CssClass="form-control" ID="cboFileTypesUpload"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>File Name</label>
                            <asp:TextBox ID="txtFileName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Access Level</label>
                            <asp:DropDownList ID="cboAccessLevel" runat="server" CssClass="form-control">
                                <asp:ListItem Value="3">Level 3</asp:ListItem>
                                <asp:ListItem Value="2">Level 2</asp:ListItem>
                                <asp:ListItem Value="1">Level 1</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox ID="chkReUpload" CssClass="checkboxlist" runat="server" /><span style="padding-left: 10px">This is a re-upload</span>
                        </div>
                        <asp:HiddenField ID="hndIsResult" runat="server" />
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdUploadDoc" runat="server" CssClass="btn btn-primary pull-right" Text="Upload" />
                    <asp:Button ID="cmdUploadHnd" runat="server" CssClass="btn btn-sm btn-default hide" Text="Upload Files" OnClick="cmdUploadHnd_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndMessageThreadID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hndMessageMyID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hndMessageMemberID" runat="server" ClientIDMode="Static" />
    <asp:Button ID="cmdNewChat" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="cmdNewChat_ModalPopupExtender" runat="server" BehaviorID="mpuMessage" Enabled="True" PopupControlID="pnlChat" TargetControlID="cmdNewChat" CancelControlID="cmdCloseChat" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlChat" runat="server" CssClass="at_modelpopup_container" Style="display: none" DefaultButton="cmdDefaultEnter">
        <div id="at_model_chat_indent" class="at_modelpopup_indent">
            <div id="at_model_chat_inner_indent" class="panel panel-widget chat-widget at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseChat" runat="server" class="btn btn-sm btn-default" Text="&times;" />
                    </div>
                    <span class="panel-icon">
                        <i class="panel-title-icon fa fa-comments-o"></i>
                    </span>
                    <span class="panel-title">Message View</span>
                </div>
                <div id="at_model_chat_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div id="oldMessage" class="row">
                            <div class="col-md-8">
                                <div class="row">
                                    <h4 class="col-sm-2 control-label text-right-sm">Title:</h4>
                                    <div class="col-sm-10 text-primary">
                                        <h4>
                                            <asp:Label ID="lblTitle" runat="server" ClientIDMode="Static" Text=""></asp:Label></h4>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="row">
                                    <h4 class="col-sm-2 control-label text-right-sm">To:</h4>
                                    <div class="col-sm-10 text-primary">
                                        <h4>
                                            <asp:Label ID="lblOtherUser" runat="server" ClientIDMode="Static" Text=""></asp:Label></h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="newMessage" class="row form-horizontal">
                            <div class="col-md-8">
                                <div class="form-group row no-margin-b">
                                    <label class="col-md-2 control-label text-left">Title</label>
                                    <div class="col-md-10">
                                        <asp:TextBox ID="txtTitle" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group row no-margin-b">
                                    <label class="col-md-2 control-label text-left">To</label>
                                    <div class="col-md-10">
                                        <asp:DropDownList ID="cboMessageUsers" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:DropDownList>
                                    </div>
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
    <asp:Button ID="cmdAddComment" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="cmdAddComment_ModalPopupExtender" runat="server" BehaviorID="mpuComment" Enabled="True" PopupControlID="pnlComment" TargetControlID="cmdAddComment" CancelControlID="cmdCloseComment" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlComment" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_com_indent" class="at_modelpopup_indent">
            <div id="at_model_com_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseComment" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add Comment</h4>
                </div>
                <div id="at_model_com_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Comment</label>
                            <asp:TextBox ID="txtCommentMain" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Comment Type</label>
                            <asp:DropDownList ID="cboCommentType" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Priority</label>
                            <asp:DropDownList ID="cboPriority" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Low" Value="3" />
                                <asp:ListItem Text="Medium" Value="2" />
                                <asp:ListItem Text="High" Value="1" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSaveComment" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSaveComment_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="cmdViewComments" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="cmdViewComment_ModalPopupExtender" runat="server" BehaviorID="mpuViewComment" Enabled="True" PopupControlID="pnlViewComment" TargetControlID="cmdViewComments" CancelControlID="cmdCloseViewComment" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewComment" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_vc_indent" class="at_modelpopup_indent">
            <div id="at_model_vc_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseViewComment2" runat="server" Text="&times;" class="btn btn-default" OnClientClick="return CloseCommentView();" />
                    </div>
                    <h4 class="panel-title">Comments - Categorised</h4>
                </div>
                <div id="at_model_vc_content" class="at_modelpopup_content styled-bar">
                    <div class="row panel-padding no-padding-b">
                        <div class="col-md-7"></div>
                        <div class="col-md-5">
                            <div class="form-group row no-margin-b">
                                <label class="col-md-2 control-label text-left" style="padding-top: 5px">Show : </label>
                                <div class="col-md-10">
                                    <asp:DropDownList ID="cboTags" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="widget-comments panel-body">
                        <div id="divCommentsOnly" runat="server" class="no-padding-vr">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCloseViewComment" runat="server" CssClass="btn btn-primary pull-right" Text="Close" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="cmdDocComment" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="cmdDocComment_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDocComment" BehaviorID="mpuDocComment" TargetControlID="cmdDocComment" CancelControlID="cmdCancelDocCom" BackgroundCssClass="at_modelpopup_background_2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlDocComment" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <asp:HiddenField ID="hndDocCommentID" runat="server" />
        <asp:HiddenField ID="hndIsDocument" runat="server" />
        <div id="at_model_doccom_indent" class="at_modelpopup_indent">
            <div id="at_model_doccom_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCancelDocCom" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add Document Comment</h4>
                </div>
                <div id="at_model_doccom_content" class="at_modelpopup_content styled-bar">
                    <div class="widget-comments panel-body no-padding-b">
                        <div id="divDocComments" runat="server" class="no-padding-vr">
                            text here
                        </div>
                    </div>
                    <div class="panel-body no-padding-t">
                        <div class="form-group">
                            <label>Comment</label>
                            <asp:TextBox ID="txtDocComment" runat="server" CssClass="form-control" TextMode="MultiLine" Height="60"></asp:TextBox>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Comment Type</label>
                                    <asp:DropDownList ID="cboDocCommentType" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Priority</label>
                                    <asp:DropDownList ID="cboDocCommentPriority" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Low" Value="3" />
                                        <asp:ListItem Text="Medium" Value="2" />
                                        <asp:ListItem Text="High" Value="1" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdAddDocComment" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdAddDocComment_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="cmdDocInfo" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="cmdDocInfo_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDocInfo" BehaviorID="mpuDocInfo" TargetControlID="cmdDocInfo" CancelControlID="cmdCancelDocInfo" BackgroundCssClass="at_modelpopup_background_2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlDocInfo" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <asp:HiddenField ID="hndDocument_ID" runat="server" />
        <div id="at_model_docinfo_indent" class="at_modelpopup_indent">
            <div id="at_model_docinfo_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Document Info</h4>
                </div>
                <div id="at_model_docinfo_content" class="at_modelpopup_content styled-bar">
                    <div id="divDocInfo" class="panel-body">
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCancelDocInfo" runat="server" Text="Close" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndLinkDocID" runat="server" />
    <asp:ModalPopupExtender ID="hndLinkDocID_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuAddDocLink" PopupControlID="pnlAddDocLink" TargetControlID="hndLinkDocID" CancelControlID="cmdCloseAddLink" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlAddDocLink" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_add_doc_link_indent" class="at_modelpopup_indent">
            <div id="at_model_add_doc_link_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseAddLink" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add Linked Document</h4>
                </div>
                <div id="at_model_add_doc_link_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body grid-with-paging">
                        <div class="table-responsive table-primary no-margin-b">
                            <asp:GridView ID="gvDocumentsFind" ClientIDMode="Static" runat="server" CssClass="table table-striped table-hover grid_table gvDocumentsFind" AutoGenerateColumns="False" OnRowDataBound="gvDocumentsFind_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="Document_ID" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <button id='cmdEdit' type='submit' runat="server" class="btn btn-primary btn-sm" title="Add Link"><i class="fa fa-link button_icon"></i></button>
                                        </ItemTemplate>
                                        <ItemStyle Width="22px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Document_No" HeaderText="Document No"></asp:BoundField>
                                    <asp:BoundField DataField="Created_Date" HeaderText="Date"></asp:BoundField>
                                    <asp:BoundField DataField="Display_Name" HeaderText="EL2 Name"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hndIsResultLink" runat="server" />
            </div>
        </div>
        <asp:Button ID="cmdSaveAddLinkhnd" runat="server" Text="Button" Style="display: none" OnClick="cmdSaveAddLinkhnd_Click" />
    </asp:Panel>
    <asp:HiddenField ID="hndDocView" runat="server" />
    <asp:ModalPopupExtender ID="hndDocView_ModalPopupExtender" runat="server" BehaviorID="mpuDocumentView" Enabled="True" PopupControlID="pnlDocView" TargetControlID="hndDocView" CancelControlID="cmdCloseViewDocs" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlDocView" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_vd_indent" class="at_modelpopup_indent">
            <div id="at_model_vd_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseViewDocs2" runat="server" Text="&times;" class="btn btn-default" OnClientClick="return closeDocView();" />
                    </div>
                    <h4 class="panel-title">Documents - Categorised</h4>
                </div>
                <div id="at_model_vd_content" class="at_modelpopup_content styled-bar">
                    <div class="row panel-padding no-padding-b">
                        <div class="col-md-2"></div>
                        <div class="col-md-10">
                            <div class="form-group row no-margin-b">
                                <label class="col-md-2 control-label text-right" style="padding-top: 5px">Show : </label>
                                <div class="col-md-5">
                                    <asp:DropDownList ID="cboDocType" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">[All Document Types]</asp:ListItem>
                                        <asp:ListItem Value="1">Sourse Documents</asp:ListItem>
                                        <asp:ListItem Value="2">Output Documents</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList ID="cboDocCategories" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div id="divDocView" runat="server" class="row no-padding-vr">
                            <div class="col-md-12">
                                <b>Title</b>
                            </div>
                            <div class="col-lg-2 col-md-3 col-sm-4 col-xs-6" style="padding-bottom: 10px">
                                <div class="row">
                                    <div class="col-md-12 text-center">
                                        <img src="assets/images/icons/audio.png" style="width: 80px; height: auto" />
                                    </div>
                                    <div class="col-md-12 text-center">
                                        testing text blok
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCloseViewDocs" runat="server" CssClass="btn btn-primary pull-right" Text="Close" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndStepsList" runat="server" ClientIDMode="Static" />
    <asp:ModalPopupExtender ID="hndStepsList_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuStepsList" PopupControlID="pnlStepsList" TargetControlID="hndStepsList" CancelControlID="cmdCloseStepsList" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlStepsList" runat="server" CssClass="at_modelpopup_container">
        <div id="at_model_stli_indent" class="at_modelpopup_indent">
            <div id="at_model_stli_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseStepsList" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Steps List</h4>
                </div>
                <div id="at_model_stli_content" class="at_modelpopup_content styled-bar">
                    <asp:Panel ID="pnlStepsListBody" runat="server" CssClass="panel-body">sdfsdfsfd</asp:Panel>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script type="text/javascript">
        <%=ChartScripts%>

        $(function () {
            $(window).resize(function () {
                ArrangePopups();
            });
        });

        init.push(function () {
            ArrangeChart();
            ArrangeMessageLoad();
            ArrangePopups();
            ArrangeDropZone();
            <%=LoadingScripts%>

            $('.ttip').tooltip();
        });

        function ArrangeDropZone() {
            $("#dropzonejs").dropzone({
                autoProcessQueue: false,
                paramName: "file",
                addRemoveLinks: true,
                maxFilesize: 50,
                parallelUploads: 3,
                maxFiles: 100,
                dictResponseError: 'Server not Configured',
                url: "fileupload.aspx?tid=<%=Request.QueryString["tid"]%>",
                thumbnailWidth: 138,
                thumbnailHeight: 120,
                previewTemplate: '<div class="dz-preview dz-file-preview"><div class="dz-details"><div class="dz-filename"><span data-dz-name></span></div><div class="dz-size">File size: <span data-dz-size></span></div><div class="dz-thumbnail-wrapper"><div class="dz-thumbnail"><img data-dz-thumbnail><span class="dz-nopreview">No preview</span><div class="dz-success-mark"><i class="fa fa-check-circle-o"></i></div><div class="dz-error-mark"><i class="fa fa-times-circle-o"></i></div><div class="dz-error-message"><span data-dz-errormessage></span></div></div></div></div><div class="progress progress-striped active"><div class="progress-bar progress-bar-success" data-dz-uploadprogress></div></div></div>',

                init: function () {
                    var myDropzone = this;
                    var uploadCount = 0;
                    var fileCount = 0;

                    $('#<%=cmdUploadDoc.ClientID%>').on('click', function (e) {
                        if (myDropzone.files.length > 0) {
                            $('.dz-remove').addClass('hide');
                            uploadCount = 0;
                            fileCount = myDropzone.files.length;
                            e.preventDefault();
                            e.stopPropagation();
                            myDropzone.processQueue();
                        }
                        else {
                            ShowError('No Files to Upload.');
                            return false;
                        }
                    });
                    this.on('sending', function (file, xhr, formData) {
                        formData.append('txt', $('#<%=txtFileName.ClientID%>').val());
                        formData.append('pri', $('#<%=cboAccessLevel.ClientID%>').val());
                        formData.append('typ', $('#<%=cboFileTypesUpload.ClientID%>').val());
                        formData.append('res', $('#<%=hndIsResult.ClientID%>').val());
                        formData.append('reup', $('#<%=chkReUpload.ClientID%>').prop('checked'));
                        formData.append('tot', fileCount);
                        formData.append('vst', '<%=ViewState.GetHashCode()%>');
                    });
                    this.on("success", function (files, response) {
                        uploadCount++;
                        if (uploadCount == fileCount) {
                            $("#<%=cmdUploadHnd.ClientID%>").trigger("click");
                        }
                        else if (uploadCount < fileCount) {
                            if (uploadCount % 3 == 0) {
                                myDropzone.processQueue();
                            }
                        }
                    });
                    this.on("error", function (files, response) {
                        ShowError(response);
                    });
                    this.on("complete", function (file) {
                        this.removeFile(file);
                    });
                },

                resize: function (file) {
                    var info = { srcX: 0, srcY: 0, srcWidth: file.width, srcHeight: file.height },
                        srcRatio = file.width / file.height;
                    if (file.height > this.options.thumbnailHeight || file.width > this.options.thumbnailWidth) {
                        info.trgHeight = this.options.thumbnailHeight;
                        info.trgWidth = info.trgHeight * srcRatio;
                        if (info.trgWidth > this.options.thumbnailWidth) {
                            info.trgWidth = this.options.thumbnailWidth;
                            info.trgHeight = info.trgWidth / srcRatio;
                        }
                    } else {
                        info.trgHeight = file.height;
                        info.trgWidth = file.width;
                    }
                    return info;
                }
            });
        }

        function ArrangeGrids() {
            $('.gvDocumentsFind').dataTable({
                "pageLength": 50,
                "order": [[1, 'desc']],
                "responsive": true,
                "autoWidth": true,
                "columnDefs": [
                    { 'orderable': false, targets: 0 },
                    { type: 'de_datetime', targets: 2 }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });

            $("#tblSubTasks").dataTable({
                pageLength: 50,
                order: [[<%=(ListSort == 1 ? "1" : "7")%>,  '<%=(ListSortDir ? "asc" : "desc")%>']],
                responsive: true,
                autoWidth: true,
                processing: true,
                serverSide: true,
                ajax:
                {
                    url: "api/tasks/GetSubTasksInfo",
                    contentType: "application/json",
                    type: "GET",
                    dataType: "JSON",
                    data: function (data) {
                        for (var i = 0, len = data.columns.length; i < len; i++) {
                            delete data.columns[i].search;
                            delete data.columns[i].searchable;
                            delete data.columns[i].orderable;
                            delete data.columns[i].name;
                        }
                        delete data.search.regex;
                        data.task_id = $('#<%=hndParentForSubs.ClientID%>').val();
                    }
                },
                columns: [
                    { data: "Edit_Button" },
                    { data: "Task_Date" },
                    { data: "Task_Number" },
                    { data: "Workflow_Name" },
                    { data: "Extra_Field_Value" },
                    { data: "Extra_Field_Value2" },
                    { data: "Current_Step" },
                    { data: "Posted_Date" },
                    { data: "Edited_User" },
                    { data: "Step_Status" }
                ],
                columnDefs: [
                    { orderable: false, width:"22px", targets: 0 },
                    { orderable: true, className: 'progress_cell', targets: 6 }
                ],
                "fnDrawCallback": function(oSettings, json) {
                    AdjustGridResp();
                }
            });
            setInterval(function () {
                $('#tblSubTasks').DataTable().ajax.reload(null, false);
            }, <%=RefFreq%>);

            $(".dataTables_filter").children().children('input').attr('placeholder', 'Search...');
        }

        function getSubStack(Stack_Item_ID, ItemDetails) {
            $('#<%=hndSubStack.ClientID%>').val($('#<%=hndSubStack.ClientID%>').val() + '|' + $('#<%=hndParentForSubs.ClientID%>').val() + '^' + $('#spanParentTaskDetails').html());
            $('#<%=hndParentForSubs.ClientID%>').val(Stack_Item_ID);
            $('#spanParentTaskDetails').html(ItemDetails);
            $('#subTaskReturn').removeClass('hide');
            $('#subTasksTitle').addClass('pl30');
            $('#tblSubTasks').DataTable().ajax.reload(null, false);            
            return false;
        }

        function clearSubStack(Main_Item_ID, ItemDetails) {
            $('#<%=hndSubStack.ClientID%>').val('');
            $('#<%=hndParentForSubs.ClientID%>').val(Main_Item_ID);
            $('#spanParentTaskDetails').html(ItemDetails);
            $('#subTaskReturn').addClass('hide');
            $('#subTasksTitle').removeClass('pl30');
            $('#tblSubTasks').DataTable().ajax.reload(null, false);
            return false;
        }

        function stepOutSubStack() {
            if ($('#<%=hndSubStack.ClientID%>').val().trim() != '') {
                var subStack = $('#<%=hndSubStack.ClientID%>').val().split('|');
                var newSubStack = '';
                for (var i = 0; i < subStack.length - 1; i++) {
                    if (subStack[i].trim() != '') {
                        newSubStack = newSubStack + '|' + subStack[i];
                    }
                }
                $('#<%=hndSubStack.ClientID%>').val(newSubStack);
                var lastSubItem = subStack[subStack.length - 1].split('^');
                $('#<%=hndParentForSubs.ClientID%>').val(lastSubItem[0]);
                $('#spanParentTaskDetails').html(lastSubItem[1]);
                if (newSubStack.trim() == '') {
                    $('#subTaskReturn').addClass('hide');
                    $('#subTasksTitle').removeClass('pl30');
                }
                $('#tblSubTasks').DataTable().ajax.reload(null, false);
            }
            return false;
        }

        function ArrangePopups() {
            AdjustPopupSize(80, 600, 'at_model_stli');
            AdjustPopupSize(80, 800, 'at_model_file');
            AdjustPopupSize(200, 1500, 'at_model_tsklst');
            AdjustPopupSize(167, 1200, 'at_model_chat');
            AdjustPopupSize(80, 1200, 'at_model_file');
            AdjustPopupSize(167, 1200, 'at_model_vc');
            AdjustPopupSize(80, 1200, 'at_model_com');
            AdjustPopupSize(80, 800, 'at_model_docinfo');
            AdjustPopupSize(80, 1200, 'at_model_doccom');
            AdjustPopupSize(80, 400, 'at_model_del_doc');
            AdjustPopupSize(80, 400, 'at_del_model_doc_link');
            AdjustPopupSize(80, 1200, 'at_model_add_doc_link');
            AdjustPopupSize(167, 1800, 'at_model_vd');
            AdjustMessagePopup();
        }

        function ArrangeChart() {
            Morris.Line({
                element: 'progress_chart',
                data: task_data,
                xkey: 't_date',
                ykeys: ['forecasted_val', 'actual_val'],
                labels: ['Forecast', 'Actual'],
                hideHover: 'auto',
                lineColors: ['#075181', '#5bb0e8'],
                fillOpacity: 0.2,
                behaveLikeLine: true,
                lineWidth: 1,
                pointSize: 2,
                gridLineColor: '#cfcfcf',
                xLabels: "day",
                resize: true
            });
        }

        function ArrangeMessageLoad() {
            $(".clickable-row").click(function () {
                LoadMessage($(this).data("href"));
            });
        }

        function AdjustMessagePopup() {
            var h = $(window).height();
            if ($("#hndMessageThreadID").val() == '0') {
                $("#MessageThread").css('min-height', h - 211);
            }
            else {
                $("#MessageThread").css('min-height', h - 196);
            }
        }

        function ClearUploadControls() {
            clearDropDown(['<%=cboFileTypesUpload.ClientID%>', '<%=cboAccessLevel.ClientID%>']);
            clearTextBox(['<%=txtFileName.ClientID%>']);
            clearCheckBox(['<%=chkReUpload.ClientID%>']);
            $('#<%=hndIsResult.ClientID%>').val('0');
            $('#<%=lblRType.ClientID%>').html('Source Document');            
            $find("mpuUpload").show();
            return false;
        }

        function ClearUploadControls2() {
            clearDropDown(['<%=cboFileTypesUpload.ClientID%>', '<%=cboAccessLevel.ClientID%>']);
            clearTextBox(['<%=txtFileName.ClientID%>']);
            $('#<%=hndIsResult.ClientID%>').val('1');
            $('#<%=lblRType.ClientID%>').html('Output Document');
            clearCheckBox(['<%=chkReUpload.ClientID%>']);
            $find("mpuUpload").show();
            return false;
        }

        function LoadMessage(thread_id) {
            $find('mpuMessage').show();
            $("#hndMessageThreadID").val(thread_id);
            $("#txtMessage").val("");
            if (thread_id == 0) {
                $("#newMessage").removeClass("hide");
                $("#oldMessage").addClass("hide");
                $("#txtTitle").val('');
                $("#cboMessageUsers").val('0');
                $("#MessageThread").html('');
                AdjustMessagePopup();
            }
            else {
                $("#newMessage").addClass("hide");
                $("#oldMessage").removeClass("hide");
                $("#lblTitle").html('');
                $("#lblOtherUser").html('');
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
            $("#lblOtherUser").html(resultObject[0].Other_Member);
            $("#hndMessageMemberID").val(resultObject[0].Other_Member_ID);

            var resultObjectSub = resultObject[0].Messages;
            var mediaString = "";
            for (var i = 0; i < resultObjectSub.length; i++) {
                if ($("#hndMessageMyID").val() == resultObjectSub[i].User_ID) {
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

        function ClearCommentControls() {
            clearTextBox(['<%=txtCommentMain.ClientID%>']);
            clearDropDown(['<%=cboPriority.ClientID%>', '<%=cboCommentType.ClientID%>']);
            $find("mpuComment").show();
            return false;
        }

        function ClearDocCommentControls() {
            clearTextBox(['<%=txtDocComment.ClientID%>']);
            clearDropDown(['<%=cboDocCommentPriority.ClientID%>', '<%=cboDocCommentType.ClientID%>']);
            return false;
        }

        function showDocComments(Doc_ID) {
            $('#<%=divDocComments.ClientID%>').html('');
            $('#<%=hndDocCommentID.ClientID%>').val(Doc_ID);
            $('#<%=hndIsDocument.ClientID%>').val('1');
            ClearDocCommentControls();
            $find("mpuDocComment").show();
            $.ajax({
                type: "GET",
                url: "api/tasks/GetDocComments",
                data: { Task_ID: $('#<%=hndTaskID.ClientID%>').val(), Task_Doc_ID: Doc_ID },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadDocCommentsPassed,
                failure: LoadValuesFailed
            });
            return false;
        }

        function LoadDocCommentsPassed(result) {
            var resultObject = result;
            var strDocComments = "";
            var CurrCommType = "";
            for (var i = 0; i < resultObject.length; i++) {
                if (resultObject[i].Comment_Category.trim() != CurrCommType) {
                    strDocComments = strDocComments + "<div class=\"comment no-padding-b  no-border\">" +
                        " <div class=\"comment-body no-margin-hr\">" +
                        "   <div class=\"comment-text\">" +
                        "    <b>" + resultObject[i].Comment_Category + "</b>" +
                        "   </div>" + " </div>" +
                        "</div>";
                    CurrCommType = resultObject[i].Comment_Category.trim();
                }
                var cssClass = resultObject[i].Comment_Type == "1" ? "timeline-label_p1" : resultObject[i].Comment_Type == "2" ? "timeline-label_p2" : "";
                strDocComments = strDocComments + "<div class=\"comment no-border\">" +
                    " <img src=\"" + resultObject[i].Image_Path + "\" alt=\"Profile Picture\" class=\"comment-avatar\">" +
                    " <div class=\"comment-body\">" +
                    "   <div class=\"comment-text\">" +
                    "       <div class=\"panel tl-body p8 \">" +
                    (resultObject[i].isParentRecord == "1" ? "<div class='history_parent ttip badge badge-primary' data-placement='right' title='From Parent Task'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                    (resultObject[i].isChildRecord == "1" ? "<div class='history_parent ttip badge badge-info' data-placement='right' title='From Sub Task'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                    "           <h5 class=\"text-warning mt\">" +
                    "               <span class=\"text-info\">" + resultObject[i].Commented_By_Name + "</span>" +
                    "           </h5>" +
                    "           <div class=\"well well-md p8 " + cssClass + "\" style=\"margin: 6px 0 0 0;\">" + resultObject[i].Comment + "</div>" +
                    "       </div>" +
                    "   </div>" +
                    "   <div class=\"comment-actions\">" +
                    "       <span class=\"pull-right text-sm\">" + resultObject[i].Comment_Date + "</span>" +
                    "   </div>" +
                    " </div>" +
                    "</div>";
            }
            if (strDocComments.trim() == "") {
                strDocComments = 'No Comments Available'
            }
            $('#<%=divDocComments.ClientID%>').html(strDocComments);
            $('.ttip').tooltip();
        }

        function showAttachComments(Doc_ID) {
            $('#<%=divDocComments.ClientID%>').html('');
            $('#<%=hndDocCommentID.ClientID%>').val(Doc_ID);
            $('#<%=hndIsDocument.ClientID%>').val('0');
            ClearDocCommentControls();
            $find("mpuDocComment").show();
            $.ajax({
                type: "GET",
                url: "api/tasks/GetAttachComments",
                data: { Task_ID: $('#<%=hndTaskID.ClientID%>').val(), Task_Doc_ID: Doc_ID },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadAttachCommentsPassed,
                failure: LoadValuesFailed
            });
            return false;
        }

        function LoadAttachCommentsPassed(result) {
            var resultObject = result;
            var strDocComments = "";
            var CurrCommType = "";
            for (var i = 0; i < resultObject.length; i++) {
                if (resultObject[i].Comment_Category.trim() != CurrCommType) {
                    strDocComments = strDocComments + "<div class=\"comment no-padding-b  no-border\">" +
                        " <div class=\"comment-body no-margin-hr\">" +
                        "   <div class=\"comment-text\">" +
                        "    <b>" + resultObject[i].Comment_Category + "</b>" +
                        "   </div>" + " </div>" +
                        "</div>";
                    CurrCommType = resultObject[i].Comment_Category.trim();
                }
                var cssClass = resultObject[i].Comment_Type == "1" ? "timeline-label_p1" : resultObject[i].Comment_Type == "2" ? "timeline-label_p2" : "";
                strDocComments = strDocComments + "<div class=\"comment no-border\">" +
                    " <img src=\"" + resultObject[i].Image_Path + "\" alt=\"Profile Picture\" class=\"comment-avatar\">" +
                    " <div class=\"comment-body\">" +
                    "   <div class=\"comment-text\">" +
                    "       <div class=\"panel tl-body p8 \">" +
                    (resultObject[i].isParentRecord == "1" ? "<div class='history_parent ttip badge badge-primary' data-placement='right' title='From Parent Task'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                    (resultObject[i].isChildRecord == "1" ? "<div class='history_parent ttip badge badge-info' data-placement='right' title='From Sub Task'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                    "           <h5 class=\"text-warning mt\">" +
                    "               <span class=\"text-info\">" + resultObject[i].Commented_By_Name + "</span>" +
                    "           </h5>" +
                    "           <div class=\"well well-md p8 " + cssClass + "\" style=\"margin: 6px 0 0 0;\">" + resultObject[i].Comment + "</div>" +
                    "       </div>" +
                    "   </div>" +
                    "   <div class=\"comment-actions\">" +
                    "       <span class=\"pull-right text-sm\">" + resultObject[i].Comment_Date + "</span>" +
                    "   </div>" +
                    " </div>" +
                    "</div>";
            }
            if (strDocComments.trim() == "") {
                strDocComments = 'No Comments Available'
            }
            $('#<%=divDocComments.ClientID%>').html(strDocComments);
            $('.ttip').tooltip();
        }

        function showAttachInfo(Doc_ID) {
            $find("mpuDocInfo").show();
            $.ajax({
                type: "GET",
                url: "api/tasks/GetDocInfo",
                data: { Document_ID: Doc_ID },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadDocInfoPassed,
                failure: LoadValuesFailed
            });
            return false;
        }

        function LoadDocInfoPassed(result) {
            $("#divDocInfo").html(result);
        }

        function LinkDocument(linkType) {
            $('#<%=hndIsResultLink.ClientID %>').val(linkType);
            $find('mpuAddDocLink').show();
            return false;
        }

        function saveLink() {
            document.getElementById('<%=cmdSaveAddLinkhnd.ClientID%>').click();
        }

        function LoadDocumentsForType(docType) {
            $('#<%=cboDocType.ClientID%>').val(docType);
            $find("mpuDocumentView").show();
            LoadDocuments();
        }

        function LoadDocuments() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetTaskDocumentsView",
                data: { Task_ID: $('#<%=hndTaskID.ClientID%>').val(), DocType: $('#<%=cboDocType.ClientID%>').val(), DocCategory: $('#<%=cboDocCategories.ClientID%>').val() },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadDocsPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadDocsPassed(result) {
            var resultObject = result;
            var strDocs = "";
            var CurrDocType = "";
            for (var i = 0; i < resultObject.length; i++) {
                if (resultObject[i].Task_Doc_Type.trim() != CurrDocType) {
                    strDocs = strDocs + "<div class=\"col-md-12\">" +
                        "<h5><b>" + resultObject[i].Task_Doc_Type + "</b></h5>" +
                        "</div>";
                    CurrDocType = resultObject[i].Task_Doc_Type.trim();
                }
                strDocs = strDocs + "<div class=\"col-lg-2 col-md-3 col-sm-4 col-xs-6\" style=\"min-height: 140px\">" +
                    "<div class=\"row\">" +
                    "<div class=\"col-md-12 text-right\">" +
                    resultObject[i].Parent_Desc +
                    "</div>" +
                    "<div class=\"col-md-12 text-center\">" +
                    "<a href='" + resultObject[i].Preview_Page + ".aspx?fid=" + resultObject[i].Doc_Path + "' target='_blank'><img src=\"" + resultObject[i].Doc_Icon + "\" style=\"width: 80px; height: auto\" /></a>" +
                    "</div>" +
                    "<div class=\"col-md-12 text-center\">" +
                    "<a href='" + resultObject[i].Preview_Page + ".aspx?fid=" + resultObject[i].Doc_Path + "' target='_blank'>" + resultObject[i].Doc_Name + "</a>" +
                    "</div>" +
                    "<div class=\"col-md-12 text-center\">" +
                    resultObject[i].Comments_Count +
                    "</div>" +
                    "</div>" +
                    "</div>";
            }

            $('#<%=divDocView.ClientID%>').html(strDocs);
            $('.ttip').tooltip();
        }

        function closeDocView() {
            $find("mpuDocumentView").hide();
            return false;
        }

        function ViewComments() {
            $find("mpuViewComment").show();
            return false;
        }

        function CloseCommentView() {
            $find("mpuViewComment").hide();
            return false;
        }

        function ShowStepsList() {
            $find("mpuStepsList").show();
            return false;
        }

        function LoadTagdComments() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetTaskComments",
                data: { Task_ID: $('#<%=hndTaskID.ClientID%>').val(), TagVal: $('#<%=cboTags.ClientID%>').val() },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadTagsPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadTagsPassed(result) {
            var resultObject = result;
            var strCommentsOnly = "";
            var CurrCommType = "";
            for (var i = 0; i < resultObject.length; i++) {
                if (resultObject[i].Comment_Category.trim() != CurrCommType) {
                    strCommentsOnly = strCommentsOnly + "<div class=\"comment no-padding-b  no-border\">" +
                        " <div class=\"comment-body no-margin-hr\">" +
                        "   <div class=\"comment-text\">" +
                        "    <b>" + resultObject[i].Comment_Category + "</b>" +
                        "   </div>" + " </div>" +
                        "</div>";
                    CurrCommType = resultObject[i].Comment_Category.trim();
                }
                var cssClass = resultObject[i].Comment_Type == "1" ? "timeline-label_p1" : resultObject[i].Comment_Type == "2" ? "timeline-label_p2" : "";
                strCommentsOnly = strCommentsOnly + "<div class=\"comment no-border\">" +
                    " <img src=\"" + resultObject[i].Image_Path + "\" alt=\"Profile Picture\" class=\"comment-avatar\">" +
                    " <div class=\"comment-body\">" +
                    "   <div class=\"comment-text\">" +
                    "       <div class=\"panel tl-body p8 \">" +
                    (resultObject[i].isParentRecord == "1" ? "<div class='history_parent ttip badge badge-primary' data-placement='right' title='From Parent Task'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                    (resultObject[i].isChildRecord == "1" ? "<div class='history_parent ttip badge badge-info' data-placement='right' title='From Sub Task'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                    "           <h5 class=\"text-warning mt\">" +
                    "               <span class=\"text-info\">" + resultObject[i].Commented_By_Name + "</span>" +
                    "           </h5>" +
                    "           <div class=\"well well-md p8 " + cssClass + "\" style=\"margin: 6px 0 0 0;\">" + resultObject[i].Comment + "</div>" +
                    "       </div>" +
                    "   </div>" +
                    "   <div class=\"comment-actions\">" +
                    "       <span class=\"pull-right text-sm\">" + resultObject[i].Comment_Date + "</span>" +
                    "   </div>" +
                    " </div>" +
                    "</div>";
            }
            $('#<%=divCommentsOnly.ClientID%>').html(strCommentsOnly);
            $('.ttip').tooltip();
        }

        function GetFiles(type) {
            $.ajax({
                type: "GET",
                url: "api/tasks/ZipDocs",
                data: { Task_ID: $('#<%=hndTaskID.ClientID%>').val(), Type: type },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: GetFilesPassed,
                failure: LoadValuesFailed
            });
            return false;
        }

        function GetFilesPassed(result) {
            var fileName = result;
            if (fileName.trim() == "") {
                alert("No Files to Download");
            }
            else {
                openNewTab('document_preview_temp.aspx?d=' + fileName);
            }
        }

        <%=Task_Script%>
    </script>
</asp:Content>