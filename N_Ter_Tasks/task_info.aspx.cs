using N_Ter.Base;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class task_info : System.Web.UI.Page
    {
        public string LoadingScripts = "";
        public string ChartScripts = "";
        public string Task_Script = "";
        private int MainControlsCount;
        System.Collections.ArrayList TagsList;

        public int RefFreq = 0;
        public int ListSort = 0;
        public bool ListSortDir = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            RefFreq = objSes.RefFreq;
            ListSort = objSes.ListSort;
            ListSortDir = objSes.ListSortDir;

            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            ((BoundField)gvDocumentsFind.Columns[3]).DataFormatString = "{0:" + Constants.DateFormat + " HH:mm}";

            if (IsPostBack == false)
            {
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                string taslIDtemp = Convert.ToString(objURL.Decrypt(Convert.ToString(Request.QueryString["tid"])));
                if (string.IsNullOrEmpty(taslIDtemp) == false)
                {
                    int Task_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["tid"])));
                    if (objTask.Can_Access_Task_View(Task_ID, objSes.UserID, objSes.isAdmin) == false)
                    {
                        Response.Redirect("no_access.aspx?");
                    }
                    else
                    {
                        ltrEL2.Text = objSes.EL2;
                        gvDocumentsFind.Columns[4].HeaderText = objSes.EL2 + " Name";
                        ViewState["tid"] = Task_ID;
                        hndTaskID.Value = Convert.ToString(Task_ID);
                    }
                }
                else
                {
                    Response.Redirect("error.aspx");
                }
            }

            MainControlsCount = 0;

            LoadTask(objTask, objSes);

            if (MainControlsCount == 0)
            {
                divMainControls.Visible = false;
            }

            cmdSaveComment.Attributes.Add("onClick", "return ValidateComment('" + txtCommentMain.ClientID + "');");
            cmdAddDocComment.Attributes.Add("onClick", "return ValidateComment('" + txtDocComment.ClientID + "');");
            hndMessageMyID.Value = Convert.ToString(objSes.UserID);
            cmdSendMessage.Attributes.Add("onClick", "javascript:return ValidateTaskMessage('" + hndMessageThreadID.ClientID + "', '" + cboMessageUsers.ClientID + "', '" + txtTitle.ClientID + "', '" + txtMessage.ClientID + "')");
            cboDocCategories.Attributes.Add("onChange", "LoadDocuments()");
            cboDocType.Attributes.Add("onChange", "LoadDocuments()");
            cboTags.Attributes.Add("onChange", "LoadTagdComments();");
        }

        private void LoadFileTypes(DS_Workflow.tblwalkflowRow dr)
        {
            cboFileTypesUpload.Items.Clear();
            cboDocCategories.Items.Clear();
            string[] strTypes = dr.Workflow_Doc_Types.Split('|');

            if (strTypes.Length > 0)
                foreach (string str in strTypes)
                {
                    if (str.Trim() != "")
                    {
                        cboFileTypesUpload.Items.Add(new ListItem(str, str));
                        cboDocCategories.Items.Add(new ListItem(str, str));
                    }
                }

            cboFileTypesUpload.Items.Insert(cboFileTypesUpload.Items.Count - 1 < 0 ? 0 : cboFileTypesUpload.Items.Count, new ListItem("Other Documents", "Other Documents"));
            cboDocCategories.Items.Insert(cboDocCategories.Items.Count - 1 < 0 ? 0 : cboDocCategories.Items.Count, new ListItem("Other Documents", "Other Documents"));
            cboDocCategories.Items.Insert(0, new ListItem("[All Document Categories]", "0"));
            cboDocCategories.Items.Add(new ListItem("Attachments", "Attachments"));
        }

        private void LoadCommentTypes(DS_Workflow.tblwalkflowRow dr)
        {
            cboCommentType.Items.Clear();
            cboDocCommentType.Items.Clear();
            string[] strTypes = dr.Workflow_Comment_Types.Split('|');

            if (strTypes.Length > 0)
                foreach (string str in strTypes)
                {
                    if (str.Trim() != "")
                    {
                        cboCommentType.Items.Add(new ListItem(str, str));
                        cboDocCommentType.Items.Add(new ListItem(str, str));
                    }
                }

            cboCommentType.Items.Insert(cboCommentType.Items.Count - 1 < 0 ? 0 : cboCommentType.Items.Count, new ListItem("Other Comments", "Other Comments"));
            cboDocCommentType.Items.Insert(cboDocCommentType.Items.Count - 1 < 0 ? 0 : cboDocCommentType.Items.Count, new ListItem("Other Comments", "Other Comments"));
        }

        private void LoadTask(Tasks objTask, SessionObject objSes)
        {
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            DS_Tasks dsTasks = objTask.Read(Task_ID, false, false, false);
            DS_Tasks dsSubs = objTask.GetActiveSubTasks(Task_ID);

            if (dsTasks.tbltasks.Rows.Count > 0)
            {
                Users objUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
                if (dsTasks.tbltasks[0].Current_Task_User > 0 && dsTasks.tbltasks[0].Current_Task_User != objSes.UserID)
                {
                    divLocked.Visible = true;
                    DS_Users dsLockedUser = objUsers.Read(dsTasks.tbltasks[0].Current_Task_User);
                    if (dsLockedUser.tblusers.Rows.Count > 0)
                    {
                        ltrLockedUser.Text = dsLockedUser.tblusers[0].First_Name + " " + dsLockedUser.tblusers[0].Last_Name;
                    }
                    else
                    {
                        divLocked.Visible = false;
                    }
                }
                else
                {
                    divLocked.Visible = false;
                }
                Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                DS_Workflow dsWF = objWF.Read(dsTasks.tbltasks[0].Walkflow_ID, false, false);
                if (dsWF.tblwalkflow[0].Document_Project_ID > 0)
                {
                    pnlAddDocLink.Visible = true;
                    hndLinkDocID_ModalPopupExtender.Enabled = true;
                    lnkDocLink.Visible = true;
                    lnkDocLinkR.Visible = true;
                    LoadProjDocuments(dsWF.tblwalkflow[0].Document_Project_ID, objSes);
                    LoadingScripts = LoadingScripts + "ArrangeGrids();" + "\r\n";
                }
                else
                {
                    pnlAddDocLink.Visible = false;
                    lnkDocLink.Visible = false;
                    lnkDocLinkR.Visible = false;
                    hndLinkDocID_ModalPopupExtender.Enabled = false;
                }
                lblTaskNo.Text = dsTasks.tbltasks[0].Task_Number;
                if (dsTasks.tbltasks[0].Queue_ID == 0)
                {
                    lblQueue.Text = "General Queue";
                }
                else
                {
                    DS_Task_Queues dsQ = ObjectCreator.GetTask_Queues(objSes.Connection, objSes.DB_Type).Read(dsTasks.tbltasks[0].Queue_ID);
                    if (dsQ.tbltask_queues.Rows.Count > 0)
                    {
                        lblQueue.Text = dsQ.tbltask_queues[0].Queue_Name;
                    }
                    else
                    {
                        lblQueue.Text = "";
                    }
                }
                lblTaskNumber.Text = " : " + dsTasks.tbltasks[0].Task_Number + (dsTasks.tbltasks[0].isFlagged ? "&nbsp;&nbsp;<i class='fa fa-flag text-danger'></i>" : "");
                lblWorkflow.Text = dsTasks.tbltasks[0].Workflow_Name;
                if (dsTasks.tbltasks[0].Creator_On_Behalf_ID == 0 || dsTasks.tbltasks[0].Creator_ID == dsTasks.tbltasks[0].Creator_On_Behalf_ID)
                {
                    lblTaskCreator.Text = dsTasks.tbltasks[0].Created_By;
                }
                else
                {
                    DS_Users dsOnBehalf = objUsers.Read(dsTasks.tbltasks[0].Creator_On_Behalf_ID);
                    lblTaskCreator.Text = dsOnBehalf.tblusers[0].First_Name + " " + dsOnBehalf.tblusers[0].Last_Name + " (on behalf of " + dsTasks.tbltasks[0].Created_By + ")";
                }
                lblEL2Name.Text = dsTasks.tbltasks[0].Display_Name;
                lblCurrentStatus.Text = (dsTasks.tbltasks[0].Current_Step_ID == -1 ? "Completed" : (dsTasks.tbltasks[0].Current_Step_ID == -2 ? "Cancelled" : (dsTasks.tbltasks[0].IsStep_StatusNull() ? "Unknown" : dsTasks.tbltasks[0].Step_Status)));
                lblTaskDate.Text = string.Format("{0:" + Constants.DateFormat + " HH:mm}", dsTasks.tbltasks[0].Task_Date);
                lblDueDate.Text = dsTasks.tbltasks[0].IsETB_ValueNull() ? "" : string.Format("{0:" + Constants.DateFormat + " HH:mm}", dsTasks.tbltasks[0].ETB_Value);

                FillTaskData(Task_ID, ref dsTasks, dsWF, objTask);

                Common_Task_Actions objCommAct = new Common_Task_Actions();
                if (dsTasks.tbltask_timeline.Count == 0)
                {
                    objCommAct.GenerateTaskTimeLine(ref dsTasks, objWF);
                    objTask.UpdateTaskTimeline(Task_ID, dsTasks.tbltask_timeline);
                }

                LoadAttachments(dsTasks.tbltask_docs, dsTasks.tbltask_attachment, dsTasks.tbltask_comments, objSes);
                LoadHistory(dsTasks, dsSubs, objSes);
                LoadTaskTimelineChart(dsTasks, objSes);
                LoadExtraFeild(dsTasks.tbltasks[0].Task_ID, objSes);
                LoadSubTasks(dsSubs, Task_ID, dsTasks.tbltasks[0].Task_Number + " - " + dsTasks.tbltasks[0].Workflow_Name + " - " + dsTasks.tbltasks[0].Display_Name);
                LoadTaskMessages(dsTasks.tbltasks[0].Walkflow_ID, dsTasks.tbltasks[0].Entity_L2_ID, dsTasks.tbltasks[0].Task_ID, objSes);
            }
            else
            {
                Response.Redirect("error.aspx?");
            }
        }

        private void LoadSubTasks(DS_Tasks ds, int ParentTask_ID, string ParentTaskDetails)
        {
            if (ds.tbltasks.Count > 0)
            {
                ltrSubTaskCount.Text = ds.tbltasks.Count + " Sub Tasks Attached";
                cmdOpenSub.Visible = true;
                hndSubStack.Value = "";
                hndParentForSubs.Value = ParentTask_ID.ToString();
                cmdOpenSub.Attributes.Add("onClick", "return clearSubStack(" + ViewState["tid"] + ", '" + ParentTaskDetails + "');");
            }
            else
            {
                cmdOpenSub.Visible = false;
            }
        }

        private void LoadProjDocuments(int Document_Project_ID, SessionObject objSes)
        {
            if (IsPostBack == false)
            {
                Document_Projects objDocProj = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
                DS_Doc_Project dsDocProj = objDocProj.Read(Document_Project_ID);
                int intTagCount = 0;
                foreach (DS_Doc_Project.tbldocument_project_indexesRow row in dsDocProj.tbldocument_project_indexes)
                {
                    BoundField col = new BoundField();
                    col.HeaderText = row.Tag_Name;
                    col.DataField = "doctag_" + row.Document_Project_Index_ID;
                    gvDocumentsFind.Columns.Add(col);
                    intTagCount++;
                    if (intTagCount > 2)
                    {
                        break;
                    }
                }
            }

            Documents objDoc = ObjectCreator.GetDocuments(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            DS_Documents ds = objDoc.ReadAll(Document_Project_ID, objSes.UserID, objSes.AccLevel);
            gvDocumentsFind.DataSource = ds.tbldocuments;
            gvDocumentsFind.DataBind();
            if (ds.tbldocuments.Rows.Count > 0)
            {
                gvDocumentsFind.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void LoadExtraFeild(int Task_ID, SessionObject objSes)
        {
            Tasks objTK = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
            string[] arr = objTK.ReadExtraField(Task_ID, true);

            if (!string.IsNullOrEmpty(arr[0].Trim(' ')))
            {
                lblExtraFieldName.Text = arr[0];
                lblExtraFieldValue.Text = arr[1];

                divExtraField.Visible = true;
            }
            else
            {
                lblExtraFieldName.Text = string.Empty;
                lblExtraFieldValue.Text = string.Empty;

                divExtraField.Visible = false;
            }

            if (!string.IsNullOrEmpty(arr[2].Trim(' ')))
            {
                lblExtraField2Name.Text = arr[2];
                lblExtraField2Value.Text = arr[3];

                divExtraField2.Visible = true;
            }
            else
            {
                lblExtraField2Name.Text = string.Empty;
                lblExtraField2Value.Text = string.Empty;

                divExtraField2.Visible = false;
            }
        }

        private void LoadAttachments(DS_Tasks.tbltask_docsDataTable tbl, DS_Tasks.tbltask_attachmentDataTable tblAtt, DS_Tasks.tbltask_commentsDataTable tblComms, SessionObject objSes)
        {
            ltrAttachments.Text = "";
            string[] fileSplits;
            int FileCommentCount = 0;
            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();

            List<DS_Tasks.tbltask_docsRow> drDocs = tbl.Where(x => x.Is_Result == false && (x.Uploaded_By == objSes.UserID || x.Access_Level >= objSes.AccLevel)).ToList();
            List<DS_Tasks.tbltask_attachmentRow> drAtts = tblAtt.Where(x => x.Is_Result == false && (x.Attached_By == objSes.UserID || x.Access_Level >= objSes.AccLevel)).ToList();

            if (objSes.TaskDocDir)
            {
                drDocs = drDocs.Where(x => x.Is_Result == false).OrderBy(y => y.Task_Doc_Type).ThenBy(z => z.Uploaded_Date).ToList();
                drAtts = drAtts.Where(x => x.Is_Result == false).OrderBy(y => y.Created_Date).ToList();
            }
            else
            {
                drDocs = drDocs.Where(x => x.Is_Result == false).OrderBy(y => y.Task_Doc_Type).ThenByDescending(z => z.Uploaded_Date).ToList();
                drAtts = drAtts.Where(x => x.Is_Result == false).OrderByDescending(y => y.Created_Date).ToList();
            }

            ltrTaskDocCount.Text = "(" + Convert.ToString(drDocs.Count + drAtts.Count) + ")";

            if (drDocs.Count > 0)
            {
                string CurrDocType = string.Empty;
                foreach (DS_Tasks.tbltask_docsRow row in drDocs)
                {
                    if (row.Task_Doc_Type.Trim() != CurrDocType)
                    {
                        ltrAttachments.Text = ltrAttachments.Text + "<div class='comment no-padding-b'>" + "\r\n" +
                                                                    " <div class='comment-body no-margin-hr'>" +
                                                                    "   <div class='comment-text'>" +
                                                                    "    <b>" + row.Task_Doc_Type + "</b>" +
                                                                    "   </div>" + " </div>\r\n" +
                                                                    "</div>";
                        CurrDocType = row.Task_Doc_Type.Trim();
                    }
                    FileCommentCount = tblComms.Where(y => y.Task_Doc_ID == row.Task_Doc_ID).Count();
                    fileSplits = row.Doc_Path.Split('\\');
                    ltrAttachments.Text = ltrAttachments.Text + " <div class='comment'><img src='" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "' alt='Profile Picture' class='comment-avatar'>" + "\r\n" +
                                                                "<div class='comment-body'>" +
                                                                   (row.isParentRecord ? "<div class='ttip badge badge-primary pull-right' title='From Parent Task' data-placement='left'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                                   (row.isChildRecord ? "<div class='ttip badge badge-info pull-right' title='From Sub Task' data-placement='left'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                                "  <div class='comment-by'>" + row.Uploaded_By_Name + "</div>" + "\r\n" +
                                                                "  <div class='comment-text'>" +
                                                                "    <button class='ttip btn btn-primary btn-sm btn-rounded' onclick='return showDocComments(" + row.Task_Doc_ID + ");' data-placement='top' title='Comments'><i class='fa fa-comment'></i>" + (FileCommentCount > 0 ? "<span class='badge badge-pill badge_small'>" + FileCommentCount + "</span>" : "") + "</button>&nbsp;<a href='document_preview.aspx?fid=" + objURL.Encrypt(Convert.ToString(row.Task_Doc_ID)) + "' target='_blank' class='media'>" +
                                                                       row.Doc_Number + (row.Is_Re_Upload ? " - (R) " : "") + " - <b>" + fileSplits[fileSplits.Length - 1] + "</b>" +
                                                                "    </a>" + "\r\n" +
                                                                "  </div>" +
                                                                "  <div class='comment-actions'>&nbsp;" +
                                                                "     <span class='pull-right'><small class='text-muted'>" + string.Format("{0:" + Constants.DateFormat + " HH:mm}", row.Uploaded_Date) + "</small></span>" + "\r\n" +
                                                                "  </div>\r\n" +
                                                                "</div> \r\n" +
                                                               "</div>\r\n";
                }
            }

            if (drAtts.Count > 0)
            {
                ltrAttachments.Text = ltrAttachments.Text + "<div class='comment no-padding-b'>" + "\r\n" +
                                                            " <div class='comment-body no-margin-hr'>" +
                                                            "   <div class='comment-text'>" +
                                                            "    <b>Attachments</b>" +
                                                            "   </div>" + " </div>\r\n" +
                                                            "</div>";
                foreach (DS_Tasks.tbltask_attachmentRow row in drAtts)
                {
                    FileCommentCount = tblComms.Where(y => y.Task_Attachment_ID == row.Task_Doc_ID).Count();
                    fileSplits = row.Document_Path.Split('\\');
                    ltrAttachments.Text = ltrAttachments.Text + "<div class='comment'>" + "\r\n" +
                                                             " <img src='" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "' alt='Profile Picture' class='comment-avatar'>" + "\r\n" +
                                                             " <div class='comment-body'>" +
                                                                 (row.isParentRecord ? "<div class='ttip badge badge-primary pull-right' title='From Parent Task' data-placement='left'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                                 (row.isChildRecord ? "<div class='ttip badge badge-info pull-right' title='From Sub Task' data-placement='left'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                             "   <div class='comment-by'>" + row.Attached_By_Name + "</div>" + "\r\n" +
                                                             "   <div class='comment-text'>" +
                                                             "    <button class='ttip btn btn-primary btn-sm btn-rounded' onclick='return showAttachComments(" + row.Task_Doc_ID + ");' data-placement='top' title='Comments'><i class='fa fa-comment'></i>" + (FileCommentCount > 0 ? "<span class='badge badge-pill badge_small'>" + FileCommentCount + "</span>" : "") + "</button>&nbsp;<button class='ttip btn btn-success btn-sm btn-rounded' onclick='return showAttachInfo(" + row.Document_ID + ");' data-placement='top' title='Document Info'><i class='fa fa-info'></i></button>&nbsp;<a href='document_preview_dp.aspx?fid=" + objURL.Encrypt(Convert.ToString(row.Document_ID)) + "' target='_blank' class='media'>" +
                                                                    row.Document_No + " - <b>" + fileSplits[fileSplits.Length - 1] + "</b>" +
                                                             "    </a>" + "\r\n" +
                                                             "   </div>" +
                                                             "   <div class='comment-actions'>&nbsp;" +
                                                             "      <span class='pull-right'><small class='text-muted'>" + string.Format("{0:" + Constants.DateFormat + " HH:mm}", row.Created_Date) + "</small></span>" + "\r\n" +
                                                             "   </div>\r\n" +
                                                             " </div>\r\n" +
                                                             "</div>";
                }
            }

            ltrAttachmentsR.Text = "";

            drDocs = tbl.Where(x => x.Is_Result && (x.Uploaded_By == objSes.UserID || x.Access_Level >= objSes.AccLevel)).ToList();
            drAtts = tblAtt.Where(x => x.Is_Result && (x.Attached_By == objSes.UserID || x.Access_Level >= objSes.AccLevel)).ToList();

            if (objSes.TaskDocDir)
            {
                drDocs = drDocs.Where(x => x.Is_Result).OrderBy(y => y.Task_Doc_Type).ThenBy(z => z.Uploaded_Date).ToList();
                drAtts = drAtts.Where(x => x.Is_Result).OrderBy(y => y.Created_Date).ToList();
            }
            else
            {
                drDocs = drDocs.Where(x => x.Is_Result).OrderBy(y => y.Task_Doc_Type).ThenByDescending(z => z.Uploaded_Date).ToList();
                drAtts = drAtts.Where(x => x.Is_Result).OrderByDescending(y => y.Created_Date).ToList();
            }

            ltrTaskDocCountR.Text = "(" + Convert.ToString(drDocs.Count + drAtts.Count) + ")";

            if (drDocs.Count > 0)
            {
                string CurrDocType = string.Empty;
                foreach (DS_Tasks.tbltask_docsRow row in drDocs)
                {
                    if (row.Task_Doc_Type.Trim() != CurrDocType)
                    {
                        ltrAttachmentsR.Text = ltrAttachmentsR.Text + "<div class='comment no-padding-b'>" + "\r\n" +
                                                                    " <div class='comment-body no-margin-hr'>" +
                                                                    "   <div class='comment-text'>" +
                                                                    "    <b>" + row.Task_Doc_Type + "</b>" +
                                                                    "   </div>" + " </div>\r\n" +
                                                                    "</div>";
                        CurrDocType = row.Task_Doc_Type.Trim();
                    }
                    FileCommentCount = tblComms.Where(y => y.Task_Doc_ID == row.Task_Doc_ID).Count();
                    fileSplits = row.Doc_Path.Split('\\');
                    ltrAttachmentsR.Text = ltrAttachmentsR.Text + " <div class='comment'><img src='" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "' alt='Profile Picture' class='comment-avatar'>" + "\r\n" +
                                                                "<div class='comment-body'>" +
                                                                   (row.isParentRecord ? "<div class='ttip badge badge-primary pull-right' title='From Parent Task' data-placement='left'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                                   (row.isChildRecord ? "<div class='ttip badge badge-info pull-right' title='From Sub Task' data-placement='left'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                                "  <div class='comment-by'>" + row.Uploaded_By_Name + "</div>" + "\r\n" +
                                                                "  <div class='comment-text'>" +
                                                                "    <button class='ttip btn btn-primary btn-sm btn-rounded' onclick='return showDocComments(" + row.Task_Doc_ID + ");' data-placement='top' title='Comments'><i class='fa fa-comment'></i>" + (FileCommentCount > 0 ? "<span class='badge badge-pill badge_small'>" + FileCommentCount + "</span>" : "") + "</button>&nbsp;<a href='document_preview.aspx?fid=" + objURL.Encrypt(Convert.ToString(row.Task_Doc_ID)) + "' target='_blank' class='media'>" +
                                                                       row.Doc_Number + (row.Is_Re_Upload ? " - (R) " : "") + " - <b>" + fileSplits[fileSplits.Length - 1] + "</b>" +
                                                                "    </a>" + "\r\n" +
                                                                "  </div>" +
                                                                "  <div class='comment-actions'>&nbsp;" +
                                                                "     <span class='pull-right'><small class='text-muted'>" + string.Format("{0:" + Constants.DateFormat + " HH:mm}", row.Uploaded_Date) + "</small></span>" + "\r\n" +
                                                                " </div>\r\n" +
                                                                "</div> \r\n" +
                                                               "</div>\r\n";
                }
            }

            if (drAtts.Count > 0)
            {
                ltrAttachmentsR.Text = ltrAttachmentsR.Text + "<div class='comment no-padding-b'>" + "\r\n" +
                                                            " <div class='comment-body no-margin-hr'>" +
                                                            "   <div class='comment-text'>" +
                                                            "    <b>Attachments</b>" +
                                                            "   </div>" + " </div>\r\n" +
                                                            "</div>";
                foreach (DS_Tasks.tbltask_attachmentRow row in drAtts)
                {
                    FileCommentCount = tblComms.Where(y => y.Task_Attachment_ID == row.Task_Doc_ID).Count();
                    fileSplits = row.Document_Path.Split('\\');
                    ltrAttachmentsR.Text = ltrAttachmentsR.Text + "<div class='comment'>" + "\r\n" +
                                                             " <img src='" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "' alt='Profile Picture' class='comment-avatar'>" + "\r\n" +
                                                             " <div class='comment-body'>" +
                                                                (row.isParentRecord ? "<div class='ttip badge badge-primary pull-right' title='From Parent Task' data-placement='left'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                                (row.isChildRecord ? "<div class='ttip badge badge-info pull-right' title='From Sub Task' data-placement='left'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                             "  <div class='comment-by'>" + row.Attached_By_Name + "</div>" + "\r\n" +
                                                             "  <div class='comment-text'>" +
                                                             "    <button class='ttip btn btn-primary btn-sm btn-rounded' onclick='return showAttachComments(" + row.Task_Doc_ID + ");' data-placement='top' title='Comments'><i class='fa fa-comment'></i>" + (FileCommentCount > 0 ? "<span class='badge badge-pill badge_small'>" + FileCommentCount + "</span>" : "") + "</button>&nbsp;<button class='ttip btn btn-success btn-sm btn-rounded' onclick='return showAttachInfo(" + row.Document_ID + ");' data-placement='top' title='Document Info'><i class='fa fa-info'></i></button>&nbsp;<a href='document_preview_dp.aspx?fid=" + objURL.Encrypt(Convert.ToString(row.Document_ID)) + "' target='_blank' class='media'>" +
                                                                    row.Document_No + " - <b>" + fileSplits[fileSplits.Length - 1] + "</b>" +
                                                             "    </a>" + "\r\n" +
                                                             "  </div>" +
                                                             "  <div class='comment-actions'>&nbsp;" +
                                                             "     <span class='pull-right'><small class='text-muted'>" + string.Format("{0:" + Constants.DateFormat + " HH:mm}", row.Created_Date) + "</small></span>" + "\r\n" +
                                                             "  </div>\r\n" +
                                                             " </div>\r\n" +
                                                             "</div>";
                }
            }
        }

        private void LoadHistory(DS_Tasks dsTask, DS_Tasks dsSubs, SessionObject objSes)
        {
            N_Ter.Common.Common_Actions objCom = new N_Ter.Common.Common_Actions();
            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();

            List<TaskHistoryItem> HistList = new List<TaskHistoryItem>();

            string TaskUpdateFields = "";
            string UserDetails = "";
            string HistoryText = "";
            string SubTasksList = "";
            string SubTaskOwnerName = "";
            string SubTaskOwnerImage = "";
            int SubTaskOwnerID = 0;
            int WeekEndDayCount = 0;
            DateTime SubTaskDate = DateTime.Today.Date;

            List<DS_Tasks.tbltasksRow> drSubs;
            List<DS_Tasks.tbltask_history_durationsRow> drTimeSlots;
            List<DS_Tasks.tbltask_update_fieldsRow> drFields;
            List<DS_Tasks.tbltask_update_fieldsRow> drUserFields;
            List<DS_Tasks.tbltask_update_addon_fieldsRow> drAddonFields;
            List<int> UpdateUserIDs;

            N_Ter.Common.Common_Task_Actions objCommAct = new N_Ter.Common.Common_Task_Actions();

            int[] Workflow_Step_IDs = dsTask.tbltask_history.Select(x => x.Workflow_Step_ID).Distinct().ToArray();
            DS_Workflow dsWFStepCats = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).ReadFieldStepCats(Workflow_Step_IDs);

            Users objUser = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);

            if (objUser.isAllowed(AllowedAreas.Task_Script_Extract, objSes.UserID))
            {
                N_Ter.Common.Script_Generator objScripts = new N_Ter.Common.Script_Generator();
                Task_Script = objScripts.TaskScript(dsTask.tbltasks[0].Task_ID);
                MainControlsCount++;
            }
            else
            {
                lstTaskScript.Visible = false;
            }
            foreach (DS_Tasks.tbltask_historyRow row in dsTask.tbltask_history)
            {
                TaskUpdateFields = "";
                drFields = dsTask.tbltask_update_fields.Where(x => x.Task_Update_ID == row.Task_Update_ID && x.Show_In_History == true).ToList();
                if (drFields.Count > 0)
                {
                    UpdateUserIDs = drFields.Select(y => y.User_ID).Distinct().ToList();
                    if (UpdateUserIDs.Count > 1)
                    {
                        TaskUpdateFields = "";

                        DS_Users dsUsrs = objUser.Read(UpdateUserIDs);
                        List<DS_Users.tblusersRow> drUsr;
                        foreach (int User_ID in UpdateUserIDs)
                        {
                            drUsr = dsUsrs.tblusers.Where(u => u.User_ID == User_ID).ToList();
                            if (drUsr.Count > 0)
                            {
                                TaskUpdateFields = TaskUpdateFields + "<div class=\"mt10 mb5 text-info\"><img src=\"" + (drUsr[0].IsImage_PathNull() ? "assets/images/user.png" : drUsr[0].Image_Path) + "\" alt=\"\" style=\"width:20px;height:20px;\" class=\"rounded\">&nbsp;&nbsp;" + drUsr[0].First_Name + " " + drUsr[0].Last_Name + "</div>";
                            }
                            else
                            {
                                TaskUpdateFields = TaskUpdateFields + "<div class=\"mt10 mb5 text-info\"><img src=\"assets/images/user.png\" alt=\"\" style=\"width:20px;height:20px;\" class=\"rounded\">&nbsp;&nbsp;Unknown User</div>";
                            }

                            drUserFields = drFields.Where(z => z.User_ID == User_ID).ToList();
                            TaskUpdateFields = TaskUpdateFields + objCommAct.GetTaskUpdateForHistory(drUserFields, dsWFStepCats.tblworkflow_step_field_cats.Where(x => x.Workflow_Step_ID == row.Workflow_Step_ID).ToList(), objSes);
                        }
                    }
                    else
                    {
                        TaskUpdateFields = objCommAct.GetTaskUpdateForHistory(drFields, dsWFStepCats.tblworkflow_step_field_cats.Where(x => x.Workflow_Step_ID == row.Workflow_Step_ID).ToList(), objSes);
                    }
                }

                if (!row.IsStep_Completed_By_NameNull())
                {
                    UserDetails = "<h5 class='text-warning mt'><img class=\"rounded\" src=\"" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "\" alt=\"Profile picture\" style=\"width: 20px;height: 20px;margin-top: -2px;\">" + "\r\n" +
                                  "<span class=\"text-info\">" + row.Step_Completed_By_Name + "</span></h5>" + "\r\n";
                }
                else
                {
                    UserDetails = "";
                }

                if (row.IsStep_Finished_DateNull())
                {
                    if (row.Posted_Date.AddDays(2) < DateTime.Now)
                    {
                        WeekEndDayCount = Enumerable.Range(0, (int)((DateTime.Today.Date.AddDays(-1) - row.Posted_Date.Date.AddDays(1)).TotalDays) + 1).Select(n => row.Posted_Date.Date.AddDays(1).AddDays(n)).Where(x => x.DayOfWeek == DayOfWeek.Saturday || x.DayOfWeek == DayOfWeek.Sunday).Count();
                        row.Waiting_Hours = (DateTime.Now - row.Posted_Date).TotalHours - (WeekEndDayCount * 24);
                    }
                    else
                    {
                        row.Waiting_Hours = (DateTime.Now - row.Posted_Date).TotalHours;
                    }
                }
                else
                {
                    if (row.Posted_Date.AddDays(2) < row.Step_Finished_Date)
                    {
                        WeekEndDayCount = Enumerable.Range(0, (int)((row.Step_Finished_Date.Date.AddDays(-1) - row.Posted_Date.Date.AddDays(1)).TotalDays) + 1).Select(n => row.Posted_Date.Date.AddDays(1).AddDays(n)).Where(x => x.DayOfWeek == DayOfWeek.Saturday || x.DayOfWeek == DayOfWeek.Sunday).Count();
                        row.Waiting_Hours = (row.Step_Finished_Date - row.Posted_Date).TotalHours - (WeekEndDayCount * 24);
                    }
                    else
                    {
                        row.Waiting_Hours = (row.Step_Finished_Date - row.Posted_Date).TotalHours;
                    }
                }

                drTimeSlots = dsTask.tbltask_history_durations.Where(x => x.Task_Update_ID == row.Task_Update_ID).ToList();
                row.Task_Hours = 0;
                foreach (DS_Tasks.tbltask_history_durationsRow drTimeSlot in drTimeSlots)
                {
                    if (drTimeSlot.IsEnd_Date_TimeNull())
                    {
                        if (drTimeSlot.Start_Date_Time.AddDays(2) < DateTime.Now)
                        {
                            WeekEndDayCount = Enumerable.Range(0, (int)((DateTime.Today.Date.Date.AddDays(-1) - drTimeSlot.Start_Date_Time.AddDays(1)).TotalDays) + 1).Select(n => drTimeSlot.Start_Date_Time.Date.AddDays(1).AddDays(n)).Where(x => x.DayOfWeek == DayOfWeek.Saturday || x.DayOfWeek == DayOfWeek.Sunday).Count();
                            row.Task_Hours = row.Task_Hours + (DateTime.Now - drTimeSlot.Start_Date_Time).TotalHours - (WeekEndDayCount * 24);
                        }
                        else
                        {
                            row.Task_Hours = row.Task_Hours + (DateTime.Now - drTimeSlot.Start_Date_Time).TotalHours;
                        }
                    }
                    else
                    {
                        if (drTimeSlot.Start_Date_Time.AddDays(2) < drTimeSlot.End_Date_Time)
                        {
                            WeekEndDayCount = Enumerable.Range(0, (int)((drTimeSlot.End_Date_Time.Date.AddDays(-1) - drTimeSlot.Start_Date_Time.AddDays(1)).TotalDays) + 1).Select(n => drTimeSlot.Start_Date_Time.Date.AddDays(1).AddDays(n)).Where(x => x.DayOfWeek == DayOfWeek.Saturday || x.DayOfWeek == DayOfWeek.Sunday).Count();
                            row.Task_Hours = row.Task_Hours + (drTimeSlot.End_Date_Time - drTimeSlot.Start_Date_Time).TotalHours - (WeekEndDayCount * 24);
                        }
                        else
                        {
                            row.Task_Hours = row.Task_Hours + (drTimeSlot.End_Date_Time - drTimeSlot.Start_Date_Time).TotalHours;
                        }
                    }
                }

                row.Waiting_Hours = row.Waiting_Hours - row.Task_Hours;
                TimeSpan WaitingHours = TimeSpan.FromHours(row.Waiting_Hours);
                TimeSpan TaskHours = TimeSpan.FromHours(row.Task_Hours);

                HistoryText = "<div class=\"tl-entry\">" + "\r\n" +
                                    "<div class=\"tl-time\">" + row.Posted_Date.Day + "|" + string.Format("{0:HH:mm}", row.Posted_Date) + "</div>" + "\r\n" +
                                    "<div class=\"tl-icon bg-dark-gray\">" + "\r\n" +
                                        "<i class=\"fa fa-check\"></i>" + "\r\n" +
                                    "</div>" + "\r\n" +
                                    "<div class='panel tl-body p8'>" + "\r\n" +
                                        (row.isParentRecord ? "<div class='history_parent ttip badge badge-primary' title='From Parent Task' data-placement='right'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                        UserDetails +
                                        "<div class='well well-md p8' style='margin: 0;'><b>" + row.Step_Status + "</b><br/>\r\n" +
                                        TaskUpdateFields.Replace("\r\n", "</br>") + "</div>\r\n" +
                                        "<div class='padding-sm-hr padding-xs-vr text-info'>Inactive Time : <b>" + WaitingHours.Days + " Days, " + WaitingHours.Hours + " Hrs, " + WaitingHours.Minutes + " Mns" + "</b></br>Active Time : <b>" + TaskHours.Days + " Days, " + TaskHours.Hours + " Hrs, " + TaskHours.Minutes + " Mns" + "</b></div>" + "\r\n" +
                                    "</div>" + "\r\n" +
                                "</div>" + "\r\n";
                TaskHistoryItem HistItem = new TaskHistoryItem();
                HistItem.ItemDate = row.Posted_Date;
                HistItem.ItemCode = HistoryText;
                HistList.Add(HistItem);

                drSubs = dsSubs.tbltasks.Where(x => x.Started_History_ID == row.Task_Update_ID).OrderBy(y => y.Creator_ID).ThenBy(z => z.Task_Date).ToList();
                if (drSubs.Count > 0)
                {
                    SubTasksList = "";
                    SubTaskOwnerID = drSubs[0].Creator_ID;
                    SubTaskOwnerName = drSubs[0].Created_By;
                    SubTaskOwnerImage = drSubs[0].IsCreated_By_ImageNull() ? "assets/images/user.png" : drSubs[0].Created_By_Image;

                    foreach (DS_Tasks.tbltasksRow drSubTask in drSubs)
                    {
                        if (drSubTask.Creator_ID != SubTaskOwnerID)
                        {
                            UserDetails = "<h5 class='text-warning mt'><img class=\"rounded\" src=\"" + SubTaskOwnerImage + "\" alt=\"Profile picture\" style=\"width: 20px;height: 20px;margin-top: -2px;\">" + "\r\n" +
                                              "<span class=\"text-info\">" + SubTaskOwnerName + "</span></h5>" + "\r\n";

                            HistoryText = "<div class=\"tl-entry\">" + "\r\n" +
                                                "<div class=\"tl-time\">" + SubTaskDate.Day + "|" + string.Format("{0:HH:mm}", SubTaskDate) + "</div>" + "\r\n" +
                                                "<div class=\"tl-icon bg-dark-gray\">" + "\r\n" +
                                                    "<i class=\"fa fa-sitemap\"></i>" + "\r\n" +
                                                "</div>" + "\r\n" +
                                                "<div class='panel tl-body p8'>" + "\r\n" +
                                                    UserDetails +
                                                    "<div class='well well-md p8' style='margin: 0;'><b>Creation of (" + SubTasksList.Split(new string[] { "</br>" }, StringSplitOptions.RemoveEmptyEntries).Length + ") Sub Tasks</b><br/>\r\n" +
                                                     SubTasksList + "</div>\r\n" +
                                                "</div>" + "\r\n" +
                                            "</div>" + "\r\n";
                            TaskHistoryItem HistSubTaskItem = new TaskHistoryItem();
                            HistSubTaskItem.ItemDate = SubTaskDate;
                            HistSubTaskItem.ItemCode = HistoryText;
                            HistList.Add(HistSubTaskItem);

                            SubTasksList = "";
                            SubTaskOwnerID = drSubTask.Creator_ID;
                            SubTaskOwnerName = drSubTask.Created_By;
                            SubTaskOwnerImage = drSubTask.IsCreated_By_ImageNull() ? "assets/images/user.png" : drSubTask.Created_By_Image;
                        }
                        SubTasksList = SubTasksList + "<a href=\"task_info.aspx?tid=" + objURL.Encrypt(drSubTask.Task_ID.ToString()) + "\">" + drSubTask.Task_Number + " - " + drSubTask.Workflow_Name + "</a></br>";
                        SubTaskDate = drSubTask.Task_Date;
                    }
                }

                if (SubTasksList.Trim() != "")
                {
                    UserDetails = "<h5 class='text-warning mt'><img class=\"rounded\" src=\"" + SubTaskOwnerImage + "\" alt=\"Profile picture\" style=\"width: 20px;height: 20px;margin-top: -2px;\">" + "\r\n" +
                                              "<span class=\"text-info\">" + SubTaskOwnerName + "</span></h5>" + "\r\n";

                    HistoryText = "<div class=\"tl-entry\">" + "\r\n" +
                                        "<div class=\"tl-time\">" + SubTaskDate.Day + "|" + string.Format("{0:HH:mm}", SubTaskDate) + "</div>" + "\r\n" +
                                        "<div class=\"tl-icon bg-dark-gray\">" + "\r\n" +
                                            "<i class=\"fa fa-sitemap\"></i>" + "\r\n" +
                                        "</div>" + "\r\n" +
                                        "<div class='panel tl-body p8'>" + "\r\n" +
                                            UserDetails +
                                            "<div class='well well-md p8' style='margin: 0;'><b>Creation of (" + SubTasksList.Split(new string[] { "</br>" }, StringSplitOptions.RemoveEmptyEntries).Length + ") Sub Tasks</b><br/>\r\n" +
                                             SubTasksList + "</div>\r\n" +
                                        "</div>" + "\r\n" +
                                    "</div>" + "\r\n";
                    TaskHistoryItem HistSubTaskItem = new TaskHistoryItem();
                    HistSubTaskItem.ItemDate = SubTaskDate;
                    HistSubTaskItem.ItemCode = HistoryText;
                    HistList.Add(HistSubTaskItem);
                    SubTasksList = "";
                }
            }

            TimeSpan TotalWaitingHours = TimeSpan.FromHours(dsTask.tbltask_history.Sum(x => x.Waiting_Hours));
            TimeSpan TotalTaskHours = TimeSpan.FromHours(dsTask.tbltask_history.Sum(x => x.Task_Hours));
            lblDuration.Text = TotalWaitingHours.Days + " Days, " + TotalWaitingHours.Hours + " Hrs, " + TotalWaitingHours.Minutes + " Mns (Inactive)</br>" + TotalTaskHours.Days + " Days, " + TotalTaskHours.Hours + " Hrs, " + TotalTaskHours.Minutes + " Mns (Active)";

            if (objSes.UnitMins > 0)
            {
                int Units = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type).ReadUnits(dsTask.tbltasks[0].Entity_L2_ID, dsTask.tbltasks[0].Walkflow_ID);
                if (Units > 0)
                {
                    ltrUnits.Text = Math.Round(TotalTaskHours.TotalMinutes / objSes.UnitMins, 2).ToString() + " / " + Units.ToString();
                    if (TotalTaskHours.TotalMinutes / objSes.UnitMins > Units)
                    {
                        divTimeUnitNo.Attributes["class"] = "col-sm-8 text-lg text-danger";
                    }
                }
                else
                {
                    divTimeUnit.Visible = false;
                }
            }
            else
            {
                divTimeUnit.Visible = false;
            }

            foreach (DS_Tasks.tbltask_addonRow row in dsTask.tbltask_addon)
            {
                TaskUpdateFields = "";
                drAddonFields = dsTask.tbltask_update_addon_fields.Where(x => x.Task_Addon_ID == row.Task_Addon_ID).ToList();
                if (drAddonFields.Count > 0)
                {
                    TaskUpdateFields = objCommAct.GetTaskAddonUpdateForHistory(drAddonFields, objSes);
                }

                if (!row.IsPosted_By_NameNull())
                {
                    UserDetails = "<h5 class='text-warning mt'><img class=\"rounded\" src=\"" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "\" alt=\"Profile picture\" style=\"width: 25px;height: 25px;margin-top: -2px;\">" + "\r\n" +
                                  "<span class=\"text-info\">" + row.Posted_By_Name + "</span></h5>" + "\r\n";
                }
                else
                {
                    UserDetails = "";
                }

                HistoryText = "<div class=\"tl-entry\">" + "\r\n" +
                                    "<div class=\"tl-time\">" + row.Posted_Date.Day + "|" + string.Format("{0:HH:mm}", row.Posted_Date) + "</div>" + "\r\n" +
                                    "<div class=\"tl-icon bg-dark-gray\">" + "\r\n" +
                                        "<i class=\"fa fa-anchor\"></i>" + "\r\n" +
                                    "</div>" + "\r\n" +
                                    "<div class='panel tl-body p8'>" + "\r\n" +
                                        (row.isParentRecord ? "<div class='history_parent ttip badge badge-primary' title='From Parent Task' data-placement='right' data-original-title=''><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                        UserDetails +
                                        "<div class='well well-md p8' style='margin: 0;'><b>" + row.Addon_Name + "</b><br/>\r\n" +
                                        TaskUpdateFields.Replace("\r\n", "</br>") + "</div>\r\n" +
                                    "</div>" + "\r\n" +
                                "</div>" + "\r\n";
                TaskHistoryItem HistItem = new TaskHistoryItem();
                HistItem.ItemDate = row.Posted_Date;
                HistItem.ItemCode = HistoryText;
                HistList.Add(HistItem);
            }

            foreach (DS_Tasks.tbltask_commentsRow row in dsTask.tbltask_comments)
            {
                string cssClass = row.Comment_Type.Equals("1") ? "bg-danger" : row.Comment_Type.Equals("2") ? "bg-info" : "bg-success";
                HistoryText = "<div class=\"tl-entry\">" + "\r\n" +
                                "<div class=\"tl-time\">" + row.Comment_Date.Day + "|" + string.Format("{0:HH:mm}", row.Comment_Date) + "</div>" + "\r\n" +
                                    "<div class=\"tl-icon " + cssClass + "\">\r\n" +
                                        "<i class=\"fa fa-comments-o\"></i>\r\n" +
                                    "</div>" + "\r\n" +
                                "<div class=\"panel tl-body p8\">\r\n" +
                                (row.isParentRecord ? "<div class='history_parent ttip badge badge-primary' title='From Parent Task' data-placement='right'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                (row.isChildRecord ? "<div class='history_parent ttip badge badge-info' title='From Sub Task' data-placement='right'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                    "<h5 class='text-warning mt'>" +
                                        "<img class=\"rounded\" src=\"" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "\" alt=\"Profile picture\" style=\"width: 20px;height: 20px;margin-top: -2px;\">" + "\r\n" +
                                        "<span class=\"text-info\">" + row.Commented_By_Name + "</span>\r\n" +
                                    "</h5>" +
                                    "<div class=\"well well-md p8\" style=\"margin: 0;\">" + DecorateHashTags(row.Comment, false).Replace("\r\n", "</br>") + "</div>\r\n" +
                                "</div>" + "\r\n" +
                                "</div>" + "\r\n";
                TaskHistoryItem HistItem = new TaskHistoryItem();
                HistItem.ItemDate = row.Comment_Date;
                HistItem.ItemCode = HistoryText;
                HistList.Add(HistItem);
            }

            foreach (DS_Tasks.tbltask_api_resultsRow row in dsTask.tbltask_api_results)
            {
                HistoryText = "<div class=\"tl-entry\">\r\n" +
                                "<div class=\"tl-time\">" + row.API_Call_Date.Day + "|" + string.Format("{0:HH:mm}", row.API_Call_Date) + "</div>\r\n" +
                                "<div class=\"tl-icon bg-dark-gray\">\r\n" +
                                     "<i class=\"fa fa-bolt\"></i>\r\n" +
                                "</div>\r\n" +
                                "<div class=\"panel tl-body p8\">\r\n" +
                                    (row.isParentRecord ? "<div class='history_parent ttip badge badge-primary' title='From Parent Task' data-placement='right'><i class='fa fa-exclamation'></i></div>\r\n" : "") +
                                   "<h5 class='text-warning mt'>" +
                                      "<img class=\"rounded\" src=\"" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "\" alt=\"Profile picture\" style=\"width: 25px;height: 25px;margin-top: -2px;\">\r\n" +
                                      "<span class=\"text-info\">" + row.Called_By_Name + "</span>\r\n" +
                                   "</h5>" +
                                   "<div class='well well-md p8' style='margin: 0;'>\r\n" +
                                      "<b>API Called : " + row.API_Call_Name + "</b><br/><span class='text-danger'>Result : </span>" + row.API_Call_Result.Replace("\r\n", "</br>") + "\r\n" +
                                   "</div>\r\n" +
                                "</div>\r\n" +
                               "</div>\r\n";
                TaskHistoryItem HistItem = new TaskHistoryItem();
                HistItem.ItemDate = row.API_Call_Date;
                HistItem.ItemCode = HistoryText;
                HistList.Add(HistItem);
            }

            int MonthNo = 0;
            int YearNo = 0;

            ltrHistory.Text = "";

            List<TaskHistoryItem> Hist;

            if (objSes.TaskHtryDir)
            {
                Hist = HistList.OrderBy(x => x.ItemDate).ToList();
            }
            else
            {
                Hist = HistList.OrderByDescending(x => x.ItemDate).ToList();
            }

            foreach (TaskHistoryItem HistItm in Hist)
            {
                if (MonthNo != HistItm.ItemDate.Month || YearNo != HistItm.ItemDate.Year)
                {
                    ltrHistory.Text = ltrHistory.Text + "<div class=\"tl-header now bg-primary\">" + "\r\n" +
                                                         objCom.getMonth(HistItm.ItemDate.Month) + " " + HistItm.ItemDate.Year + "\r\n" +
                                                        "</div>" + "\r\n";
                    MonthNo = HistItm.ItemDate.Month;
                    YearNo = HistItm.ItemDate.Year;
                }
                ltrHistory.Text = ltrHistory.Text + HistItm.ItemCode;
            }

            LoadTaskComents(dsTask.tbltask_comments, objSes);
        }

        private void LoadTaskComents(DS_Tasks.tbltask_commentsDataTable tbl, SessionObject objSes)
        {
            TagsList = new System.Collections.ArrayList();
            string strCommentsOnly = "";
            if (tbl.Rows.Count > 0)
            {
                string CurrCommType = string.Empty;
                List<DS_Tasks.tbltask_commentsRow> Coms;
                if (objSes.TaskHtryDir)
                {
                    Coms = tbl.OrderBy(x => x.Comment_Category).ThenBy(y => y.Comment_Date).ToList();
                }
                else
                {
                    Coms = tbl.OrderBy(x => x.Comment_Category).ThenByDescending(y => y.Comment_Date).ToList();
                }

                foreach (DS_Tasks.tbltask_commentsRow row in Coms)
                {
                    if (row.Comment_Category.Trim() != CurrCommType)
                    {
                        strCommentsOnly = strCommentsOnly + "<div class='comment no-padding-b  no-border'>" + "\r\n" +
                                                                    " <div class='comment-body no-margin-hr'>" +
                                                                    "   <div class='comment-text'>" +
                                                                    "    <b>" + row.Comment_Category + "</b>" +
                                                                    "   </div>" + " </div>\r\n" +
                                                                    "</div>";
                        CurrCommType = row.Comment_Category.Trim();
                    }
                    string cssClass = row.Comment_Type.Equals("1") ? "timeline-label_p1" : row.Comment_Type.Equals("2") ? "timeline-label_p2" : "";
                    strCommentsOnly = strCommentsOnly + "<div class='comment no-border'>" + "\r\n" +
                                                                " <img src='" + (row.IsImage_PathNull() ? "assets/images/user.png" : row.Image_Path) + "' alt='Profile Picture' class='comment-avatar'>" + "\r\n" +
                                                                " <div class='comment-body'>" +
                                                                "   <div class='comment-text'>" +
                                                                "       <div class=\"panel tl-body p8 \">\r\n" +
                                                                (row.isParentRecord ? "<div class='history_parent ttip badge badge-primary' title='From Parent Task' data-placement='right'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                                (row.isChildRecord ? "<div class='history_parent ttip badge badge-info' title='From Sub Task' data-placement='right'><i class='fa fa-exclamation'></i></div>" + "\r\n" : "") +
                                                                "           <h5 class='text-warning mt'>" +
                                                                "               <span class=\"text-info\">" + row.Commented_By_Name + "</span>\r\n" +
                                                                "           </h5>" +
                                                                "           <div class=\"well well-md p8 " + cssClass + "\" style=\"margin: 6px 0 0 0;\">" + DecorateHashTags(row.Comment, true).Replace("\r\n", "</br>") + "</div>\r\n" +
                                                                "       </div>" + "\r\n" +
                                                                "   </div>" +
                                                                "   <div class='comment-actions'>" +
                                                                "       <span class='pull-right text-sm'>" + string.Format("{0:" + Constants.DateFormat + " HH:mm}", row.Comment_Date) + "</span>" + "\r\n" +
                                                                "   </div>\r\n" +
                                                                " </div>\r\n" +
                                                                "</div>";
                }
            }
            divCommentsOnly.InnerHtml = strCommentsOnly;
            TagsList.Sort();
            string Tag = "";
            cboTags.Items.Clear();
            foreach (string str in TagsList)
            {
                if (Tag != str)
                {
                    cboTags.Items.Add(new ListItem(str, str));
                    Tag = str;
                }
            }
            cboTags.Items.Insert(0, new ListItem("[All Comments]", "0"));
        }

        private string DecorateHashTags(string InputText, bool FillTagsDropDown)
        {
            string[] splits = InputText.Split('#');
            string outputText = "";
            int EndIndex = 0;
            char[] SearchEndFor = { ' ', ',', '-', ':', ';' };
            if (splits.Length > 1)
            {
                for (int i = 0; i < splits.Length; i++)
                {
                    if (i > 0)
                    {
                        EndIndex = splits[i].IndexOfAny(SearchEndFor);
                        if (EndIndex > 0)
                        {
                            if (FillTagsDropDown == true)
                            {
                                if (!TagsList.Contains("#" + splits[i].Substring(0, EndIndex)))
                                {
                                    TagsList.Add("#" + splits[i].Substring(0, EndIndex));
                                }
                            }
                            splits[i] = splits[i].Substring(0, EndIndex) + "</span>" + splits[i].Substring(EndIndex);
                        }
                        else
                        {
                            if (FillTagsDropDown == true)
                            {
                                if (TagsList.Contains(TagsList.Add("#" + splits[i])))
                                {
                                    TagsList.Add("#" + splits[i]);
                                }
                            }
                            splits[i] = splits[i] + "</span>";
                        }
                        splits[i] = "<span class='text-info'>#" + splits[i];
                    }
                    outputText = outputText + splits[i];
                }
            }
            else
            {
                outputText = InputText;
            }
            return outputText;
        }

        private void LoadTaskTimelineChart(DS_Tasks dsCurrentTask, SessionObject objSes)
        {
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow dsWFMain = objWF.Read(dsCurrentTask.tbltasks[0].Walkflow_ID, false, false);

            if (IsPostBack == false)
            {
                LoadFileTypes(dsWFMain.tblwalkflow[0]);
                LoadCommentTypes(dsWFMain.tblwalkflow[0]);
            }

            DS_Workflow.tblworkflow_stepsRow drCurrentStep;
            DS_Workflow.tblworkflow_stepsRow drLastStep;

            if (dsCurrentTask.tbltasks[0].Current_Step_ID == -1 || dsCurrentTask.tbltasks[0].Current_Step_ID == -2 || dsCurrentTask.tbltasks[0].Current_Step_ID == 0)
            {
                if (dsCurrentTask.tbltasks[0].Parent_Task_ID > 0)
                {
                    DS_Tasks dsCurrentTaskOnly = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading).Read(dsCurrentTask.tbltasks[0].Task_ID, false, false, false);
                    drCurrentStep = dsWFMain.tblworkflow_steps.Where(x => x.Workflow_Step_ID == dsCurrentTaskOnly.tbltask_history[dsCurrentTaskOnly.tbltask_history.Rows.Count - 1].Workflow_Step_ID).FirstOrDefault();
                }
                else
                {
                    drCurrentStep = dsWFMain.tblworkflow_steps.Where(x => x.Workflow_Step_ID == dsCurrentTask.tbltask_history[dsCurrentTask.tbltask_history.Rows.Count - 1].Workflow_Step_ID).FirstOrDefault();
                }

                drLastStep = dsWFMain.tblworkflow_steps.Where(x => x.Sort_order >= drCurrentStep.Sort_order && x.Is_Last_Step == true).FirstOrDefault();
            }
            else
            {
                drCurrentStep = dsWFMain.tblworkflow_steps.Where(x => x.Workflow_Step_ID == dsCurrentTask.tbltasks[0].Current_Step_ID).FirstOrDefault();
                drLastStep = dsWFMain.tblworkflow_steps.Where(x => x.Sort_order >= drCurrentStep.Sort_order && x.Is_Last_Step == true).FirstOrDefault();
            }

            if (drLastStep == null)
            {
                drLastStep = dsWFMain.tblworkflow_steps[dsWFMain.tblworkflow_steps.Rows.Count - 1];
            }

            DS_Workflow dsWF = new DS_Workflow();
            DS_Workflow.tblworkflow_stepsRow drWFStep;

            foreach (DS_Workflow.tblworkflow_stepsRow rowWF in dsWFMain.tblworkflow_steps)
            {
                if (dsCurrentTask.tbltask_timeline.Where(x => x.Workflow_Step_ID == rowWF.Workflow_Step_ID).Count() > 0 || rowWF.Workflow_Step_ID == dsCurrentTask.tbltasks[0].Current_Step_ID)
                {
                    if (rowWF.Sort_order <= drLastStep.Sort_order)
                    {
                        if (rowWF.Sort_order < drCurrentStep.Sort_order)
                        {
                            if (dsCurrentTask.tbltask_history.Where(x => x.Workflow_Step_ID == rowWF.Workflow_Step_ID).Count() > 0)
                            {
                                drWFStep = dsWF.tblworkflow_steps.Newtblworkflow_stepsRow();
                                drWFStep.ItemArray = rowWF.ItemArray;
                                dsWF.tblworkflow_steps.Rows.Add(drWFStep);
                            }
                        }
                        else
                        {
                            drWFStep = dsWF.tblworkflow_steps.Newtblworkflow_stepsRow();
                            drWFStep.ItemArray = rowWF.ItemArray;
                            dsWF.tblworkflow_steps.Rows.Add(drWFStep);
                        }
                    }
                }
                
            }

            int TotalStepWeight = dsWF.tblworkflow_steps.Sum(x => x.Step_Weight);
            int CurrentStepWeight = 0;
            for (int i = 0; i < dsWF.tblworkflow_steps.Rows.Count; i++)
            {
                if (dsWF.tblworkflow_steps[i].Workflow_Step_ID == dsCurrentTask.tbltasks[0].Current_Step_ID)
                {
                    break;
                }
                CurrentStepWeight += dsWF.tblworkflow_steps[i].Step_Weight;
            }

            dsCurrentTask.tbltasks[0].Progress_Value = (int)((double)((double)CurrentStepWeight / (double)TotalStepWeight * (double)100));

            divProgress.Attributes["style"] = "width: " + dsCurrentTask.tbltasks[0].Progress_Value + "%";
            if (dsCurrentTask.tbltasks[0].Current_Step_ID > 0 && dsCurrentTask.tbltasks[0].IsETB_ValueNull() == false && dsCurrentTask.tbltasks[0].Hours_Before_Conclusion > 0 && dsCurrentTask.tbltasks[0].ETB_Value.AddHours(-(dsCurrentTask.tbltasks[0].Hours_Before_Conclusion)).Date <= DateTime.Today.Date)
            {
                divProgress.Attributes["class"] = "progress-bar progress-bar-danger";
            }
            else if (dsCurrentTask.tbltasks[0].IsETB_ValueNull() == false && dsCurrentTask.tbltasks[0].ETB_Value < DateTime.Now)
            {
                divProgress.Attributes["class"] = "progress-bar progress-bar-danger";
            }
            else
            {
                divProgress.Attributes["class"] = "progress-bar progress-bar-success";
            }

            DateTime FromDate = dsCurrentTask.tbltasks[0].Task_Date;
            DateTime ToDate = dsCurrentTask.tbltasks[0].ETB_Value;

            DateTime dtTLMax = dsCurrentTask.tbltask_timeline.Select(x => x.Step_Deadline).Max();
            if (dtTLMax > ToDate)
            {
                ToDate = dtTLMax;
            }

            DateTime dtPostMax = dsCurrentTask.tbltask_history[dsCurrentTask.tbltask_history.Rows.Count - 1].Posted_Date;
            if (dtPostMax > ToDate)
            {
                ToDate = dtPostMax;
            }

            if (ToDate < DateTime.Now)
            {
                ToDate = DateTime.Now;
            }

            for (int i = 0; i < dsCurrentTask.tbltask_timeline.Count; i++)
            {
                dsCurrentTask.tbltask_timeline[i].Step_Index = i + 1;
            }

            DS_Tasks.tbltask_timelineRow drTL = dsCurrentTask.tbltask_timeline.Newtbltask_timelineRow();
            drTL.Step_Index = 0;
            drTL.Step_Status = "Start";
            drTL.Step_Deadline = FromDate.Date;
            drTL.Workflow_Step_ID = 0;
            dsCurrentTask.tbltask_timeline.Rows.InsertAt(drTL, 0);

            string Forecasted_Value = null;
            string Actual_Value = null;

            ChartScripts = "var task_data = [" + "\r\n";

            if (ToDate < DateTime.Now)
            {
                ToDate = DateTime.Now;
            }

            for (DateTime day = FromDate.Date; day.Date <= ToDate.Date; day = day.AddDays(1))
            {
                Forecasted_Value = null;
                List<DS_Tasks.tbltask_timelineRow> drStep = dsCurrentTask.tbltask_timeline.Where(x => x.Step_Deadline.Date == day).OrderByDescending(y => y.Step_Index).ToList();
                if (drStep.Count > 0)
                {
                    Forecasted_Value = drStep[0].Step_Index.ToString();
                }

                Actual_Value = null;
                List<DS_Tasks.tbltask_historyRow> drTH = dsCurrentTask.tbltask_history.Where(x => !x.IsStep_Finished_DateNull() && x.Step_Finished_Date.Date == day).OrderByDescending(y => y.Task_Update_ID).ToList();
                if (drTH.Count > 0)
                {
                    foreach (DS_Tasks.tbltask_historyRow row in drTH)
                    {
                        drStep = dsCurrentTask.tbltask_timeline.Where(x => x.Workflow_Step_ID == row.Workflow_Step_ID).ToList();
                        if (drStep.Count > 0)
                        {
                            Actual_Value = drStep[0].Step_Index.ToString();
                            break;
                        }
                    }
                }
                if (Actual_Value == null && FromDate.Date == day.Date)
                {
                    Actual_Value = "0";
                }

                ChartScripts = ChartScripts + "{ t_date: '" + day.ToString("yyyy-MM-dd") + "', forecasted_val: " + (Forecasted_Value == null ? "null" : Forecasted_Value) + ", actual_val: " + (Actual_Value == null ? "null" : Actual_Value) + " }";
                if (day.Date == ToDate.Date)
                {
                    ChartScripts = ChartScripts + "\r\n";
                }
                else
                {
                    ChartScripts = ChartScripts + "," + "\r\n";
                }
            }

            ChartScripts = ChartScripts + "];";

            LoadStepsList(dsCurrentTask.tbltask_timeline);
        }

        private void LoadStepsList(DS_Tasks.tbltask_timelineDataTable tblTL)
        {
            pnlStepsListBody.Controls.Clear();

            HtmlGenericControl divTableControl = new HtmlGenericControl("div");
            divTableControl.Attributes.Add("class", "table-responsive no-margin-b");
            Table tblSteps = new Table();
            tblSteps.CssClass = "table table-striped table-hover grid_table mb";

            TableHeaderRow tblHead = new TableHeaderRow();
            tblHead.TableSection = TableRowSection.TableHeader;
            TableHeaderCell thIndex = new TableHeaderCell();
            thIndex.Width = 70;
            thIndex.Text = "Index";
            tblHead.Cells.Add(thIndex);

            TableHeaderCell thStep = new TableHeaderCell();
            thStep.Text = "Step";
            tblHead.Cells.Add(thStep);

            tblSteps.Rows.Add(tblHead);

            foreach (DS_Tasks.tbltask_timelineRow row in tblTL)
            {
                TableRow tblRow = new TableRow();
                tblRow.TableSection = TableRowSection.TableBody;

                TableCell tdIndex = new TableCell();
                tdIndex.Width = 70;
                tdIndex.Text = row.Step_Index.ToString();
                tblRow.Cells.Add(tdIndex);

                TableCell tdStep = new TableCell();
                tdStep.Text = row.Step_Status;
                tblRow.Cells.Add(tdStep);

                tblSteps.Rows.Add(tblRow);
            }

            divTableControl.Controls.Add(tblSteps);

            pnlStepsListBody.Controls.Add(divTableControl);
        }

        private void LoadTaskMessages(int Walkflow_ID, int Entity_L2_ID, int Task_ID, SessionObject objSes)
        {
            Messages objMessages = ObjectCreator.GetMessages(objSes.Connection, objSes.DB_Type);
            if (IsPostBack == false)
            {
                DS_Messages ds = objMessages.ReadUsersForTask(Walkflow_ID, Entity_L2_ID, objSes.UserID);
                cboMessageUsers.DataSource = ds.tblusers;
                cboMessageUsers.DataTextField = "Full_Name";
                cboMessageUsers.DataValueField = "User_ID";
                cboMessageUsers.DataBind();
                cboMessageUsers.Items.Insert(0, new ListItem("[All Involved]", "-1"));
                cboMessageUsers.Items.Insert(0, new ListItem("[Not Selected]", "0"));
            }
            LoadThreads(objMessages, Task_ID, objSes);
        }

        private void LoadThreads(Messages objMsgs, int Task_ID, SessionObject objSes)
        {
            DS_Messages ds = objMsgs.ReadThreadsForTask(Task_ID, objSes.UserID);

            if (ds.tblmessage_threads.Rows.Count > 0)
            {
                ltrThreads.Text = "<div class=\"table-responsive table-light no-margin-b\"><table id=\"message_table\" class=\"table table-striped table-hover grid_table row-pointer no-margin-b non_full_width_table\" data-size=\"non_full_width_table\">" + "\r\n" +
                                        "<thead>" + "\r\n" +
                                            "<tr>" + "\r\n" +
                                                "<th>Subject</th>" + "\r\n" +
                                                "<th class=\"hidden-xs w40\"></th>" + "\r\n" +
                                                "<th class=\"w100\">With</th>" + "\r\n" +
                                                "<th class=\"text-center w100\">Date</th>" + "\r\n" +
                                            "</tr>" + "\r\n" +
                                        "</thead>" + "\r\n" +
                                        "<tbody>" + "\r\n";
                foreach (DS_Messages.tblmessage_threadsRow row in ds.tblmessage_threads)
                {
                    ltrThreads.Text = ltrThreads.Text + "<tr class=\"" + (row.New_Message_Count > 0 ? "message-read " : "message-unread ") + "clickable-row\" data-href='" + row.Thread_ID + "'>" + "\r\n" +
                                                            "<td>" + row.Title + "</td>" + "\r\n" +
                                                            "<td class=\"hidden-xs w40\">" + "\r\n" +
                                                            (row.New_Message_Count > 0 ? "<span class=\"label label-info mr10 fs11\">" + row.New_Message_Count + "</span>" + "\r\n" : "") +
                                                            "</td>" + "\r\n" +
                                                            "<th class=\"w100\">" + row.Other_Member + "</th>" + "\r\n" +
                                                            "<td class=\"text-center w100\">" + string.Format("{0:" + Constants.DateFormat + "}", row.Last_Message_Date) + "</td>" + "\r\n" +
                                                        "</tr>" + "\r\n";
                }
                ltrThreads.Text = ltrThreads.Text + "</tbody>" + "\r\n" +
                                                "</table></div>" + "\r\n";
            }
            else
            {
                ltrThreads.Text = "";
            }
        }

        protected void cmdSendMessage_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Messages objMessage = ObjectCreator.GetMessages(objSes.Connection, objSes.DB_Type);
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            if (hndMessageThreadID.Value == "0")
            {
                if (cboMessageUsers.SelectedItem.Value == "-1")
                {
                    DS_Tasks dsTS = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading).Read(Task_ID, false, false, false);
                    DS_Messages dsUsers = objMessage.ReadUsersForTask(dsTS.tbltasks[0].Walkflow_ID, dsTS.tbltasks[0].Entity_L2_ID, objSes.UserID);
                    bool AllProcesses = true;
                    foreach (DS_Messages.tblusersRow row in dsUsers.tblusers)
                    {
                        if (objMessage.AddThread(objSes.UserID, row.User_ID, txtTitle.Text, txtMessage.Text, objSes.UserID, objSes.PhysicalRoot, objSes.WebRoot, Task_ID) == false)
                        {
                            AllProcesses = false;
                        }
                    }

                    if (AllProcesses == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey23", "ShowError('All Messages could not be Sent');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey23", "ShowSuccess('Messages Successfully Sent');", true);

                        Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                        DS_Tasks ds = objTask.Read(Task_ID, false, false, false);

                        LoadTaskMessages(ds.tbltasks[0].Walkflow_ID, ds.tbltasks[0].Entity_L2_ID, Task_ID, objSes);
                    }
                }
                else
                {
                    if (objMessage.AddThread(objSes.UserID, Convert.ToInt32(cboMessageUsers.SelectedValue), txtTitle.Text, txtMessage.Text, objSes.UserID, objSes.PhysicalRoot, objSes.WebRoot, Task_ID) == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Message cannot be Sent');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Message Successfully Sent');", true);

                        Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                        DS_Tasks dsTask = objTask.Read(Task_ID, false, false, false);

                        LoadTaskMessages(dsTask.tbltasks[0].Walkflow_ID, dsTask.tbltasks[0].Entity_L2_ID, Task_ID, objSes);
                    }
                }
            }
            else
            {
                if (objMessage.AddMessage(Convert.ToInt32(hndMessageThreadID.Value), objSes.UserID, Convert.ToInt32(hndMessageMemberID.Value), txtMessage.Text, objSes.UserID, objSes.PhysicalRoot, objSes.WebRoot, Task_ID) == false)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Message cannot be Sent');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Message Successfully Sent');", true);

                    Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                    DS_Tasks dsTask = objTask.Read(Task_ID, false, false, false);

                    LoadTaskMessages(dsTask.tbltasks[0].Walkflow_ID, dsTask.tbltasks[0].Entity_L2_ID, Task_ID, objSes);
                }
            }
        }

        protected void cmdSaveComment_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            if (objTask.AddComment(Task_ID, objSes.UserID, txtCommentMain.Text, cboPriority.SelectedValue, cboCommentType.SelectedItem.Text) == true)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Comment Successfully Added');", true);

                DS_Tasks dsTask = objTask.Read(Task_ID, false, false, false);
                DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(dsTask.tbltasks[0].Walkflow_ID, false, false);
                FillTaskData(Task_ID, ref dsTask, dsWF, objTask);

                DS_Tasks dsSubs = objTask.GetActiveSubTasks(Task_ID);

                LoadHistory(dsTask, dsSubs, objSes);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Database Error : Cannot Add the Comment');", true);
            }
        }

        protected void cmdAddDocComment_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            bool ActionOK = true;

            if (Convert.ToString(hndIsDocument.Value) == "1")
            {
                ActionOK = objTask.AddComment(Task_ID, objSes.UserID, txtDocComment.Text, cboDocCommentPriority.SelectedValue, cboDocCommentType.SelectedItem.Text, Convert.ToInt32(hndDocCommentID.Value), 0);
            }
            else
            {
                ActionOK = objTask.AddComment(Task_ID, objSes.UserID, txtDocComment.Text, cboDocCommentPriority.SelectedValue, cboDocCommentType.SelectedItem.Text, 0, Convert.ToInt32(hndDocCommentID.Value));
            }

            if (ActionOK == true)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Comment Successfully Added');", true);

                DS_Tasks dsTask = objTask.Read(Task_ID, false, false, false);
                DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(dsTask.tbltasks[0].Walkflow_ID, false, false);
                FillTaskData(Task_ID, ref dsTask, dsWF, objTask);

                DS_Tasks dsSubs = objTask.GetActiveSubTasks(Task_ID);

                LoadHistory(dsTask, dsSubs, objSes);
                LoadAttachments(dsTask.tbltask_docs, dsTask.tbltask_attachment, dsTask.tbltask_comments, objSes);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Database Error : Cannot Add the Comment');", true);
            }
        }

        protected void cmdSaveAddLinkhnd_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            int Document_ID = Convert.ToInt32(hndLinkDocID.Value);
            bool isResult = (hndIsResultLink.Value == "1" ? true : false);

            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);


            if (objTask.AddLink(Task_ID, Document_ID, objSes.UserID, isResult) == true)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Document Link Successfully Linked');", true);

                DS_Tasks dsTask = objTask.Read(Task_ID, false, false, false);
                DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(dsTask.tbltasks[0].Walkflow_ID, false, false);
                FillTaskData(Task_ID, ref dsTask, dsWF, objTask);

                LoadAttachments(dsTask.tbltask_docs, dsTask.tbltask_attachment, dsTask.tbltask_comments, objSes);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('The file is already Linked');", true);
            }
        }

        protected void gvDocumentsFind_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    cmdEdit.Attributes.Add("onclick", "document.getElementById('" + hndLinkDocID.ClientID + "').value = '" + e.Row.Cells[0].Text + "'; saveLink(); return false;");
                }
            }
        }

        private void FillTaskData(int Task_ID, ref DS_Tasks dsTask, DS_Workflow dsWF, Tasks objTask)
        {
            if (dsTask == null)
            {
                dsTask = objTask.Read(Task_ID, dsWF.tblwalkflow[0].Show_Parent_Task_History, dsWF.tblwalkflow[0].Show_SubTask_Documents, dsWF.tblwalkflow[0].Show_SubTask_Comments);
            }
            else
            {
                if ((dsTask.tbltasks[0].Parent_Task_ID > 0 && dsWF.tblwalkflow[0].Show_Parent_Task_History) || dsWF.tblwalkflow[0].Show_SubTask_Documents || dsWF.tblwalkflow[0].Show_SubTask_Comments)
                {
                    foreach (System.Data.DataTable tbl in dsTask.Tables)
                    {
                        tbl.Rows.Clear();
                    }
                    dsTask = objTask.Read(Task_ID, dsWF.tblwalkflow[0].Show_Parent_Task_History, dsWF.tblwalkflow[0].Show_SubTask_Documents, dsWF.tblwalkflow[0].Show_SubTask_Comments);
                }
            }
        }

        protected void cmdUploadHnd_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Task_ID = Convert.ToInt32(ViewState["tid"]);
            Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            DS_Tasks dsTask = objTask.Read(Task_ID, false, false, false);
            DS_Workflow dsWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).Read(dsTask.tbltasks[0].Walkflow_ID, false, false);
            FillTaskData(Task_ID, ref dsTask, dsWF, objTask);

            LoadAttachments(dsTask.tbltask_docs, dsTask.tbltask_attachment, dsTask.tbltask_comments, objSes);
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(this.GetType(), "AKey19", "ShowSuccess('Files Successfully Uploaded');", true);
        }
    }
}