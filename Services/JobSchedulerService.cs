
using Coravel.Scheduling.Schedule.Interfaces;
using CoravelSchedulerApp.Data;
using Microsoft.EntityFrameworkCore;
namespace CoravelSchedulerApp.Services
{
    public class JobSchedulerService
    {
        private readonly IScheduler _scheduler;
        private readonly AppDbContext _db;

        public JobSchedulerService(IScheduler scheduler, AppDbContext db)
        {
            _scheduler = scheduler;
            _db = db;
        }

        /*public async Task ScheduleAllJobsAsync()
        {


            var q = from x in _db.scheduledJob
                    where x.IsActive == "Y"
                    select x;
            //var jobs = await _db.ScheduledJobs.Where(j => j.IsActive.Equals("Y")).ToListAsync();
            var jobs = q.ToList();
            foreach (var job in jobs)
            {
                _scheduler.Schedule(() => ExecuteJob(job.JobName))
                          .Cron(job.CronExpression);
            }
        }*/

        public void ExecuteJob(string jobName)
        {
            Console.WriteLine($"執行任務：{jobName}，時間：{DateTime.Now}");
        }
        public async Task ScheduleAllJobsAsync()
{
    var jobs = await _db.ScheduledJobs.Where(j => j.IsActive).ToListAsync();

    foreach (var job in jobs)
    {
        _scheduler.Schedule(() => ExecuteJob(job))
                  .Cron(job.CronExpression);
    }
}

public async Task ExecuteJob(ScheduledJob job)
{
    var log = new JobExecutionLog
    {
        JobId = job.Id,
        ExecutedAt = DateTime.Now
    };

    try
    {
        switch (job.Type)
        {
            case JobType.Console:
                Console.WriteLine($"[Console] 執行任務：{job.JobName}");
                break;
            case JobType.Email:
                await SendEmailAsync(job.Payload ?? "無內容");
                break;
            case JobType.ApiCall:
                await CallApiAsync(job.Payload ?? "");
                break;
        }

        log.IsSuccess = true;
        log.Message = "執行成功";
    }
    catch (Exception ex)
    {
        log.IsSuccess = false;
        log.Message = $"錯誤：{ex.Message}";
    }

    _db.JobExecutionLogs.Add(log);
    await _db.SaveChangesAsync();
}

private Task SendEmailAsync(string content)
{
    Console.WriteLine($"📧 寄送 Email：{content}");
    return Task.CompletedTask;
}

private async Task CallApiAsync(string url)
{
    using var client = new HttpClient();
    var response = await client.GetAsync(url);
    Console.WriteLine($"🌐 API 回應：{response.StatusCode}");
}

    }
}