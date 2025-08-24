using CoravelSchedulerApp.Data;
using CoravelSchedulerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoravelSchedulerApp.Pages.Jobs
{

    public class EditModel : PageModel
    {
        private readonly CoravelContext _db;

        [BindProperty] public ScheduledJob Job { get; set; } = new();

        public EditModel(CoravelContext db) => _db = db;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Job = await _db.ScheduledJob.FindAsync(id);
            return Job == null ? NotFound() : Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var jobInDb = await _db.ScheduledJob.FindAsync(Job.Id);
            if (jobInDb == null) return NotFound();

            jobInDb.JobName = Job.JobName;
            jobInDb.CronExpression = Job.CronExpression;
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
