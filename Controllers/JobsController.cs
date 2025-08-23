using Microsoft.AspNetCore.Mvc;
namespace CoravelSchedulerApp {
[ApiController]
[Route("api/jobs")]
public class JobsController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly JobSchedulerService _schedulerService;

    public JobsController(AppDbContext db, JobSchedulerService schedulerService)
    {
        _db = db;
        _schedulerService = schedulerService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateJob([FromBody] ScheduledJob job)
    {
        job.CreatedAt = DateTime.Now;
        job.IsActive = true;

        _db.ScheduledJobs.Add(job);
        await _db.SaveChangesAsync();

        await _schedulerService.ScheduleAllJobsAsync();

        return Ok("排程已新增並啟用");
    }
}
}