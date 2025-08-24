using CoravelSchedulerApp.Data;
using CoravelSchedulerApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoravelSchedulerApp.Pages.Logs
{
    public class IndexModel : PageModel
    {
        private readonly CoravelContext _db;
        public List<JobExecutionLog> Logs { get; set; } = new();

        public IndexModel(CoravelContext db) => _db = db;

        public async Task OnGetAsync()
        {
            Logs = await _db.JobExecutionLog
                //.Include(t => t.Job)
                .OrderByDescending(t => t.Id)
                .Take(20)
                .ToListAsync();
        }
    }
}
