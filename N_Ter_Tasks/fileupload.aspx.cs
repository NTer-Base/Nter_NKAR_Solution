using System;
using System.Web;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter.Base;
using System.Collections.Generic;
namespace N_Ter_Tasks
{
    public partial class fileupload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["tid"] != null)
                {
                    if (Session["dt"] != null)
                    {
                        try
                        {
                            SessionObject objSes = (SessionObject)Session["dt"];
                            if (objSes.UserID > 0)
                            {
                                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                                int Task_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["tid"])));
                                Tasks objTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                                DS_Tasks dsTask = objTask.Read(Task_ID, false, false, false);

                                DS_Entity_Level_2.tblentity_level_2Row drComp = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type).Read(dsTask.tbltasks[0].Entity_L2_ID);
                                string PathPart = "\\" + drComp.Folder_Name + "\\Workflow_Uploads\\" + DateTime.Today.Year + "\\" + DateTime.Today.Month;
                                string SaveLocation = objSes.PhysicalRoot + "\\nter_app_uploads\\client_docs" + PathPart;
                                string SaveLocationDB = "nter_app_uploads\\client_docs" + PathPart;

                                if (System.IO.Directory.Exists(SaveLocation) == false)
                                {
                                    System.IO.Directory.CreateDirectory(SaveLocation);
                                }

                                string FilePrefix = Request.Form["txt"].Split(',')[0];
                                string AccessLevel = Request.Form["pri"].Split(',')[0];
                                string DocumentType = Request.Form["typ"].Split(',')[0];
                                string is_Result = Request.Form["res"].Split(',')[0];
                                bool is_Reupload = false;
                                if (Request.Form["reup"] != null)
                                {
                                    is_Reupload = Convert.ToBoolean(Request.Form["reup"].Split(',')[0]);
                                }
                                string TotalFiles = Request.Form["tot"].Split(',')[0];
                                string VState = Request.Form["vst"].Split(',')[0] + "vs";

                                List<File_Uploads> objList;
                                if (Session[VState] == null)
                                {
                                    objList = new List<File_Uploads>();
                                }
                                else
                                {
                                    objList = (List<File_Uploads>)Session[VState];
                                }

                                N_Ter.Common.Common_Actions objComAct = new N_Ter.Common.Common_Actions();

                                string SaveLocationFile = "";
                                string SaveLocationDBFile = "";
                                foreach (string s in Context.Request.Files)
                                {
                                    HttpPostedFile file = Context.Request.Files[s];
                                    string fileName = file.FileName;
                                    string[] fnSP = fileName.Split('.');

                                    string NewFileName = N_Ter.Common.Common_Actions.ShortTimeStampForFileName();
                                    if (FilePrefix.Trim() != "")
                                    {
                                        NewFileName = FilePrefix.Trim() + (objList.Count > 0 ? "(" + (objList.Count + 1) + ")" : "") + "_" + NewFileName;
                                    }
                                    else
                                    {
                                        NewFileName = fileName.Substring(0, fileName.LastIndexOf('.')) + "_" + NewFileName;
                                    }
                                    NewFileName = objComAct.FormatFileName(NewFileName);
                                    SaveLocationFile = SaveLocation + "\\" + NewFileName + "." + fnSP[fnSP.Length - 1];
                                    SaveLocationDBFile = SaveLocationDB + "\\" + NewFileName + "." + fnSP[fnSP.Length - 1];

                                    file.SaveAs(SaveLocationFile);

                                    objList.Add(new File_Uploads
                                    {
                                        Physical_Path = SaveLocationFile,
                                        Virtual_Path = SaveLocationDBFile
                                    });
                                }
                                if (Convert.ToInt32(TotalFiles) == objList.Count)
                                {
                                    objTask.InitiateBulkOCR();
                                    foreach (File_Uploads flUpload in objList)
                                    {
                                        objTask.UploadFileQueForOCR(Task_ID, flUpload.Virtual_Path, objSes.UserID, DocumentType, Convert.ToInt32(AccessLevel), flUpload.Physical_Path, (is_Result == "0" ? false : true), is_Reupload);
                                    }
                                    if (objSes.EnableReading)
                                    {
                                        objTask.ProcessBulkOCR();
                                    }
                                    Session.Remove(VState);
                                }
                                else
                                {
                                    Session[VState] = objList;
                                }
                            }
                            else
                            {
                                Response.StatusCode = 400;
                                Response.Write("Invalid Request, No Active User");
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.StatusCode = 400;
                            Response.Write(ex.Message);
                        }
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        Response.Write("Your Session has ended, Please Refresh the Page");
                    }
                }
                else
                {
                    Response.StatusCode = 400;
                    Response.Write("Invalid Request, Please Refresh the Page");
                }
            }
        }
    }
}