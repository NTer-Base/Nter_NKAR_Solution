<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin.master" AutoEventWireup="true" CodeBehind="notice_board.aspx.cs" Inherits="N_Ter_Tasks.notice_board" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Test-Tasks > Notice Board
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
    <link href="assets/stylesheets/pages.min.css" rel="stylesheet" type="text/css">
    <link href="assets/galleries/css/swipebox.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Notice Board
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <asp:HiddenField ID="hndSlab" runat="server" />
    <asp:HiddenField ID="hndLastSeen" runat="server" />
    <asp:HiddenField ID="hndEaliestDate" runat="server" />
    <div class="row">
        <div class="col-md-12">
            <div class="btn-group pull-right">
                <button class="btn btn-labeled btn-primary" data-toggle="dropdown"><span class="btn-label icon fa fa-plus"></span>New Notice Post&nbsp;&nbsp;&nbsp;<i class="fa fa-caret-down"></i></button>
                <ul class="dropdown-menu dropdown-menu-right">
                    <li><a href="#" onclick="return NewNotice(1);" class="newItem">Note</a></li>
                    <li><a href="#" onclick="return NewNotice(2);" class="newItem">Pictures</a></li>
                    <li><a href="#" onclick="return NewNotice(3);" class="newItem">Documents</a></li>
                    <li><a href="#" onclick="return NewNotice(4);" class="newItem">URL</a></li>
                </ul>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12" style="min-height: 300px">
            <div id="notice_board" class="timeline">
                <asp:Literal ID="ltrNoticeBoard" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hndNotice_ID" runat="server" />
    <asp:ModalPopupExtender ID="hndNotice_ID_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuNoticePost" PopupControlID="pnlNoticePost" TargetControlID="hndNotice_ID" CancelControlID="cmdCancelPost" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlNoticePost" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_notice_indent" class="at_modelpopup_indent">
            <div id="at_notice_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Add New Post</h4>
                </div>
                <div id="at_notice_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Notice Type</label>
                            <asp:DropDownList ID="cboNoticeType" runat="server" CssClass="form-control">
                                <asp:ListItem Value="1">Note</asp:ListItem>
                                <asp:ListItem Value="2">Pictures</asp:ListItem>
                                <asp:ListItem Value="3">Documents</asp:ListItem>
                                <asp:ListItem Value="4">URL</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Title</label>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="row">
                            <div id="divDesc2" class="col-sm-3">
                                <asp:HiddenField ID="hndThumbPath" runat="server" />
                                <div class="form-group">
                                    <label>Thumb</label>
                                    <div id="divDesc3" style="overflow: hidden" class="text-center bordered"></div>
                                </div>
                            </div>
                            <div id="divDesc1" class="col-sm-9">
                                <div class="form-group">
                                    <label>Description</label>
                                    <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div id="divLoadingSub" class="row hide">
                            <div class="col-md-12 text-center">
                                <img src="assets/images/loading_2.gif" style="width: 70px" />
                            </div>
                        </div>
                        <div id="divURL" class="form-group">
                            <label>URL</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtURL" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:Button ID="cmdFetchData" runat="server" Text="Fetch URL Data" CssClass="btn btn-primary" OnClientClick="return fetchData();" />
                                </span>
                            </div>
                        </div>
                        <asp:HiddenField ID="hndUploadResponse" runat="server" />
                        <div id="divUploadImages" class="row">
                            <div class="col-md-12">
                                <label>Files</label>
                                <div id="dropzonejs1" class="dropzone-box form-group">
                                    <div class="dz-default dz-message">
                                        <i class="fa fa-cloud-upload"></i>
                                        Drop files in here<br>
                                        <span class="dz-text-small">or click to pick manually</span>
                                    </div>
                                    <div class="fallback">
                                        <input name="file" type="file" multiple="multiple" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divUploadOther" class="row">
                            <div class="col-md-12">
                                <label>Files</label>
                                <div id="dropzonejs2" class="dropzone-box form-group">
                                    <div class="dz-default dz-message">
                                        <i class="fa fa-cloud-upload"></i>
                                        Drop files in here<br>
                                        <span class="dz-text-small">or click to pick manually</span>
                                    </div>
                                    <div class="fallback">
                                        <input name="file" type="file" multiple="multiple" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdPostUpload1" runat="server" Text="Post" CssClass="btn btn-primary" />
                    <asp:Button ID="cmdPostUpload2" runat="server" Text="Post" CssClass="btn btn-primary" />
                    <asp:Button ID="cmdPost" runat="server" Text="Post" CssClass="btn btn-primary" OnClick="cmdPost_Click" />
                    <asp:Button ID="cmdCancelPost" runat="server" Text="Cencel" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndReadLikes" runat="server" />
    <asp:ModalPopupExtender ID="hndReadLikes_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuReadLikes" PopupControlID="pnlReadLikes" TargetControlID="hndReadLikes" CancelControlID="cmdCloseReadLikes" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlReadLikes" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_likes_indent" class="at_modelpopup_indent">
            <div id="at_model_likes_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title"><span id="spanLikeCount"></span> Like(s)</h4>
                </div>
                <div id="at_model_likes_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div id="divLikesList" class="row"></div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCloseReadLikes" runat="server" Text="Close" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndComments" runat="server" />
    <asp:ModalPopupExtender ID="hndComments_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuComments" PopupControlID="pnlComments" TargetControlID="hndComments" CancelControlID="cmdCommentsClose" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlComments" runat="server" CssClass="at_modelpopup_container" Style="display: none" DefaultButton="cmdDefaultEnter">
        <div id="at_model_comms_indent" class="at_modelpopup_indent">
            <div id="at_model_comms_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCommentsClose" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title"><span id="spanCommentsCount"></span> Comment(s)</h4>
                </div>
                <div id="at_model_comms_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div id="divCommentsList" class="row"></div>
                    </div>
                </div>
                <div class="panel-footer">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtComment" runat="server" class="form-control" placeholder="Type your comment here..." TextMode="MultiLine" Height="100"></asp:TextBox>
                        </div>
                        <div class="col-md-12 mt10 text-right">
                            <asp:Button ID="cmdAddComment" runat="server" class="btn btn-primary" Text="Add Comment" OnClick="cmdAddComment_Click" />
                            <asp:Button ID="cmdDefaultEnter" runat="server" CssClass="hidden" OnClientClick="return false;" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndDelPost" runat="server" />
    <asp:ModalPopupExtender ID="hndDelPost_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuDelPost" PopupControlID="pnlDelPost" TargetControlID="hndDelPost" CancelControlID="cmdCancelDelPost" BackgroundCssClass="at_modelpopup_background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlDelPost" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_post_indent" class="at_modelpopup_indent">
            <div id="at_del_post_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete Post</h4>
                </div>
                <div id="at_del_post_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDelPost" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDelPost_Click" />
                    <asp:Button ID="cmdCancelDelPost" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hndDelCom" runat="server" />
    <asp:ModalPopupExtender ID="hndDelCom_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuDelCom" PopupControlID="pnlDelCom" TargetControlID="hndDelCom" CancelControlID="cmdCancelDelCom" BackgroundCssClass="at_modelpopup_background_2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlDelCom" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_com_indent" class="at_modelpopup_indent">
            <div id="at_del_com_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete Comment</h4>
                </div>
                <div id="at_del_com_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDelCom" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDelCom_Click" />
                    <asp:Button ID="cmdCancelDelCom" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script src="assets/galleries/js/jquery.swipebox.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $(window).resize(function () {
                AdjustPopups();
            });
        });

        init.push(function () {
            AdjustPopups();
            ArrangeDropZone();
            ArrangeGalleries();
        });

        function AdjustPopups() {
            AdjustPopupSize(80, 1000, 'at_notice');
            AdjustPopupSize(80, 600, 'at_model_likes');
            AdjustPopupSize(80, 400, 'at_del_post');
            AdjustPopupSize(80, 400, 'at_del_com');
            AdjustPopupSizeSpecial(80, 1000, 100, 'at_model_comms');
        }

        var WallCall = false;

        $(window).scroll(function () {
            if ($(window).scrollTop() + $(window).height() > $(document).height() - 5) {
                if (WallCall == false) {
                    WallCall = true;
                    GetMoreWall();
                }
            }
        });

        function GetMoreWall() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetWall",
                data: { Slab: $('#<%=hndSlab.ClientID%>').val(), LastSeen: $('#<%=hndLastSeen.ClientID%>').val(), EarliestDate: $('#<%=hndEaliestDate.ClientID%>').val() },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: GetWallPassed,
                failure: LoadValuesFailed
            });
        }

        function GetWallPassed(result) {
            var resultObject = result;
            $('#notice_board').append(resultObject[0].Item_Name);
            $('#<%=hndEaliestDate.ClientID%>').val(resultObject[1].Item_Name);
            $('#<%=hndSlab.ClientID%>').val(resultObject[2].Item_Name);
            var galls = resultObject[3].Item_Name.split('_');
            for (var i = 0; i < galls.length; i++) {
                if (galls[i].trim() != '') {
                    $('.gal_' + galls[i].trim()).swipebox();
                }
            }
            WallCall = false;
        }

        function CheckNoticeType() {
            var post_type = $('#<%=cboNoticeType.ClientID%>').val();
            $('#<%=txtURL.ClientID%>').val('');
            $('#<%=txtTitle.ClientID%>').val('');
            $('#<%=txtNote.ClientID%>').val('');
            $('#divDesc1').attr('class', 'col-sm-12');
            $('#divDesc3').html('');
            $('#<%=hndThumbPath.ClientID%>').val('');
            $('#divDesc2').addClass('hide');
            $('#divLoadingSub').addClass('hide');

            if (post_type == 4) {
                $('#divURL').removeClass('hide');
            }
            else {
                $('#divURL').addClass('hide');
            }
            if (post_type == 2 || post_type == 3) {
                if (post_type == 2) {
                    $('#divUploadImages').removeClass('hide');
                    $('#<%=cmdPostUpload1.ClientID%>').removeClass('hide');
                    $('#divUploadOther').addClass('hide');
                    $('#<%=cmdPostUpload2.ClientID%>').addClass('hide');
                    $('#<%=cmdPost.ClientID%>').addClass('hide');
                }
                if (post_type == 3) {
                    $('#divUploadOther').removeClass('hide');
                    $('#<%=cmdPostUpload2.ClientID%>').removeClass('hide');
                    $('#divUploadImages').addClass('hide');
                    $('#<%=cmdPostUpload1.ClientID%>').addClass('hide');
                    $('#<%=cmdPost.ClientID%>').addClass('hide');
                }
            }
            else {
                $('#divUploadImages').addClass('hide');
                $('#divUploadOther').addClass('hide');
                $('#<%=cmdPostUpload1.ClientID%>').addClass('hide');
                $('#<%=cmdPostUpload2.ClientID%>').addClass('hide');
                $('#<%=cmdPost.ClientID%>').removeClass('hide');
            }
        }

        function NewNotice(post_type) {
            $('#<%=cboNoticeType.ClientID%>').val(post_type);
            $find("mpuNoticePost").show();
            CheckNoticeType();
            return false;
        }

        function ArrangeDropZone() {
            $("#dropzonejs1").dropzone({
                autoProcessQueue: false,
                paramName: "file",
                addRemoveLinks: true,
                maxFilesize: 50,
                parallelUploads: 3,
                maxFiles: 100,
                dictResponseError: 'Server not Configured',
                url: "fileupload_nb.aspx?",
                thumbnailWidth: 138,
                thumbnailHeight: 120,
                acceptedFiles: ".jpeg,.jpg,.png,.gif",
                previewTemplate: '<div class="dz-preview dz-file-preview"><div class="dz-details"><div class="dz-filename"><span data-dz-name></span></div><div class="dz-size">File size: <span data-dz-size></span></div><div class="dz-thumbnail-wrapper"><div class="dz-thumbnail"><img data-dz-thumbnail><span class="dz-nopreview">No preview</span><div class="dz-success-mark"><i class="fa fa-check-circle-o"></i></div><div class="dz-error-mark"><i class="fa fa-times-circle-o"></i></div><div class="dz-error-message"><span data-dz-errormessage></span></div></div></div></div><div class="progress progress-striped active"><div class="progress-bar progress-bar-success" data-dz-uploadprogress></div></div></div>',

                init: function () {
                    var myDropzone = this;
                    var uploadCount = 0;
                    var fileCount = 0;

                    $('#<%=cmdPostUpload1.ClientID%>').on('click', function (e) {
                        if (ValidateNotice('<%=txtTitle.ClientID%>')) {
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
                        }
                        else {
                            return false;
                        }
                    });

                    this.on('sending', function (file, xhr, formData) {
                        formData.append('tot', fileCount);
                        formData.append('vst', '<%=ViewState.GetHashCode()%>');
                    });

                    $('#<%=cboNoticeType.ClientID%>').on('change', function (e) {
                        myDropzone.removeAllFiles();
                    });
                    $('.newItem').on('click', function (e) {
                        myDropzone.removeAllFiles();
                    });
                    this.on("success", function (files, response) {
                        uploadCount++;
                        if (uploadCount == fileCount) {
                            var responseText = response.slice(0, response.indexOf("^"));
                            $('#<%=hndUploadResponse.ClientID%>').val(responseText);
                            $("#<%=cmdPost.ClientID%>").trigger("click");
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

            $("#dropzonejs2").dropzone({
                autoProcessQueue: false,
                paramName: "file",
                addRemoveLinks: true,
                maxFilesize: 50,
                parallelUploads: 3,
                maxFiles: 100,
                dictResponseError: 'Server not Configured',
                url: "fileupload_nb.aspx?",
                thumbnailWidth: 138,
                thumbnailHeight: 120,
                previewTemplate: '<div class="dz-preview dz-file-preview"><div class="dz-details"><div class="dz-filename"><span data-dz-name></span></div><div class="dz-size">File size: <span data-dz-size></span></div><div class="progress progress-striped active"><div class="progress-bar progress-bar-success" data-dz-uploadprogress></div></div></div>',

                init: function () {
                    var myDropzone2 = this;
                    var uploadCount2 = 0;
                    var fileCount2 = 0;

                    $('#<%=cmdPostUpload2.ClientID%>').on('click', function (e) {
                        if (ValidateNotice('<%=txtTitle.ClientID%>')) {
                            if (myDropzone2.files.length > 0) {
                                $('.dz-remove').addClass('hide');
                                uploadCount2 = 0;
                                fileCount2 = myDropzone2.files.length;
                                e.preventDefault();
                                e.stopPropagation();
                                myDropzone2.processQueue();
                            }
                            else {
                                ShowError('No Files to Upload.');
                                return false;
                            }
                        }
                        else {
                            return false;
                        }
                    });

                    this.on('sending', function (file, xhr, formData) {
                        formData.append('tot', fileCount2);
                        formData.append('vst', '<%=ViewState.GetHashCode()%>');
                    });

                    $('#<%=cboNoticeType.ClientID%>').on('change', function (e) {
                        myDropzone2.removeAllFiles();
                    });
                    $('.newItem').on('click', function (e) {
                        myDropzone2.removeAllFiles();
                    });
                    this.on('sending', function (file, xhr, formData) {
                        formData.append('ses', '23');
                    });
                    this.on("success", function (files, response) {
                        uploadCount2++;
                        if (uploadCount2 == fileCount2) {
                            var responseText = response.slice(0, response.indexOf("^"));
                            $('#<%=hndUploadResponse.ClientID%>').val(responseText);
                            $("#<%=cmdPost.ClientID%>").trigger("click");
                        }
                        else if (uploadCount2 < fileCount2) {
                            if (uploadCount2 % 3 == 0) {
                                myDropzone2.processQueue();
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

        function fetchData() {
            $('#divLoadingSub').removeClass('hide');
            $.ajax({
                type: "GET",
                url: "api/tasks/GetPageDetails",
                data: { Page_URL: $('#<%=txtURL.ClientID%>').val() },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: GetPageDetailsPassed,
                failure: LoadValuesFailed
            });
            return false;
        }

        function GetPageDetailsPassed(result) {
            var resultObject = result;
            if (resultObject[0].Item_Name.trim() == "None") {
                ShowError('Invalid URL');
                $('#divLoadingSub').addClass('hide');
            }
            if (resultObject[0].Item_Name.trim() != "") {
                $('#<%=txtTitle.ClientID%>').val(resultObject[0].Item_Name);
            }
            if (resultObject[1].Item_Name.trim() != "") {
                $('#<%=txtNote.ClientID%>').val(resultObject[1].Item_Name);
            }
            if (resultObject[2].Item_Name.trim() != "") {
                $('#divDesc1').attr('class', 'col-sm-9');
                $('#divDesc3').html(resultObject[2].Item_Name + "<button class='btn btn-sm btn-danger' style='position: absolute; right: 15px; top: 25px' onClick='return removeThumb();'>&times;</button>");
                $('#<%=hndThumbPath.ClientID%>').val(resultObject[3].Item_Name);
                $('#divDesc2').removeClass('hide');
            }
            if (resultObject[4].Item_Name.trim() != "") {
                $('#<%=txtURL.ClientID%>').val(resultObject[4].Item_Name);
            }
            $('#divLoadingSub').addClass('hide');
        }

        function removeThumb() {
            $('#divDesc1').attr('class', 'col-sm-12');
            $('#divDesc3').html('');
            $('#<%=hndThumbPath.ClientID%>').val('');
            $('#divDesc2').addClass('hide');
            return false;
        }

        function NewLike(notice_id) {
            if ($('#myLike_' + notice_id).hasClass("fa-thumbs-up")) {
                $('#myLike_' + notice_id).attr("class", "fa fa-thumbs-o-up wall_icon");
            }
            else {
                $('#myLike_' + notice_id).attr("class", "fa fa-thumbs-up wall_icon");
            }
            $.ajax({
                type: "GET",
                url: "api/tasks/AddNoticeLike",
                data: { Notice_ID: notice_id },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: NewLikePassed,
                failure: LoadValuesFailed
            });
            return false;
        }

        function NewLikePassed(result) {
            var ResText = result.split("_");
            if (ResText[0].trim() == "Added") {
                $('#myLike_' + ResText[1]).attr("class", "fa fa-thumbs-up wall_icon")
            }
            else {
                $('#myLike_' + ResText[1]).attr("class", "fa fa-thumbs-o-up wall_icon")
            }
            $('#likeNumber_' + ResText[1]).html(ResText[2]);
        }

        function ReadLikes(notice_id) {
            $('#spanLikeCount').html('0');
            $('#divLikesList').html('');
            $find("mpuReadLikes").show();
            $.ajax({
                type: "GET",
                url: "api/tasks/GetLikesList",
                data: { Notice_ID: notice_id },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: ReadLikesPassed,
                failure: LoadValuesFailed
            });
            return false;
        }

        function ReadLikesPassed(result) {
            var resultObject = result;
            $('#spanLikeCount').html(resultObject[1].Item_Name);
            $('#divLikesList').html(resultObject[0].Item_Name);
        }

        function NewComment(notice_id) {
            $('#<%=hndComments.ClientID%>').val(notice_id);
            $('#spanCommentsCount').html('0');
            $('#divCommentsList').html('');
            $('#<%=txtComment.ClientID%>').val('');
            $find("mpuComments").show();
            $.ajax({
                type: "GET",
                url: "api/tasks/GetCommentsList",
                data: { Notice_ID: notice_id },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: NewCommentPassed,
                failure: LoadValuesFailed
            });
            return false;
        }

        function ReadComments(notice_id) {
            $('#<%=hndComments.ClientID%>').val(notice_id);
            $('#spanCommentsCount').html('0');
            $('#divCommentsList').html('');
            $('#<%=txtComment.ClientID%>').val('');
            $find("mpuComments").show();
            $.ajax({
                type: "GET",
                url: "api/tasks/GetCommentsList",
                data: { Notice_ID: notice_id },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: NewCommentPassed,
                failure: LoadValuesFailed
            });
            return false;
        }

        function NewCommentPassed(result) {
            var resultObject = result;
            $('#spanCommentsCount').html(resultObject[1].Item_Name);
            $('#divCommentsList').html(resultObject[0].Item_Name);
        }

        function ArrangeGalleries() {
            <%=Galleries_Script%>
        }

        function delPost(notice_id) {
            $('#<%=hndDelPost.ClientID%>').val(notice_id);
            $find("mpuDelPost").show();
            return false;
        }

        function delComment(comment_id) {
            $('#<%=hndDelCom.ClientID%>').val(comment_id);
            $find("mpuDelCom").show();
            return false;
        }
    </script>
</asp:Content>