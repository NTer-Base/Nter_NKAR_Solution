<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid.master" AutoEventWireup="true" CodeBehind="task.aspx.cs" Inherits="N_Ter_Tasks.task" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter > Task
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
    <link href="assets/stylesheets/pages.min.css" rel="stylesheet" type="text/css">
    <%=Dock_CSS_Init%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Task 
	 <asp:Label ID="lblTaskNumber" runat="server" Text="Label"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <asp:HiddenField ID="hndTaskID" runat="server" />
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
                                <li id="lstAnchor" runat="server"><a href="#" onclick="return ShowAnchorLink();"><span class="btn-label menu_icon fa fa-anchor"></span>Get Doc Upload URL</a></li>
                                <li id="lstGuestPost" runat="server"><a href="#" onclick="return ShowGuestPostLink();"><span class="btn-label menu_icon fa fa-anchor"></span>Get Guest Post URL</a></li>
                                <li id="lstExtraField1" runat="server"><a href="#" onclick="return ShowExtraField();"><span class="btn-label menu_icon fa fa-edit"></span>Update
                                    <asp:Literal ID="ltrExtraFieldName" runat="server"></asp:Literal></a></li>
                                <li id="lstExtraField2" runat="server"><a href="#" onclick="return ShowExtraField2();"><span class="btn-label menu_icon fa fa-edit"></span>Update
                                    <asp:Literal ID="ltrExtraField2Name" runat="server"></asp:Literal></a></li>
                                <li id="lstDueDate" runat="server"><a href="#" onclick="return ShowDueDate();"><span class="btn-label menu_icon fa fa-calendar"></span>Update Due Date</a></li>
                                <li id="lstStepDeadlines" runat="server"><a href="#" onclick="return ShowStepDeadlines();"><span class="btn-label menu_icon fa fa-clock-o"></span>Update Step Deadlines</a></li>
                                <li id="lstEditEL2" runat="server"><a href="#" onclick="return ShowEditEL2();"><span class="btn-label menu_icon fa fa-edit"></span>Update
                                    <asp:Literal ID="ltrEL2_12" runat="server"></asp:Literal></a></li>
                                <li id="lstUpdateQueue" runat="server"><a href="#" onclick="return ShowQueue();"><span class="btn-label menu_icon fa fa-sort-amount-asc"></span>Update Task Queue</a></li>
                                <li id="lstEL2Hierarchy" runat="server"><a href="#" onclick="return ShowEL2Structure();"><span class="btn-label menu_icon fa fa-bars"></span><asp:Literal ID="ltrEL2_2" runat="server"></asp:Literal> Structure</a></li>
                                <li id="lstSpecialData" runat="server"><a href="#" onclick="return ShowSpecialData();"><span class="btn-label menu_icon fa fa-puzzle-piece"></span>Special Task Data</a></li>
                                <li id="lstTaskScript" runat="server"><a href="#" onclick="return GetTaskScript();"><span class="btn-label menu_icon fa fa-download"></span>Get Task Script</a></li>
                                <asp:Literal ID="ltrCustomScreens" runat="server"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                    <asp:ModalPopupExtender ID="SubTaskModalPopup" runat="server" Enabled="True" PopupControlID="pnlSubTasks" TargetControlID="cmdOpenSub" CancelControlID="cmdCloseTskList" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                    <div class="panel-title-nter">
                        <asp:Literal ID="ltrTaskDetailsPanelName" runat="server"></asp:Literal></div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Task Number</div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblTaskNo" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
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
                        <div class="col-md-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Workflow Name</div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblWorkflow" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Current Status</div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblCurrentStatus" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Task Creator</div>
                                <div class="col-sm-5">
                                    <asp:Label ID="lblTaskCreator" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Task Queue</div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblQueue" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Task Date/Time</div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblTaskDate" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Task Due Date/Time</div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblDueDate" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div id="divExtraField" runat="server" class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">
                                    <asp:Literal ID="lblExtraFieldName" runat="server"></asp:Literal>
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblExtraFieldValue" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div id="divExtraField2" runat="server" class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">
                                    <asp:Literal ID="lblExtraField2Name" runat="server"></asp:Literal>
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblExtraField2Value" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div id="divTaskDuration" runat="server" class="col-md-6">
                            <div class="row pt3">
                                <div class="col-sm-4 text-bold text-right-md">Task Duration</div>
                                <div class="col-sm-8 text-info text-bold">
                                    <asp:Label ID="lblDuration" runat="server" Text="N/A"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div id="divTimeUnit" runat="server" class="col-md-6">
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
        <div id="divPost" runat="server" class="col-sm-7">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="panel_menu">
                        <button id="cmdPause" class="btn btn-labeled btn-primary" onclick="return pauseTask();"><span id="spanPause" class="btn-label icon fa fa-pause"></span>Pause</button>
                        <div class="btn-group">
                            <button class="btn btn-labeled btn-primary dropdown-toggle" data-toggle="dropdown"><span class="btn-label icon fa fa-cogs"></span><i class="fa fa-caret-down"></i></button>
                            <ul class="dropdown-menu pull-right">
                                <li id="cmdCancel" runat="server"><a href="#"><span class="btn-label menu_icon fa fa-trash-o"></span>Discard Task</a></li>
                                <li id="cmdRelease" runat="server"><a href="#"><span class="btn-label menu_icon fa fa-unlock"></span>Unclaim Task</a></li>
                                <li class="divider"></li>
                                <li id="cmdViewProgress" runat="server"><a href="#"><span class="btn-label menu_icon fa fa-dashboard"></span>View Progress</a></li>
                                <li id="cmdViewAddons" runat="server"><a href="#"><span class="btn-label menu_icon fa fa-eye"></span>View Addons</a></li>
                                <li id="cmdAddonDivider" runat="server" class="divider"></li>
                                <li id="cmdAddAddon" runat="server"><a href="#"><span class="btn-label menu_icon fa fa-anchor"></span><asp:Literal ID="ltrAddonName" runat="server"></asp:Literal></a></li>
                            </ul>
                        </div>
                        <asp:ModalPopupExtender ID="cmdCancel_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlConfirmCancel" TargetControlID="cmdCancel" CancelControlID="cmdCancelTaskCancel" BackgroundCssClass="at_modelpopup_background">
                        </asp:ModalPopupExtender>
                        <asp:ModalPopupExtender ID="cmdRelease_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlConfirmRelease" TargetControlID="cmdRelease" CancelControlID="cmdCancelRelease" BackgroundCssClass="at_modelpopup_background">
                        </asp:ModalPopupExtender>
                        <asp:ModalPopupExtender ID="cmdAddAddon_ModalPopupExtender" runat="server" BehaviorID="mpuAddon" Enabled="True" PopupControlID="pnlAddon" TargetControlID="cmdAddAddon" CancelControlID="cmdCloseAddon" BackgroundCssClass="at_modelpopup_background">
                        </asp:ModalPopupExtender>
                        <asp:ModalPopupExtender ID="cmdViewProgress_ModalPopupExtender" runat="server" BehaviorID="mpuTaskProgress" Enabled="True" PopupControlID="pnlTaskProgress" TargetControlID="cmdViewProgress" CancelControlID="cmdCloseTaskProgress2" BackgroundCssClass="at_modelpopup_background">
                        </asp:ModalPopupExtender>
                        <asp:ModalPopupExtender ID="cmdViewAddons_ModalPopupExtender" runat="server" BehaviorID="mpuTaskAddons" Enabled="True" PopupControlID="pnlTaskAddons" TargetControlID="cmdViewAddons" CancelControlID="cmdCloseTaskAddons2" BackgroundCssClass="at_modelpopup_background">
                        </asp:ModalPopupExtender>
                    </div>
                    <div class="panel-title-nter">
                        <asp:Literal ID="ltrTaskPostPanelName" runat="server"></asp:Literal></div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <div id="divSpecialDataMain" runat="server" class="alert alert-warning"></div>
                    <div id="divAddEL2Alert" runat="server" class="alert alert-warning">
                        <asp:HiddenField ID="hndRequireEL2" runat="server" />
                        <strong>Alert !</strong> You are required to add a
                        <asp:Literal ID="ltrEL2_9" runat="server"></asp:Literal>
                        at this step.
                        <div class="row">
                            <div class="col-md-12 text-right">
                                <asp:Button ID="cmdAddEL2" runat="server" Text="Add El2" class="btn btn-warning" />
                                <asp:ModalPopupExtender ID="cmdAddEL2_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlEL2" TargetControlID="cmdAddEL2" CancelControlID="cmdNoEL2" BackgroundCssClass="at_modelpopup_background">
                                </asp:ModalPopupExtender>
                            </div>
                        </div>
                    </div>
                    <div id="divAddEL2Alert2" runat="server" class="alert alert-warning">
                        <strong>Alert !</strong> The connected
                        <asp:Literal ID="ltrEL2_11" runat="server"></asp:Literal>
                        will be deactivated at Submit.
                    </div>
                    <div id="divHelpText" runat="server" class="alert alert-success">
                        <strong>Information !</strong>
                        <asp:Label ID="ltrHelpText" runat="server" Text="Label"></asp:Label>
                    </div>
                    <asp:Panel ID="pnlCuzPost" runat="server"></asp:Panel>
                    <asp:Panel ID="pnlStepData" runat="server">
                    </asp:Panel>
                </div>
                <div id="divSubmitFooter" runat="server" class="panel-footer text-right">
                    <asp:Button ID="cmdReject" runat="server" Text="Reject" class="btn btn-default pull-left strj" data-toggle="tooltip" data-placement="right" data-original-title="-" />
                    <asp:ModalPopupExtender ID="cmdReject_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlRejectConfirm" BehaviorID="mpuReject" TargetControlID="cmdReject" CancelControlID="cmdRejectCancel" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                    <asp:Button ID="cmdSave" runat="server" Text="Save" class="btn btn-primary" OnClick="cmdSave_Click" />
                    <asp:Button ID="cmdAddSub" runat="server" Text="Create Sub Tasks" class="btn btn-primary ml5" />
                    <asp:ModalPopupExtender ID="cmdAddSub_ModalPopupExtender" runat="server" Enabled="true" PopupControlID="pnlSubTaskStart" TargetControlID="cmdAddSub" BackgroundCssClass="at_modelpopup_background" CancelControlID="cmdSwfClose">
                    </asp:ModalPopupExtender>
                    <asp:Button ID="cmdSubmit" runat="server" Text="Submit" class="btn btn-primary ml5 stt" OnClick="cmdSubmit_Click" data-toggle="tooltip" data-placement="left" data-original-title="-" />
                    <asp:Button ID="cmdCloseMain" runat="server" Text="Submit" class="btn btn-primary ml5 stt" OnClick="cmdCloseMain_Click" data-toggle="tooltip" data-placement="left" data-original-title="-" />
                    <asp:HiddenField ID="hndSubmitType" runat="server" />
                    <asp:ModalPopupExtender ID="cmdAddSub_Close_ModalPopupExtender" runat="server" Enabled="false" PopupControlID="pnlSubTaskStart" BehaviorID="mpuCloseMain" TargetControlID="hndSubmitType" BackgroundCssClass="at_modelpopup_background" CancelControlID="cmdSwfClose">
                    </asp:ModalPopupExtender>
                    <div id="divAnyStep" runat="server" class="input-group mar-btm mar-top mt10">
                        <asp:DropDownList ID="cboSteps" runat="server" CssClass="form-control"></asp:DropDownList>
                        <span class="input-group-btn">
                            <asp:Button ID="cmdSubmitSpecial" runat="server" Text="Submit - Selected Step" CssClass="btn btn-primary" OnClick="cmdSubmitSpecial_Click" />
                        </span>
                    </div>
                    <asp:Button ID="cmdGotoTaskList" runat="server" Text="Submit" class="btn btn-primary hide" OnClick="cmdGotoTaskList_Click" />
                </div>
            </div>
            <div id="divTaskTimeline" runat="server" class="panel panel-info">
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
            <div id="divTaskChats" runat="server" class="panel panel-info">
                <div class="panel-body">
                    <div class="panel_menu">
                        <div class="btn-group">
                            <button class="btn btn-labeled btn-primary dropdown-toggle" data-toggle="dropdown"><span class="btn-label icon fa fa-cogs"></span><i class="fa fa-caret-down"></i></button>
                            <ul class="dropdown-menu pull-right">
                                <li id="lstNewChat" runat="server"><a href="#" onclick="return LoadMessage(0);"><span class="btn-label menu_icon fa fa-comments"></span>Start New Chat</a></li>
                            </ul>
                        </div>
                    </div>
                    <asp:ModalPopupExtender ID="lstNewChat_ModalPopupExtender" runat="server" BehaviorID="mpuMessage" Enabled="True" PopupControlID="pnlChat" TargetControlID="lstNewChat" CancelControlID="cmdCloseChat" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                    <div class="panel-title-nter">Task Chats</div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <asp:Literal ID="ltrThreads" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
        <div id="divSupportPane" runat="server" class="col-sm-5">
            <div id="divSourceDoc" runat="server" class="panel panel-info">
                <a href="#" id="divSourceDoc_toggler" runat="server" class="fa fa-file-o"></a>
                <div class="panel-body">
                    <div class="panel_menu">
                        <div class="btn-group">
                            <button class="btn btn-labeled btn-primary dropdown-toggle" data-toggle="dropdown"><span class="btn-label icon fa fa-cogs"></span><i class="fa fa-caret-down"></i></button>
                            <ul class="dropdown-menu pull-right">
                                <li id="cmdViewDocs" runat="server"><a href="#" onclick="return LoadDocumentsForType('1');"><span class="btn-label menu_icon fa fa-eye"></span><b>View Documents</b></a></li>
                                <li id="cmdDownload1" runat="server"><a href="#" onclick="return GetFiles(false);"><span class="btn-label menu_icon fa fa-download"></span>Download All</a></li>
                                <li class="divider"></li>
                                <li id="cmdAddDocument" runat="server"><a href="#" onclick="return ClearUploadControls();"><span class="btn-label menu_icon fa fa-upload"></span>Upload Documents</a></li>
                                <li id="cmdReplaceDocument" runat="server"><a href="#" onclick="return ReplaceFile(0);"><span class="btn-label menu_icon fa fa-retweet"></span>Replace Document</a></li>
                                <li id="cmdDocLink" runat="server"><a href="#" onclick="return UpdateRType('0');"><span class="btn-label menu_icon fa fa-link"></span>Attach Documents</a></li>
                                <li id="cmdDocLinkUP" runat="server"><a href="#" onclick="return UpdateRType('0');"><span class="btn-label menu_icon fa fa-link"></span>Upload Documents (DP)</a></li>
                                <li id="lstSeperator1" runat="server" class="divider"></li>
                                <li id="cmdDocFilter" runat="server"><a id="cmdDocFilterClick" runat="server"><span class="btn-label menu_icon fa fa-filter"></span>Filter Attached Documents</a></li>
                            </ul>
                        </div>
                    </div>
                    <asp:ModalPopupExtender ID="cmdViewDocs_ModalPopupExtender" runat="server" BehaviorID="mpuDocumentView1" Enabled="True" PopupControlID="pnlDocView" TargetControlID="cmdViewDocs" CancelControlID="cmdCloseViewDocs" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                    <asp:ModalPopupExtender ID="cmdDocLink_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlAddDocLink" TargetControlID="cmdDocLink" CancelControlID="cmdCloseAddLink" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                    <asp:ModalPopupExtender ID="cmdDocLinkUP_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDP_Upload" TargetControlID="cmdDocLinkUP" CancelControlID="cmdCloseDP_Upload" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                    <asp:ModalPopupExtender ID="cmdAddDocument_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlUpload" TargetControlID="cmdAddDocument" CancelControlID="cmdCloseUpload" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                    <div class="panel-title-nter">
                        Source Documents
						<asp:Literal ID="ltrTaskDocCount" runat="server"></asp:Literal>
                    </div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <div id="divSourceDoc_content" class="widget-comments documents_scroll styled-bar<%=Dock_CSS_1%>">
                        <div class="no-padding-vr mr8 long_word_wrap">
                            <asp:Literal ID="ltrAttachments" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divOutDoc" runat="server" class="panel panel-info">
                 <a href="#" id="divOutDoc_toggler" runat="server" class="fa fa-file"></a>
                 <div class="panel-body">
                    <div class="panel_menu">
                        <div class="btn-group">
                            <button class="btn btn-labeled btn-primary dropdown-toggle" data-toggle="dropdown"><span class="btn-label icon fa fa-cogs"></span><i class="fa fa-caret-down"></i></button>
                            <ul class="dropdown-menu pull-right">
                                <li id="cmdViewDocsR" runat="server"><a href="#" onclick="return LoadDocumentsForType('2');"><span class="btn-label menu_icon fa fa-eye"></span><b>View Documents</b></a></li>
                                <li id="cmdDownload2" runat="server"><a href="#" onclick="return GetFiles(true);"><span class="btn-label menu_icon fa fa-download"></span>Download All</a></li>
                                <li class="divider"></li>
                                <li id="cmdAddDocumentR" runat="server"><a href="#" onclick="return ClearUploadControls2();"><span class="btn-label menu_icon fa fa-upload"></span>Upload Documents</a></li>
                                <li id="cmdReplaceDocumentR" runat="server"><a href="#" onclick="return ReplaceFile(1);"><span class="btn-label menu_icon fa fa-retweet"></span>Replace Document</a></li>
                                <li id="cmdDocLinkR" runat="server"><a href="#" onclick="return UpdateRType('1');"><span class="btn-label menu_icon fa fa-link"></span>Attach Documents</a></li>
                                <li id="cmdDocLinkUPR" runat="server"><a href="#" onclick="return UpdateRType('1');"><span class="btn-label menu_icon fa fa-link"></span>Upload Documents (DP)</a></li>
                                <li id="lstSeperator1R" runat="server" class="divider"></li>
                                <li id="cmdDocFilterR" runat="server"><a id="cmdDocFilterRClick" runat="server"><span class="btn-label menu_icon fa fa-filter"></span>Filter Attached Documents</a></li>
                            </ul>
                        </div>
                    </div>
                    <asp:ModalPopupExtender ID="cmdViewDocsR_ModalPopupExtender" runat="server" BehaviorID="mpuDocumentView2" Enabled="True" PopupControlID="pnlDocView" TargetControlID="cmdViewDocsR" CancelControlID="cmdCloseViewDocs" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                    <asp:ModalPopupExtender ID="cmdDocLinkR_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlAddDocLink" TargetControlID="cmdDocLinkR" CancelControlID="cmdCloseAddLink" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                    <asp:ModalPopupExtender ID="cmdDocLinkUPR_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDP_Upload" TargetControlID="cmdDocLinkUPR" CancelControlID="cmdCloseDP_Upload" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                    <asp:ModalPopupExtender ID="cmdAddDocumentR_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlUpload" TargetControlID="cmdAddDocumentR" CancelControlID="cmdCloseUpload" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                    <div class="panel-title-nter">
                        Output Documents
						<asp:Literal ID="ltrTaskDocCountR" runat="server"></asp:Literal>
                    </div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <div id="divOutDoc_content" class="widget-comments documents_scroll styled-bar<%=Dock_CSS_1%>">
                        <div class="no-padding-vr mr8 long_word_wrap">
                            <asp:Literal ID="ltrAttachmentsR" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divHistory" runat="server" class="panel panel-info">
                <a href="#" id="divHistory_toggler" runat="server" class="fa fa-check-square-o"></a>
                <div class="panel-body">
                    <div class="panel_menu">
                        <div class="btn-group">
                            <button class="btn btn-labeled btn-primary dropdown-toggle" data-toggle="dropdown"><span class="btn-label icon fa fa-cogs"></span><i class="fa fa-caret-down"></i></button>
                            <ul class="dropdown-menu pull-right">
                                <li id="cmdViewComments" runat="server"><a href="#" onclick="return false;"><span class="btn-label menu_icon fa fa-eye"></span><b>View Comments</b></a></li>
                                <li id="cmdAddComment" runat="server"><a href="#" onclick="return ClearCommentControls();"><span class="btn-label menu_icon fa fa-comment"></span>Add Comments</a></li>
                                <li class="divider"></li>
                                <li><a href="#" onclick="return drawFlowchart();"><span class="btn-label menu_icon fa fa-qrcode"></span>View Flowchart</a></li>
                            </ul>
                        </div>
                    </div>
                    <asp:ModalPopupExtender ID="cmdAddComment_ModalPopupExtender" runat="server" BehaviorID="mpuComment" Enabled="True" PopupControlID="pnlComment" TargetControlID="cmdAddComment" CancelControlID="cmdCloseComment" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                    <asp:ModalPopupExtender ID="cmdViewComment_ModalPopupExtender" runat="server" BehaviorID="mpuViewComment" Enabled="True" PopupControlID="pnlViewComment" TargetControlID="cmdViewComments" CancelControlID="cmdCloseViewComment" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                    <div class="panel-title-nter">Task History</div>
                    <div class="border-t panel-info" style="height: 20px"></div>
                    <div id="divHistory_content" class="<%=Dock_CSS_2%>"">
                        <div class="timeline">
                            <asp:Literal ID="ltrHistory" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlRejectConfirm" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_reg_indent" class="at_modelpopup_indent">
            <div id="at_model_reg_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Confirm - Reject Task</h4>
                </div>
                <div id="at_model_reg_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            Are you sure, you want to <b>reject</b> this Task?
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdRejectConfirm" runat="server" CssClass="btn btn-primary" Text="Yes" OnClick="cmdRejectConfirm_Click" />
                    <asp:Button ID="cmdRejectCancel" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlConfirmRelease" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_rel_indent" class="at_modelpopup_indent">
            <div id="at_model_rel_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Confirm - Unclaim Task</h4>
                </div>
                <div id="at_model_rel_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            Are you sure, you want to <b>unclaim</b> this task?
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdReleaseConfirm" runat="server" CssClass="btn btn-primary" Text="Yes" OnClick="cmdReleaseConfirm_Click" />
                    <asp:Button ID="cmdCancelRelease" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlConfirmCancel" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_cans_indent" class="at_modelpopup_indent">
            <div id="at_model_cans_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Confirm - Discard Task</h4>
                </div>
                <div id="at_model_cans_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            Are you sure, you want to <b>Discard</b> this task?
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCancelTaskConform" runat="server" CssClass="btn btn-primary" Text="Yes" OnClick="cmdCancelTaskConform_Click" />
                    <asp:Button ID="cmdCancelTaskCancel" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndMessageThreadID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hndMessageMyID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hndMessageMemberID" runat="server" ClientIDMode="Static" />
    <asp:Panel ID="pnlChat" runat="server" CssClass="at_modelpopup_container" Style="display: none" DefaultButton="cmdDefaultEnter">
        <div id="at_model_chat_indent" class="at_modelpopup_indent">
            <div id="at_model_chat_inner_indent" class="panel panel-widget chat-widget at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseChat" runat="server" class="btn btn-default" Text="&times;" />
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
    <asp:Panel ID="pnlAddon" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_addon_indent" class="at_modelpopup_indent">
            <div id="at_model_addon_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseAddon" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add
                        <asp:Literal ID="ltrAddonName2" runat="server"></asp:Literal></h4>
                </div>
                <div id="at_model_addon_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <asp:Panel ID="pnlAddonFields" runat="server">
                        </asp:Panel>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSaveAddon" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSaveAddon_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndLinkDocID" runat="server" />
    <asp:Button ID="cmdSaveAddLinkhnd" runat="server" Text="Button" Style="display: none" OnClick="cmdSaveAddLinkhnd_Click" />
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
    </asp:Panel>
    <asp:HiddenField ID="hndAnchor" runat="server" />
    <asp:ModalPopupExtender ID="hndAnchor_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuAnchor" PopupControlID="pnlAnchor" TargetControlID="hndAnchor" CancelControlID="cmdCloseAnchor" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlAnchor" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_doc_anchor_indent" class="at_modelpopup_indent">
            <div id="at_model_doc_anchor_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">External Document Upload Link</h4>
                </div>
                <div id="at_model_doc_anchor_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <asp:Literal ID="ltrDocAnchor" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCloseAnchor" runat="server" Text="Close" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndGuestPost" runat="server" />
    <asp:ModalPopupExtender ID="hndGuestPost_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuGuestPost" PopupControlID="pnlGuestPost" TargetControlID="hndGuestPost" CancelControlID="cmdCloseGuestPost" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlGuestPost" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_guest_post_indent" class="at_modelpopup_indent">
            <div id="at_model_guest_post_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">External Guest Task Posting Link</h4>
                </div>
                <div id="at_model_guest_post_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <asp:Literal ID="ltrGuestPost" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCloseGuestPost" runat="server" Text="Close" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndDeleteLink" runat="server" />
    <asp:ModalPopupExtender ID="hndDeleteLink_ModalPopupExtender1" runat="server" Enabled="True" BehaviorID="mpuDeleteLink" PopupControlID="pnlDeleteDocLink" TargetControlID="hndDeleteLink" CancelControlID="cmdCancelDocLink" BackgroundCssClass="at_modelpopup_background_2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlDeleteDocLink" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_model_doc_link_indent" class="at_modelpopup_indent">
            <div id="at_del_model_doc_link_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete Link</h4>
                </div>
                <div id="at_del_model_doc_link_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDeleteDocLink" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDeleteDocLink_Click" />
                    <asp:Button ID="cmdCancelDocLink" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
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
    <asp:Panel ID="pnlTaskProgress" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_vp_indent" class="at_modelpopup_indent">
            <div id="at_model_vp_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseTaskProgress" runat="server" Text="&times;" class="btn btn-default" OnClientClick="return CloseTaskProgress();" />
                    </div>
                    <h4 class="panel-title">Task Progress</h4>
                </div>
                <div id="at_model_vp_content" class="at_modelpopup_content styled-bar">
                    <div class="widget-comments panel-body">
                        <div class="no-padding-vr">
                            <asp:Literal ID="ltrTaskProgress" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCloseTaskProgress2" runat="server" CssClass="btn btn-primary pull-right" Text="Close" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlTaskAddons" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_va_indent" class="at_modelpopup_indent">
            <div id="at_model_va_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseTaskAddons" runat="server" Text="&times;" class="btn btn-default" OnClientClick="return CloseTaskAddons();" />
                    </div>
                    <h4 class="panel-title">Task Addons</h4>
                </div>
                <div id="at_model_va_content" class="at_modelpopup_content styled-bar">
                    <div class="widget-comments panel-body">
                        <div class="no-padding-vr">
                            <asp:Literal ID="ltrTaskAddons" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCloseTaskAddons2" runat="server" CssClass="btn btn-primary pull-right" Text="Close" />
                </div>
            </div>
        </div>
    </asp:Panel>
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
                                        <asp:ListItem Value="1">Source Documents</asp:ListItem>
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
    <asp:Button ID="cmdEditDueDate" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="cmdEditDueDate_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuDueDate" PopupControlID="pnlDueDate" TargetControlID="cmdEditDueDate" CancelControlID="cmdCloseEditDueDate" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlDueDate" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_tr_indent" class="at_modelpopup_indent">
            <div id="at_model_tr_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseEditDueDate" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Due Date</h4>
                </div>
                <div id="at_model_tr_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Due Date : </label>
                            <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control dtPicker"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Due Time : </label>
                            <asp:DropDownList ID="cboDueTime" runat="server" class="form-control">
                                <asp:ListItem>00:30</asp:ListItem>
                                <asp:ListItem>01:00</asp:ListItem>
                                <asp:ListItem>01:30</asp:ListItem>
                                <asp:ListItem>02:00</asp:ListItem>
                                <asp:ListItem>02:30</asp:ListItem>
                                <asp:ListItem>03:00</asp:ListItem>
                                <asp:ListItem>03:30</asp:ListItem>
                                <asp:ListItem>04:00</asp:ListItem>
                                <asp:ListItem>04:30</asp:ListItem>
                                <asp:ListItem>05:00</asp:ListItem>
                                <asp:ListItem>05:30</asp:ListItem>
                                <asp:ListItem>06:00</asp:ListItem>
                                <asp:ListItem>06:30</asp:ListItem>
                                <asp:ListItem>07:00</asp:ListItem>
                                <asp:ListItem>07:30</asp:ListItem>
                                <asp:ListItem>08:00</asp:ListItem>
                                <asp:ListItem>08:30</asp:ListItem>
                                <asp:ListItem>09:00</asp:ListItem>
                                <asp:ListItem>09:30</asp:ListItem>
                                <asp:ListItem>10:00</asp:ListItem>
                                <asp:ListItem>10:30</asp:ListItem>
                                <asp:ListItem>11:00</asp:ListItem>
                                <asp:ListItem>11:30</asp:ListItem>
                                <asp:ListItem>12:00</asp:ListItem>
                                <asp:ListItem>12:30</asp:ListItem>
                                <asp:ListItem>13:00</asp:ListItem>
                                <asp:ListItem>13:30</asp:ListItem>
                                <asp:ListItem>14:00</asp:ListItem>
                                <asp:ListItem>14:30</asp:ListItem>
                                <asp:ListItem>15:00</asp:ListItem>
                                <asp:ListItem>15:30</asp:ListItem>
                                <asp:ListItem>16:00</asp:ListItem>
                                <asp:ListItem>16:30</asp:ListItem>
                                <asp:ListItem>17:00</asp:ListItem>
                                <asp:ListItem>17:30</asp:ListItem>
                                <asp:ListItem>18:00</asp:ListItem>
                                <asp:ListItem>18:30</asp:ListItem>
                                <asp:ListItem>19:00</asp:ListItem>
                                <asp:ListItem>19:30</asp:ListItem>
                                <asp:ListItem>20:00</asp:ListItem>
                                <asp:ListItem>20:30</asp:ListItem>
                                <asp:ListItem>21:00</asp:ListItem>
                                <asp:ListItem>21:30</asp:ListItem>
                                <asp:ListItem>22:00</asp:ListItem>
                                <asp:ListItem>22:30</asp:ListItem>
                                <asp:ListItem>23:00</asp:ListItem>
                                <asp:ListItem>23:30</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdUpdateDueDate" runat="server" CssClass="btn btn-primary pull-right" Text="Update" OnClick="cmdUpdateDueDate_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="cmdUpdateExtraField" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="cmdUpdateExtraField_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuExtraField" PopupControlID="pnlExtraField" TargetControlID="cmdUpdateExtraField" CancelControlID="cmdExtraClose" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlExtraField" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_ef_indent" class="at_modelpopup_indent">
            <div id="at_model_ef_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdExtraClose" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">
                        <asp:Literal ID="ltrExtraFieldTitle" runat="server"></asp:Literal></h4>
                </div>
                <div id="at_model_ef_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>
                                <asp:Literal ID="ltrExtraFieldTitle2" runat="server"></asp:Literal></label>
                            <asp:TextBox ID="txtExtraFieldValue" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:DropDownList ID="cboExtraFieldValue" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdExtraSave" runat="server" CssClass="btn btn-primary pull-right" Text="Update" OnClick="cmdExtraSave_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="cmdUpdateExtraField2" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="cmdUpdateExtraField2_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuExtraField2" PopupControlID="pnlExtraField2" TargetControlID="cmdUpdateExtraField2" CancelControlID="cmdExtraClose2" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlExtraField2" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_ef2_indent" class="at_modelpopup_indent">
            <div id="at_model_ef2_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdExtraClose2" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">
                        <asp:Literal ID="ltrExtraField2Title" runat="server"></asp:Literal></h4>
                </div>
                <div id="at_model_ef2_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>
                                <asp:Literal ID="ltrExtraField2Title2" runat="server"></asp:Literal></label>
                            <asp:TextBox ID="txtExtraField2Value" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:DropDownList ID="cboExtraField2Value" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdExtra2Save" runat="server" CssClass="btn btn-primary pull-right" Text="Update" OnClick="cmdExtra2Save_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="cmdUpdateEL2" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="cmdUpdateEL2_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuUpdateEL2" PopupControlID="pnlUpdateEL2" TargetControlID="cmdUpdateEL2" CancelControlID="cmdCloseUpdateEL2" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlUpdateEL2" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_uel2_indent" class="at_modelpopup_indent">
            <div id="at_model_uel2_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseUpdateEL2" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Update
                        <asp:Literal ID="ltrEL2_15" runat="server"></asp:Literal></h4>
                </div>
                <div id="at_model_uel2_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>
                                <asp:Literal ID="ltrEL2_14" runat="server"></asp:Literal></label>
                            <asp:DropDownList ID="cboUpdateEL2" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdChangeEL2" runat="server" CssClass="btn btn-primary pull-right" Text="Update" OnClick="cmdChangeEL2_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndUpdateQueue" runat="server" />
    <asp:ModalPopupExtender ID="hndUpdateQueue_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuUpdateQueue" PopupControlID="pnlUpdateQueue" TargetControlID="hndUpdateQueue" CancelControlID="cmdCloseUpdateQueue" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlUpdateQueue" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_queue_indent" class="at_modelpopup_indent">
            <div id="at_model_queue_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseUpdateQueue" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Update Task Queue</h4>
                </div>
                <div id="at_model_queue_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Task Queue</label>
                            <asp:DropDownList ID="cboQueue" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdChaangeQueue" runat="server" CssClass="btn btn-primary pull-right" Text="Update" OnClick="cmdChaangeQueue_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndReplaceFile" runat="server" />
    <asp:ModalPopupExtender ID="hndReplaceFile_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuReplaceFile" PopupControlID="pnlReplaceFile" TargetControlID="hndReplaceFile" CancelControlID="cmdCloseReplaceFile" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlReplaceFile" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_replace_file_indent" class="at_modelpopup_indent">
            <div id="at_model_replace_file_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseReplaceFile" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Replace File</h4>
                </div>
                <div id="at_model_replace_file_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div id="replceSource" class="form-group">
                            <label>File to Replace</label>
                            <asp:DropDownList ID="cboFileSource" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div id="replceResult" class="form-group">
                            <label>File to Replace</label>
                            <asp:DropDownList ID="cboFileResult" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>New File</label>
                            <asp:FileUpload ID="fulNewFile" runat="server" CssClass="form-control st_file_upload" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdReplaceFile" runat="server" CssClass="btn btn-primary pull-right" Text="Upload" OnClick="cmdReplaceFile_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField runat="server" ID="hndDeleteAttachment" ClientIDMode="Static" Value="" />
    <asp:ModalPopupExtender ID="hndDeleteAttachment_ModalPopupExtender" runat="server" Enabled="true" PopupControlID="pnlDeleteAttachment" BehaviorID="mpuDeleteAttachment" TargetControlID="hndDeleteAttachment" BackgroundCssClass="at_modelpopup_background_2" CancelControlID="cmdDelAttchmentNo">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlDeleteAttachment" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_del_doc_indent" class="at_modelpopup_indent">
            <div id="at_model_del_doc_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete - Attachment</h4>
                </div>
                <div id="at_model_del_doc_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you Sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDelAttchmentYes" runat="server" CssClass="btn btn-primary" Text="Yes" OnClick="cmdDelAttchmentYes_Click" />
                    <asp:Button ID="cmdDelAttchmentNo" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField runat="server" ID="hndDeleteAddon" ClientIDMode="Static" Value="" />
    <asp:ModalPopupExtender ID="hndDeleteAddon_ModalPopupExtender" runat="server" Enabled="true" PopupControlID="pnlDeleteAddon" BehaviorID="mpuDeleteAddon" TargetControlID="hndDeleteAddon" BackgroundCssClass="at_modelpopup_background_2" CancelControlID="cmdDeleteAddonNo">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlDeleteAddon" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_del_addon_indent" class="at_modelpopup_indent">
            <div id="at_model_del_addon_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete - Task Addon</h4>
                </div>
                <div id="at_model_del_addon_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you Sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDeleteAddon" runat="server" CssClass="btn btn-primary" Text="Yes" OnClick="cmdDeleteAddon_Click" />
                    <asp:Button ID="cmdDeleteAddonNo" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField runat="server" ID="hndDeleteComment" ClientIDMode="Static" Value="" />
    <asp:ModalPopupExtender ID="DeleteComment_ModalPopupExtender" runat="server" Enabled="true" PopupControlID="pnlDeleteComment" BehaviorID="mpuDeleteComment" TargetControlID="hndDeleteComment" BackgroundCssClass="at_modelpopup_background_2" CancelControlID="cmdDeleteCommentNo">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlDeleteComment" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_del_com_indent" class="at_modelpopup_indent">
            <div id="at_model_del_com_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete - Task Comment</h4>
                </div>
                <div id="at_model_del_com_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you Sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDeleteComment" runat="server" CssClass="btn btn-primary" Text="Yes" OnClick="cmdDeleteComment_Click" />
                    <asp:Button ID="cmdDeleteCommentNo" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndIsStartSubFromAtt" runat="server" />
    <asp:HiddenField ID="hndSubTask" runat="server" />
    <asp:ModalPopupExtender ID="hndSubTask_ModalPopupExtender" runat="server" Enabled="true" PopupControlID="pnlSubTaskStart" BehaviorID="mpuAddSub" TargetControlID="hndSubTask" CancelControlID="cmdSwfClose" BackgroundCssClass="at_modelpopup_background_2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlSubTaskStart" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_swf_indent" class="at_modelpopup_indent">
            <div id="at_model_swf_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdSwfClose" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Start Sub Tasks</h4>
                </div>
                <div id="at_model_swf_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Sub Workflow</label>
                            <asp:DropDownList ID="cboSubWorkflow" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Number of Sub Tasks To Start</label>
                            <asp:TextBox ID="txtNoSubOfTasks" runat="server" CssClass="form-control" Min="0" Max="99" TextMode="Number"></asp:TextBox>
                        </div>
                        <div id="divExtraField1Sub" class="form-group">
                            <label id="ex1Naming"></label>
                            <asp:TextBox ID="txtSubTaskExtraFieldValue1" runat="server" CssClass="form-control" Placeholder="123|234"></asp:TextBox>
                            <select id="cboSubTaskExtraFieldValue1" class="form-control" onchange="CopySubWFEx1Data();"></select>
                        </div>
                        <div id="divExtraField2Sub" class="form-group">
                            <label id="ex2Naming"></label>
                            <asp:TextBox ID="txtSubTaskExtraFieldValue2" runat="server" CssClass="form-control" Placeholder="123|234"></asp:TextBox>
                            <select id="cboSubTaskExtraFieldValue2" class="form-control" onchange="CopySubWFEx1Data();"></select>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdInitiateSubTask" runat="server" CssClass="btn btn-primary" Text="Initiate Sub Task" OnClick="cmdInitiateSubTask_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndAttachDoc_ID" runat="server" />
    <asp:HiddenField ID="hndSubTaskFromDoc" runat="server" />
    <asp:ModalPopupExtender ID="hndSubTaskFromDoc_ModalPopupExtender" runat="server" Enabled="true" PopupControlID="pnlSubTaskFromDoc" BehaviorID="mpuSubTaskFromDoc" TargetControlID="hndSubTaskFromDoc" CancelControlID="cmdSubTaskFromDocClose" BackgroundCssClass="at_modelpopup_background_2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlSubTaskFromDoc" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_stfd_indent" class="at_modelpopup_indent">
            <div id="at_model_stfd_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdSubTaskFromDocClose" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Sub Tasks From Document</h4>
                </div>
                <div id="at_model_stfd_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Sub Workflow</label>
                                    <asp:DropDownList ID="cboSubWFForDocs" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div id="divSubFromDoc" class="col-md-12">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSubTaskFromDoc" runat="server" CssClass="btn btn-primary" Text="Initiate Sub Tasks" OnClick="cmdSubTaskFromDoc_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="cmdValidateAddonFieldHidden" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="cmdValidateAddonFieldHidden_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlAddonFieldValidation" BehaviorID="mpuAddonFieldValidation" TargetControlID="cmdValidateAddonFieldHidden" CancelControlID="cmdAddonAvoid" BackgroundCssClass="at_modelpopup_background_2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlAddonFieldValidation" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_addon_nomatch_indent" class="at_modelpopup_indent">
            <div id="at_model_addon_nomatch_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Field Validation Error</h4>
                </div>
                <div id="at_model_addon_nomatch_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Some fields don't match with Original Data, Continue?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdAddonContinue" runat="server" CssClass="btn btn-primary" Text="Yes" OnClick="cmdAddonContinue_Click" />
                    <asp:Button ID="cmdAddonAvoid" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="cmdValidateFieldHidden" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="cmdValidateFieldHidden_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlFieldValidation" BehaviorID="mpuFieldValidation" TargetControlID="cmdValidateFieldHidden" CancelControlID="cmdAvoid" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlFieldValidation" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_nomatch_indent" class="at_modelpopup_indent">
            <div id="at_model_nomatch_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Field Validation Error</h4>
                </div>
                <div id="at_model_nomatch_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Some fields don't match with Original Data, Continue?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdContinue" runat="server" CssClass="btn btn-primary" Text="Yes" OnClick="cmdContinue_Click" />
                    <asp:Button ID="cmdAvoid" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="cmdValidateFieldHiddenSpecial" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="cmdValidateFieldHiddenSpecial_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlFieldValidationSpecial" BehaviorID="mpuFieldValidationSpecial" TargetControlID="cmdValidateFieldHiddenSpecial" CancelControlID="cmdAvoidSpecial" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlFieldValidationSpecial" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_bulk_nomatch_indent" class="at_modelpopup_indent">
            <div id="at_model_bulk_nomatch_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Field Validation Error</h4>
                </div>
                <div id="at_model_bulk_nomatch_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Some fields don't match with Original Data, Continue?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdContinueSpecial" runat="server" CssClass="btn btn-primary" Text="Yes" OnClick="cmdContinueSpecial_Click" />
                    <asp:Button ID="cmdAvoidSpecial" runat="server" Text="No" CssClass="btn btn-default" />
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
    <asp:Button ID="cmdEL2Hierarchy" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="cmdEL2Hierarchy_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlEL2Hierarchy" BehaviorID="mpuEL2Hierarchy" TargetControlID="cmdEL2Hierarchy" CancelControlID="cmdCancelEL2Hierarchy" BackgroundCssClass="at_modelpopup_background_2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlEL2Hierarchy" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_el2h_indent" class="at_modelpopup_indent">
            <div id="at_model_el2h_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <asp:Literal ID="ltrEL2_3" runat="server"></asp:Literal>
                        Structure</h4>
                </div>
                <div id="at_model_el2h_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="timeline" style="padding-bottom: 0px; margin-bottom: 0px">
                            <asp:Literal ID="ltrParentEL2" runat="server"></asp:Literal>
                            <div class="tl-entry left">
                                <div class="tl-icon bg-dark-gray"><i class="fa fa-check"></i></div>
                                <div class="panel tl-body">
                                    <h4 class="text-info">Current Level</h4>
                                    <asp:Literal ID="ltrCurrEL2" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="tl-entry left">
                                <div class="tl-icon bg-success"><i class="fa fa-check"></i></div>
                                <div class="panel tl-body">
                                    <h4 class="text-info">Subodinate
                                        <asp:Literal ID="ltrEL2_4" runat="server"></asp:Literal></h4>
                                    <asp:Literal ID="ltrSubordinateEl2" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCancelEL2Hierarchy" runat="server" Text="Close" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="cmdSpecialData" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="cmdSpecialData_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlSpecialData" BehaviorID="mpuSpecialData" TargetControlID="cmdSpecialData" CancelControlID="cmdCancelSpecialData" BackgroundCssClass="at_modelpopup_background_2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlSpecialData" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_spcdata_indent" class="at_modelpopup_indent">
            <div id="at_model_spcdata_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Special Task Data</h4>
                </div>
                <div id="at_model_spcdata_content" class="at_modelpopup_content styled-bar">
                    <div id="divSpecialData" runat="server" class="panel-body">
                        Special Data Here...
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCancelSpecialData" runat="server" Text="Close" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndHelp" runat="server" />
    <asp:ModalPopupExtender ID="hndHelp_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlHelp" BehaviorID="mpuHelp" TargetControlID="hndHelp" CancelControlID="cmdCancelHelp" BackgroundCssClass="at_modelpopup_background_2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlHelp" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_help_indent" class="at_modelpopup_indent">
            <div id="at_model_help_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 id="help_title" class="panel-title">Help Instructions</h4>
                </div>
                <div id="at_model_help_content" class="at_modelpopup_content styled-bar">
                    <div id="divFieldHelp" class="panel-body"></div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCancelHelp" runat="server" Text="Close" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndAddonHelp" runat="server" />
    <asp:ModalPopupExtender ID="hndAddonHelp_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlAddonHelp" BehaviorID="mpuAddonHelp" TargetControlID="hndAddonHelp" CancelControlID="cmdCancelAddonHelp" BackgroundCssClass="at_modelpopup_background_2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlAddonHelp" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_addon_help_indent" class="at_modelpopup_indent">
            <div id="at_model_addon_help_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 id="addon_help_title" class="panel-title">Help Instructions</h4>
                </div>
                <div id="at_model_addon_help_content" class="at_modelpopup_content styled-bar">
                    <div id="divAddonFieldHelp" class="panel-body"></div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCancelAddonHelp" runat="server" Text="Close" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndDocHelp" runat="server" />
    <asp:ModalPopupExtender ID="hndDocHelp_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDocHelp" BehaviorID="mpuDocHelp" TargetControlID="hndDocHelp" CancelControlID="cmdCancelDocHelp" BackgroundCssClass="at_modelpopup_background_2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlDocHelp" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_doc_help_indent" class="at_modelpopup_indent">
            <div id="at_model_doc_help_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 id="doc_help_title" class="panel-title">Help Instructions</h4>
                </div>
                <div id="at_model_doc_help_content" class="at_modelpopup_content styled-bar">
                    <div id="divDocFieldHelp" class="panel-body"></div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCancelDocHelp" runat="server" Text="Close" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlEL2" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_el2_indent" class="at_modelpopup_indent">
            <div id="at_model_el2_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdNoEL2" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add
                        <asp:Literal ID="ltrEL2_10" runat="server"></asp:Literal></h4>
                </div>
                <div id="at_model_el2_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <asp:HiddenField ID="hndEL2_ID" runat="server" />
                        <div class="tab-base">
                            <ul class="nav nav-tabs nav-tabs-simple">
                                <li id="tab1" class="active">
                                    <a data-toggle='tab' href='#tab_cont1'>Main Info</a>
                                </li>
                                <li id="tab3">
                                    <a data-toggle='tab' href='#tab_cont3'>Workflows</a>
                                </li>
                                <li id="tab2">
                                    <a data-toggle='tab' href='#tab_cont2'>Users</a>
                                </li>
                                <li id="tab4">
                                    <a data-toggle='tab' href='#tab_cont4'>Structure</a>
                                </li>
                            </ul>
                            <div class="panel panel-info no-margin-b">
                                <div class="panel-body no-padding">
                                    <div class="tab-content grid-with-paging">
                                        <div id="tab_cont1" class="tab-pane fade active in">
                                            <div class="form-group">
                                                <label>Folder Name</label>
                                                <asp:TextBox ID="txtFolder" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>
                                                    <asp:Literal ID="ltrEL2_8" runat="server"></asp:Literal>
                                                    Code</label>
                                                <asp:TextBox ID="txtEntityCode" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Diaplay Name</label>
                                                <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Legal Name</label>
                                                <asp:TextBox ID="txtLegalName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Description</label>
                                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Height="80"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Street or PO Box</label>
                                                <asp:TextBox ID="txtPHStreet" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Town / City</label>
                                                <asp:TextBox ID="txtPHTown" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>State / Region</label>
                                                <asp:TextBox ID="txtPHState" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Main Contact Person</label>
                                                <asp:TextBox ID="txtMainContact" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Phone</label>
                                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>E-mail</label>
                                                <asp:TextBox ID="txtE_Mail" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Website</label>
                                                <asp:TextBox ID="txtWebSite" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>
                                                    Parent
                                                    <asp:Literal ID="ltrEL2_6" runat="server"></asp:Literal>
                                                    Name</label>
                                                <asp:DropDownList ID="cboEntity_Level_2" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label>
                                                    <asp:Literal ID="ltrEL1" runat="server"></asp:Literal></label>
                                                <asp:DropDownList ID="cboEntity_Level_1" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label>Default Letterhead</label>
                                                <asp:DropDownList ID="cboLetterHead" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div id="tab_cont3" class="tab-pane fade">
                                            <div class="form-group-margin text-right">
                                                <button class="btn btn-labeled btn-primary" onclick="return selectAllWF();">Select All</button>
                                                <button class="btn btn-labeled btn-primary" onclick="return deSelectAllWF();">Unselect All</button>
                                            </div>
                                            <asp:HiddenField ID="hndWorkflows" ClientIDMode="Static" runat="server" />
                                            <div class="table-responsive table-primary no-margin-b">
                                                <asp:GridView ID="gvWorkflows" runat="server" CssClass="table table-striped table-hover grid_table grid_wf" AutoGenerateColumns="False" OnRowDataBound="gvWorkflows_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="Walkflow_ID" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxlist wfs" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="22px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Workflow_Name" HeaderText="Workflow Name" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div id="tab_cont2" class="tab-pane fade">
                                            <div class="col-md-12 hp0">
                                                <div class="alert alert-success mb10">
                                                    <strong>Required User Groups :</strong>
                                                    <div id="divUserGroups" runat="server"></div>
                                                </div>
                                            </div>
                                            <div class="form-group-margin text-right">
                                                <button class="btn btn-labeled btn-primary" onclick="return selectAllUsers();">Select All</button>
                                                <button class="btn btn-labeled btn-primary" onclick="return deSelectAllUsers();">Unselect All</button>
                                            </div>
                                            <asp:HiddenField ID="hndUsers" ClientIDMode="Static" runat="server" />
                                            <div class="table-responsive table-primary no-margin-b">
                                                <asp:GridView ID="gvUsers" runat="server" CssClass="table table-striped table-hover grid_table grid_users" AutoGenerateColumns="False" OnRowDataBound="gvUsers_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="User_ID" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxlist users" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="22px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Full_Name" HeaderText="Name" />
                                                        <asp:BoundField DataField="Groups" HeaderText="Member Of" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div id="tab_cont4" class="tab-pane fade" style="padding: 20px 50px 20px 0">
                                            <div class="timeline" style="padding-bottom: 0px; margin-bottom: 0px">
                                                <div id="strParents"></div>
                                                <div class="tl-entry left">
                                                    <div class="tl-icon bg-dark-gray"><i class="fa fa-check"></i></div>
                                                    <div class="panel tl-body">
                                                        <h4 class="text-info">Current Level</h4>
                                                        <div id="currCompany"></div>
                                                    </div>
                                                </div>
                                                <div class="tl-entry left">
                                                    <div class="tl-icon bg-success"><i class="fa fa-check"></i></div>
                                                    <div class="panel tl-body">
                                                        <h4 class="text-info">Subodinate
                                                            <asp:Literal ID="ltrEL2_7" runat="server"></asp:Literal></h4>
                                                        <div id="subCompanies"></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSaveEL2" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSaveEL2_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndFlowChart" runat="server" />
    <asp:ModalPopupExtender ID="cmdFlowchart_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuFlowchart" PopupControlID="pnlFlowchart" TargetControlID="hndFlowChart" CancelControlID="cmdCloseFL" BackgroundCssClass="at_modelpopup_background_2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlFlowchart" runat="server" CssClass="at_modelpopup_container">
        <div id="at_model_flch_indent" class="at_modelpopup_indent">
            <div id="at_model_flch_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Flowchart</h4>
                </div>
                <div id="at_model_flch_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <asp:Literal ID="ltrFlowChartSteps" runat="server"></asp:Literal>
                        <div class="row">
                            <div class="col-md-12" style="max-height: 500px; overflow: scroll">
                                <div class="flowchart">
                                    <asp:Literal ID="ltrFlowChart" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCloseFL" runat="server" Text="Close" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndDoc_Proj_ID" runat="server" />
    <asp:Panel ID="pnlDP_Upload" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_dpu_indent" class="at_modelpopup_indent">
            <div id="at_model_dpu_inner_indent" class="panel at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseDP_Upload" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Upload to
                        <asp:Literal ID="ltrDocProjectName" runat="server"></asp:Literal></h4>
                </div>
                <div id="at_model_dpu_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>File</label>
                            <asp:FileUpload ID="fulDocumentDP" runat="server" CssClass="form-control st_file_upload" />
                        </div>
                        <div class="form-group">
                            <label>File Name</label>
                            <asp:TextBox ID="txtFileNameDP" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Access Level</label>
                            <asp:DropDownList ID="cboAccessLevelDP" runat="server" CssClass="form-control">
                                <asp:ListItem Value="3">Level 3</asp:ListItem>
                                <asp:ListItem Value="2">Level 2</asp:ListItem>
                                <asp:ListItem Value="1">Level 1</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <h3>Tags</h3>
                        </div>
                        <div id="divTags" runat="server"></div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDP_Upload" runat="server" Text="Upload" CssClass="btn btn-primary" OnClick="cmdDP_Upload_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndStepDeadline" runat="server" ClientIDMode="Static" />
    <asp:ModalPopupExtender ID="hndStepDeadline_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuStepDeadline" PopupControlID="pnlStepDeadline" TargetControlID="hndStepDeadline" CancelControlID="cmdCloseStepDeadline" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlStepDeadline" runat="server" CssClass="at_modelpopup_container">
        <div id="at_model_sdl_indent" class="at_modelpopup_indent">
            <div id="at_model_sdl_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCloseStepDeadline" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Edit Step Deadlines</h4>
                </div>
                <div id="at_model_sdl_content" class="at_modelpopup_content styled-bar">
                    <div id="divStepDeadlines" class="panel-body"></div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSaveStepDeadlines" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="cmdSaveStepDeadlines_Click" />
                    <asp:Button ID="cmdRecalcStepDeadlines" runat="server" Text="Recalculate" CssClass="btn btn-default" />
                    <asp:Button ID="cmdResetStepDeadlines" runat="server" Text="Reset" CssClass="btn btn-default" />
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
    <script src="assets/javascripts/flowchart/raphael.min.js"></script>
    <script src="assets/javascripts/flowchart/flowchart.min.js"></script>
    <script src="assets/javascripts/flowchart/jquery.flowchart.js"></script>
    <%=Dock_JS_Init%>
    <script type="text/javascript">
        <%=ChartScript%>

        $(function () {
            $(window).resize(function () {
                AdjustPopups();
            });
            $(window).focus(function () {
                checkPause();
            });
        });

        init.push(function () {
            window.onbeforeunload = DisableLink;
            AdjustPopups();
            ArrangeTaskChart();
            ArrangeMessageLoading();
            ArrangeDropZone();
            ArrangeGrids();
            ArrangeTooltip();
            FindNextStep();
            <%=LoadingScripts%>
        });

        function DisableLink() {
            if (document.getElementById("<%=cmdCloseMain.ClientID%>") != null) {
                document.getElementById("<%=cmdCloseMain.ClientID%>").disabled = true;
            }
            if (document.getElementById("<%=cmdInitiateSubTask.ClientID%>") != null) {
                document.getElementById("<%=cmdInitiateSubTask.ClientID%>").disabled = true;
            }
            if (document.getElementById("<%=cmdReject.ClientID%>") != null) {
                document.getElementById("<%=cmdReject.ClientID%>").disabled = true;
            }
            if (document.getElementById("<%=cmdAddSub.ClientID%>") != null) {
                document.getElementById("<%=cmdAddSub.ClientID%>").disabled = true;
            }
            if (document.getElementById("<%=cmdAddSub.ClientID%>") != null) {
                document.getElementById("<%=cmdAddSub.ClientID%>").disabled = true;
            }
            if (document.getElementById("<%=cmdSubmitSpecial.ClientID%>") != null) {
                document.getElementById("<%=cmdSubmitSpecial.ClientID%>").disabled = true;
            }
            document.getElementById("<%=cmdSubmit.ClientID%>").disabled = true;
            document.getElementById("<%=cmdSave.ClientID%>").disabled = true;
        }

        function AdjustPopups() {
            <%=EL2PanelResizeScript%>
            <%=QueuePanelResizeScript%>
            <%=HelpPanelResizeScript%>
            <%=AddonHelpPanelResizeScript%>
            <%=DocHelpPanelResizeScript%>
            <%=UpdateEL2ResizeScript%>
            <%=AddonPanelResizeScript%>
            <%=TaskDeadlineResizeScript%>
            <%=StepListPanelResizeScript%>
            <%=ChatPanelResizeScript%>
            <%=DocReplaceResizeScript%>
            AdjustPopupSize(80, 400, 'at_model_reg');
            AdjustPopupSize(80, 1200, 'at_model_file');
            AdjustPopupSize(80, 1200, 'at_model_com')
            AdjustPopupSize(80, 800, 'at_model_dpu');
            AdjustPopupSize(80, 1200, 'at_model_doccom');
            AdjustPopupSize(80, 800, 'at_model_docinfo');
            AdjustPopupSize(80, 600, 'at_model_tr');
            AdjustPopupSize(80, 600, 'at_model_ef');
            AdjustPopupSize(80, 600, 'at_model_ef2');
            AdjustPopupSize(80, 800, 'at_model_swf');
            AdjustPopupSize(80, 600, 'at_model_nomatch');
            AdjustPopupSize(80, 600, 'at_model_addon_nomatch');
            AdjustPopupSize(80, 600, 'at_model_bulk_nomatch');
            AdjustPopupSize(80, 600, 'at_model_doc_anchor');
            AdjustPopupSize(80, 600, 'at_model_guest_post');
            AdjustPopupSize(80, 1200, 'at_model_add_doc_link');
            AdjustPopupSize(80, 400, 'at_del_model_doc_link');
            AdjustPopupSize(80, 400, 'at_model_del_doc');
            AdjustPopupSize(80, 400, 'at_model_del_addon');
            AdjustPopupSize(80, 400, 'at_model_del_com');
            AdjustPopupSize(80, 400, 'at_model_cans');
            AdjustPopupSize(80, 400, 'at_model_rel');
            AdjustPopupSize(200, 1500, 'at_model_tsklst');
            AdjustPopupSize(167, 1200, 'at_model_vc');
            AdjustPopupSize(167, 1200, 'at_model_vp');
            AdjustPopupSize(167, 1200, 'at_model_va');
            AdjustPopupSize(167, 1800, 'at_model_vd');
            AdjustPopupSize(167, 1000, 'at_model_el2h');
            AdjustPopupSize(167, 600, 'at_model_spcdata');
            AdjustPopupSize(167, 1800, 'at_model_flch');
            AdjustPopupSize(167, 800, 'at_model_stfd');
            AdjustMessagePopup();
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
                        formData.append('reup', 'false');
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
            $.fn.dataTable.ext.errMode = 'throw';

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
                order: [[<%=(ListSort == 1 ? "2" : "8")%>,  '<%=(ListSortDir ? "asc" : "desc")%>']],
                responsive: true,
                autoWidth: true,
                processing: true,
                serverSide: true,
                ajax:
                {
                    url: "api/tasks/GetSubTasks",
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
                    { data: "Check_Box" },
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
                    { orderable: false, width:"22px", targets: 1 },
                    { orderable: true, className: 'progress_cell', targets: 7 }
                ],
                "fnDrawCallback": function(oSettings, json) {
                    AdjustGridResp();
                }
            });
            setInterval(function () {
                $('#tblSubTasks').DataTable().ajax.reload(null, false);
            }, <%=RefFreq%>);

            <%=NewEL2Script%>

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

        function ArrangeTaskChart() {
            <%=ChartLoadScript%>
        }

        function ArrangeMessageLoading() {
            <%=Chat_Arrange_Script%>
        }

        function ValidateAgainstAddonField() {
            if (ValidatRequiredAddonFields() == true) {
                if (ValidateWithOldAddonField() == true) {
                    return true;
                }
                else {
                    $find('mpuAddonFieldValidation').show();
                    return false;
                }
            }
            else {
                return false;
            }
        }

        function ValidateAgainstField() {
            if (ValidatRequiredFields() == true) {
                if (ValidateWithOldField() == true) {
                    return true;
                }
                else {
                    $find('mpuFieldValidation').show();
                    return false;
                }
            }
            else {
                return false;
            }
        }

        function ValidateAgainstFieldSpecial() {
            if (ValidatRequiredFields() == true) {
                if (ValidateWithOldField() == true) {
                    return true;
                }
                else {
                    $find('mpuFieldValidationSpecial').show();
                    return false;
                }
            }
            else {
                return false;
            }
        }

        function ReplaceFile(replaceType) {
            if (replaceType == 0) {
                $('#replceSource').removeClass('hide');
                $('#replceResult').addClass('hide');
                $('#<%=hndReplaceFile.ClientID%>').val('0');
            }
            else {
                $('#replceSource').addClass('hide');
                $('#replceResult').removeClass('hide');
                $('#<%=hndReplaceFile.ClientID%>').val('1');
            }
            $find('mpuReplaceFile').show();
            return false;
        }

        function DeleteAttachment(attID) {
            $('#<%=hndDeleteAttachment.ClientID%>').val(attID);
            $find('mpuDeleteAttachment').show();
            return false;
        }

        function DeleteAddon(addonID) {
            $('#<%=hndDeleteAddon.ClientID%>').val(addonID);
            $find('mpuDeleteAddon').show();
            return false;
        }

        function DeleteComment(commentID) {
            $('#<%=hndDeleteComment.ClientID%>').val(commentID);
            $find('mpuDeleteComment').show();
            return false;
        }

        function ClearCommentControls() {
            clearTextBox(['<%=txtCommentMain.ClientID%>']);
            clearDropDown(['<%=cboPriority.ClientID%>', '<%=cboCommentType.ClientID%>']);
            return false;
        }

        function ClearUploadControls() {
            clearDropDown(['<%=cboFileTypesUpload.ClientID%>', '<%=cboAccessLevel.ClientID%>']);
            clearTextBox(['<%=txtFileName.ClientID%>']);
            $('#<%=hndIsResult.ClientID%>').val('0');
            $('#<%=lblRType.ClientID%>').html('Source Document');

            return false;
        }

        function ClearUploadControls2() {
            clearDropDown(['<%=cboFileTypesUpload.ClientID%>', '<%=cboAccessLevel.ClientID%>']);
            clearTextBox(['<%=txtFileName.ClientID%>']);
            $('#<%=hndIsResult.ClientID%>').val('1');
            $('#<%=lblRType.ClientID%>').html('Output Document');
            return false;
        }

        function UpdateRType(RType) {
            clearDropDown(['<%=cboAccessLevelDP.ClientID%>']);
            clearTextBox(['<%=txtFileNameDP.ClientID%>']);
            $('#<%=hndIsResultLink.ClientID%>').val(RType);
        }

        function ShowAnchorLink() {
            $find("mpuAnchor").show();
            return false;
        }

        function ShowGuestPostLink() {
            $find("mpuGuestPost").show();
            return false;
        }

        function ShowExtraField() {
            $find("mpuExtraField").show();
            return false;
        }

        function ShowExtraField2() {
            $find("mpuExtraField2").show();
            return false;
        }

        function ShowDueDate() {
            $find("mpuDueDate").show();
            return false;
        }

        function ShowEditEL2() {
            $find("mpuUpdateEL2").show();
            return false;
        }

        function ShowQueue() {
            $find("mpuUpdateQueue").show();
            return false;
        }

        function ShowEL2Structure() {
            $find("mpuEL2Hierarchy").show();
            return false;
        }

        function ShowSpecialData() {
            $find("mpuSpecialData").show();
            return false;
        }

        function CloseCommentView() {
            $find("mpuViewComment").hide();
            return false;
        }

        function CloseTaskProgress() {
            $find("mpuTaskProgress").hide();
            return false;
        }

        function CloseTaskAddons() {
            $find("mpuTaskAddons").hide();
            return false;
        }

        function ShowStepsList() {
            $find("mpuStepsList").show();
            return false;
        }

        function saveLink() {
            document.getElementById('<%=cmdSaveAddLinkhnd.ClientID%>').click();
        }

        function showLinkDel(attID) {
            $('#<%=hndDeleteLink.ClientID %>').val(attID);
            $find("mpuDeleteLink").show();
            return false;
        }

        function showErrorSp() {
            ShowWarning('This Task is locked by another user <br> Contact the Systems Administrator to Unlock the Task');
            return false;
        }

        function LoadMessage(thread_id) {
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
                    resultObject[i].Delete_Comment.trim() +
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

        function closeDocView() {
            $find("mpuDocumentView2").hide();
            $find("mpuDocumentView1").hide();
            return false;
        }

        function LoadDocumentsForType(docType) {
            $('#<%=cboDocType.ClientID%>').val(docType);
            LoadDocuments();
        }

        function LoadDocuments() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetTaskDocuments",
                data: { Task_ID: $('#<%=hndTaskID.ClientID%>').val(), DocType: $('#<%=cboDocType.ClientID%>').val(), DocCategory: $('#<%=cboDocCategories.ClientID%>').val(), StartSub: $('#<%=hndIsStartSubFromAtt.ClientID%>').val() },
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
                    resultObject[i].Delete_Link +
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
                data: { Task_ID: <%=Convert.ToString(ViewState["tid"])%>, Task_Doc_ID: Doc_ID },
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
                    strDocComments = strDocComments + "<div class=\"comment no-padding-b no-border\">" +
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
                data: { Task_ID: <%=Convert.ToString(ViewState["tid"])%>, Task_Doc_ID: Doc_ID },
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

        var FlowChartViewed = false;
        function drawFlowchart() {
            $find("mpuFlowchart").show();

            if (FlowChartViewed == false) {
                $(".flowchart").flowChart();
                FlowChartViewed = true;
            }
            return false;
        }

        function checkPause() {
            $.ajax({
                type: "GET",
                url: "api/tasks/CheckPauseTask",
                data: { Task_ID: <%=Convert.ToString(ViewState["tid"])%>, CSID: <%=ViewState["csid"]%>, TUID: <%=ViewState["tuid"]%> },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: CheckPauseTaskPassed,
                failure: LoadValuesFailed
            });
        }

        function CheckPauseTaskPassed(result) {
            var resultObject = result;
            if (resultObject.trim() == "Posted") {
                $('#<%=cmdGotoTaskList.ClientID%>').click();
            }
            else if (resultObject.trim() == "Paused") {
                $('#cmdPause').removeClass('btn-primary');
                $('#cmdPause').addClass('btn-warning');
                $('#cmdPause').html("<span class='btn-label icon fa fa-play'></span>Resume");
            }
            else {
                $('#cmdPause').addClass('btn-primary');
                $('#cmdPause').removeClass('btn-warning');
                $('#cmdPause').html("<span class='btn-label icon fa fa-pause'></span>Pause");
            }
        }

        function pauseTask() {
            if ($('#cmdPause').hasClass('btn-primary')) {
                $('#cmdPause').removeClass('btn-primary');
                $('#cmdPause').addClass('btn-warning');
                $('#cmdPause').html("<span class='btn-label icon fa fa-play'></span>Resume");
                $.ajax({
                    type: "GET",
                    url: "api/tasks/PauseTask",
                    data: { Task_ID: <%=Convert.ToString(ViewState["tid"])%> },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: pauseTaskPassed,
                    failure: LoadValuesFailed
                });
            }
            else {
                $('#cmdPause').addClass('btn-primary');
                $('#cmdPause').removeClass('btn-warning');
                $('#cmdPause').html("<span class='btn-label icon fa fa-pause'></span>Pause");
                $.ajax({
                    type: "GET",
                    url: "api/tasks/ResumeTask",
                    data: { Task_ID: <%=Convert.ToString(ViewState["tid"])%> },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: pauseTaskPassed,
                    failure: LoadValuesFailed
                });
            }
            return false;
        }

        function pauseTaskPassed(result) {
            var resultObject = result;
        }

        function GetFiles(type) {
            $.ajax({
                type: "GET",
                url: "api/tasks/ZipDocs",
                data: { Task_ID:<%=Convert.ToString(ViewState["tid"])%>, Type: type },
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
                ShowError("No Files to Download");
            }
            else {
                openNewTab('document_preview_temp.aspx?d=' + fileName);
            }
        }

        function ArrangeTooltip() {
            $(".stt").tooltip();
            $(".strj").tooltip();
            $('.ttip').tooltip();
        }

        function FindNextStep() {
            <%=Next_Step_Script%>
        }

        function SetNextStepText(StepName) {
            $(".stt").attr("data-original-title", StepName);
        }

        function OpenTask(control) {
            openPage("task.aspx?tid=" + $(control).attr("data-id") + "&bck=<%=CurrentPageURL%>");
            return false;
        }

        <%=Task_Deadlines_Script%>

        <%=Sub_Task_Script%>

        <%=Task_Script%>

        <%=NewEL2Script2%>

        <%=HelpScript%>

        <%=AddonHelpScript%>

        <%=DocHelpScript%>

        <%=Required_Fields_dp%>

        <%=Old_Field_Validation%>

        <%=Required_Fields%>

        <%=Formula_Fields%>

        <%=Old_Addon_Field_Validation%>

        <%=Required_Addon_Fields%>

        <%=Custom_Scripts%>

        <%=Sub_Task_From_Doc_Script%>
    </script>
</asp:Content>
