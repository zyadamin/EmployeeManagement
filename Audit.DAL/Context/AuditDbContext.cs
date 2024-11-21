using Audit.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Audit.DAL.Context
{
    public class AuditDbContext : DbContext
    {
        public AuditDbContext(DbContextOptions<AuditDbContext> options) : base(options)
        {
        }

        public DbSet<AuditLog> AuditLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AuditLog>(
                entity =>
                {
                    entity.Property(x => x.ActionType).HasConversion<int>();

                    entity.Property(a => a.OldData)
                                .HasColumnType("nvarchar(max)");

                    entity.Property(a => a.NewData)
                                .HasColumnType("nvarchar(max)");
                }
                );


        }
    }

}
