public class ActivityType
{
    private int activityId;
    private string activityName;

    public ActivityType(int activityId, string activityName)
    {
        this.activityId = activityId;
        this.activityName = activityName;
    }

    public int getActivityId() { return activityId; }
    public void setActivityId(int activityId) { this.activityId = activityId; }
    public string getActivityName() { return activityName; }
    public void setActivityName(string activityName) { this.activityName = activityName; }
}
