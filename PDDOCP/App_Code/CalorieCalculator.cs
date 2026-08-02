using System;

public class CalorieCalculator
{
    public double calculateWalking(double steps, double distance, double time)
    {
        return steps * 0.04 + distance * 20 + time * 3.5;
    }

    public double calculateSwimming(double laps, double time, double heartRate)
    {
        return laps * 10 + time * 6 + heartRate * 0.5;
    }

    public double calculateJogging(double distance, double time, double heartRate)
    {
        return distance * 60 + time * 7 + heartRate * 0.3;
    }

    public double calculateTaiChi(double duration, double poses, double heartRate)
    {
        return duration * 3.5 + poses * 1.5 + heartRate * 0.25;
    }

    public double calculateGolf(double holes, double time, double distance)
    {
        return holes * 15 + time * 3 + distance * 25;
    }

    public double calculateRollerSkating(double distance, double time, double speed)
    {
        return distance * 40 + time * 5 + speed * 2;
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
