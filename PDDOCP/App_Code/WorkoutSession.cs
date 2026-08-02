using System;

public class WorkoutSession
{
    private int sessionId;
    private int activityMetricId;
    private int memberId;
    private double recordedValue;
    private DateTime sessionDate;

    public WorkoutSession(int sessionId, int activityMetricId, int memberId, double recordedValue, DateTime sessionDate)
    {
        this.sessionId = sessionId;
        this.activityMetricId = activityMetricId;
        this.memberId = memberId;
        this.recordedValue = recordedValue;
        this.sessionDate = sessionDate;
    }

    public int getSessionId() { return sessionId; }
    public void setSessionId(int sessionId) { this.sessionId = sessionId; }
    public int getActivityMetricId() { return activityMetricId; }
    public void setActivityMetricId(int activityMetricId) { this.activityMetricId = activityMetricId; }
    public int getMemberId() { return memberId; }
    public void setMemberId(int memberId) { this.memberId = memberId; }
    public double getRecordedValue() { return recordedValue; }
    public void setRecordedValue(double recordedValue) { this.recordedValue = recordedValue; }
    public DateTime getSessionDate() { return sessionDate; }
    public void setSessionDate(DateTime sessionDate) { this.sessionDate = sessionDate; }
}
