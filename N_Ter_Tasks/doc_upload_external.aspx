<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_guest.master" AutoEventWireup="true" CodeBehind="doc_upload_external.aspx.cs" Inherits="N_Ter_Tasks.doc_upload_external" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter > Exteral Document Upload
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contBody" runat="Server">
    <div class="row no-margin-hr">
        <div class="col-lg-12">
            <div id="altHelp" runat="server" class="alert alert-success">
                <asp:Literal ID="ltrGuestHelp" runat="server"></asp:Literal>
            </div>
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="panel-title-nter">File Upload</div>
                    <div id="divUpload" runat="server" class="form-group">
                        <label>Document</label>
                        <asp:FileUpload ID="fulDocument" runat="server" CssClass="form-control st_file_upload" />
                    </div>
                    <div id="divDocTags" runat="server">
                    </div>
                    <div id="divError" runat="server" class="alert alert-danger alert-dark">
                        <strong>Oops!</strong> Something really went wrong. Please try again later.
                    </div>
                </div>
                <div id="divSubmit" runat="server" class="modal-footer">
                    <asp:Button ID="cmdSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="cmdSubmit_Click" />
                </div>
            </div>
        </div>
    </div>
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contScripts" runat="Server">
    <script type="text/javascript">
        $(function () {
            $(window).resize(function () {
                ArrangePopups();
            });
        });

        init.push(function () {
            ArrangePopups();
        });

        function ArrangePopups() {
            <%=DocHelpPanelResizeScript%>
        }

        function ValidateForm() {
            var validated = true;
            var FileUploadPath = $("#<%=fulDocument.ClientID%>").val();

            if (FileUploadPath.trim() == '') {
                ShowError('Please select a file to upload');
                validated = false;
            }

            validated = ValidatRequiredFields();
            return validated;
        }

        <%=Required_Fields%>

        <%=DocHelpScript%>
    </script>
</asp:Content>