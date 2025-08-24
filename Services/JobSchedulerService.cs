
using Coravel.Scheduling.Schedule.Interfaces;
using CoravelSchedulerApp.Data;
using CoravelSchedulerApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Microsoft.CodeAnalysis.CSharp.Scripting;
namespace CoravelSchedulerApp.Services
{
    public class JobSchedulerService
    {
        private readonly IScheduler _scheduler;
        private  CoravelContext _db;

        public JobSchedulerService(CoravelContext db,IScheduler scheduler)
        {
            _scheduler = scheduler;
            _db = db;
        }
        public static CoravelContext GetDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CoravelContext>();

            IConfiguration config = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                     .Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection2"));
            return new CoravelContext(optionsBuilder.Options);

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
            // var jobs = await _db.ScheduledJobs.Where(j => j.IsActive.Value.Equals(1)).ToListAsync();
            var q = from x in _db.ScheduledJob
                    where 1 == 1
                    select x;
            var jobs = q.ToList();
            foreach (var job in jobs)
            {
                
                _scheduler.Schedule(() => ExecuteJob(job))
                         .Cron(job.CronExpression);
            }
        }

        public async Task ExecuteJob(ScheduledJob job)
        {

            CoravelContext context = GetDbContext();
           
            JobExecutionLog log = new JobExecutionLog
            {
                JobId = job.Id,
                ExecutedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                Job ="",
                CronExpression=job.CronExpression.Trim(),
                JobName=job.JobName,
                IsSuccess = true,
                Message = "執行成功"
            };

            try
            {
                switch (job.JobType)
                {
                    case (int)JobType.Console:
                        //Console.WriteLine($"[Console] 執行任務：{job.JobName} at {DateTime.Now.ToString("HH:mm:ss")}");
                        string script = @"﻿int[] x = new int[7] { 1, 2, 3, 4, 5, 6, 7 };
int y=0;
for(var i=0;i<7;i++) y+=x[i] ;
return y;";
                        await CSharpScriptAsync(script);
                        break;
                    case (int)JobType.Email:
                        await SendEmailAsync(job.Payload ?? "無內容");
                        break;
                    case (int)JobType.ApiCall:
                        await CallApiAsync(job.Payload ?? "");
                        break;
                }

                context.JobExecutionLog.Add(log);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                log.IsSuccess = false;
                log.Message = $"錯誤：{ex.Message}";
                context.JobExecutionLog.Add(log);
                context.SaveChanges();
            }

           
            
        }

        private Task SendEmailAsync(string content)
        {
            Console.WriteLine($"寄送 Email：{content}");
            return Task.CompletedTask;
        }

        private async Task CallApiAsync(string url)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            Console.WriteLine($" API 回應：{response.StatusCode}");
        }
        private  Task CSharpScriptAsync(string content)
        {
            //var result= CSharpScript.RunAsync(content).Result;
            var s4 = CSharpScript.Create(content);
            var result= s4.RunAsync().Result;
            Console.WriteLine(result.ReturnValue);
            return Task.CompletedTask;
        }

    }
}