<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid_wf.master" AutoEventWireup="true" CodeBehind="workflow.aspx.cs" Inherits="N_Ter_Tasks.workflow" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Workflows
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
    <link href="assets/stylesheets/pages.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Workflow -
    <asp:Literal ID="ltrWorkflowName" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div style="margin: -18px -20px;" class="border-t">
        <div class="mail-nav">
            <div class="compose-btn text-right">
                <div class="pull-left" style="margin-top: 4px">
                    Steps
                </div>
                <button id="cmdFlowchart" runat="server" class="btn btn-labeled btn-success" title="View Flowchart" onclick="return drawFlowchart();"><i class="fa fa-qrcode" onclick="return showFlowchart()"></i></button>
                <button id="cmdNewStep" runat="server" class="btn btn-labeled btn-primary" title="New Step" onserverclick="cmdNewStep_ServerClick"><i class="fa fa-plus"></i></button>
            </div>
            <asp:HiddenField ID="hndWFStepID" runat="server" />
            <asp:Button ID="cmdLoadStep" runat="server" Text="Button" Style="display: none" OnClick="cmdLoadStep_Click" />
            <div class="navigation">
                <ul id="ul_steps" class="sections">
                    <li class="mail-select-folder active"><a href="#">Select a Step...</a></li>
                    <asp:Literal ID="ltrSteps" runat="server"></asp:Literal>
                </ul>
            </div>
        </div>
        <asp:Panel ID="pnlStep" runat="server" CssClass="mail-container">
            <div class="mail-container-header show">
                <asp:Literal ID="ltrStepMode" runat="server"></asp:Literal>
                <div class="pull-right">
                    <button id="cmdDuplicateStep" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-files-o"></span>Create Duplicate Step</button>
                    <asp:ModalPopupExtender ID="cmdDuplicate_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDuplicateStep" TargetControlID="cmdDuplicateStep" CancelControlID="cmdCancelDuplicate" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                    <button id="cmdDeleteStep" runat="server" class="btn btn-labeled btn-danger"><span class="btn-label icon fa fa-times"></span>Delete Step</button>
                    <asp:ModalPopupExtender ID="cmdDeleteStep_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlStepDelete" TargetControlID="cmdDeleteStep" CancelControlID="cmdStepDeleteNo" BackgroundCssClass="at_modelpopup_background">
                    </asp:ModalPopupExtender>
                </div>
            </div>
            <div class="new-mail-form form-horizontal">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <span class="panel-title">Main Step Details</span>
                    </div>
                    <div class="panel-body" style="padding-bottom: 0px!important">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-sm-3 control-label pt14">Step Name</label>
                                <div class="col-sm-9" style="padding-top: 7px">
                                    <asp:TextBox ID="txtStepName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label pt14">Step Weight</label>
                                <div class="col-sm-9" style="padding-top: 7px">
                                    <asp:TextBox ID="txtStepWeight" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label pt14">User Group</label>
                                <div class="col-sm-9" style="padding-top: 7px">
                                    <asp:DropDownList ID="cboUserGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label pt14">Milestone</label>
                                <div class="col-sm-9" style="padding-top: 7px">
                                    <asp:DropDownList ID="cboMilestone" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label pt14">Addon</label>
                                <div class="col-sm-9" style="padding-top: 7px">
                                    <asp:DropDownList ID="cboAddon" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label pt14">Step Duration</label>
                                <div class="col-sm-9" style="padding-top: 7px">
                                    <asp:DropDownList ID="cboStepDuration" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">Unlimited</asp:ListItem>
                                        <asp:ListItem Value="16">1 hour</asp:ListItem>
                                        <asp:ListItem Value="17">2 hours</asp:ListItem>
                                        <asp:ListItem Value="18">3 hours</asp:ListItem>
                                        <asp:ListItem Value="19">5 hours</asp:ListItem>
                                        <asp:ListItem Value="20">7 hours</asp:ListItem>
                                        <asp:ListItem Value="21">10 hours</asp:ListItem>
                                        <asp:ListItem Value="22">20 hours</asp:ListItem>
                                        <asp:ListItem Value="1">1 Day</asp:ListItem>
                                        <asp:ListItem Value="2">2 Days</asp:ListItem>
                                        <asp:ListItem Value="3">3 Days</asp:ListItem>
                                        <asp:ListItem Value="4">4 Days</asp:ListItem>
                                        <asp:ListItem Value="5">5 Days</asp:ListItem>
                                        <asp:ListItem Value="6">6 Days</asp:ListItem>
                                        <asp:ListItem Value="7">1 Week</asp:ListItem>
                                        <asp:ListItem Value="8">2 Weeks</asp:ListItem>
                                        <asp:ListItem Value="9">3 Weeks</asp:ListItem>
                                        <asp:ListItem Value="10">1 Month</asp:ListItem>
                                        <asp:ListItem Value="11">2 Months</asp:ListItem>
                                        <asp:ListItem Value="12">3 Months</asp:ListItem>
                                        <asp:ListItem Value="13">4 Months</asp:ListItem>
                                        <asp:ListItem Value="14">6 Months</asp:ListItem>
                                        <asp:ListItem Value="15">1 Year</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label pt14">
                                    Move to<br />
                                    (if Step Duration fails)</label>
                                <div class="col-sm-9" style="padding-top: 7px">
                                    <asp:DropDownList ID="cboMoveToStep" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label pt14">Hours to Task Due Date</label>
                                <div class="col-sm-9" style="padding-top: 7px">
                                    <asp:TextBox ID="txtHoursBeforeDeDate" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label pt14">Step Number</label>
                                <div class="col-sm-9" style="padding-top: 7px">
                                    <asp:DropDownList ID="cboStepNumber" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label pt14">Help Text</label>
                                <div class="col-sm-9" style="padding-top: 7px">
                                    <asp:TextBox ID="txtHelpText" runat="server" CssClass="form-control" TextMode="MultiLine" Style="height: 100px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label pt14">
                                    Next Step<br />
                                    (if All Conditions Evaluate to "False")</label>
                                <div class="col-sm-9" style="padding-top: 7px">
                                    <asp:DropDownList ID="cboNextStep" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label pt14">Step Options</label>
                                <div class="col-sm-9" style="padding-top: 7px">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Task Details Header Text</label>
                                            <asp:TextBox ID="txtTaskDetailsHeader" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Task Post Header Text</label>
                                            <asp:TextBox ID="txtTaskPostHeader" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 text-bold">
                                            Step Access Rights
                                        </div>
                                        <div class="col-md-12 border-t"></div>
                                        <div class="col-md-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkCheckHierarchy" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Check for User Hierarchy</span>
                                        </div>
                                        <div class="col-md-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkShowToCreator" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Show Only to Creator (if Creator is in the Group)</span>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkEditExtraField" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Allow Edit of Special Fields</span>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkEditDueDate" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Allow Edit of Due Date</span>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkEditEL2" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Allow Edit of
                                                <asp:Literal ID="ltrEL2" runat="server"></asp:Literal></span>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkEditQueue" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Allow Edit of Task Queue</span>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkAlloEditTL" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Allow Edit of Task Step Deadlines</span>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkDeleteComments" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Allow Delete Own Comments</span>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkDeleteAddons" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Allow Delete Own Task Addons</span>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkDeleteDocuments" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Allow Delete Own Task Documents</span>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkAllowCancellations" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Allow Task Cancellation</span>
                                        </div>
                                        <div class="col-md-12 mt10 text-bold">
                                            Step Type
                                        </div>
                                        <div class="col-md-12 border-t"></div>
                                        <div class="col-md-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkApproverStep" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">This is an Approver Step</span>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <div class="input-group">
                                                <asp:CheckBox ID="chkAutoSubmit" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">This is an Auto Submit Step</span>
                                                <div id="iAutoSubmit" runat="server" class="input-group-addon no-background no-border no-padding-vr" data-toggle="tooltip" data-placement="left" data-original-title="Only works with Text Fields that has Default Text">
                                                    <span><i class="fa fa-2x fa-exclamation-circle"></i></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkLastStep" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Can be the Last Step</span>
                                        </div>
                                        <div class="col-md-12 mt10 text-bold">
                                            Step Posting
                                        </div>
                                        <div class="col-md-12 border-t"></div>
                                        <div class="col-md-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkShowHistory" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Show Task History</span>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkShowDocuments" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Show Task Documents</span>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkShowTimeline" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Show Task Timeline</span>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkShowChats" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Show Task Chats</span>
                                        </div>
                                        <div class="col-md-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkAllowMultipost" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Allow Multiple Posting</span>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkCreator_Cannot_Submit" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Creator Cannot Submit this Step</span>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkRemain" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Remain in Task after Submission</span>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <div class="input-group">
                                                <asp:CheckBox ID="chkCustomPost" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Use Custom Post Screen</span>
                                                <div id="iCustomPost" runat="server" class="input-group-addon no-background no-border no-padding-vr" data-toggle="tooltip" data-placement="left" data-original-title="Field Categories, Conditions and Folumas will be Omitted">
                                                    <span><i class="fa fa-2x fa-exclamation-circle"></i></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkStepLinks" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Display Links to Custom Screens</span>
                                            <div id="divLinkedScreens" runat="server" class="row" style="padding-left: 15%">
                                                <div class="col-md-12">
                                                    <b>Custom Screens</b>
                                                </div>
                                                <div id="divLinkedAvailable" runat="server" class="col-md-12 pt5">
                                                    <asp:CheckBoxList ID="chkCustomScreens" runat="server" CssClass="checkboxlist"></asp:CheckBoxList>
                                                </div>
                                                <div id="divLinkedNotAvailable" runat="server" class="col-md-12 pt5">
                                                    <div class="alert alert-success">
                                                        No Custom Screens available
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 mt10 text-bold">
                                            Other
                                        </div>
                                        <div class="col-md-12 border-t"></div>
                                        <div class="col-md-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkProductivity" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Take for Productivity Calculations</span>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkCreateEL2" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px"><asp:Literal ID="ltrCreateEL2" runat="server"></asp:Literal></span>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkDeactivateEL2" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px"><asp:Literal ID="ltrDeactivateEL2" runat="server"></asp:Literal></span>
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 7px">
                                            <asp:CheckBox ID="chkShowReject" runat="server" CssClass="checkboxlist" /><span style="padding-left: 10px">Show Reject Button</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="cmdSaveStep" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSaveStep_Click" />
                        <asp:Button ID="cmdCancel" runat="server" Text="Reset" CssClass="btn btn-default" OnClick="cmdCancel_Click" />
                    </div>
                </div>
                <asp:Panel ID="pnlStepFieldCats" runat="server" CssClass="panel panel-info">
                    <asp:HiddenField ID="hndStepFieldCat_ID" runat="server" />
                    <div class="panel-heading">
                        <div class="at_modelpopup_add">
                            <button id="cmdNewFieldCat" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Field Catgory</button>
                            <asp:ModalPopupExtender ID="cmdNewFieldCat_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlFieldCat" TargetControlID="cmdNewFieldCat" CancelControlID="cmdCancelFieldCat" BackgroundCssClass="at_modelpopup_background">
                            </asp:ModalPopupExtender>
                        </div>
                        <span class="panel-title">Step Field Categories</span>
                    </div>
                    <div class="panel-body" style="padding-bottom: 0px!important">
                        <div class="table-responsive no-margin-b">
                            <asp:GridView ID="gvStepFieldCats" runat="server" CssClass="table table-striped table-hover grid_table non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvStepFieldCats_RowDataBound" RowStyle-CssClass="group field_cat_row">
                                <Columns>
                                    <asp:BoundField DataField="Workflow_Step_Field_Cat_ID" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <button id='cmdEditStepFieldCat' type='submit' runat="server" class="btn btn-primary btn-xs" title="Edit"><i class="fa fa-edit button_icon"></i></button>
                                            <asp:ModalPopupExtender ID="cmdEditStepFieldCat_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlFieldCat" TargetControlID="cmdEditStepFieldCat" CancelControlID="cmdCancelFieldCat" BackgroundCssClass="at_modelpopup_background">
                                            </asp:ModalPopupExtender>
                                        </ItemTemplate>
                                        <ItemStyle Width="22px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <button id='cmdDeleteStepFieldCat' type='submit' runat="server" class="btn btn-danger btn-xs" title="Delete"><i class="fa fa-trash-o button_icon"></i></button>
                                            <asp:ModalPopupExtender ID="cmdDeleteStepField_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDeleteFieldCat" TargetControlID="cmdDeleteStepFieldCat" CancelControlID="cmdCalcelDelFieldCat" BackgroundCssClass="at_modelpopup_background">
                                            </asp:ModalPopupExtender>
                                        </ItemTemplate>
                                        <ItemStyle Width="22px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Workflow_Step_Field_Cat" HeaderText="Field Category Name"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <i class="fa fa-bars"></i>
                                        </ItemTemplate>
                                        <ItemStyle Width="10px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlFields" runat="server" CssClass="panel panel-info">
                    <asp:HiddenField ID="hndField_ID" runat="server" />
                    <div class="panel-heading">
                        <div class="at_modelpopup_add">
                            <button id="cmdNewStepField" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Field</button>
                            <asp:ModalPopupExtender ID="cmdNewStepField_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlFieldData" TargetControlID="cmdNewStepField" CancelControlID="cmdCancelField" BackgroundCssClass="at_modelpopup_background">
                            </asp:ModalPopupExtender>
                        </div>
                        <span class="panel-title">Step Fields</span>
                    </div>
                    <div class="panel-body" style="padding-bottom: 0px!important">
                        <div class="table-responsive no-margin-b">
                            <asp:GridView ID="gvStepFields" runat="server" CssClass="table table-striped table-hover grid_table non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvStepFields_RowDataBound" RowStyle-CssClass="group field_row">
                                <Columns>
                                    <asp:BoundField DataField="Workflow_Step_Field_ID" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <button id='cmdEditStepField' type='submit' runat="server" class="btn btn-primary btn-xs" title="Edit"><i class="fa fa-edit button_icon"></i></button>
                                            <asp:ModalPopupExtender ID="cmdEditStepField_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlFieldData" TargetControlID="cmdEditStepField" CancelControlID="cmdCancelField" BackgroundCssClass="at_modelpopup_background">
                                            </asp:ModalPopupExtender>
                                        </ItemTemplate>
                                        <ItemStyle Width="22px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <button id='cmdDeleteStepField' type='submit' runat="server" class="btn btn-danger btn-xs" title="Delete"><i class="fa fa-trash-o button_icon"></i></button>
                                            <asp:ModalPopupExtender ID="cmdDeleteStepField_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDeleteField" TargetControlID="cmdDeleteStepField" CancelControlID="cmdCalcelDelField" BackgroundCssClass="at_modelpopup_background">
                                            </asp:ModalPopupExtender>
                                        </ItemTemplate>
                                        <ItemStyle Width="22px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Field_Name" HeaderText="Field Name"></asp:BoundField>
                                    <asp:BoundField DataField="Field_TypeSP" HeaderText="Field Type">
                                        <ItemStyle Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Field_SizeSP" HeaderText="Field Type">
                                        <ItemStyle Width="120px" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <i class="fa fa-bars"></i>
                                        </ItemTemplate>
                                        <ItemStyle Width="10px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlFieldConditions" runat="server" CssClass="panel panel-info">
                    <asp:HiddenField ID="hndFieldCond_ID" runat="server" />
                    <div class="panel-heading">
                        <div class="at_modelpopup_add">
                            <button id="cmdNewFieldCond" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Condition</button>
                            <asp:ModalPopupExtender ID="cmdNewFieldCond_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlConditionData" TargetControlID="cmdNewFieldCond" CancelControlID="cmdCancelCond" BackgroundCssClass="at_modelpopup_background">
                            </asp:ModalPopupExtender>
                        </div>
                        <span class="panel-title">Step Conditions</span>
                    </div>
                    <div class="panel-body" style="padding-bottom: 0px!important">
                        <div class="table-responsive no-margin-b">
                            <asp:GridView ID="gvStepConditions" runat="server" CssClass="table table-striped table-hover grid_table non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvStepConditions_RowDataBound" RowStyle-CssClass="group field_con_row">
                                <Columns>
                                    <asp:BoundField DataField="Condition_ID" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <button id='cmdEditStepCond' type='submit' runat="server" class="btn btn-primary btn-xs" title="Edit"><i class="fa fa-edit button_icon"></i></button>
                                            <asp:ModalPopupExtender ID="cmdEditStepCond_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlConditionData" TargetControlID="cmdEditStepCond" CancelControlID="cmdCancelCond" BackgroundCssClass="at_modelpopup_background">
                                            </asp:ModalPopupExtender>
                                        </ItemTemplate>
                                        <ItemStyle Width="22px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <button id='cmdDeleteStepFieldCond' type='submit' runat="server" class="btn btn-danger btn-xs" title="Delete"><i class="fa fa-trash-o button_icon"></i></button>
                                            <asp:ModalPopupExtender ID="cmdDeleteStepFieldCond_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDeleteCondition" TargetControlID="cmdDeleteStepFieldCond" CancelControlID="cmdCancelDelFieldCond" BackgroundCssClass="at_modelpopup_background">
                                            </asp:ModalPopupExtender>
                                        </ItemTemplate>
                                        <ItemStyle Width="22px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Field_Name" HeaderText="Field Name"></asp:BoundField>
                                    <asp:BoundField DataField="Operator_Name" HeaderText="Operator">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Evaluate_With" HeaderText="Compare To">
                                        <ItemStyle Width="120px" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <i class="fa fa-bars"></i>
                                        </ItemTemplate>
                                        <ItemStyle Width="10px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlStepFormula" runat="server" CssClass="panel panel-info">
                    <asp:HiddenField ID="hndFormula_ID" runat="server" />
                    <asp:HiddenField ID="hndFormula_Type" runat="server" />
                    <div class="panel-heading">
                        <div class="at_modelpopup_add">
                            <button id="cmdDateDiffFormula" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Formula (Date Diff)</button>
                            <asp:ModalPopupExtender ID="cmdDateDiffFormula_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlFormula" TargetControlID="cmdDateDiffFormula" CancelControlID="cmdCancelFormula" BackgroundCssClass="at_modelpopup_background">
                            </asp:ModalPopupExtender>
                            <button id="cmdDateAddFormula" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Formula (Add Days)</button>
                            <asp:ModalPopupExtender ID="cmdDateAddFormula_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlFormula" TargetControlID="cmdDateAddFormula" CancelControlID="cmdCancelFormula" BackgroundCssClass="at_modelpopup_background">
                            </asp:ModalPopupExtender>
                            <button id="cmdNumberFormula" runat="server" class="btn btn-labeled btn-primary"><span class="btn-label icon fa fa-plus"></span>New Formula (Numbers)</button>
                            <asp:ModalPopupExtender ID="cmdNumberFormula_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlFormula" TargetControlID="cmdNumberFormula" CancelControlID="cmdCancelFormula" BackgroundCssClass="at_modelpopup_background">
                            </asp:ModalPopupExtender>
                        </div>
                        <span class="panel-title">Step Formulas</span>
                    </div>
                    <div class="panel-body" style="padding-bottom: 0px!important">
                        <div class="table-responsive no-margin-b">
                            <asp:GridView ID="gvFormulas" runat="server" CssClass="table table-striped table-hover grid_table non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvFormulas_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="Formula_ID" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <button id='cmdEditStepFormula' type='submit' runat="server" class="btn btn-primary btn-xs" title="Edit"><i class="fa fa-edit button_icon"></i></button>
                                            <asp:ModalPopupExtender ID="cmdEditStepFormula_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlFormula" TargetControlID="cmdEditStepFormula" CancelControlID="cmdCancelFormula" BackgroundCssClass="at_modelpopup_background">
                                            </asp:ModalPopupExtender>
                                        </ItemTemplate>
                                        <ItemStyle Width="22px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <button id='cmdDeleteStepFormula' type='submit' runat="server" class="btn btn-danger btn-xs" title="Delete"><i class="fa fa-trash-o button_icon"></i></button>
                                            <asp:ModalPopupExtender ID="cmdDeleteStepFormula_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDeleteFormula" TargetControlID="cmdDeleteStepFormula" CancelControlID="cmdCancelDelFormula" BackgroundCssClass="at_modelpopup_background">
                                            </asp:ModalPopupExtender>
                                        </ItemTemplate>
                                        <ItemStyle Width="22px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Field_Name" HeaderText="Calculation For"></asp:BoundField>
                                    <asp:BoundField DataField="Formula_TypeSP" HeaderText="Formula Type">
                                        <ItemStyle Width="120px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </asp:Panel>
    </div>
    <asp:Panel ID="pnlStepDelete" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_step_indent" class="at_modelpopup_indent">
            <div id="at_del_step_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete - Step</h4>
                </div>
                <div id="at_del_step_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdStepDeleteOK" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdStepDeleteOK_Click" />
                    <asp:Button ID="cmdStepDeleteNo" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDeleteFieldCat" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_field_cat_indent" class="at_modelpopup_indent">
            <div id="at_del_field_cat_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete - Field Category</h4>
                </div>
                <div id="at_del_field_cat_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDeleteFieldCat" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDeleteFieldCat_Click" />
                    <asp:Button ID="cmdCalcelDelFieldCat" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDeleteField" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_field_indent" class="at_modelpopup_indent">
            <div id="at_del_field_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete - Field</h4>
                </div>
                <div id="at_del_field_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDeleteField" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDeleteField_Click" />
                    <asp:Button ID="cmdCalcelDelField" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDeleteCondition" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_cond_indent" class="at_modelpopup_indent">
            <div id="at_del_cond_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete - Field Condition</h4>
                </div>
                <div id="at_del_cond_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDeleteFieldCond" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDeleteFieldCond_Click" />
                    <asp:Button ID="cmdCancelDelFieldCond" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDeleteFormula" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_del_formula_indent" class="at_modelpopup_indent">
            <div id="at_del_formula_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <h4 class="panel-title">Delete - Field Condition</h4>
                </div>
                <div id="at_del_formula_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        Are you sure ?
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdDeleteFieldFormula" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="cmdDeleteFieldFormula_Click" />
                    <asp:Button ID="cmdCancelDelFormula" runat="server" Text="No" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlFieldData" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_field_indent" class="at_modelpopup_indent">
            <div id="at_model_field_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCancelField" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add/Edit Workflow Step Field</h4>
                </div>
                <div id="at_model_field_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Field Name</label>
                            <asp:TextBox ID="txtFieldName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Field Type</label>
                            <asp:DropDownList ID="cboFieldType" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">Label Only</asp:ListItem>
                                <asp:ListItem Value="1">Yes/No (Dropdown)</asp:ListItem>
                                <asp:ListItem Value="13">Yes/No (Switch)</asp:ListItem>
                                <asp:ListItem Value="2">Text</asp:ListItem>
                                <asp:ListItem Value="3">Memo</asp:ListItem>
                                <asp:ListItem Value="4">Number</asp:ListItem>
                                <asp:ListItem Value="8">Currency</asp:ListItem>
                                <asp:ListItem Value="9">Percentage</asp:ListItem>
                                <asp:ListItem Value="5">Selection</asp:ListItem>
                                <asp:ListItem Value="6">Date</asp:ListItem>
                                <asp:ListItem Value="7">Time</asp:ListItem>
                                <asp:ListItem Value="12">Time Span</asp:ListItem>
                                <asp:ListItem Value="11">File Upload</asp:ListItem>
                                <asp:ListItem Value="10">Master Table</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div id="accessLevel" class="form-group">
                            <label>Access Level</label>
                            <asp:DropDownList ID="cboAccessLevel" runat="server" CssClass="form-control">
                                <asp:ListItem Value="3">Level 3</asp:ListItem>
                                <asp:ListItem Value="2">Level 2</asp:ListItem>
                                <asp:ListItem Value="1">Level 1</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div id="defaultDocCat" class="form-group">
                            <label>Default Upload Category</label>
                            <asp:DropDownList ID="cboDocCat" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div id="divMultipleFiles" class="form-group">
                            <asp:CheckBox Text="" runat="server" ID="chkAllowMltUpload" CssClass="checkboxlist" /><span style="padding-left: 10px">Allow Multiple File Uploads</span>
                        </div>
                        <div id="divTypable" class="form-group">
                            <asp:CheckBox Text="" runat="server" ID="chkTypable" CssClass="checkboxlist" /><span style="padding-left: 10px">Enable Search</span>
                        </div>
                        <div id="default_text" class="form-group">
                            <label>Default Text</label>
                            <asp:TextBox ID="txtDefaultText" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div id="default_text_memo" class="form-group">
                            <label>Default Text</label>
                            <asp:TextBox ID="txtDefaultTextMemo" runat="server" CssClass="form-control" TextMode="MultiLine" Height="75"></asp:TextBox>
                        </div>
                        <div id="max_length" class="form-group">
                            <label>Maximum Lenght</label>
                            <asp:TextBox runat="server" CssClass="form-control" TextMode="Number" ID="txtFieldMaxLength" Min="0" />
                        </div>
                        <div id="selection_text" class="form-group">
                            <label>Selection Text</label>
                            <asp:TextBox ID="txtSelectionText" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div id="masterTable" class="form-group">
                            <label>Master Table</label>
                            <asp:DropDownList ID="cboMasterTable" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div id="masterTableParamType" class="form-group">
                            <label>Master Table Parameter</label>
                            <asp:DropDownList ID="cboMasterTableParamType" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">[No Parameter]</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div id="masterTableParam2Type" class="form-group">
                            <label>Master Table Parameter 2</label>
                            <asp:DropDownList ID="cboMasterTableParam2Type" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">[No Parameter]</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Field Size</label>
                            <asp:DropDownList ID="cboFieldSize" runat="server" CssClass="form-control">
                                <asp:ListItem Value="1">Full Width</asp:ListItem>
                                <asp:ListItem Value="2">1/2 Width</asp:ListItem>
                                <asp:ListItem Value="3">1/3 Width</asp:ListItem>
                                <asp:ListItem Value="4">2/3 Width</asp:ListItem>
                                <asp:ListItem Value="5">1/4 Width</asp:ListItem>
                                <asp:ListItem Value="6">3/4 Width</asp:ListItem>
                                <asp:ListItem Value="7">1/6 Width</asp:ListItem>
                                <asp:ListItem Value="8">5/6 Width</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div id="validateField" class="form-group">
                            <label>Validate with</label>
                            <asp:DropDownList ID="cboValidateField" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div id="copyData" class="form-group">
                            <label>Copy Data From</label>
                            <asp:DropDownList ID="cboCopyDataFrom" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div id="divDisableControl" class="form-group">
                            <asp:CheckBox Text="" runat="server" ID="chkDisableControl" CssClass="checkboxlist" /><span style="padding-left: 10px">Make Control Read Only</span>
                        </div>
                        <div class="form-group">
                            <label>Field Category</label>
                            <asp:DropDownList ID="cboFieldCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div id="helpContent" class="form-group">
                            <label>Help Text</label>
                            <asp:TextBox ID="txtFieldHelpText" runat="server" CssClass="form-control" TextMode="MultiLine" Style="height: 100px"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Custom Data</label>
                            <asp:TextBox ID="txtCustomData" runat="server" CssClass="form-control" TextMode="MultiLine" Style="height: 100px"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox Text="" runat="server" ID="chkShowInHistory" CssClass="checkboxlist" /><span style="padding-left: 10px">Show in Task History</span>
                        </div>
                        <div id="requiredField" class="form-group">
                            <asp:CheckBox Text="" runat="server" ID="chkFieldRequired" CssClass="checkboxlist" /><span style="padding-left: 10px">Required Field</span>
                        </div>
                        <div id="copyEF1" class="form-group">
                            <asp:CheckBox Text="" runat="server" ID="chkCopyEF1" CssClass="checkboxlist" /><span style="padding-left: 10px">Copy to Special Field 1 (<asp:Literal ID="ltrEx1Name" runat="server"></asp:Literal>)</span>
                        </div>
                        <div id="copyEF2" class="form-group">
                            <asp:CheckBox Text="" runat="server" ID="chkCopyEF2" CssClass="checkboxlist" /><span style="padding-left: 10px">Copy to Special Field 2 (<asp:Literal ID="ltrEx2Name" runat="server"></asp:Literal>)</span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSaveField" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSaveField_Click" />
                    <asp:Button ID="cmdResetField" runat="server" Text="Reset" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlFieldCat" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_field_cat_indent" class="at_modelpopup_indent">
            <div id="at_model_field_cat_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCancelFieldCat" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add/Edit Workflow Step Field Category</h4>
                </div>
                <div id="at_model_field_cat_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Category Name</label>
                            <asp:TextBox ID="txtStepFieldCat" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSaveFieldCat" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSaveFieldCat_Click" />
                    <asp:Button ID="cmdResetFieldCat" runat="server" Text="Reset" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlConditionData" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_cond_indent" class="at_modelpopup_indent">
            <div id="at_model_cond_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCancelCond" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add/Edit Workflow Step Condition</h4>
                </div>
                <div id="at_model_cond_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Condition On</label>
                            <asp:DropDownList ID="cboConditionOn" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
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
                        <div class="form-group">
                            <label>Criteria</label>
                            <asp:TextBox ID="txtEvaluateWith" runat="server" CssClass="form-control"></asp:TextBox>
                            <select id="cboCondtTemp" class="form-control"></select>
                            <input type="text" id="txtCondtDateTemp" class="form-control dtapi"/>
                        </div>
                        <div class="form-group">
                            <label>Move To (if True)</label>
                            <asp:DropDownList ID="cboMoveToIfTrue" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSaveCondition" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSaveCondition_Click" />
                    <asp:Button ID="cmdResetCondition" runat="server" Text="Reset" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlFormula" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_formula_indent" class="at_modelpopup_indent">
            <div id="at_model_formula_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCancelFormula" runat="server" Text="&times;" class="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Add/Edit Workflow Step Formula</h4>
                </div>
                <div id="at_model_formula_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div id="FormulaType1">
                            <div class="form-group">
                                <label>Date Difference (Result = Date 1, Time 1 - Date 2, Time 2)</label>
                            </div>
                            <div class="row" style="margin-left: 0px; margin-right: 0px">
                                <div class="col-sm-6" style="padding-left: 0px">
                                    <div class="form-group">
                                        <label>Date 1</label>
                                        <asp:DropDownList ID="cboT1Date1" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6" style="padding-right: 0px">
                                    <div class="form-group">
                                        <label>Time 1</label>
                                        <asp:DropDownList ID="cboT1Time1" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-left: 0px; margin-right: 0px">
                                <div class="col-sm-6" style="padding-left: 0px">
                                    <div class="form-group">
                                        <label>Date 2</label>
                                        <asp:DropDownList ID="cboT1Date2" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6" style="padding-right: 0px">
                                    <div class="form-group">
                                        <label>Time 2</label>
                                        <asp:DropDownList ID="cboT1Time2" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Result in</label>
                                <asp:DropDownList ID="cboT1Result" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div id="FormulaType2">
                            <div class="form-group">
                                <label>Add Days (Result = Date 1 + Number of Days)</label>
                            </div>
                            <div class="row" style="margin-left: 0px; margin-right: 0px">
                                <div class="col-sm-6" style="padding-left: 0px">
                                    <div class="form-group">
                                        <label>Date 1</label>
                                        <asp:DropDownList ID="cboT2Date1" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6" style="padding-right: 0px">
                                    <div class="form-group">
                                        <label>Number of Days</label>
                                        <asp:DropDownList ID="cboT2Days" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Result in</label>
                                <asp:DropDownList ID="cboT2Result" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div id="FormulaType3">
                            <div class="form-group">
                                <label>Numbers (Result = Field 1 [Operator] Field 2)</label>
                            </div>
                            <div class="row" style="margin-left: 0px; margin-right: 0px">
                                <div class="col-sm-4" style="padding-left: 0px">
                                    <div class="form-group">
                                        <label>Number Field 1</label>
                                        <asp:DropDownList ID="cboT3Number1" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>Operator</label>
                                        <asp:DropDownList ID="cboT3Operator" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="1">Add (+)</asp:ListItem>
                                            <asp:ListItem Value="2">Substract (-)</asp:ListItem>
                                            <asp:ListItem Value="3">Multiply (x)</asp:ListItem>
                                            <asp:ListItem Value="4">Divide (/)</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4" style="padding-right: 0px">
                                    <div class="form-group">
                                        <label>Number Field 1</label>
                                        <asp:DropDownList ID="cboT3Number2" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Result in</label>
                                <asp:DropDownList ID="cboT3Result" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdSaveFormula" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="cmdSaveFormula_Click" />
                    <asp:Button ID="cmdResetFormula" runat="server" Text="Reset" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDuplicateStep" runat="server" CssClass="at_modelpopup_container" Style="display: none">
        <div id="at_model_dup_indent" class="at_modelpopup_indent">
            <div id="at_model_dup_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                <div class="panel-heading">
                    <div class="at_modelpopup_add">
                        <asp:Button ID="cmdCancelDuplicate" runat="server" Text="&times;" CssClass="btn btn-default" />
                    </div>
                    <h4 class="panel-title">Duplicate Step</h4>
                </div>
                <div id="at_model_dup_content" class="at_modelpopup_content styled-bar">
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Step to Duplicate</label>
                            <asp:TextBox ID="txtCurrentStep" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>New Step Name</label>
                            <asp:TextBox ID="txtNewStep" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cmdCreateDuplicate" runat="server" Text="Create Duplicate Step" CssClass="btn btn-primary" OnClick="cmdCreateDuplicate_Click" />
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
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script src="assets/javascripts/flowchart/raphael.min.js"></script>
    <script src="assets/javascripts/flowchart/flowchart.min.js"></script>
    <script src="assets/javascripts/flowchart/jquery.flowchart.js"></script>
    <script src="assets/javascripts/app_scripts/operator_pages.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $(window).resize(function () {
                ArrangePoups();
            });
        });

        init.push(function () {
            ArrangePoups();
            ArrangeNavigation();
            ArrangeAutopostTooltip();
            ArrangeStepSort();
            ArrangeFieldCatSort();
            ArrangeFieldSort();
            ArrangeFieldConditionSort();
            InitialteCondControls('<%=txtEvaluateWith.ClientID%>', 'dtapi', 'cboCondtTemp', 'txtCondtDateTemp', '<%=cboConditionOn.ClientID%>');
        });

        function ArrangePoups() {
            AdjustPopupSize(167, 600, 'at_model_field_cat');
            AdjustPopupSize(167, 600, 'at_model_field');
            AdjustPopupSize(167, 600, 'at_model_cond');
            AdjustPopupSize(167, 600, 'at_model_formula');
            AdjustPopupSize(80, 400, 'at_del_step');
            AdjustPopupSize(80, 400, 'at_del_field_cat');
            AdjustPopupSize(80, 400, 'at_del_field');
            AdjustPopupSize(80, 400, 'at_del_cond');
            AdjustPopupSize(80, 400, 'at_del_formula');
            AdjustPopupSize(167, 800, 'at_model_dup');
            AdjustPopupSize(167, 1800, 'at_model_flch');
        }

        function ArrangeNavigation() {
            $('.mail-nav .navigation li.active a').click(function () {
                $('.mail-nav .navigation').toggleClass('open');
                return false;
            });
        }

        function ArrangeAutopostTooltip() {
            $('#<%=iAutoSubmit.ClientID%>').tooltip();
            $('#<%=iCustomPost.ClientID%>').tooltip();
        }

        function ArrangeStepSort() {
            $("#ul_steps").sortable({
                axis: "y",
                handle: "i",
                stop: function (event, ui) {
                    ui.item.children("i").triggerHandler("focusout");
                    var intIndex = 1;
                    var strIDs = "";
                    $(".stp_no").each(function () {
                        $(this).html('Step ' + intIndex);
                        strIDs = strIDs + $(this).attr('data-id') + '|';
                        intIndex++;
                    });

                    $.ajax({
                        type: "GET",
                        url: "api/tasks/UpdateWFStepSort",
                        data: { Workflow_ID: '<%=Convert.ToString(ViewState["fid"])%>', Sort_List: strIDs },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: UpdateStepSortPassed,
                        failure: LoadValuesFailed
                    });
                }
            });
        }

        function ArrangeFieldCatSort() {
            $("#<%=gvStepFieldCats.ClientID%> tbody").attr('id', 'table_field_cats');
            $("#table_field_cats").sortable({
                axis: "y",
                handle: "i",
                stop: function (event, ui) {
                    ui.item.children("i").triggerHandler("focusout");
                    var strIDs = "";
                    $(".field_cat_row").each(function () {
                        strIDs = strIDs + $(this).attr('data-id') + '|';
                    });

                    $.ajax({
                        type: "GET",
                        url: "api/tasks/UpdateWFStepFieldCatSort",
                        data: { Workflow_Step_ID: $("#<%=hndWFStepID.ClientID%>").val(), Sort_List: strIDs },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: UpdateStepSortPassed,
                        failure: LoadValuesFailed
                    });
                }
            });
        }

        function ArrangeFieldSort() {
            $("#<%=gvStepFields.ClientID%> tbody").attr('id', 'table_fields');
            $("#table_fields").sortable({
                axis: "y",
                handle: "i",
                stop: function (event, ui) {
                    ui.item.children("i").triggerHandler("focusout");
                    var strIDs = "";
                    $(".field_row").each(function () {
                        strIDs = strIDs + $(this).attr('data-id') + '|';
                    });

                    $.ajax({
                        type: "GET",
                        url: "api/tasks/UpdateWFStepFieldSort",
                        data: { Workflow_Step_ID: $("#<%=hndWFStepID.ClientID%>").val(), Sort_List: strIDs },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: UpdateStepSortPassed,
                        failure: LoadValuesFailed
                    });
                }
            });
        }

        function ArrangeFieldConditionSort() {
            $("#<%=gvStepConditions.ClientID%> tbody").attr('id', 'table_field_cons');
            $("#table_field_cons").sortable({
                axis: "y",
                handle: "i",
                stop: function (event, ui) {
                    ui.item.children("i").triggerHandler("focusout");
                    var strIDs = "";
                    $(".field_con_row").each(function () {
                        strIDs = strIDs + $(this).attr('data-id') + '|';
                    });

                    $.ajax({
                        type: "GET",
                        url: "api/tasks/UpdateWFStepConditionSort",
                        data: { Workflow_Step_ID: $("#<%=hndWFStepID.ClientID%>").val(), Sort_List: strIDs },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: UpdateStepSortPassed,
                        failure: LoadValuesFailed
                    });
                }
            });
        }

        function UpdateStepSortPassed(result) { }

        function LoadStep() {
            document.getElementById('<%=cmdLoadStep.ClientID%>').click();
        }

        function ClearControls() {
            clearTextBox(['<%=txtFieldName.ClientID%>', '<%=txtSelectionText.ClientID%>', '<%=txtDefaultText.ClientID%>', '<%=txtDefaultTextMemo.ClientID%>', '<%=txtFieldMaxLength.ClientID%>', '<%=txtFieldHelpText.ClientID%>', '<%=txtCustomData.ClientID%>']);
            clearDropDown(['<%=cboFieldType.ClientID%>', '<%=cboValidateField.ClientID%>', '<%=cboCopyDataFrom.ClientID%>', '<%=cboFieldSize.ClientID%>', '<%=cboFieldCategory.ClientID%>', '<%=cboMasterTable.ClientID%>', '<%=cboMasterTableParamType.ClientID%>', '<%=cboMasterTableParam2Type.ClientID%>', '<%=cboAccessLevel.ClientID%>', '<%=cboDocCat.ClientID%>']);
            clearCheckBox(['<%=chkFieldRequired.ClientID%>', '<%=chkCopyEF1.ClientID%>', '<%=chkCopyEF2.ClientID%>', '<%=chkDisableControl.ClientID%>', '<%=chkTypable.ClientID%>', '<%=chkAllowMltUpload.ClientID%>']);
            onCheckBox('#<%=chkShowInHistory.ClientID%>');
            ShowHideSections(false);
            return false;
        }

        function ResetControls() {
            if ($("#<%=hndField_ID.ClientID%>").val().trim() == '0') {
                ClearControls();
            }
            else {
                LoadValues();
            }
            return false;
        }

        function LoadValues() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetWorklFlowField",
                data: { Workflow_Step_Field_ID: $("#<%=hndField_ID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassed(result) {
            ClearControls();

            var resultObject = result;
            $('#<%=txtFieldName.ClientID%>').val(resultObject[0].Field_Name);
            $('#<%=cboFieldType.ClientID%>').val(resultObject[0].Field_Type);
            $('#<%=cboFieldSize.ClientID%>').val(resultObject[0].Field_Size);
            $('#<%=txtSelectionText.ClientID%>').val(resultObject[0].Selection_Texts);
            if (resultObject[0].Field_Type.trim() == "3") {
                $('#<%=txtDefaultTextMemo.ClientID%>').val(resultObject[0].Default_Texts);
            }
            else {
                $('#<%=txtDefaultText.ClientID%>').val(resultObject[0].Default_Texts);
            }
            $('#<%=cboValidateField.ClientID%>').val(resultObject[0].Validate_With_Field_ID);
            $('#<%=cboFieldCategory.ClientID%>').val(resultObject[0].Workflow_Step_Field_Cat_ID);
            $('#<%=txtFieldHelpText.ClientID%>').val(resultObject[0].Help_Text);
            $('#<%=txtCustomData.ClientID%>').val(resultObject[0].Custom_Data);
            if (resultObject[0].IsRequired == true) {
                onCheckBox('#<%=chkFieldRequired.ClientID%>');
            }
            if (resultObject[0].Show_In_History == false) {
                offCheckBox('#<%=chkShowInHistory.ClientID%>');
            }
            if (resultObject[0].Copy_To_EF1 == true) {
                onCheckBox('#<%=chkCopyEF1.ClientID%>');
            }
            if (resultObject[0].Copy_To_EF2 == true) {
                onCheckBox('#<%=chkCopyEF2.ClientID%>');
            }
            if (resultObject[0].isTypable == true) {
                onCheckBox('#<%=chkTypable.ClientID%>');
            }
            if (resultObject[0].Allow_Multiple_Uploads == true) {
                onCheckBox('#<%=chkAllowMltUpload.ClientID%>');
            }
            if (parseInt(resultObject[0].Max_Length) > 0) {
                $('#<%=txtFieldMaxLength.ClientID%>').val(resultObject[0].Max_Length);
            }
            $('#<%=cboCopyDataFrom.ClientID%>').val(resultObject[0].Copy_Data_From);
            $('#<%=cboMasterTable.ClientID%>').val(resultObject[0].Master_Table_ID);
            $('#<%=cboMasterTableParamType.ClientID%>').val(resultObject[0].Master_Table_Param_Type);
            $('#<%=cboMasterTableParam2Type.ClientID%>').val(resultObject[0].Master_Table_Param_2_Type);
            $('#<%=cboAccessLevel.ClientID%>').val(resultObject[0].Access_Level);
            $('#<%=cboDocCat.ClientID%>').val(resultObject[0].Default_Doc_Category);
            ShowHideSections(resultObject[0].Disable_Control);
        }

        function CheckCopyDisable(isDisabled) {
            if ($('#<%=cboCopyDataFrom.ClientID%>').val() == '0') {
                $('#divDisableControl').addClass('hide');
                offCheckBox('#<%=chkDisableControl.ClientID%>');
            }
            else {
                $('#divDisableControl').removeClass('hide');
                if (isDisabled == true) {
                    onCheckBox('#<%=chkDisableControl.ClientID%>');
                }
                else {
                    offCheckBox('#<%=chkDisableControl.ClientID%>');
                }
            }
        }

        function ShowHideSections(isControlDisabled) {
            if ($('#<%=cboFieldType.ClientID%>').val() == "0") {
                $('#helpContent').addClass('hide');
                $('#<%=txtFieldHelpText.ClientID%>').val('');
                $('#copyEF1').addClass('hide');
                offCheckBox('#<%=chkCopyEF1.ClientID%>');
                $('#copyEF2').addClass('hide');
                offCheckBox('#<%=chkCopyEF2.ClientID%>');
            }
            else {
                $('#helpContent').removeClass('hide');
                $('#copyEF1').removeClass('hide');
                $('#copyEF2').removeClass('hide');
            }

            if ($('#<%=cboFieldType.ClientID%>').val() == "0" || $('#<%=cboFieldType.ClientID%>').val() == "11") {
                $('#validateField').addClass('hide');
                $('#<%=cboValidateField.ClientID%>').val('0');
                $('#copyData').addClass('hide');
                $('#<%=cboCopyDataFrom.ClientID%>').val('0');
            }
            else {
                $('#validateField').removeClass('hide');
                $('#copyData').removeClass('hide');
            }

            if ($('#<%=cboFieldType.ClientID%>').val() == "0" || $('#<%=cboFieldType.ClientID%>').val() == "13") {
                $('#requiredField').addClass('hide');
                offCheckBox('#<%=chkFieldRequired.ClientID%>');
            }
            else {
                $('#requiredField').removeClass('hide');
            }

            if ($('#<%=cboFieldType.ClientID%>').val() == "10") {
                $('#masterTable').removeClass('hide');
                $('#masterTableParamType').removeClass('hide');
                $('#masterTableParam2Type').removeClass('hide');
            }
            else {
                $('#masterTable').addClass('hide');
                $('#masterTableParamType').addClass('hide');
                $('#masterTableParam2Type').addClass('hide');
            }

            if ($('#<%=cboFieldType.ClientID%>').val() == "5") {
                $('#selection_text').removeClass('hide');
            }
            else {
                $('#selection_text').addClass('hide');
            }

            if ($('#<%=cboFieldType.ClientID%>').val() == "2") {
                $('#default_text').removeClass('hide');
                $('#default_text_memo').addClass('hide');
            }
            else if ($('#<%=cboFieldType.ClientID%>').val() == "3") {
                $('#default_text').addClass('hide');
                $('#default_text_memo').removeClass('hide');
            }
            else {
                $('#default_text').addClass('hide');
                $('#default_text_memo').addClass('hide');
            }

            if ($('#<%=cboFieldType.ClientID%>').val() == "2" || $('#<%=cboFieldType.ClientID%>').val() == "3") {
                $('#max_length').removeClass('hide');
            }
            else {
                $('#max_length').addClass('hide');
            }

            if ($('#<%=cboFieldType.ClientID%>').val() == "5" || $('#<%=cboFieldType.ClientID%>').val() == "7" || $('#<%=cboFieldType.ClientID%>').val() == "12" || $('#<%=cboFieldType.ClientID%>').val() == "10") {
                $('#divTypable').removeClass('hide');
            }
            else {
                $('#divTypable').addClass('hide');
                offCheckBox('#<%=chkTypable.ClientID%>');
            }

            if ($('#<%=cboFieldType.ClientID%>').val() == "11") {
                $('#accessLevel').removeClass('hide');
                $('#defaultDocCat').removeClass('hide');
                $('#divMultipleFiles').removeClass('hide');
            }
            else {
                $('#accessLevel').addClass('hide');
                $('#<%=cboAccessLevel.ClientID%>').val('3');
                $('#defaultDocCat').addClass('hide');
                $('#<%=cboDocCat.ClientID%>').val('General Documents');
                $('#divMultipleFiles').addClass('hide');
                offCheckBox('#<%=chkAllowMltUpload.ClientID%>');
            }
            CheckCopyDisable(isControlDisabled);
        }

        function CheckScreenLinks() {
            if ($("#<%=chkStepLinks.ClientID%>").prop("checked") == false) {
                $("#<%=divLinkedScreens.ClientID%>").addClass('hide');
                clearCheckListBox(['<%=chkCustomScreens.ClientID%>']);
            }
            else {
                $("#<%=divLinkedScreens.ClientID%>").removeClass('hide');
            }
        }

        //Step Field Cats
        function ClearStepFeldCats() {
            clearTextBox(['<%=txtStepFieldCat.ClientID%>']);
            return false;
        }

        function ResetStepFeldCats() {
            if ($("#<%=hndStepFieldCat_ID.ClientID%>").val().trim() == '0') {
                ClearStepFeldCats();
            }
            else {
                LoadStepFieldCatValues();
            }
            return false;
        }

        function LoadStepFieldCatValues() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetWorklFlowStepFieldCat",
                data: { Workflow_Step_Field_Cat_ID: $("#<%=hndStepFieldCat_ID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadStepFieldCatValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadStepFieldCatValauesPassed(result) {
            ClearStepFeldCats();
            var resultObject = result;
            $('#<%=txtStepFieldCat.ClientID%>').val(resultObject[0].Workflow_Step_Field_Cat);
        }

        //Conditions
        function ClearConControls(reload_codt_drps) {
            clearDropDown(['<%=cboConditionOn.ClientID%>', '<%=cboOperator.ClientID%>', '<%=cboMoveToIfTrue.ClientID%>']);
            SelectOperators('<%=cboConditionOn.ClientID%>', '<%=cboOperator.ClientID%>');
            DisplayConditionText('<%=cboConditionOn.ClientID%>', '<%=txtEvaluateWith.ClientID%>', 'cboCondtTemp', 'txtCondtDateTemp', '', reload_codt_drps);
            return false;
        }

        function ResetConControls() {
            if ($("#<%=hndFieldCond_ID.ClientID%>").val().trim() == '0') {
                ClearConControls(true);
            }
            else {
                LoadConValues();
            }
            return false;
        }

        function LoadConValues() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetWorklFlowCondition",
                data: { Condition_ID: $("#<%=hndFieldCond_ID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadConValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadConValauesPassed(result) {
            ClearConControls(false);
            var resultObject = result;
            $("#<%=cboConditionOn.ClientID%> > option").each(function () {
                if ($(this).val().split('_')[0].trim() == resultObject[0].Workflow_Step_Field_ID) {
                    $('#<%=cboConditionOn.ClientID%>').val($(this).val());
                }
            });
            SelectOperators('<%=cboConditionOn.ClientID%>', '<%=cboOperator.ClientID%>');
            $('#<%=cboOperator.ClientID%>').val(resultObject[0].Operator_ID);
            DisplayConditionText('<%=cboConditionOn.ClientID%>', '<%=txtEvaluateWith.ClientID%>', 'cboCondtTemp', 'txtCondtDateTemp', resultObject[0].Evaluate_With, true);
            $('#<%=cboMoveToIfTrue.ClientID%>').val(resultObject[0].Move_To_Step_ID);
        }

        //Formulas
        function ClearFormulaControls() {
            clearDropDown(['<%=cboT1Date1.ClientID%>', '<%=cboT1Date2.ClientID%>', '<%=cboT1Result.ClientID%>', '<%=cboT1Time1.ClientID%>', '<%=cboT1Time2.ClientID%>', '<%=cboT2Date1.ClientID%>', '<%=cboT2Days.ClientID%>', '<%=cboT2Result.ClientID%>', '<%=cboT3Number1.ClientID%>', '<%=cboT3Number2.ClientID%>', '<%=cboT3Operator.ClientID%>', '<%=cboT3Result.ClientID%>']);
            if ($("#<%=hndFormula_Type.ClientID%>").val().trim() == '1') {
                $('#FormulaType1').removeClass("hide");
                $('#FormulaType2').addClass("hide");
                $('#FormulaType3').addClass("hide");
            }
            else if ($("#<%=hndFormula_Type.ClientID%>").val().trim() == '2') {
                $('#FormulaType1').addClass("hide");
                $('#FormulaType2').removeClass("hide");
                $('#FormulaType3').addClass("hide");
            }
            else {
                $('#FormulaType1').addClass("hide");
                $('#FormulaType2').addClass("hide");
                $('#FormulaType3').removeClass("hide");
            }
            return false;
        }

        function ResetFormulaControls() {
            if ($("#<%=hndFormula_ID.ClientID%>").val().trim() == '0') {
                ClearFormulaControls();
            }
            else {
                LoadFormulaValues();
            }
            return false;
        }

        function LoadFormulaValues() {
            $.ajax({
                type: "GET",
                url: "api/tasks/GetWorklFlowFormula",
                data: { Formula_ID: $("#<%=hndFormula_ID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadFormulaValuesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadFormulaValuesPassed(result) {
            var resultObject = result;

            $('#<%=hndFormula_Type.ClientID%>').val(resultObject[0].Formula_Type);
            ClearFormulaControls();

            $('#<%=cboT1Date1.ClientID%>').val(resultObject[0].Date_1_Field);
            $('#<%=cboT1Date2.ClientID%>').val(resultObject[0].Date_2_Field);
            $('#<%=cboT1Result.ClientID%>').val(resultObject[0].Result_Field);
            $('#<%=cboT1Time1.ClientID%>').val(resultObject[0].Time_1_Field);
            $('#<%=cboT1Time2.ClientID%>').val(resultObject[0].Time_2_Field);
            $('#<%=cboT2Date1.ClientID%>').val(resultObject[0].Date_1_Field);
            $('#<%=cboT2Days.ClientID%>').val(resultObject[0].Number_1_Field);
            $('#<%=cboT2Result.ClientID%>').val(resultObject[0].Result_Field);
            $('#<%=cboT3Number1.ClientID%>').val(resultObject[0].Number_1_Field);
            $('#<%=cboT3Number2.ClientID%>').val(resultObject[0].Number_2_Field);
            $('#<%=cboT3Operator.ClientID%>').val(resultObject[0].F_Operator);
            $('#<%=cboT3Result.ClientID%>').val(resultObject[0].Result_Field);
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
    </script>
</asp:Content>
