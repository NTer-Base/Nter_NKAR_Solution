using System;
using System.Web.UI.WebControls;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter.Base;

namespace N_Ter_Tasks
{
    public partial class client_docs : System.Web.UI.Page
    {
        public string Loading_Script = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            if (IsPostBack == false)
            {
                ltrEL2.Text = objSes.EL2;
                ltrEL2_2.Text = objSes.EL2;
                ltrEL2_3.Text = objSes.EL2;
                ltrEL2_4.Text = objSes.EL2;
                gvEL2Select.Columns[2].HeaderText = objSes.EL2 + " Name";

                if (Request.QueryString["cid"] != null)
                {
                    divFileManager.Visible = true;
                    Loading_Script = "";
                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    int Entity_L2_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["cid"])));

                    Entity_Level_2 objComp = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type);
                    DS_Entity_Level_2.tblentity_level_2Row dr = objComp.Read(Entity_L2_ID);

                    if (objComp.isEL2Available(objSes.UserID, Entity_L2_ID) == true)
                    {
                        lblEL2Name.Text = " - " + dr.Display_Name;
                        string AppServer = objSes.PhysicalRoot + "\\nter_app_uploads\\client_docs";
                        if (System.IO.Directory.Exists(AppServer + "\\" + dr.Folder_Name) == false)
                        {
                            System.IO.Directory.CreateDirectory(AppServer + "\\" + dr.Folder_Name);
                        }
                        fileManagerClients.Settings.RootFolder = "~\\nter_app_uploads\\client_docs\\" + dr.Folder_Name;
                    }
                    else
                    {
                        Response.Redirect("no_access.aspx?");
                    }
                }
                else
                {
                    divFileManager.Visible = false;
                    Loading_Script = "showClientSelect();";
                }
            }
            LoadMyEL2s(objSes);
        }

        private void LoadMyEL2s(SessionObject objSes)
        {
            Entity_Level_2 objEL2 = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type);
            DS_Entity_Level_2 ds = objEL2.ReadForUser(objSes.UserID);
            gvEL2Select.SelectedIndex = -1;
            gvEL2Select.DataSource = ds.tblentity_level_2;
            gvEL2Select.DataBind();
            if (ds.tblentity_level_2.Rows.Count > 0)
            {
                gvEL2Select.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvEL2Select_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].HasControls())
                {
                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    System.Web.UI.HtmlControls.HtmlButton cmdEdit = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[1].Controls[1]);
                    cmdEdit.Attributes.Add("onClick", "return openPage('client_docs.aspx?cid=" + objURL.Encrypt(e.Row.Cells[0].Text.Trim()) + "')");
                }
            }
        }
    }
}