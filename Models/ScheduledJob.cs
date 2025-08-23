using System;
using System.Collections.Generic;
namespace CoravelSchedulerApp.Models
{
public class ScheduledJob
{
    public int Id { get; set; }
    public string JobName { get; set; }
    public string CronExpression { get; set; }
    public string IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
}