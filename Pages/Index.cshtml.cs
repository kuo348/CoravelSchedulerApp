using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CoravelSchedulerApp.Data;
using CoravelSchedulerApp.Nodels;
using CoravelSchedulerApp.Services;

namespace CoravelSchedulerApp{

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
     private readonly AppDbContext _db;
    private readonly JobSchedulerService _scheduler;

    [BindProperty]
    public string JobName { get; set; }

    [BindProperty]
    public string CronExpression { get; set; }
    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    public IndexModel(AppDbContext db, JobSchedulerService scheduler)
    {
        _db = db;
        _scheduler = scheduler;
    }
    public void OnGet()
    {

    }
    public async Task<IActionResult> OnPostAsync()
    {
        var job = new ScheduledJob
        {
            JobName = JobName,
            CronExpression = CronExpression,
            IsActive = true,
            CreatedAt = DateTime.Now
        };

        _db.ScheduledJobs.Add(job);
        await _db.SaveChangesAsync();
        await _scheduler.ScheduleAllJobsAsync();

        return RedirectToPage();
    }
}
}