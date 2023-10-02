using N_Ter.Base;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Web.UI;

namespace N_Ter_Tasks
{
    public partial class messages : System.Web.UI.Page
    {
        public string Start_Script;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            if (!IsPostBack)
            {
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                LoadUsers(objURL, objSes);

                if (Request.QueryString["trid"] != null)
                {
                    Start_Script = "$find('mpuMessage').show();\r\n" +
                                   "LoadMessage('" + objURL.Decrypt(Convert.ToString(Request.QueryString["trid"])) + "');";
                }
                else
                {
                    Start_Script = "";
                }
            }
            else
            {
                Start_Script = "";
            }
            hndMID.Value = Convert.ToString(objSes.UserID);
            cmdNewMessage.Attributes.Add("onClick", "javascript:LoadMessage(0);");
            cmdSendMessage.Attributes.Add("onClick", "javascript:return ValidateMessage('" + hndTID.ClientID + "', '" + txtTitle.ClientID + "', '" + txtMessage.ClientID + "')");
        }

        private void LoadUsers(N_Ter.Security.URL_Manager objURL, SessionObject objSes)
        {
            Messages objMsgs = ObjectCreator.GetMessages(objSes.Connection, objSes.DB_Type);
            DS_Messages ds = objMsgs.ReadUsers(objSes.UserID);

            int CurrentUser = 0;
            if (IsPostBack == false)
            {
                if (Request.QueryString["uid"] != null)
                {
                    CurrentUser = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["uid"])));
                }
                else
                {
                    if (ds.tblusers.Rows.Count > 0)
                    {
                        CurrentUser = ds.tblusers[0].User_ID;
                    }
                }
                ViewState["uid"] = CurrentUser;
            }
            else
            {
                CurrentUser = Convert.ToInt32(ViewState["uid"]);
            }

            ltrUsersList.Text = "";
            foreach (DS_Messages.tblusersRow row in ds.tblusers)
            {
                if (row.New_Message_Count > 0)
                {
                    ltrUsersList.Text = ltrUsersList.Text + "<div class=\"message h5 text-bold " + (row.User_ID == CurrentUser ? "well well-sm" : "") + "\">" +
                                                                "<a href=\"messages.aspx?uid=" + objURL.Encrypt(row.User_ID.ToString()) + "\" title=\"\" class=\"title msg-user\"><div class=\"action-checkbox\">" +
                                                                    "<i class=\"fa fa-user\"></i>" +
                                                                "</div>" +
                                                                "<div class='date'><span class=\"label label-primary pull-right\">" + row.New_Message_Count + "</span></div>" +
                                                                string.Format("{0} {1}", row.First_Name, row.IsLast_NameNull() ? "" : row.Last_Name) + "</a>" +
                                                            "</div> ";
                }
                else
                {
                    ltrUsersList.Text = ltrUsersList.Text + "<div class=\"message h5 text-bold " + (row.User_ID == CurrentUser ? "well well-sm" : "") + "\">" +
                                                                "<a href=\"messages.aspx?uid=" + objURL.Encrypt(row.User_ID.ToString()) + "\" title=\"\" class=\"title msg-user\"><div class=\"action-checkbox\">" +
                                                                    "<i class=\"fa fa-user\"></i>" +
                                                                "</div>" +
                                                                string.Format("{0} {1}", row.First_Name, row.IsLast_NameNull() ? "" : row.Last_Name) + "</a>" +
                                                            "</div> ";
                }
            }
            LoadThreads(objMsgs, CurrentUser, objSes);
        }

        private void LoadThreads(Messages objMsgs, int User_ID, SessionObject objSes)
        {
            DS_Messages ds = objMsgs.ReadThreads(objSes.UserID, User_ID);

            if (ds.tblmessage_threads.Rows.Count > 0)
            {
                divNoRecords.Visible = false;
                ltrThreads.Text = "<div class=\"table-responsive table-primary no-margin-b\"><table id=\"message_table\" class=\"table table-striped table-hover grid_table row-pointer no-margin-b non_full_width_table\" data-size=\"non_full_width_table\">" + "\r\n" +
                                        "<thead>" + "\r\n" +
                                            "<tr>" + "\r\n" +
                                                "<th>Subject</th>" + "\r\n" +
                                                "<th class=\"hidden-xs ww40\">#</th>" + "\r\n" +
                                                "<th class=\"text-center ww100\">Date</th>" + "\r\n" +
                                            "</tr>" + "\r\n" +
                                        "</thead>" + "\r\n" +
                                        "<tbody>" + "\r\n";
                foreach (DS_Messages.tblmessage_threadsRow row in ds.tblmessage_threads)
                {
                    ltrThreads.Text = ltrThreads.Text + "<tr class=\"" + (row.New_Message_Count > 0 ? "message-read " : "message-unread ") + "clickable-row\" data-href='" + row.Thread_ID + "'>" + "\r\n" +
                                                            "<td>" + row.Title + "</td>" + "\r\n" +
                                                            "<td class=\"hidden-xs ww40\">" + "\r\n" +
                                                            (row.New_Message_Count > 0 ? "<span class=\"label label-info mr10 fs11\">" + row.New_Message_Count + "</span>" + "\r\n" : "") +
                                                            "</td>" + "\r\n" +
                                                            "<td class=\"text-center ww100\">" + string.Format("{0:" + Constants.DateFormat + "}", row.Last_Message_Date) + "</td>" + "\r\n" +
                                                        "</tr>" + "\r\n";
                }
                ltrThreads.Text = ltrThreads.Text + "</tbody>" + "\r\n" +
                                                "</table></div>" + "\r\n";
            }
            else
            {
                divNoRecords.Visible = true;
                ltrThreads.Text = "";
            }
        }

        protected void cmdSendMessage_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Messages objMessage = ObjectCreator.GetMessages(objSes.Connection, objSes.DB_Type);
            int Other_User = Convert.ToInt32(ViewState["uid"]);
            if (hndTID.Value == "0")
            {
                if (objMessage.AddThread(objSes.UserID, Other_User, txtTitle.Text, txtMessage.Text, objSes.UserID, objSes.PhysicalRoot, objSes.WebRoot) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Message cannot be Sent');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Message Successfully Sent');", true);
                }
            }
            else
            {
                if (objMessage.AddMessage(Convert.ToInt32(hndTID.Value), objSes.UserID, Other_User, txtMessage.Text, objSes.UserID, objSes.PhysicalRoot, objSes.WebRoot) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Message cannot be Sent');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Message Successfully Sent');", true);
                }
            }
            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
            LoadUsers(objURL, objSes);
        }
    }
}