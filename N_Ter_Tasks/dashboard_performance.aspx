<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin.master" AutoEventWireup="true" CodeBehind="dashboard_performance.aspx.cs" Inherits="N_Ter_Tasks.dashboard_performance" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Dashboard
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Dashboard
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div class="row mb10">
        <div class="col-sm-12">
            <div class="row mb10">
                <div class="col-md-8">
                    <label>Workflow</label>
                    <div class="input-group">
                        <span class="input-group-addon" style="border: none; background: #fff; background: rgba(0,0,0,.05);"><i class="fa fa-archive"></i></span>
                        <asp:DropDownList ID="cboWorkflows" runat="server" CssClass="form-control no-padding-hr" Style="border: none; background: #fff; background: rgba(0,0,0,.05);"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>From</label>
                    <div class="input-group">
                        <span class="input-group-addon" style="border: none; background: #fff; background: rgba(0,0,0,.05);"><i class="fa fa-calendar"></i></span>
                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control no-padding-hr dtPicker" Style="border: none; background: #fff; background: rgba(0,0,0,.05);"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>To</label>
                    <div class="input-group">
                        <span class="input-group-addon" style="border: none; background: #fff; background: rgba(0,0,0,.05);"><i class="fa fa-calendar"></i></span>
                        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control no-padding-hr dtPicker" Style="border: none; background: #fff; background: rgba(0,0,0,.05);"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <button id="cmdRefresh" runat="server" class="btn btn-labeled btn-primary" onclick="return Refresh();"><span class="btn-label icon fa fa-refresh"></span>Refresh Content</button>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-4">
            <div class="stat-panel">
                <div class="stat-cell bg-danger valign-middle">
                    <span class="text-xlg"><span class="text-lg text-slim"></span><strong id="lblTotalStart" runat="server">147</strong></span><br>
                    <span class="text-bg">Total Tasks Started</span><br>
                </div>
            </div>
        </div>
        <div class="col-sm-12 col-md-4">
            <div class="stat-panel">
                <div class="stat-cell bg-warning valign-middle">
                    <span class="text-xlg"><span class="text-lg text-slim"></span><strong id="lblTotalCompleted" runat="server">147</strong></span><br>
                    <span class="text-bg">Total Tasks Completed</span><br>
                </div>
            </div>
        </div>
        <div class="col-sm-12 col-md-4">
            <div class="stat-panel">
                <div class="stat-cell bg-info valign-middle">
                    <span class="text-xlg"><span class="text-lg text-slim"></span><strong id="lblAvgPerDay" runat="server">147</strong></span><br>
                    <span class="text-bg">Average Tasks per Day</span><br>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class=" col-md-12">
            <div class="stat-panel">
                <div class="stat-row">
                    <div class="stat-cell col-sm-3 padding-sm-hr bordered no-border-r valign-top">
                        <h4 class="padding-sm no-padding-t padding-xs-hr"><i class="fa fa-tasks text-primary"></i>&nbsp;&nbsp;Total Tasks</h4>
                        <ul class="list-group no-margin">
                            <li class="list-group-item no-border-hr padding-xs-hr no-bg no-border-radius">Started <span id="lblTotalStart2" runat="server" class="label label-danger pull-right">34</span>
                            </li>
                            <li class="list-group-item no-border-hr padding-xs-hr no-bg">Completed <span id="lblTotalCompleted2" runat="server" class="label label-warning pull-right">128</span>
                            </li>
                            <li class="list-group-item no-border-hr no-border-b padding-xs-hr no-bg">Inactive <span id="lblTotalInactive" runat="server" class="label label-dark-gray pull-right">12</span>
                            </li>
                            <li class="list-group-item no-border-hr no-border-b padding-xs-hr no-bg">Unclaimed <span id="lblTotalUnclaimed" runat="server" class="label label-pa-purple pull-right">12</span>
                            </li>
                            <li class="list-group-item no-border-hr no-border-b padding-xs-hr no-bg">Claimed by you <span id="lblTotalClaimed" runat="server" class="label label-success pull-right">12</span>
                            </li>                            
                        </ul>
                    </div>
                    <div class="stat-cell col-sm-9 bg-primary padding-sm valign-middle">
                        <div>
                            <table style="display: inline-table">
                                <tr>
                                    <td>
                                        <div style="height: 20px; width: 20px; background-color: #aed7ff"></div>
                                    </td>
                                    <td style="padding-left: 10px; padding-right: 20px">Started</td>
                                    <td>
                                        <div style="height: 20px; width: 20px; background-color: #ffa7a7"></div>
                                    </td>
                                    <td style="padding-left: 10px; padding-right: 20px">Completed</td>
                                </tr>
                            </table>
                        </div>
                        <div id="taskData" class="graph" style="height: 300px;"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="stat-panel">
                <div class="stat-row">
                    <div class="stat-cell padding-sm-hr bordered no-border-r valign-top stat_gb_drk" style="width: 40px">
                        <h4 class="padding-sm no-padding-t padding-xs-hr chart_header_90"><i class="fa fa-check text-primary"></i>&nbsp;&nbsp;Total Per User (Created)</h4>
                    </div>
                    <div class="stat-cell padding-sm valign-middle bordered">
                        <div class="graph-container">
							<div id="userData" class="graph" style="height: 530px;"></div>
						</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="stat-panel">
                <div class="stat-row">
                    <div class="stat-cell padding-sm-hr bordered no-border-r valign-top stat_gb_drk" style="width: 40px">
                        <h4 class="padding-sm no-padding-t padding-xs-hr chart_header_90"><i class="fa fa-check text-primary"></i>&nbsp;&nbsp;Total Per User (Involvements)</h4>
                    </div>
                    <div class="stat-cell padding-sm valign-middle bordered">
                        <div class="graph-container">
							<div id="userData2" class="graph" style="height: 530px;"></div>
						</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="stat-panel">
                <div class="stat-row">
                    <div class="stat-cell padding-sm-hr bordered no-border-r valign-top stat_gb_drk" style="width: 40px">
                        <h4 class="padding-sm no-padding-t padding-xs-hr chart_header_90"><i class="fa fa-check text-primary"></i>&nbsp;&nbsp;Total Per
                            <asp:Literal ID="ltrEL2" runat="server"></asp:Literal> (Created)</h4>
                    </div>
                    <div class="stat-cell padding-sm valign-middle bordered">
                        <div class="graph-container">
							<div id="clientData" class="graph" style="height: 530px;"></div>
						</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
    <script type="text/javascript">
        var day_data = <%=MainChartScript%>
        var user_data = <%=UserChartScript%>
        var user_data2 = <%=UserChart2Script%>
        var client_data = <%=ClientChartScript%>

        init.push(function () {
            DrawCharts();
        });

        function DrawCharts() {
            $("#taskData").empty();
            Morris.Line({
                element: 'taskData',
                data: day_data,
                xkey: 't_date',
                ykeys: ['started_count_h', 'completed_count_h'],
                labels: ['Started', 'Completed'],
                lineColors: ['#aed7ff', '#ffa7a7'],
                lineWidth: 2,
                pointSize: 3,
                behaveLikeLine: true,
                gridLineColor: 'rgba(255,255,255,.5)',
                resize: true,
                gridTextColor: '#fff',
                xLabels: "Date",
            });

            $("#userData").empty();
            Morris.Bar({
                element: 'userData',
                data: user_data,
                xkey: 'user',
                ykeys: ['no_tasks'],
                labels: ['# Tasks'],
                barColors: ['#f4b04f'],
                barRatio: 0.4,
                xLabelAngle: 90,
                hideHover: 'auto',
                gridLineColor: '#cfcfcf',
                resize: true
            });

            $("#userData2").empty();
            Morris.Bar({
                element: 'userData2',
                data: user_data2,
                xkey: 'user',
                ykeys: ['no_tasks'],
                labels: ['# Tasks'],
                barColors: ['#e66454'],
                barRatio: 0.4,
                xLabelAngle: 90,
                hideHover: 'auto',
                gridLineColor: '#cfcfcf',
                resize: true
            });

            $("#clientData").empty();
            Morris.Bar({
                element: 'clientData',
                data: client_data,
                xkey: 'client',
                ykeys: ['no_tasks'],
                labels: ['# Tasks'],
                barColors: ['#5ebd5e'],
                barRatio: 0.4,
                xLabelAngle: 90,
                hideHover: 'auto',
                gridLineColor: '#cfcfcf',
                resize: true
            });
        }

        function Refresh() {
            onPleaseWait();
            $.ajax({
                type: "GET",
                url: "api/tasks/GetPerformanceDashboard",
                data: { Workflow_ID: $("#<%=cboWorkflows.ClientID%>").val(), FromDate: $("#<%=txtFrom.ClientID%>").val(), ToDate: $("#<%=txtTo.ClientID%>").val() },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadValauesPassed,
                failure: LoadValuesFailed
            });

            return false;
        }

        function LoadValauesPassed(result) {
            var resultObject = result;
            day_data = resultObject[0].OMainChart;
            user_data = resultObject[0].OUserChart;
            user_data2 = resultObject[0].OUserChart2;
            client_data = resultObject[0].OClientChart;
            $('#<%=lblTotalStart.ClientID%>').html(resultObject[0].TotalStarted);
            $('#<%=lblTotalCompleted.ClientID%>').html(resultObject[0].TotalCompletd);
            $('#<%=lblAvgPerDay.ClientID%>').html(resultObject[0].AveragePerDay);
            $('#<%=lblTotalStart2.ClientID%>').html(resultObject[0].TotalStarted);
            $('#<%=lblTotalCompleted2.ClientID%>').html(resultObject[0].TotalCompletd);
            $('#<%=lblTotalInactive.ClientID%>').html(resultObject[0].TotalInactive);
            $('#<%=lblTotalUnclaimed.ClientID%>').html(resultObject[0].TotalUnclaimed);
            $('#<%=lblTotalClaimed.ClientID%>').html(resultObject[0].TotalClaimedByYou);
            DrawCharts();
            offPleaseWait();
        }
    </script>
</asp:Content>