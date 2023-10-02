using N_Ter.Structures;
using System;


namespace N_Ter_Tasks
{
    public partial class n_ter_base_loggedin_grid : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void BuildAlerts(SessionObject objSes)
        {
            n_ter_base_loggedin pg = (n_ter_base_loggedin)Master;
            pg.BuildAlerts(objSes);
        }

        public string PageClass
        {
            set
            {
                n_ter_base_loggedin pg = (n_ter_base_loggedin)this.Master;
                pg.PageClass = value;
            }
        }

        public bool ShowHelp
        {
            set
            {
                n_ter_base_loggedin pg = (n_ter_base_loggedin)this.Master;
                pg.ShowHelp = value;
            }
        }
    }
}