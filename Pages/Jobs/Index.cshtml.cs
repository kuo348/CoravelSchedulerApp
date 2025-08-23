using CoravelSchedulerApp.Data;
using CoravelSchedulerApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoravelSchedulerApp.Pages.Jobs;

public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    public List<ScheduledJob> Jobs { get; set; } = new();

    public IndexModel(AppDbContext db) => _db = db;

    public async Task OnGetAsync()
    {
        Jobs = await _db.ScheduledJobs.OrderByDescending(j => j.CreatedAt).ToListAsync();
    }
}
