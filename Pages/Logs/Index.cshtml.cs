public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    public List<JobExecutionLog> Logs { get; set; } = new();

    public IndexModel(AppDbContext db) => _db = db;

    public async Task OnGetAsync()
    {
        Logs = await _db.JobExecutionLogs
            .Include(l => l.Job)
            .OrderByDescending(l => l.ExecutedAt)
            .Take(100)
            .ToListAsync();
    }
}
