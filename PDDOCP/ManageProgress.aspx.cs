using System;
using System.Data;

namespace PDDOCP
{
    public partial class ManageProgress : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.DropDownList ddlActivity;
        protected global::System.Web.UI.WebControls.TextBox txtValue1, txtValue2, txtValue3;
        protected global::System.Web.UI.WebControls.Label lblMessage, lblTotal, lblTarget, lblGoalStatus;
        protected global::System.Web.UI.WebControls.GridView gvProgress;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Protected page: redirect unauthenticated users back to Login.aspx.
            if (Session["mId"] == null) { Response.Write("<script>location.assign('Login.aspx');</script>"); return; }
            if (!IsPostBack) loadData();
        }

        private void loadData()
        {
            DBHelper db = new DBHelper();
            ddlActivity.DataSource = db.getActivities(); ddlActivity.DataTextField = "activityName"; ddlActivity.DataValueField = "activityId"; ddlActivity.DataBind();
            loadProgress();
        }

        private void loadProgress()
        {
            // Compare total calories with the latest target goal and report achievement clearly.
            DBHelper db = new DBHelper();
            int memberId = Convert.ToInt32(Session["mId"]);
            double totalCalories = db.getTotalCaloriesByMember(memberId);
            int targetCalories = db.getLatestTargetCaloriesByMember(memberId);
            gvProgress.DataSource = db.getProgressByMember(memberId); gvProgress.DataBind();
            lblTotal.Text = totalCalories.ToString("0.00");
            lblTarget.Text = targetCalories.ToString();
            if (targetCalories > 0 && totalCalories >= targetCalories) lblGoalStatus.Text = "Goal Achieved!";
            else lblGoalStatus.Text = "Goal Not Yet Reached";
        }

        protected void ddlActivity_SelectedIndexChanged(object sender, EventArgs e) { }

        protected void btnRecord_Click(object sender, EventArgs e)
        {
            try
            {
                DBHelper db = new DBHelper();
                int activityId = Convert.ToInt32(ddlActivity.SelectedValue);
                DataTable metrics = db.getActivityMetricsByActivity(activityId);
                if (metrics.Rows.Count == 0) { lblMessage.Text = "Please link metrics for this activity first."; return; }

                // Keep the three raw user inputs and also calculate calories for progress reporting.
                double value1 = Convert.ToDouble(txtValue1.Text);
                double value2 = Convert.ToDouble(txtValue2.Text);
                double value3 = Convert.ToDouble(txtValue3.Text);
                double calories = new CalorieCalculator().calculateCalories(ddlActivity.SelectedItem.Text, value1, value2, value3);
                int activityMetricId = Convert.ToInt32(metrics.Rows[0]["activityMetricId"]);
                WorkoutSession session = new WorkoutSession(0, activityMetricId, Convert.ToInt32(Session["mId"]), value1, value2, value3, calories, DateTime.Now);
                db.insertWorkoutSession(session);
                loadProgress();
                lblMessage.Text = "Workout recorded. Calories: " + calories.ToString("0.00");
            }
            catch (Exception ex) { lblMessage.Text = ex.Message; }
        }

        protected void btnLogout_Click(object sender, EventArgs e) { Session.Clear(); Session.Abandon(); Response.Redirect("Login.aspx"); }
    }
}
