using N_Ter.Base;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Web.UI;

namespace N_Ter_Tasks
{
    public partial class schedule : System.Web.UI.Page
    {
        public string Schedule_Field_Names;
        public string Schedule_Fields;
        public string Schedule_ID;
        public string HasEnt2Data;
        public string LastFieldIndex;
        public string Refresh_Frequency;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            if (IsPostBack == false)
            {
                if (Request.QueryString["wsid"] == null)
                {
                    Response.Redirect("error.aspx?");
                }
                else
                {
                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    int Workflow_Schedule_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["wsid"])));
                    ViewState["wsid"] = Workflow_Schedule_ID;
                    Workflow_Schedules objWS = ObjectCreator.GetWorkflow_Schedules(objSes.Connection, objSes.DB_Type);
                    DS_Workflow_Scedules.tblworkflow_schedulesRow dr = objWS.Read(Workflow_Schedule_ID);
                    lblScheduleName.Text = ": " + dr.Schedule_Name;
                    lblEntityName.Text = dr.Schedule_Entity_Name;
                    if (dr.Schedule_Entity2_Name.Trim() == "")
                    {
                        divEntity2.Attributes["class"] = "form-group hide";
                    }
                    else
                    {
                        divEntity2.Attributes["class"] = "form-group";
                        lblEntity2Name.Text = dr.Schedule_Entity2_Name;
                    }

                    lblEntityDate.Text = dr.Schedule_Entity_Date_Name + " (Date)";
                    lblEntityTime.Text = dr.Schedule_Entity_Date_Name + " (Time)";
                }
                hndScheduleLineID.Value = "0";
            }
            RefreshGrid(objSes);
            cmdNewRecord.Attributes.Add("onClick", "javascript:document.getElementById('" + hndScheduleLineID.ClientID + "').value = '0'; ClearControls();");
            cmdResetSchedule.OnClientClick = "return ResetControls();";
            cmdSaveSchedule.OnClientClick = "return ValidateWorkflowScheduleData('" + txtEntityName.ClientID + "', '" + txtEntityDate.ClientID + "', '" + cboEntityTime.ClientID + "');";
            Refresh_Frequency = objSes.RefFreq.ToString();
        }

        private void RefreshGrid(SessionObject objSes)
        {
            Schedule_ID = Convert.ToString(ViewState["wsid"]);
            Schedule_Field_Names = "";
            Schedule_Fields = "";
            DS_Workflow_Scedules.tblworkflow_schedulesRow dr = ObjectCreator.GetWorkflow_Schedules(objSes.Connection, objSes.DB_Type).Read(Convert.ToInt32(Schedule_ID));
            int intIndex = 2;
            Schedule_Field_Names = "<th>" + dr.Schedule_Entity_Name + "</th>";
            Schedule_Fields = ",\r\n{ data: \"Entity_Data\" }";
            intIndex++;
            if (dr.IsSchedule_Entity2_NameNull() == false && dr.Schedule_Entity2_Name.Trim() != "")
            {
                HasEnt2Data = "1";
                Schedule_Field_Names = Schedule_Field_Names + "<th>" + dr.Schedule_Entity2_Name + "</th>";
                Schedule_Fields = Schedule_Fields + ",\r\n{ data: \"Entity_Data2\" }";
                intIndex++;
            }
            else
            {
                HasEnt2Data = "0";
            }
            Schedule_Field_Names = Schedule_Field_Names + "<th>" + dr.Schedule_Entity_Date_Name + "</th>";
            Schedule_Fields = Schedule_Fields + ",\r\n{ data: \"Entity_Data_Date\" }";
            LastFieldIndex = intIndex.ToString();
        }

        protected void cmdSaveSchedule_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            DateTime EntityDate = DateTime.Today.Date;
            Common_Actions objCom = new Common_Actions();

            if (objCom.ValidateDateTime(txtEntityDate.Text + " " + cboEntityTime.Text, ref EntityDate) == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Invalid Date');", true);
            }
            else
            {
                EntityDate = DateTime.ParseExact(txtEntityDate.Text + " " + cboEntityTime.Text, Constants.DateFormat + " HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                Workflow_Schedules objWS = ObjectCreator.GetWorkflow_Schedules(objSes.Connection, objSes.DB_Type);
                if (hndScheduleLineID.Value == "0")
                {
                    if (objWS.InsertScheduleData(Convert.ToInt32(ViewState["wsid"]), txtEntityName.Text, txtEntity2Name.Text, EntityDate) == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Record could not be saved');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Record Successfully Added');", true);
                        hndScheduleLineID.Value = "0";
                    }
                }
                else
                {
                    if (objWS.UpdateScheduleData(Convert.ToInt32((hndScheduleLineID.Value)), Convert.ToInt32(ViewState["wsid"]), txtEntityName.Text, txtEntity2Name.Text, EntityDate) == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Record could not be saved');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Record Successfully Updated');", true);
                        hndScheduleLineID.Value = "0";
                    }
                }
            }
        }

        protected void cmdDeleteSchedule_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Workflow_Schedules objWS = ObjectCreator.GetWorkflow_Schedules(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objWS.DeleteSheduleData(Convert.ToInt32(hndScheduleLineID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Record Successfully Deleted');", true);
                hndScheduleLineID.Value = "0";
            }
        }
    }
}