using CoravelSchedulerApp.Data;
using CoravelSchedulerApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoravelSchedulerApp.Pages.Jobs;

public class ToggleModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly JobSchedulerService _scheduler;

    public ToggleModel(AppDbContext db, JobSchedulerService scheduler)
    {
        _db = db;
        _scheduler = scheduler;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var job = await _db.ScheduledJobs.FindAsync(id);
        if (job == null) return NotFound();

        job.IsActive = !job.IsActive;
        await _db.SaveChangesAsync();

        await _scheduler.ScheduleAllJobsAsync(); // 重新載入排程
        return RedirectToPage("./Index");
    }
}
