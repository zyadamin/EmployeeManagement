using EmployeeManagement.Domian.Entity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DAL.Context
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<Project> Project { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
            .HasMany(x => x.Projects)
            .WithOne(x => x.Employee)
            .HasForeignKey(x => x.EmployeeId);

            modelBuilder.Entity<Employee>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Employee>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Project>().HasQueryFilter(p => !p.IsDeleted);
        }

    }
}
