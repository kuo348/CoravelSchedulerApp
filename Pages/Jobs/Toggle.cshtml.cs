using CoravelSchedulerApp.Data;
using CoravelSchedulerApp.Models;
using CoravelSchedulerApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoravelSchedulerApp.Pages.Jobs
{

    public class ToggleModel : PageModel
    {
        private readonly CoravelContext _db;
        private readonly JobSchedulerService _scheduler;

        public ToggleModel(CoravelContext db, JobSchedulerService scheduler)
        {
            _db = db;
            _scheduler = scheduler;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var job = await _db.ScheduledJob.FindAsync(id);
            if (job == null) return NotFound();

            job.IsActive = !job.IsActive;
            _db.ScheduledJob.Update(job);
            await _db.SaveChangesAsync();

            await _scheduler.ScheduleAllJobsAsync(); // 重新載入排程
            return RedirectToPage("./Index");
        }
    }
}
