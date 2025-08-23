using System;
using System.Collections.Generic;
namespace CoravelSchedulerApp.Models
{
    public class JobExecutionLog
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public ScheduledJob Job { get; set; } = null!;
        public DateTime ExecutedAt { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
    }
}
