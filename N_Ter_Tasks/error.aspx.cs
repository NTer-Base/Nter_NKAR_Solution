using System;

namespace N_Ter_Tasks
{
    public partial class error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["eid"]))
            {
                lblErrorMessage.Text = "Oops.. Something happend. Please try the function again.";
            }
            else
            {
                int ErrorId = Convert.ToInt32(Request.QueryString["eid"]);
                if (ErrorId == 1)
                {
                    lblErrorMessage.Text = "Document cannot be found..";
                }
            }
        }
    }
}