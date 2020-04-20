using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ResearchProfilerRepo.Database.Models;

namespace ResearchProfilerRepo.Database
{
    public partial class ResearcherProfilerRepositoryContext : DbContext
    {
        public ResearcherProfilerRepositoryContext()
        {
        }

        public ResearcherProfilerRepositoryContext(DbContextOptions<ResearcherProfilerRepositoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aggregation> Aggregation { get; set; }
        public virtual DbSet<GlobalMeasure> GlobalMeasure { get; set; }
        public virtual DbSet<Measure> Measure { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Threshold> Threshold { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("host=localhost;database=ResearcherProfilerRepository_DEV;Username=postgres;Password=Sherman01");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Aggregation>(entity =>
            {
                entity.ToTable("aggregation");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.Property(e => e.ParentName).HasColumnName("parent_name");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.ParentNameNavigation)
                    .WithMany(p => p.InverseParentNameNavigation)
                    .HasForeignKey(d => d.ParentName)
                    .HasConstraintName("aggregation_parent_name_fkey");
            });

            modelBuilder.Entity<GlobalMeasure>(entity =>
            {
                entity.ToTable("global_measure");

                entity.HasIndex(e => new { e.DateMeasured, e.AggregateId })
                    .HasName("global_measure_date_measured_aggregate_id_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AggregateId).HasColumnName("aggregate_id");

                entity.Property(e => e.DateMeasured)
                    .HasColumnName("date_measured")
                    .HasColumnType("date");

                entity.Property(e => e.MaximumValue).HasColumnName("maximum_value");

                entity.Property(e => e.Mean).HasColumnName("mean");

                entity.Property(e => e.Median).HasColumnName("median");

                entity.Property(e => e.MinimumValue).HasColumnName("minimum_value");

                entity.Property(e => e.StandardDeviation).HasColumnName("standard_deviation");

                entity.HasOne(d => d.Aggregate)
                    .WithMany(p => p.GlobalMeasure)
                    .HasForeignKey(d => d.AggregateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("global_measure_aggregate_id_fkey");
            });

            modelBuilder.Entity<Measure>(entity =>
            {
                entity.ToTable("measure");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AggregateMeasured).HasColumnName("aggregate_measured");

                entity.Property(e => e.DateMeasured)
                    .HasColumnName("date_measured")
                    .HasColumnType("date");

                entity.Property(e => e.PersonMeasured)
                    .IsRequired()
                    .HasColumnName("person_measured")
                    .HasMaxLength(9);

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.AggregateMeasuredNavigation)
                    .WithMany(p => p.Measure)
                    .HasForeignKey(d => d.AggregateMeasured)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("measure_aggregate_measured_fkey");

                entity.HasOne(d => d.PersonMeasuredNavigation)
                    .WithMany(p => p.Measure)
                    .HasForeignKey(d => d.PersonMeasured)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("measure_person_measured_fkey");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Mnumber)
                    .HasName("person_pkey");

                entity.ToTable("person");

                entity.HasIndex(e => e.Email)
                    .HasName("person_email_key")
                    .IsUnique();

                entity.Property(e => e.Mnumber)
                    .HasColumnName("mnumber")
                    .HasMaxLength(9);

                entity.Property(e => e.Department)
                    .HasColumnName("department")
                    .HasColumnType("character varying");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("character varying");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasColumnType("character varying");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Threshold>(entity =>
            {
                entity.ToTable("threshold");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Aggregation).HasColumnName("aggregation");

                entity.Property(e => e.ThresholdEnd).HasColumnName("threshold_end");

                entity.Property(e => e.ThresholdName)
                    .IsRequired()
                    .HasColumnName("threshold_name")
                    .HasColumnType("character varying");

                entity.Property(e => e.ThresholdStart).HasColumnName("threshold_start");

                entity.HasOne(d => d.AggregationNavigation)
                    .WithMany(p => p.Threshold)
                    .HasForeignKey(d => d.Aggregation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("threshold_aggregation_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
