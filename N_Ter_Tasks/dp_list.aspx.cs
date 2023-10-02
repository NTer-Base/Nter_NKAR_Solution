using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class dp_list : System.Web.UI.Page
    {
        private Task_Controls_Main objDPCtrlList = new Task_Controls_Main();
        public string required_fields;
        public string Tag_List;
        public string Tag_Names;
        public string Grid_Last_Col;
        public string Refresh_Frequency;
        public string DocHelpScript = "";

        public string DocHelpPanelResizeScript = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            if (IsPostBack == false)
            {
                bool AllowDelete = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).isAllowed(AllowedAreas.Delete_Docs, objSes.UserID);
                ViewState["del"] = (AllowDelete ? "1" : "0");

                ltrEL2.Text = objSes.EL2;

                LoadEL2s(objSes);
                if (!string.IsNullOrEmpty(Request.QueryString["dpid"]))
                {
                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    string Doc_Repo_ID = objURL.Decrypt(Convert.ToString(Request.QueryString["dpid"]));

                    if (ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type).Can_Access_Repo(Convert.ToInt32(Doc_Repo_ID), objSes.UserID))
                    {
                        hndDoc_Proj_ID.Value = Doc_Repo_ID;
                    }
                    else
                    {
                        hndDoc_Proj_ID.Value = "0";
                    }
                }
                else
                {
                    hndDoc_Proj_ID.Value = "0";
                }
            }

            RefreshGrid(objSes);

            cmdSave.Attributes.Add("onclick", "return ValidatRequiredFields();");
            cmdReset.Attributes.Add("onclick", "return ClearUploadControls();");
            cboEL2Name.Attributes.Add("onchange", "LoadRelatedTasks();");
            Refresh_Frequency = objSes.RefFreq.ToString();
        }

        private void LoadEL2s(SessionObject objSes)
        {
            Entity_Level_2 objEL2 = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type);
            DS_Entity_Level_2 ds = objEL2.ReadForUser(objSes.UserID);

            if (ds.tblentity_level_2.Rows.Count > 0)
            {
                cboEL2Name.DataSource = ds.tblentity_level_2;
                cboEL2Name.DataValueField = "Entity_L2_ID";
                cboEL2Name.DataTextField = "Display_Name";
                cboEL2Name.DataBind();
            }
            else
            {
                cboEL2Name.Items.Add(new ListItem("", "0"));
            }
        }

        private void RefreshGrid(SessionObject objSes)
        {
            Document_Projects objDoc = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
            DS_Doc_Project ds = objDoc.ReadForUser(objSes.UserID);

            ltrDocProjects.Text = "";
            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();

            if (ds.tbldocument_project.Rows.Count > 0)
            {
                if (hndDoc_Proj_ID.Value == "0")
                {
                    hndDoc_Proj_ID.Value = Convert.ToString(ds.tbldocument_project[0].Document_Project_ID);
                }
            }

            foreach (DS_Doc_Project.tbldocument_projectRow row in ds.tbldocument_project)
            {
                if (Convert.ToInt32(hndDoc_Proj_ID.Value) == row.Document_Project_ID)
                {
                    ltrDocProjects.Text = ltrDocProjects.Text + "<a href=\"dp_list.aspx?dpid=" + objURL.Encrypt(Convert.ToString(row.Document_Project_ID)) + "\" class=\"list-group-item active\">" + "\r\n" +
                                                        "<h4 class=\"list-group-item-heading text-thin\">" + row.Doc_Project_Name + "</h4>" + "\r\n" +
                                                    "</a>" + "\r\n";
                }
                else
                {
                    ltrDocProjects.Text = ltrDocProjects.Text + "<a href=\"dp_list.aspx?dpid=" + objURL.Encrypt(Convert.ToString(row.Document_Project_ID)) + "\" class=\"list-group-item\">" + "\r\n" +
                                                        "<h4 class=\"list-group-item-heading text-thin\">" + row.Doc_Project_Name + "</h4>" + "\r\n" +
                                                    "</a>" + "\r\n";
                }
            }

            if (Convert.ToInt32(hndDoc_Proj_ID.Value) > 0)
            {
                pnlStep.Visible = true;
                LoadDocuments(objSes);
                LoadTags(Convert.ToInt32(hndDoc_Proj_ID.Value), objSes);
            }
            else
            {
                pnlStep.Visible = false;
            }
        }

        private void LoadDocuments(SessionObject objSes)
        {
            Document_Projects objDocProj = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
            DS_Doc_Project dsDocProj = objDocProj.Read(Convert.ToInt32(hndDoc_Proj_ID.Value));

            Tag_List = "";
            Tag_Names = "";
            int intIndex = 1;
            foreach (DS_Doc_Project.tbldocument_project_indexesRow row in dsDocProj.tbldocument_project_indexes)
            {
                Tag_Names = Tag_Names + "\r\n<th>" + row.Tag_Name + "</th>";
                Tag_List = Tag_List + ",\r\n{ data: \"doctag_" + intIndex + "\" }";
                intIndex++;
            }
            Grid_Last_Col = (intIndex + 3).ToString();
        }

        private void LoadTags(int Document_Project_ID, SessionObject objSes)
        {
            divTags.Controls.Clear();

            N_Ter.Common.Common_Task_Actions objTskAct = new N_Ter.Common.Common_Task_Actions();

            Document_Projects objDP = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
            DS_Doc_Project ds = objDP.ReadWithLabels(Document_Project_ID);

            string strRequiredFieldValidation = "";
            required_fields = "function ValidatRequiredFields() {" + "\r\n" +
                                    "remove_field_erros();" + "\r\n" +
                                    "var ret = ValidateFileUpload('" + fulDocument.ClientID + "');" + "\r\n";

            DS_Documents dsTemp = new DS_Documents();
            N_Ter.Common.Script_Generator objScripts = new N_Ter.Common.Script_Generator();
            N_Ter.Customizable.Master_Tables objMasters = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);
            int ControlIndex = 0;
            List<string> Help_Texts = new List<string>();

            divTags.Attributes.Add("class", "row");

            System.Web.UI.HtmlControls.HtmlGenericControl divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
            int rowWidth = 0;

            foreach (DS_Doc_Project.tbldocument_project_indexesRow row in ds.tbldocument_project_indexes)
            {
                divMainRowControl.Controls.Add(objTskAct.GetDocObject(objScripts, IsPostBack, objMasters, dsTemp.tbldocument_tags, objSes.Currency_Sbl, row, ref objDPCtrlList, ref strRequiredFieldValidation, ref rowWidth, ControlIndex, "GetDocHelp", true, false));
                if (row.Help_Text.Trim() != "")
                {
                    Help_Texts.Add(row.Tag_Name + "|" + row.Help_Text);
                    ControlIndex++;
                }
                if (rowWidth == 12)
                {
                    divTags.Controls.Add(divMainRowControl);
                    divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                    divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                    rowWidth = 0;
                }
            }
            if (rowWidth > 0)
            {
                divTags.Controls.Add(divMainRowControl);
            }

            required_fields = required_fields + strRequiredFieldValidation + "return ret;" + "\r\n" +
                                                                "}" + "\r\n";
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

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Document_Project_ID = Convert.ToInt32(hndDoc_Proj_ID.Value);
            Documents objDocs = ObjectCreator.GetDocuments(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            DS_Entity_Level_2.tblentity_level_2Row drComp = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type).Read(Convert.ToInt32(cboEL2Name.SelectedItem.Value));

            string PathPart = "\\" + drComp.Folder_Name + "\\Document_Repos\\" + DateTime.Today.Year + "\\" + DateTime.Today.Month;

            string fn = System.IO.Path.GetFileName(fulDocument.PostedFile.FileName);
            string[] fnSP = fn.Split('.');

            string SaveLocation = objSes.PhysicalRoot + "\\nter_app_uploads\\client_docs" + PathPart;
            string SaveLocationDB = "nter_app_uploads\\client_docs" + PathPart;
            if (System.IO.Directory.Exists(SaveLocation) == false)
            {
                System.IO.Directory.CreateDirectory(SaveLocation);
            }

            string NewFileName = N_Ter.Common.Common_Actions.TimeStampForFileName();
            if (txtFileName.Text.Trim() != "")
            {
                NewFileName = txtFileName.Text.Trim() + "_" + N_Ter.Common.Common_Actions.TimeStampForFileName();
            }
            else
            {
                NewFileName = fulDocument.FileName.Substring(0, fulDocument.FileName.LastIndexOf('.')) + "_" + NewFileName;
            }

            N_Ter.Common.Common_Actions objComAct = new N_Ter.Common.Common_Actions();
            NewFileName = objComAct.FormatFileName(NewFileName);

            SaveLocation = SaveLocation + "\\" + NewFileName + "." + fnSP[fnSP.Length - 1];
            SaveLocationDB = SaveLocationDB + "\\" + NewFileName + "." + fnSP[fnSP.Length - 1];
            try
            {
                fulDocument.PostedFile.SaveAs(SaveLocation);
                int MaxID = objDocs.Insert(SaveLocationDB, Document_Project_ID, objSes.UserID, Convert.ToInt32(cboEL2Name.SelectedItem.Value), SaveLocation, Convert.ToInt32(cboAccessLevel.SelectedItem.Value));
                if (MaxID > 0)
                {
                    N_Ter.Common.Common_Task_Actions objCommAct = new N_Ter.Common.Common_Task_Actions();
                    DS_Documents ds = objCommAct.PrepareDocControlsDataSet(MaxID, objDPCtrlList, true);

                    if (objDocs.UpdateTags(MaxID, ds.tbldocument_tags) == true)
                    {
                        if (Convert.ToInt32(hndRelatedTaskID.Value) > 0)
                        {
                            Documents objDoc = ObjectCreator.GetDocuments(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                            if (objDoc.AddTaskAttachment(Convert.ToInt32(hndRelatedTaskID.Value), MaxID, objSes.UserID) == false)
                            {
                                ClientScriptManager cs = Page.ClientScript;
                                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Database Error : Cannot attach file details to task');", true);
                            }
                            else
                            {
                                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                                Response.Redirect("document.aspx?did=" + objURL.Encrypt(Convert.ToString(MaxID)));
                            }
                        }
                        else
                        {
                            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                            Response.Redirect("document.aspx?did=" + objURL.Encrypt(Convert.ToString(MaxID)));
                        }
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Database Error : Cannot save the Document Tags');", true);
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

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            bool AllowDelete = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).isAllowed(AllowedAreas.Delete_Docs, objSes.UserID);
            if (AllowDelete == true)
            {
                Documents objDoc = ObjectCreator.GetDocuments(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                DeleteReason del = objDoc.DeleteDocument(Convert.ToInt32(hndDocument_ID.Value));
                if (del.Deleted == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Document Successfully Deleted');", true);
                    RefreshGrid(objSes);
                    hndDocument_ID.Value = "0";
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('You are not allowed to Delete Documents');", true);
            }
        }

        protected void cmdAdvFilter_ServerClick(object sender, EventArgs e)
        {
            N_Ter.Security.URL_Manager obURL = new N_Ter.Security.URL_Manager();
            Response.Redirect("dp_search.aspx?dpid=" + obURL.Encrypt(hndDoc_Proj_ID.Value));
        }
    }
}