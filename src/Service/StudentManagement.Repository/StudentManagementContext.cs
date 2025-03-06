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
            entity.HasKey(e => e.CourseId).HasName(EntityConstants.Course_PrimaryKey);

            entity.ToTable(EntityConstants.Course_TableName);

            entity.Property(e => e.CourseId)
                .UseIdentityAlwaysColumn()
                .HasColumnName(EntityConstants.CourseId);
            entity.Property(e => e.CourseName)
                .HasMaxLength(20)
                .HasColumnName(EntityConstants.CourseName);
            entity.Property(e => e.Duration)
                .HasMaxLength(20)
                .HasColumnName(EntityConstants.Duration);
            entity.Property(e => e.Fees).HasColumnName(EntityConstants.Fees);
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName(EntityConstants.IsActive);
        });

        modelBuilder.Entity<CourseDetail>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName(EntityConstants.CourseDetail_PrimaryKey);

            entity.ToTable(EntityConstants.CourseDetail_TableName);

            entity.Property(e => e.EnrollmentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName(EntityConstants.EnrollmentId);
            entity.Property(e => e.CourseId).HasColumnName(EntityConstants.CourseId);
            entity.Property(e => e.EnrollmentDate).HasColumnName(EntityConstants.EnrollmentDate);
            entity.Property(e => e.Grade)
                .HasMaxLength(1)
                .HasColumnName(EntityConstants.Grade);
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName(EntityConstants.IsActive);
            entity.Property(e => e.StudentId).HasColumnName(EntityConstants.StudentId);

            entity.HasOne(d => d.Course).WithMany(p => p.CourseDetails)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(EntityConstants.CourseForeignKey);

            entity.HasOne(d => d.GradeNavigation).WithMany(p => p.CourseDetails)
                .HasPrincipalKey(p => p.Grade1)
                .HasForeignKey(d => d.Grade)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(EntityConstants.GradeForeignKey);

            entity.HasOne(d => d.Student).WithMany(p => p.CourseDetails)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(EntityConstants.StudentForeignKey);
        });
        modelBuilder.Entity<FeesDetail>(entity =>
        {
            entity.HasKey(e => e.FeesDetailId).HasName(EntityConstants.FeesDetail_PrimaryKey);

            entity.ToTable(EntityConstants.FeesDetail_TableName);

            entity.Property(e => e.FeesDetailId)
                .UseIdentityAlwaysColumn()
                .HasColumnName(EntityConstants.FeesDetailId);
            entity.Property(e => e.AmountPaid).HasColumnName(EntityConstants.AmountPaid);
            entity.Property(e => e.EnrollmentId).HasColumnName(EntityConstants.EnrollmentId);
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName(EntityConstants.IsActive);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasColumnName(EntityConstants.PaymentStatus);

            entity.HasOne(d => d.Enrollment).WithMany(p => p.FeesDetails)
                .HasForeignKey(d => d.EnrollmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(EntityConstants.EnrollmentForeignKey);
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName(EntityConstants.Grade_PrimaryKey);

            entity.ToTable(EntityConstants.Grade_TableName);

            entity.HasIndex(e => e.Grade1, EntityConstants.GradeKey).IsUnique();

            entity.Property(e => e.GradeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName(EntityConstants.GradeId);
            entity.Property(e => e.Grade1)
                .HasMaxLength(1)
                .HasColumnName(EntityConstants.Grade);
            entity.Property(e => e.ScoreRange)
                .HasMaxLength(20)
                .HasColumnName(EntityConstants.ScoreRange);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName(EntityConstants.Student_PrimaryKey);

            entity.ToTable(EntityConstants.Student_TableName);

            entity.Property(e => e.StudentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName(EntityConstants.StudentId);
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName(EntityConstants.Address);
            entity.Property(e => e.Dob).HasColumnName(EntityConstants.Dob);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName(EntityConstants.Email);
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName(EntityConstants.IsActive);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName(EntityConstants.PhoneNumber);
            entity.Property(e => e.StudentName)
                .HasMaxLength(50)
                .HasColumnName(EntityConstants.StudentName);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
