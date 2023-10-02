<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="cuz_dashboard.ascx.cs" Inherits="N_Ter_Tasks.controls_customizable.cuz_dashboard" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<div class="row mb10">
    <div class="col-sm-12">
        <div class="row mb10">
            <div class="col-md-6">
                <label>Workflow Category</label>
                <div class="input-group">
                    <span class="input-group-addon" style="border: none; background: #fff; background: rgba(0,0,0,.05);"><i class="fa fa-sitemap"></i></span>
                    <asp:DropDownList ID="cboWF_Category" runat="server" CssClass="form-control no-padding-hr" Style="border: none; background: #fff; background: rgba(0,0,0,.05);"></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6">
                <label>Workflow</label>
                <div class="input-group">
                    <asp:HiddenField ID="hndWorkflowID" runat="server" />
                    <span class="input-group-addon" style="border: none; background: #fff; background: rgba(0,0,0,.05);"><i class="fa fa-archive"></i></span>
                    <select id="workflowList" class="form-control no-padding-hr" style="border: none; background: #fff; background: rgba(0,0,0,.05);" onchange="SaveWorkflowLine();"></select>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-sm-12 col-md-4">
        <div class="stat-panel">
            <div class="stat-cell bg-danger valign-middle">
                <span class="text-xlg"><span class="text-lg text-slim"></span><strong id="lblTotal">0</strong></span><br>
                <span class="text-bg">Tasks</span><br>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-4">
        <div class="stat-panel">
            <div class="stat-cell bg-warning valign-middle">
                <span class="text-xlg"><span class="text-lg text-slim"></span><strong id="lblCustom1">0</strong></span><br>
                <span class="text-bg">Custom Indicator 1</span><br>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-4">
        <div class="stat-panel">
            <div class="stat-cell bg-info valign-middle">
                <span class="text-xlg"><span class="text-lg text-slim"></span><strong id="lblCustom2">0</strong></span><br>
                <span class="text-bg">Custom Indicator 2</span><br>
            </div>
        </div>
    </div>
</div>
<div class="row">
    <div id="divMilestones" class="col-md-12">
        <div class="stat-panel">
            <div class="stat-row">
                <div class="stat-cell padding-sm-hr bordered no-border-r valign-top stat_gb_drk" style="width: 40px">
                    <h4 class="padding-sm no-padding-t padding-xs-hr chart_header_90"><i class="fa fa-tasks text-primary"></i>&nbsp;&nbsp;Milestones</h4>
                    <div style="position: absolute; bottom: 8px; left: 8px">
                        <button id="cmdShowReportMain" runat="server" class="btn btn-labeled btn-primary btn-xs" title="View as Report" onserverclick="cmdShowReportMain_Click"><span class="fa fa-file-text"></span></button>
                    </div>
                </div>
                <div class="stat-cell valign-middle bordered" style="padding: 15px 15px 0 15px">
                    <div id="divChartsMain" class="row">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divSteps" class="col-md-12">
        <div class="stat-panel">
            <div class="stat-row">
                <div class="stat-cell padding-sm-hr bordered no-border-r valign-top stat_gb_drk" style="width: 40px">
                    <h4 class="padding-sm no-padding-t padding-xs-hr chart_header_90"><i class="fa fa-tasks text-primary"></i>&nbsp;&nbsp;Steps</h4>
                    <div style="position: absolute; bottom: 8px; left: 8px">
                        <button id="cmdShowReport" runat="server" class="btn btn-labeled btn-primary btn-xs" title="View as Report" onserverclick="cmdShowReport_Click"><span class="fa fa-file-text"></span></button>
                    </div>
                </div>
                <div class="stat-cell valign-middle bordered" style="padding: 15px 15px 0 15px">
                    <div id="divCharts" class="row">
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div id="divNoTasks" class="col-md-12 hide no-padding-hr">
    <div class="alert alert-success">
        <strong>Information !</strong> There are no Tasks available.
    </div>
</div>

