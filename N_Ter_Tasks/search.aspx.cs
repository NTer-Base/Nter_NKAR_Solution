using N_Ter.Base;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N_Ter_Tasks
{
    public partial class search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((BoundField)gvTasksSearch.Columns[2]).DataFormatString = "{0:" + Constants.DateFormat + " HH:mm}";
            ((BoundField)gvTasksSearch.Columns[7]).DataFormatString = "{0:" + Constants.DateFormat + " HH:mm}";
            ((BoundField)gvDocumentsSearch.Columns[5]).DataFormatString = "{0:" + Constants.DateFormat + " HH:mm}";

            if (IsPostBack == false)
            {
                SessionObject objSes = (SessionObject)Session["dt"];
                gvTasksSearch.Columns[5].HeaderText = objSes.EL2 + " Name";
                gvDocumentsSearch.Columns[5].HeaderText = objSes.EL2 + " Name";
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                string Search = objURL.Decrypt(Convert.ToString(Request.QueryString["s"]));
                ltrSearch.Text = "'" + Search + "'";
                SearchTasks(Search, objSes);
            }
        }

        private void SearchTasks(string Criteria, SessionObject objSes)
        {
            if (Criteria.Trim() == "")
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowWarning('Your Search Criteria is Empty');", true);
            }
            else
            {
                Tasks objTasks = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                DS_Tasks ds = objTasks.Search(Criteria, objSes.UserID, ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type));

                Documents objDocs = ObjectCreator.GetDocuments(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                DS_Documents dsDoc = objDocs.Search(Criteria, objSes.UserID);

                if (ds.tbltasks.Rows.Count == 0 && dsDoc.tbldocuments.Rows.Count == 0)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowWarning('There are no results for your search criteria');", true);
                }
                else if (ds.tbltasks.Rows.Count > 500 || dsDoc.tbldocuments.Rows.Count > 500)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowWarning('Your Search Resulted in more than 500 tasks. Please narrow your search');", true);
                }
                else
                {
                    foreach (DS_Tasks.tbltasksRow row in ds.tbltasks)
                    {
                        switch (row.Current_Step_ID)
                        {
                            case -1:
                                row.Step_Status = "Completed";
                                break;
                            case -2:
                                row.Step_Status = "Cancelled";
                                break;
                        }
                    }
                    gvTasksSearch.DataSource = ds.tbltasks;
                    gvTasksSearch.DataBind();
                    if (ds.tbltasks.Rows.Count > 0)
                    {
                        gvTasksSearch.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    gvDocumentsSearch.DataSource = dsDoc.tbldocuments;
                    gvDocumentsSearch.DataBind();
                    if (dsDoc.tbldocuments.Rows.Count > 0)
                    {
                        gvDocumentsSearch.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
            }

        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                if (e.Row.Cells[1].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdUpdate = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[1].Controls[1]);
                    cmdUpdate.Attributes.Add("onclick", "window.open('task_info.aspx?tid=" + objURL.Encrypt(e.Row.Cells[0].Text) + "', '_self'); return false;");
                }
            }
        }

        protected void gvDocumentsSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();
                if (e.Row.Cells[1].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdView = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[1].Controls[1]);
                    cmdView.Attributes.Add("onclick", "window.open('document.aspx?did=" + objURL.Encrypt(e.Row.Cells[0].Text) + "', '_self'); return false;");
                }
                if (e.Row.Cells[2].HasControls())
                {
                    System.Web.UI.HtmlControls.HtmlButton cmdInfo = ((System.Web.UI.HtmlControls.HtmlButton)e.Row.Cells[2].Controls[1]);
                    cmdInfo.Attributes.Add("onclick", "return showAttachInfo(" + e.Row.Cells[0].Text + ");");
                }
            }
        }
    }
}