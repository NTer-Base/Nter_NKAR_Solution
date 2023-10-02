<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid_wf.master" AutoEventWireup="true" CodeBehind="wf_doc_templates.aspx.cs" Inherits="N_Ter_Tasks.wf_doc_templates" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Workflow Templates
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Workflow Templates -
    <asp:Label ID="lblWorkflowName" runat="server" Text="Label"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="row" style="margin-top: -18px">
        <div class="col-lg-12">
            <div class="tab-base">
                <ul class="nav nav-tabs nav-tabs-simple">
                    <asp:Literal ID="ltrTabs" runat="server"></asp:Literal>
                </ul>
                <div class="panel panel-info">
                    <div class="panel-body no-padding">
                        <div class="tab-content grid-with-paging">
                            <asp:Panel ID="lft_tab_1" ClientIDMode="Static" runat="server" CssClass="">
                                <asp:HiddenField ID="hndDocumentID" runat="server" />
                                <asp:HiddenField ID="hndWF_ID" runat="server" />
                                <div class="padding-xs-vr">
                                    <button id="cmdNew" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Document Template</button>
                                    <asp:ModalPopupExtender ID="cmdNew_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlData" TargetControlID="cmdNew" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                                    </asp:ModalPopupExtender>
                                </div>
                                <div class="table-responsive table-primary no-margin-b">
                                    <asp:GridView ID="gvMain" runat="server" CssClass="table table-striped table-hover grid_table grid_wf_categories" AutoGenerateColumns="False" OnRowDataBound="gvMain_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Document_ID" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <button id='cmdEdit' type='submit' runat="server" class="btn btn-primary btn-xs" title="Edit"><i class="fa fa-edit button_icon"></i></button>
                                                    <asp:ModalPopupExtender ID="cmdEdit_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlData" TargetControlID="cmdEdit" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                                                    </asp:ModalPopupExtender>
                                                </ItemTemplate>
                                                <ItemStyle Width="22px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <button id='cmdDeleteDoc' type='submit' runat="server" class="btn btn-danger btn-xs" title="Delete"><i class="fa fa-trash-o button_icon"></i></button>
                                                    <asp:ModalPopupExtender ID="cmdDeleteDoc_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDelete" TargetControlID="cmdDeleteDoc" CancelControlID="cmdCancel" BackgroundCssClass="at_modelpopup_background">
                                                    </asp:ModalPopupExtender>
                                                </ItemTemplate>
                                                <ItemStyle Width="22px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Document_Name" HeaderText="Document Name" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="lft_tab_3" ClientIDMode="Static" runat="server" CssClass="">
                                <div class="padding-xs-vr">
                                    <button id="cmdNewFile" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Flat File Template</button>
                                    <asp:ModalPopupExtender ID="cmdNewFile_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDataFile" TargetControlID="cmdNewFile" CancelControlID="cmdCloseFile" BackgroundCssClass="at_modelpopup_background">
                                    </asp:ModalPopupExtender>
                                </div>
                                <div class="table-responsive table-primary no-margin-b">
                                    <asp:GridView ID="gvFlatFiles" runat="server" CssClass="table table-striped table-hover grid_table grid_wf_categories" AutoGenerateColumns="False" OnRowDataBound="gvFlatFiles_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Document_ID" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <button id='cmdEditFile' type='submit' runat="server" class="btn btn-primary btn-xs" title="Edit"><i class="fa fa-edit button_icon"></i></button>
                                                    <asp:ModalPopupExtender ID="cmdEditFile_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDataFile" TargetControlID="cmdEditFile" CancelControlID="cmdCloseFile" BackgroundCssClass="at_modelpopup_background">
                                                    </asp:ModalPopupExtender>
                                                </ItemTemplate>
                                                <ItemStyle Width="22px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <button id='cmdDeleteFile' type='submit' runat="server" class="btn btn-danger btn-xs" title="Delete"><i class="fa fa-trash-o button_icon"></i></button>
                                                    <asp:ModalPopupExtender ID="cmdDeleteFile_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDelete" TargetControlID="cmdDeleteFile" CancelControlID="cmdCancel" BackgroundCssClass="at_modelpopup_background">
                                                    </asp:ModalPopupExtender>
                                                </ItemTemplate>
                                                <ItemStyle Width="22px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Document_Name" HeaderText="Document Name" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="lft_tab_2" ClientIDMode="Static" runat="server" CssClass="">
                                <asp:HiddenField ID="hndEmail_ID" runat="server" />
                                <div class="padding-xs-vr">
                                    <button id="cmdNewEmail" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Email Template</button>
                                    <asp:ModalPopupExtender ID="cmdNewEMail_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDataEmail" TargetControlID="cmdNewEmail" CancelControlID="cmdNoEmail" BackgroundCssClass="at_modelpopup_background">
                                    </asp:ModalPopupExtender>
                                </div>
                                <div class="table-responsive table-primary no-margin-b">
                                    <asp:GridView ID="gvEmail" runat="server" CssClass="table table-striped table-hover grid_table grid_wf_categories" AutoGenerateColumns="False" OnRowDataBound="gvEmail_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Email_ID" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <button id='cmdEditEmail' type='submit' runat="server" class="btn btn-primary btn-xs" title="Edit"><i class="fa fa-edit button_icon"></i></button>
                                                    <asp:ModalPopupExtender ID="cmdEditEmail_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDataEmail" TargetControlID="cmdEditEmail" CancelControlID="cmdNoEmail" BackgroundCssClass="at_modelpopup_background">
                                                    </asp:ModalPopupExtender>
                                                </ItemTemplate>
                                                <ItemStyle Width="22px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <button id='cmdDeleteEmail' type='submit' runat="server" class="btn btn-danger btn-xs" title="Delete"><i class="fa fa-trash-o button_icon"></i></button>
                                                    <asp:ModalPopupExtender ID="cmdDeleteEmail_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDeleteEmail" TargetControlID="cmdDeleteEmail" CancelControlID="cmdCancelEmail" BackgroundCssClass="at_modelpopup_background">
                                                    </asp:ModalPopupExtender>
                                                </ItemTemplate>
                                                <ItemStyle Width="22px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Email_Name" HeaderText="Email Name" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlData" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_indent" class="at_modelpopup_indent">
            <div id="at_model_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <button id='cmdHelp1' type='submit' runat="server" class="btn btn-success" title="Help"><i class="fa fa-question"></i></button>
                        <asp:ModalPopupExtender ID="cmdHelp1_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlHelp" TargetControlID="cmdHelp1" CancelControlID="cmdCloseHelp" BackgroundCssClass="at_modelpopup_background_2">
                        </asp:ModalPopupExtender>
                        <asp:Button ID="cmdNo" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add/Edit Document Template</h4>
                </div>
                <div id="at_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Document Name</label>
                            <asp:TextBox ID="txtDocumentName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox runat="server" ID="chkUseDefault" CssClass="checkboxlist" /><span style="padding-left: 10px">Use Default Letterhead</span>
                        </div>
                        <div id="divTemplate" class="form-group">
                            <label>Document Template</label>
                            <asp:DropDownList ID="cboTemplates" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div id="divBody" class="form-group">
                            <label>Document Body</label>
                            <asp:TextBox ID="txtDocumentBody" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="10"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Document Type</label>
                            <asp:DropDownList ID="cboDocType" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Access Lecel</label>
                            <asp:DropDownList ID="cboAccessLevel" runat="server" CssClass="form-control">
                                <asp:ListItem Value="3">Level 3</asp:ListItem>
                                <asp:ListItem Value="2">Level 2</asp:ListItem>
                                <asp:ListItem Value="1">Level 1</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox runat="server" ID="chkSingleDoc" CssClass="checkboxlist" /><span style="padding-left: 10px">Keep a Single Document per Task</span>
                        </div>
                        <div class="form-group">
                            <label>Create the Documennt</label>
                            <asp:DropDownList ID="cboCreationTime" runat="server" CssClass="form-control">
                                <asp:ListItem Value="1">When Task Enters the Step</asp:ListItem>
                                <asp:ListItem Value="0">When Task Leaves the Step</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Include in - Workflow Steps</label>
                            <asp:CheckBoxList ID="chkAllowedWorkflowStep" runat="server" CssClass="checkboxlist"></asp:CheckBoxList>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox runat="server" ID="chkCondition" CssClass="checkboxlist" /><span style="padding-left: 10px">Enable Conditional Execution</span>
                        </div>
                        <div id="divCondition" class="alert alert-info mb">
                            <h4>Apply Only if</h4>
                            <div class="row">
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>Field</label>
                                        <asp:DropDownList ID="cboStepField" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Operator</label>
                                        <asp:DropDownList ID="cboOperator" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="1">&lt;</asp:ListItem>
                                            <asp:ListItem Value="2">&gt;</asp:ListItem>
                                            <asp:ListItem Value="3">=</asp:ListItem>
                                            <asp:ListItem Value="6">&#33;=</asp:ListItem>
                                            <asp:ListItem Value="4">&lt;=</asp:ListItem>
                                            <asp:ListItem Value="5">&gt;=</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>Criteria</label>
                                        <asp:TextBox ID="txtCriteria" runat="server" CssClass="form-control"></asp:TextBox>
                                        <select id="cboCondtTemp" class="form-control"></select>
                                        <input type="text" id="txtCondtDateTemp" class="form-control dtapi"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSave_Click" />
                    <asp:Button ID="cmdReset" runat="server" Text="Reset" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDataFile" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_file_indent" class="at_modelpopup_indent">
            <div id="at_model_file_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <button id='cmdHelp3' type='submit' runat="server" class="btn btn-success" title="Help"><i class="fa fa-question"></i></button>
                        <asp:ModalPopupExtender ID="cmdHelp3_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlHelp3" TargetControlID="cmdHelp3" CancelControlID="cmdCloseHelp3" BackgroundCssClass="at_modelpopup_background_2">
                        </asp:ModalPopupExtender>
                        <asp:Button ID="cmdCloseFile" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add/Edit Document Template</h4>
                </div>
                <div id="at_model_file_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>File Name</label>
                            <asp:TextBox ID="txtFileName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Extension</label>
                            <asp:TextBox ID="txtExtension" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>File Content</label>
                            <asp:TextBox ID="txtFileContent" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="10"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Document Type</label>
                            <asp:DropDownList ID="cboDocType_File" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Access Lecel</label>
                            <asp:DropDownList ID="cboAccessLevel_File" runat="server" CssClass="form-control">
                                <asp:ListItem Value="3">Level 3</asp:ListItem>
                                <asp:ListItem Value="2">Level 2</asp:ListItem>
                                <asp:ListItem Value="1">Level 1</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox runat="server" ID="chkOneFile_File" CssClass="checkboxlist" /><span style="padding-left: 10px">Keep a Single Document per Task</span>
                        </div>
                        <div class="form-group">
                            <label>Create the Documennt</label>
                            <asp:DropDownList ID="cboCreationTime_File" runat="server" CssClass="form-control">
                                <asp:ListItem Value="1">When Task Enters the Step</asp:ListItem>
                                <asp:ListItem Value="0">When Task Leaves the Step</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Include in - Workflow Steps</label>
                            <asp:CheckBoxList ID="chkAllowedWorkflowStep_File" runat="server" CssClass="checkboxlist"></asp:CheckBoxList>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox runat="server" ID="chkCondition_File" CssClass="checkboxlist" /><span style="padding-left: 10px">Enable Conditional Execution</span>
                        </div>
                        <div id="divCondition_File" class="alert alert-info mb">
                            <h4>Apply Only if</h4>
                            <div class="row">
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>Field</label>
                                        <asp:DropDownList ID="cboStepField_File" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Operator</label>
                                        <asp:DropDownList ID="cboOperator_File" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="1">&lt;</asp:ListItem>
                                            <asp:ListItem Value="2">&gt;</asp:ListItem>
                                            <asp:ListItem Value="3">=</asp:ListItem>
                                            <asp:ListItem Value="6">&#33;=</asp:ListItem>
                                            <asp:ListItem Value="4">&lt;=</asp:ListItem>
                                            <asp:ListItem Value="5">&gt;=</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>Criteria</label>
                                        <asp:TextBox ID="txtCriteria_File" runat="server" CssClass="form-control"></asp:TextBox>
                                        <select id="cboCondtTemp_File" class="form-control"></select>
                                        <input type="text" id="txtCondtDateTemp_File" class="form-control dtapi_File"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSaveFile" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSaveFile_Click" />
                    <asp:Button ID="cmdResetFile" runat="server" Text="Reset" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDataEmail" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_email_indent" class="at_modelpopup_indent">
            <div id="at_model_email_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <button id='cmdHelp2' type='submit' runat="server" class="btn btn-success" title="Help"><i class="fa fa-question"></i></button>
                        <asp:ModalPopupExtender ID="cmdHelp2_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlHelp" TargetControlID="cmdHelp2" CancelControlID="cmdCloseHelp" BackgroundCssClass="at_modelpopup_background_2">
                        </asp:ModalPopupExtender>
                        <asp:Button ID="cmdNoEmail" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add/Edit E-Mail Template</h4>
                </div>
                <div id="at_model_email_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>E-Mail Template Name</label>
                            <asp:TextBox ID="txtEmailName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>From Name (Leave blank to use System Defaults)</label>
                            <asp:TextBox ID="txtFromName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>From E-Mail Address (Leave blank to use System Defaults)</label>
                            <asp:TextBox ID="txtFromAddress" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>To E-Mail Address</label>
                            <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>CC Addresses</label>
                            <asp:TextBox ID="txtCCAddresses" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Attachment - Document Templates</label>
                            <asp:DropDownList ID="cboDocumentToAttach" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Attachment - Data Extracts</label>
                            <asp:DropDownList ID="cboExtractToAttach" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Attachment - Task Documents</label>
                            <asp:DropDownList ID="cboTaskDocsToAttach" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>E-Mail Title</label>
                            <asp:TextBox ID="txtEmailTitle" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>E-Mail Body</label>
                            <asp:TextBox ID="txtEmailBody" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="10"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Send the E-mail</label>
                            <asp:DropDownList ID="cboSentTime" runat="server" CssClass="form-control">
                                <asp:ListItem Value="1">When Task Enters the Step</asp:ListItem>
                                <asp:ListItem Value="0">When Task Leaves the Step</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox ID="chkSendMeetingRequest" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Send Meeting Request</span>
                        </div>
                        <div id="divMeetingRequest" class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Meeting Date/Time (Template)</label>
                                    <asp:TextBox ID="txtMeetingDate" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Meeting Duration (Template)</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtMeetingDuration" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-addon">Minutes</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>Include in - Workflow Steps</label>
                            <asp:CheckBoxList ID="chkAllowedWorkflowStepEmail" runat="server" CssClass="checkboxlist"></asp:CheckBoxList>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox runat="server" ID="chkCondition_Email" CssClass="checkboxlist" /><span style="padding-left: 10px">Enable Conditional Execution</span>
                        </div>
                        <div id="divCondition_Email" class="alert alert-info mb">
                            <h4>Apply Only if</h4>
                            <div class="row">
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>Field</label>
                                        <asp:DropDownList ID="cboStepField_Email" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Operator</label>
                                        <asp:DropDownList ID="cboOperator_Email" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="1">&lt;</asp:ListItem>
                                            <asp:ListItem Value="2">&gt;</asp:ListItem>
                                            <asp:ListItem Value="3">=</asp:ListItem>
                                            <asp:ListItem Value="6">&#33;=</asp:ListItem>
                                            <asp:ListItem Value="4">&lt;=</asp:ListItem>
                                            <asp:ListItem Value="5">&gt;=</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>Criteria</label>
                                        <asp:TextBox ID="txtCriteria_Email" runat="server" CssClass="form-control"></asp:TextBox>
                                        <select id="cboCondtTemp_Email" class="form-control"></select>
                                        <input type="text" id="txtCondtDateTemp_Email" class="form-control dtapi_Email"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSaveEmail" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSaveEmail_Click" />
                    <asp:Button ID="cmdResetEmail" runat="server" Text="Reset" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlHelp" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_help_indent" class="at_modelpopup_indent">
            <div id="at_model_help_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Template Help</h4>
                </div>
                <div id="at_model_help_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <b>Loggedn in User Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_code] : </span>User Code
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_name] : </span>Full Name
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_designation] : </span>Designation
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_phone] : </span>Phone
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_email] : </span>Email
                            </div>
                            <div class="col-md-12">
                                <span class="text-danger">[s_signature] : </span>Singature Image
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Task Owner Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[o_code] : </span>User Code
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[o_name] : </span>Full Name
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[o_designation] : </span>Designation
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[o_phone] : </span>Phone
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[o_email] : </span>Email
                            </div>
                            <div class="col-md-12">
                                <span class="text-danger">[o_signature] : </span>Singature Image
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Other User Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_name:Group Name] : </span>Full Name of the Last Accessed user in the User Group
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_name:Step Number] : </span>Full Name of the Last Accessed user of the Workflow Step
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_designation:Group Name] : </span>Designation of the Last Accessed user in the User Group
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_designation:Step Number] : </span>Designation of the Last Accessed user of the Workflow Step
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_email:Group Name] : </span>Email of the Last Accessed user in the User Group
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_email:Step Number] : </span>Email of the Last Accessed user of the Workflow Step
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_signature:Group Name] : </span>Signature of the Last Accessed user in the User Group
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_signature:Step Number] : </span>Signature of the Last Accessed user of the Workflow Step
                            </div>
                            <div class="col-md-12 mt10">
                                <b>
                                    <asp:Literal ID="ltrEL2" runat="server"></asp:Literal>
                                    Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[c_code] : </span>
                                <asp:Literal ID="ltrEL2_1" runat="server"></asp:Literal>
                                Code
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[c_name] : </span>Diaplay Name
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[c_address] : </span>Address
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[c_phone] : </span>Phone
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[c_email] : </span>Email
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[c_main_contact] : </span>Main Contact Name
                            </div>
                            <div class="col-md-12 mt10">
                                <b>
                                    <asp:Literal ID="ltrEL1" runat="server"></asp:Literal>
                                    Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[cc_code] : </span>
                                <asp:Literal ID="ltrEL1_1" runat="server"></asp:Literal>
                                Code
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[cc_name] : </span>Diaplay Name
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[cc_phone] : </span>Phone
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[cc_email] : </span>Email
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[cc_main_contact] : </span>Main Contact Name
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Other</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[scd_name] : </span>Schedule Description 1 (if available)
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[scd_name_2] : </span>Schedule Description 2 (if available)
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[today] : </span>Today's Date
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[time] : </span>Now Time (HH:MM)
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[last_day] : </span>Task Deadline
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[app_link] : </span>N-Ter Application Link
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[step_owners_emails] : </span>All E-mail Addresses of the users in the Step User Group (Only for E-mail Templates)
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[step_owners_names] : </span>First Name of the users in the Step User Group (Only for E-mail Templates)
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Task Related Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[task_no] : </span>Task Number
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[task_date] : </span>Task Created Date
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[ef1] : </span>Special Field 1 Data
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[ef2] : </span>Special Field 2 Data
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[tp_ext_link] : </span>Guest Task Posting Link
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[task_link] : </span>Task Link
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[tf:Field Name] : </span>Task Step Field
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[img:Field Name|Height|Width] : </span>Task Step Image
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[api:API Name] : </span>API Call Result
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[af:Field Name] : </span>Task Step Addon Field
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[date:Step Number] : </span>Last Posted Date of the Workflow Step
                            </div>
                            <div class="col-md-12">
                                <span class="text-danger">⟦afr:Addon Name:Section to Repeat⟧ : </span>Task Step Addon - Repeat Section
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Task Fields/Addon Fields Bracket Hierarchy</b>
                            </div>
                            <div class="col-md-12">
                                <span class="text-danger">〈 〉  ⟪ ⟫  [ ]</span>
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Sample Condition</b>
                            </div>
                            <div class="col-md-12">
                                <span class="text-danger">[tf:Field Name{1|First Value|{2|Second Value|{3|Third Value|{4|Fourth Value|Fifth Value}}}}]</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCloseHelp" runat="server" Text="Close" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlHelp3" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_help3_indent" class="at_modelpopup_indent">
            <div id="at_model_help3_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Template Help</h4>
                </div>
                <div id="at_model_help3_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <b>Loggedn in User Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_code] : </span>User Code
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_name] : </span>Full Name
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_designation] : </span>Designation
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_phone] : </span>Phone
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_email] : </span>Email
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Task Owner Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[o_code] : </span>User Code
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[o_name] : </span>Full Name
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[o_designation] : </span>Designation
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[o_phone] : </span>Phone
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[o_email] : </span>Email
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Other User Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_name:Group Name] : </span>Full Name of the Last Accessed user in the User Group
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_name:Step Number] : </span>Full Name of the Last Accessed user of the Workflow Step
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_designation:Group Name] : </span>Designation of the Last Accessed user in the User Group
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_designation:Step Number] : </span>Designation of the Last Accessed user of the Workflow Step
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_email:Group Name] : </span>Email of the Last Accessed user in the User Group
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[s_email:Step Number] : </span>Email of the Last Accessed user of the Workflow Step
                            </div>
                            <div class="col-md-12 mt10">
                                <b>
                                    <asp:Literal ID="ltrEL2_2" runat="server"></asp:Literal>
                                    Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[c_code] : </span>
                                <asp:Literal ID="ltrEL2_3" runat="server"></asp:Literal>
                                Code
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[c_name] : </span>Diaplay Name
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[c_address] : </span>Address
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[c_phone] : </span>Phone
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[c_email] : </span>Email
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[c_main_contact] : </span>Main Contact Name
                            </div>
                            <div class="col-md-12 mt10">
                                <b>
                                    <asp:Literal ID="ltrEL1_2" runat="server"></asp:Literal>
                                    Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[cc_code] : </span>
                                <asp:Literal ID="ltrEL1_3" runat="server"></asp:Literal>
                                Code
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[cc_name] : </span>Diaplay Name
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[cc_phone] : </span>Phone
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[cc_email] : </span>Email
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[cc_main_contact] : </span>Main Contact Name
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Other</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[scd_name] : </span>Schedule Description 1 (if available)
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[scd_name_2] : </span>Schedule Description 2 (if available)
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[today] : </span>Today's Date
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[time] : </span>Now Time (HH:MM)
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[last_day] : </span>Task Deadline
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[app_link] : </span>N-Ter Application Link
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Task Related Details</b>
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[task_no] : </span>Task Number
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[task_date] : </span>Task Created Date
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[ef1] : </span>Special Field 1 Data
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[ef2] : </span>Special Field 2 Data
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[tp_ext_link] : </span>Guest Task Posting Link
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[task_link] : </span>Task Link
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[tf:Field Name] : </span>Task Step Field
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[api:API Name] : </span>API Call Result
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[af:Field Name] : </span>Task Step Addon Field
                            </div>
                            <div class="col-md-6">
                                <span class="text-danger">[date:Step Number] : </span>Last Posted Date of the Workflow Step
                            </div>
                            <div class="col-md-12">
                                <span class="text-danger">⟦afr:Addon Name:Section to Repeat⟧ : </span>Task Step Addon - Repeat Section
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Task Fields/Addon Fields Bracket Hierarchy</b>
                            </div>
                            <div class="col-md-12">
                                <span class="text-danger">〈 〉  ⟪ ⟫  [ ]</span>
                            </div>
                            <div class="col-md-12 mt10">
                                <b>Sample Condition</b>
                            </div>
                            <div class="col-md-12">
                                <span class="text-danger">[tf:Field Name{1|First Value|{2|Second Value|{3|Third Value|{4|Fourth Value|Fifth Value}}}}]</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCloseHelp3" runat="server" Text="Close" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDelete" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_model_indent" class="at_modelpopup_indent">
            <div id="at_del_model_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete</h4>
                </div>
                <div id="at_del_model_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDelete" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDelete_Click" />
                    <asp:Button ID="cmdCancel" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDeleteEmail" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_model_email_indent" class="at_modelpopup_indent">
            <div id="at_del_model_email_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete</h4>
                </div>
                <div id="at_del_model_email_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDeleteEmail" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDeleteEmail_Click" />
                    <asp:Button ID="cmdCancelEmail" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script src="assets/javascripts/app_scripts/operator_pages.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $(window).resize(function () {
                ArrangePopups();
            });
        });

        init.push(function () {
            ArrangePopups();
            ArrangeGrids();
            InitialteCondControls('<%=txtCriteria.ClientID%>', 'dtapi', 'cboCondtTemp', 'txtCondtDateTemp', '<%=cboStepField.ClientID%>');
            InitialteCondControls('<%=txtCriteria_File.ClientID%>', 'dtapi_File', 'cboCondtTemp_File', 'txtCondtDateTemp_File', '<%=cboStepField_File.ClientID%>');
            InitialteCondControls('<%=txtCriteria_Email.ClientID%>', 'dtapi_Email', 'cboCondtTemp_Email', 'txtCondtDateTemp_Email', '<%=cboStepField_Email.ClientID%>');
        });

        function ArrangePopups() {
            AdjustPopupSize(167, 800, 'at_model');
            AdjustPopupSize(167, 800, 'at_model_file');
            AdjustPopupSize(80, 400, 'at_del_model');
            AdjustPopupSize(167, 800, 'at_model_email');
            AdjustPopupSize(80, 400, 'at_del_model_email');
            AdjustPopupSize(80, 700, 'at_model_help');
            AdjustPopupSize(80, 700, 'at_model_help3');
        }

        function ArrangeGrids() {
            $('.grid_wf_categories').dataTable({
                "pageLength": 50,
                "order": [[2, 'asc']],
                "responsive": true,
                "autoWidth": true,
                "columnDefs": [
                    { 'orderable': false, targets: 0 },
                    { 'orderable': false, targets: 1 }
                ],
                "fnDrawCallback": function (oSettings, json) {
                    AdjustGridResp();
                }
            });

            $(".dataTables_filter").children().children('input').attr('placeholder', 'Search...');
        }

        function ClearControls(reload_codt_drps) {
            $('#divCondition').addClass('hide');

            clearTextBox(['<%=txtDocumentName.ClientID%>', '<%=txtDocumentBody.ClientID%>']);
            clearCheckListBox(['<%=chkAllowedWorkflowStep.ClientID%>'])
            clearDropDown(['<%=cboCreationTime.ClientID%>', '<%=cboTemplates.ClientID%>', '<%=cboDocType.ClientID%>', '<%=cboAccessLevel.ClientID%>', '<%=cboStepField.ClientID%>', '<%=cboOperator.ClientID%>']);
            clearCheckBox(['<%=chkUseDefault.ClientID%>', '<%=chkSingleDoc.ClientID%>', '<%=chkCondition.ClientID%>']);
            ShowHideDocSelection(false);

            CheckConditions('<%=chkCondition.ClientID%>', '<%=cboStepField.ClientID%>', '<%=cboOperator.ClientID%>', '<%=txtCriteria.ClientID%>', 'divCondition', 'cboCondtTemp', 'txtCondtDateTemp', '', reload_codt_drps);
            return false;
        }

        function ResetControls() {
            if ($("#<%=hndDocumentID.ClientID%>").val().trim() == '0') {
                ClearControls(true);
            }
            else {
                LoadValues();
            }
            return false;
        }

        function LoadValues() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetWFDocumentTemplate",
                data: { Document_ID: $("#<%=hndDocumentID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassed(result) {
            ClearControls(false);

            var resultObject = result;
            $('#<%=txtDocumentName.ClientID%>').val(resultObject[0].Document_Name);
            $('#<%=cboCreationTime.ClientID%>').val(resultObject[0].At_Begining);
            $('#<%=cboDocType.ClientID%>').val(resultObject[0].Document_Type);
            $('#<%=cboAccessLevel.ClientID%>').val(resultObject[0].Access_Level);
            if (resultObject[0].Use_Default_Letterhead == "1") {
                onCheckBox('#<%=chkUseDefault.ClientID%>');
                ShowHideDocSelection(true);
                $('#<%=txtDocumentBody.ClientID%>').val(resultObject[0].Document_Body);
            }
            else {
                $("#<%=cboTemplates.ClientID%> option").filter(function () {
                    return $(this).text() == resultObject[0].Document_Title;
                }).prop('selected', true);
            }
            if (resultObject[0].Keep_Single_Doc == "1") {
                onCheckBox('#<%=chkSingleDoc.ClientID%>');
            }
            if (resultObject[0].Enable_Conditions == "1") {
                onCheckBox('#<%=chkCondition.ClientID%>');
                $("#<%=cboStepField.ClientID%> > option").each(function () {
                    if ($(this).val().split('_')[0].trim() == resultObject[0].Workflow_Step_Field_ID) {
                        $('#<%=cboStepField.ClientID%>').val($(this).val());
                    }
                });
                CheckConditions('<%=chkCondition.ClientID%>', '<%=cboStepField.ClientID%>', '<%=cboOperator.ClientID%>', '<%=txtCriteria.ClientID%>', 'divCondition', 'cboCondtTemp', 'txtCondtDateTemp', resultObject[0].Condition_Value, true);
                $('#<%=cboOperator.ClientID%>').val(resultObject[0].Operator_ID);
            }

            var resultObjectSub = resultObject[0].WF_Steps_For_Template;
            $('#<%=chkAllowedWorkflowStep.ClientID%> input').each(function () {
                for (var i = 0; i < resultObjectSub.length; i++) {
                    if (resultObjectSub[i].Workflow_Step_ID.trim() == $(this).val().trim()) {
                        onCheckBox(this);
                    }
                }
            });
        }

        $("#<%=chkUseDefault.ClientID%>").change(function () {
            if (this.checked) {
                ShowHideDocSelection(true)
            }
            else {
                ShowHideDocSelection(false);
            }
        });

        function ShowHideDocSelection(isDefault) {
            if (isDefault == true) {
                $('#divTemplate').addClass('hide');
                $('#divBody').removeClass('hide');
            }
            else {
                $('#divTemplate').removeClass('hide');
                $('#divBody').addClass('hide');
            }
        }


        function ClearControlsEmail(reload_codt_drps) {
            $('#divCondition_Email').addClass('hide');

            clearTextBox(['<%=txtEmailName.ClientID%>', '<%=txtEmailBody.ClientID%>', '<%=txtEmailTitle.ClientID%>', '<%=txtEmailAddress.ClientID%>', '<%=txtFromName.ClientID%>', '<%=txtFromAddress.ClientID%>', '<%=txtCCAddresses.ClientID%>', '<%=txtMeetingDate.ClientID%>', '<%=txtMeetingDuration.ClientID%>']);
            clearCheckListBox(['<%=chkAllowedWorkflowStepEmail.ClientID%>']);
            clearCheckBox(['<%=chkSendMeetingRequest.ClientID%>', '<%=chkCondition_Email.ClientID%>']);
            clearDropDown(['<%=cboSentTime.ClientID%>', '<%=cboDocumentToAttach.ClientID%>', '<%=cboExtractToAttach.ClientID%>', '<%=cboTaskDocsToAttach.ClientID%>', '<%=cboStepField_Email.ClientID%>', '<%=cboOperator_Email.ClientID%>']);
            CheckMeetingRequest();

            CheckConditions('<%=chkCondition_Email.ClientID%>', '<%=cboStepField_Email.ClientID%>', '<%=cboOperator_Email.ClientID%>', '<%=txtCriteria_Email.ClientID%>', 'divCondition_Email', 'cboCondtTemp_Email', 'txtCondtDateTemp_Email', '', reload_codt_drps);
            return false;
        }

        function ResetControlsEmail() {
            if ($("#<%=hndEmail_ID.ClientID%>").val().trim() == '0') {
                ClearControlsEmail(true);
            }
            else {
                LoadValuesEmail();
            }
            return false;
        }

        function LoadValuesEmail() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetWFEmailTemplate",
                data: { Email_ID: $("#<%=hndEmail_ID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesEmailPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesEmailPassed(result) {
            ClearControlsEmail(false);

            var resultObject = result;
            $('#<%=txtEmailName.ClientID%>').val(resultObject[0].Email_Name);
            $('#<%=txtFromName.ClientID%>').val(resultObject[0].From_Name);
            $('#<%=txtFromAddress.ClientID%>').val(resultObject[0].From_Address);
            $('#<%=txtEmailAddress.ClientID%>').val(resultObject[0].Email_Address);
            $('#<%=txtCCAddresses.ClientID%>').val(resultObject[0].CC_Addresses);
            $('#<%=txtEmailBody.ClientID%>').val(resultObject[0].Email_Body);
            $('#<%=txtEmailTitle.ClientID%>').val(resultObject[0].Email_Title);
            $('#<%=cboSentTime.ClientID%>').val(resultObject[0].At_Begining);
            $('#<%=cboDocumentToAttach.ClientID%>').val(resultObject[0].Document_ID);
            $('#<%=cboExtractToAttach.ClientID%>').val(resultObject[0].Extract_Template_ID);
            $("#<%=cboTaskDocsToAttach.ClientID%> > option").each(function () {
                if ($(this).text().trim() == resultObject[0].Task_Documents) {
                    $('#<%=cboTaskDocsToAttach.ClientID%>').val($(this).val());
                }
            });

            if (resultObject[0].Send_Meeting_Request == '1') {
                onCheckBox('#<%=chkSendMeetingRequest.ClientID%>');
                $('#<%=txtMeetingDate.ClientID%>').val(resultObject[0].Meeting_Request_Time);
                $('#<%=txtMeetingDuration.ClientID%>').val(resultObject[0].Meeting_Duration);
            }
            if (resultObject[0].Enable_Conditions == "1") {
                onCheckBox('#<%=chkCondition_Email.ClientID%>');
                $("#<%=cboStepField_Email.ClientID%> > option").each(function () {
                    if ($(this).val().split('_')[0].trim() == resultObject[0].Workflow_Step_Field_ID) {
                        $('#<%=cboStepField_Email.ClientID%>').val($(this).val());
                    }
                });
                CheckConditions('<%=chkCondition_Email.ClientID%>', '<%=cboStepField_Email.ClientID%>', '<%=cboOperator_Email.ClientID%>', '<%=txtCriteria_Email.ClientID%>', 'divCondition_Email', 'cboCondtTemp_Email', 'txtCondtDateTemp_Email', resultObject[0].Condition_Value, true);
                $('#<%=cboOperator_Email.ClientID%>').val(resultObject[0].Operator_ID);
            }

            var resultObjectSub = resultObject[0].WF_Steps_For_Template;
            $('#<%=chkAllowedWorkflowStepEmail.ClientID%> input').each(function () {
                for (var i = 0; i < resultObjectSub.length; i++) {
                    if (resultObjectSub[i].Workflow_Step_ID.trim() == $(this).val().trim()) {
                        onCheckBox(this);
                    }
                }
            });

            CheckMeetingRequest();
        }

        function CheckMeetingRequest() {
            if ($("#<%=chkSendMeetingRequest.ClientID%>").prop('checked') == true) {
                $("#divMeetingRequest").removeClass('hide');
            }
            else {
                $("#divMeetingRequest").addClass('hide');
            }
        }

        function ClearControlsFile(reload_codt_drps) {
            $('#divCondition_File').addClass('hide');

            clearTextBox(['<%=txtFileName.ClientID%>', '<%=txtExtension.ClientID%>', '<%=txtFileContent.ClientID%>']);
            clearCheckListBox(['<%=chkAllowedWorkflowStep_File.ClientID%>']);
            clearCheckBox(['<%=chkOneFile_File.ClientID%>', '<%=chkCondition_File.ClientID%>']);
            clearDropDown(['<%=cboDocType_File.ClientID%>', '<%=cboAccessLevel_File.ClientID%>', '<%=cboCreationTime_File.ClientID%>', '<%=cboStepField_File.ClientID%>', '<%=cboOperator_File.ClientID%>']);

            CheckConditions('<%=chkCondition_File.ClientID%>', '<%=cboStepField_File.ClientID%>', '<%=cboOperator_File.ClientID%>', '<%=txtCriteria_File.ClientID%>', 'divCondition_File', 'cboCondtTemp_File', 'txtCondtDateTemp_File', '', reload_codt_drps);
            return false;
        }

        function ResetControlsFile() {
            if ($("#<%=hndDocumentID.ClientID%>").val().trim() == '0') {
                ClearControlsFile(true);
            }
            else {
                LoadValuesFile();
            }
            return false;
        }

        function LoadValuesFile() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetWFDocumentTemplate",
                data: { Document_ID: $("#<%=hndDocumentID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesFilePassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesFilePassed(result) {
            ClearControlsFile(false);

            var resultObject = result;
            $('#<%=txtFileName.ClientID%>').val(resultObject[0].Document_Name);
            $('#<%=cboCreationTime_File.ClientID%>').val(resultObject[0].At_Begining);
            $('#<%=cboDocType_File.ClientID%>').val(resultObject[0].Document_Type);
            $('#<%=cboAccessLevel_File.ClientID%>').val(resultObject[0].Access_Level);
            $('#<%=txtFileContent.ClientID%>').val(resultObject[0].Document_Body);
            $('#<%=txtExtension.ClientID%>').val(resultObject[0].Document_Title);

            if (resultObject[0].Keep_Single_Doc == "1") {
                onCheckBox('#<%=chkOneFile_File.ClientID%>');
            }

            var resultObjectSub = resultObject[0].WF_Steps_For_Template;
            $('#<%=chkAllowedWorkflowStep_File.ClientID%> input').each(function () {
                for (var i = 0; i < resultObjectSub.length; i++) {
                    if (resultObjectSub[i].Workflow_Step_ID.trim() == $(this).val().trim()) {
                        onCheckBox(this);
                    }
                }
            });
            if (resultObject[0].Enable_Conditions == "1") {
                onCheckBox('#<%=chkCondition_File.ClientID%>');
                $("#<%=cboStepField_File.ClientID%> > option").each(function () {
                    if ($(this).val().split('_')[0].trim() == resultObject[0].Workflow_Step_Field_ID) {
                        $('#<%=cboStepField_File.ClientID%>').val($(this).val());
                    }
                });
                CheckConditions('<%=chkCondition_File.ClientID%>', '<%=cboStepField_File.ClientID%>', '<%=cboOperator_File.ClientID%>', '<%=txtCriteria_File.ClientID%>', 'divCondition_File', 'cboCondtTemp_File', 'txtCondtDateTemp_File', resultObject[0].Condition_Value, true);
                $('#<%=cboOperator_File.ClientID%>').val(resultObject[0].Operator_ID);
            }
        }
    </script>
</asp:Content>
