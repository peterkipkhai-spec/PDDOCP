public class ActivityMetric
{
    private int activityMetricId;
    private int activityId;
    private int metricId;
    private double baseMultiplier;

    public ActivityMetric(int activityMetricId, int activityId, int metricId, double baseMultiplier)
    {
        this.activityMetricId = activityMetricId;
        this.activityId = activityId;
        this.metricId = metricId;
        this.baseMultiplier = baseMultiplier;
    }

    public int getActivityMetricId() { return activityMetricId; }
    public void setActivityMetricId(int activityMetricId) { this.activityMetricId = activityMetricId; }
    public int getActivityId() { return activityId; }
    public void setActivityId(int activityId) { this.activityId = activityId; }
    public int getMetricId() { return metricId; }
    public void setMetricId(int metricId) { this.metricId = metricId; }
    public double getBaseMultiplier() { return baseMultiplier; }
    public void setBaseMultiplier(double baseMultiplier) { this.baseMultiplier = baseMultiplier; }
}
