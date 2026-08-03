using System;

namespace PDDOCP
{
    public partial class ManageGoal : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.TextBox txtTargetCalories;
        protected global::System.Web.UI.WebControls.Label lblMessage;
        protected global::System.Web.UI.WebControls.GridView gvGoals;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Protected page: check the Session exactly as required by the assignment.
            if (Session["mId"] == null) { Response.Write("<script>location.assign('Login.aspx');</script>"); return; }
            if (!IsPostBack) loadGoals();
        }

        private void loadGoals() { DBHelper db = new DBHelper(); gvGoals.DataSource = db.getGoalsByMember(Convert.ToInt32(Session["mId"])); gvGoals.DataBind(); }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Save one target calorie record for this member.
                FitnessGoal goal = new FitnessGoal(0, Convert.ToInt32(txtTargetCalories.Text), Convert.ToInt32(Session["mId"]), DateTime.Now);
                new DBHelper().insertGoal(goal);
                loadGoals();
                lblMessage.Text = "Goal saved.";
            }
            catch (Exception ex) { lblMessage.Text = ex.Message; }
        }

        protected void btnLogout_Click(object sender, EventArgs e) { Session.Clear(); Session.Abandon(); Response.Redirect("Login.aspx"); }
    }
}
