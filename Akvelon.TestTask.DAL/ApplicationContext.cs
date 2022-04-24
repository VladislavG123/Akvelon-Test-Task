using Akvelon.TestTask.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Akvelon.TestTask.DAL;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    #region DbSets

    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    #endregion
}