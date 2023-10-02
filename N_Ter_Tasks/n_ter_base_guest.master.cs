using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;

namespace N_Ter_Tasks
{
    public partial class n_ter_base_guest : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, System.EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            DS_Settings dsSett = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type).Read();
            if (dsSett.tblsettings[0].User_Group_Guest > 0)
            {
                DS_Users dsUsr = ObjectCreator.GetUser_Groups(objSes.Connection, objSes.DB_Type).ReadUsers(dsSett.tblsettings[0].User_Group_Guest);
                if (dsUsr.tblusers.Rows.Count > 0)
                {
                    objSes.UserID = -1;
                    objSes.FullName = "Guest User";
                    Session["dt"] = objSes;
                }
                else
                {
                    Response.Redirect("guest_error.aspx?");
                }
            }
            else
            {
                Response.Redirect("guest_error.aspx?");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            n_ter_base pg = (n_ter_base)this.Master;
            pg.PageClass = "main-menu-animated";

            LoadHeader_Footer(objSes);
        }

        private void LoadHeader_Footer(SessionObject objSes)
        {
            DS_Settings ds = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type).Read();
            ltrHeader.Text = ds.tblsettings[0].Guest_Post_Header;
            ltrFooter.Text = ds.tblsettings[0].Guest_Post_Footer;
        }
    }
}