using N_Ter.Structures;
using N_Ter.Base;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.IO;
using System.Web;
using N_Ter.Security;

namespace N_Ter_Tasks
{
    public partial class document_preview_temp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["dt"] != null)
                {
                    SessionObject objSes = (SessionObject)Session["dt"];
                    if (objSes != null && objSes.UserID > 0)
                    {
                        string Doc_Name = Convert.ToString(Request.QueryString["d"]);
                        string FilePath;
                        if (Doc_Name.StartsWith("wl:"))
                        {
                            URL_Manager objURL = new URL_Manager();
                            DS_Notice_Board ds = ObjectCreator.GetNoticeBoard(objSes.Connection, objSes.DB_Type).ReadFile(Convert.ToInt32(objURL.Decrypt(Doc_Name.Substring(Doc_Name.IndexOf(":") + 1))));
                            if (ds.tblnotice_board_files.Rows.Count > 0)
                            {
                                FilePath = objSes.PhysicalRoot + "\\" + ds.tblnotice_board_files[0].File_Path;
                            }
                            else
                            {
                                FilePath = "";
                                Response.Redirect("error.aspx?eid=1");
                            }
                        }
                        else if (Doc_Name.StartsWith("up:"))
                        {
                            FilePath = objSes.PhysicalRoot + "\\nter_app_uploads\\profile_docs\\profile_" + objSes.UserID + "\\" + Doc_Name.Substring(Doc_Name.IndexOf(":") + 1);
                        }
                        else
                        {
                            FilePath = objSes.PhysicalRoot + "\\nter_app_uploads\\temp_email_attachments\\" + Doc_Name;
                        }

                        string[] SplitFilePath = FilePath.Split('.');
                        string extention = SplitFilePath[1];
                        if (File.Exists(FilePath))
                        {
                            if (extention.ToUpper() == "PDF")
                            {
                                Response.ContentType = "Application/pdf";
                            }
                            else if (extention.ToUpper() == "JPG")
                            {
                                Response.ContentType = "image/jpg";
                            }
                            else if (extention.ToUpper() == "JPEG")
                            {
                                Response.ContentType = "image/jpeg";
                            }
                            else if (extention.ToUpper() == "BMP")
                            {
                                Response.ContentType = "image/bmp";
                            }
                            else if (extention.ToUpper() == "PNG")
                            {
                                Response.ContentType = "image/png";
                            }
                            else
                            {
                                string FileName = SplitFilePath[0].ToString();
                                string[] SplitFileName = FileName.Split('\\');
                                FileName = SplitFileName[SplitFileName.Length - 1] + "." + extention;
                                Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
                                Response.ContentType = "application/octet-stream";
                                HttpContext.Current.ApplicationInstance.CompleteRequest();
                            }
                            Response.WriteFile(FilePath);
                            Response.End();
                        }
                        else
                        {
                            Response.Redirect("error.aspx?eid=1");
                        }
                    }
                    else
                    {
                        Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority) + (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath));
                    }
                }
                else
                {
                    Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority) + (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath));
                }
            }
        }
    }
}