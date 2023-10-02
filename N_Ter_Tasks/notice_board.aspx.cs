using N_Ter.Base;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace N_Ter_Tasks
{
    public partial class notice_board : System.Web.UI.Page
    {
        public string Galleries_Script = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            cboNoticeType.Attributes.Add("onChange", "CheckNoticeType();");
            cmdPost.OnClientClick = "return ValidateNotice('" + txtTitle.ClientID + "');";
            cmdAddComment.OnClientClick = "return ValidateComment('" + txtComment.ClientID + "');";
            txtNote.Attributes.Add("maxlength", "500");
            SessionObject objSes = (SessionObject)Session["dt"];
            LoadWall(objSes);
        }

        private void LoadWall(SessionObject objSes)
        {
            Notice_Board objNotice = ObjectCreator.GetNoticeBoard(objSes.Connection, objSes.DB_Type);
            DS_Notice_Board dsNB = objNotice.ReadAll(0, 0, objSes.UserID);
            hndSlab.Value = "0";
            hndLastSeen.Value = "0";
            hndEaliestDate.Value = string.Format("{0:" + Constants.DateFormat + "}", DateTime.Now.Date.AddMonths(1));

            ltrNoticeBoard.Text = "";
            Galleries_Script = "";

            if (dsNB.tblnotice_board.Rows.Count > 0)
            {
                N_Ter.Common.Common_Actions objComAct = new N_Ter.Common.Common_Actions();
                List<int> intGalleries = new List<int>();
                ltrNoticeBoard.Text = objComAct.GetWallHTML(dsNB, DateTime.Now.Date.AddMonths(1), objSes.UserID, ref intGalleries);
                Galleries_Script = "";
                foreach (int Gallery in intGalleries)
                {
                    Galleries_Script += "$('.gal_" + Gallery.ToString() + "').swipebox();\r\n";
                }
                List<DS_Notice_Board.tblnotice_boardRow> dr = dsNB.tblnotice_board.OrderBy(x => x.Record_ID).ToList();
                hndLastSeen.Value = dr.Last().Record_ID.ToString();
                hndEaliestDate.Value = string.Format("{0:" + Constants.DateFormat + "}", dr.First().Update_Date);
                hndSlab.Value = "1";
            }
        }

        protected void cmdPost_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            NoticeTypes objNType = (NoticeTypes)Convert.ToInt32(cboNoticeType.SelectedItem.Value);
            Notice_Board objNotice = ObjectCreator.GetNoticeBoard(objSes.Connection, objSes.DB_Type);
            DS_Notice_Board ds = new DS_Notice_Board();
            DS_Notice_Board.tblnotice_board_filesRow dr;
            bool NoticeAdded;
            switch (objNType)
            {
                case NoticeTypes.Note:
                    NoticeAdded = objNotice.Insert(objSes.UserID, 1, txtTitle.Text, txtNote.Text, "", ds.tblnotice_board_files);
                    break;
                case NoticeTypes.Images:
                    string ResponseImages = hndUploadResponse.Value;
                    foreach (string file in ResponseImages.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (file.Trim() != "")
                        {
                            dr = ds.tblnotice_board_files.Newtblnotice_board_filesRow();
                            dr.Notice_ID = 0;
                            dr.File_Path = file.Trim();
                            dr.File_Description = "";
                            ds.tblnotice_board_files.Rows.Add(dr);
                        }
                    }
                    NoticeAdded = objNotice.Insert(objSes.UserID, 2, txtTitle.Text, txtNote.Text, "", ds.tblnotice_board_files);
                    break;
                case NoticeTypes.Documents:
                    string ResponseFiles = hndUploadResponse.Value;
                    foreach (string file in ResponseFiles.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (file.Trim() != "")
                        {
                            dr = ds.tblnotice_board_files.Newtblnotice_board_filesRow();
                            dr.Notice_ID = 0;
                            dr.File_Path = file.Trim();
                            dr.File_Description = "";
                            ds.tblnotice_board_files.Rows.Add(dr);
                        }
                    }
                    NoticeAdded = objNotice.Insert(objSes.UserID, 3, txtTitle.Text, txtNote.Text, "", ds.tblnotice_board_files);
                    break;
                case NoticeTypes.Link:
                    if (hndThumbPath.Value.Trim() != "")
                    {
                        dr = ds.tblnotice_board_files.Newtblnotice_board_filesRow();
                        dr.Notice_ID = 0;
                        dr.File_Path = hndThumbPath.Value.Trim();
                        dr.File_Description = "";
                        ds.tblnotice_board_files.Rows.Add(dr);
                    }
                    NoticeAdded = objNotice.Insert(objSes.UserID, 4, txtTitle.Text, txtNote.Text, txtURL.Text, ds.tblnotice_board_files);
                    break;
            }
            LoadWall(objSes);
        }

        protected void cmdAddComment_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Notice_Board objNotice = ObjectCreator.GetNoticeBoard(objSes.Connection, objSes.DB_Type);
            if (objNotice.InsertComment(Convert.ToInt32(hndComments.Value), objSes.UserID, txtComment.Text))
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Comment Suceessfullt Added');", true);
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Comment could not be Added');", true);
            }
            LoadWall(objSes);
        }

        protected void cmdDelPost_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Notice_Board objNB = ObjectCreator.GetNoticeBoard(objSes.Connection, objSes.DB_Type);
            DS_Notice_Board ds = objNB.Read(Convert.ToInt32(hndDelPost.Value));
            if (ds.tblnotice_board.Rows.Count > 0)
            {
                if (ds.tblnotice_board[0].User_ID == objSes.UserID)
                {
                    DeleteReason objDel = objNB.Delete(Convert.ToInt32(hndDelPost.Value));
                    if (objDel.Deleted == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + objDel.Reason + "');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Post Successfully Deleted');", true);
                        hndDelPost.Value = "0";
                    }
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('You cannot delete a post done by another person');", true);
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Something Went wrong, Please contact the Systems Administrator');", true);
            }
            LoadWall(objSes);
        }

        protected void cmdDelCom_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Notice_Board objNB = ObjectCreator.GetNoticeBoard(objSes.Connection, objSes.DB_Type);
            DS_Notice_Board ds = objNB.ReadComment(Convert.ToInt32(hndDelCom.Value));
            if (ds.tblnotice_board_comments.Rows.Count > 0)
            {
                if (ds.tblnotice_board_comments[0].User_ID == objSes.UserID)
                {
                    DeleteReason objDel = objNB.DeleteComment(Convert.ToInt32(hndDelCom.Value));
                    if (objDel.Deleted == false)
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + objDel.Reason + "');", true);
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Comment Successfully Deleted');", true);
                        hndDelCom.Value = "0";
                    }
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('You cannot delete a Comment added by another person');", true);
                }
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Something Went wrong, Please contact the Systems Administrator');", true);
            }
            LoadWall(objSes);
        }
    }
}