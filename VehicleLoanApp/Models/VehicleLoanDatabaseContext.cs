using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using VehicleLoanApp.ViewModel;

#nullable disable

namespace VehicleLoanApp.Models
{
    public partial class VehicleLoanDatabaseContext : DbContext
    {
        public VehicleLoanDatabaseContext()
        {
        }

        public VehicleLoanDatabaseContext(DbContextOptions<VehicleLoanDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminLogin> AdminLogins { get; set; }
        public virtual DbSet<ApplicantDetail> ApplicantDetails { get; set; }
        public virtual DbSet<EmploymentDetail> EmploymentDetails { get; set; }
        public virtual DbSet<IdentityDocument> IdentityDocuments { get; set; }
        public virtual DbSet<LoanApplicationStatus> LoanApplicationStatuses { get; set; }
        public virtual DbSet<LoanDetail> LoanDetails { get; set; }
        public virtual DbSet<LoanScheme> LoanSchemes { get; set; }
        public virtual DbSet<VehicleDetail> VehicleDetails { get; set; }
        public virtual DbSet<PendingList> PendingList { get; set; }
        public virtual DbSet<AcceptedList> AcceptedList { get; set; }
        public virtual DbSet<RejectedList> RejectedList { get; set; }
        public virtual DbSet<ApplicationDetailed> ApplicationDetailed { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=.\\sqlexpress;database=VehicleLoanDatabase;trusted_connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AdminLogin>(entity =>
            {
                entity.HasKey(e => e.AdminId)
                    .HasName("pk_application_status");

                entity.ToTable("AdminLogin");

                entity.Property(e => e.AdminPassword)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ApplicantDetail>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("pk_applicant_details");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmploymentDetail>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("pk_employment_details");

                entity.Property(e => e.ExistingEmi).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TypeOfEmployement)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.YearlySalary).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.EmploymentDetails)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employmen__Custo__36B12243");
            });

            modelBuilder.Entity<IdentityDocument>(entity =>
            {
                entity.HasKey(e => e.IdentityId)
                    .HasName("pk_identity_documents");

                entity.Property(e => e.Adharcard)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.Pancard)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.Salaryslip)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.IdentityDocuments)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_identity_documents");
            });

            modelBuilder.Entity<LoanApplicationStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("pk_loan_application_status");

                entity.ToTable("LoanApplicationStatus");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LoanDetail>(entity =>
            {
                entity.HasKey(e => e.LoanId)
                    .HasName("pk_loan_details");

                entity.Property(e => e.LoanAmount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.LoanDetails)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LoanDetai__Custo__38996AB5");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.LoanDetails)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_loan_details_status_id");
            });

            modelBuilder.Entity<LoanScheme>(entity =>
            {
                entity.HasKey(e => e.SchemeId)
                    .HasName("pk_loan_scheme");

                entity.ToTable("LoanScheme");

                entity.Property(e => e.AccountType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Emi).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MaxLoanAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProcessingFee).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SchemeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.LoanSchemes)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_loan_scheme");
            });

            modelBuilder.Entity<VehicleDetail>(entity =>
            {
                entity.HasKey(e => e.VehicleId)
                    .HasName("pk_vehicle");

                entity.Property(e => e.CarMake)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CarModel)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ExshowroomPrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OnroadPrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.VehicleDetails)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VehicleDe__Custo__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
