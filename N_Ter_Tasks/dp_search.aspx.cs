using N_Ter.Base;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class dp_search : System.Web.UI.Page
    {
        private DP_Controls_Main objDPCtrlList = new DP_Controls_Main();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            if (IsPostBack == false)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["dpid"]))
                {
                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                    int Doc_Repo_ID = Convert.ToInt32(objURL.Decrypt(Convert.ToString(Request.QueryString["dpid"])));

                    if (ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type).Can_Access_Repo(Doc_Repo_ID, objSes.UserID))
                    {
                        hndDoc_Proj_ID.Value = Doc_Repo_ID.ToString();
                    }
                    else
                    {
                        hndDoc_Proj_ID.Value = "0";
                    }
                }
                else
                {
                    hndDoc_Proj_ID.Value = "0";
                }

                LoadEL2s(objSes);
            }

            ltrEL2.Text = objSes.EL2;
            RefreshGrid(objSes);

            if (!IsPostBack)
            {
                PerformSearch();
            }
        }

        private void LoadEL2s(SessionObject objSes)
        {
            Entity_Level_2 objEL2 = ObjectCreator.GetEntity_Level_2(objSes.Connection, objSes.DB_Type);
            DS_Entity_Level_2 ds = objEL2.ReadForUser(objSes.UserID);

            if (ds.tblentity_level_2.Rows.Count > 0)
            {
                cboEL2Name.DataSource = ds.tblentity_level_2;
                cboEL2Name.DataValueField = "Entity_L2_ID";
                cboEL2Name.DataTextField = "Display_Name";
                cboEL2Name.DataBind();
            }
            else
            {
                cboEL2Name.Items.Add(new ListItem("", "0"));
            }
        }

        private void RefreshGrid(SessionObject objSes)
        {
            Document_Projects objDoc = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
            DS_Doc_Project ds = objDoc.ReadForUser(objSes.UserID);

            ltrDocProjects.Text = "";
            N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();

            if (ds.tbldocument_project.Rows.Count > 0)
            {
                if (hndDoc_Proj_ID.Value == "0")
                {
                    hndDoc_Proj_ID.Value = Convert.ToString(ds.tbldocument_project[0].Document_Project_ID);
                }
            }

            foreach (DS_Doc_Project.tbldocument_projectRow row in ds.tbldocument_project)
            {
                if (Convert.ToInt32(hndDoc_Proj_ID.Value) == row.Document_Project_ID)
                {
                    ltrDocProjects.Text = ltrDocProjects.Text + "<a href=\"dp_search.aspx?dpid=" + objURL.Encrypt(Convert.ToString(row.Document_Project_ID)) + "\" class=\"list-group-item active\">" + "\r\n" +
                                                        "<h4 class=\"list-group-item-heading text-thin\">" + row.Doc_Project_Name + "</h4>" + "\r\n" +
                                                    "</a>" + "\r\n";
                }
                else
                {
                    ltrDocProjects.Text = ltrDocProjects.Text + "<a href=\"dp_search.aspx?dpid=" + objURL.Encrypt(Convert.ToString(row.Document_Project_ID)) + "\" class=\"list-group-item\">" + "\r\n" +
                                                        "<h4 class=\"list-group-item-heading text-thin\">" + row.Doc_Project_Name + "</h4>" + "\r\n" +
                                                    "</a>" + "\r\n";
                }
            }

            if (Convert.ToInt32(hndDoc_Proj_ID.Value) > 0)
            {
                pnlDP.Visible = true;
                LoadTagDetails(Convert.ToInt32(hndDoc_Proj_ID.Value), objSes);
            }
            else
            {
                pnlDP.Visible = false;
            }
        }

        private void LoadTagDetails(int Document_Project_ID, SessionObject objSes)
        {
            Document_Projects objDP = ObjectCreator.GetDocument_Projects(objSes.Connection, objSes.DB_Type);
            DS_Doc_Project ds = objDP.Read(Document_Project_ID);

            Common_Doc_Actions objComDoc = new Common_Doc_Actions();

            ControlTypes objCtrl = ControlTypes.LabelOnly;

            foreach (DS_Doc_Project.tbldocument_project_indexesRow row in ds.tbldocument_project_indexes)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divControl.Attributes.Add("class", "row padding-xs-vr");
                divControl.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                divControl.ID = "divDocControl" + row.Document_Project_Index_ID;

                System.Web.UI.HtmlControls.HtmlGenericControl divLabel = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divLabel.Attributes.Add("class", "col-md-3");
                divLabel.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                divLabel.ID = "divLabel" + row.Document_Project_Index_ID;
                Label lblTitle = new Label();
                lblTitle.Text = row.Tag_Name;
                divLabel.Controls.Add(lblTitle);
                divControl.Controls.Add(divLabel);

                System.Web.UI.HtmlControls.HtmlGenericControl divCondition = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divCondition.Attributes.Add("class", "col-md-3");
                divCondition.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                divCondition.ID = "divCondition" + row.Document_Project_Index_ID;

                System.Web.UI.HtmlControls.HtmlGenericControl divValue = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divValue.Attributes.Add("class", "col-md-6");
                divValue.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                divValue.ID = "divValue" + row.Document_Project_Index_ID;

                DP_Controls drUI = new DP_Controls();
                drUI.Document_Project_Index_ID = row.Document_Project_Index_ID;

                objCtrl = (ControlTypes)row.Tag_Type;
                drUI.Tag_Type = objCtrl;

                objComDoc.PrepareFilterControl(row, objCtrl, ref divValue, ref divCondition, ref drUI);

                objDPCtrlList.Controls.Add(drUI);

                divControl.Controls.Add(divCondition);
                divControl.Controls.Add(divValue);

                divDocFilters.Controls.Add(divControl);
            }
        }

        protected void cmdFilter_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        protected void cboEL2Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void PerformSearch()
        { 
            //implement
        }
    }
}