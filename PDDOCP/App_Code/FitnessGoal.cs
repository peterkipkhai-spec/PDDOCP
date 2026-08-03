using System;

public class FitnessGoal
{
    private int goalId;
    private int targetCalories;
    private int memberId;
    private DateTime createdDate;

    public FitnessGoal(int goalId, int targetCalories, int memberId, DateTime createdDate)
    {
        this.goalId = goalId;
        this.targetCalories = targetCalories;
        this.memberId = memberId;
        this.createdDate = createdDate;
    }

    public int getGoalId() { return goalId; }
    public void setGoalId(int goalId) { this.goalId = goalId; }
    public int getTargetCalories() { return targetCalories; }
    public void setTargetCalories(int targetCalories) { this.targetCalories = targetCalories; }
    public int getMemberId() { return memberId; }
    public void setMemberId(int memberId) { this.memberId = memberId; }
    public DateTime getCreatedDate() { return createdDate; }
    public void setCreatedDate(DateTime createdDate) { this.createdDate = createdDate; }
}
