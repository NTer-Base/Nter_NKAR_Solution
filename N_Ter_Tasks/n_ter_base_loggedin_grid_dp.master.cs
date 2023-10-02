using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;

namespace N_Ter_Tasks
{
    public partial class n_ter_base_loggedin_grid_dp : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BuildWFDropdowns();
        }

        private void BuildWFDropdowns()
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Document_Projects objDP = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
            DS_Doc_Project dsDP = objDP.ReadAll();

            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();

            int Repo_ID = 0;
            if (Request.QueryString["rid"] == null)
            {
                Repo_ID = dsDP.tbldocument_project[0].Document_Project_ID;
                Response.Redirect(Request.Path + "?rid=" + objURL.Encrypt(Convert.ToString(Repo_ID)));
            }
            else
            {
                ltrOtherRepos.Text = "";
                ltrSelectedRepo.Text = "";
                Repo_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["rid"])));
                foreach (DS_Doc_Project.tbldocument_projectRow row in dsDP.tbldocument_project)
                {
                    if (row.Document_Project_ID == Repo_ID)
                    {
                        DS_Doc_Project dsDPAct = objDP.ReadActionCounts(row.Document_Project_ID);

                        N_Ter.Common.Common_Actions objComAct = new N_Ter.Common.Common_Actions();
                        ltrSelectedRepo.Text = row.Doc_Project_Name;
                        ltrActionMenu.Text = objComAct.GetDocRepoActionMenu(objURL.Encrypt(row.Document_Project_ID.ToString()), " btn-labeled", " btn-primary", " pull-right", dsDPAct.tblaction_counts[0]);
                    }
                    else
                    {
                        ltrOtherRepos.Text = ltrOtherRepos.Text + "<li><a href=\"" + Request.Path + "?rid=" + objURL.Encrypt(Convert.ToString(row.Document_Project_ID)) + "\">" + row.Doc_Project_Name + "</span></a></li>";
                    }
                }
            }

            if (ltrOtherRepos.Text.Trim() == "")
            {
                ltrOtherRepos.Text = "<li style='padding: 0 10px'>No Other Document Repos</li>";
            }
        }

        public void BuildAlerts()
        {
            n_ter_base_loggedin_grid pg = (n_ter_base_loggedin_grid)Master;
            SessionObject objSes = (SessionObject)Session["dt"];
            pg.BuildAlerts(objSes);
        }

        public string PageClass
        {
            set
            {
                n_ter_base_loggedin_grid pg = (n_ter_base_loggedin_grid)this.Master;
                pg.PageClass = value;
            }
        }
    }
}