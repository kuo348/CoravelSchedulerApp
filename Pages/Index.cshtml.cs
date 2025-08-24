using CoravelSchedulerApp.Data;
using CoravelSchedulerApp.Models;
using CoravelSchedulerApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace CoravelSchedulerApp.Pages
{

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
     private readonly CoravelContext _db;
    private readonly JobSchedulerService _scheduler;
[BindProperty] public string? JobName { get; set; }
[BindProperty] public string? CronExpression { get; set; }
[BindProperty] public int Type { get; set; }
[BindProperty] public string? Payload { get; set; }

      //public IndexModel(ILogger<IndexModel> logger)
    //{
    //    _logger = logger;
    //}
    public IndexModel(CoravelContext db, JobSchedulerService scheduler, ILogger<IndexModel> logger)
    {
        _db = db;
        _scheduler = scheduler;
            _logger = logger;
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
        JobType = Type,
        Payload = Payload,
        IsActive = true,
        CreatedAt = DateTime.Now
    };

    _db.ScheduledJob.Add(job);
    await _db.SaveChangesAsync();
    await _scheduler.ScheduleAllJobsAsync();

    return RedirectToPage("/Jobs/Index");
}

}
}