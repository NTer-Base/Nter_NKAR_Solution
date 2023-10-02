using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Linq;
using System.Web.UI;

namespace N_Ter_Tasks
{
    public partial class n_ter_base_loggedin_grid_wf : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            BuildWFDropdowns();
        }

        private void BuildWFDropdowns()
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            DS_Workflow dsCat = ObjectCreator.GetWorkflow_Categories(objSes.Connection, objSes.DB_Type).ReadAllActive();
            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            DS_Workflow dsWF = objWF.ReadAll();

            if (dsCat.tblworkflow_categories.Rows.Count > 0 && dsWF.tblwalkflow.Rows.Count > 0)
            {
                ltrOtherCategories.Text = "";
                ltrOtherWorkflows.Text = "";

                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();

                DS_Workflow.tblwalkflowRow[] drWF;

                int Workflow_Cat_ID = 0;
                if (Request.QueryString["fid"] == null)
                {
                    Workflow_Cat_ID = dsCat.tblworkflow_categories[0].Workflow_Category_ID;
                    drWF = dsWF.tblwalkflow.Where(x => x.Workflow_Category_ID == Workflow_Cat_ID).ToArray();
                    Response.Redirect(Request.Path + "?fid=" + objURL.Encrypt(Convert.ToString(drWF[0].Walkflow_ID)));
                }
                else
                {
                    int Workflow_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["fid"])));
                    drWF = dsWF.tblwalkflow.Where(x => x.Walkflow_ID == Workflow_ID).ToArray();
                    if (drWF.Length > 0)
                    {
                        Workflow_Cat_ID = drWF[0].Workflow_Category_ID;
                    }

                    foreach (DS_Workflow.tblworkflow_categoriesRow row in dsCat.tblworkflow_categories)
                    {
                        if (row.Workflow_Category_ID == Workflow_Cat_ID)
                        {
                            ltrSelectedCategory.Text = row.Workflow_Category_Name;
                        }
                        else
                        {
                            ltrOtherCategories.Text = ltrOtherCategories.Text + "<li><a href=\"" + Request.Path + "?fid=" + objURL.Encrypt(Convert.ToString(dsWF.tblwalkflow.Where(x => x.Workflow_Category_ID == row.Workflow_Category_ID).First().Walkflow_ID)) + "\">" + row.Workflow_Category_Name + "</a></li>";

                        }
                    }

                    foreach (DS_Workflow.tblwalkflowRow row in dsWF.tblwalkflow.Where(x => x.Workflow_Category_ID == Workflow_Cat_ID))
                    {
                        if (row.Walkflow_ID == Workflow_ID)
                        {
                            DS_Workflow dsWFAct = objWF.ReadActionCounts(row.Walkflow_ID);
                            N_Ter.Common.Common_Actions objComAct = new N_Ter.Common.Common_Actions();
                            ltrSelectedWorkflow.Text = row.Workflow_Name;
                            ltrActionMenu.Text = objComAct.GetWorkflowActionMenu(objURL.Encrypt(row.Walkflow_ID.ToString()), row.Walkflow_ID, row.Workflow_Name.Replace("'", "\\'"), " btn-labeled", " btn-primary", " pull-right", dsWFAct.tblaction_counts[0]);
                        }
                        else
                        {
                            ltrOtherWorkflows.Text = ltrOtherWorkflows.Text + "<li><a href=\"" + Request.Path + "?fid=" + objURL.Encrypt(Convert.ToString(row.Walkflow_ID)) + "\">" + row.Workflow_Name + "</span></a></li>";
                        }
                    }
                }

                if (ltrOtherCategories.Text.Trim() == "")
                {
                    ltrOtherCategories.Text = "<li style='padding: 0 10px'>No Other Categories</li>";
                }
                if (ltrOtherWorkflows.Text.Trim() == "")
                {
                    ltrOtherWorkflows.Text = "<li style='padding: 0 10px'>No Other Workflows</li>";
                }
            }
            else
            {
                Response.Redirect("error.aspx?");
            }
        }

        protected void cmdCreateDuplicate_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            int Workflow_ID = Convert.ToInt32(hndDuplicate.Value);

            Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
            if (objWF.CreateDuplicateWorkflow(Workflow_ID, txtNewWorkflowName.Text))
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKeyM1", "ShowSuccess('Duplicate Workflow Successfully Updated');", true);
                hndDuplicate.Value = "0";
                BuildWFDropdowns();
            }
            else
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKeyM1", "ShowError('Duplicate Workflow could not be saved');", true);
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