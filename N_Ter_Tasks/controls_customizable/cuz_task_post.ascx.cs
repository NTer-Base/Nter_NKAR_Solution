using N_Ter.Structures;
using System;

namespace N_Ter_Tasks.controls_customizable
{
    public partial class cuz_task_post : System.Web.UI.UserControl
    {
        private Task_Post_Elements _PostElements = new Task_Post_Elements();

        protected void Page_Init(object sender, System.EventArgs e) 
        {
            SessionObject objSes = (SessionObject)Session["dt"];

            N_Ter.Customizable.UI.Task_Posts objPost = new N_Ter.Customizable.UI.Task_Posts();
            objPost.PostElements = _PostElements;
            objPost.GenerateScreen(objSes, IsPostBack, Page);
            _PostElements = objPost.PostElements;
            pnlTaskPost.Controls.Clear();
            pnlTaskPost.Controls.Add(objPost.Display);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public Task_Post_Elements PostElements
        {
            get
            {
                return _PostElements;
            }
        }
    }
}