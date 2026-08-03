public class MetricType
{
    private int metricId;
    private string metricName;

    public MetricType(int metricId, string metricName)
    {
        this.metricId = metricId;
        this.metricName = metricName;
    }

    public int getMetricId() { return metricId; }
    public void setMetricId(int metricId) { this.metricId = metricId; }
    public string getMetricName() { return metricName; }
    public void setMetricName(string metricName) { this.metricName = metricName; }
}
