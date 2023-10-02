using N_Ter.Base;
using N_Ter.PeriodicalAutomations;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class read_folders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                SessionObject objSes = (SessionObject)Session["dt"];
                gvRules.Columns[2].HeaderText = objSes.EL2;

                RefreshGrids(objSes);
            }
        }

        private void RefreshGrids(SessionObject objSes)
        {
            Workflow_Folder_Read_Rules objRule = ObjectCreator.GetWorkflow_Folder_Read_Rules(objSes.Connection, objSes.DB_Type);
            DS_WF_Folder_Read_Rules ds2 = objRule.ReadAllInfo();
            gvRules.SelectedIndex = -1;
            gvRules.DataSource = ds2.tblworkflow_folder_read_rules;
            gvRules.DataBind();
            if (ds2.tblworkflow_folder_read_rules.Rows.Count > 0)
            {
                gvRules.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            if (ds2.tblworkflow_folder_read_rules.Rows.Count == 0)
            {
                ltrStatus.Text = "There are no Rules to Create Tasks after Reading Folders";
                divSubmit.Visible = false;
            }
            else
            {
                ltrStatus.Text = "All Unread Files in all below Folders will be read and all rules associalted in folder will be executed.";
            }

            divError.Visible = false;
        }

        protected void cmdReceiveHide_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            string WebFolderRoot = ConfigurationManager.AppSettings["FolderReadRoot"].ToString();
            string PhysicalFolderRoot = Server.MapPath(WebFolderRoot);

            Automations objAuto = new Automations();
            System.Threading.Thread objThread1 = new System.Threading.Thread(delegate () { objAuto.ReadFolders(objSes, PhysicalFolderRoot); });
            objThread1.Priority = System.Threading.ThreadPriority.BelowNormal;
            objThread1.Start();

            RefreshGrids(objSes);

            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Force Read Folders Initiated');", true);
        }
    }
}