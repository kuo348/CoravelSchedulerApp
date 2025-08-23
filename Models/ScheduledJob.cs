namespace CoravelSchedulerApp {
public class ScheduledJob
{
    public int Id { get; set; }
    public string JobName { get; set; }
    public string CronExpression { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
}