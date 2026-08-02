using System;

// This model represents one workout entry made by a member.
// It stores the three raw metric values entered by the user and the final calculated calories.
public class WorkoutSession
{
    private int sessionId;
    private int activityMetricId;
    private int memberId;
    private double value1;
    private double value2;
    private double value3;
    private double caloriesBurned;
    private DateTime sessionDate;

    public WorkoutSession(int sessionId, int activityMetricId, int memberId, double value1, double value2, double value3, double caloriesBurned, DateTime sessionDate)
    {
        this.sessionId = sessionId;
        this.activityMetricId = activityMetricId;
        this.memberId = memberId;
        this.value1 = value1;
        this.value2 = value2;
        this.value3 = value3;
        this.caloriesBurned = caloriesBurned;
        this.sessionDate = sessionDate;
    }

    public int getSessionId() { return sessionId; }
    public void setSessionId(int sessionId) { this.sessionId = sessionId; }
    public int getActivityMetricId() { return activityMetricId; }
    public void setActivityMetricId(int activityMetricId) { this.activityMetricId = activityMetricId; }
    public int getMemberId() { return memberId; }
    public void setMemberId(int memberId) { this.memberId = memberId; }
    public double getValue1() { return value1; }
    public void setValue1(double value1) { this.value1 = value1; }
    public double getValue2() { return value2; }
    public void setValue2(double value2) { this.value2 = value2; }
    public double getValue3() { return value3; }
    public void setValue3(double value3) { this.value3 = value3; }
    public double getCaloriesBurned() { return caloriesBurned; }
    public void setCaloriesBurned(double caloriesBurned) { this.caloriesBurned = caloriesBurned; }
    public DateTime getSessionDate() { return sessionDate; }
    public void setSessionDate(DateTime sessionDate) { this.sessionDate = sessionDate; }
}
