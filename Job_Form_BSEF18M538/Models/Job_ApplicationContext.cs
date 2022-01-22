using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Job_Form_BSEF18M538.Models
{
    public partial class Job_ApplicationContext : DbContext
    {
        public Job_ApplicationContext()
        {
        }

        public Job_ApplicationContext(DbContextOptions<Job_ApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Application> Applications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                optionsBuilder.UseSqlServer("Data Source=SQL5053.site4now.net,1433;Initial Catalog=db_a765d3_bsef18m538;User Id=db_a765d3_bsef18m538_admin;Password=uzairbsef18m538");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Application>(entity =>
            {
                entity.ToTable("Application");

                entity.Property(e => e.Already)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Applied).HasMaxLength(50);

                entity.Property(e => e.Cnic)
                    .HasMaxLength(50)
                    .HasColumnName("CNIC");

                entity.Property(e => e.ContactNo).HasMaxLength(50);

                entity.Property(e => e.Disability)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Dob)
                    .HasMaxLength(50)
                    .HasColumnName("DOB");

                entity.Property(e => e.Domicile).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Fname).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.Hafiz)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Marital).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.OtherContactNo).HasMaxLength(50);

                entity.Property(e => e.PermanentAddress).HasMaxLength(50);

                entity.Property(e => e.Place).HasMaxLength(50);

                entity.Property(e => e.PostalAddress).HasMaxLength(50);

                entity.Property(e => e.Religion).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
