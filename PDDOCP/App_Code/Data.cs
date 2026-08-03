using System.Threading;

// This static class follows the simple lecture style for storing failed login attempts.
// Static variables keep their values while the web application is running.
public class Data
{
    private static int failedLoginCount = 0;
    private static string lastFailedUser = "";

    public static int getFailedLoginCount()
    {
        return failedLoginCount;
    }

    public static void addFailedAttempt(string name)
    {
        // If a different user tries to login, begin counting again for that username.
        if (lastFailedUser != name)
        {
            lastFailedUser = name;
            failedLoginCount = 0;
        }

        failedLoginCount = failedLoginCount + 1;

        // Video 3 style lockout: pause the request after three failed attempts.
        if (failedLoginCount >= 3)
        {
            Thread.Sleep(3000);
        }
    }

    public static void clearFailedAttempt()
    {
        failedLoginCount = 0;
        lastFailedUser = "";
    }
}
