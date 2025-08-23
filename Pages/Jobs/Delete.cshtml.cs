using CoravelSchedulerApp.Data;
using CoravelSchedulerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoravelSchedulerApp.Pages.Jobs;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _db;

    public DeleteModel(AppDbContext db) => _db = db;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var job = await _db.ScheduledJobs.FindAsync(id);
        if (job == null) return NotFound();

        _db.ScheduledJobs.Remove(job);
        await _db.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
