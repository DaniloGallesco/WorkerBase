using ClubeAss.Domain;
using ClubeAss.Repository.Ef.Maper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ClubeAss.Repository.Ef.Context
{
    [ExcludeFromCodeCoverage]
    public class BaseContext : DbContext
    {

        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Ignore<ValidationResult>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfiguration(new CustomerMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
