using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models.University;

public partial class UniversityDbContext : DbContext
{
    public UniversityDbContext()
    {
    }

    public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CountryEntity> Countries { get; set; }

    public virtual DbSet<RankingCriterionEntity> RankingCriteria { get; set; }

    public virtual DbSet<RankingSystemEntity> RankingSystems { get; set; }

    public virtual DbSet<UniversityEntity> Universities { get; set; }

    public virtual DbSet<UniversityRankingYearEntity> UniversityRankingYears { get; set; }

    public virtual DbSet<UniversityYearEntity> UniversityYears { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("data source=c:\\data\\university.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CountryEntity>(entity =>
        {
            entity.ToTable("country");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CountryName)
                .HasDefaultValueSql("NULL")
                .HasColumnName("country_name");
        });

        modelBuilder.Entity<RankingCriterionEntity>(entity =>
        {
            entity.ToTable("ranking_criteria");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CriteriaName)
                .HasDefaultValueSql("NULL")
                .HasColumnName("criteria_name");
            entity.Property(e => e.RankingSystemId)
                .HasDefaultValueSql("NULL")
                .HasColumnName("ranking_system_id");

            entity.HasOne(d => d.RankingSystem).WithMany(p => p.RankingCriteria).HasForeignKey(d => d.RankingSystemId);
        });

        modelBuilder.Entity<RankingSystemEntity>(entity =>
        {
            entity.ToTable("ranking_system");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.SystemName)
                .HasDefaultValueSql("NULL")
                .HasColumnName("system_name");
        });

        modelBuilder.Entity<UniversityEntity>(entity =>
        {
            entity.ToTable("university");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CountryId)
                .HasDefaultValueSql("NULL")
                .HasColumnName("country_id");
            entity.Property(e => e.UniversityName)
                .HasDefaultValueSql("NULL")
                .HasColumnName("university_name");

            entity.HasOne(d => d.Country).WithMany(p => p.Universities).HasForeignKey(d => d.CountryId);
        });

        modelBuilder.Entity<UniversityRankingYearEntity>(entity =>
        {
            entity
                .HasKey(e => new { e.RankingCriteriaId, e.UniversityId, e.Year });
            entity.ToTable("university_ranking_year");

            entity.Property(e => e.RankingCriteriaId)
                .HasDefaultValueSql("NULL")
                .HasColumnName("ranking_criteria_id");
            entity.Property(e => e.Score)
                .HasDefaultValueSql("NULL")
                .HasColumnName("score");
            entity.Property(e => e.UniversityId)
                .HasDefaultValueSql("NULL")
                .HasColumnName("university_id");
            entity.Property(e => e.Year)
                .HasDefaultValueSql("NULL")
                .HasColumnName("year");

            entity.HasOne(d => d.RankingCriteria).WithMany(p => p.UniversityRankingYearEntity).HasForeignKey(d => d.RankingCriteriaId);

            entity.HasOne(d => d.University).WithMany().HasForeignKey(d => d.UniversityId);
        });

        modelBuilder.Entity<UniversityYearEntity>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("university_year");

            entity.Property(e => e.NumStudents)
                .HasDefaultValueSql("NULL")
                .HasColumnName("num_students");
            entity.Property(e => e.PctFemaleStudents)
                .HasDefaultValueSql("NULL")
                .HasColumnName("pct_female_students");
            entity.Property(e => e.PctInternationalStudents)
                .HasDefaultValueSql("NULL")
                .HasColumnName("pct_international_students");
            entity.Property(e => e.StudentStaffRatio)
                .HasDefaultValueSql("NULL")
                .HasColumnName("student_staff_ratio");
            entity.Property(e => e.UniversityId)
                .HasDefaultValueSql("NULL")
                .HasColumnName("university_id");
            entity.Property(e => e.Year)
                .HasDefaultValueSql("NULL")
                .HasColumnName("year");

            entity.HasOne(d => d.University).WithMany().HasForeignKey(d => d.UniversityId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
