using System.ComponentModel.DataAnnotations;
using CMS.Systems.StockManagement.Entities.BaseEntities;

namespace CMS.Systems.StockManagement.Entities.StockRoot
{
    public class Accessory : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
