using Coravel;
using CoravelSchedulerApp.Data;
using CoravelSchedulerApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("mssqlconnection"));

builder.Services.AddScheduler();
builder.Services.AddScoped<JobSchedulerService>();
builder.Services.AddHttpClient();

var app = builder.Build();

app.Services.UseScheduler(async scheduler =>
{
    using var scope = app.Services.CreateScope();
    var jobService = scope.ServiceProvider.GetRequiredService<JobSchedulerService>();
    await jobService.ScheduleAllJobsAsync();
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
