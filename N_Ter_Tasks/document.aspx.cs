using DocumentFormat.OpenXml.Spreadsheet;
using N_Ter.Base;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using ZXing;

namespace N_Ter_Tasks
{
    public partial class document : System.Web.UI.Page
    {
        private Task_Controls_Main objDPCtrlList = new Task_Controls_Main();
        public string Required_Fields = "";
        public string Delete_Scripts = "";
        public string Refresh_Frequency = "";
        public string Document_ID = "";
        public string Tag_List;
        public string Tag_Names;
        public string Grid_Last_Col;
        public string Doc_Project_ID;
        public string DocHelpScript = "";

        public string DocHelpPanelResizeScript = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            if (IsPostBack == false)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["did"]))
                {
                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    int Doc_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["did"])));

                    Documents objDocs = ObjectCreator.GetDocuments(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                    DS_Documents ds = objDocs.Read(Doc_ID);
                    if (ds.tbldocuments.Rows.Count > 0)
                    {
                        if (ds.tbldocuments[0].Access_Level < objSes.AccLevel)
                        {
                            Response.Redirect("no_access.aspx?");
                        }
                        else
                        {
                            if (objDocs.Can_Access_Doc(Doc_ID, objSes.UserID) == false)
                            {
                                Response.Redirect("no_access.aspx?");
                            }
                            else
                            {
                                ltrEL2.Text = objSes.EL2;
                                ltrEL2_2.Text = objSes.EL2;
                                ltrEL2_3.Text = objSes.EL2;

                                ViewState["dpid"] = ds.tbldocuments[0].Document_Project_ID;
                                ViewState["cid"] = ds.tbldocuments[0].Entity_L2_ID;
                                ViewState["did"] = Doc_ID;
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("error.aspx?");
                    }
                }
                else
                {
                    Response.Redirect("error.aspx?");
                }
            }

            User_Groups objGrps = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type);
            DS_Users dsGrps = objGrps.ReadAll(objSes.UserID);
            bool isEditable = false;
            if (ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).isAllowed(AllowedAreas.Edit_Doc_Repos, objSes.UserID))
            {
                isEditable = true;
            }
            ViewState["isEdit"] = isEditable;

            LoadDocument(objSes, isEditable);
            cmdSave.OnClientClick = "return ValidatRequiredFields()";
            cmdAddComment.Attributes.Add("onClick", "ClearCommentControls();");
            cmdUpdateDoc.Attributes.Add("onclick", "return ValidateFileUpload('" + fulDocument.ClientID + "');");
            cmdSaveComment.Attributes.Add("onClick", "return ValidateComment('" + txtCommentMain.ClientID + "');");
            Refresh_Frequency = objSes.RefFreq.ToString();
            Document_ID = Convert.ToString(ViewState["did"]);
        }

        private void LoadDocument(SessionObject objSes, bool isEditable)
        {
            if (isEditable == false)
            {
                divSave.Visible = false;
                divMainEdit.Visible = false;
                cmdChangeAccess_ModalPopupExtender.Enabled = false;
                cmdChangeDocument_ModalPopupExtender.Enabled = false;
                hndDeleteLink_ModalPopupExtender.Enabled = false;
                hndDeleteComment_ModalPopupExtender.Enabled = false;
                pnlUpdateAccessLevel.Visible = false;
                pnlUpdateDoc.Visible = false;
                pnlDeleteDocLink.Visible = false;
                pnlDeleteComment.Visible = false;
                hndDeleteComment.Visible = false;
                hndDeleteLink.Visible = false;
            }
            else
            {
                Delete_Scripts = "function showLinkDel(attID) {\r\n" +
                                    "$('#" + hndDeleteLink.ClientID + "').val(attID);\r\n" +
                                    "$find('mpuDeleteLink').show();\r\n" +
                                    "return false;\r\n" +
                                 "}\r\n" +
                                 "function DeleteComment(commentID) {\r\n" +
                                    "$('#" + hndDeleteComment.ClientID + "').val(commentID);\r\n" +
                                    "$find('mpuDeleteComment').show();\r\n" +
                                    "return false;\r\n" +
                                 "}";
            }
            int Doc_ID = Convert.ToInt32(ViewState["did"]);
            Documents objDocs = ObjectCreator.GetDocuments(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            DS_Documents ds = objDocs.Read(Doc_ID);

            lblDocNumber.Text = ds.tbldocuments[0].Document_No;
            lblDocNumb.Text = ds.tbldocuments[0].Document_No;
            lblEL2Name.Text = ds.tbldocuments[0].Display_Name;
            lblDocProject.Text = ds.tbldocuments[0].Doc_Project_Name;
            lblAccessLevel.Text = "Level " + ds.tbldocuments[0].Access_Level;
            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
            cmdViewDoc.Attributes.Add("onClick", "window.open('document_preview_dp.aspx?fid=" + objURL.Encrypt(Convert.ToString(Doc_ID)) + "', '_blank'); return false;");

            if (IsPostBack == false)
            {
                cboAccessLevel.SelectedValue = Convert.ToString(ds.tbldocuments[0].Access_Level);
            }
            string[] fileSplits = ds.tbldocuments[0].Document_Path.Split('\\');
            lblDocPath.Text = fileSplits[fileSplits.Length - 1];
            LoadBarQR(objSes, ds.tbldocuments[0].Document_No);
            LoadProjDocuments(ds.tbldocuments[0].Document_Project_ID, objSes);
            LoadLinkedDocs(ds.tbldocument_links, objSes, isEditable);
            LoadComments(ds.tbldocument_comments, objSes, isEditable);
            LoadTagDetails(ds.tbldocument_tags, ds.tbldocuments[0].Document_Project_ID, objSes, isEditable);
        }

        private void LoadBarQR(SessionObject objSes, string Document_No)
        {
            BarcodeWriter objBar = new BarcodeWriter();
            objBar.Options.Margin = 0;
            objBar.Format = BarcodeFormat.QR_CODE;
            System.Drawing.Bitmap objResult = objBar.Write(Document_No);
            string FileName = "nter_app_uploads\\temp_email_attachments\\" + Common_Actions.TimeStampForFileName() + objSes.UserID;
            objResult.Save(objSes.PhysicalRoot + "\\" + FileName + "_1.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            objBar.Format = BarcodeFormat.CODE_128;
            objBar.Options.Width = 250;
            objResult = objBar.Write(Document_No);
            objResult.Save(objSes.PhysicalRoot + "\\" + FileName + "_2.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            imgQR_Code.ImageUrl = FileName + "_1.jpg";
            imgBarcode.ImageUrl = FileName + "_2.jpg";
        }

        private void LoadLinkedDocs(DS_Documents.tbldocument_linksDataTable tbl, SessionObject objSes, bool isEditable)
        {
            string AttachmentText = "";
            string[] fileSplits;
            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
            foreach (DS_Documents.tbldocument_linksRow row in tbl.Where(x => x.Doc_Layer == 0))
            {
                if (row.Access_Level >= objSes.AccLevel)
                {
                    fileSplits = row.Document_Path.Split('\\');
                    AttachmentText = AttachmentText + "<div class='comment'>" + "\r\n" +
                                                      " <img src='" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "' alt='Profile Picture' class='comment-avatar'>" + "\r\n" +
                                                      " <div class='comment-body'>" +
                                                            (isEditable && row.Linked_By == objSes.UserID ? "<button class='ttip btn btn-danger btn-sm btn-rounded pull-right' onclick='return showLinkDel(" + row.Document_Link_ID + ");' title='Unlink Document' data-placement='left'><i class='fa fa-unlink'></i></button>" + "\r\n" : "") +
                                                      "     <div class='comment-by'>" + row.Linked_By_Name + "</div>" + "\r\n" +
                                                      "     <div class='comment-text'>" +
                                                      "         <a href='document.aspx?did=" + objURL.Encrypt(Convert.ToString(row.Document_Link_ID)) + "' target='_blank' class='media'>" +
                                                                row.Document_No + " - <b>" + fileSplits[fileSplits.Length - 1] + " - " + row.Entity_L2_Name + "</b>" +
                                                      "         </a>" + "\r\n" +
                                                      "     </div>" +
                                                      "     <div class='comment-actions'>&nbsp;" +
                                                      "         <small><span class='pull-right'>" + string.Format("{0:" + Constants.DateFormat + " HH:mm}", row.Linked_Date) + "</span></small>" + "\r\n" +
                                                      "     </div>\r\n" +
                                                      " </div>\r\n" +
                                                      "</div>";
                    LoadLinkedDocsSub(tbl, row.Linked_Document_ID, ref AttachmentText, objSes, objURL);
                }
            }
            ltrAttachments.Text = AttachmentText;
        }

        private void LoadLinkedDocsSub(DS_Documents.tbldocument_linksDataTable tbl, int Main_Doc_ID, ref string AttachmentsText, SessionObject objSes, N_Ter.Security.URL_Manager objURL)
        {
            string[] fileSplits;
            foreach (DS_Documents.tbldocument_linksRow row in tbl.Where(x => x.Document_ID == Main_Doc_ID))
            {
                if (row.Access_Level >= objSes.AccLevel)
                {
                    fileSplits = row.Document_Path.Split('\\');
                    AttachmentsText = AttachmentsText + "<div class='comment'>" + "\r\n" +
                                                        " <img src='" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "' alt='Profile Picture' class='comment-avatar'>" + "\r\n" +
                                                        " <div class='comment-body'>" +
                                                        "   <div class='comment-by'>" + row.Linked_By_Name + "</div>" + "\r\n" +
                                                        "   <div class='comment-text'>" +
                                                            GetLayer(row.Doc_Layer) + "<a href='document.aspx?did=" + objURL.Encrypt(Convert.ToString(row.Document_Link_ID)) + "' target='_blank' class='media'>" +
                                                                row.Document_No + " - <b>" + fileSplits[fileSplits.Length - 1] + " - " + row.Entity_L2_Name + "</b>" +
                                                        "    </a>" + "\r\n" +
                                                        "   </div>" +
                                                        "   <div class='comment-actions'>&nbsp;" +
                                                        "     <small><span class='pull-right'>" + string.Format("{0:" + Constants.DateFormat + " HH:mm}", row.Linked_Date) + "</span></small>" + "\r\n" +
                                                        "   </div>\r\n" +
                                                        " </div>\r\n" +
                                                        "</div>";
                    LoadLinkedDocsSub(tbl, row.Linked_Document_ID, ref AttachmentsText, objSes, objURL);
                }
            }
        }

        private string GetLayer(int Doc_Layer)
        {
            string LayerString = "....";
            for (int i = 1; i < Doc_Layer; i++)
            {
                LayerString = LayerString + "\\....";
            }
            return LayerString;
        }

        private void LoadComments(DS_Documents.tbldocument_commentsDataTable tbl, SessionObject objSes, bool isEditable)
        {
            ltrHistory.Text = "";

            if (tbl.Rows.Count > 0)
            {
                N_Ter.Common.Common_Actions objCom = new N_Ter.Common.Common_Actions();

                int MonthNo = 0;
                int YearNo = 0;

                foreach (DS_Documents.tbldocument_commentsRow row in tbl.Select("", "Comment_Date DESC, Document_Comment_ID"))
                {
                    if (MonthNo != row.Comment_Date.Month || YearNo != row.Comment_Date.Year)
                    {
                        ltrHistory.Text = ltrHistory.Text + "<div class=\"tl-header now bg-primary\">\r\n" +
                                                                objCom.getMonth(row.Comment_Date.Month) + " " + row.Comment_Date.Year + "\r\n" +
                                                               "</div>\r\n";
                        MonthNo = row.Comment_Date.Month;
                        YearNo = row.Comment_Date.Year;
                    }
                    ltrHistory.Text = ltrHistory.Text + "<div class=\"tl-entry\">" + "\r\n" +
                                                        "   <div class=\"tl-time\">" + row.Comment_Date.Day + "|" + string.Format("{0:HH:mm}", row.Comment_Date) + "</div>" + "\r\n" +
                                                        "       <div class=\"tl-icon bg-success\">\r\n" +
                                                        "           <i class=\"fa fa-comments-o\"></i>\r\n" +
                                                        "       </div>" + "\r\n" +
                                                        "       <div class=\"panel tl-body p10 \">\r\n" +
                                                                    (isEditable ? "<button class='ttip btn btn-danger btn-sm btn-rounded history_delete' onclick='return DeleteComment(" + row.Document_Comment_ID + ");' title='Delete Comment' data-placement='left'><i class='fa fa-trash-o'></i></button>" + "\r\n" : "") +
                                                        "           <h5 class='text-warning mt'>" +
                                                        "               <img class=\"rounded\" src=\"" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "\" alt=\"Profile picture\" style=\"width: 20px;height: 20px;margin-top: -2px;\">" + "\r\n" +
                                                        "               <span class=\"text-info\">" + row.Commented_By_Name + "</span>\r\n" +
                                                        "           </h5>" +
                                                        "       <div class=\"well well-md p8 \" style=\"margin: 6px 0 0 0;\">" + row.Doc_Comment.Replace("\r\n", "</br>") + "</div>\r\n" +
                                                        "   </div>" + "\r\n" +
                                                        "</div>" + "\r\n";
                }
            }
        }

        private void LoadTagDetails(DS_Documents.tbldocument_tagsDataTable tblTags, int Document_Project_ID, SessionObject objSes, bool isEditable)
        {
            pnlDocumentTagData.Controls.Clear();

            N_Ter.Common.Common_Task_Actions objTskAct = new N_Ter.Common.Common_Task_Actions();

            Document_Projects objDP = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
            DS_Doc_Project ds = objDP.ReadWithLabels(Document_Project_ID);

            string strRequiredFieldValidation = "";
            Required_Fields = "function ValidatRequiredFields() {" + "\r\n" +
                                     "remove_field_erros();" + "\r\n" +
                                     "var ret = true;" + "\r\n";

            bool ControlsReadOnly = false;
            if (isEditable == false)
            {
                ControlsReadOnly = true;
            }

            N_Ter.Common.Script_Generator objScripts = new N_Ter.Common.Script_Generator();
            N_Ter.Customizable.Master_Tables objMasters = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);
            int ControlIndex = 0;
            List<string> Help_Texts = new List<string>();

            pnlDocumentTagData.Attributes.Add("class", "row");

            System.Web.UI.HtmlControls.HtmlGenericControl divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
            int rowWidth = 0;

            foreach (DS_Doc_Project.tbldocument_project_indexesRow row in ds.tbldocument_project_indexes)
            {
                pnlDocumentTagData.Controls.Add(objTskAct.GetDocObject(objScripts, IsPostBack, objMasters, tblTags, objSes.Currency_Sbl, row, ref objDPCtrlList, ref strRequiredFieldValidation, ref rowWidth, ControlIndex, "GetDocHelp", false, ControlsReadOnly));
                if (row.Help_Text.Trim() != "")
                {
                    Help_Texts.Add(row.Tag_Name + "|" + row.Help_Text);
                    ControlIndex++;
                }
                if (rowWidth == 12)
                {
                    pnlDocumentTagData.Controls.Add(divMainRowControl);
                    divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                    divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                    rowWidth = 0;
                }
            }
            if (rowWidth > 0)
            {
                pnlDocumentTagData.Controls.Add(divMainRowControl);
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

        private void LoadProjDocuments(int Document_Project_ID, SessionObject objSes)
        {
            Document_Projects objDocProj = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
            DS_Doc_Project dsDocProj = objDocProj.Read(Convert.ToInt32(Document_Project_ID));

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
            Doc_Project_ID = Document_Project_ID.ToString();
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            int Document_ID = Convert.ToInt32(ViewState["did"]);

            N_Ter.Common.Common_Task_Actions objCommAct = new N_Ter.Common.Common_Task_Actions();

            DS_Documents ds = objCommAct.PrepareDocControlsDataSet(Document_ID, objDPCtrlList, false);

            SessionObject objSes = (SessionObject)Session["dt"];
            Documents objDocs = ObjectCreator.GetDocuments(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            if (objDocs.UpdateTags(Document_ID, ds.tbldocument_tags) == true)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Document Tags Successfully Updated');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Database Error : Cannot save the Document Tags');", true);
            }
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
            string Document_ID = Convert.ToString(ViewState["did"]);
            Response.Redirect("document.aspx?did=" + objURL.Encrypt(Document_ID));
        }

        protected void cmdUpdateDoc_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Entity_L2_ID = Convert.ToInt32(ViewState["cid"]);
            DS_Entity_Level_2.tblentity_level_2Row drComp = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type).Read(Entity_L2_ID);

            int Document_ID = Convert.ToInt32(ViewState["did"]);
            Documents objDocs = ObjectCreator.GetDocuments(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            string PathPart = "\\" + drComp.Folder_Name + "\\Document_Repos\\" + DateTime.Today.Year + "\\" + DateTime.Today.Month;

            string fn = System.IO.Path.GetFileName(fulDocument.PostedFile.FileName);
            string[] fnSP = fn.Split('.');

            string SaveLocation = objSes.PhysicalRoot + "\\nter_app_uploads\\client_docs" + PathPart;
            string SaveLocationDB = "nter_app_uploads\\client_docs" + PathPart;
            if (System.IO.Directory.Exists(SaveLocation) == false)
            {
                System.IO.Directory.CreateDirectory(SaveLocation);
            }

            string NewFileName = Common_Actions.TimeStampForFileName();
            if (txtFileName.Text.Trim() != "")
            {
                NewFileName = txtFileName.Text.Trim() + "_" + Common_Actions.TimeStampForFileName();
            }
            else
            {
                NewFileName = fulDocument.FileName.Substring(0, fulDocument.FileName.LastIndexOf('.')) + "_" + NewFileName;
            }

            Common_Actions objComAct = new Common_Actions();
            NewFileName = objComAct.FormatFileName(NewFileName);
            SaveLocation = SaveLocation + "\\" + NewFileName + "." + fnSP[fnSP.Length - 1];
            SaveLocationDB = SaveLocationDB + "\\" + NewFileName + "." + fnSP[fnSP.Length - 1];
            try
            {
                fulDocument.PostedFile.SaveAs(SaveLocation);
                if (objDocs.UploadFile(Document_ID, SaveLocationDB, SaveLocation) == true)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('File Successfully Uploaded');", true);

                    lblDocPath.Text = NewFileName + "." + fnSP[fnSP.Length - 1];
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

        protected void cmdUpdateAccessLevel_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Doc_ID = Convert.ToInt32(ViewState["did"]);
            Documents objDocs = ObjectCreator.GetDocuments(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            if (objDocs.ChangeAccessLevel(Doc_ID, Convert.ToInt32(cboAccessLevel.SelectedItem.Value)) == true)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Access Level Successfully Updated');", true);

                lblAccessLevel.Text = "Level " + cboAccessLevel.SelectedItem.Value;
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Database Error : Cannot Update the Access Level');", true);
            }
        }

        protected void cmdSaveAddLinkhnd_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Doc_ID = Convert.ToInt32(ViewState["did"]);
            Documents objDocs = ObjectCreator.GetDocuments(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            if (Doc_ID != Convert.ToInt32(hndLinkDocID.Value))
            {
                ActionDone Act = objDocs.AddLink(Doc_ID, Convert.ToInt32(hndLinkDocID.Value), objSes.UserID);
                if (Act.Done == true)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('File Successfully Linked');", true);

                    DS_Documents ds = objDocs.Read(Doc_ID);
                    bool isEditable = Convert.ToBoolean(ViewState["isEdit"]);
                    LoadLinkedDocs(ds.tbldocument_links, objSes, isEditable);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + Act.Reason + " : Cannot Link the File');", true);
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('You cannot link the same file to itself');", true);
            }
        }

        protected void cmdSaveComment_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Doc_ID = Convert.ToInt32(ViewState["did"]);
            Documents objDocs = ObjectCreator.GetDocuments(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            if (objDocs.AddComment(Doc_ID, objSes.UserID, txtCommentMain.Text) == true)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Comment Successfully Added');", true);

                DS_Documents ds = objDocs.Read(Doc_ID);
                bool isEditable = Convert.ToBoolean(ViewState["isEdit"]);
                LoadComments(ds.tbldocument_comments, objSes, isEditable);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Database Error : Cannot Add the Comment');", true);
            }
        }

        protected void cmdDeleteDocLink_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int DelLink_ID = Convert.ToInt32(hndDeleteLink.Value);
            Documents objDocs = ObjectCreator.GetDocuments(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            DeleteReason del = objDocs.DeleteLink(DelLink_ID);
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Linked Document Successfully Deleted');", true);
                hndDeleteLink.Value = "0";

                int Doc_ID = Convert.ToInt32(ViewState["did"]);
                DS_Documents ds = objDocs.Read(Doc_ID);
                bool isEditable = Convert.ToBoolean(ViewState["isEdit"]);
                LoadLinkedDocs(ds.tbldocument_links, objSes, isEditable);
            }
        }

        protected void cmdDeleteComment_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Comment_ID = Convert.ToInt32(hndDeleteComment.Value);
            Documents objDocs = ObjectCreator.GetDocuments(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            DeleteReason del = objDocs.DeleteComment(Comment_ID);
            if (del.Deleted == false)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + del.Reason + "');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Document Comment Successfully Deleted');", true);
                hndDeleteLink.Value = "0";

                int Doc_ID = Convert.ToInt32(ViewState["did"]);
                DS_Documents ds = objDocs.Read(Doc_ID);
                bool isEditable = Convert.ToBoolean(ViewState["isEdit"]);
                LoadComments(ds.tbldocument_comments, objSes, isEditable);
            }
        }
    }
}