<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin_grid_wf.master" AutoEventWireup="true" CodeBehind="periodical_tasks.aspx.cs" Inherits="N_Ter_Tasks.periodical_tasks" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Periodical Tasks
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Periodical Tasks -
    <asp:Label ID="lblWorkflow" runat="server" Text="Label"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <asp:HiddenField ID="hndJobID" runat="server" />
    <div class="row" style="margin-top: -18px">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="padding-xs-vr">
                        <div class="btn-group">
                            <button class="btn btn-labeled btn-primary" data-toggle="dropdown"><span class="btn-label icon fa fa-plus"></span>New Periodical Task&nbsp;&nbsp;&nbsp;<i class="fa fa-caret-down"></i></button>
                            <ul class="dropdown-menu dropdown-menu-right">
                                <li><a href="#" onclick="return NewJob(1);">Daily Task</a></li>
                                <li><a href="#" onclick="return NewJob(2);">Weekly Task</a></li>
                                <li><a href="#" onclick="return NewJob(3);">Monthly Task</a></li>
                                <li><a href="#" onclick="return NewJob(4);">Yearly Task</a></li>
                            </ul>
                        </div>
                        <asp:HiddenField ID="hndRecType" runat="server" />
                        <asp:ModalPopupExtender ID="hndRecType_ModalPopupExtender" runat="server" Enabled="True" BehaviorID="mpuNewJob" PopupControlID="pnlData" TargetControlID="hndRecType" CancelControlID="cmdNo" BackgroundCssClass="at_modelpopup_background">
                        </asp:ModalPopupExtender>
                    </div>
                    <div class="table-responsive table-primary no-margin-b">
                        <asp:GridView ID="gvMain" runat="server" CssClass="table table-striped table-hover grid_table grid_jobs non_full_width_table" data-size="non_full_width_table" AutoGenerateColumns="False" OnRowDataBound="gvMain_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="Job_ID" />
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
                                        <button id='cmdDelete' type='submit' runat="server" class="btn btn-danger btn-xs" title="Delete"><i class="fa fa-trash-o button_icon"></i></button>
                                        <asp:ModalPopupExtender ID="cmdDelete_ModalPopupExtender" runat="server" Enabled="True" PopupControlID="pnlDelete" TargetControlID="cmdDelete" CancelControlID="cmdCancel" BackgroundCssClass="at_modelpopup_background">
                                        </asp:ModalPopupExtender>
                                    </ItemTemplate>
                                    <ItemStyle Width="22px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Job_Name" HeaderText="Job Name" />
                                <asp:BoundField DataField="Display_Name" HeaderText="EL2 Name" />
                                <asp:BoundField DataField="Rec_Type_SP" HeaderText="Job Type" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <asp:Panel ID="pnlData" runat="server" CssClass="at_modelpopup_container" Style="display: none">
                <div id="at_model_indent" class="at_modelpopup_indent">
                    <div id="at_model_inner_indent" class="panel panel-inverse at_modelpopup_inner_indent">
                        <div class="panel-heading">
                            <div class="at_modelpopup_add">
                                <asp:Button ID="cmdNo" runat="server" Text="&times;" class="btn btn-default" />
                            </div>
                            <h4 class="panel-title">Add/Edit Periodical Task <span id="Rec_Type"></span></h4>
                        </div>
                        <div id="at_model_content" class="at_modelpopup_content styled-bar">
                            <div class="panel-body">
                                <div class="form-group">
                                    <label>Job Name</label>
                                    <asp:TextBox ID="txtJobName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>Create a Task</label>
                                            <div class="input-group">
                                                <span class="input-group-addon">Every</span>
                                                <asp:TextBox ID="txtFrequendy" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                <span id="Rec_Type2" class="input-group-addon">Day(s)</span>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <label>Starting From</label>
                                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control dtPicker"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
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
                                <div class="form-group">
                                    <label># Days to Complete</label>
                                    <asp:TextBox ID="txtDaysToComplete" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
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
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script type="text/javascript">
        $(function () {
            $(window).resize(function () {
                ArrangePopups();
            });
        });

        init.push(function () {
            ArrangePopups();
            ArrangeGrids();
        });

        function ArrangePopups() {
            AdjustPopupSize(167, 800, 'at_model');
            AdjustPopupSize(80, 400, 'at_del_model');
        }

        function ArrangeGrids() {
            $('.grid_jobs').dataTable({
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

        function NewJob(recType) {
            $("#<%=hndJobID.ClientID%>").val('0');
            ClearControls();
            ActivateType(recType);
            $find('mpuNewJob').show();            
        }

        function ActivateType(recType) {
            $('#<%=hndRecType.ClientID%>').val(recType);
            switch (recType) {
                case 1:
                    $('#Rec_Type').html("- Daily");
                    $('#Rec_Type2').html("Day(s)");
                    break;
                case 2:
                    $('#Rec_Type').html("- Weekly");
                    $('#Rec_Type2').html("Week(s)");
                    break;
                case 3:
                    $('#Rec_Type').html("- Monthly");
                    $('#Rec_Type2').html("Month(s)");
                    break;
                case 4:
                    $('#Rec_Type').html("- Yearly");
                    $('#Rec_Type2').html("Year(s)");
                    break;
            }
        }

        function ClearControls() {
            clearTextBox(['<%=txtFrequendy.ClientID%>', '<%=txtDaysToComplete.ClientID%>', '<%=txtExtraField1.ClientID%>', '<%=txtExtraField2.ClientID%>', '<%=txtJobName.ClientID%>']);
            clearDateTextBox(['<%=txtStartDate.ClientID%>']);
            clearDropDown(['<%=cboEL2.ClientID%>', '<%=cboExtraField1.ClientID%>', '<%=cboExtraField2.ClientID%>']);
            $('#<%=txtExtraField1.ClientID%>').val($('#<%=cboExtraField1.ClientID%>').val());
            $('#<%=txtExtraField2.ClientID%>').val($('#<%=cboExtraField2.ClientID%>').val());
            return false;
        }

        function ResetControls() {
            if ($("#<%=hndJobID.ClientID%>").val().trim() == '0') {
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
                url: "api/tasks/GetPeriodicTask",
                data: { Job_ID: $("#<%=hndJobID.ClientID%>")[0].value },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });
        }

        function LoadValauesPassed(result) {
            ClearControls();

            var resultObject = result;

            ActivateType(Number(resultObject[0].Recurrence_Type));

            $('#<%=txtJobName.ClientID%>').val(resultObject[0].Job_Name);
            $('#<%=cboEL2.ClientID%>').val(resultObject[0].Entity_L2_ID);
            $('#<%=txtExtraField1.ClientID%>').val(resultObject[0].Extra_Field_1_Value);
            $('#<%=txtExtraField2.ClientID%>').val(resultObject[0].Extra_Field_2_Value);
            $('#<%=cboExtraField1.ClientID%>').val(resultObject[0].Extra_Field_1_Value);
            $('#<%=cboExtraField2.ClientID%>').val(resultObject[0].Extra_Field_2_Value);
            $('#<%=txtDaysToComplete.ClientID%>').val(resultObject[0].Days_To_Complete);
            $('#<%=txtFrequendy.ClientID%>').val(resultObject[0].Frequency);
            $('#<%=txtStartDate.ClientID%>').val(resultObject[0].Start_Date);
        }
    </script>
</asp:Content>