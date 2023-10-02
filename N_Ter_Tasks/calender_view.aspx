<%@ Page Title="" Language="C#" MasterPageFile="~/n_ter_base_loggedin.master" AutoEventWireup="true" CodeBehind="calender_view.aspx.cs" Inherits="N_Ter_Tasks.calender_view" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v22.1, Version=22.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>

<%@ Register Assembly="DevExpress.XtraScheduler.v22.1.Core, Version=22.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v22.1.Core.Desktop, Version=22.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contTitle" runat="Server">
    N-Ter-Tasks > Calendars
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contCSS" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contHeader" runat="Server">
    Calendars
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contBody" runat="Server">
    <div id="divNoCals" runat="server" class="row">
        <div class="col-md-12">
            <div class="alert alert-success">
                <strong>Information !</strong> There are no Calendars available for you.
            </div>
        </div>
    </div>
    <div id="divCalender" runat="server" style="margin: -18px -20px; min-height: 400px">
        <div style="padding: 18px 20px 0px 20px">
            <div class="row">
                <div class="col-md-12">
                    <div class="btn-group w100">
                        <button type="button" class="btn btn-primary dropdown-toggle text-left w100" data-toggle="dropdown">
                            Calendar :
                                    <asp:Literal ID="ltrSelectedCal" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-caret-down pull-right pt3"></i></button>
                        <ul class="dropdown-menu dropdown-menu-right w100">
                            <asp:Literal ID="ltrOtherCal" runat="server"></asp:Literal>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div style="margin: 18px 20px">
            <dxwschs:ASPxScheduler ID="scdCalender" runat="server" AppointmentDataSourceID="nterCalData" ClientIDMode="AutoID" Theme="Moderno" ActiveViewType="Month">
                <Storage>
                    <Appointments AutoRetrieveId="True">
                        <Mappings AllDay="AllDay" AppointmentId="UniqueID" Description="Description" End="EndDate" Label="Label" Location="Location" RecurrenceInfo="RecurrenceInfo" ReminderInfo="ReminderInfo" ResourceId="ResourceId" Start="StartDate" Status="Status" Subject="Subject" Type="Type" />
                        <CustomFieldMappings>
                            <dxwschs:ASPxAppointmentCustomFieldMapping Member="Calender_ID" Name="CalenderID" />
                            <dxwschs:ASPxAppointmentCustomFieldMapping Member="Event_ID" Name="EventID" />
                        </CustomFieldMappings>
                    </Appointments>
                </Storage>
                <Views>
                    <DayView ViewSelectorItemAdaptivePriority="2">
                        <TimeRulers>
                            <cc1:TimeRuler></cc1:TimeRuler>
                        </TimeRulers>
                        <AppointmentDisplayOptions ColumnPadding-Left="2" ColumnPadding-Right="4"></AppointmentDisplayOptions>
                    </DayView>
                    <WorkWeekView ViewSelectorItemAdaptivePriority="6">
                        <TimeRulers>
                            <cc1:TimeRuler></cc1:TimeRuler>
                        </TimeRulers>
                        <AppointmentDisplayOptions ColumnPadding-Left="2" ColumnPadding-Right="4"></AppointmentDisplayOptions>
                    </WorkWeekView>
                    <WeekView Enabled="false"></WeekView>
                    <MonthView ViewSelectorItemAdaptivePriority="5"></MonthView>
                    <TimelineView ViewSelectorItemAdaptivePriority="3"></TimelineView>
                    <FullWeekView Enabled="true">
                        <TimeRulers>
                            <cc1:TimeRuler></cc1:TimeRuler>
                        </TimeRulers>
                        <AppointmentDisplayOptions ColumnPadding-Left="2" ColumnPadding-Right="4"></AppointmentDisplayOptions>
                    </FullWeekView>
                    <AgendaView DayHeaderOrientation="Auto" ViewSelectorItemAdaptivePriority="1"></AgendaView>
                </Views>
                <OptionsCustomization AllowAppointmentCopy="None" AllowAppointmentCreate="None" AllowAppointmentDelete="None" AllowAppointmentDrag="None" AllowAppointmentDragBetweenResources="None" AllowAppointmentEdit="None" AllowAppointmentMultiSelect="False" AllowAppointmentResize="None" AllowDisplayAppointmentForm="Never" AllowInplaceEditor="None" />
                <OptionsToolTips AppointmentToolTipCornerType="None"></OptionsToolTips>
            </dxwschs:ASPxScheduler>
            <asp:SqlDataSource ID="nterCalData" runat="server" ConnectionString="<%$ ConnectionStrings:nterConnectionString %>" ProviderName="<%$ ConnectionStrings:nterConnectionString.ProviderName %>" DeleteCommand="DELETE FROM tblcalender_events WHERE UniqueID = @UniqueID" SelectCommand="SELECT * FROM tblcalender_events WHERE Calender_ID = @Calender_ID" >
                <DeleteParameters>
                    <asp:Parameter Name="UniqueID" Type="Int32" />
                </DeleteParameters>
                <SelectParameters>
                    <asp:Parameter Name="Calender_ID" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contScripts" runat="Server">
</asp:Content>