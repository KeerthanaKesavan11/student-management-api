using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StudentManagement.Constants;

namespace StudentManagement.Models.Models;

public partial class StudentManagementContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public StudentManagementContext(DbContextOptions<StudentManagementContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseDetail> CourseDetails { get; set; }

    public virtual DbSet<FeesDetail> FeesDetails { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName(EntityConstants.Course.PrimaryKey);

            entity.ToTable(EntityConstants.Course.TableName);

            entity.Property(e => e.CourseId)
                .UseIdentityAlwaysColumn()
                .HasColumnName(EntityConstants.Course.CourseId);
            entity.Property(e => e.CourseName)
                .HasMaxLength(20)
                .HasColumnName(EntityConstants.Course.CourseName);
            entity.Property(e => e.Duration)
                .HasMaxLength(20)
                .HasColumnName(EntityConstants.Course.Duration);
            entity.Property(e => e.Fees).HasColumnName(EntityConstants.Course.Fees);
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName(EntityConstants.Course.IsActive);
        });

        modelBuilder.Entity<CourseDetail>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName(EntityConstants.CourseDetail.PrimaryKey);

            entity.ToTable(EntityConstants.CourseDetail.TableName);

            entity.Property(e => e.EnrollmentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName(EntityConstants.CourseDetail.EnrollmentId);
            entity.Property(e => e.CourseId).HasColumnName(EntityConstants.CourseDetail.CourseId);
            entity.Property(e => e.EnrollmentDate).HasColumnName(EntityConstants.CourseDetail.EnrollmentDate);
            entity.Property(e => e.Grade)
                .HasMaxLength(1)
                .HasColumnName(EntityConstants.CourseDetail.Grade);
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName(EntityConstants.CourseDetail.IsActive);
            entity.Property(e => e.StudentId).HasColumnName(EntityConstants.CourseDetail.StudentId);

            entity.HasOne(d => d.Course).WithMany(p => p.CourseDetails)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(EntityConstants.CourseDetail.CourseForeignKey);

            entity.HasOne(d => d.GradeNavigation).WithMany(p => p.CourseDetails)
                .HasPrincipalKey(p => p.Grade1)
                .HasForeignKey(d => d.Grade)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(EntityConstants.CourseDetail.GradeForeignKey);

            entity.HasOne(d => d.Student).WithMany(p => p.CourseDetails)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(EntityConstants.CourseDetail.StudentForeignKey);
        });
        modelBuilder.Entity<FeesDetail>(entity =>
        {
            entity.HasKey(e => e.FeesDetailId).HasName(EntityConstants.FeesDetail.PrimaryKey);

            entity.ToTable(EntityConstants.FeesDetail.TableName);

            entity.Property(e => e.FeesDetailId)
                .UseIdentityAlwaysColumn()
                .HasColumnName(EntityConstants.FeesDetail.FeesDetailId);
            entity.Property(e => e.AmountPaid).HasColumnName(EntityConstants.FeesDetail.AmountPaid);
            entity.Property(e => e.EnrollmentId).HasColumnName(EntityConstants.FeesDetail.EnrollmentId);
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName(EntityConstants.FeesDetail.IsActive);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasColumnName(EntityConstants.FeesDetail.PaymentStatus);

            entity.HasOne(d => d.Enrollment).WithMany(p => p.FeesDetails)
                .HasForeignKey(d => d.EnrollmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(EntityConstants.FeesDetail.EnrollmentForeignKey);
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName(EntityConstants.Grade.PrimaryKey);

            entity.ToTable(EntityConstants.Grade.TableName);

            entity.HasIndex(e => e.Grade1, EntityConstants.Grade.GradeKey).IsUnique();

            entity.Property(e => e.GradeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName(EntityConstants.Grade.GradeId);
            entity.Property(e => e.Grade1)
                .HasMaxLength(1)
                .HasColumnName(EntityConstants.Grade.Grade1);
            entity.Property(e => e.ScoreRange)
                .HasMaxLength(20)
                .HasColumnName(EntityConstants.Grade.ScoreRange);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName(EntityConstants.Student.PrimaryKey);

            entity.ToTable(EntityConstants.Student.TableName);

            entity.Property(e => e.StudentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName(EntityConstants.Student.StudentId);
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName(EntityConstants.Student.Address);
            entity.Property(e => e.Dob).HasColumnName(EntityConstants.Student.Dob);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName(EntityConstants.Student.Email);
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName(EntityConstants.Student.IsActive);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName(EntityConstants.Student.PhoneNumber);
            entity.Property(e => e.StudentName)
                .HasMaxLength(50)
                .HasColumnName(EntityConstants.Student.StudentName);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
