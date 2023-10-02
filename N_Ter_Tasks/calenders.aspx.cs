using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter.Base;

namespace N_Ter_Tasks
{
    public partial class calenders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                SessionObject objSes = (SessionObject)Session["dt"];
                hndCalender_ID.Value = "0";
                LoadUserGroup(objSes);
                RefreshGrid(objSes);
            }
            cmdNew.Attributes.Add("onClick", "javascript:document.getElementById('" + hndCalender_ID.ClientID + "').value = '0'; ClearControls();");
            cmdReset.OnClientClick = "return ResetControls();";
            cmdSave.OnClientClick = "return ValidateCalender('" + txtCalenderName.ClientID + "', '" + cboUserGroup.ClientID + "', '" + txtSubject.ClientID + "', '" + txtDescription.ClientID + "');";
        }

        private void LoadUserGroup(SessionObject objSes)
        {
            User_Groups objUG = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type);
            DS_Users ds = objUG.ReadAll();
            cboUserGroup.DataSource = ds.tbluser_groups;
            cboUserGroup.DataValueField = "User_Group_ID";
            cboUserGroup.DataTextField = "User_Group_Name";
            cboUserGroup.DataBind();
        }

        private void RefreshGrid(SessionObject objSes)
        {
            Calendars objCal = ObjectCreator.GetCalendars(objSes.Connection, objSes.DB_Type);
            DS_Calenders ds = objCal.ReadAll();
            gvMain.SelectedIndex = -1;
            gvMain.DataSource = ds.tblcalenders;
            gvMain.DataBind();
            if (ds.tblcalenders.Rows.Count > 0)
            {
                gvMain.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                if (e.Row.Cells[1].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdEdit = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[1].Controls[1]);
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndCalender_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; LoadValues();");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdDelete = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdDelete.Attributes.Add("onclick", "document.getElementById('" + hndCalender_ID.ClientID + "').value = '" + e.Row.Cells[0].Text + "';");
                }
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Calendars objCal = ObjectCreator.GetCalendars(objSes.Connection, objSes.DB_Type);
            if (hndCalender_ID.Value == "0")
            {
                if (objCal.Insert(txtCalenderName.Text, Convert.ToInt32(cboUserGroup.SelectedItem.Value), chkAllowConflicts.Checked, txtSubject.Text, txtDescription.Text) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Calendar could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Calendar Details Successfully Added');", true);
                    hndCalender_ID.Value = "0";
                }
            }
            else
            {
                if (objCal.Update(Convert.ToInt32(hndCalender_ID.Value), txtCalenderName.Text, Convert.ToInt32(cboUserGroup.SelectedItem.Value), chkAllowConflicts.Checked, txtSubject.Text, txtDescription.Text) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Calendar could not be saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Calendar Details Successfully Updated');", true);
                    hndCalender_ID.Value = "0";
                }
            }
            RefreshGrid(objSes);
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Calendars objCal = ObjectCreator.GetCalendars(objSes.Connection, objSes.DB_Type);
            DeleteReason del = objCal.Delete(Convert.ToInt32(hndCalender_ID.Value));
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Calendar Successfully Deleted');", true);
                hndCalender_ID.Value = "0";
            }
            RefreshGrid(objSes);
        }
    }
}