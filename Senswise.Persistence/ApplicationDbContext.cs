using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Senswise.Domain.Entities;
using System.Reflection;

namespace Senswise.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly SqlDbOptions _dbOptions;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<SqlDbOptions> dbOptions) : base(options)
        {
            _dbOptions = dbOptions.Value;
        }

        public DbSet<Customer> Customers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_dbOptions.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditableProperties();
            return await base.SaveChangesAsync(cancellationToken);
        }
        private void SetAuditableProperties()
        {
            var datas = ChangeTracker.Entries<AuditableEntity>();
            foreach (var item in datas)
            {
                if (item.State == EntityState.Added)
                {
                    if (item.Entity.CreatedDate == DateTime.MinValue) item.Entity.CreatedDate = DateTime.UtcNow;
                    if (string.IsNullOrEmpty(item.Entity.CreatedBy)) item.Entity.CreatedBy = "customer_api";
                }
                if (item.State == EntityState.Modified)
                {
                    if (!item.Entity.UpdatedDate.HasValue) item.Entity.UpdatedDate = DateTime.UtcNow;
                }
            }
        }
    }
}
