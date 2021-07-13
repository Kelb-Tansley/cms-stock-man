using System;

namespace CMS.Systems.StockManagement.Entities.BaseEntities
{
    public class SoftDeleteBase
    {
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
