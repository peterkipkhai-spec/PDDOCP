// This class contains only calculation logic.
// value1, value2 and value3 are interpreted differently depending on the activity selected.
public class CalorieCalculator
{
    public double calculateWalking(double value1, double value2, double value3)
    {
        return value1 * 0.04 + value2 * 20 + value3 * 3.5;
    }

    public double calculateSwimming(double value1, double value2, double value3)
    {
        return value1 * 10 + value2 * 6 + value3 * 0.5;
    }

    public double calculateJogging(double value1, double value2, double value3)
    {
        return value1 * 60 + value2 * 7 + value3 * 0.3;
    }

    public double calculateTaiChi(double value1, double value2, double value3)
    {
        return value1 * 3.5 + value2 * 1.5 + value3 * 0.25;
    }

    public double calculateGolf(double value1, double value2, double value3)
    {
        return value1 * 15 + value2 * 3 + value3 * 25;
    }

    public double calculateRollerSkating(double value1, double value2, double value3)
    {
        return value1 * 40 + value2 * 5 + value3 * 2;
    }

    public double calculateCalories(string activityName, double value1, double value2, double value3)
    {
        if (activityName == "Walking") return calculateWalking(value1, value2, value3);
        if (activityName == "Swimming") return calculateSwimming(value1, value2, value3);
        if (activityName == "Jogging") return calculateJogging(value1, value2, value3);
        if (activityName == "Tai Chi") return calculateTaiChi(value1, value2, value3);
        if (activityName == "Golf") return calculateGolf(value1, value2, value3);
        if (activityName == "Roller Skating") return calculateRollerSkating(value1, value2, value3);
        return 0;
    }
}
