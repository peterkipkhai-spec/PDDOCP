using System;

namespace PDDOCP
{
    public partial class ManageActivity : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.TextBox txtActivity, txtMetric, txtMultiplier;
        protected global::System.Web.UI.WebControls.DropDownList ddlActivity, ddlMetric;
        protected global::System.Web.UI.WebControls.Label lblMessage;
        protected global::System.Web.UI.WebControls.GridView gvActivityMetric;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Protected page: visitors without a valid Session must login first.
            if (Session["mId"] == null) { Response.Write("<script>location.assign('Login.aspx');</script>"); return; }
            if (!IsPostBack) loadData();
        }

        private void loadData()
        {
            // Load dropdowns and the GridView from the database.
            DBHelper db = new DBHelper();
            ddlActivity.DataSource = db.getActivities(); ddlActivity.DataTextField = "activityName"; ddlActivity.DataValueField = "activityId"; ddlActivity.DataBind();
            ddlMetric.DataSource = db.getMetrics(); ddlMetric.DataTextField = "metricName"; ddlMetric.DataValueField = "metricId"; ddlMetric.DataBind();
            gvActivityMetric.DataSource = db.getActivityMetrics(); gvActivityMetric.DataBind();
        }

        protected void btnAddActivity_Click(object sender, EventArgs e) { try { new DBHelper().insertActivity(txtActivity.Text.Trim()); loadData(); lblMessage.Text = "Activity added."; } catch (Exception ex) { lblMessage.Text = ex.Message; } }
        protected void btnAddMetric_Click(object sender, EventArgs e) { try { new DBHelper().insertMetric(txtMetric.Text.Trim()); loadData(); lblMessage.Text = "Metric added."; } catch (Exception ex) { lblMessage.Text = ex.Message; } }
        protected void btnLink_Click(object sender, EventArgs e) { try { ActivityMetric am = new ActivityMetric(0, Convert.ToInt32(ddlActivity.SelectedValue), Convert.ToInt32(ddlMetric.SelectedValue), Convert.ToDouble(txtMultiplier.Text)); new DBHelper().insertActivityMetric(am); loadData(); lblMessage.Text = "Activity metric linked."; } catch (Exception ex) { lblMessage.Text = ex.Message; } }
        protected void btnLogout_Click(object sender, EventArgs e) { Session.Clear(); Session.Abandon(); Response.Redirect("Login.aspx"); }
    }
}
