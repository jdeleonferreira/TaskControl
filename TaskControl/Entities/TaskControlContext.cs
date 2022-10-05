using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace TaskControl.Entities
{
    public partial class TaskControlContext : DbContext
    {
        public TaskControlContext()
        {
        }

        public TaskControlContext(DbContextOptions<TaskControlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<TaskMember> TaskMembers { get; set; } = null!;
        public virtual DbSet<TaskRecord> TaskRecords { get; set; } = null!;
        public virtual DbSet<TaskRole> TaskRoles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Workspace> Workspaces { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.HasIndex(e => e.Email, "Company_Email_Unique")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasMany(d => d.Workspaces)
                    .WithMany(p => p.Companies)
                    .UsingEntity<Dictionary<string, object>>(
                        "CompanyWorkspace",
                        l => l.HasOne<Workspace>().WithMany().HasForeignKey("WorkspaceId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_CompanyWorkspace_Workspace"),
                        r => r.HasOne<Company>().WithMany().HasForeignKey("CompanyId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_CompanyWorkspace_Company"),
                        j =>
                        {
                            j.HasKey("CompanyId", "WorkspaceId");

                            j.ToTable("CompanyWorkspace");
                        });
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Status");

                entity.HasOne(d => d.Workspace)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.WorkspaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Workspace");
            });

            modelBuilder.Entity<TaskMember>(entity =>
            {
                entity.HasKey(e => new { e.TaskId, e.UserId });

                entity.ToTable("TaskMember");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskMembers)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskMember_Task");

                entity.HasOne(d => d.TaskRole)
                    .WithMany(p => p.TaskMembers)
                    .HasForeignKey(d => d.TaskRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskMember_TaskRole");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TaskMembers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskMember_User");
            });

            modelBuilder.Entity<TaskRecord>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TaskRecord");

                entity.Property(e => e.Action)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TaskRole>(entity =>
            {
                entity.ToTable("TaskRole");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RoleName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "Unique_User_Email")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fullname)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Company");
            });

            modelBuilder.Entity<Workspace>(entity =>
            {
                entity.ToTable("Workspace");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