<script type="text/javascript">
    init.push(function () {
        LoadWorkflows();
        setInterval(DiaplayMIS, <%=RefreshFreq%>);
    });

    function LoadWorkflows() {
        $("#lblTotal").html("0");
        $("#divCharts").html("");
        $("#divSteps").addClass("hide");
        $("#divChartsMain").html("");
        $("#divMilestones").addClass("hide");
        $("#divNoTasks").addClass("hide");

        $.ajax({
            type: "GET",
            url: "api/tasks/GetAllRelatedWorkflows",
            data: { Workflow_Category_ID: $('#<%=cboWF_Category.ClientID%>').val() },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: LoadWorkflowsPassed,
            failure: LoadFailed
        });
    }

    function LoadWorkflowsPassed(result) {
        var resultObject = result;

        $('#workflowList').empty();
        for (var i = 0; i < resultObject.length; i++) {
            $('#workflowList').append(
                $('<option>', {
                    value: resultObject[i].Walkflow_ID.trim() + "|" + resultObject[i].Schedule_ID.trim(),
                    text: resultObject[i].Workflow_Name.trim()
                }, '<option/>'))
        }
        SaveWorkflowLine();
    }

    function SaveWorkflowLine() {
        $('#<%=hndWorkflowID.ClientID%>').val($('#workflowList').val());
        DiaplayMIS();
    }

    function LoadFailed(result) {

    }

    function DiaplayMIS() {
        $("#lblTotal").html("0");
        $("#divCharts").html("");
        $("#divSteps").addClass("hide");
        $("#divChartsMain").html("");
        $("#divMilestones").addClass("hide");
        $("#divNoTasks").addClass("hide");

        $.ajax({
            type: "GET",
            url: "api/custom/GetDashboardStats",
            data: { Workflow_ID: $('#workflowList').val().split('|')[0] },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: StatsPassed,
            failure: StatsFailed
        });
    }

    function StatsFailed(result) {
    }

    function StatsPassed(result) {
        if (JSON.stringify(result) != "[]" && JSON.stringify(result) != "") {
            var resultObject = result;

            $("#lblTotal").html(resultObject[0].Total_Tasks);

            var resultObjectMain = resultObject[0].Milestone_Data;
            if (resultObjectMain.length > 0) {
                var chartsMain = "";
                for (var i = 0; i < resultObjectMain.length; i++) {
                    chartsMain = chartsMain + "<div class='col-lg-3 col-md-4 col-sm-6' >\n" +
                        "<div class='panel mb15'>\n" +
                        "<div class='panel-heading'>\n" +
                        resultObjectMain[i].Step_Status +
                        "</div>\n" +
                        "<div class='panel-body text-center'>\n" +
                        "<div class='pie-chart pie-chart-" + resultObjectMain[i].Band_Col + "' data-percent='" + resultObjectMain[i].Tasks_Percentage + "'>\n" +
                        "<div class='pie-chart-label'></div>\n" +
                        "</div>\n" +
                        "</div>\n" +
                        "<div class='panel-footer'>\n" +
                        resultObjectMain[i].Number_Of_Tasks + " Tasks\n" +
                        "</div>\n" +
                        "</div>\n" +
                        "</div>\n";
                }
                $("#divChartsMain").html(chartsMain);
                $("#divMilestones").removeClass("hide");
            }

            var resultObjectSub = resultObject[0].Step_Data;
            if (resultObjectSub.length > 0) {
                var charts = "";
                for (var i = 0; i < resultObjectSub.length; i++) {
                    charts = charts + "<div class='col-lg-3 col-md-4 col-sm-6' >\n" +
                        "<div class='panel mb15'>\n" +
                        "<div class='panel-heading'>\n" +
                        "<a href='" + resultObjectSub[i].WF_Step_URL + "'>" + resultObjectSub[i].Step_Status + "</a>" +
                        "</div>\n" +
                        "<div class='panel-body text-center'>\n" +
                        "<div class='pie-chart pie-chart-" + resultObjectSub[i].Band_Col + "' data-percent='" + resultObjectSub[i].Tasks_Percentage + "'>\n" +
                        "<div class='pie-chart-label'></div>\n" +
                        "</div>\n" +
                        "</div>\n" +
                        "<div class='panel-footer'>\n" +
                        resultObjectSub[i].Number_Of_Tasks + " Tasks\n" +
                        "</div>\n" +
                        "</div>\n" +
                        "</div>\n";
                }
                $("#divCharts").html(charts);
                $("#divSteps").removeClass("hide");
            }

            if (resultObjectMain.length == 0 && resultObjectSub.length == 0) {
                $("#divNoTasks").removeClass("hide");
            }
        }
        else {
            $("#divNoTasks").removeClass("hide");
        }

        $('.pie-chart-r').easyPieChart({
            animate: 2000,
            scaleColor: false,
            lineWidth: 7,
            easing: 'easeOutBounce',
            barColor: '#ff0202',
            onStep: function (from, to, percent) {
                $(this.el).find('.pie-chart-label').text(Math.round(percent) + '%');
            }
        });
        $('.pie-chart-g').easyPieChart({
            animate: 2000,
            scaleColor: false,
            lineWidth: 7,
            easing: 'easeOutBounce',
            barColor: "#138c22",
            onStep: function (from, to, percent) {
                $(this.el).find('.pie-chart-label').text(Math.round(percent) + '%');
            }
        });
        $('.pie-chart-b').easyPieChart({
            animate: 2000,
            scaleColor: false,
            lineWidth: 7,
            easing: 'easeOutBounce',
            barColor: "#168cb9",
            onStep: function (from, to, percent) {
                $(this.el).find('.pie-chart-label').text(Math.round(percent) + '%');
            }
        });
    }
</script>