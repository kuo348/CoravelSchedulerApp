using System;
using System.Collections.Generic;

namespace CoravelSchedulerApp.Models;

public partial class JobExecutionLog
{
    public int Id { get; set; }

    public int JobId { get; set; }

    public string JobName { get; set; } = null!;

    public string CronExpression { get; set; } = null!;

    public bool? IsSuccess { get; set; }

    public DateTime? ExecutedAt { get; set; }

    public string? Job { get; set; }

    public string? Message { get; set; }
}
