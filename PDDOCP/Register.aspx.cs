using System;
using System.Text.RegularExpressions;

namespace PDDOCP
{
    public partial class Register : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.TextBox txtName;
        protected global::System.Web.UI.WebControls.TextBox txtPassword;
        protected global::System.Web.UI.WebControls.Label lblMessage;

        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim();
                string password = txtPassword.Text.Trim();
                if (!Regex.IsMatch(name, "^[a-zA-Z0-9]+$")) { lblMessage.Text = "Username must be alphanumeric."; return; }
                if (password.Length != 12 || !Regex.IsMatch(password, "[A-Z]") || !Regex.IsMatch(password, "[a-z]")) { lblMessage.Text = "Password must be exactly 12 characters and contain uppercase and lowercase letters."; return; }
                DBHelper db = new DBHelper();
                int result = db.registerMember(new Member(0, name, password));
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = result > 0 ? "Registration successful. Please login." : "Registration failed.";
            }
            catch (Exception ex) { lblMessage.Text = ex.Message; }
        }
    }
}
