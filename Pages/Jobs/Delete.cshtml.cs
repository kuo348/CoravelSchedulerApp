using CoravelSchedulerApp.Data;
using CoravelSchedulerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoravelSchedulerApp.Pages.Jobs
{

    public class DeleteModel : PageModel
    {
        private readonly CoravelContext _db;

        public DeleteModel(CoravelContext db) => _db = db;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var job = await _db.ScheduledJob.FindAsync(id);
            if (job == null) return NotFound();

            _db.ScheduledJob.Remove(job);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
