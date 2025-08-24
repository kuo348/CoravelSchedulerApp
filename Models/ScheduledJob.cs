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

    public partial class ScheduledJob
    {
        public int Id { get; set; }

        public string? JobName { get; set; } = null!;
        public int? JobType { get; set; }
        public string? CronExpression { get; set; } = null!;

        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? Payload { get; set; }
    }
}
