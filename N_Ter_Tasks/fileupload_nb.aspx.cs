using N_Ter.Structures;
using System;
using System.Collections.Generic;
using System.Web;


namespace N_Ter_Tasks
{
    public partial class fileupload_nb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["dt"] != null)
                {
                    try
                    {
                        SessionObject objSes = (SessionObject)Session["dt"];
                        if (objSes.UserID > 0)
                        {
                            string TotalFiles = Request.Form["tot"].Split(',')[0];
                            string VState = Request.Form["vst"].Split(',')[0] + "vs";
                            string SaveLocationDB = "nter_app_uploads\\profile_docs\\profile_" + objSes.UserID + "\\wall\\" + DateTime.Today.Year + "\\" + DateTime.Today.Month;
                            string SaveLocation = objSes.PhysicalRoot + "\\" + SaveLocationDB;
                            if (System.IO.Directory.Exists(SaveLocation) == false)
                            {
                                System.IO.Directory.CreateDirectory(SaveLocation);
                            }

                            List<string> objFiles;
                            if (Session[VState] == null)
                            {
                                objFiles = new List<string>();
                            }
                            else
                            {
                                objFiles = (List<string>)Session[VState];
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
                                NewFileName = fileName.Substring(0, fileName.LastIndexOf('.')) + "_" + NewFileName;
                                NewFileName = objComAct.FormatFileName(NewFileName);
                                SaveLocationFile = SaveLocation + "\\" + NewFileName + "." + fnSP[fnSP.Length - 1];
                                SaveLocationDBFile = SaveLocationDB + "\\" + NewFileName + "." + fnSP[fnSP.Length - 1];

                                file.SaveAs(SaveLocationFile);
                                objFiles.Add(SaveLocationDBFile);
                            }
                            if (Convert.ToInt32(TotalFiles) == objFiles.Count)
                            {
                                string ResponseText = "";
                                foreach (string file in objFiles)
                                {
                                    ResponseText = ResponseText + file + "\r\n";
                                }
                                Response.Write(ResponseText + "^");
                                Session.Remove(VState);
                            }
                            else
                            {
                                Session[VState] = objFiles;
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
        }
    }
}