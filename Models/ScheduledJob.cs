using System;
using System.Collections.Generic;
namespace CoravelSchedulerApp.Models
{
    public enum JobType
{
    Console,
    Email,
    ApiCall
}
public class ScheduledJob
    {
        public int Id { get; set; }
        public string JobName { get; set; }
        public string CronExpression { get; set; }
        public string IsActive { get; set; }
         public JobType Type { get; set; }
        public string? Payload { get; set; } // Email內容或API URL
        public DateTime CreatedAt { get; set; }
    }
}