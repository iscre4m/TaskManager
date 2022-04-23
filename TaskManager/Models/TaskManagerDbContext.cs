using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models
{
    public sealed class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
    }
}