using CoravelSchedulerApp.Data;
using CoravelSchedulerApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoravelSchedulerApp.Pages.Jobs
{

    public class IndexModel : PageModel
    {
        private readonly CoravelContext _db;
        public List<ScheduledJob> Jobs { get; set; } = new();

        public IndexModel(CoravelContext db) => _db = db;

        public async Task OnGetAsync()
        {
            if(_db.ScheduledJob!=null)
            Jobs = await _db.ScheduledJob.OrderByDescending(j => j.CreatedAt).ToListAsync();
        }
    }
}
