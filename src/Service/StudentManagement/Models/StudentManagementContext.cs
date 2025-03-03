using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.API.Models;

public partial class StudentManagementContext : DbContext
{
    public StudentManagementContext()
    {
    }

    public StudentManagementContext(DbContextOptions<StudentManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseDetail> CourseDetails { get; set; }

    public virtual DbSet<FeesDetail> FeesDetails { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=student_management;Username=postgres;Password=Welcome@123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("course_pkey");

            entity.ToTable("course");

            entity.Property(e => e.CourseId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("course_id");
            entity.Property(e => e.CourseName)
                .HasMaxLength(20)
                .HasColumnName("course_name");
            entity.Property(e => e.Duration)
                .HasMaxLength(20)
                .HasColumnName("duration");
            entity.Property(e => e.Fees).HasColumnName("fees");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
        });

        modelBuilder.Entity<CourseDetail>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("course_detail_pkey");

            entity.ToTable("course_detail");

            entity.Property(e => e.EnrollmentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("enrollment_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.EnrollmentDate).HasColumnName("enrollment_date");
            entity.Property(e => e.Grade)
                .HasMaxLength(1)
                .HasColumnName("grade");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseDetails)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("course_detail_course_id_fkey");

            entity.HasOne(d => d.GradeNavigation).WithMany(p => p.CourseDetails)
                .HasPrincipalKey(p => p.Grade1)
                .HasForeignKey(d => d.Grade)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("course_detail_grade_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.CourseDetails)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("course_detail_student_id_fkey");
        });

        modelBuilder.Entity<FeesDetail>(entity =>
        {
            entity.HasKey(e => e.FeesDetailId).HasName("fees_detail_pkey");

            entity.ToTable("fees_detail");

            entity.Property(e => e.FeesDetailId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("fees_detail_id");
            entity.Property(e => e.AmountPaid).HasColumnName("amount_paid");
            entity.Property(e => e.EnrollmentId).HasColumnName("enrollment_id");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasColumnName("payment_status");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.FeesDetails)
                .HasForeignKey(d => d.EnrollmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fees_detail_enrollment_id_fkey");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("grade_pkey");

            entity.ToTable("grade");

            entity.HasIndex(e => e.Grade1, "grade_grade_key").IsUnique();

            entity.Property(e => e.GradeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("grade_id");
            entity.Property(e => e.Grade1)
                .HasMaxLength(1)
                .HasColumnName("grade");
            entity.Property(e => e.ScoreRange)
                .HasMaxLength(20)
                .HasColumnName("score_range");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("student_pkey");

            entity.ToTable("student");

            entity.Property(e => e.StudentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("student_id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.StudentName)
                .HasMaxLength(50)
                .HasColumnName("student_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
