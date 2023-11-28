using CoursesApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoursesApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Signup> Signups { get; set; }
        public DbSet<CoursesCategory> CoursesCategories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<SignupMessage> SignupMessages { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonVideo> LessonVideos { get; set; }
        public DbSet<CourseDate> CourseDates { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<ForumCategory> ForumCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();

            modelBuilder.Entity<User>().ToTable("Users")
                .HasMany(u => u.Signups)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Signups)
                .WithOne(s => s.Course)
                .HasForeignKey(s => s.CourseId);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.CoursesCategory)
                .WithMany()
                .HasForeignKey(c => c.CourseCategoryId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Payments)
                .WithOne(p => p.Course)
                .HasForeignKey(p => p.CourseId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Lessons)
                .WithOne(l => l.Course)
                .HasForeignKey(l => l.CourseId);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.SignupMessage)
                .WithOne(sm => sm.Course)
                .HasForeignKey<Course>(c => c.SignupMessageId);

            modelBuilder.Entity<Signup>()
                .HasOne(s => s.CourseDate)
                .WithMany()
                .HasForeignKey(s => s.CourseDateId);

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.LessonVideo)
                .WithOne(lv => lv.Lesson)
                .HasForeignKey<LessonVideo>(lv => lv.Id);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ForumPosts)
                .WithOne(fp => fp.User)
                .HasForeignKey(fp => fp.UserId);

            modelBuilder.Entity<ForumCategory>()
                .HasMany(fc => fc.ForumPosts)
                .WithOne(fp => fp.ForumCategory)
                .HasForeignKey(fp => fp.CategoryId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId);

        }
    }
}