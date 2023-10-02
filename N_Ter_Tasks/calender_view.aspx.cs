using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using N_Ter.Base;


namespace N_Ter_Tasks
{
    public partial class calender_view : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Calendars objCal = ObjectCreator.GetCalendars(objSes.Connection, objSes.DB_Type);
            objCal.AdjustSchedulerMappings(ref scdCalender);
            if (IsPostBack == false)
            {
                DS_Calenders dsCal = objCal.ReadAllForUser(objSes.UserID);
                if (dsCal.tblcalenders.Rows.Count > 0)
                {
                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();

                    int Cal_ID;
                    if (Request.QueryString["cid"] == null)
                    {
                        Cal_ID = dsCal.tblcalenders[0].Calender_ID;
                    }
                    else
                    {
                        Cal_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["cid"])));
                    }

                    ViewState["cid"] = Cal_ID;

                    ltrOtherCal.Text = "";
                    foreach (DS_Calenders.tblcalendersRow row in dsCal.tblcalenders)
                    {
                        if (row.Calender_ID == Cal_ID)
                        {
                            ltrSelectedCal.Text = row.Calender_Name;
                        }
                        else
                        {
                            ltrOtherCal.Text = ltrOtherCal.Text + "<li><a href=\"calender_view.aspx?cid=" + objURL.Encrypt(Convert.ToString(row.Calender_ID)) + "\">" + row.Calender_Name + "</span></a></li>";
                        }
                    }

                    if (ltrOtherCal.Text.Trim() == "")
                    {
                        ltrOtherCal.Text = "<li style='padding: 0 10px'>No Other Calendars</li>";
                    }

                    divCalender.Visible = true;
                    divNoCals.Visible = false;
                    if (objSes.isAdmin == 1 || objSes.isAdmin == 2)
                    {
                        scdCalender.OptionsCustomization.AllowAppointmentDelete = DevExpress.XtraScheduler.UsedAppointmentType.NonRecurring;
                    }
                }
                else
                {
                    divCalender.Visible = false;
                    divNoCals.Visible = true;
                }
                scdCalender.Start = DateTime.Today;
            }
            nterCalData.SelectParameters["Calender_ID"].DefaultValue = Convert.ToString(ViewState["cid"]);
        }
    }
}