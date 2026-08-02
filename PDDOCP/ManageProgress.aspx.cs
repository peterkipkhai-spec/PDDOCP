using System;
using System.Data;

namespace PDDOCP
{
    public partial class ManageProgress : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.DropDownList ddlActivity;
        protected global::System.Web.UI.WebControls.TextBox txtValue1, txtValue2, txtValue3;
        protected global::System.Web.UI.WebControls.Label lblMessage, lblTotal;
        protected global::System.Web.UI.WebControls.GridView gvProgress;
        protected void Page_Load(object sender, EventArgs e) { if (Session["mId"] == null) { Response.Write("<script>location.assign('Login.aspx');</script>"); return; } if (!IsPostBack) loadData(); }
        private void loadData() { DBHelper db = new DBHelper(); ddlActivity.DataSource = db.getActivities(); ddlActivity.DataTextField = "activityName"; ddlActivity.DataValueField = "activityId"; ddlActivity.DataBind(); loadProgress(); }
        private void loadProgress() { DBHelper db = new DBHelper(); int memberId = Convert.ToInt32(Session["mId"]); gvProgress.DataSource = db.getProgressByMember(memberId); gvProgress.DataBind(); lblTotal.Text = db.getTotalCaloriesByMember(memberId).ToString("0.00"); }
        protected void ddlActivity_SelectedIndexChanged(object sender, EventArgs e) { }
        protected void btnRecord_Click(object sender, EventArgs e)
        {
            try
            {
                DBHelper db = new DBHelper();
                int activityId = Convert.ToInt32(ddlActivity.SelectedValue);
                DataTable metrics = db.getActivityMetricsByActivity(activityId);
                if (metrics.Rows.Count == 0) { lblMessage.Text = "Please link metrics for this activity first."; return; }
                double calories = new CalorieCalculator().calculateCalories(ddlActivity.SelectedItem.Text, Convert.ToDouble(txtValue1.Text), Convert.ToDouble(txtValue2.Text), Convert.ToDouble(txtValue3.Text));
                int activityMetricId = Convert.ToInt32(metrics.Rows[0]["activityMetricId"]);
                WorkoutSession session = new WorkoutSession(0, activityMetricId, Convert.ToInt32(Session["mId"]), calories, DateTime.Now);
                db.insertWorkoutSession(session);
                loadProgress();
                lblMessage.Text = "Workout recorded. Calories: " + calories.ToString("0.00");
            }
            catch (Exception ex) { lblMessage.Text = ex.Message; }
        }
    }
}
