using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using CMS.Systems.StockManagement.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;

namespace CMS.Systems.StockManagement.Data.Helpers
{
    public class EntityConfigurationHelper
    {
        public static void ConfigureManyKeyBase<T>(ModelBuilder modelBuilder) where T : ManyKeyBase
        {
            //need to get table name here.
            //var tableName = GetTableName<T>();

            //modelBuilder.Entity<T>().Property(x => x.Id).HasColumnName(tableName + "Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<T>().Property(t => t.IsDeleted).HasDefaultValue(false);
            modelBuilder.Entity<T>().Property(t => t.CreatedDate).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<T>().Property(t => t.ModifiedDate).HasDefaultValue(DateTime.Now);
        }
        public static void ConfigureEntityBase<T>(ModelBuilder modelBuilder) where T : EntityBase
        {
            //need to get table name here.
            var tableName = GetTableName<T>();

            modelBuilder.Entity<T>().Property(x => x.Id).HasColumnName(tableName + "Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<T>().Property(t => t.IsDeleted).HasDefaultValue(false);
            modelBuilder.Entity<T>().Property(t => t.CreatedDate).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<T>().Property(t => t.ModifiedDate).HasDefaultValue(DateTime.Now);
        }
        private static string GetTableName<T>() where T : SoftDeleteBase
        {
            var tableName = string.Empty;
            var tAttribute = typeof(T).GetCustomAttributes<TableAttribute>(true);
            if (tAttribute.Any())
            {
                tableName = tAttribute.FirstOrDefault().Name;
            }
            //if the table attribute is not mentioned then use type name.
            return string.IsNullOrWhiteSpace(tableName) ? typeof(T).Name : tableName;
        }
    }
}
