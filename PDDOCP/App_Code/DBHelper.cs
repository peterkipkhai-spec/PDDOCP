using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class DBHelper
{
    private SqlConnection con;

    public DBHelper()
    {
        string cs = ConfigurationManager.ConnectionStrings["FitTrackConnectionString"].ConnectionString;
        con = new SqlConnection(cs);
    }

    public void openConnection()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Database connection open error: " + ex.Message);
        }
    }

    public void closeConnection()
    {
        try
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Database connection close error: " + ex.Message);
        }
    }

    public int registerMember(Member member)
    {
        try
        {
            openConnection();
            string query = "INSERT INTO Member(name, password) VALUES(@name, @password)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", member.getName());
            cmd.Parameters.AddWithValue("@password", member.getPassword());
            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception("Register member error: " + ex.Message);
        }
        finally { closeConnection(); }
    }

    public Member loginMember(string name, string password)
    {
        try
        {
            openConnection();
            string query = "SELECT mId, name, password FROM Member WHERE name=@name AND password=@password";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@password", password);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return new Member(Convert.ToInt32(dr["mId"]), dr["name"].ToString(), dr["password"].ToString());
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception("Login member error: " + ex.Message);
        }
        finally { closeConnection(); }
    }

    public DataTable getActivities()
    {
        return getTable("SELECT activityId, activityName FROM ActivityType ORDER BY activityName");
    }

    public DataTable getMetrics()
    {
        return getTable("SELECT metricId, metricName FROM MetricType ORDER BY metricName");
    }

    public DataTable getActivityMetrics()
    {
        return getTable("SELECT am.activityMetricId, at.activityName, mt.metricName, am.baseMultiplier FROM ActivityMetric am INNER JOIN ActivityType at ON am.activityId=at.activityId INNER JOIN MetricType mt ON am.metricId=mt.metricId ORDER BY at.activityName, mt.metricName");
    }

    public DataTable getActivityMetricsByActivity(int activityId)
    {
        try
        {
            openConnection();
            string query = "SELECT am.activityMetricId, mt.metricName, am.baseMultiplier FROM ActivityMetric am INNER JOIN MetricType mt ON am.metricId=mt.metricId WHERE am.activityId=@activityId ORDER BY mt.metricName";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@activityId", activityId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { throw new Exception("Get activity metrics error: " + ex.Message); }
        finally { closeConnection(); }
    }

    public int insertActivity(string activityName)
    {
        return executeNonQuery("INSERT INTO ActivityType(activityName) VALUES(@activityName)", new string[] { "@activityName" }, new object[] { activityName });
    }

    public int insertMetric(string metricName)
    {
        return executeNonQuery("INSERT INTO MetricType(metricName) VALUES(@metricName)", new string[] { "@metricName" }, new object[] { metricName });
    }

    public int insertActivityMetric(ActivityMetric activityMetric)
    {
        return executeNonQuery("INSERT INTO ActivityMetric(activityId, metricId, baseMultiplier) VALUES(@activityId, @metricId, @baseMultiplier)", new string[] { "@activityId", "@metricId", "@baseMultiplier" }, new object[] { activityMetric.getActivityId(), activityMetric.getMetricId(), activityMetric.getBaseMultiplier() });
    }

    public int insertGoal(FitnessGoal goal)
    {
        return executeNonQuery("INSERT INTO FitnessGoal(targetCalories, memberId, createdDate) VALUES(@targetCalories, @memberId, @createdDate)", new string[] { "@targetCalories", "@memberId", "@createdDate" }, new object[] { goal.getTargetCalories(), goal.getMemberId(), goal.getCreatedDate() });
    }

    public DataTable getGoalsByMember(int memberId)
    {
        return getTableWithMember("SELECT goalId, targetCalories, createdDate FROM FitnessGoal WHERE memberId=@memberId ORDER BY createdDate DESC", memberId);
    }

    public int insertWorkoutSession(WorkoutSession session)
    {
        return executeNonQuery("INSERT INTO WorkoutSession(activityMetricId, memberId, recordedValue, sessionDate) VALUES(@activityMetricId, @memberId, @recordedValue, @sessionDate)", new string[] { "@activityMetricId", "@memberId", "@recordedValue", "@sessionDate" }, new object[] { session.getActivityMetricId(), session.getMemberId(), session.getRecordedValue(), session.getSessionDate() });
    }

    public DataTable getProgressByMember(int memberId)
    {
        return getTableWithMember("SELECT ws.sessionId, at.activityName, mt.metricName, ws.recordedValue, ws.sessionDate FROM WorkoutSession ws INNER JOIN ActivityMetric am ON ws.activityMetricId=am.activityMetricId INNER JOIN ActivityType at ON am.activityId=at.activityId INNER JOIN MetricType mt ON am.metricId=mt.metricId WHERE ws.memberId=@memberId ORDER BY ws.sessionDate DESC", memberId);
    }

    public double getTotalCaloriesByMember(int memberId)
    {
        try
        {
            openConnection();
            string query = "SELECT ISNULL(SUM(recordedValue),0) FROM WorkoutSession WHERE memberId=@memberId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@memberId", memberId);
            return Convert.ToDouble(cmd.ExecuteScalar());
        }
        catch (Exception ex) { throw new Exception("Total calories error: " + ex.Message); }
        finally { closeConnection(); }
    }

    private DataTable getTable(string query)
    {
        try
        {
            openConnection();
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { throw new Exception("Get table error: " + ex.Message); }
        finally { closeConnection(); }
    }

    private DataTable getTableWithMember(string query, int memberId)
    {
        try
        {
            openConnection();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@memberId", memberId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex) { throw new Exception("Get member table error: " + ex.Message); }
        finally { closeConnection(); }
    }

    private int executeNonQuery(string query, string[] names, object[] values)
    {
        try
        {
            openConnection();
            SqlCommand cmd = new SqlCommand(query, con);
            for (int i = 0; i < names.Length; i++) cmd.Parameters.AddWithValue(names[i], values[i]);
            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex) { throw new Exception("Database save error: " + ex.Message); }
        finally { closeConnection(); }
    }
}
