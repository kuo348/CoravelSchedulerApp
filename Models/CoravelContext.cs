using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CoravelSchedulerApp.Models;

public partial class CoravelContext : DbContext
{
    public CoravelContext(DbContextOptions<CoravelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<JobExecutionLog> JobExecutionLog { get; set; }

    public virtual DbSet<ScheduledJob> ScheduledJob { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JobExecutionLog>(entity =>
        {
           // entity.HasNoKey();
            //entity.HasKey(k => k.Id);
          
            entity.Property(e => e.CronExpression)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ExecutedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.IsSuccess).HasDefaultValue(false);
            entity.Property(e => e.Job)
                .HasMaxLength(500)
                .IsFixedLength();
            entity.Property(e => e.JobName).HasMaxLength(50);
            entity.Property(e => e.Message)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ScheduledJob>(entity =>
        {
            //entity.HasNoKey();
            //entity.HasKey(k => k.Id);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CronExpression)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.IsActive).HasDefaultValue(false);
            entity.Property(e => e.JobName).HasMaxLength(50);
            entity.Property(e => e.Payload)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
