using System;
using System.ComponentModel.DataAnnotations;

namespace CMS.Systems.StockManagement.Entities.BaseEntities
{
    public class EntityBase : SoftDeleteBase
    {
        [Key]
        public int Id { get; set; }
    }
}
