<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="cuz_reports.ascx.cs" Inherits="N_Ter_Tasks.controls_customizable.cuz_reports" %>

<%@ Register Assembly="DevExpress.XtraReports.v22.1.Web.WebForms, Version=22.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<div id="divFilter" runat="server"></div>
<div id="divReport" runat="server" class="panel panel-info form-horizontal">
    <div class="panel-body">
        <div class="panel-title-nter">Report</div>
        <div class="border-t panel-info" style="height: 20px"></div>
        <div class="row">
            <div class="col-sm-12">
                <div id="divNoData" runat="server" class="col-md-12 no-padding-hr">
                    <div class="alert alert-success mb">
                        <strong>Information !</strong> There's no Data available.
                    </div>
                </div>
                <dx:ASPxDocumentViewer ID="docViewMain" runat="server" Theme="Metropolis">
                    <SettingsReportViewer EnableReportMargins="true" />
                    <StylesToolbar>
                        <ToolbarMenuStyle BackColor="White"></ToolbarMenuStyle>
                    </StylesToolbar>
                    <StylesSplitter>
                        <Pane BackColor="White">
                        </Pane>
                    </StylesSplitter>
                </dx:ASPxDocumentViewer>
            </div>
        </div>
    </div>
</div>
