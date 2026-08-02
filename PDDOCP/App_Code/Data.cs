using System.Collections.Generic;

public class Data
{
    private static Dictionary<string, int> failedAttempts = new Dictionary<string, int>();
    private static Dictionary<string, System.DateTime> lockedUntil = new Dictionary<string, System.DateTime>();

    public static int getFailedAttempts(string name)
    {
        if (failedAttempts.ContainsKey(name)) return failedAttempts[name];
        return 0;
    }

    public static void addFailedAttempt(string name)
    {
        if (!failedAttempts.ContainsKey(name)) failedAttempts[name] = 0;
        failedAttempts[name] = failedAttempts[name] + 1;
        if (failedAttempts[name] >= 3) lockedUntil[name] = System.DateTime.Now.AddMinutes(1);
    }

    public static void clearFailedAttempt(string name)
    {
        if (failedAttempts.ContainsKey(name)) failedAttempts.Remove(name);
        if (lockedUntil.ContainsKey(name)) lockedUntil.Remove(name);
    }

    public static bool isLocked(string name)
    {
        if (lockedUntil.ContainsKey(name))
        {
            if (System.DateTime.Now < lockedUntil[name]) return true;
            clearFailedAttempt(name);
        }
        return false;
    }
}
