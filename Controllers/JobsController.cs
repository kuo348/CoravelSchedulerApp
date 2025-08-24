using Microsoft.AspNetCore.Mvc;
using CoravelSchedulerApp.Data;
using CoravelSchedulerApp.Services;
using CoravelSchedulerApp.Models;
namespace CoravelSchedulerApp.Controllers 
{
[ApiController]
[Route("api/jobs")]
public class JobsController : ControllerBase
{
    private readonly CoravelContext _db;
    private readonly JobSchedulerService _schedulerService;

    public JobsController(CoravelContext db, JobSchedulerService schedulerService)
    {
        _db = db;
        _schedulerService = schedulerService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateJob([FromBody] ScheduledJob job)
    {
        job.CreatedAt = DateTime.Now;
        job.IsActive =false;

        _db.ScheduledJob.Add(job);
        await _db.SaveChangesAsync();

        await _schedulerService.ScheduleAllJobsAsync();

        return Ok("排程已新增並啟用");
    }
}
}