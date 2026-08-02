using System;

namespace PDDOCP
{
    public partial class Login : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.TextBox txtName;
        protected global::System.Web.UI.WebControls.TextBox txtPassword;
        protected global::System.Web.UI.WebControls.Label lblMessage;

        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim();
                string password = txtPassword.Text.Trim();

                // Ask DBHelper to check the username and password using a parameterized query.
                DBHelper db = new DBHelper();
                Member member = db.loginMember(name, password);
                if (member != null)
                {
                    // Successful login resets the static failed counter and stores the member id in Session.
                    Data.clearFailedAttempt();
                    Session["mId"] = member.getMId();
                    Session["name"] = member.getName();
                    Response.Redirect("ManageProgress.aspx");
                }
                else
                {
                    // Failed attempts are tracked in Data. After 3 attempts Data pauses for 3 seconds.
                    Data.addFailedAttempt(name);
                    lblMessage.Text = "Invalid login. Failed attempts: " + Data.getFailedLoginCount();
                }
            }
            catch (Exception ex) { lblMessage.Text = ex.Message; }
        }
    }
}
