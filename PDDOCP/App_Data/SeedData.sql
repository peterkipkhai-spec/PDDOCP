-- Seed data for the mandatory Fitness Tracker activities.
-- The IF checks stop duplicate rows being inserted when the script is run more than once.

IF NOT EXISTS (SELECT 1 FROM ActivityType WHERE activityName = 'Walking')
BEGIN
    INSERT INTO ActivityType(activityName) VALUES('Walking');
END

IF NOT EXISTS (SELECT 1 FROM ActivityType WHERE activityName = 'Swimming')
BEGIN
    INSERT INTO ActivityType(activityName) VALUES('Swimming');
END

IF NOT EXISTS (SELECT 1 FROM MetricType WHERE metricName = 'steps')
BEGIN
    INSERT INTO MetricType(metricName) VALUES('steps');
END

IF NOT EXISTS (SELECT 1 FROM MetricType WHERE metricName = 'distance')
BEGIN
    INSERT INTO MetricType(metricName) VALUES('distance');
END

IF NOT EXISTS (SELECT 1 FROM MetricType WHERE metricName = 'time')
BEGIN
    INSERT INTO MetricType(metricName) VALUES('time');
END

IF NOT EXISTS (SELECT 1 FROM MetricType WHERE metricName = 'laps')
BEGIN
    INSERT INTO MetricType(metricName) VALUES('laps');
END

IF NOT EXISTS (SELECT 1 FROM MetricType WHERE metricName = 'heart_rate')
BEGIN
    INSERT INTO MetricType(metricName) VALUES('heart_rate');
END

DECLARE @walkingId INT;
DECLARE @swimmingId INT;
DECLARE @stepsId INT;
DECLARE @distanceId INT;
DECLARE @timeId INT;
DECLARE @lapsId INT;
DECLARE @heartRateId INT;

SELECT @walkingId = activityId FROM ActivityType WHERE activityName = 'Walking';
SELECT @swimmingId = activityId FROM ActivityType WHERE activityName = 'Swimming';
SELECT @stepsId = metricId FROM MetricType WHERE metricName = 'steps';
SELECT @distanceId = metricId FROM MetricType WHERE metricName = 'distance';
SELECT @timeId = metricId FROM MetricType WHERE metricName = 'time';
SELECT @lapsId = metricId FROM MetricType WHERE metricName = 'laps';
SELECT @heartRateId = metricId FROM MetricType WHERE metricName = 'heart_rate';

IF NOT EXISTS (SELECT 1 FROM ActivityMetric WHERE activityId = @walkingId AND metricId = @stepsId)
    INSERT INTO ActivityMetric(activityId, metricId, baseMultiplier) VALUES(@walkingId, @stepsId, 0.04);
IF NOT EXISTS (SELECT 1 FROM ActivityMetric WHERE activityId = @walkingId AND metricId = @distanceId)
    INSERT INTO ActivityMetric(activityId, metricId, baseMultiplier) VALUES(@walkingId, @distanceId, 20);
IF NOT EXISTS (SELECT 1 FROM ActivityMetric WHERE activityId = @walkingId AND metricId = @timeId)
    INSERT INTO ActivityMetric(activityId, metricId, baseMultiplier) VALUES(@walkingId, @timeId, 3.5);
IF NOT EXISTS (SELECT 1 FROM ActivityMetric WHERE activityId = @swimmingId AND metricId = @lapsId)
    INSERT INTO ActivityMetric(activityId, metricId, baseMultiplier) VALUES(@swimmingId, @lapsId, 10);
IF NOT EXISTS (SELECT 1 FROM ActivityMetric WHERE activityId = @swimmingId AND metricId = @timeId)
    INSERT INTO ActivityMetric(activityId, metricId, baseMultiplier) VALUES(@swimmingId, @timeId, 6);
IF NOT EXISTS (SELECT 1 FROM ActivityMetric WHERE activityId = @swimmingId AND metricId = @heartRateId)
    INSERT INTO ActivityMetric(activityId, metricId, baseMultiplier) VALUES(@swimmingId, @heartRateId, 0.5);
