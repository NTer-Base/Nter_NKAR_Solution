using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace N_Ter_Tasks
{
    public partial class doc_upload_external : System.Web.UI.Page
    {
        private Task_Controls_Main objDPCrtlList = new Task_Controls_Main();
        public string Required_Fields = "";
        public string DocHelpScript = "";

        public string DocHelpPanelResizeScript = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            divDocTags.Visible = false;
            divSubmit.Visible = false;
            divUpload.Visible = false;
            altHelp.Visible = false;
            divError.Visible = true;

            if (Request.QueryString["t"] != null && Request.QueryString["t"].Trim() != "")
            {
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                int Task_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["t"])));

                Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                DS_Tasks dsTask = objTask.Read(Task_ID, false, false, false);

                if (dsTask.tbltasks.Rows.Count > 0)
                {
                    Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                    DS_Workflow dsWF = objWF.Read(dsTask.tbltasks[0].Walkflow_ID, false, false);

                    if (dsWF.tblwalkflow.Rows.Count > 0 && dsWF.tblwalkflow[0].Document_Project_ID > 0)
                    {
                        divDocTags.Visible = true;
                        divSubmit.Visible = true;
                        divUpload.Visible = true;
                        divError.Visible = false;
                        LoadTagDetails(dsWF.tblwalkflow[0].Document_Project_ID, objSes);
                        LoadGuestHelp(objSes, dsWF.tblwalkflow[0].Document_Project_ID);
                        ViewState["tid"] = Task_ID;
                    }
                }
            }

            cmdSubmit.OnClientClick = "return ValidateForm()";
        }

        private void LoadGuestHelp(SessionObject objSes, int Doc_Project_ID)
        {
            DS_Doc_Project dsDP = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type).Read(Doc_Project_ID);
            if (dsDP.tbldocument_project.Rows.Count > 0 && dsDP.tbldocument_project[0].Guest_Help.Trim() != "")
            {
                altHelp.Visible = true;
                ltrGuestHelp.Text = dsDP.tbldocument_project[0].Guest_Help;
            }
        }

        private void LoadTagDetails(int Document_Project_Name, SessionObject objSes)
        {
            divDocTags.Controls.Clear();
            Document_Projects objDP = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
            DS_Doc_Project ds = objDP.ReadWithLabels(Document_Project_Name);

            N_Ter.Common.Common_Task_Actions objTskAct = new N_Ter.Common.Common_Task_Actions();

            string strRequiredFieldValidation = "";
            Required_Fields = "function ValidatRequiredFields() {" + "\r\n" +
                                      "remove_field_erros();" + "\r\n" +
                                      "var ret = true;" + "\r\n";

            DS_Documents dsTemp = new DS_Documents();

            N_Ter.Common.Script_Generator objScripts = new N_Ter.Common.Script_Generator();
            N_Ter.Customizable.Master_Tables objMasters = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);
            int ControlIndex = 0;
            List<string> Help_Texts = new List<string>();

            divDocTags.Attributes.Add("class", "row");

            System.Web.UI.HtmlControls.HtmlGenericControl divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
            int rowWidth = 0;

            foreach (DS_Doc_Project.tbldocument_project_indexesRow row in ds.tbldocument_project_indexes)
            {
                divDocTags.Controls.Add(objTskAct.GetDocObject(objScripts, IsPostBack, objMasters, dsTemp.tbldocument_tags, objSes.Currency_Sbl, row, ref objDPCrtlList, ref strRequiredFieldValidation, ref rowWidth, ControlIndex, "GetDocHelp", false, false));
                if (row.Help_Text.Trim() != "")
                {
                    Help_Texts.Add(row.Tag_Name + "|" + row.Help_Text);
                    ControlIndex++;
                }
                if (rowWidth == 12)
                {
                    divDocTags.Controls.Add(divMainRowControl);
                    divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                    divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                    rowWidth = 0;
                }
            }
            if (rowWidth > 0)
            {
                divDocTags.Controls.Add(divMainRowControl);
            }

            Required_Fields = Required_Fields + strRequiredFieldValidation + "return ret;" + "\r\n" +
                                                                "}";
            if (Help_Texts.Count > 0)
            {
                hndDocHelp_ModalPopupExtender.Enabled = true;
                pnlDocHelp.Visible = true;
                objScripts.LoadDocHelpScripts(ref DocHelpScript, ref DocHelpPanelResizeScript, Help_Texts);
            }
            else
            {
                DocHelpScript = "";
                DocHelpPanelResizeScript = "";
                hndDocHelp_ModalPopupExtender.Enabled = false;
                pnlDocHelp.Visible = false;
            }
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            DS_Settings dsSett = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type).Read();
            if (dsSett.tblsettings[0].User_Group_Guest > 0)
            {
                DS_Users dsUsr = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type).ReadUsers(dsSett.tblsettings[0].User_Group_Guest);
                if (dsUsr.tblusers.Rows.Count > 0)
                {
                    int User_ID = dsUsr.tblusers[0].User_ID;

                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    int Task_ID = Convert.ToInt32(ViewState["tid"]);

                    Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                    DS_Tasks dsTask = objTask.Read(Task_ID, false, false, false);

                    if (dsTask.tbltasks.Rows.Count > 0)
                    {
                        Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                        DS_Workflow dsWF = objWF.Read(dsTask.tbltasks[0].Walkflow_ID, false, false);

                        if (dsWF.tblwalkflow.Rows.Count > 0 && dsWF.tblwalkflow[0].Document_Project_ID > 0)
                        {
                            Documents objDocs = ObjectCreator.GetDocuments(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

                            DS_Entity_Level_2.tblentity_level_2Row drComp = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type).Read(dsTask.tbltasks[0].Entity_L2_ID);
                            string PathPart = "\\" + drComp.Folder_Name + "\\Document_Repos\\" + DateTime.Today.Year + "\\" + DateTime.Today.Month;

                            string fn = System.IO.Path.GetFileName(fulDocument.PostedFile.FileName);
                            string[] fnSP = fn.Split('.');

                            string SaveLocation = objSes.PhysicalRoot + "\\nter_app_uploads\\client_docs" + PathPart;
                            string SaveLocationDB = "nter_app_uploads\\client_docs" + PathPart;
                            if (System.IO.Directory.Exists(SaveLocation) == false)
                            {
                                System.IO.Directory.CreateDirectory(SaveLocation);
                            }

                            string NewFileName = "doc_" + N_Ter.Common.Common_Actions.TimeStampForFileName();
                            SaveLocation = SaveLocation + "\\" + NewFileName + "." + fnSP[fnSP.Length - 1];
                            SaveLocationDB = SaveLocationDB + "\\" + NewFileName + "." + fnSP[fnSP.Length - 1];
                            try
                            {
                                fulDocument.PostedFile.SaveAs(SaveLocation);
                                int MaxID = objDocs.Insert(SaveLocationDB, dsWF.tblwalkflow[0].Document_Project_ID, User_ID, drComp.Entity_L2_ID, SaveLocation, 3);
                                if (MaxID > 0)
                                {
                                    if (objDocs.AddTaskAttachment(Task_ID, MaxID, User_ID) == false)
                                    {
                                        ClientScriptManager cs = Page.ClientScript;
                                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Database Error : Cannot attach file details to task');", true);
                                    }
                                    else
                                    {
                                        N_Ter.Common.Common_Task_Actions objCommAct = new N_Ter.Common.Common_Task_Actions();
                                        DS_Documents dsDoc = objCommAct.PrepareDocControlsDataSet(MaxID, objDPCrtlList, true);

                                        if (objDocs.UpdateTags(MaxID, dsDoc.tbldocument_tags) == true)
                                        {
                                            objDocs.SendDPTemplateEmail(MaxID, User_ID, objSes.Currency_Sbl);
                                            Response.Redirect("doc_upload_external.aspx?t=" + objURL.Encrypt(Convert.ToString(Task_ID)) + "&msg=" + new N_Ter.Security.URL_Manager().Encrypt("Document Successfully Uploaded"));
                                        }
                                        else
                                        {
                                            ClientScriptManager cs = Page.ClientScript;
                                            cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Database Error : Cannot save the Document Tags');", true);
                                        }
                                    }
                                }
                                else
                                {
                                    ClientScriptManager cs = Page.ClientScript;
                                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Database Error : Cannot save the file details');", true);
                                }
                            }
                            catch (Exception Exc)
                            {
                                ClientScriptManager cs = Page.ClientScript;
                                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Error: " + Exc.Message + "');", true);
                            }
                        }
                        else
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Workflow is disabled, Please contact the System's Asministrator');", true);
                        }
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Guest user account is disabled, Please contact the System's Asministrator');", true);
                    }
                }
                else
                {
                    Response.Redirect("guest_error.aspx?");
                }
            }
            else
            {
                Response.Redirect("guest_error.aspx?");
            }
        }
    }
}