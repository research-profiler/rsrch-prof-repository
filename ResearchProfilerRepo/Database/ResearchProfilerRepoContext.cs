using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ResearchProfilerRepo.Database
{
    public partial class ResearchProfilerRepoContext : DbContext
    {
        public ResearchProfilerRepoContext()
        {
        }

        public ResearchProfilerRepoContext(DbContextOptions<ResearchProfilerRepoContext> options)
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("host=localhost;database=ResearchProfilerRepo;Username=postgres;Password=Sherman01");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aggregation>(entity =>
            {
                entity.Property(e => e.AggregationId)
                    .HasColumnName("aggregationId")
                    .ValueGeneratedNever();

                entity.Property(e => e.AggregateType)
                    .IsRequired()
                    .HasColumnName("aggregateType")
                    .HasColumnType("character varying");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.InputColumn)
                    .IsRequired()
                    .HasColumnName("inputColumn")
                    .HasColumnType("character varying");

                entity.Property(e => e.InputCondition)
                    .HasColumnName("inputCondition")
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.Property(e => e.ParentAggregation).HasColumnName("parentAggregation");

                entity.HasOne(d => d.AggregationNavigation)
                    .WithOne(p => p.InverseAggregationNavigation)
                    .HasForeignKey<Aggregation>(d => d.AggregationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("child_of");
            });

            modelBuilder.Entity<GlobalMeasure>(entity =>
            {
                entity.HasKey(e => new { e.AggregateId, e.DateMeasured })
                    .HasName("GlobalMeasure_pkey");

                entity.Property(e => e.AggregateId).HasColumnName("aggregateId");

                entity.Property(e => e.DateMeasured)
                    .HasColumnName("dateMeasured")
                    .HasColumnType("date");

                entity.Property(e => e.Mean).HasColumnName("mean");

                entity.Property(e => e.Median).HasColumnName("median");

                entity.Property(e => e.Range).HasColumnName("range");

                entity.Property(e => e.StandardDeviation).HasColumnName("standardDeviation");

                entity.HasOne(d => d.Aggregate)
                    .WithMany(p => p.GlobalMeasure)
                    .HasForeignKey(d => d.AggregateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("child_of");
            });

            modelBuilder.Entity<Measure>(entity =>
            {
                entity.HasKey(e => new { e.PersonId, e.AggregateId, e.DateMeasured })
                    .HasName("Measure_pkey");

                entity.Property(e => e.PersonId)
                    .HasColumnName("personId")
                    .HasMaxLength(9);

                entity.Property(e => e.AggregateId).HasColumnName("aggregateId");

                entity.Property(e => e.DateMeasured)
                    .HasColumnName("dateMeasured")
                    .HasColumnType("time with time zone")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.JobTitle)
                    .IsRequired()
                    .HasColumnName("jobTitle")
                    .HasColumnType("character varying");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Measure)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("measure_of");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Ucid)
                    .HasName("Person_pkey");

                entity.Property(e => e.Ucid)
                    .HasColumnName("ucid")
                    .HasMaxLength(9);

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasColumnName("department")
                    .HasColumnType("character varying");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("character varying");

                entity.Property(e => e.First)
                    .IsRequired()
                    .HasColumnName("first")
                    .HasColumnType("character varying");

                entity.Property(e => e.Last)
                    .IsRequired()
                    .HasColumnName("last")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Threshold>(entity =>
            {
                entity.HasKey(e => new { e.AggregateId, e.ThresholdId })
                    .HasName("Threshold_pkey");

                entity.Property(e => e.AggregateId).HasColumnName("aggregateId");

                entity.Property(e => e.ThresholdId).HasColumnName("thresholdId");

                entity.Property(e => e.EndValue).HasColumnName("endValue");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.Property(e => e.StartValue).HasColumnName("startValue");

                entity.HasOne(d => d.Aggregate)
                    .WithMany(p => p.Threshold)
                    .HasForeignKey(d => d.AggregateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("child_of");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
