using System;
using System.Linq;
using CMS.Systems.StockManagement.Data.Helpers;
using CMS.Systems.StockManagement.Entities.BaseEntities;
using CMS.Systems.StockManagement.Entities.StockRoot;
using CMS.Systems.StockManagement.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CMS.Systems.StockManagement.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<VehicleStock> VehicleStocks { get; set; }
        public DbSet<VehicleStockImage> VehicleStockImages { get; set; }
        public DbSet<VehicleStockAccessory> VehicleStockAccessories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            EntityConfigurationHelper.ConfigureEntityBase<Accessory>(builder);
            EntityConfigurationHelper.ConfigureEntityBase<VehicleStock>(builder);
            EntityConfigurationHelper.ConfigureEntityBase<VehicleStockImage>(builder);

            EntityConfigurationHelper.ConfigureManyKeyBase<VehicleStockAccessory>(builder);
            builder.Entity<VehicleStockAccessory>()
                .HasKey(c => new { c.AccessoryId, c.VehicleStockId });
        }
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            var now = DateTime.UtcNow;

            //newly created SoftDeleteBase
            foreach (var item in ChangeTracker.Entries<SoftDeleteBase>().Where(e => e.State == EntityState.Added))
            {
                item.Property("CreatedDate").CurrentValue = now;
                item.Property("ModifiedDate").CurrentValue = now;
            }

            //modified SoftDeleteBase
            foreach (var item in ChangeTracker.Entries<SoftDeleteBase>().Where(e => e.State == EntityState.Modified))
            {
                item.Property("ModifiedDate").CurrentValue = now;
            }

            return base.SaveChanges();
        }
    }
}
