using N_Ter.Structures;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using N_Ter.Base;

namespace N_Ter_Tasks
{
    public partial class direct_db : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                divQuery.Visible = false;
            }
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtStatement.Text = "";
            divQuery.Visible = false;
        }

        protected void cmdExecute_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Direct_DB objDB = ObjectCreator.GetDirect_DB(objSes.Connection, objSes.DB_Type);

            divQuery.Visible = false;

            if (txtStatement.Text.ToLower().StartsWith("select"))
            {
                DataSet ds = new DataSet();
                ActionDone Act = objDB.ExecuteQuery(txtStatement.Text, ref ds);
                if (Act.Done == true)
                {
                    if (ds.Tables.Count > 0)
                    {
                        gvMain.DataSource = ds.Tables[0];
                        gvMain.DataBind();

                        divQuery.Visible = true;

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            gvMain.HeaderRow.TableSection = TableRowSection.TableHeader;
                        }
                    }
                    else
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('No Data to Display');", true);
                    }
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError(\"Query could not be Executed - " + Act.Reason + "\");", true);
                }
            }
            else
            {
                ActionDone Act = objDB.ExecuteNonQuery(txtStatement.Text);
                if (Act.Done == true)
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Query Executed Successfully');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError(\"Query could not be Executed - " + Act.Reason + "\");", true);
                }
            }
        }
    }
}