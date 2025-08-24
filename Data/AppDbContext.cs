
using CoravelSchedulerApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace CoravelSchedulerApp.Data
{
    public partial class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //warning To protect potentially sensitive information in your connection string,
                //you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration -
                //see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=Coravel;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
        public virtual DbSet<ScheduledJob>ScheduledJobs { get; set; }
        public virtual DbSet<JobExecutionLog>JobExecutionLogs { get; set; }
        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScheduledJob>(entity =>
            {
                entity.HasKey(k => k.Id);
            });
            modelBuilder.Entity<JobExecutionLog>(entity =>
            {
                entity.HasKey(k => k.Id);
            });
            OnModelCreatingPartial(modelBuilder);
        }

        

    }
}
