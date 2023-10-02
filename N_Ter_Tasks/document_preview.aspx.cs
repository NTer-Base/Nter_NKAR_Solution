using System;
using System.IO;
using System.Web;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter.Base;

namespace N_Ter_Tasks
{
    public partial class document_preview : System.Web.UI.Page
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
                        N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                        int TaskDocID = Convert.ToInt32(objURL.Decrypt(Request.QueryString["fid"]));

                        Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                        DS_Tasks.tbltask_docsRow dr = objTask.ReadTaskDocument(TaskDocID);
                        string FilePath = dr.Doc_Path;

                        string[] SplitFilePath = FilePath.Split('.');
                        string extention = SplitFilePath[1];
                        if (File.Exists(Server.MapPath(FilePath)))
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
                            Response.WriteFile(Server.MapPath(FilePath));
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