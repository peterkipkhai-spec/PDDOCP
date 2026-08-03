using System;

namespace PDDOCP
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Send logged in members to the dashboard, otherwise send visitors to login.
            if (Session["mId"] == null) Response.Redirect("Login.aspx");
            else Response.Redirect("ManageProgress.aspx");
        }
    }
}
