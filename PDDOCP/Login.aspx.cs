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
                if (Data.isLocked(name)) { lblMessage.Text = "Account locked. Please wait 1 minute."; return; }
                DBHelper db = new DBHelper();
                Member member = db.loginMember(name, password);
                if (member != null)
                {
                    Data.clearFailedAttempt(name);
                    Session["mId"] = member.getMId();
                    Session["name"] = member.getName();
                    Response.Redirect("ManageProgress.aspx");
                }
                else
                {
                    Data.addFailedAttempt(name);
                    lblMessage.Text = "Invalid login. Failed attempts: " + Data.getFailedAttempts(name);
                }
            }
            catch (Exception ex) { lblMessage.Text = ex.Message; }
        }
    }
}
